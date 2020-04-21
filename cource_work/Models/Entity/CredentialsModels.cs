using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace cource_work.Models.Entity
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Треба логін")]
        public string Login { get; set; }

        [Required(ErrorMessage ="Треба роль")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Треба працівника")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Треба пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Треба однакові")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Треба логін")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Треба пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
