using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CosmosDBDemo.DataModel;
using CosmosDBDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CosmosDBDemo.Pages
{
    public class editAppModel : PageModel
    {
        [BindProperty]
        public Apps apps { get; set; }

        private static DataContext context = new DataContext();
        public async Task OnGet(string id)
        {
            apps = await context.GetAppsByIdAsync(id);
        }

        public async Task<ActionResult> OnPost()
        {
            await context.UpdateAppAsync(apps);
            return RedirectToPage("/AppStore");
        }
    }
}
