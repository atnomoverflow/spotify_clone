using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using Spotify_clone2.Configuration;
using Spotify_clone2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Spotify_clone2.Repositories;
namespace server.Controllers
{
    public class PaymentsController : Controller
    {
        public readonly IOptions<StripeOptions> options;
        private readonly IStripeClient client;
        private readonly UserManager<User> _userManager;
        private readonly IMembershipRepository _membershipRepository;
        private readonly IClientRepository _clientRepository;
        public PaymentsController(IOptions<StripeOptions> options, UserManager<User> userManager, IMembershipRepository membershipRepository 
            ,IClientRepository clientRepository)
        {
            _userManager = userManager;
            this.options = options;
            this.client = new StripeClient(this.options.Value.SecretKey);
            _membershipRepository = membershipRepository;
            _clientRepository = clientRepository;
        }

        [HttpGet("config")]
        public ConfigResponse Setup()
        {
            return new ConfigResponse
            {
                PremiumPrice = this.options.Value.PremiumPrice,
                PublishableKey = this.options.Value.PublishableKey,
            };
        }
        [Authorize(Roles = "client")]
        [HttpPost("create-checkout-session")]
        public async Task<IActionResult> CreateCheckoutSession()
        {
            var options = new SessionCreateOptions
            {
                SuccessUrl = $"{this.options.Value.Domain}/order/success?session_id={{CHECKOUT_SESSION_ID}}",
                CancelUrl = $"{this.options.Value.Domain}",
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                Mode = "subscription",
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Price = this.options.Value.PremiumPrice,
                        Quantity = 1,
                    },
                },
                // AutomaticTax = new SessionAutomaticTaxOptions { Enabled = true },
            };
            var service = new SessionService(this.client);
            try
            {
                var session = await service.CreateAsync(options);
                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);
            }
            catch (StripeException e)
            {
                Console.WriteLine(e.StripeError.Message);
                return BadRequest(new ErrorResponse
                {
                    ErrorMessage = new ErrorMessage
                    {
                        Message = e.StripeError.Message,
                    }
                });
            }
        }

        [HttpGet("checkout-session")]
        public async Task<IActionResult> CheckoutSession(string sessionId)
        {
            var service = new SessionService(this.client);
            var session = await service.GetAsync(sessionId);
            return Ok(session);
        }
        private async Task<User> GetCurrentCustomer()
        {
            return await _userManager.GetUserAsync(HttpContext.User);
        }

        [Authorize(Roles = "client")]
        [HttpPost("customer-portal")]
        public async Task<IActionResult> CustomerPortal(string sessionId)
        {
            var customer = await GetCurrentCustomer();
            if (customer == null)
            {
                return BadRequest();
            }

            // This is the URL to which your customer will return after
            // they are done managing billing in the Customer Portal.
            var returnUrl = this.options.Value.Domain;

            var options = new Stripe.BillingPortal.SessionCreateOptions
            {
                Customer = customer.Id,
                ReturnUrl = returnUrl,
            };
            var service = new Stripe.BillingPortal.SessionService(this.client);
            var session = await service.CreateAsync(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
        private async Task updateSubscription(Subscription subscription)
        {
            try
            {
                var subscriptionFromDb = await _membershipRepository.GetByIdAsync(subscription.Id);
                if (subscriptionFromDb != null)
                {
                    subscriptionFromDb.Status = subscription.Status;
                    subscriptionFromDb.CurrentPeriodEnd = subscription.CurrentPeriodEnd;
                    await _membershipRepository.UpdateAsync(subscriptionFromDb);
                    Console.WriteLine("Subscription Updated");
                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);

                Console.WriteLine("Unable to update subscription");

            }

        }

        private async Task addCustomerIdToUser(Customer customer)
        {
            try
            {
                var userFromDb = await _userManager.FindByEmailAsync(customer.Email);

                var clientFromDb = await _clientRepository.GetByUserIdAsync(userFromDb.Id);

                if (userFromDb != null)
                {
                    clientFromDb.CustomerId = customer.Id;
                    await _userManager.UpdateAsync(userFromDb);
                    Console.WriteLine("Customer Id added to user ");
                }

            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Unable to add customer id to user");
                Console.WriteLine(ex);
            }
        }

        private async Task addSubscriptionToDb(Subscription subscription)
        {
            try
            {
                var membership = new Memebership
                {
                    Id = subscription.Id,
                    CustomerId = subscription.CustomerId,
                    Status = "active",
                    CurrentPeriodEnd = subscription.CurrentPeriodEnd
                };
                await _membershipRepository.CreateAsync(membership);

                //You can send the new membership an email welcoming the new membership
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Unable to add new membership to Database");
                Console.WriteLine(ex.Message);
            }
        }
        [HttpPost("webhook")]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            Event stripeEvent;
            try
            {
                stripeEvent = EventUtility.ConstructEvent(
                    json,
                    Request.Headers["Stripe-Signature"],
                    this.options.Value.WebhookSecret
                );
                Console.WriteLine(stripeEvent.Type);
                // Handle the event
                if (stripeEvent.Type == Events.CustomerSubscriptionCreated)
                {
                    var subscription = stripeEvent.Data.Object as Subscription;
                    //Do stuff
                    await addSubscriptionToDb(subscription);
                }
                else if (stripeEvent.Type == Events.CustomerSubscriptionUpdated)
                {
                    var session = stripeEvent.Data.Object as Stripe.Subscription;

                    // Update Subsription
                    await updateSubscription(session);
                }
                else if (stripeEvent.Type == Events.CustomerCreated)
                {
                    var customer = stripeEvent.Data.Object as Customer;
                    //Do Stuff
                    await addCustomerIdToUser(customer);
                }
                // ... handle other event types
                else
                {
                    // Unexpected event type
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something failed {e}");
                return BadRequest();
            }
        }
    }
}