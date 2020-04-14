using System;

namespace IT.Employer.Services.Exceptions.Common
{
    public class InvalidPaginationParametersException : Exception
    {
        private const string ExceptionMessage = "Pagination parameters are invalid. Page / PerPage parameters should be >= 0.";

        public InvalidPaginationParametersException()
            : base(ExceptionMessage) { }

        public InvalidPaginationParametersException(string message)
            : base(message) { }

        public InvalidPaginationParametersException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
