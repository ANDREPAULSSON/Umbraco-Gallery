using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Umbraco_Gallery.ViewModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Please enter your name")]
        public string Name {get; set;}

        [Required(ErrorMessage = "Please enter your email address")]
        [EmailAddress(ErrorMessage = "You must enter a valid email address")]
        public string Email {get; set;}

        [Required(ErrorMessage = "Please enter your message")]
        [MaxLength(50, ErrorMessage = "Your message can't be longer than 500 characters")]
        public string Message {get; set;}

        public int ContactFormId {get; set;}
    }
}
