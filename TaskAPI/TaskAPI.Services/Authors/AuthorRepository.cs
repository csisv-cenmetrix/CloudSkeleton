using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskAPI.Models;
using TaskAPI.DataAccess;
using Microsoft.Extensions.Configuration;

namespace TaskAPI.Services.Authors
{
    public class AuthorRepository : IAuthorRepository
    {
        public AuthorRepository(TodoDbContext context, IConfiguration config)
        {
            _config = config;
            _context = context;
        }

        public List<Author> GetAllAuthors()
        {
            return _context.Authors.ToList();
        }

        public List<Author> GetAllAuthors(string job)
        {
            if (string.IsNullOrWhiteSpace(job))
            {
                return GetAllAuthors();
            }

            var authorCollection = _context.Authors as IQueryable<Author>;

            if (!string.IsNullOrWhiteSpace(job))
            {
                job = job.Trim();
                authorCollection = _context.Authors.Where(a => a.JobRole == job);
            }

            //if (!string.IsNullOrWhiteSpace(search))
            //{
            //    search = search.Trim();
            //    authorCollection = authorCollection.Where(a => 
            //        a.FullName.Contains(search) || a.City.Contains(search));
            //}

            return authorCollection.ToList();
        }

        public Author GetAuthor(int id)
        {
            return _context.Authors.Find(id);
        }

        public Author AddAuthor(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();

            return _context.Authors.Find(author.Id);
        }

        private TodoDbContext _context;
        private IConfiguration _config;
    }
}
