using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CosmosDBDemo.Models;
using CosmosDBDemo.DataModel;

namespace CosmosDBDemo.Pages
{
    public class addNewAppModel : PageModel
    {
        [BindProperty]
        public Apps apps {get; set;}

        private static DataContext context = new DataContext();
        public void OnGet()
        {
        }

        public async Task<ActionResult> OnPost()
        {
            var app = new Apps
            {
                id = Guid.NewGuid().ToString(),
                AppName=apps.AppName,
                Org = apps.Org,
                Date = apps.Date,
                Version = apps.Version,
                feedback = new Feedback
                {
                    feedbackID = Guid.NewGuid().ToString(),
                    rating = apps.feedback.rating,
                    review = apps.feedback.review
                }
            };
            await context.CreateAppAsync(app);
            return RedirectToPage("/AppStore");
        }
    }
}
