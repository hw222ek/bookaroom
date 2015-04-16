using BookARoom.Model;
using BookARoom.Model.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookARoom.Pages.BookingPages
{
    public partial class BookingInsert : System.Web.UI.Page
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

        //Lagrar date från route som sträng i egenskap.
        protected string Date
        {
            get { return (string)RouteData.Values["date"]; }
        }

        //Lagrar hour från route som sträng i egenskap.
        protected string Hour
        {
            get { return (string)RouteData.Values["hour"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Om listan kan hittas och ej är postback populeras listan med kunddata.
            if (!IsPostBack && CustomerDropDownList != null)
            {
                try
                {
                    CustomerDropDownList.DataSource = Service.GetAllCustomers();
                    CustomerDropDownList.DataValueField = "CustomerId";
                    CustomerDropDownList.DataTextField = "Name";
                    CustomerDropDownList.DataBind();
                }
                catch (Exception)
                {
                    Page.ModelState.AddModelError(String.Empty, "Fel inträffade vid inläsning av kundinformation.");
                } 
            }

            //Om label kan hittas sätts textinnehållet innehållande värden från route data.
            if (TimeLabel != null)
	        {
		        var start = Convert.ToDateTime(String.Format("{0} {1}:00:00", Date, Hour));
                var end = start.AddMinutes(59);

                TimeLabel.Text = String.Format("Datum: <strong>{0}</strong> Starttid: <strong>{1}:00</strong>", Date, Hour); 
	        }
        }

        /// <summary>
        /// Lägger till bokning till databasen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BokaButton_Click(object sender, EventArgs e)
        {
            var start = Convert.ToDateTime(String.Format("{0} {1}:00:00", Date, Hour));
            var end = start.AddMinutes(59);

            //Skapar ny boknings-objekt med hårdkodat rumsnummer, kundnummer från lista och starttid från route data (konvertering ovan)
            var booking = new Booking
            {
                RoomId = Room,
                CustomerId = Int32.Parse(CustomerDropDownList.SelectedValue),
                StartTime = start,
                EndTime = end,
                Person = PersonTextBox.Text.ToString()
            };

            if (Page.ModelState.IsValid)
            {
                try
                {
                    //Spara bokningsdata till databasen
                    Service.SaveBooking(booking);

                    //Sätt meddelande om lyckad adderande av kund
                    Page.SetTempData("Message", String.Format("<strong>Lyckat!</strong> Bokning <strong>{0}</strong> klockan  <strong>{1}</strong> tillagd.", booking.StartTime.ToString("yyMMdd"), booking.StartTime.ToString("HH:mm")));

                    //PRG
                    Response.RedirectToRoute("Bookings", new { room = Room.ToString(), date = Date });
                    Context.ApplicationInstance.CompleteRequest();
                }
                catch (Exception ex)
                {
                    //Kontrollera om en annan bokning är gjord under tiden - gå tillbaka och visa meddelande om upptaget
                    var bookings = Service.GetBookingsByDay(booking.RoomId, booking.StartTime.ToString("yyMMdd"), booking.StartTime.ToString(" HH:mm:ss"), booking.StartTime.ToString(" HH:mm:ss"));

                    foreach (var boking in bookings)
                    {
                        if (boking.CustomerId != 0)
                        {
                            //Sätt meddelande
                            Page.SetTempData("Message", "<strong>TOO SLOW!</strong> Tiden är redan bokad.");

                            //PRG
                            Response.RedirectToRoute("Bookings", new { room = Room.ToString(), date = Date });
                            Context.ApplicationInstance.CompleteRequest();
                        }
                    }

                    var validationResults = ex.Data["ValidationResults"] as IEnumerable<ValidationResult>;
                    if (validationResults != null)
                    {
                        foreach (var validationResult in validationResults)
                        {
                            foreach (var memberName in validationResult.MemberNames)
	                        {
                                Page.ModelState.AddModelError(memberName, validationResult.ErrorMessage);
	                        } 
                        }
                    }
                    Page.ModelState.AddModelError(String.Empty, "Fel inträffade! Tyvärr kunde bokning ej genomföras.");
                }
            }
        }

        /// <summary>
        /// Tillbaka till bokningar vid knapptryck
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TillbakaLinkButton_Click(object sender, EventArgs e)
        {
            Response.RedirectToRoute("Bookings", new { room = Room.ToString(), date = Date });
            Context.ApplicationInstance.CompleteRequest();
        }       
    }
}