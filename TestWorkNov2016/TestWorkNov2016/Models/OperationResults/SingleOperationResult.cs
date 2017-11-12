using System;
using System.Collections.Generic;

namespace TestWorkNov2016.Models.OperationResults
{
    public class SingleOperationResult<T> : OperationResultBase
    {
        public SingleOperationResult(T entity) 
            : base(new List<Exception>())
        {
            Entity = entity;
        }

        public SingleOperationResult(Exception error) 
            : base(new List<Exception> {error})
        {
        }

        public SingleOperationResult(ICollection<Exception> errors) 
            : base(errors)
        {
        }

        public T Entity { get; }
    }
}