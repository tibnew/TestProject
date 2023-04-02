using Microsoft.AspNetCore.Mvc;
using TestProject.Contract;
using TestProject.Repositories;

namespace TestProject.Controllers
{
    [ApiController]
    [Route("data")]
    public class DataController : ControllerBase
    {
        private readonly IDataRepository dataRepository;

        public DataController(IDataRepository dataRepository) { this.dataRepository = dataRepository; } 

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateDataRequest request)
        {
            await dataRepository.CreateAsync(request.Item1, request.Item2);
            return new CreatedResult("", null);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int start, int end)
        {
            var result = await dataRepository.GetAsync(start, end);
            return new OkObjectResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await dataRepository.DeleteAsync(id);
            return new OkResult();
        }
    }

}
