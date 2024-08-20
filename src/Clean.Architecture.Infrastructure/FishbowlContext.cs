using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Clean.Architecture.Infrastructure.HttpObjectMapping;
using Microsoft.Extensions.Configuration;

namespace Clean.Architecture.Infrastructure;
public class FishbowlContext
{
  private readonly HttpClient _httpClient;
  public FishbowlContext(HttpClient httpClient, IConfiguration config)
  {
    this._httpClient = httpClient;
    this._httpClient.BaseAddress = new Uri("na");
    this._httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
  }
  public async Task CreateLogin()
  {
    var content = new StringContent("na");
    var resp = await _httpClient.PostAsync(_httpClient.BaseAddress + "na", content);
    var respAsString = await resp.Content.ReadAsStringAsync();
    Console.WriteLine(respAsString);
    Http_FB_Login? data = JsonSerializer.Deserialize<Http_FB_Login>(respAsString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    if (data != null) _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", data.token);
    else throw new Exception("Bad api request for fishbowl login");
  }
  public async Task GetUsers()
  {
    var resp = await _httpClient.GetAsync(_httpClient.BaseAddress + "/users");
    var respAsString = await resp.Content.ReadAsStringAsync();
    Console.WriteLine(respAsString);
  }
  public async Task CreateFBSO(string orderString)
  {
    await CreateLogin();
    var content = new StringContent(orderString, Encoding.UTF8, "application/json");
    Console.WriteLine(content);
    var resp = await _httpClient.PostAsync(_httpClient.BaseAddress + "/import/Sales-Order", content);
    var respString = await resp.Content.ReadAsStringAsync();
    Console.WriteLine(resp.StatusCode + " " + resp.ReasonPhrase);
    Console.WriteLine(respString);
  }
}
