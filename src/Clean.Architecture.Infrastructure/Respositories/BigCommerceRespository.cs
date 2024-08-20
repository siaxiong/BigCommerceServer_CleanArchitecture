using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.BC;
using Clean.Architecture.Core.Interfaces;
using Clean.Architecture.UseCases.Abstractions.Respository;
using Clean.Architecture.Infrastructure.HttpObjectMapping;
using Microsoft.Extensions.Configuration;
using MimeKit.Tnef;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Clean.Architecture.Infrastructure.BigCommerce;
using Clean.Architecture.Core.Interfaces.Command;

namespace Clean.Architecture.Infrastructure.Respositories;
public class BigCommerceRepository : IBigCommerceRepository
{
  private readonly B2C_V2_Context _b2c_v2_context;
  private readonly B2C_V3_Context _b2c_v3_context;
  private readonly B2B_Context _b2b_context;

  public BigCommerceRepository (B2C_V2_Context b2c_v2_context, B2C_V3_Context b2c_v3_context, B2B_Context b2b_context)
  {
    _b2c_v2_context = b2c_v2_context;
    _b2c_v3_context = b2c_v3_context;
    _b2b_context = b2b_context;
  }
  public async Task<BC_Order> GetBCOrder(int orderId)
  {
    var order = await _b2c_v2_context.GetOrder(orderId);

    BC_BillingAddress billingAddress = await GetBCOrderBillingAddress(orderId);
    BC_ShippingAddress shippingAddress = await GetBCOrderShippingAddress(orderId);
    List<BC_OrderProduct> productList = await GetBCOrderProductList(orderId);

    return new BC_Order(
      order.id,
      order.status_id,
      order.customer_id,
      order.subtotal_inc_tax,
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
  public async Task<BC_Customer> GetBCCustomer(int customerId)
  {
    Http_B2C_V3_Customer customer = await _b2c_v3_context.GetCustomer(customerId);
    Http_B2B_CompanyUser b2b_User = await _b2b_context.GetB2BCompanyUserUsingEmail(customer.email);
    Http_B2B_Company b2b_Company = await _b2b_context.GetCompany(b2b_User.companyId);
    Console.WriteLine($"b2b_Company: {b2b_Company}");

    if (customer != null)
      return new BC_Customer(customer.id, b2b_Company.companyName, customer.first_name,
        customer.last_name, customer.email, customer.phone, customer.notes);
    else
      throw new Exception("null resp, BC Repo GetCustomer()");
  }

}
