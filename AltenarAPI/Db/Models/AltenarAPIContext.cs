using AltenarAPI.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AltenarAPI.Models
{
    // Определение контекста базы данных AltenarAPIContext
    public class AltenarAPIContext : DbContext
    {
        public AltenarAPIContext(DbContextOptions<AltenarAPIContext> options) : base(options) { }

        // Определение сущности таблицы IncomingMessages DbSet для модели IncomingMessage
        public DbSet<IncomingMessage> IncomingMessages { get; set; }

        // Определение сущности таблицы Messages DbSet для модели Parameter
        public DbSet<Parameter> Parameters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка отношения «один ко многим» между моделями таблиц IncomingMessages и Parameters
            modelBuilder.Entity<IncomingMessage>()
            .HasMany(p => p.Parameters)
            .WithOne();

            // Предварительная загрузка данных в БД
            modelBuilder.Seed();
        }
    }
}
