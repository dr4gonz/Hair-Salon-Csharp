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

            Get["/stylist/{id}"] = parameters => {
                Stylist stylist = Stylist.Find(parameters.id);
                return View["stylist.cshtml", stylist];
            };

            Post["/stylist/client/add/"] = _ => {
                string clientName = Request.Form["client-name"];
                string clientPhone = Request.Form["client-phone"];
                string clientEmail = Request.Form["client-email"];
                int clientStylistId = Request.Form["stylist-id"];
                Stylist stylist = Stylist.Find(clientStylistId);
                Client newClient = new Client(clientName, clientPhone, clientEmail, clientStylistId);
                newClient.Save();
                return View["stylist.cshtml", stylist];
            };
        }
    }
}
