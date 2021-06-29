using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;
using Stripe.Checkout;

namespace WebApplicationStripeRazor.Pages
{
    public class checkoutModel : PageModel
    {
        public void OnGet()
        {

        }

        public void OnPostSubmit()
        {
            //Create the product
            var productCreateGoldOptions = new ProductCreateOptions
            {
                Name = "Gold Special",
            };
            var productCreateGoldService = new ProductService();
            Product productCreateGoldToken = productCreateGoldService.Create(productCreateGoldOptions);

            var productCreateSilverOptions = new ProductCreateOptions
            {
                Name = "Silver Special",
            };
            var productCreateSilverService = new ProductService();
            Product productCreateSilverToken = productCreateSilverService.Create(productCreateSilverOptions);

            //Create the price
            var priceCreateGoldOptions = new PriceCreateOptions
            {
                UnitAmount = 2000,
                Currency = "usd",
                Recurring = new PriceRecurringOptions
                {
                    Interval = "month",
                },
                Product = productCreateGoldToken.Id,
            };
            var priceCreateGoldService = new PriceService();
            Price priceCreateGoldToken = priceCreateGoldService.Create(priceCreateGoldOptions);

            var priceCreateSilverOptions = new PriceCreateOptions
            {
                UnitAmount = 1000,
                Currency = "usd",
                Recurring = new PriceRecurringOptions
                {
                    Interval = "month",
                },
                Product = productCreateSilverToken.Id,
            };
            var priceCreateSilverService = new PriceService();
            Price priceCreateSilverToken = priceCreateSilverService.Create(priceCreateSilverOptions);

            //Create a session token for the redirect to the stripe checkout
            var options = new SessionCreateOptions
            {
                SuccessUrl = "https://localhost:44320/success?id=yourtransactionid",
                CancelUrl = "https://localhost:44320/cancel",
                PaymentMethodTypes = new List<string>
                  {
                    "card",
                  },
                  LineItems = new List<SessionLineItemOptions>
                  {
                    new SessionLineItemOptions
                    {
                      Price = priceCreateGoldToken.Id,
                      Quantity = 1,
                    },
                    new SessionLineItemOptions
                    {
                      Price = priceCreateSilverToken.Id,
                      Quantity = 3,
                    },
                  },
                Mode = "subscription",
            };
            var service = new SessionService();
            var sessionToken = service.Create(options);

            //Save to database
            //sessionToken.Id

            Response.Redirect(sessionToken.Url);
        }
    }
}
