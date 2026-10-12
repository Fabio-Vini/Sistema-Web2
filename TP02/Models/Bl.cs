using System.ComponentModel.DataAnnotations;

namespace TP02.Models
{
    public class Bl
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O número do BL é obrigatório.")]
        [StringLength(50, ErrorMessage = "O número do BL deve ter no máximo 50 caracteres.")]
        [Display(Name = "Número do BL")]
        public string Numero { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Consignee é obrigatório.")]
        [StringLength(100, ErrorMessage = "O Consignee deve ter no máximo 100 caracteres.")]
        public string Consignee { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Navio é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do navio deve ter no máximo 100 caracteres.")]
        public string Navio { get; set; } = string.Empty;

        public ICollection<Container> Containers { get; set; } = new List<Container>();
    }
}
