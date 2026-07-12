using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public class OperationResult<T>
    {
        public T PayLead { get; set; }
        public bool IsError { get; set; } 
        public List<string> Errors { get; set; } = new List<string>();
    }
}
