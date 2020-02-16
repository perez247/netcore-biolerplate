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
        /// Returns not found if no api is found
        /// </summary>
        /// <returns>index.html</returns>
        public IActionResult Index() {
            return NotFound();
        }
    }

}   