﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.BC;
using Clean.Architecture.Core.Entities.Fishbowl;
using Clean.Architecture.Core.Interfaces;
using Clean.Architecture.UseCases.Abstractions.Respository;
using Clean.Architecture.Infrastructure.HttpObjectMapping;
using Microsoft.Extensions.Configuration;
using MimeKit.Tnef;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Clean.Architecture.Infrastructure.BigCommerce;
using Clean.Architecture.Core.Interfaces.Command;
using Clean.Architecture.Infrastructure.Data;
using Clean.Architecture.UseCases.Abstractions;

namespace Clean.Architecture.Infrastructure.Respositories;
public class BigCommerceRepository : IBigCommerceRepository
{
  private readonly B2C_V2_Context _b2c_v2_context;
  private readonly B2C_V3_Context _b2c_v3_context;
  private readonly B2B_Context _b2b_context;
  private readonly IFishbowlRespository _fishbowlRespository;

  public BigCommerceRepository (B2C_V2_Context b2c_v2_context, B2C_V3_Context b2c_v3_context, B2B_Context b2b_context, IFishbowlRespository fishbowlRespository)
  {
    _b2c_v2_context = b2c_v2_context;
    _b2c_v3_context = b2c_v3_context;
    _b2b_context = b2b_context;
    _fishbowlRespository = fishbowlRespository;
  }
  public async Task<BC_Order> GetBCOrder(int orderId)
  {

    var shippingAdd = await GetBCOrderShippingAddress(orderId);
    var order = await _b2c_v2_context.GetOrder(orderId);

    BC_BillingAddress billingAddress = await GetBCOrderBillingAddress(orderId);
    BC_ShippingAddress shippingAddress = await GetBCOrderShippingAddress(orderId);
    List<BC_OrderProduct> productList = await GetBCOrderProductList(orderId);
    Console.WriteLine("***SHIPPING COST START***");
    Console.WriteLine(order.shipping_cost_ex_tax);
    Console.WriteLine(shippingAdd.shipping_method);
    Console.WriteLine("***SHIPPING COST END***");


    return new BC_Order(
      order.id,
      order.status_id,
      order.customer_id,
      order.subtotal_inc_tax,
      Convert.ToDouble(order.shipping_cost_ex_tax),
      shippingAdd.shipping_method,
      order.payment_status,
      order.payment_method,
      order.store_credit_amount,
      billingAddress,
      shippingAddress,
      productList
      );
  }
  public async Task<BC_BillingAddress> GetBCOrderBillingAddress(int orderId)
  {
    var order = await _b2c_v2_context.GetOrder(orderId);
    var addr = order.billing_address;

    if(addr.street_2 != null)
      return new BC_BillingAddress(addr.first_name,addr.last_name,addr.company,addr.street_1,
        addr.street_2,addr.city, addr.state, addr.zip,addr.country, addr.phone, addr.email
        );
    else
      return new BC_BillingAddress(addr.first_name, addr.last_name, addr.company, addr.street_1,
      addr.city, addr.state, addr.zip, addr.country, addr.phone, addr.email);
  }
  public async Task<List<BC_OrderProduct>> GetBCOrderProductList(int orderId)
  {
    List<BC_OrderProduct> bC_OrderProductList = new List<BC_OrderProduct>();
    var products = await _b2c_v2_context.GetOrderProducts(orderId);
    /*    foreach (Http_B2C_OrderProduct product in products)
        {
          Console.WriteLine(products.Length);
          Console.WriteLine(product);
          bC_OrderProductList.Add(new BC_OrderProduct(product.id, product.order_id, product.product_id,
            product.sku, product.quantity,Convert.ToDouble( product.base_price), Double.Parse(product.applied_discounts[0].amount), product.applied_discounts[0].id));
        }*/


    for(int i = 0; i < products.Length; i++)
    {
      var product = products[i];
      var item = new BC_OrderProduct(product.id, product.order_id, product.product_id,
      product.sku, product.name, product.quantity, Convert.ToDouble(product.base_price), null, null);
      bC_OrderProductList.Add(item);
    }
    return bC_OrderProductList;
  }
  public async Task<BC_ShippingAddress> GetBCOrderShippingAddress(int orderId)
  {
    var shipAddr = await _b2c_v2_context.GetOrderShippingAddress(orderId);

    if (shipAddr.street_2 != null)
      return new BC_ShippingAddress(
      shipAddr.id,
      shipAddr.order_id,
      shipAddr.first_name,
      shipAddr.last_name,
      shipAddr.company,
      shipAddr.street_1,
      shipAddr.street_2,
      shipAddr.city,
      shipAddr.zip,
      shipAddr.state,
      shipAddr.country,
      shipAddr.email,
      shipAddr.phone,
      shipAddr.items_total,
      shipAddr.shipping_method,
      shipAddr.cost_ex_tax
      );
    else
      return new BC_ShippingAddress(
      shipAddr.id,
      shipAddr.order_id,
      shipAddr.first_name,
      shipAddr.last_name,
      shipAddr.company,
      shipAddr.street_1,
      shipAddr.city,
      shipAddr.zip,
      shipAddr.state,
      shipAddr.country,
      shipAddr.email,
      shipAddr.phone,
      shipAddr.items_total,
      shipAddr.shipping_method,
      shipAddr.cost_ex_tax
      );
  }

  public async Task<int> GetB2BCustomerId(int orderId)
  {
    var b2bCustomerId = await _b2b_context.GetB2BCompanyIdUsingB2COrderId(orderId);
    return b2bCustomerId;
  }
  public async Task<BC_Customer> GetBCCustomer(int customerId)
  {
    Http_B2C_V3_Customer customer = await _b2c_v3_context.GetCustomer(customerId);
    Http_B2B_CompanyUser b2b_User = await _b2b_context.GetB2BCompanyUserUsingEmail(customer.email);
    Http_B2B_Company b2b_Company = await _b2b_context.GetCompany(b2b_User.companyId);

    if (customer != null)
      return new BC_Customer(customer.id, b2b_Company.companyName, customer.first_name,
        customer.last_name, customer.email, customer.phone, customer.notes);
    else
      throw new Exception("null resp, BC Repo GetCustomer()");
  }

  public async Task ArchiveOrder(int orderId)
  { 
    await _b2c_v2_context.ArchiveBadOrder(orderId);
  }

  public async Task ChangeOrderStatus(int orderId)
  {
    await _b2b_context.ChangeB2BOrdStatus(orderId);
  }

  public async Task UpdateCompanyCredits(int companyId, double credits)
  {
    await _b2b_context.UpdateB2BCompanyCredits(companyId, credits);
  }

  public bool CheckExistence(string b2bCompanyName, List<FB_Credit> fbCredit)
  {
    return fbCredit.Exists(x => x.CustomerName == b2bCompanyName);
  }
  public async Task UpdateAllCustomerCredits()
  {
    List<FB_Credit> fbCredits = await _fishbowlRespository.GetAllFBCustomerCredits();
    List<Http_B2B_Company> allB2BCompanies = await _b2b_context.GetAllCompanies();
    List<B2B_Company_Credit> b2BCompanyCredits = new List<B2B_Company_Credit>();
    var now = DateTime.Now;
    TimeZoneInfo pacificTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Pacific Standard Time");
    DateTime utcNow = TimeZoneInfo.ConvertTime(now, pacificTimeZone);

    foreach (FB_Credit fbCredit in fbCredits)
    {
      foreach (Http_B2B_Company b2bBCompany in allB2BCompanies)
      {
        if ((fbCredit.CustomerName)?.Trim() == (b2bBCompany.companyName)?.Trim())
        {
          List<B2B_Company_Extra_Field> extraFields = new List<B2B_Company_Extra_Field>();
           extraFields.Add(new B2B_Company_Extra_Field{fieldName = "Remaining_Credits (Negative # means have money.)", fieldValue=$"${fbCredit.CreditAmount.ToString()}   {utcNow.ToString() + " - Pacific Time" }"});
          
           b2BCompanyCredits.Add(new B2B_Company_Credit(b2bBCompany.companyId.ToString(), extraFields));
        }
      }
    }

    for (int i = 0; i < (b2BCompanyCredits.Count) / 10; i++)
    {
      await _b2b_context.UpdateBulkB2BCompanyCredits(b2BCompanyCredits.GetRange(i * 10 , 10));
    }

  }
}
