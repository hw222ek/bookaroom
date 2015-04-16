using BookARoom.Model;
using BookARoom.Model.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookARoom.Pages.CustomerPages
{
    public partial class CustomerListing : System.Web.UI.Page
    {
        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Visar meddelande om det finns något
            MessageLiteral.Text = Page.GetTempData("Message") as string;
            MessagePanel.Visible = !String.IsNullOrWhiteSpace(MessageLiteral.Text);
        }

        /// <summary>
        /// Hämtar kunddata på samtliga kunder från databasen
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> CustomerListView_GetData()
        {
            try
            {
                return Service.GetAllCustomers();
            }
            catch (Exception)
            {
                Page.ModelState.AddModelError(String.Empty, "Fel inträffade vid hämtning av kundinformation.");
                return null;
            }      
        }

        /// <summary>
        /// Läser in kundtyp från databas för att presenteras i label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CustomerListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            //Letar reda på label i formuläret
            var label = e.Item.FindControl("CustomerTypeLabel") as Label;

            //Om label återfanns - Läs in data och sätt text på labeln som motsvarar kundens kundtyp
            if (label != null)
            {
                var customer = (Customer)e.Item.DataItem;

                var customerType = Service.GetAllCustomerTypes()
                    .Single(ct => ct.CustomerTypeId == customer.CustomerTypeId);

                label.Text = String.Format(label.Text, customerType.CustomerType);
            }
        }

        /// <summary>
        /// Lägger till kund till databasen
        /// </summary>
        /// <param name="customer"></param>
        public void CustomerListView_InsertItem(Customer customer)
        {
            if (Page.ModelState.IsValid)
            {
                try
                {
                    //Spara kunddata till databasen
                    Service.SaveCustomer(customer);

                    //Sätt meddelande om lyckad adderande av kund
                    Page.SetTempData("Message", String.Format("<strong>Lyckat! {0}</strong> tillagd.", customer.Name));

                    //PRG
                    Response.RedirectToRoute("CustomerListing", null);
                    Context.ApplicationInstance.CompleteRequest();
                }
                catch (Exception ex)
                {
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
                    Page.ModelState.AddModelError(String.Empty, "Fel inträffade vid adderande av kundinformation.");
                }
            }
        }

        /// <summary>
        /// Uppdaterar en kunds information i databsen
        /// </summary>
        /// <param name="customerId"></param>
        public void CustomerListView_UpdateItem(int customerId)
        {
            try
            {
                var customer = Service.GetCustomer(customerId);

                //Fel om kunden ej kan lokaliseras
                if (customer.CustomerId != customerId)
                {
                    ModelState.AddModelError(String.Empty, String.Format("Fel! Kund med id {0} hittades ej.", customerId));
                    return;
                }

                if (TryUpdateModel(customer))
                {
                    //Spara uppdateringar till databasen
                    Service.SaveCustomer(customer);

                    //Sätt meddelande om lyckad adderande av kund
                    Page.SetTempData("Message", "<strong>Lyckat!</strong> Kundinformationen uppdaterad.");

                    //PRG
                    Response.RedirectToRoute("CustomerListing", null);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception ex)
            {
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
                Page.ModelState.AddModelError(String.Empty, "Fel inträffade vid uppdatering av kundinformation.");
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

        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
            ViewState.Clear();
            FormsAuthentication.SignOut();
            Response.Redirect("/");
        }
    }
}