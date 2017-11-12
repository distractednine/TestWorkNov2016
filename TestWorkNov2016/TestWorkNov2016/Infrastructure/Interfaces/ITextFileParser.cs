using System.Collections.Generic;
using System.Threading.Tasks;
using TestWorkNov2016.Models.OperationResults;

namespace TestWorkNov2016.Infrastructure.Interfaces
{
    public interface ITextFileParser
    {
        Task<ICollection<MultipleOperationsResult>> ProcessUploadedFilesAsync(MultipleOperationsResult uploadResult);

        MultipleOperationsResult TryParseFile(string path);
    }
}