using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using To_do_list_NET_Club.Models;

namespace To_do_list_NET_Club.DataBase
{
    public class NotesContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<NoteModel> Notes { get; set; }
        public DbSet<StatusModel> Statuses { get; set; }

        public NotesContext(DbContextOptions<NotesContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
