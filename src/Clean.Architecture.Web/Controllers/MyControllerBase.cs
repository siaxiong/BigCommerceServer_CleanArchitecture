using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;

namespace Clean.Architecture.Web.Controllers;

[Produces(MediaTypeNames.Application.Json)]
[Route("api/[controller]")]
[ApiController]
public class MyControllerBase : ControllerBase
{
}
