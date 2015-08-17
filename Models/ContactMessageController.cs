using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFirstApp.Models
{
    public class ContactMessage {
    
        [Required]
        public string contactName { get; set;}
        [Required]
        public string Message { get; set;}
        [Required]
        [EmailAddress]
        public string FromEmail { get; set;}
    }  
}