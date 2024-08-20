using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.Entities.Fishbowl;
public record FB_Account (
  string CustomerName,
  string TaxRateName = "None"
  );
