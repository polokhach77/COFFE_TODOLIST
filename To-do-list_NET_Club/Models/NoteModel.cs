using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace To_do_list_NET_Club.Models
{
    public class NoteModel
    {
        [Key]
        public int Id { get; set; }

        public UserModel User { get; set; }
        public StatusModel Status { get; set; }

        [StringLength(20)]
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Введіть заголовок нотатки")]
        public string Head { get; set; }

        [StringLength(200)]
        [Column(TypeName = "nvarchar(1000)")]
        [Required(ErrorMessage = "Введіть текст нотатки")]
        public string MainText { get; set; }

        [Required(ErrorMessage = "Оберіть дату початку")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "Оберіть дату закінчення")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? EndDate { get; set; }
    }
}

