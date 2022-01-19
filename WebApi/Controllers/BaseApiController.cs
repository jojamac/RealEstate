using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseApiController<T> : ControllerBase
    {
        private IMediator _mediatorInstance;
        private ILogger<T> _loggerInstance;

        protected IMediator _mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();
        protected ILogger<T> _logger => _loggerInstance ??= HttpContext.RequestServices.GetService<ILogger<T>>();
    }
}
