using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;
using WebApplicationStripeRazor.Models;

namespace WebApplicationStripeRazor.Pages
{
    public class DashboardModel : PageModel
    {
        public string subValue { get; set; }

        public void OnGet()
        {
            


        }
        public void OnPostSubmit(CreditCardModel cc)
        {
            //Create the customer
            var customerCreateOptions = new CustomerCreateOptions
            {
                Description = "My First Test Customer (created for API docs)",
            };
            var customerService = new CustomerService();
            Customer customerCreateToken = customerService.Create(customerCreateOptions);

            //Add the card
            var options = new CardCreateOptions
            {
                Source = "tok_visa"
            };
            var service = new CardService();
            service.Create(customerCreateToken.Id, options);

            //Create the product
            var productCreateOptions = new ProductCreateOptions
            {
                Name = "Gold Special",
            };
            var productCreateService = new ProductService();
            Product productCreateToken = productCreateService.Create(productCreateOptions);

            //Create the price
            var priceCreateOptions = new PriceCreateOptions
            {
                UnitAmount = 2000,
                Currency = "usd",
                Recurring = new PriceRecurringOptions
                {
                    Interval = "month",
                },
                Product = productCreateToken.Id,
            };
            var priceCreateService = new PriceService();
            Price priceCreateToken = priceCreateService.Create(priceCreateOptions);

            //Create the Subscription
            var subscriptionCreateOptions = new SubscriptionCreateOptions
            {
                Customer = customerCreateToken.Id,
                Items = new List<SubscriptionItemOptions>
                  {
                    new SubscriptionItemOptions
                    {
                      Price = priceCreateToken.Id,
                    },
                  },
            };
            var subscriptionService = new SubscriptionService();
            Subscription subscriptionToken = subscriptionService.Create(subscriptionCreateOptions);

            subValue = subscriptionToken.Id;

        }
    }
}
