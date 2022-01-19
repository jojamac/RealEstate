using Application.DTOs.PropertyImage;
using Application.Features.PropertyImages.Commands.Create;
using Application.Features.PropertyImages.Commands.Delete;
using Application.Features.PropertyImages.Commands.Update;
using Application.Features.PropertyImages.Queries.GetAllCached;
using Application.Features.PropertyImages.Queries.GetById;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers.v1
{
    public class PropertyImageController : BaseApiController<PropertyImageController>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var propertyImage = await _mediator.Send(new GetAllPropertyImageCacheQuery());
            return Ok(propertyImage);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdProperty(int id)
        {
            var propertyImage = await _mediator.Send(new GetPropertyImageByIdPropertyQuery() { Id = id });
            return Ok(propertyImage);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyImageDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] PropertyImageDTO propertyImageDTO)
        {
            byte[] imageData = null;
            if (propertyImageDTO.Image != null)
            {
                using (var binaryReader = new BinaryReader(propertyImageDTO.Image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)propertyImageDTO.Image.Length);
                }
            }

            return Ok(await _mediator.Send(new CreatePropertyImageCommand()
            {
                Enabled = propertyImageDTO.Enabled,
                File =  imageData,
                IdProperty =  propertyImageDTO.IdProperty

            }));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdatePropertyImageCommand command)
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
            return Ok(await _mediator.Send(new DeletePropertyImageCommand() { Id = id }));
        }
    }
}
