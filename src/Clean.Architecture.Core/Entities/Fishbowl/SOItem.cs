using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Clean.Architecture.Core.Entities.Fishbowl;
public class SOItem
{
  public enum SOItemType {
    Sale = 10,
    MiscSale = 11,
    DropShip = 12,
    CreditReturn = 20,
    MiscCredit = 21,
    DiscountPercentage = 30,
    DiscountAmount = 21,
    Subtotal = 40,
    AssocPrice = 50,
    Shipping = 60,
    Tax = 70,
    Kit = 80,
    Note = 90,
    BOMConfigItem = 100
  }

  [StringLength(4, ErrorMessage = "Flag variable cannot exceed 4 characters.")]
  public const string Flag = "Item";
  public SOItemType SOItemTypeID;

  [StringLength(60, ErrorMessage = "ProductNumber variable cannot exceed 60 characters.")]
  public string ProductNumber { set; get; }
  [StringLength(256, ErrorMessage = "ProductDescription variable cannot exceed 256 characters.")]
  public string ProductDescription;
  public int ProductQuantity;
  public string UOM;
 
   public SOItem(SOItemType SOItemType, string ProductNumber, string ProductDescription, int ProductQuantity, string UOM)
   {
    this.SOItemTypeID = SOItemType;
    this.ProductNumber = ProductNumber;
    this.ProductDescription = ProductDescription;
    this.ProductQuantity = ProductQuantity;
    this.UOM = UOM;

  }


}
