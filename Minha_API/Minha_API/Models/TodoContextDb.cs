using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minha_API.Models
{
    public class TodoContextDb : DbContext
    {
        public TodoContextDb(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TodoItemModel> TodoItemModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItemModel>()
                .Property(p => p.Cadastro)
                .HasDefaultValueSql("GETDATE()");

#if DEBUG
            modelBuilder.Entity<TodoItemModel>()
                .HasData(new TodoItemModel
                {
                    Id = 1,
                    Name = "Robson",
                    Apelido = "Cafe",
                    Ativo = true,
                    Cadastro = DateTime.Now
                },

                new TodoItemModel
                {
                    Id = 2,
                    Name = "Carlo",
                    Ativo = false,
                    Cadastro = DateTime.Now
                });

#endif
            base.OnModelCreating(modelBuilder);


        }
    }
}
