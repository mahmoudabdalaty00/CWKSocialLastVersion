using Domain.Models.Conasts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public class OperationResult<T>
    {
        public T? Result { get; set; }
        public bool IsError { get; set; } 
        public List<Error> Errors { get; set; } = new List<Error>();





        public void AddError(ErrorCodes code, string message)
        {
            IsError = true;
            Errors.Add(new Error { Code = code, Message = message });
        }

        public void AddErrors(IEnumerable<Error> errors)
        {
            IsError = true;
            Errors.AddRange(errors);
        }

        public void SetSuccess(T result)
        {
            Result = result;
        }
    }
}
