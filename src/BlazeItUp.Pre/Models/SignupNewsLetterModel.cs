using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazeItUp.Pre.Models
{
    public class SignupNewsLetterModel
    {
        [Required]
        [EmailAddress]
        public String Email { get; set; }
    }
}
