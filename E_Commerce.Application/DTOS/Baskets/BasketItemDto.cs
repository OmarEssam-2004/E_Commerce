using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Application.DTOS.Baskets
{
    public class BasketItemDto
    {
        [Required(ErrorMessage = "Product Id Is Required")] 
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name Is Required")]
        public string ProductName { get; set; } = default!;

        [Required(ErrorMessage = "Picture Url Is Required")]
        public string PictureUrl { get; set; } = default!;

        [Range(1, double.MaxValue, ErrorMessage = "Price Must Be A Positive Number")]
         public decimal Price { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Quantity Must Be At Least 1")]
        public int Quantity { get; set; }
    }
}