using ClientNotification.Common.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ClientNotification.Common.Controllers.Abstractions
{
    [ApiController]
    [TypeFilter(typeof(ApiExceptionFilter))]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
