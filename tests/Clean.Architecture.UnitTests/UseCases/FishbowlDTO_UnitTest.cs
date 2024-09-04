
using Clean.Architecture.Core.Entities.BC;
using Clean.Architecture.Core.Entities.Fishbowl;
using Clean.Architecture.UseCases.DTO;
using FluentAssertions;
using Moq;
using Xunit;


/*
 *These tests are meant to make sure Big Commerce
 * orders are converted correctly to Fishbowl orders.
 *
 */

public class FishbowlDT_UnitTest
{
  
  FishbowlDTO fishBowl_DTO = new FishbowlDTO();

  [Fact]
  public void CheckShippingAddressDTO()
  {
    //Arrange test
    
    //Need to test commas because commas cannot exist in the values 
    //because these values will use in a CSV string which will contain the 
    //correct # of commas for their parameter position
    var bc_ShippingAdd = new Mock<BC_ShippingAddress>(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()+",,,,,",
      It.IsAny<string>()+",,,,,", It.IsAny<string>()+",,,,,", It.IsAny<string>()+",,,,,", It.IsAny<string>()+",,,,,", It.IsAny<string>(),
      It.IsAny<string>()+",,,,,", It.IsAny<string>()+",,,,,", It.IsAny<string>()+",,,,,", It.IsAny<string>()+",,,,,", It.IsAny<int>(), It.IsAny<string>()+",,,,,",
      It.IsAny<string>()+",,,,,");
    //Act test
    var fb_shippingAdd = fishBowl_DTO.ShippingAddressDTO(bc_ShippingAdd.Object);

    //Assert test
    Assert.Equal(bc_ShippingAdd.Object.first_name + " " + bc_ShippingAdd.Object.last_name, fb_shippingAdd.ShipToName);
    Assert.Equal(bc_ShippingAdd.Object.street_1, fb_shippingAdd.street_1);
    Assert.Equal(bc_ShippingAdd.Object.street_2, fb_shippingAdd.street_2);
    Assert.Equal(bc_ShippingAdd.Object.city, fb_shippingAdd.city);
    Assert.Equal(bc_ShippingAdd.Object.state, fb_shippingAdd.state);
    Assert.Equal(bc_ShippingAdd.Object.zipcode, fb_shippingAdd.zipcode);
    Assert.Equal(bc_ShippingAdd.Object.country, fb_shippingAdd.country);
    
    Assert.DoesNotContain(",",fb_shippingAdd.ShipToName);
    Assert.DoesNotContain(",",fb_shippingAdd.street_1);
    Assert.DoesNotContain(",",fb_shippingAdd.street_2);
    Assert.DoesNotContain(",",fb_shippingAdd.city);
    Assert.DoesNotContain(",",fb_shippingAdd.state);
    Assert.DoesNotContain(",",fb_shippingAdd.zipcode);
    Assert.DoesNotContain(",",fb_shippingAdd.country);
  }

  [Fact]
  public void CheckBillingAddressDTO()
  {
    //Arrange test
    var bc_BillingAdd = new Mock<BC_BillingAddress>(It.IsAny<string>()+",,,,,", It.IsAny<string>()+",,,,,",
      It.IsAny<string>()+",,,,,", It.IsAny<string>()+",,,,,", It.IsAny<string>()+",,,,,", It.IsAny<string>()+",,,,,", It.IsAny<string>()+",,,,,",
      It.IsAny<string>()+",,,,,", It.IsAny<string>()+",,,,,", It.IsAny<string>()+",,,,,", It.IsAny<string>()+",,,,,");
    
    //Act test
    var fb_billingAdd = fishBowl_DTO.BillingAddressDTO(bc_BillingAdd.Object);
    
    //Assert test
    Assert.Equal(bc_BillingAdd.Object.first_name + " " + bc_BillingAdd.Object.last_name, fb_billingAdd.billToName);
    Assert.Equal(bc_BillingAdd.Object.street_1, fb_billingAdd.street_1);
    Assert.Equal(bc_BillingAdd.Object.street_2, fb_billingAdd.street_2);
    Assert.Equal(bc_BillingAdd.Object.city, fb_billingAdd.city);
    Assert.Equal(bc_BillingAdd.Object.state, fb_billingAdd.state);
    Assert.Equal(bc_BillingAdd.Object.zipcode, fb_billingAdd.zipcode);
    Assert.Equal(bc_BillingAdd.Object.country, fb_billingAdd.country);
    
    Assert.DoesNotContain(",",fb_billingAdd.billToName);
    Assert.DoesNotContain(",",fb_billingAdd.street_1);
    Assert.DoesNotContain(",",fb_billingAdd.street_2);
    Assert.DoesNotContain(",",fb_billingAdd.city);
    Assert.DoesNotContain(",",fb_billingAdd.state);
    Assert.DoesNotContain(",",fb_billingAdd.zipcode);
    Assert.DoesNotContain(",",fb_billingAdd.country);
  }

  [Fact]
  public void SOItemListDTO()
  {
    
    //Arrange
    var soItemList = new List<BC_OrderProduct>();

    for (int i = 0; i<10; i++)
    {
      soItemList.Add(new BC_OrderProduct(It.IsAny<int>(),It.IsAny<int>(),It.IsAny<int>(),
        It.IsAny<string>() +",,,,,", It.IsAny<string>()+",,,,,", It.IsAny<int>(), It.IsAny<double>(),
        It.IsAny<double>(), It.IsAny<string>()+",,,,,"));
    }
    //Assert
    var fbSOItemList = fishBowl_DTO.SOItemListDTO(soItemList);
    
    //Test
    for (int i = 0; i < soItemList.Count; i++)
    {
      Assert.Equal(fbSOItemList[i].b2cOrderItemId, soItemList[i].product_id.ToString());
      Assert.Equal(fbSOItemList[i].ProductPrice, soItemList[i].base_price);
      Assert.Equal(fbSOItemList[i].ProductQuantity, soItemList[i].quantity);
    }
    Assert.Equal(fbSOItemList.Count, soItemList.Count);

  }
}
