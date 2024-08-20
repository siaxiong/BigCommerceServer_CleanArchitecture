using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Clean.Architecture.Infrastructure.HttpObjectMapping;
using Microsoft.Extensions.Configuration;

namespace Clean.Architecture.Infrastructure.BigCommerce;
public class B2B_Context
{
  private readonly HttpClient _httpClient;

  public B2B_Context(HttpClient httpClient, IConfiguration config)
  {
    _httpClient = httpClient;
    _httpClient.BaseAddress = new Uri("na");
    _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    _httpClient.DefaultRequestHeaders.Add("authToken", "na");
  }

  public async Task<HttpResponseMessage> GetAllQuotes()
  {
    return await _httpClient.GetAsync(_httpClient.BaseAddress + "/rfq");
  }
  public async Task<HttpResponseMessage> GetQuote(int quoteId)
  {
    return await _httpClient.GetAsync(_httpClient.BaseAddress + "/rfq/" + quoteId);
  }
  public async Task<Http_B2B_CompanyUser> GetB2BCompanyUserUsingEmail(string email)
  {
    var resp = await _httpClient.GetAsync(_httpClient.BaseAddress + "/users?email=" + email);
    var respString = await resp.Content.ReadAsStringAsync();
    Http_B2B_CompanyUser_Payload? data = JsonSerializer.Deserialize<Http_B2B_CompanyUser_Payload>(respString,new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    if (data != null) return data.data[0];
    else throw new Exception("null resp, GetB2BCompanyUserUsingEmail()");
  }
/*  public async Task<Http_B2B_CompanyUser> GetB2BCompanyUserUsingCustomerId(int customerId)
  {
    var resp =  await _httpClient.GetAsync(_httpClient.BaseAddress + "/users/" + customerId);
    var respString = await resp.Content.ReadAsStringAsync();
    Http_B2B_CompanyUser? data = JsonSerializer.Deserialize<Http_B2B_CompanyUser>(respString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    if (data != null) return data;
    else throw new Exception("null resp,GetB2BCompanyUserUsingCustomerId() ");
  }
*/  
  public async Task<Http_B2B_Company> GetCompany(int companyId)
  {
    var resp =  await _httpClient.GetAsync(_httpClient.BaseAddress + "/companies/" + companyId);
    var respString = await resp.Content.ReadAsStringAsync();
    Console.WriteLine(respString);
    Http_B2B_Company_Payload? data = JsonSerializer.Deserialize<Http_B2B_Company_Payload>(respString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    Console.WriteLine(respString);
    if (data != null) return data.data;
    else throw new Exception("null resp, GetCompanies()");
  }
/*  public async Task<Http_B2B_CompanyUser> GetCompanyUserMembers(int companyId)
  {
    var resp =  await _httpClient.GetAsync(_httpClient.BaseAddress + "/users?companyId=" + companyId);
    var respString = await resp.Content.ReadAsStringAsync();
    Console.WriteLine(respString);
    Http_B2B_CompanyUser_Payload? data = JsonSerializer.Deserialize<Http_B2B_CompanyUser_Payload>(respString, new JsonSerializerOptions { PropertyNameCaseInsensitive=true});
    if (data != null) return data.data[0];
    else throw new Exception("null resp,GetCompanyUserMembers() ");
  }
*//*  public async Task<HttpResponseMessage> GetSalesStaffs()
  {
    return await _httpClient.GetAsync(_httpClient.BaseAddress + "/sales-staffs");
  }*/
}
