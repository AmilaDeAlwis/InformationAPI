using System;

namespace InformationAPI.Misc
{
    public class DataAccessException : Exception
    {
        public DataAccessException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }

    public class ServiceException : Exception
    {
        public ServiceException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
