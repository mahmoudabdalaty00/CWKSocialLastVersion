using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public class OperationResult<T>
    {
        public T Result { get; set; }
        public bool IsError { get; set; } 
        public List<Error> Errors { get; set; } = new List<Error>();
    }
}
