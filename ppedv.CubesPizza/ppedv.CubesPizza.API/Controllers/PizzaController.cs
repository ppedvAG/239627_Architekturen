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

        IUnitOfWork uow;

        public PizzaController(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        // GET: api/<PizzaController>
        [HttpGet]
        public IEnumerable<PizzaDTO> Get()
        {
            return uow.FoodRepository.GetAll().Select(x => PizzaMapper.MapToDTO(x));
        }

        // GET api/<PizzaController>/5
        [HttpGet("{id}")]
        public PizzaDTO Get(int id)
        {
            return PizzaMapper.MapToDTO(uow.FoodRepository.GetById(id));
        }

        // POST api/<PizzaController>
        [HttpPost]
        public void Post([FromBody] PizzaDTO value)
        {
            uow.FoodRepository.Add(PizzaMapper.MapToPizza(value));
            uow.SaveAll();
        }

        // PUT api/<PizzaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] PizzaDTO value)
        {
            uow.FoodRepository.Update(PizzaMapper.MapToPizza(value));
            uow.SaveAll();
        }

        // DELETE api/<PizzaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            uow.FoodRepository.Delete(uow.FoodRepository.GetById(id));
            uow.SaveAll();
        }
    }
}
