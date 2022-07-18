using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace To_do_list_NET_Club.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введіть електронну пошту")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введіть пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
