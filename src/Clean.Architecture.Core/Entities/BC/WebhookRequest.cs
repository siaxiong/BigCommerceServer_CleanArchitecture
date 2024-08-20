using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Core.Entities.BC;
public class WebhookRequest
{
  public string scope;
  public string store_id;
  public WebhookRequestData data;
  public string hash;
  public BigInteger created_at;
  public string producer;

  public WebhookRequest(string scope, string store_id, WebhookRequestData data, string hash, BigInteger created_at, string producer)
  {
    this.scope = scope;
    this.store_id = store_id;
    this.data = data;
    this.hash = hash;
    this.created_at = created_at;
    this.producer = producer;
  }
}
