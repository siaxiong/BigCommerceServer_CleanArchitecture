using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.Entities.BC;
public class WebhookRequestData
{
  public string type;
  public int id;

  public WebhookRequestData(string type, int id)
  {
    this.type = type;
    this.id = id;
  }
}
