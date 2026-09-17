using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TP02.Models
{
    public class Bl
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O número do BL é obrigatório.")]
        [Display(Name = "Número do BL")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "O Consignee é obrigatório.")]
        public string Consignee { get; set; }

        [Required(ErrorMessage = "O Navio é obrigatório.")]
        public string Navio { get; set; }

   
        public ICollection<Container> Containers { get; set; } = new List<Container>();
    }
}
