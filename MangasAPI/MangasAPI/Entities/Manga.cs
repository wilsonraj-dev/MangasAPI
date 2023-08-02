namespace MangasAPI.Entities
{
    public class Manga : Entity
    {
        public string Titulo { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public string Editora { get; set; } = string.Empty;
        public int Paginas { get; set; }
        public DateTime Publicacao { get; set; }
        public string Formato { get; set; } = string.Empty;
        public string Cor { get; set; } = string.Empty;
        public string Origem { get; set; } = string.Empty;
        public string Imagem { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
    }
}
