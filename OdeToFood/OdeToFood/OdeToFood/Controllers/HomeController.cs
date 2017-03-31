using OdeToFood.Models;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using System.Web.UI;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        OdeToFoodDb db = new OdeToFoodDb();

        public ActionResult AutoComplete(string term) //must specify 'term' b/c jquery UI autocomplete widget requires it
        {
            var model =  db.Restaurants
                        .Where(r => r.Name.StartsWith(term))
                        .Take(10)     
                        .Select(r=>  new  {
                            label = r.Name    //setting label bcoz b/c jquery UI autocomplete widget requires it 
                        });

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //[ChildActionOnly]
        //[OutputCache(Duration =30)]
        //public ActionResult SomeAction()
        //{
        //    return Content("Hello");
        //}

            
        //[OutputCache(Duration = 50 , VaryByHeader ="X-Requested-With" , Location = OutputCacheLocation.Server)]
        [OutputCache(CacheProfile = "Long", VaryByHeader = "X-Requested-With", Location = OutputCacheLocation.Server)]
        public ActionResult Index(string searchTerm = null , int page = 1 ) //Null b/c unit testing
        {
            //Comprehension LINQ query
            #region
            // var restaurants = db.Restaurants.ToList(); 
            //var restaurants = from r in db.Restaurants
            //                  //orderby r.Name
            //                  // orderby r.Reviews.Count() descending
            //                  orderby r.Reviews.Average(review => review.Rating) descending
            //                  //select r;
            //                  select new RestaurantListViewModel
            //                  {
            //                      Id = r.RestaurantId,
            //                      Name= r.Name,
            //                      City= r.City,
            //                      Country= r.Country,
            //                      NumberOfReviews = r.Reviews.Count()
            //                  };
            #endregion
            // Extension Method Query
            
            var restaurants = db.Restaurants
                               .OrderByDescending(r => r.Reviews.Average(review=> review.Rating))
                                .Where(r=> searchTerm == null || r.Name.StartsWith(searchTerm))
                             // .Take(10)  //replace by pagedList
                               .ToList() //Key part for RestaurantListViewModel
                                .Select(r=> new RestaurantListViewModel
                              {
                                  Id = r.RestaurantId,
                                  Name= r.Name,
                                  City= r.City,
                                  Country= r.Country,
                                  NumberOfReviews = r.Reviews.Count()
                              }).ToPagedList(page, 10);
            if (Request.IsAjaxRequest())
            {
                Response.Write("invoking ajax");
                return PartialView("_RestaurantList",restaurants);
            }
            Response.Write("not invoking ajax");
            return View(restaurants);
        }

        public ActionResult Test()
        {
            var Name = RouteData.Values["controller"];
            var action = RouteData.Values["action"];
            var id = RouteData.Values["id"];

            //return RedirectToRoute("Default", new { controller = "Home", action = "Index" });
            //return File(Server.MapPath("~/Content/Site.css"),"text/css");
            //return Json(new {name="Zafar Ali" , location = "Skardu"},JsonRequestBehavior.AllowGet);
            return Content(string.Format("{0} : {1} : {2}", Name, action, id));

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your Sample Model description page.";
            //ViewBag.Name = "Zafar Ali";
            //ViewBag.location = "Karachi";

            var sampleModel = new SampleModel();
            sampleModel.Name = "Zafar Ali";
            sampleModel.Location = "Skardu";

            return View(sampleModel);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing) //coz db implements the Idisposable interface
        {
            if (db != null)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}