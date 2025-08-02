using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Core
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string  message) : base(message){ }
    }

    public class ValidationException : Exception
    {
        public List<string> ValidationErrors { get; }
        public ValidationException(List<string> errors) 
        {
            ValidationErrors = errors;
        }
    }
}
