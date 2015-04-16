using BookARoom.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookARoom.Pages.BookingPages
{
    public partial class BookingDelete : System.Web.UI.Page
    {
        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        //Lagrar rum från route som int i egenskap.
        protected int Room
        {
            get { return int.Parse(RouteData.Values["room"].ToString()); }
        }

        //Lagrar id från route som int i egenskap
        protected int Id
        {
            get { return int.Parse(RouteData.Values["id"].ToString()); }
        }

        //Lagrar date från route som sträng i egenskap
        protected string Date
        {
            get { return (string)RouteData.Values["date"]; }
        }

        //Lagrar hour från route som sträng i egenskap
        protected string Hour
        {
            get { return (string)RouteData.Values["hour"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Sätter åter länk vid avbryt av radering tillbaka till detaljer
            CancelHyperLink.NavigateUrl = GetRouteUrl("BookingDetails", new {room = Room, id = Id, date = Date, hour = Hour });

            //Läser in bokningsinformation (för att presenteras i "är du säker" meddelande)
            if (!IsPostBack)
            {
                try
                {
                    var booking = Service.GetBooking(Room, Id, Date, Hour);
                    if (booking != null)
                    {
                        BookingDateTime.Text = booking.StartTime.ToString("yy-MM-dd HH:mm");
                        return;
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, "Fel! Bokningen hittades inte.");
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "Fel inträffade vid inläsning av bokningsinformation.");
                }
            }
        }

        /// <summary>
        /// Raderar bokning
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DeleteLinkButton_Command(object sender, CommandEventArgs e)
        {
            try
            {
                Service.DeleteBooking(Room, Id, Date, Hour);

                //Sätt meddelande om lyckad borttagning av kunddata
                Page.SetTempData("Message", String.Format("<strong>Lyckat!</strong> Bokning <strong>{0}</strong> klockan <strong>{1}:00</strong> borttagen.", Date, Hour));

                //PRG
                Response.RedirectToRoute("Bookings", new { room = Room.ToString(), date = Date });
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Fel inträffade vid radering av bokning.");
            }
        }
    }
}