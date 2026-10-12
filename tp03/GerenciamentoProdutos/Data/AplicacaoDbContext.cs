
using GerenciamentoProdutos.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoProdutos.Data
{
    public class AplicacaoDbContext : DbContext
    {
        public AplicacaoDbContext(
            DbContextOptions<AplicacaoDbContext> options)
            : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
    }
}
