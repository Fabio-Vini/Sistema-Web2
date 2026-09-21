using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP02.Models
{
    public class Container
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O número do container é obrigatório.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "O número deve ter exatamente 11 caracteres.")]
        [Display(Name = "Número")]
        public string Numero { get; set; } = string.Empty;

        [Required(ErrorMessage = "O tipo é obrigatório.")]
        [RegularExpression("^(Dry|Reefer)$", ErrorMessage = "Selecione Dry ou Reefer.")]
        public string Tipo { get; set; } = string.Empty;

        [Required(ErrorMessage = "O tamanho é obrigatório.")]
        public int Tamanho { get; set; } // 20 ou 40

        // Chave Estrangeira Obrigatória para o BL
        [Required(ErrorMessage = "A associação com um BL é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um BL válido.")]
        [Display(Name = "BL Associado")]
        public int BlId { get; set; }

        [ForeignKey(nameof(BlId))]
        public Bl? Bl { get; set; }
    }
}
