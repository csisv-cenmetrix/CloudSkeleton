using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAPI.Models;
using TaskAPI.DataAccess;
using Microsoft.Extensions.Configuration;

namespace TaskAPI.Services.Todos
{
    public class TodoRepository : ITodoRepository
    {
        public TodoRepository(TodoDbContext context)
        {
            _context = context;
        }

        public List<Todo> AllTodos(int authorId)
        {
            return _context.Todos.Where(t => t.AuthorId == authorId).ToList();
        }

        public Todo GetTodo(int authoId, int id)
        {
            return _context.Todos.FirstOrDefault(t => t.Id == id && t.AuthorId == authoId);
        }

        public Todo AddTodo(int authorId, Todo todo)
        {
            todo.AuthorId = authorId;

            _context.Todos.Add(todo);
            _context.SaveChanges();

            return _context.Todos.Find(todo.Id);
        }

        private TodoDbContext _context;
    }
}
