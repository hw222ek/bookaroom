using BookARoom.Model;
using BookARoom.Model.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookARoom.Pages.CustomerPages
{
    public partial class CustomerDetails : System.Web.UI.Page
    {
        private Service _service;

        private Service Service
        {
            get { return _service ?? (_service = new Service()); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Hämtar kunddata med id (från route)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer CustomerListView_GetItem([RouteData] int id)
        {
            try
            {
                return Service.GetCustomer(id);
            }
            catch (Exception)
            {
                Page.ModelState.AddModelError(String.Empty, "Fel inträffade vid hämtning av kundinformation.");
                return null;
            }
        }

        /// <summary>
        /// Bearbetar innehåll efter att ItemDataBound för listview är genomförd
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
    }
}