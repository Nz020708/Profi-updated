using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Profi.ViewModels
{
    public class CreateVM
    {
        [Required]
        public string Name { get; set; }
        [Required,NotMapped]
        public IFormFile Photo { get; set; }
        [Required]
        public string Theme { get; set; }
        [Required]
        public string Description { get; set; }
        [Required,RegularExpression(@"^\d+\.\d{0,2}$")]
        public int StarCount { get; set; }
    }
}
