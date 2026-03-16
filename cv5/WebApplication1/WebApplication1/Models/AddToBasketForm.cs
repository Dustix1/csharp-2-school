using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class AddToBasketForm
    {
        [DisplayName("Počet kusů")]
        [Required(ErrorMessage = "Počet kusů vole")]
        [Range(0, 10) /* tu něco ještě vole neví,*/]
        //[EmailAddress]
        public int? Quantity { get; set; }
        public int ProductId { get; set; }
    }
}
