using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RandPasscode.Models;
using Microsoft.AspNetCore.Http;


namespace RandPasscode.Controllers
{
    public class HomeController : Controller
    {

        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            // if not in session(this is how session works its wild what even is this.)
            if(HttpContext.Session.GetInt32("count")==null)
            {
                // set the count to start at the 0 index.
                HttpContext.Session.SetInt32("count", 0);
                //  Gotta set the variable to use in other places by making a non-nullable variable nullable.
                int? IntVariable = HttpContext.Session.GetInt32("count");
            }
            // THIS IS THE VIEWBAG FOR SESSION
             ViewBag.Pigeons = HttpContext.Session.GetString("rand");
             ViewBag.Counter = HttpContext.Session.GetInt32("count");
            return View();
        }

        [Route("generate")]
        [HttpGet]

        // When user clicks generate button it generates a new passcode using this method
        public IActionResult Generate()
        {
            string passcode = "";

            List<string> lower_case = new List<string>(){"a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"};
            List<string> numbers = new List<string>(){"1","2","3","4","5","6","7","8","9","0"};
            List<string> upper_case = new List<string>(){"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"};

            // in order for the code to pick a random number/letter all lists have to be active. This time you can put a list in a list because its a new list that holds all the previous lists.

            List<List<string>>all_lists = new List<List<string>>(){lower_case, numbers, upper_case};

            // Use the random method in order to make the computer pick a random item from each other the lists.
            Random rand = new Random();
            // Loop through the lists so it picks 14 numbers/letters
            for(var i =  0; i<=14; i++)
            {
                // New lists that creates a random list that takes in all_lists and counts randomly from 0 to the length of the list. 
                List<string> randlist  = all_lists[rand.Next(0, all_lists.Count)];
                passcode = passcode + randlist[rand.Next(0,randlist.Count)];

                System.Console.WriteLine(passcode);
                
            }
            // In order for the viewbag to work in the index ya GOTTA FUCKIN DO SOMETHING WITH IT IN THE CONTROLLER.
            

            // From the H to the end, it returns a non-nullable value which means it wont work so you need a nullable value, which is what is happening below (called casting) its stupid.
            int temp = (int)HttpContext.Session.GetInt32("count")+1;
            // this line reassigns the count key in session to the temp value.
            HttpContext.Session.SetInt32("count", temp);
            
            
            // stores the passcode in session and now can access in the index method
            HttpContext.Session.SetString("rand", passcode);
            // MAKE SURE THIS LINE DOES NOT HAVE A SPACE BETWEEN A METHOD AND THE PARENTHESES. 
            return RedirectToAction("Index");
        }

    }
}
