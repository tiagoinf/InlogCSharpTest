using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using InlogCSharpTest.Dominio.Entidades;

namespace InlogCSharpTest.Infra.Data
{
    public class InlogCSharpTestContext : DbContext
    {
        public InlogCSharpTestContext()
            : base("name=InlogCSharpTestContext")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Properties<string>().Configure(c => c.IsUnicode(false));
            modelBuilder.Properties<string>().Configure(c => c.HasColumnType("varchar"));
            modelBuilder.Properties<string>().Configure(c => c.HasMaxLength(100));


            //Config Map de Veículo
            modelBuilder.Entity<Veiculo>().HasKey(u => u.Id);
            modelBuilder.Entity<Veiculo>().Property(p => p.Chassi)
                .HasMaxLength(17)
                .IsRequired();
            modelBuilder.Entity<Veiculo>().Property(p => p.Cor)
               .HasMaxLength(50)
               .IsRequired();
            modelBuilder.Entity<Veiculo>().Ignore(i => i.NumeroPassageiros);
        }

        public DbSet<Veiculo> Veiculos { get; set; }
    }
}
