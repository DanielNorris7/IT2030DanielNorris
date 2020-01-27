using Microsoft.AspNetCore.Mvc;

namespace Lab2.Controllers
{
    public class ProductController : Controller
    {
        /// <summary>
        /// GET: /Product/
        /// </summary>
        /// <returns></returns>
        public string Index()
        {
            return "Product/Index is displayed";
        }

        /// <summary>
        /// GET: /Product/Browse/
        /// </summary>
        /// <returns></returns>
        public string Browse()
        {
            return "Browse displayed";
        }

        /// <summary>
        /// GET: /Product/Details/ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string Details(long ID)
        {
            return $"Details displayed for Id = {ID}";
        }

        /// <summary>
        /// GET: /Product/LocationCode/locationCode
        /// </summary>
        /// <param name="locationCode"></param>
        /// <returns></returns>
        public string LocationCode(long locationCode)
        {
            return $"Location displayed for zip = {locationCode}";
        }
    }
}