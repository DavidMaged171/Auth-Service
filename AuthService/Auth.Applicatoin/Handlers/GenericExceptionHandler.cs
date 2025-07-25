using Microsoft.Extensions.Logging;

namespace Auth.Applicatoin.Handlers
{
    public static class GenericExceptionHandler
    {
        public static T Handle<T>(Func<T> func,ILogger logger)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                logger.LogError(ex.StackTrace);
                throw;
            }
        }
    }
}
