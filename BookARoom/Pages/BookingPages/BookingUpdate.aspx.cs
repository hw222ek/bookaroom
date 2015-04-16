//using BookARoom.Model;
//using BookARoom.Model.BLL;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//namespace BookARoom.Pages.BookingPages
//{
//    public partial class BookingUpdate : System.Web.UI.Page
//    {
//        private Service _service;

//        private Service Service
//        {
//            get { return _service ?? (_service = new Service()); }
//        }

//        //Lagrar id från route som int i egenskap
//        protected int Id
//        {
//            get { return int.Parse(RouteData.Values["id"].ToString()); }
//        }
        
//        //Lagrar date från route som sträng i egenskap.
//        protected string Date
//        {
//            get { return (string)RouteData.Values["date"]; }
//        }

//        //Lagrar hour från route som sträng i egenskap.
//        protected string Hour
//        {
//            get { return (string)RouteData.Values["hour"]; }
//        }

//        protected void Page_Load(object sender, EventArgs e)
//        {
//            //Om listan kan hittas och ej är postback populeras listan med kunddata.
//            if (!IsPostBack && TimeDropDownList != null)
//            {
//                try
//                {
//                    var start = Convert.ToDateTime(String.Format("{0} {1}:00:00", Date, Hour));
//                    var dateString = start.ToString("yyMMdd");

//                    TimeDropDownList.DataSource = Service.GetAvailableTimes(dateString, "08:00:00", "17:00:00");
//                    TimeDropDownList.DataValueField = "StartTime";
//                    TimeDropDownList.DataTextField = String.Format("{0}", "StartTime");
//                    TimeDropDownList.DataBind();
//                }
//                catch (Exception)
//                {
//                    Page.ModelState.AddModelError(String.Empty, "Fel inträffade vid adderande av tillgängliga bokningstider.");
//                }
//            }
//        }

//        protected void UppdateraLinkButton_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                //Läser in ny starttid från dropdownlist och lagrar befintlig tid i end
//                DateTime newStartTime = Convert.ToDateTime(TimeDropDownList.SelectedValue);
//                //Lagrar tidigare använd starttid
//                var oldStartTime = Convert.ToDateTime(String.Format("{0} {1}:00:00", Date, Hour));

//                //Kontrollerar om en bokning finns för att senare kunna uppdateras
//                var booking = Service.GetBooking(1, Id, Date, Hour);

//                //Fel om bokningsobjekt lästs in eller inget har lästas in
//                if (booking.StartTime != oldStartTime || booking == null)
//                {
//                    ModelState.AddModelError(String.Empty, "Fel vid inläsning av booking objekt.");
//                    return;
//                }

//                //Lagrar nya bokningstider i booking objektet för validering och uppdatering
//                booking.StartTime = newStartTime;
//                booking.EndTime = oldStartTime;

//                if (Page.ModelState.IsValid)
//                {
//                    //Spara uppdateringar till databasen
//                    Service.UpdateBooking(booking);

//                    //Sätt meddelande om lyckad adderande av kund
//                    Page.SetTempData("Message", "<strong>Lyckat!</strong> Bokningstiden uppdaterad.");

//                    //PRG
//                    Response.RedirectToRoute("Bookings", null);
//                    Context.ApplicationInstance.CompleteRequest();
//                }
//            }
//            catch (Exception ex)
//            {
//                var validationResults = ex.Data["ValidationResults"] as IEnumerable<ValidationResult>;
//                if (validationResults != null)
//                {
//                    foreach (var validationResult in validationResults)
//                    {
//                        foreach (var memberName in validationResult.MemberNames)
//                        {
//                            Page.ModelState.AddModelError(memberName, validationResult.ErrorMessage);
//                        }
//                    }
//                }
//                Page.ModelState.AddModelError(String.Empty, "Fel inträffade vid uppdatering av bokningsinformation.");
//            }
//        }

//        /// <summary>
//        /// Tillbaka till bokningsdetaljer vid klick
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        protected void TillbakaLinkButton_Click(object sender, EventArgs e)
//        {
//            Response.RedirectToRoute("BookingDetails", new { id = Id, date = Date, hour = Hour });
//        }
//    }
//}