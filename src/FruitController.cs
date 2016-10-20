using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HelloApiWithCoreDapper.Models;
using HelloApiWithCoreDapper.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloApiWithCoreDapper.Controllers
{
    [Route("api/v1/[controller]")]
    public class FruitController : Controller
    {
        IFruitService _fruitService;
        public FruitController(IFruitService fruitService)
        {
            _fruitService = fruitService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var fruitDtoList = await _fruitService.FruitQuery();
            if (fruitDtoList == null)
                return NoContent();
            return Ok(fruitDtoList);
        }

        // GET api/values/5
        [HttpGet("{name}",Name ="GetFruit")]
        public async Task<IActionResult> Get(string name)
        {
            var fruitDtoList = await _fruitService.FruitQuery(name);
            if (fruitDtoList == null)
                return NoContent();
            return  Ok(fruitDtoList);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]List<FruitDto> fruitDtoList)
        {
            try
            {
                if (fruitDtoList == null)
                {
                    return BadRequest();
                }

                _fruitService.FruitAdd(fruitDtoList);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError,ex.Message);
            }

        }

        // PUT api/values/5
        [HttpPut("{name}")]
        public IActionResult Put(string name, [FromBody]FruitDto fruitDto)
        {
            try
            {
                if (fruitDto == null)
                {
                    return BadRequest();
                }

                var fruit = _fruitService.FruitQuery(name);
                if (fruit == null)
                {
                    return NoContent();
                }

                _fruitService.FruitUpdate(fruitDto);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{name}")]
        public IActionResult Delete(string name)
        {
            try
            {
                if (name == null)
                {
                    return BadRequest();
                }

                _fruitService.FruitDelete(name);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
