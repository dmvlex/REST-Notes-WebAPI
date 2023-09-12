namespace Notes.WebAPI.Middleware
{
    public static class CustomExceptionHandlerMiddlewareExtentions
    {
        //Обычный метод расширения, работает как и с сервисами
        public static IApplicationBuilder UseCustomExceptionHandler(this 
            IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
