using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TaskManager.Api.Core.Models;
using TaskManager.Core.Interfaces;

namespace TaskManager.Api.Core.Controllers
{
    public abstract class MainController : ControllerBase
    {
        protected ICollection<string> Erros = new List<string>();
        protected readonly INotifiable _notifiable;

        public MainController(INotifiable notifiable)
        {
            _notifiable = notifiable;
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if (isValid())
            {
                return Ok(new OkModel(result));
            }

            return BadRequest(new BadRequestModel(_notifiable.GetNotifications.ToArray()));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                _notifiable.AddNotification(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected IActionResult InternalServerError(object resultado = null)
        {
            return StatusCode(500, new InternalServerErrorModel(resultado, this.Erros));
        }

        protected bool isValid()
        {
            return !_notifiable.HasNotification;
        }
    }
}
