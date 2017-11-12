using TestWorkNov2016.Models.OperationResults;

namespace TestWorkNov2016.Infrastructure.Interfaces
{
    public interface ITextParser<out T> where T : OperationResultBase
    {
        T TryParse(string str);
        T Parse(string str);
    }
}