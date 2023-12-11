using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceModel
{
    public class SignUpRequestDTO
    {
        [Required(ErrorMessage ="Name is required")]
        public string Name {  get; set; }

        [Required(ErrorMessage ="Email is required")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$",ErrorMessage ="Invalid Email address")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Phone Number is required.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage ="Password is required.")]
        [DataType(DataType.Password)]
        public string Password {  get; set; }

        [Required(ErrorMessage = "ConfirmPassword is required.")]
        [DataType(DataType.Password)]

        [Compare("Password",ErrorMessage ="Password and confirm password is not matched")]
        public string ConfirmPassword { get; set; }
        

    }
}
