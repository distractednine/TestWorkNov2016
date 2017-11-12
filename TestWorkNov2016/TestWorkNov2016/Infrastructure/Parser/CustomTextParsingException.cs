using System;

namespace TestWorkNov2016.Infrastructure.Parser
{
    public class CustomTextParsingException : Exception
    {
        public CustomTextParsingException(string message, string parsedString) 
            : base(message)
        {
            ParsedString = parsedString;
        }

        public string ParsedString { get; }
    }
}