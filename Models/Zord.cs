using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodBase.Models
{
    public class Zord
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Year { get; set; }
        
        //Foreign Key
        public int ProdId { get; set; }
        public Prod Prod { get; set; }
    }
}