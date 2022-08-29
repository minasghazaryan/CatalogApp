using CatalogApp.Data.Models;
using CatalogApp.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatalogApp.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {

        private readonly ICatalogService _catalogService;
        public CatalogController(ICatalogService catalogService)
        {
            _catalogService=catalogService;
        }
        // GET: api/<CatalogController>
        [HttpGet("test")]
        public async Task Get()
        
        {
            var mmodel = new CatalogModel { Coast = 15, Name = "Test", Price = 120 };
           await _catalogService.CreateAsync(mmodel);
        }

        
        // POST api/<CatalogController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CatalogController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CatalogController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
