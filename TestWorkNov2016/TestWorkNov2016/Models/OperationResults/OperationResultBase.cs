using System;
using System.Collections.Generic;
using System.Linq;

namespace TestWorkNov2016.Models.OperationResults
{
    public abstract class OperationResultBase
    {
        protected OperationResultBase(Exception error)
        {
            Errors = new List<Exception> { error };
        }

        protected OperationResultBase(ICollection<Exception> errors)
        {
            Errors = errors;
        }

        public ICollection<Exception> Errors { get; }

        public bool IsSuccessful => !Errors.Any();

        public void AddError(Exception error)
        {
            Errors.Add(error);
        }
    }
}