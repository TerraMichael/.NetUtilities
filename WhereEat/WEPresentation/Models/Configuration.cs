using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static WEPresentation.Enums.Enums;

namespace WEPresentation.Models
{
    public class Configuration
    {
        [Key]
        public int Id { get; set; }
        public Category Category { get; set; }
        public bool NiceTryMode { get; set; }
    }
}