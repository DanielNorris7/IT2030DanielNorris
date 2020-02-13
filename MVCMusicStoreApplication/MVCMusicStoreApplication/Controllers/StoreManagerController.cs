using MVCMusicStoreApplication.Data;
using MVCMusicStoreApplication.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MVCMusicStoreApplication.Controllers
{
    public class StoreManagerController : Controller
    {
        private MVCMusicStoreDB db = new MVCMusicStoreDB();

        // GET: StoreManager
        public ActionResult Index()
        {
            var albums = db.Albums.Include(a => a.Artist).Include(a => a.Genre);
            return View(albums.ToList());
        }

        // GET: StoreManager/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // GET: StoreManager/Create
        public ActionResult Create()
        {
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name");
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name");
            return View();
        }

        // POST: StoreManager/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // GET: StoreManager/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // POST: StoreManager/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumId,GenreId,ArtistId,Title,Price,AlbumArtUrl,CountryOfOrigin,InStock")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistId = new SelectList(db.Artists, "ArtistId", "Name", album.ArtistId);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", album.GenreId);
            return View(album);
        }

        // GET: StoreManager/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: StoreManager/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Test()
        {
            DbSet<Album> albumns = db.Albums;

            // Find an album by Album Name
            //IQueryable<Album> albumByName = from a in albumns
            //                                where a.Title == "Stormbringer"
            //                                select a;

            IQueryable<Album> albumByName = albumns.Where(a => a.Title == "Stormbringer");


            // Find an album by Artist Name
            //IQueryable<Album> albumByArtist = from a in albumns
            //                                  where a.Artist.Name == "Chic"
            //                                  select a;

            IQueryable<Album> albumByArtist = albumns.Where(a => a.Artist.Name == "Chic");


            // Find an album by Genre
            //IQueryable<Album> albumByGenre = from a in albumns
            //                                 where a.Genre.Name == "Classical"
            //                                 orderby a.Title descending
            //                                 select a;

            IQueryable<Album> albumByGenre = albumns.Where(a => a.Genre.Name == "Classical").OrderByDescending(a => a.Title);

            ViewBag.AlbumsByName = new SelectList(albumByName, "AlbumId", "Title", albumByName.First().AlbumId);
            ViewBag.AlbumsByArtist = new SelectList(albumByArtist, "AlbumId", "Title", albumByArtist.First().AlbumId);
            ViewBag.AlbumsByGenre = new SelectList(albumByGenre, "AlbumId", "Title", albumByGenre.First().AlbumId);

            return View();
        }
    }
}
