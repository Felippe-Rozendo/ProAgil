using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebApi.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O campo deve conter entre 2 e 50 caracteres.")]
        public string Local { get; set; }
        public string DataEvento { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O campo deve conter entre 2 e 50 caracteres.")]
        public string Tema { get; set; }
        [Required]
        [Range(2, 120000)]
        public int QtdPessoas { get; set; }
        public string ImageURL { get; set; }
        [Phone]
        public string Telefone { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        [StringLength(50, MinimumLength = 2)]
        public string Email { get; set; }
        public List<LoteDto> Lotes { get; set; }
        public List<RedeSocialDto> RedesSociais { get; set; }
        public List<PalestranteDto> Palestrante { get; set; }
    }
}