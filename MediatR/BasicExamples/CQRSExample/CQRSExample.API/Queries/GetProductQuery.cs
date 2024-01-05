using CQRSExample.API.Models;
using MediatR;

namespace CQRSExample.API.Queries;

public class GetProductQuery : IRequest<Product>
{
    public int Id { get; set; }
}