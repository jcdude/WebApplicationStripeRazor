using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe.Checkout;

namespace WebApplicationStripeRazor.Pages
{
    public class successModel : PageModel
    {
        public string returnValue { get; set; }
        public void OnGet(string id)
        {
            // lookup the stripe session id from the database

            var tokenId = "saved stripe id";

            var service = new SessionService();
            var result = service.Get(
              tokenId
            );

            returnValue = result.PaymentStatus;
        }

    }
}
