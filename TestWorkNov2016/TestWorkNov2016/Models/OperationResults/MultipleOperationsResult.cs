using System;
using System.Collections.Generic;
using System.Linq;

namespace TestWorkNov2016.Models.OperationResults
{
    public class MultipleOperationsResult : OperationResultBase
    {
        public MultipleOperationsResult()
            : this(new List<OperationResultBase>())
        {
        }

        public MultipleOperationsResult(ICollection<OperationResultBase> results) 
            : base(new List<Exception>())
        {
            Results = results;
        }

        public MultipleOperationsResult(Exception error) : base(error)
        {
            Results = new List<OperationResultBase>();
        }

        public MultipleOperationsResult(ICollection<Exception> errors) : base(errors)
        {
            Results = new List<OperationResultBase>();
        }

        public ICollection<OperationResultBase> Results { get; set; }

        public IEnumerable<OperationResultBase> SuccessfulOperations =>
            Results.Where(x => x.IsSuccessful);

        public IEnumerable<OperationResultBase> UnsuccessfulOperations =>
            Results.Where(x => !x.IsSuccessful);

        public void AddFileResult(OperationResultBase operationResult)
        {
            Results.Add(operationResult);
        }
    }
}