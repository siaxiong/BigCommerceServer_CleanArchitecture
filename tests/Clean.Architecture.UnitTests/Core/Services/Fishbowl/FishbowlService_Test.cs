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
public class FishbowlService_Test
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

    /*
      IFishbowlService _fishbowlService;

      public CheckAddressDuplication(IFishbowlService fishbowlService)
      {
        this._fishbowlService = fishbowlService;
      }*/



    [Fact]
    public void CheckAddress()
    {
      //Arrange test
      Address _address = new Address("7830 W Sahara", "Las Vegas", "NV", "89117", "USA");
      FishbowlService fishbowlService = new FishbowlService();


      //Act test
      AddressBase copiedAddress = fishbowlService.CreateFBShippingAddress(_address);


      //Assert test
      copiedAddress.Should().BeEquivalentTo(_address);
      copiedAddress.Should().NotBeNull();

    }

  [Fact]
  public void CheckAddress2()
  {
    "string".Should().NotBeEmpty();
  }
}
