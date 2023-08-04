using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangasAPI.DTOs
{
    public class MangaCategoriaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é requerido")]
        [MinLength(3)]
        [MaxLength(100)]
        [DisplayName("Título")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória")]
        [MinLength(5)]
        [MaxLength(200)]
        [DisplayName("Descrição")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "O autor é requerido")]
        [MinLength(3)]
        [MaxLength(200)]
        public string Autor { get; set; } = string.Empty;

        [Required(ErrorMessage = "A editora é requerida")]
        [MinLength(3)]
        [MaxLength(100)]
        public string Editora { get; set; } = string.Empty;

        [Required(ErrorMessage = "O número de páginas é requerido")]
        [Range(1, 9999)]
        public int Paginas { get; set; }

        [Display(Name = "Data Publicação")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "A data de publicação é requerida")]
        [DisplayFormat(DataFormatString = "{0: dd/MM/yyyy hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime Publicacao { get; set; }

        [Required(ErrorMessage = "O formato é requerido")]
        [MinLength(3)]
        [MaxLength(100)]
        public string Formato { get; set; } = string.Empty;

        [Required(ErrorMessage = "A cor é requerida")]
        [MinLength(3)]
        [MaxLength(50)]
        public string Cor { get; set; } = string.Empty;

        [Required(ErrorMessage = "A origem é requerida")]
        [MinLength(3)]
        [MaxLength(80)]
        public string Origem { get; set; } = string.Empty;

        [MaxLength(250)]
        public string Imagem { get; set; } = string.Empty;

        [Required(ErrorMessage = "O preço é obrigatório")]
        [Column(TypeName = "decimal(10,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [DataType(DataType.Currency)]
        [DisplayName("Preço")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "O estoque é obrigatório")]
        [Range(1, 999)]
        public int Estoque { get; set; }

        //public Categoria? Categoria { get; set; }
        public string? NomeCategoria { get; set; }
    }
}
