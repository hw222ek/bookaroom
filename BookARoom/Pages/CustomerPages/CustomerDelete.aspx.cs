using BookARoom.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookARoom.Pages.CustomerPages
{
    public partial class CustomerDelete : System.Web.UI.Page
    {
        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        //Lagrar id från route som int i egenskap
        protected int Id
        {
            get { return int.Parse(RouteData.Values["id"].ToString()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Sätter åter länk vid avbryt av radering tillbaka till detaljer
            CancelHyperLink.NavigateUrl = GetRouteUrl("CustomerDetails", new { id = Id });

            //Läser in kundinformation (för att presenteras i "är du säker" meddelande)
            if (!IsPostBack)
            {
                try
                {
                    var customer = Service.GetCustomer(Id);
                    if (customer != null)
                    {
                        CustomerName.Text = customer.Name;
                        return;
                    }
                    else
                    {
                        ModelState.AddModelError(String.Empty, String.Format("Fel! Kundnummer {0} hittades inte.", Id));
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError(String.Empty, "Fel inträffade vid inläsning av kundinformation.");
                } 
            }
        }

        /// <summary>
        /// Raderar kund ur databasen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DeleteLinkButton_Command(object sender, CommandEventArgs e)
        {
            try
            {
                var id = int.Parse(e.CommandArgument.ToString());
                Service.DeleteCustomer(id);

                //Sätt meddelande om lyckad borttagning av kunddata
                Page.SetTempData("Message", String.Format("<strong>Lyckat!</strong> Kund med kundid <strong>{0}</strong> borttagen.", Id));

                //PRG
                Response.RedirectToRoute("CustomerListing", null);
                Context.ApplicationInstance.CompleteRequest();
            }
            catch (Exception)
            {
                ModelState.AddModelError(String.Empty, "Fel inträffade vid radering av kund.");
            }
        }
    }
}