using TaskAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace TaskAPI.DataAccess
{
    public partial class TodoDbContext : DbContext
    {
        private IConfiguration _config;

        public DbSet<Todo> Todos { get; set; }
        public DbSet<Author> Authors { get; set; }

        public TodoDbContext(DbContextOptions<TodoDbContext> options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config["DbConnString"]);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(new Author[] {
                new Author { Id = 1, FullName = "Trevor Duncan", AddressNo = "45", Street = "Street 1", City = "Belgium", JobRole = "Developer"},
                new Author { Id = 2, FullName = "Carol McLean", AddressNo = "35", Street = "Street 2", City = "Brazil", JobRole = "Systems Engineer"},
                new Author { Id = 3, FullName = "Alexander Pullman", AddressNo = "25", Street = "Street 3", City = "Netherlands", JobRole = "Developer"},
                new Author { Id = 4, FullName = "Adrian Parr", AddressNo = "15", Street = "Street 4", City = "Ukraine", JobRole = "QA"}
            });

            modelBuilder.Entity<Todo>().HasData(new Todo[]
            {
                new Todo
                {
                    Id = 1,
                    Title = "Get books for school - DB",
                    Description = "Get some text books for school",
                    Created = DateTime.Now,
                    Due = DateTime.Now.AddDays(5),
                    Status = TodoStatus.New,
                    AuthorId = 1
                },
                new Todo
                {
                    Id = 2,
                    Title = "Need some grocceries",
                    Description = "Go to supermarket and by some stuff",
                    Created = DateTime.Now,
                    Due = DateTime.Now.AddDays(5),
                    Status = TodoStatus.New,
                    AuthorId = 1
                },
                new Todo
                {
                    Id = 3,
                    Title = "Purchase Camera",
                    Description = "Buy new camera",
                    Created = DateTime.Now,
                    Due = DateTime.Now.AddDays(5),
                    Status = TodoStatus.New,
                    AuthorId = 2
                },
            });
        }
    }
}
