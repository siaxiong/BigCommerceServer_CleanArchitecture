using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.BC;
using Clean.Architecture.Core.Entities.Fishbowl;
using Clean.Architecture.Core.Interfaces;
using Clean.Architecture.UseCases.Abstractions.Respository;
using Microsoft.Extensions.Configuration;
using Clean.Architecture.UseCases.Abstractions;
using Clean.Architecture.Infrastructure.HttpObjectMapping;

namespace Clean.Architecture.Infrastructure.Respositories;

public class FishbowlRespository : IFishbowlRespository
{
  public readonly FishbowlContext _fbContext;
  public FishbowlRespository(FishbowlContext _fbContext)
  {
    this._fbContext = _fbContext;
  }
  public async Task CreateFBSO(string orderString)
  {
    await _fbContext.CreateFBSO(orderString);
  }

  public async Task<double> GetFbCustomerCredit(string customerName)
  {
    var resp = await _fbContext.GetCustomerCredit(customerName);
    return resp;
  }

}
