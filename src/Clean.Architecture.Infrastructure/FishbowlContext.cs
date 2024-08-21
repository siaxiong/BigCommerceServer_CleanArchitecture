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
    var content = new StringContent($"{{\"appName\":\"{FB_APPNAME}\",\"appId\":{FB_APPID},\"username\":\"{FB_USERNAME}\",\"password\":\"{FB_PASSWORD}\"}}", Encoding.UTF8, "application/json");

    /*
    var content = new StringContent($"{{\"appName\":\"{_configuration["env:FB_APPNAME"]}\",\"appId\":{_configuration["env:FB_APPID"]},\"username\":\"{_configuration["env:FB_USERNAME"]}\",\"password\":\"{_configuration["FB_PASSWORD"]}\"}}", Encoding.UTF8, "application/json");
    */
    Console.WriteLine(content);
    var resp = await _httpClient.PostAsync(_httpClient.BaseAddress + "/login", content);
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
