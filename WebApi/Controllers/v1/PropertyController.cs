using Application.Features.Properties.Commands.Delete;
using Application.Features.Properties.Queries.GetAllPaged;
using Application.Features.Properties.Queries.GetById;
using Application.Features.Propertys.Commands.Create;
using Application.Features.Propertys.Commands.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    public class PropertyController : BaseApiController<PropertyController>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
        {
            var properties = await _mediator.Send(new GetAllPropertiesQuery(pageNumber, pageSize));
            return Ok(properties);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var properties = await _mediator.Send(new GetPropertyByIdQuery());
            return Ok(properties);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreatePropertyCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdatePropertyCommand command)
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
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeletePropertyCommand { Id = id }));
        }
    }
}
