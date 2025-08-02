using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Core.Wrapper.Interface
{
    public interface IResponseWrapper
    {
        List<string> Messages { get; set; }
        bool IsSuccessful { get; set; }        
    }

    public interface IResponseWrapper<out T> : IResponseWrapper
    {
        T ResponseData { get; }
    }
}
