using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using To_do_list_NET_Club.DataBase;
using To_do_list_NET_Club.Models;

namespace To_do_list_NET_Club
{
    public class SampleData
    {
        public static void Initialize (NotesContext context)
        {
            if (context.Users.Any())
            {
                return;
            }

            context.Users.AddRange(
                new UserModel
                {
                    Email = "Roman@gmail.com",
                    Password = "1111"
                }
                );
            context.SaveChanges();

            //context.Notes.AddRange(
            //    new NoteModel
            //    {
            //        Head = "Greeting",
            //        MainText = "Hello world"
            //    }
            //    );
            //context.SaveChanges();

            context.Statuses.AddRange(
                new StatusModel
                {
                    Name = "To Do"
                },
                new StatusModel
                {
                    Name = "In Progress"
                },
                new StatusModel
                {
                    Name = "Done"
                }
                );
            context.SaveChanges();
        }
    }
}
