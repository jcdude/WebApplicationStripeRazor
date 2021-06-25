using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationStripeRazor.Models
{
    public class CreditCardModel
    {
        [BindProperty]
        public string CardNumber { get; set; }

        [BindProperty]
        public string Cvc { get; set; }

        [BindProperty]
        public int Month { get; set; }

        [BindProperty]
        public int Year { get; set; }
    }
}
