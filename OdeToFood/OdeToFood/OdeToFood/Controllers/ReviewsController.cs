using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.Controllers
{
    public class ReviewsController : Controller
    {
        private OdeToFoodDb _db = new OdeToFoodDb();

        public ActionResult Index([Bind(Prefix = "id")]int restaurantId) 
        {
            var restaurant = _db.Restaurants.Find(restaurantId);
            if (restaurant == null)
            {
                return HttpNotFound();
            }
            return View(restaurant);

        }
        [HttpGet]
        public ActionResult Create(int restaurantId) 
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RestaurantReviews review)
        {
            if (ModelState.IsValid)
            {
                _db.Reviews.Add(review);
                _db.SaveChanges();
               return RedirectToAction("Index", new { id = review.RestaurantId });
            }
            return View(review);
        }

        public ActionResult Edit([Bind(Prefix="id")]int restaurantReiewsId) 
        {
            RestaurantReviews review = _db.Reviews.Find(restaurantReiewsId);
            return View(review);
                
        }
        [HttpPost]
        public ActionResult Edit(RestaurantReviews editedReview)
        {
            //RestaurantReviews findReviewToBeEdit = _db.Reviews.Single(r => r.Id == editedReview.Id);
            //if (TryUpdateModel(findReviewToBeEdit))
            //{
            //    _db.SaveChanges();
            //     return RedirectToAction("Index", new { id = editedReview.RestaurantId });
            //}
            //return View(editedReview);
            if (ModelState.IsValid)
            {
                _db.Entry(editedReview).State = EntityState.Modified;  //updating the existing entry
                _db.SaveChanges();
                return RedirectToAction("Index", new { id = editedReview.RestaurantId });
            }
            return View(editedReview);
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }
        
        
        
        //[ChildActionOnly]
        //public ActionResult BestReview()
        //{
        //    var bestReview = from r in _review
        //                     orderby r.Rating descending
        //                     select r;
        //    return PartialView("_Review", bestReview.First());
        //}

        //// GET: Reviews
        //public ActionResult Index1()
        //{
        //    var model = from r in _review
        //                orderby r.Country
        //                select r;

        //    return View(model);
        //}

        //// GET: Reviews/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Reviews/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Reviews/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Reviews/Edit/5
        //public ActionResult Edit1(int id)
        //{
        //    var selectedReview = _review.Single(x => x.Id == id);
        //    return View(selectedReview);
        //}

        //// POST: Reviews/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    var selectedReview = _review.Single(x => x.Id == id);
        //    if (TryUpdateModel(selectedReview))
        //    {
        //        //...
        //        return RedirectToAction("Index");
        //    }
        //    return View(selectedReview);
        //}

        //// GET: Reviews/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Reviews/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //static List<RestaurantReviews> _review = new List<RestaurantReviews> 
        //{
        //    new RestaurantReviews
        //    {
        //        Id=1,
        //        Name = "First Restaurant",
        //        City ="Karachi",
        //        Country = "Pakistan",
        //        Rating = 5
        //    },
        //    new RestaurantReviews
        //    {
        //        Id=2,
        //        Name = "Second Restaurant",
        //       // City ="<script>alert('xss');</script>",
        //        City ="Lahore",
        //        Country = "Pakistan",
        //        Rating = 4
        //    },
        //    new RestaurantReviews
        //    {
        //        Id=3,
        //        Name = "Third Restaurant",
        //        City ="Peshawar",
        //        Country = "Pakistan",
        //        Rating = 3
        //    }
        //};


    }
}
