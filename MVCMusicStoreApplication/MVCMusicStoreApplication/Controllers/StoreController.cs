using MVCMusicStoreApplication.Data;
using MVCMusicStoreApplication.Models;
using System.Linq;
using System.Web.Mvc;

namespace MVCMusicStoreApplication.Controllers
{
    public class StoreController : Controller
    {
        private MVCMusicStoreDB db = new MVCMusicStoreDB();

        // GET: Browse
        public ActionResult Browse()
        {
            return View(db.Genres.OrderBy(o => o.Name));
        }

        /// <summary>
        /// GET: Index
        /// </summary>
        /// <param name="id"> The ID of the Genre. </param>
        /// <returns></returns>
        public ActionResult Index(int id)
        {
            IQueryable<Album> albums = db.Albums.Where(o => o.GenreId == id)
                .OrderBy(o => o.Title);

            return View(albums);
        }

        /// <summary>
        /// GET: Details
        /// </summary>
        /// <param name="id"> The ID of the Album. </param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            return View(db.Albums.Where(o => o.AlbumId == id));
        }
    }
}