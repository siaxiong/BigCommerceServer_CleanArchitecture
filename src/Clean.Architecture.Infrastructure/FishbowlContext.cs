using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Clean.Architecture.Infrastructure.HttpObjectMapping;
using Microsoft.Extensions.Configuration;

namespace Clean.Architecture.Infrastructure;

public record FB_Credit_Struct
{
  public string? name;
  public double? remainingCredits;
  public int? id;
};

public record FB_Credit_Resp
{
  public List<FB_Credit_Struct>? credits;
}

public class FishbowlContext
{
  private readonly HttpClient _httpClient;
  private readonly IConfiguration _configuration;
  private string FB_APPNAME;
  private int FB_APPID;
  private string FB_USERNAME;
  private string FB_PASSWORD;

  public FishbowlContext(HttpClient httpClient, IConfiguration config)
  {
    this._httpClient = httpClient;
    this._configuration = config;
    this.FB_APPNAME = this._configuration["env:FB_APPNAME"]!;
    this.FB_APPID = int.Parse(this._configuration["env:FB_APPID"]!);
    this.FB_USERNAME = this._configuration["env:FB_USERNAME"]!;
    this.FB_PASSWORD = this._configuration["env:FB_PASSWORD"]!;
    this._httpClient.BaseAddress = new Uri(config["env:FB_ENDPOINT"]!);
    this._httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
  }

  public async Task CreateLogin()
  {
    var content =
      new StringContent(
        $"{{\"appName\":\"{FB_APPNAME}\",\"appId\":{FB_APPID},\"username\":\"{FB_USERNAME}\",\"password\":\"{FB_PASSWORD}\"}}",
        Encoding.UTF8, "application/json");

    var resp = await _httpClient.PostAsync(_httpClient.BaseAddress + "/login", content);
    var respAsString = await resp.Content.ReadAsStringAsync();
    Http_FB_Login? data = JsonSerializer.Deserialize<Http_FB_Login>(respAsString,
      new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    if (data != null)
      _httpClient.DefaultRequestHeaders.Authorization =
        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", data.token);
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

  public async Task<double> GetCustomerCredit(string customerName)
  {
    await CreateLogin();
    var httpContent =
      new StringContent(
        $"select c.id, c.name, SUM(s.totalPrice) as \"remainingCredits\" from customer c left join so s on c.id = s.customerId where c.name = \"{customerName}\" and (s.statusId=60 or s.statusId=20);",
        Encoding.UTF8, "application/sql"
      );
    var request1 = new HttpRequestMessage(HttpMethod.Get, _httpClient.BaseAddress + "/data-query");
    request1.Headers.Add("Accept", "application/sql");
    request1.Content = httpContent;
    var request = await _httpClient.SendAsync(request1);

    var jssonResp1 = JsonNode.Parse(await request.Content.ReadAsStringAsync());
    Console.WriteLine(jssonResp1?.ToString());
    Console.WriteLine((jssonResp1?[0]?["remainingCredits"]?.ToJsonString()));

    if (jssonResp1?[0]?["remainingCredits"] != null)
      return Convert.ToDouble(jssonResp1?[0]?["remainingCredits"]?.ToJsonString());
    else
      throw new Exception("Bad api request or no fishbowl credits");
  }

}
