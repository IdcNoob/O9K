namespace O9K.Core.Extensions
{
    using Ensage.SDK.Handlers;

    public static class UpdateHandlerExtensions
    {
        public static void SetUpdateRate(this IUpdateHandler updateHandler, int rate)
        {
            if (updateHandler.Executor is TimeoutHandler timeoutHandler)
            {
                timeoutHandler.Timeout = rate;
            }
        }
    }
}