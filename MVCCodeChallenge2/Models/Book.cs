using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCCodeChallenge2.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Genere { get; set; }
        public decimal Price { get; set; }
        public Author author { get; set; }

        
    }
}