using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebApi.Dtos
{
    public class PalestranteDto
    {
         public int Id { get; set; }
         [Required]
         [StringLength(50, MinimumLength = 4, ErrorMessage = "O campo deve conter entre 4 e 50 caracteres.")]
         public string Nome { get; set; }
         public string MiniCurriculo { get; set; }
         public string ImagemURL { get; set; }
         
         [Phone]
         public string Telefone { get; set; }

         [Required]
         [EmailAddress(ErrorMessage = "Email invalido")]
         public string Email { get; set; }
         public List<RedeSocialDto> RedesSociais { get; set; }
         public List<EventoDto> Eventos { get; set; }
    }
}