using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaskAPI.Services.Todos;

namespace TaskAPI.Controllers
{
    [Route("api/authors/{authorId}/todos")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoRepository _todoService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public TodosController(ITodoRepository repository, IMapper mapper, IConfiguration config)
        {
            _todoService = repository;
            _mapper = mapper;
            _config = config;
        }

        [HttpPost]
        [Route("/api/pdf/author/{id}")]
        public async Task<IActionResult> GetPdf(int id)
        {
            HttpClient client = new HttpClient();

            var todo = _todoService.AllTodos(id);

            if (todo is null)
            {
                return NotFound();
            }

            string json = JsonConvert.SerializeObject(todo);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_config["FUNCTION_APP_URL"]}/api/PdfCreator/?id={id}", content);
            return Ok(response.Content.ReadAsStream());
        }
    }
}