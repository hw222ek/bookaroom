using BookARoom.Model;
using BookARoom.Model.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookARoom.Pages
{
    public partial class Default : System.Web.UI.Page
    {
        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        //Lagrar rum från route som int i egenskap.
        protected int Room
        {
            get 
            {
                if (RouteData.Values["room"] == null)
                {
                    return 1;
                }
                return int.Parse(RouteData.Values["room"].ToString());
            }
        }

        //Lagrar date från route som sträng i egenskap
        protected string Date
        {
            get 
            {
                if (RouteData.Values["date"] == null)
                {
                    return DateTime.UtcNow.AddHours(2).ToString("yyyy-MM-dd");
                }
                return (string)RouteData.Values["date"]; 
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Visa rumsnamn
            if (Room == 1)
            {
                RoomLabel.Text = "MÖLLE";
            }
            else if (Room == 2)
            {
                RoomLabel.Text = "VEN";
            }
            else if (Room == 3)
            {
                RoomLabel.Text = "KÄRNAN";
            }
            else if (Room == 11)
            {
                RoomLabel.Text = "PROJEKTOR";
            }
            else
            {
                RoomLabel.Text = "Rum ?";
            }

            //Visa datum
            var testDate = Convert.ToDateTime(Date);
            if (testDate.Date == DateTime.UtcNow.AddHours(2).Date)
            {
                DateLabel.Text = "Idag " + Date;
            }
            else
            {
                DateLabel.Text = Date;
            }

            //Visar meddelande om det finns något
            MessageLiteral.Text = Page.GetTempData("Message") as string;
            MessagePanel.Visible = !String.IsNullOrWhiteSpace(MessageLiteral.Text);
        }

        /// <summary>
        /// Hämtar alla bokningar valt datum
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Booking> BookingsListView_GetData()
        {
            try
            {
                string DateModification = Date.Replace("-", "");

                //"Hårdkodat att visa resultat mellan 8-18 dagens datum efter kravspec."
                return Service.GetBookingsByDay(Room, DateModification, " 08:00:00", " 17:00:00");
            }
            catch (Exception)
            {
                Page.ModelState.AddModelError(String.Empty, "Fel inträffade vid hämtning av bokningsinformation.");
                return null;
            }
        }

        /// <summary>
        /// Stänger meddelande vid knapptryck
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MessageButton_Click(object sender, EventArgs e)
        {
            MessagePanel.Visible = false;
        }

        /// <summary>
        /// Ändrar/skapar innehåll efter itemdatabound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BookingsListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            var label = e.Item.FindControl("CustomerLabel") as Label;
            
            if (label != null)
            {
                //Letar reda på label i formuläret
                var booking = (Booking)e.Item.DataItem;
                
                //Om label återfanns - Läs in data och sätt text på labeln som motsvarar kundens namn
                if (booking.CustomerId == 0)
                {
                    label.Text = "Ledigt";
                } 
                else 
                {
                    var customer = Service.GetCustomer(booking.CustomerId);
                    label.Text = String.Format(label.Text, customer.Name);
                }
            }

            //Letar reda på länk i formuläret
            var link = e.Item.FindControl("DetailsLink") as HyperLink;
            
            //Beroende på om länkens klass indikerar på bokning eller ledigt sätts olika textattribut och länkar (info/boka)
            if (link != null)
            {
                var booking = (Booking)e.Item.DataItem;

                if (booking.CustomerId == 0)
                {
                    link.Text = "Boka nu";
                    link.CssClass = "action-button shadow animate green";
                    link.NavigateUrl = GetRouteUrl("BookingInsert", new { room = booking.RoomId, date = booking.StartTime.ToString("yyyy-MM-dd"), hour = booking.StartTime.ToString("HH") });
                }
                else
                {
                    link.Text = "Detaljer";
                    link.CssClass = "action-button shadow animate blue";
                    link.NavigateUrl = GetRouteUrl("BookingDetails", new { room = booking.RoomId, id = booking.CustomerId, date = booking.StartTime.ToString("yyyy-MM-dd"), hour = booking.StartTime.ToString("HH") });
                }
            }
        }

        protected void DateButton_Click(object sender, EventArgs e)
        {
            if (DatePicker.SelectedDate.Year.ToString() == "1")
            {
                Response.RedirectToRoute("Bookings", new { room = Room.ToString(), date = Date });
            }
            else
            {
                var tempDate = DatePicker.SelectedDate.ToString("yyyy-MM-dd");
                Response.RedirectToRoute("Bookings", new { room = Room.ToString(), date = tempDate });
            }
        }

        protected void RoomsDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            Response.RedirectToRoute("Bookings", new { room = RoomsDropDownList.SelectedValue, date = Date });
        }
    }
}