using System.ComponentModel.DataAnnotations;
using System.Net;
using FluentValidation;
using System.Text.Json;
using Notes.Application.Common.Exceptions;

namespace Notes.WebAPI.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public CustomExceptionHandlerMiddleware(RequestDelegate _next)
        {
            next = _next;
        }

        //при выполненнии мидлвары именно этот метод вызывается и передается в него контекст запроса
        public async Task Invoke(HttpContext context) 
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }


        }

        //Если ошибка не валидации - выкидываем 500-ю ошибку.
        //Если валидации - то записываем и выкидываем все валидационные ошибки
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; //стандартный код ошибки

            var result = string.Empty; //текст ошибки

            switch (exception)
            {
                case FluentValidation.ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    result = JsonSerializer.Serialize(validationException.Errors);
                    break;// если ошибка валидации, то получаем все ошибки
                case NotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;//Если ошибка не найденной записки - кидаем 404
                default:
                    break;
            }

            context.Response.ContentType = "application/json"; //присваеваем тип ответа
            context.Response.StatusCode = (int)code; //И статус код

            if (result == string.Empty) //Если валидационных ошибок нет, то пишем в резульат первоначальную ошибку
            {
                result = JsonSerializer.Serialize(new { error = exception.Message });
            }

           
            return context.Response.WriteAsync(result); //Все записываем в ответ
        }
    }
}
