using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FoundryMissionsCom
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                "RandomMission",
                "missions/random",
                new { controller = "Missions", action = "Random" }
            );

            routes.MapRoute(
                "SubmitMission",
                "missions/submit",
                new { controller = "Missions", action = "Submit" }
            );

            routes.MapRoute(
                "SearchMission",
                "missions/search",
                new { controller = "Missions", action = "Search" }
            );

            routes.MapRoute(
                "EditMissionLink",
                "missions/edit/{link}",
                new { controller = "Missions", action = "Edit", link = "{link}" }
            );

            routes.MapRoute(
                "MissionLink",
                "missions/{link}",
                new { controller = "Missions", action = "Details" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
