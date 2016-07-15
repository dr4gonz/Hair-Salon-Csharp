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

            Get["/stylist/edit/{id}"] = parameters => {
                Stylist selectedStylist = Stylist.Find(parameters.id);
                return View["stylist_edit.cshtml", selectedStylist];
            };

            Patch["/stylist/edit/{id}"] = parameters => {
              Stylist selectedStylist = Stylist.Find(parameters.id);
              selectedStylist.Update(Request.Form["stylist-name"], Request.Form["stylist-phone"], Request.Form["stylist-email"]);
              List<Stylist> allStylists = Stylist.GetAll();
              return View["index.cshtml", allStylists];
            };

            Get["/stylist/delete/{id}"] = parameters => {
                Stylist selectedStylist = Stylist.Find(parameters.id);
                return View["stylist_delete.cshtml", selectedStylist];
            };

            Delete["/stylist/delete/{id}"] = parameters => {
                Stylist selectedStylist = Stylist.Find(parameters.id);
                selectedStylist.Delete();
                List<Stylist> allStylists = Stylist.GetAll();
                return View["index.cshtml", allStylists];
            };
            //Client Routes
            Get["/client/edit/{id}"] = parameters => {
                Client selectedClient = Client.Find(parameters.id);
                return View["client_edit.cshtml", selectedClient];
            };

            Patch["/client/edit/{id}"] = parameters => {
              Client selectedClient = Client.Find(parameters.id);
              Stylist stylist = Stylist.Find(Request.Form["client-stylist-id"]);
              selectedClient.Update(Request.Form["client-name"], Request.Form["client-phone"], Request.Form["client-email"], Request.Form["client-stylist-id"]);
              return View["stylist.cshtml", stylist];
            };

            Get["/client/delete/{id}"] = parameters => {
                Client selectedClient = Client.Find(parameters.id);
                return View["client_delete.cshtml", selectedClient];
            };

            Delete["/client/delete/{id}"] = parameters => {
                Client selectedClient = Client.Find(parameters.id);
                Stylist stylist = Stylist.Find(Request.Form["client-stylist-id"]);
                selectedClient.Delete();
                return View["stylist.cshtml", stylist];
            };
        }
    }
}
