namespace OdeToFood.Migrations
{
    using OdeToFood.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<OdeToFood.Models.OdeToFoodDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "OdeToFood.Models.OdeToFoodDb";
        }

        protected override void Seed(OdeToFood.Models.OdeToFoodDb context)
        {
            context.Restaurants.AddOrUpdate(r => r.Name,
                new Restaurants { Name = "First Restaurant", City = "Karachi", Country = "Pakistan" },
                new Restaurants { Name = "Second Restaurant", City = "Lahore", Country = "Pakistan" },
                
                new Restaurants
                {
                    Name = "Third Restaurant",
                    City = "Peshawar",
                    Country = "Pakistan",
                    Reviews = new List<RestaurantReviews>() 
                    { 
                        new RestaurantReviews 
                        { 
                            Rating = 9, 
                            Body = "Great Food!" , 
                            ReviewerName = "Abc"
                        }
                    }
                } );
            for (int i = 0; i < 1000; i++)
            {
                context.Restaurants.AddOrUpdate(r=> r.Name,
                    new Restaurants 
                    { 
                        Name = i.ToString() , 
                        City ="Nowhere" , 
                        Country = "Pakistan"  
                    });
            }
        }
    }
}
