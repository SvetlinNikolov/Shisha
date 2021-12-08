using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ShishaProject.Services.Data.Models.Configs;
using ShishaProject.Services.Interfaces;
using ShishaProject.Web.ViewModels.Payment;
using Stripe;
using Stripe.Checkout;

namespace ShishaProject.Services
{
    public class StripeService : IStripeService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public StripeService(
            IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public StatusCodeResult CreatePayment(StripeChargeInputModel inputModel)
        {
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                      PriceData = new()
                      {
                          UnitAmount = inputModel.Price,
                          Currency = "BGN", // maybe need to change this to dynamic and not only bgn
                          ProductData = new()
                          {
                              Name = inputModel.ProductName,
                          },
                      },
                      Quantity = inputModel.Quantity,
                  },
                },
                Mode = "payment",
                SuccessUrl = "https://www.google.com",
                CancelUrl = "https://yahoo.com",
            };
            var service = new SessionService();
            Session session = service.Create(options);

            this.httpContextAccessor.HttpContext.Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult((int)HttpStatusCode.SeeOther);
        }


    }
}
