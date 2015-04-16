using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace BookARoom
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            //Routes för kundsidor
            routes.MapPageRoute("CustomerListing",  "kunder",                                       "~/Pages/CustomerPages/CustomerListing.aspx");
            routes.MapPageRoute("CustomerDetails",  "kunder/{id}",                                  "~/Pages/CustomerPages/CustomerDetails.aspx");
            routes.MapPageRoute("CustomerDelete",   "kunder/{id}/tabort",                           "~/Pages/CustomerPages/CustomerDelete.aspx");

            //Routes för bokningssidor
            routes.MapPageRoute("Bookings",         "bokningar/{room}/{date}",                      "~/Pages/BookingPages/Bookings.aspx");
            routes.MapPageRoute("BookingInsert",    "bokningar/ny/{room}/{date}/{hour}",            "~/Pages/BookingPages/BookingInsert.aspx");
            routes.MapPageRoute("BookingDetails",   "bokningar/{room}/{id}/{date}/{hour}",          "~/Pages/BookingPages/BookingDetails.aspx");
            routes.MapPageRoute("BookingDelete",    "bokningar/radera/{room}/{id}/{date}/{hour}",   "~/Pages/BookingPages/BookingDelete.aspx");
            routes.MapPageRoute("BookingUpdate",    "bokningsändring/{room}/{id}/{date}/{hour}",    "~/Pages/BookingPages/BookingUpdate.aspx");

            //Error
            routes.MapPageRoute("Error",            "serverfel",                                    "~/Shared/Error.aspx");

            //Start
            routes.MapPageRoute("Default",          "",                                             "~/Pages/StaticPages/Start.aspx");
        }
    }
}