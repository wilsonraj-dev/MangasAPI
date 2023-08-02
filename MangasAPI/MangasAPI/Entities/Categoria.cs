using MangasAPI.Validation;

namespace MangasAPI.Entities
{
    public sealed class Categoria : Entity
    {
        public string Nome { get; private set; } = string.Empty;

        public Categoria(string nome) => ValidateDomain(nome);

        public Categoria(int id, string nome)
        {
            DomainExceptionValidation.When(id < 0, "Id inválido.");
            Id = id;

            ValidateDomain(nome);
        }

        private void ValidateDomain(string nome)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "O nome é obrigatório.");
            Nome = nome;
        }

        public IEnumerable<Manga> Mangas { get; set; }
    }
}
