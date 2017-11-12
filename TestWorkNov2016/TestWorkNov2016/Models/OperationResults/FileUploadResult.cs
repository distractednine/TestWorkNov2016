using System;

namespace TestWorkNov2016.Models.OperationResults
{
    public class FileUploadResult : SingleOperationResult<string>
    {
        public FileUploadResult(string entity) : base(entity)
        {
        }

        public FileUploadResult(Exception error) : base(error)
        {
        }
    }
}