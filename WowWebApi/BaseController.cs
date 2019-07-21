using Application;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Repository;
using System.Net;

namespace WowWebApi
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TApp"></typeparam>
    /// <typeparam name="TRep"></typeparam>
    public class BaseController<TApp, TRep> : Controller, IBaseController
        where TApp : ApplicationBase<TRep>
        where TRep : RepositoryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public Controller GetViewData()
        {
            return this;
        }
        /// <summary>
        /// Retorna a Aplicação definida para a controler
        /// </summary>
        public TApp Application { get; }
        /// <summary>
        /// Retorna o objecto com as configurações da Web API
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="application"></param>
        protected BaseController(IConfiguration configuration, TApp application)
        {
            this.Configuration = configuration;
            this.Application = application;
        }

        /// <summary>
        /// Valida a model
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var _controller = (context.Controller as IBaseController);
            var _viewData = _controller.GetViewData();

            if (!_viewData.ModelState.IsValid)
            {
                var _msg = "";
                foreach (var _field in context.ModelState)
                {
                    _msg += (_msg == "" ? "" : "\r\n ") + $"Campo: {_field.Key} -> ";
                    foreach (var _error in _field.Value.Errors)
                    {
                        _msg += string.IsNullOrEmpty(_error.ErrorMessage) ? _error.Exception.Message : _error.ErrorMessage;
                    }
                }

                var response = new ErrorResponse() { Message = _msg };

                context.Result = new ObjectResult(response)
                {
                    StatusCode = (int)HttpStatusCode.BadRequest, // (int)status,
                    DeclaredType = typeof(ErrorResponse)
                };
            }
        }

    }
}
