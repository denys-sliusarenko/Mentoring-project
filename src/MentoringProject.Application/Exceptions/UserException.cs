using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProject.Application.Exceptions
{
    public class UserException : Exception
    {
        public UserException() { }

        public UserException(string message)
            : base(message)
        {

        }
    }
}
