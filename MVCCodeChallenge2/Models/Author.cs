using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCCodeChallenge2.Models
{
    public class Author
    {
        [Key]
        public int IdNo { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public List<Book>Books { get; set; } //depicts one to many relationship only
    }
}