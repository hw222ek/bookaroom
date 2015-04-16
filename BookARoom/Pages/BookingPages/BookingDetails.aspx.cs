using BookARoom.Model;
using BookARoom.Model.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookARoom.Pages.BookingPages
{
    public partial class BookingDetails : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Hämtar bokningsdata med parametrar plockade från route data.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <param name="hour"></param>
        /// <returns></returns>
        public Booking BookingListView_GetData([RouteData] int id, [RouteData] string date, [RouteData] string hour)
        {
            try
            {
                return Service.GetBooking(Room, id, date, hour);
            }
            catch (Exception)
            {
                Page.ModelState.AddModelError(String.Empty, "Fel inträffade vid hämtning av bokningsinformation.");
                return null;
            }
        }

        /// <summary>
        /// Bearbetar innehåll efter att ItemDataBound för listview är genomförd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BookingListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //Letar reda på label i formuläret
            var label = e.Item.FindControl("CustomerLabel") as Label;

            //Om label återfanns - Läs in data och sätt text på labeln som motsvarar kundens namn
            if (label != null)
            {
                var booking = (Booking)e.Item.DataItem;

                if (booking != null)
                {
                    var customer = Service.GetCustomer(booking.CustomerId);
                    label.Text = String.Format(label.Text, customer.Name);
                }
            }
        }
    }
}