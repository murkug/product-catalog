using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalogBackend.Data;
using ProductCatalogBackend.DTO;
using ProductCatalogBackend.Models;

namespace ProductCatalogBackend.Controllers
{
	[Route("api/v1/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly ProductDbContext _context;

		public ProductsController(ProductDbContext context)
		{
			_context = context;
		}

		// GET: api/v1/products
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductListItemResponse>>> GetProducts()
		{
			var products = await _context.Products
			.Include(p => p.ProductType)
			.Include(p => p.Colours)
			.OrderByDescending(p => p.CreatedAt)
			.Select(p => new ProductListItemResponse
			{
				Id = p.Id,
				Name = p.Name,
				ProductType = p.ProductType.Name,
				Colours = p.Colours.Select(c => c.Name).ToList()
			})
			.ToListAsync();

			return Ok(products);
		}

		// GET: api/v1/products/5
		[HttpGet("{id}")]
		public async Task<ActionResult<ProductDetailResponse>> GetProduct(int id)
		{
			var product = await _context.Products
				.Include(p => p.ProductType)
				.Include(p => p.Colours)
				.FirstOrDefaultAsync(p => p.Id == id);

			if (product == null)
			{
				return NotFound();
			}

			// Map the entity to the new detailed DTO
			var response = new ProductDetailResponse
			{
				Id = product.Id,
				Name = product.Name,
				ProductType = new ProductTypeDetailDto
				{
					Id = product.ProductType.Id.ToString(), // Convert int to string
					Name = product.ProductType.Name
				},
				Colors = product.Colours.Select(c => new ColorDetailDto
				{
					Id = c.Id,
					Name = c.Name
				}).ToList()
			};

			return Ok(response);
		}

		// POST: api/v1/products
		[HttpPost]
		public async Task<ActionResult<ProductDetailResponse>> AddProduct(CreateProductRequest request)
		{
			var productType = await _context.ProductTypes.FindAsync(request.ProductTypeId);
			if (productType == null)
			{
				return BadRequest("Invalid Product Type ID.");
			}

			var colours = await _context.Colors
				.Where(c => request.ColorIds.Contains(c.Id))
				.ToListAsync();

			if (colours.Count != request.ColorIds.Count)
			{
				return BadRequest("One or more Colour IDs are invalid.");
			}

			var newProduct = new Product
			{
				Name = request.Name,
				ProductTypeId = request.ProductTypeId,
				Colours = colours
			};

			_context.Products.Add(newProduct);
			await _context.SaveChangesAsync();

			var response = new ProductDetailResponse
			{
				Id = newProduct.Id,
				Name = newProduct.Name,
				ProductType = new ProductTypeDetailDto
				{
					Id = productType.Id.ToString(),
					Name = productType.Name
				},
				Colors = newProduct.Colours.Select(c => new ColorDetailDto
				{
					Id = c.Id,
					Name = c.Name
				}).ToList()
			};

			return CreatedAtAction(nameof(GetProduct), new { id = newProduct.Id }, response);
		}

		[HttpGet("types")]
		public async Task<ActionResult<IEnumerable<ProductType>>> GetProductTypes()
		{
			return await _context.ProductTypes.ToListAsync();
		}

		[HttpGet("colors")]
		public async Task<ActionResult<IEnumerable<Color>>> GetColors()
		{
			return await _context.Colors.ToListAsync();
		}
	}
}
