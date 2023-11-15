using EcommerceModel;
using System.ComponentModel.DataAnnotations;

namespace BlazorEcommerce_Client.Shared.ViewModels
{
    public class DetailsViewModel
    {
        public DetailsViewModel()
        {
            ProductPrice = new();
            Count = 1;
        }

        [Range(1, int.MaxValue,ErrorMessage ="Please enter a value greater than 0")]
        public int Count { get; set; }
        [Required]
        public int SelectedProductPriceID { get; set; }
        public ProductPriceDTO ProductPrice { get; set; }
    }

}
