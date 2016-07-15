using Nancy;
using System;
using System.Collections.Generic;

namespace HairSalon
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => {
                List<Stylist> allStylists = Stylist.GetAll();
                return View["index.cshtml", allStylists];
            };

            Post["/stylist/add/"] = _ => {
                string name = Request.Form["stylist-name"];
                string phone = Request.Form["stylist-phone"];
                string email = Request.Form["stylist-email"];
                Stylist newStylist = new Stylist(name, phone, email);
                newStylist.Save();
                List<Stylist> allStylists = Stylist.GetAll();
                return View["index.cshtml", allStylists];
            };
        }
    }
}
