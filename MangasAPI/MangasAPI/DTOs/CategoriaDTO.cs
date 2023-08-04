using System.ComponentModel.DataAnnotations;

namespace MangasAPI.DTOs
{
    public class CategoriaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MinLength(3)]
        [MaxLength(100)]
        public string Nome { get; set; } = string.Empty;
    }
}
