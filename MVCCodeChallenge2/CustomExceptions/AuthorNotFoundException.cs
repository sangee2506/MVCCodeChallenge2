using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCCodeChallenge2.CustomExceptions
{
    public class AuthorNotFoundException : Exception
    {
       

        public AuthorNotFoundException(string message) : base(message)
        {
        }
    }
}