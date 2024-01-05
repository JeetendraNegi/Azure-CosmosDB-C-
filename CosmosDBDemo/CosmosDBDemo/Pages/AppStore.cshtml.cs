using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CosmosDBDemo.DataModel;
using CosmosDBDemo.Models;

namespace CosmosDBDemo.Pages
{
    public class AppStoreModel : PageModel
    {
        private static DataContext context = new DataContext();
        public async Task OnGet()
        {
            List<Apps> apps = await context.GetAppsAsync();
            ViewData["Apps"] = apps;
        }

        public async Task<ActionResult> OnPostDelete(Guid id)
        {
            await context.DeleteAppAsync(id);
            return RedirectToPage("/AppStore");
        }
    }
}
