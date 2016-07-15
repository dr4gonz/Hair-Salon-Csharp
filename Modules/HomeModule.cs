using Nancy;
using System;
using System.Collections.Generic;

namespace HairSalon
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
            Get["/"] = _ => View["index.cshtml"];
        }
    }
}
