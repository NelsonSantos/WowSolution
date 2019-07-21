using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Net;

namespace WowWebApi.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorFilter : IExceptionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public ErrorFilter(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceptionContext"></param>
        public void OnException(ExceptionContext exceptionContext)
        {
            var _response = new ErrorResponse();

            var _exceptionType = exceptionContext.Exception.GetType();
            var _isProduction = true;

#if DEBUG
            _isProduction = false;
#endif
            switch (_exceptionType.Name)
            {
                case nameof(UnauthorizedAccessException):
                    _response.Message = exceptionContext.Exception.Message;
                    _response.HttpStatusCode = HttpStatusCode.Unauthorized;
                    break;

                case nameof(TimeoutException):
                    _response.Message = "O servidor demorou para responder, tente novamente.";
                    _response.HttpStatusCode = HttpStatusCode.NotImplemented;
                    break;
                case nameof(NotImplementedException):
                    _response.Message = "Funcionalidade não implementada.";
                    _response.HttpStatusCode = HttpStatusCode.NotImplemented;
                    break;
                case nameof(Exception):
                case nameof(SqlException):
                default:
                    var _guid = Guid.NewGuid().ToString();
                    //var _message = $"Id: {_guid}\r\n\r\n{GetInnerException(exceptionContext.Exception)}";
                    var _message = $"{GetInnerException(exceptionContext.Exception)}";
                    var _exception = new Exception(_message, exceptionContext.Exception);

                    // não precisamos expor o nosso erro interno aos usuários, por isso vamos substituir a mensagem e indicar apenas o id do erro para possível rastreio
                    if (_isProduction)
                        _response.Message = $"Um erro foi encontrado! Entre em contato com o suporte e informe o Id [{_guid}] para tentarmos solucionar o problema.";
                    else //se estamos aqui, então estamos em debug
                        _response.Message = $"Um erro foi encontrado!\r\n{_message}";

                    _response.HttpStatusCode = HttpStatusCode.InternalServerError;
                    _response.StackTrace = exceptionContext.Exception.StackTrace;
                    _response.Id = _guid;

                    _isProduction = false; //--> já que foi escrito acima, não precisamos fazer novamente. mesmo que seja debug
                    break;
            }

            exceptionContext.Result = new ObjectResult(_response)
            {
                StatusCode = (int)_response.HttpStatusCode,
                DeclaredType = typeof(ErrorResponse)
            };
        }

        private string GetInnerException(Exception exception)
        {
            if (exception == null) return "";

            var _msg = exception.Message + "\r\n" + GetInnerException(exception.InnerException);

            return _msg;
        }
    }
}
