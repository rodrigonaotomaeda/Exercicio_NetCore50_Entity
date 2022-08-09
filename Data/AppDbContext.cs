using MeuTeste.Models;
using Microsoft.EntityFrameworkCore;
    //Microsoft.EntityFrameworkCore 5.0.17 o 6.0.0 ou > não funciona
namespace MeuTeste.Data
{
    public class AppDbContext : DbContext
    {
        //Criar o item DbSet(Representação da Tabela)
        public DbSet<Teste> Testes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString:"DataSource=app.db;Cache=Shared");

        }
    }
}
