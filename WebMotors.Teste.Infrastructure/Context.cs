using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using WebMotors.Teste.Entities;

namespace WebMotors.Teste.Infrastructure
{
    /// <summary>
    /// Mapeia base de dados conforme enunciado da WebMotors
    /// </summary>
    public class Context : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<Car> Cars { get; set; }

        public Context()
        { }

        public Context(IConfiguration configuration)
        {
            _configuration = configuration;

            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Define o nome da tabela conforme enunciado:
            modelBuilder.Entity<Car>()
            .ToTable("Tb_AnuncioWebmotors");

            //Define os nomes das colunas conforme enunciado:
            modelBuilder.Entity<Car>()
                .Property(c => c.Brand)
                .HasColumnName("Marca");

            modelBuilder.Entity<Car>()
               .Property(c => c.Model)
               .HasColumnName("Modelo");

            modelBuilder.Entity<Car>()
               .Property(c => c.Version)
               .HasColumnName("Versão");

            modelBuilder.Entity<Car>()
               .Property(c => c.Year)
               .HasColumnName("Ano");

            modelBuilder.Entity<Car>()
                .Property(c => c.Mileage)
                .HasColumnName("Quilometragem");

            modelBuilder.Entity<Car>()
                .Property(c => c.Note)
                .HasColumnName("Observacao");

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetSection("ConnectionStrings:WmConnectionString")?.Value;
            optionsBuilder.UseSqlServer(connectionString);

            //Observação: Caso seja necessário apontar diretamente por aqui, descomentar a linha 69 e comentar as linhas 64 e 65.     :)

            //optionsBuilder.UseSqlServer("Data Source=DESKTOP-ALJIDDC\\SQLEXPRESS;Initial Catalog=Teste_Webmotors;Trusted_Connection=True;Integrated Security=True;");

            base.OnConfiguring(optionsBuilder);
        }
    }
}