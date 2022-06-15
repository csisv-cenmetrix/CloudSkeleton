using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskAPI.DataAccess;
using TaskAPI.Models;
using TaskAPI.Services.Todos;
using Xunit;


namespace TaskAPI.UnitTests
{
    public class TodoRepositoryTests : IDisposable
    {
        protected readonly TodoDbContext _context;
        protected readonly TodoRepository _repository;
        protected List<Todo> todos;

        public TodoRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<TodoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new TodoDbContext(options, new ConfigurationBuilder().Build());

            todos = new List<Todo>
            {
                new Todo { AuthorId = 3, Description = "Todo 01" },
                new Todo { AuthorId = 1, Description = "Todo 02" },
                new Todo { AuthorId = 1, Description = "Todo 03" },
                new Todo { AuthorId = 2, Description = "Todo 04" },
                new Todo { AuthorId = 1, Description = "Todo 05" },
            };

            _context.Todos.AddRange(todos);
            _context.SaveChanges();

            _repository = new TodoRepository(_context);

        }

        [Fact]
        public void AllTodos_AuthorId_ReturnsAllAuthorTodos()
        {
            int authorId = 1;
            var result = _repository.AllTodos(authorId);

            Assert.NotNull(result);
            Assert.Equal(todos.Where(todo => todo.AuthorId == authorId).Count(), result.Count());
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}