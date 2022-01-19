using Application.DTOs.Owner;
using Application.Features.Owners.Commands.Create;
using Application.Features.Owners.Commands.Delete;
using Application.Features.Owners.Commands.Update;
using Application.Features.Owners.Queries.GetAllCached;
using Application.Features.Owners.Queries.GetById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    public class OwnerController : BaseApiController<OwnerController>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var owners = await _mediator.Send(new GetAllOwnerCachedQuery());
            return Ok(owners);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var owner = await _mediator.Send(new GetOwnerByIdQuery() { Id = id });
            return Ok(owner);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ownerDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] OwnerDTO ownerDTO)
        {
            byte[] imageData = null;
            if (ownerDTO.Image != null)
            {
                using (var binaryReader = new BinaryReader(ownerDTO.Image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)ownerDTO.Image.Length);
                }
            }
           
            return Ok(await _mediator.Send(new CreateOwnerCommand()
            {
                Name = ownerDTO.Name,
                Address = ownerDTO.Address,
                BirthDay = ownerDTO.BirthDay,
                Photo = imageData
            }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateOwnerCommand command)
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
            return Ok(await _mediator.Send(new DeleteOwnerCommand() { Id = id }));
        }
    }
}
