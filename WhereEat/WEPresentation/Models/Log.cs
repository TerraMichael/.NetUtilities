using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WEPresentation.Models
{
    public class Log
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Restaurant Restaurant { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}