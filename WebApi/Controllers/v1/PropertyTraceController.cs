using Application.Features.PropertyTraces.Commands.Create;
using Application.Features.PropertyTraces.Commands.Delete;
using Application.Features.PropertyTraces.Commands.Update;
using Application.Features.PropertyTraces.Queries.GetAllCached;
using Application.Features.PropertyTraces.Queries.GetById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    
    public class PropertyTraceController : BaseApiController<PropertyTraceController>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var propertyTraces = await _mediator.Send(new GetAllPropertyTraceCacheQuery());
            return Ok(propertyTraces);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var propertyTrace = await _mediator.Send(new GetPropertyTraceByIdQuery() { Id = Id });
            return Ok(propertyTrace);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreatePropertyTraceCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdatePropertyTraceCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            return Ok(await _mediator.Send(command));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeletePropertyTraceCommand() { Id = id }));
        }
    }
}
