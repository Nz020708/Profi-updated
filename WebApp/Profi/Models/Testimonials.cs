using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Profi.Models
{
    public class Testimonials
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Theme { get; set; }
        public string Description { get; set; }
        public int Star { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
