using MangasAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace MangasAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Manga> Mangas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            builder.Entity<Categoria>().HasKey(x => x.Id);
            builder.Entity<Categoria>().HasMany(x => x.Mangas)
                                       .WithOne(x => x.Categoria)
                                       .HasForeignKey(x => x.CategoriaId);

            builder.Entity<Manga>().HasKey(x => x.Id);
            builder.Entity<Manga>().Property(x => x.Titulo).HasMaxLength(100).IsRequired();
            builder.Entity<Manga>().Property(x => x.Descricao).HasMaxLength(200).IsRequired();
            builder.Entity<Manga>().Property(x => x.Autor).HasMaxLength(100).IsRequired();
            builder.Entity<Manga>().Property(x => x.Editora).HasMaxLength(100).IsRequired();
            builder.Entity<Manga>().Property(x => x.Formato).HasMaxLength(100).IsRequired();
            builder.Entity<Manga>().Property(x => x.Cor).HasMaxLength(50).IsRequired();
            builder.Entity<Manga>().Property(x => x.Origem).HasMaxLength(100).IsRequired();
            builder.Entity<Manga>().Property(x => x.Imagem).HasMaxLength(350).IsRequired();
            builder.Entity<Manga>().Property(x => x.Preco).HasPrecision(10, 2).IsRequired();

            foreach (var relacoes in builder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
            {
                relacoes.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }
        }
    }
}
