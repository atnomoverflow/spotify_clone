using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace Spotify_clone2.Controllers
{
  public class SuccessController : Controller
  {
    [HttpGet("/order/success")]
    public ActionResult OrderSuccess([FromQuery] string session_id)
    {
      var sessionService = new SessionService();
      Session session = sessionService.Get(session_id);

      var customerService = new CustomerService();
      Customer customer = customerService.Get(session.CustomerId);

      return View();
    }
  }
}