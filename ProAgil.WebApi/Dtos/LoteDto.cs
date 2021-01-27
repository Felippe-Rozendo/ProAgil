using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebApi.Dtos
{
    public class LoteDto
    {        
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O campo deve conter entre 2 e 50 caracteres.")]
        public string Nome { get; set; }
        [Required]
        public decimal Preco { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }

        [Required(ErrorMessage = "Este Campo é obrigatório.")] 
        [Range(2, 120000)]
        public int Quantidade { get; set; }
    }
}