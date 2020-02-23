using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodBase.Models
{
    public class Prod
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        // Cos probas

    }
}