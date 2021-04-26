using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCCodeChallenge2.CustomExceptions
{
    public class BookNotFoundException:Exception
    {
        public BookNotFoundException(string message):base(message)
        {

        }
    }
}