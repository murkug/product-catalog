using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalogBackend.Data;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

var AllowSpecificOrigins = "_allowSpecificOrigins";

builder.Services.AddCors(options =>
{
	options.AddPolicy(name: AllowSpecificOrigins,
					  policy =>
					  {
						  policy.WithOrigins("http://localhost:5173")
								.AllowAnyHeader()
								.AllowAnyMethod();
					  });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ProductDbContext>(options =>
	options.UseSqlite(connectionString));

builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
{
	options.InvalidModelStateResponseFactory = context =>
	{
		// Extract the validation errors from the ModelState.
		var errors = context.ModelState
			.Where(e => e.Value.Errors.Count > 0)
			.ToDictionary(
				kvp => kvp.Key,
				kvp => kvp.Value.Errors.Select(e =>
				{
					if (e.ErrorMessage.Contains("could not be converted"))
					{
						return "A value in the 'colours' array is not a valid integer.";
					}
					
					return e.ErrorMessage;
				}).ToArray()
			);

		var errorResponse = new
		{
			Title = "One or more validation errors occurred.",
			Status = 400,
			Errors = errors
		};

		return new BadRequestObjectResult(errorResponse);
	};
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(AllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

// Apply database migrations on startup
using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var context = services.GetRequiredService<ProductDbContext>();
	context.Database.Migrate();
}

app.Run();
