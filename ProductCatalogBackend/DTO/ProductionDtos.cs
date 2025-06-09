using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductCatalogBackend.DTO
{
	public class CreateProductRequest
	{
		[Required]
		[JsonPropertyName("name")]
		public string Name { get; set; } = string.Empty;

		[Required]
		[JsonPropertyName("productType")]
		public int ProductTypeId { get; set; }

		[Required]
		[MinLength(1)]
		[JsonPropertyName("colors")]
		public List<int> ColorIds { get; set; } = [];
	}

	public class ProductListItemResponse
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string ProductType { get; set; } = string.Empty;
		public List<string> Colours { get; set; } = new();
	}

	public class ProductTypeDetailDto
	{
		public string Id { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
	}

	public class ColorDetailDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
	}

	public class ProductDetailResponse
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public ProductTypeDetailDto ProductType { get; set; } = null!;
		public List<ColorDetailDto> Colors { get; set; } = new();
	}
}
