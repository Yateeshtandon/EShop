using EShop.Data;

namespace EShop.Models
{
	public class ProductModel
	{
		public int ProductId { get; set; }
		public int CustomerId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string Image { get; set; }
		public string Quantity { get; set; }
		public string Category { get; set; }

		public static ProductModel Map(Product product, Customer customer)
		{
			ProductModel model = new ProductModel()
			{
				Name = product.Name,
				Category = product.Category,
				Description = product.Description,
				Image = product.Image,
				Quantity = product.Quantity,
				ProductId = product.ProductId,
				CustomerId = customer.CustomerId
			};

			return model;
		}
	}
}