using API.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Database
{
    public class ConexaoDB : DbContext
    {
        public ConexaoDB(DbContextOptions<ConexaoDB> options) : base(options){}
        public DbSet<ModeloCliente> Clientes {get;set;}
    }
}