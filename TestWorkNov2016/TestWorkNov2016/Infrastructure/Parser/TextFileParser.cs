using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestWorkNov2016.Infrastructure.Interfaces;
using TestWorkNov2016.Models.OperationResults;

namespace TestWorkNov2016.Infrastructure.Parser
{
    public class TextFileParser : ITextFileParser
    {
        private readonly ITextParser<StationParsingResult> _parser;
        private readonly ConcurrentBag<StationParsingResult> _resultBag;

        public TextFileParser(ITextParser<StationParsingResult> parser)
        {
            _parser = parser;
            _resultBag = new ConcurrentBag<StationParsingResult>();
        }

        public async Task<ICollection<MultipleOperationsResult>> ProcessUploadedFilesAsync(MultipleOperationsResult uploadResult)
        {
            var res = await Task.Run(() => ProcessUploadedFiles(uploadResult));
            return res;
        }

        private ICollection<MultipleOperationsResult> ProcessUploadedFiles(MultipleOperationsResult uploadResult)
        {
            var parsingResults = new ConcurrentBag<MultipleOperationsResult>();

            Parallel.ForEach(
                    uploadResult.SuccessfulOperations,
                    file => ParseUploadResultFile(file, parsingResults));

            return parsingResults.ToList();
        }

        private void ParseUploadResultFile(OperationResultBase fileUpload, ConcurrentBag<MultipleOperationsResult> parsingResults)
        {
            var fileUplodResult = (FileUploadResult)fileUpload;
            var res = TryParseFile(fileUplodResult.Entity);

            parsingResults.Add(res);
        }

        public MultipleOperationsResult TryParseFile(string path)
        {
            try
            {
                return ParseFile(path);
            }
            catch (AggregateException exception)
            {
                return new MultipleOperationsResult(exception);
            }
        }

        private MultipleOperationsResult ParseFile(string path)
        {
            var lines = File.ReadAllLines(path);

            Parallel.ForEach(lines, ParseOne);

            return new MultipleOperationsResult(_resultBag.ToArray());
        }

        private void ParseOne(string line)
        {
            _resultBag.Add(_parser.TryParse(line));
        }
    }
}