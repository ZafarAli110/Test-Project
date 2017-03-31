using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.Controllers
{
    public class CuisineController : Controller
    {
        // GET: Cuisine
        public ActionResult Search(string name = "French")  //default parameter 
        {
            throw new Exception("something terrible was happened!");
            var message = Server.HtmlEncode(name); //use to avoid cross site scripting(xss) attacks
            return Content(message);
        }

        // this will do nothing  bcoz we define in routeConfig that we will only pass our parameter name to
        // the Search action of Cuisine Controller...So Mvc dont look to any other action except Search in this controller 
        // Note that we are passing the Parameter after the Controller Name & we are not passing the action result 
        // e.g /Cuisine/french  note that the second route value is parameter name not the action result name
        // so /Cuisine/Index will treat Index as a parameter name and not as action result.
        
        public ActionResult Index() 
        {
            // we will never reach this method unless our routing request dont match with the our cuisine routing settings 
            // or change our routing  configuration of this controller....
            // Remember if our first routing dont match then MVC will follow its convention and will look for the Index
            // Method in the controller so we will end up here
            // Note that if we dont provide the name parameter then by Convention MVC will look for the Index
            // Method despite the fact that we explicitly provide the default action name in our Cuisine route config
            return Content("Hello!");
        }
    }
}