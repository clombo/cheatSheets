using CQRSExample.API.Models;
using MediatR;

namespace CQRSExample.API.Queries.Handlers;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product>
{
    // Define a fake data source (in-memory list)
    private readonly List<Product> _products = new List<Product>
    {
        new Product { Id = 1, Name = "Product A", Price = 10.99m },
        new Product { Id = 2, Name = "Product B", Price = 19.99m },
        // Add more fake products as needed
    };
    
    public async Task<Product> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        // Simulate retrieving a product by ID from the fake data source
        // You can take the records from database also.
        
        var product = _products.FirstOrDefault(p => p.Id == request.Id);

        if (product == null)
        {
            // If the product is not found, you can throw an exception or return null
            throw new Exception($"Product with ID {request.Id} not found.");
        }

        return product;
    }
}