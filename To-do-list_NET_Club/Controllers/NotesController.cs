using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using To_do_list_NET_Club.DataBase;
using To_do_list_NET_Club.Models;

namespace To_do_list_NET_Club.Controllers
{
    public class NotesController : Controller
    {
        private List<NoteModel> myNotes;
        private NotesContext db;


        public NotesController(NotesContext context)
        {
            db = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            myNotes = db.Notes.ToList();

            if (myNotes != null)
            {
                var dictionary = db.Notes
                    .Include(x => x.User)
                    .Include(y => y.Status)
                    .AsEnumerable()
                    .GroupBy(z => z.Status)
                    .OrderBy(w => w.Key.Id)
                    .ToDictionary(g => g.Key.Name, g => g.AsEnumerable());

                return View(dictionary);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create() 
        {
            ViewBag.Status = await db.Statuses.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NoteModel note)
        {
            note.User = db.Users.First(x => x.Email == User.Identity.Name);
            note.Status = db.Statuses.First(x => x.Id == note.Status.Id);

            if (note.EndDate <= note.StartDate)
            {
                ModelState.AddModelError("EndDate", "Нотатка не може закінчуватись швидше ніж почалась!");
            }

            if (ModelState.IsValid)
            {
                db.Notes.Add(note);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", myNotes);
            }
            else
            {
                ViewBag.Status = await db.Statuses.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToListAsync();
                return View(myNotes);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Status = await db.Statuses.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToListAsync();

            if (id == null)
            {
                return NotFound();
            }

            var note = await db.Notes.FindAsync(id);

            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NoteModel note)
        {
            ViewBag.Status = await db.Statuses.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToListAsync();

            note.User = db.Users.First(x => x.Email == User.Identity.Name);
            note.Status = db.Statuses.First(x => x.Id == note.Status.Id);

            if (id != note.Id)
            {
                return NotFound();
            }

            if (note.EndDate <= note.StartDate)
            {
                ModelState.AddModelError("EndDate", "Нотатка не може закінчуватись швидше ніж почалась!");
            }

            if (ModelState.IsValid)
            {
                db.Update(note);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", myNotes);
            }
            else
            {
                return View(note);
            }
        }


        public async Task<IActionResult> MoveNext(int id, NoteModel note)
        {
            note = await db.Notes.Include(y => y.Status).FirstAsync(x => x.Id == id);
            var nexStatus = db.Statuses.OrderBy(x => x.Id).AsEnumerable().SkipWhile(item => item.Id != note.Status.Id).Skip(1).FirstOrDefault();

            if (nexStatus != null)
            {
                note.Status = nexStatus;
                db.Update(note);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index", myNotes);
        }

        public async Task<IActionResult> MoveBack(int id, NoteModel note)
        {
            note = await db.Notes.Include(y => y.Status).FirstAsync(x => x.Id == id);
            var prevStatus = db.Statuses.OrderByDescending(x => x.Id).AsEnumerable().SkipWhile(item => item.Id != note.Status.Id).Skip(1).FirstOrDefault();

            if (prevStatus != null)
            {
                note.Status = prevStatus;
                db.Update(note);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index", myNotes);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            myNotes = await db.Notes.ToListAsync();

            if (id.HasValue && myNotes.Any(x => x.Id == id))
            {
                NoteModel noteModel = myNotes.Find(x => x.Id == id);
                db.Notes.Remove(noteModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", myNotes);
            }

            return RedirectToAction("NotExists");
        }
    }
}
