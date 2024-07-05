using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.Base;
using Clean.Architecture.Core.Entities.BC.B2C;
using Clean.Architecture.Core.Entities.Fishbowl;
using Clean.Architecture.Core.Interfaces.Fishbowl;
using Clean.Architecture.Core.Services;
using Clean.Architecture.Core.Services.Fishbowl;
using FluentAssertions;
using Moq;
using Xunit;


namespace Clean.Architecture.UnitTests.Core.Services.Fishbowl;
public class FishbowlService_Tests
{
  /*  [Fact]
    public void FishbowlService()
    {
      var mock = new Mock<IFishbowlService>();

      var fbBillAddress = new Mock<FBBillingAddress>();
      var fbShippingAddress = new Mock<FBShippingAddress>();
      var soItem = new Mock<List<SOItem>>();
      var fbSO = new Mock<FBSO>();


      var address = new Mock<AddressBase>();
      var B2COrder = new Mock<B2COrder>();
      var B2CBillingAddress = new Mock<B2CBillingAddress>();
      var B2COrderProduct = new Mock<B2COrderProduct>();

      mock.Setup(a => a.CreateFBSO(fbShippingAddress.Object, fbBillAddress.Object, soItem.Object)).Verifiable("Is not called!");

      mock.Object.CreateFBSO(fbShippingAddress.Object, fbBillAddress.Object, soItem.Object).Should().NotBeNull();
    }

  }*/

  //Testing data
  public B2CShippingAddress Get_B2CShippingAddress()
  {
    int id = 100;
    int order_id = 200;
    string first_name = "Tom";
    string last_name = "Brady";
    string company = "Body Armor";
    string email = "tom.brady@ibsimplant.net";
    string phone = "888.888.8888";
    int items_total = 5;
    string shipping_method = "UPS Ground";
    string cost_ex_tax = "na";

    string street = "7830 W Sahara Ave";
    string city = "Las Vegas";
    string state = "NV";
    string zipcode = "89117";
    string country = "USA";

    return new B2CShippingAddress
      (id, order_id, first_name, last_name, company, street, city, zipcode, state,
      country, email, phone, items_total, shipping_method, cost_ex_tax);
  }
  public B2CBillingAddress Get_B2CBillingAddress()
  {
    string first_name = "Tom";
    string last_name = "Ford";
    string company = "Ford Company";
    string street_1 = "3860 El Dorado";
    string street_2 = "STE 501";
    string city = "El Dorado Hills";
    string state = "CA";
    string country = "USA";
    string zipcode = "95762";
    string phone = "999.999.9999";
    string email = "tom.ford@ibsimplant.net";

    return new B2CBillingAddress(first_name, 
      last_name, company, street_1, street_2, city, 
      state, zipcode, country, phone, email);

  }
  public B2COrderProduct Get_B2COrderProduct_1()
  {
    string id = "222";
    string order_id = "bc_333";
    string product_id = "100";
    string sku = "453M4511";
    int quantity = 10;
    double base_price = 420;

    return new B2COrderProduct(id, order_id, product_id, sku, quantity, base_price);
  }
  public B2COrderProduct Get_B2COrderProduct_2()
  {
    string id = "333";
    string order_id = "bc_444";
    string product_id = "200";
    string sku = "454M3511";
    int quantity = 20;
    double base_price = 420;

    return new B2COrderProduct(id, order_id, product_id, sku, quantity, base_price);
  }
  public B2COrderProduct Get_B2COrderProduct_3()
  {
    string id = "444";
    string order_id = "bc_555";
    string product_id = "300";
    string sku = "551M5545";
    int quantity = 40;
    double base_price = 420;

    return new B2COrderProduct(id, order_id, product_id, sku, quantity, base_price);
  }



  [Fact]
  /*
   Check if B2C shipping address is copy correctly
   to FB order import shipping address.
   */
  public void CheckShippingAddress()
  {
    int id = 100;
    int order_id = 200;
    string first_name = "Tom";
    string last_name = "Brady";
    string company = "Body Armor";
    string email = "tom.brady@ibsimplant.net";
    string phone = "888.888.8888";
    int items_total = 5;
    string shipping_method = "UPS Ground";
    string cost_ex_tax = "na";

    string street = "7830 W Sahara Ave";
    string city = "Las Vegas";
    string state = "NV";
    string zipcode = "89117";
    string country = "USA";


    //Arrange test
    B2CShippingAddress shippingAddress = new B2CShippingAddress
      (id, order_id, first_name, last_name, company, street, city, zipcode, state,
      country, email, phone, items_total, shipping_method, cost_ex_tax);
    FishbowlService fishbowlService = new FishbowlService();

    //Act test
    FBShippingAddress fBShippingAddress = fishbowlService.CreateFBShippingAddress(shippingAddress);

    //Assert test
    fBShippingAddress.Should().NotBeNull();
    fBShippingAddress.street.Should().Be(street);
    fBShippingAddress.city.Should().Be(city);
    fBShippingAddress.state.Should().Be(state);
    fBShippingAddress.country.Should().Be(country);
    fBShippingAddress.zipcode.Should().Be(zipcode);

  }

  [Fact]
  /*
   Check if B2C order billing address is copy correctly
   to FB order import shipping address.
   */
  public void CheckBillingAddress()
  {
    string first_name = "Tom";
    string last_name = "Ford";
    string company = "Ford Company";
    string street_1 = "3860 El Dorado";
    string street_2 = "STE 501";
    string city = "El Dorado Hills";
    string state = "CA";
    string country = "USA";
    string zipcode = "95762";
    string phone = "999.999.9999";
    string email = "tom.ford@ibsimplant.net";

    //Arrange test
    B2CBillingAddress _billingAddresss = new B2CBillingAddress(first_name,
      last_name, company, street_1, street_2, city,
      state, zipcode, country, phone, email);

    FishbowlService fishbowlService2 = new FishbowlService();

  //Act test
    FBBillingAddress copiedAddress2 = fishbowlService2.CreateFBBillingAddress(_billingAddresss);

  //Assert test
   copiedAddress2.Should().NotBeNull(); 
   copiedAddress2.billToName.Should().Be(first_name + " " + last_name);
    copiedAddress2.street.Should().Be(street_1);
    copiedAddress2.city.Should().Be(city);
    copiedAddress2.state.Should().Be(state);
    copiedAddress2.zipcode.Should().Be(zipcode);
    copiedAddress2.country.Should().Be(country);



  }

  [Fact]
  /*
 Check if B2C order product item is copy correctly
 to FB order import product item.
 */
  public void CheckFBSOItemCreation() 
  {

    string id = "222";
    string order_id = "bc_333";
    string product_id = "100";
    string sku = "453M4511";
    int quantity = 10;
    double base_price = 420;


    //Arrange test
    B2COrderProduct b2COrderProduct = new B2COrderProduct(id,order_id,product_id,sku,quantity,base_price);
    FishbowlService fishbowlService = new FishbowlService();

    //Act test

    FBSOItem fbSOItem = fishbowlService.CreateFBSOItem(b2COrderProduct);

    //Assert test
    fbSOItem.Should().NotBeNull();
    fbSOItem.ProductNumber.Should().Be(sku);
    fbSOItem.ProductPrice.Should().Be(base_price);
    fbSOItem.ProductQuantity.Should().Be(quantity);
   }

  [Fact]
  /*
 Check if B2C order details are copy correctly
 to FB order import order.
 */
  public void CheckFBSO() 
  {
    //Arrange test
    FishbowlService fishbowlService = new FishbowlService();

    FBShippingAddress fBShippingAddress = fishbowlService.CreateFBShippingAddress(Get_B2CShippingAddress());
    FBBillingAddress fBBillingAddress = fishbowlService.CreateFBBillingAddress(Get_B2CBillingAddress());

    FBSOItem item1 = fishbowlService.CreateFBSOItem(Get_B2COrderProduct_1());
    FBSOItem item2 = fishbowlService.CreateFBSOItem(Get_B2COrderProduct_2());
    FBSOItem item3 = fishbowlService.CreateFBSOItem(Get_B2COrderProduct_3());

    FBSO fbSO = fishbowlService.CreateFBSO(new List<FBSOItem>(),
      fBBillingAddress, fBShippingAddress);

    //Act test
    fbSO.AddFBSOItem(item1);
    fbSO.AddFBSOItem(item2);
    fbSO.AddFBSOItem(item3);

    //Assert test
    fbSO.GetFBSOItem(0).b2cOrderItemId.Should().Be(Get_B2COrderProduct_1().id);
    fbSO.GetFBSOItem(1).b2cOrderItemId.Should().Be(Get_B2COrderProduct_2().id);
    fbSO.GetFBSOItem(2).b2cOrderItemId.Should().Be(Get_B2COrderProduct_3().id);

  }
}
