using Microsoft.AspNetCore.Mvc;
using ppedv.CubesPizza.API.Model;
using ppedv.CubesPizza.Model;
using ppedv.CubesPizza.Model.Contracts;


namespace ppedv.CubesPizza.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {

        IRepository repo;

        public PizzaController(IRepository repo)
        {
            this.repo = repo;
        }

        // GET: api/<PizzaController>
        [HttpGet]
        public IEnumerable<PizzaDTO> Get()
        {
            return repo.GetAll<Pizza>().Select(x => PizzaMapper.MapToDTO(x));
        }

        // GET api/<PizzaController>/5
        [HttpGet("{id}")]
        public PizzaDTO Get(int id)
        {
            return PizzaMapper.MapToDTO(repo.GetById<Pizza>(id));
        }

        // POST api/<PizzaController>
        [HttpPost]
        public void Post([FromBody] PizzaDTO value)
        {
            repo.Add<Pizza>(PizzaMapper.MapToPizza(value));
            repo.SaveAll();
        }

        // PUT api/<PizzaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] PizzaDTO value)
        {
            repo.Update<Pizza>(PizzaMapper.MapToPizza(value));
            repo.SaveAll();
        }

        // DELETE api/<PizzaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            repo.Delete<Pizza>(repo.GetById<Pizza>(id));
            repo.SaveAll();
        }
    }
}
