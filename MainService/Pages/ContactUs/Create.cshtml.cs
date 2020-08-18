using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MainService.Data;
using MainService.Models;
using System.Text.Json;
using System.Net.Http;
using System.Text;

namespace MainService.Pages_ContactUs
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ContactUs ContactUs { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Console.WriteLine(JsonSerializer.Serialize(new Dictionary<string, string>
                {
                    { "email", ContactUs.EmailAddress },
                    { "firstName", ContactUs.FirstName },
                    { "lastName", ContactUs.LastName }
                }));

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("x-api-key", "MdUOmM06pg5nOsBARzlqfa5P9yHt0QOUaUIrGphS");
            //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            var response = await client.PostAsync("https://n9jpobea75.execute-api.us-east-1.amazonaws.com/default/CustomerDataNoSQL",
                new StringContent(JsonSerializer.Serialize(new Dictionary<string, string>
                {
                    { "email", ContactUs.EmailAddress },
                    { "firstName", ContactUs.FirstName },
                    { "lastName", ContactUs.LastName }
                })));

            if (response.StatusCode.ToString() != "200")
            {
                ModelState.AddModelError("Sorry, an issue occured with submitting the contact form.", "Sorry, an issue occured with submitting the contact form.");
            }

            return RedirectToPage("./Index");
        }
    }
}
