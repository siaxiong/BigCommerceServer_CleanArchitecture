using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.Entities.Fishbowl;
public record FB_SO
(
  FB_BillingAddress _fbillingAddress,
  FB_ShippingAddress _fbShippingAddress,
  List<FB_SOItem> _fbItemList
 );

