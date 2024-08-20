using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Clean.Architecture.Infrastructure;
public class FishbowlHTTPClient
{
  private readonly HttpClient _httpClient;

  public FishbowlHTTPClient(HttpClient httpClient, IConfiguration config)
  {
    this._httpClient = httpClient;
    this._httpClient.BaseAddress = new Uri("http://v2064.myfishbowl.com:3765/api/login");
    this._httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
  }

}
