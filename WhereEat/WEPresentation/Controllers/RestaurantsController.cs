using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WEPresentation.Models;
using Excel;
using static WEPresentation.Enums.Enums;

namespace WEPresentation.Controllers
{
    public class RestaurantsController : Controller
    {
        private WEContext db = new WEContext();

        // GET: Restaurants
        public ActionResult Index()
        {
            return View(db.Restaurants.ToList());
        }
        [Authorize]
        public ActionResult Upload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadFile(HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    Stream stream = upload.InputStream;

                    IExcelDataReader reader = null;

                    if (upload.FileName.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (upload.FileName.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }

                    reader.IsFirstRowAsColumnNames = true;

                    DataSet result = reader.AsDataSet();
                    reader.Close();

                    List<Restaurant> restaurants = new List<Restaurant>();

                    foreach(DataRow row in result.Tables[0].Rows)
                        restaurants.Add(new Restaurant { Name = row["Restaurante"].ToString(), Category = (Category)Enum.Parse(typeof(Category), row["Categoria"].ToString(), true) });

                    db.Restaurants.AddRange(restaurants);
                    db.SaveChanges();
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }
            return RedirectToAction("Index");
        }
        [Authorize]
        public ActionResult Random()
        {
            int number = 0;
            List<Restaurant> restaurants = db.Restaurants.ToList();
            if(restaurants.Count > 0)
            {
                Configuration config = db.Configurations.FirstOrDefault();
                Restaurant restaurantChoiced = new Restaurant();

                int minIdRestaurant = restaurants.Min(r => r.Id);
                int maxIdRestaurant = restaurants.Max(r => r.Id);

                do
                {
                    Random random = new System.Random();
                    number = random.Next(minIdRestaurant, maxIdRestaurant);
                    restaurantChoiced = restaurants.FirstOrDefault(r => r.Id == number);
                }
                while (restaurantChoiced.Category != config.Category);


                if (number > 0)
                {
                    db.Logs.Add(new Log { Id = Guid.NewGuid(), Restaurant = restaurantChoiced, Date = DateTime.Now });
                    db.SaveChanges();
                }
            }
            

            return RedirectToAction("Details", new { id = number });
        }
        // GET: Restaurants/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null || restaurant.Id == 0)
            {
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

        // GET: Restaurants/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Category")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Restaurants.Add(restaurant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(restaurant);
        }

        // GET: Restaurants/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,Name,Category")] Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(restaurant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(restaurant);
        }

        // GET: Restaurants/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Restaurant restaurant = db.Restaurants.Find(id);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Restaurant restaurant = db.Restaurants.Find(id);
            db.Restaurants.Remove(restaurant);
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
    }
}
