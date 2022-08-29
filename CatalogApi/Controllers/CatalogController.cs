using CatalogApi.Data.Models;
using CatalogApi.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatalogApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogService _catalogService;
        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }
        // GET: api/<CatalogController>


        // GET api/<CatalogController>/5
        [HttpGet("{id}")]
        public async Task<CatalogModel> Get(int id)
        {
            var catalog = await _catalogService.GetById(id);
            if (catalog!=null)
            {
                return catalog;
            }
            return null;
        }

        // POST api/<CatalogController>
        [HttpPut]
        public async Task<ActionResult> Create([FromBody] CatalogModel model)
        {
            try
            {
                await _catalogService.CreateAsync(model);
                return Ok();
            }
            catch (System.Exception)
            {

                return BadRequest();
            }
           
        }

        // PUT api/<CatalogController>/5
        [HttpPost]
        public async Task<ActionResult>  Update(CatalogModel model)
        {
            try
            {
                await _catalogService.UpdateAsync(model);
                return Ok();
            }
            catch (System.Exception)
            {

                return BadRequest();
            }
           

        }

        // DELETE api/<CatalogController>/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            if (id > 0)
            {
                await _catalogService.DeleteAsync(id);
            }
        }
    }
}
