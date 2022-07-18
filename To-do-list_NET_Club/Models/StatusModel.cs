using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace To_do_list_NET_Club.Models
{
    public class StatusModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<NoteModel> Notes { get; set; }
    }
}
