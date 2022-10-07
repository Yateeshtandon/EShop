using EShop.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EShop.Models
{
	public class CustomerModel
	{
		public CustomerModel()
		{
			Products = new List<ProductModel>();
		}

		public int CustomerId { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string Mobile { get; set; }

		[Required]
		public string Gender { get; set; }

		[Required]
		[Display(Name = "Address")]
		public string AddressLine1 { get; set; }

		public List<ProductModel> Products { get; set; }

		public static CustomerModel Map(Customer customer)
		{
			CustomerModel model = new CustomerModel()
			{
				AddressLine1 = customer.AddressLine1,
				Email = customer.Email,
				CustomerId = customer.CustomerId,
				Name = customer.Name,
				Gender = customer.Gender,
				Mobile = customer.Mobile
			};

			foreach (var p in customer.ProductCustomers)
			{
				model.Products.Add(ProductModel.Map(p.Product, customer));
			}

			return model;
		}

		public static Customer Map(CustomerModel model)
		{
			Customer customer = new Customer()
			{
				AddressLine1 = model.AddressLine1,
				Email = model.Email,
				CustomerId = model.CustomerId,
				Name = model.Name,
				Gender = model.Gender,
				Mobile = model.Mobile
			};

			return customer;
		}
	}
}