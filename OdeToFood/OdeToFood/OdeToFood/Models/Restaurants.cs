using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OdeToFood.Models
{
    public class Restaurants
    {
        [Key]
        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public virtual ICollection<RestaurantReviews> Reviews { get; set; }
    }
}