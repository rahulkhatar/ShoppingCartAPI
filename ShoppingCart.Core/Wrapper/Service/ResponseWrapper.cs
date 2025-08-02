using ShoppingCart.Core.Wrapper.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Core.Wrapper.Service
{
    public class ResponseWrapper : IResponseWrapper
    {
        public ResponseWrapper()
        {

        }

        public List<string> Messages { get; set; } = new();
        public bool IsSuccessful { get; set; }
        public int TotalCount { get; set; }

        public static IResponseWrapper Fail()
        {
            return new ResponseWrapper() { IsSuccessful = false };
        }

        public static IResponseWrapper Fail(string message)
        {
            return new ResponseWrapper() { IsSuccessful = false, Messages = new List<string> { message} };
        }

        public static IResponseWrapper Fail(List<string> messages)
        {
            return new ResponseWrapper() { IsSuccessful = false, Messages = messages };
        }

        //async
        public static Task<IResponseWrapper> FailAsync()
        {
            return Task.FromResult(Fail());
        }

        public static Task<IResponseWrapper> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }

        public static Task<IResponseWrapper> FailAsync(List<string> messages)
        {
            return Task.FromResult(Fail(messages));
        }

        //success
        public static IResponseWrapper Success()
        {
            return new ResponseWrapper() { IsSuccessful = true };
        }

        public static IResponseWrapper Success(string message)
        {
            return new ResponseWrapper() { IsSuccessful = true, Messages = new List<string> { message } };
        }

        public static IResponseWrapper Success(List<string> messages)
        {
            return new ResponseWrapper() { IsSuccessful = true, Messages = messages };
        }

        //async
        public static Task<IResponseWrapper> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        public static Task<IResponseWrapper> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }

        public static Task<IResponseWrapper> SuccessAsync(List<string> messages)
        {
            return Task.FromResult(Success(messages));
        }
    }

    public class ResponseWrapper<T> : ResponseWrapper, IResponseWrapper<T>
    {
        public ResponseWrapper(){}

        public T ResponseData { get; set; }

        public new static ResponseWrapper<T> Fail()
        {
            return new ResponseWrapper<T> { IsSuccessful = false };
        }

        public new static ResponseWrapper<T> Fail(string message)
        {
            return new ResponseWrapper<T> { IsSuccessful = false, Messages = new List<string> { message } };
        }

        public new static ResponseWrapper<T> Fail(List<string> messages)
        {
            return new ResponseWrapper<T> { IsSuccessful = false, Messages = messages };
        }

        //async
        public new static Task<ResponseWrapper<T>> FailAsync()
        {
            return Task.FromResult(Fail());
        }

        public new static Task<ResponseWrapper<T>> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }

        public new static Task<ResponseWrapper<T>> FailAsync(List<string> messages)
        {
            return Task.FromResult(Fail(messages));
        }

        //success
        public new static ResponseWrapper<T> Success()
        {
            return new ResponseWrapper<T> { IsSuccessful = true };
        }

        public new static ResponseWrapper<T> Success(string message)
        {
            return new ResponseWrapper<T> { IsSuccessful = true, Messages = new List<string> { message } };
        }

        public new static ResponseWrapper<T> Success(List<string> messages)
        {
            return new ResponseWrapper<T> { IsSuccessful = true, Messages = messages };
        }

        //async
        public new static Task<ResponseWrapper<T>> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        public new static Task<ResponseWrapper<T>> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }

        public new static Task<ResponseWrapper<T>> SuccessAsync(List<string> messages)
        {
            return Task.FromResult(Success(messages));
        }

        //OTHER METHODS FOR T DATA
        public static ResponseWrapper<T> Success(T data)
        {
            return new ResponseWrapper<T> { IsSuccessful = true, ResponseData = data };
        }

        public static ResponseWrapper<T> Success(T data, string message)
        {
            return new ResponseWrapper<T> { IsSuccessful = true, ResponseData = data, Messages = new List<string> { message } };
        }

        public static ResponseWrapper<T> Success(T data, List<string> messages)
        {
            return new ResponseWrapper<T> { IsSuccessful = true, ResponseData = data, Messages = messages};
        }

        //ADDITIONAL
        public static ResponseWrapper<T> SuccessWithTotalCount(T data, int count)
        {
            return new ResponseWrapper<T> { IsSuccessful = true, ResponseData = data, TotalCount = count };
        }

        //async
        public static Task<ResponseWrapper<T>> SuccessWithTotalCountAsync(T data, int count)
        {
            return Task.FromResult(SuccessWithTotalCount(data, count));
        }

        public static ResponseWrapper<T> SuccessWithData(T data, string message)
        {
            return new ResponseWrapper<T> { IsSuccessful = true, ResponseData = data, Messages = new List<string> { message }, TotalCount = 1 };
        }

        //async
        public static Task<ResponseWrapper<T>> SuccessWithDataAsync(T data, string message)
        {
            return Task.FromResult(SuccessWithData(data, message));
        }
    }
}
