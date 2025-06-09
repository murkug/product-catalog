namespace ProductCatalogBackend.Models
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;

		public int ProductTypeId { get; set; }
		public ProductType ProductType { get; set; } = null!;

		public ICollection<Color> Colours { get; set; } = new List<Color>();

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}
