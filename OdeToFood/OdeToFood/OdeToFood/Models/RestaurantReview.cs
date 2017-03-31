using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OdeToFood.Models
{
    public class RestaurantReviews : IValidatableObject
    {
        [Key]
        public int Id { get; set; }
        
        [Range(1,10)]
        [Required]
        public int Rating { get; set; }
        
        [Display(Name="User Name")]
        [DisplayFormat(NullDisplayText="Anonymous")]  //if no name provide the default is Anonymous
        public string ReviewerName { get; set; }
        
        [Required]
        [StringLength(1024)]
        public string Body { get; set; }

        public int RestaurantId { get; set; }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Rating < 2 && ReviewerName.ToLower().StartsWith("anonymous"))
            {
                yield return new ValidationResult("Sorry anonymous you can't do this.");
            }
        }
    }
}