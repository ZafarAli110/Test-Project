using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OdeToFood.Models
{
    public class OdeToFoodDb : DbContext
    {
        public OdeToFoodDb() : base("name = DefaultConnection")
        {

        }
        public DbSet<Restaurants> Restaurants { get; set; }
        public DbSet<RestaurantReviews> Reviews { get; set; }

        public System.Data.Entity.DbSet<OdeToFood.Models.RestaurantListViewModel> RestaurantListViewModels { get; set; }

    }
}