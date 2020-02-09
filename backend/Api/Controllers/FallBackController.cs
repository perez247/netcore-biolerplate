using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// Fallback controller that returns the front end application
    /// </summary>
    public class FallBackController : Controller
    {
        /// <summary>
        /// The action that returns the app files
        /// </summary>
        /// <returns>index.html</returns>
        public IActionResult Index() {
            return Ok(new { ok = "hallo" });
            // return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/HTML");
        }
    }

    
    // /// <summary>
    // /// Fallback controller that returns the front end application
    // /// </summary>
    // [Route("api/")]
    // public class FallBackApiController : Controller
    // {
    //     /// <summary>
    //     /// The action that returns the app files
    //     /// </summary>
    //     /// <returns>index.html</returns>
    //     [Route("")]
    //     public IActionResult Index() {
    //         return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/HTML");
    //     }
    // }
}   