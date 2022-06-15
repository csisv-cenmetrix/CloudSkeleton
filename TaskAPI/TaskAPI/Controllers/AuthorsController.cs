using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TaskAPI.Services.Authors;
using TaskAPI.Services.Models;

namespace TaskAPI.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorRepository _service;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IConfiguration _config;

        public AuthorsController(IAuthorRepository service, IMapper mapper, ILogger<AuthorsController> logger, IConfiguration config)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
            _config = config;
        }

        [HttpGet("{id}", Name = "GetAuthor")]
        public IActionResult GetAuthor(int id)
        {
            var author = _service.GetAuthor(id);

            if (author is null)
            {
                return NotFound();
            }

            var mappedAuthor = _mapper.Map<AuthorDto>(author);

            return Ok(mappedAuthor);
        }
    }
}
