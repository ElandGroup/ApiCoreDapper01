using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HelloApiWithCoreDapper.Models;
using HelloApiWithCoreDapper.Service;
using Microsoft.AspNetCore.Mvc;
using HelloApiWithCoreDapper.Common.HttpPack;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HelloApiWithCoreDapper.Controllers
{
    [Route("api/v1/[controller]")]
    public class NewFruitController : Controller
    {
        IFruitService _fruitService;
        public NewFruitController(IFruitService fruitService)
        {
            _fruitService = fruitService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var fruitDtoList = await _fruitService.FruitQuery();
            if (fruitDtoList == null)
                return this.NotFoundEx();
            return this.OkEx(fruitDtoList);
        }

        // GET api/values/5
        [HttpGet("{name}",Name ="GetFruit2")]
        public async Task<IActionResult> Get(string name)
        {
            var fruitDtoList = await _fruitService.FruitQuery(name);
            if (fruitDtoList == null)
                return this.NoContentEx();
            return this.OkEx(fruitDtoList);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]List<FruitDto> fruitDtoList)
        {
            try
            {
                if (fruitDtoList == null)
                {
                    return this.BadRequestEx("fruitDtoList");
                }
                _fruitService.FruitAdd(fruitDtoList);
                return this.NoContentEx();
            }
            catch (Exception ex)
            {
                return this.ErrorEx(ex.Message);
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
                    return this.BadRequestEx("fruitDto");
                }

                var fruit = _fruitService.FruitQuery(name);
                if (fruit == null)
                {
                    return this.NotFoundEx();
                }

                _fruitService.FruitUpdate(fruitDto);
                return this.NoContentEx();
            }
            catch (Exception ex)
            {
                return this.ErrorEx(ex.Message);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{name}")]
        public IActionResult Delete(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    return this.BadRequestEx("name");
                }

                _fruitService.FruitDelete(name);

                return this.NoContentEx();
            }
            catch (Exception ex)
            {
                return this.ErrorEx(ex.Message);
            }
        }
    }
}
