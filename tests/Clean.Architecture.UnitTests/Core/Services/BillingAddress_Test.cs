// Ignore Spelling: Fb

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clean.Architecture.Core.Entities.Fishbowl;
using FluentAssertions;
using Xunit;
using Clean.Architecture.Core.Services.Fishbowl;
using Moq;

namespace Clean.Architecture.UnitTests.Core.Services;
public class BillingAddress_Test
{


  [Fact]
  public void Is_FbBillToAddress_Exist()
  {
    var mock = new Mock<IFBBillingAddressService>();

    mock.Setup(foo => foo.IGetFbBillingAddress()).Returns("hello");

    mock.Object.IGetFbBillingAddress().Should().NotBeNull();
    mock.Object.IGetFbBillingAddress().Should().NotBeEmpty();

  }

  [Fact]
  public void Is_FbBillToAddress_Created()
  {
    var mock2 = new Mock<IFBBillingAddressService>();

/*     mock2.Setup(foo => foo.ICreateFbBillingAddress(It.IsAny<string>())).Returns(It.IsAny<string>());
*/

    //the below tests are not working right. need to fix them
    IFBBillingAddressService fb = mock2.Object;
    fb.ICreateFbBillingAddress("  ").Should().NotBeEmpty();
    fb.ICreateFbBilling(false).Should().NotBe(true);


  }




}
