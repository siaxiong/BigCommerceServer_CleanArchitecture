using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using FluentValidation;

namespace Clean.Architecture.Core.Interfaces.Command;
public interface ICommand<out TResponse> : IRequest<TResponse>
{
}

public interface ICommandHandler
  <in TCommand, TResponse>: IRequestHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
{ }

public interface IQuery<out TResponse> : IRequest<TResponse>
{

}

public interface IQueryHandler<in TQuery, TResponse>: IRequestHandler<TQuery,TResponse>
  where TQuery : IQuery<TResponse>
{ 
};

public sealed record UpdateUserCommand(int userId, string firstName, string lastName): ICommand<Unit>;

public sealed class UpdateUserCommandValidator: AbstractValidator<UpdateUserCommand>
{
  public UpdateUserCommandValidator()
  {
    RuleFor(x=>x.userId).NotEmpty();
    RuleFor(x => x.firstName).NotEmpty().MaximumLength(100);
    RuleFor(x=>x.lastName).NotEmpty().MaximumLength(100);
  }
}

public class Product
{
  public int Id { get; set; }
  public string Name { get; set; }

  public Product(int id,string name)
  {
    this.Id = id;
    this.Name = name;
  }

}

/*public class FakeDataStore
{
  private static List<Product>? _products;

  public FakeDataStore()
  {
    _products = new List<Product>
        {
            new Product (  1,  "Test Product 1" ),
            new Product (  2,  "Test Product 2" ),
            new Product (  3,  "Test Product 3" )
        };
  }

  public async Task AddProduct(Product product)
  {
    _products.Add(product);
    await Task.CompletedTask;
  }

  public async Task<IEnumerable<Product>> GetAllProducts() => await Task.FromResult(_products);
}
*/


/*public record GetProductsQuery() : IRequest<IEnumerable<Product>>;

public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
{
  private readonly FakeDataStore _fakeDataStore;
  public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    => await _fakeDataStore.GetAllProducts();
}
*/

