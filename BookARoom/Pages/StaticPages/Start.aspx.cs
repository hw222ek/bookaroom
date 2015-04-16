using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookARoom.Pages.StaticPages
{
    public partial class Start : System.Web.UI.Page
    {
        //Lagrar date från idag i egenskap.
        protected string Date
        {
            get { return DateTime.UtcNow.AddHours(2).ToString("yyyy-MM-dd"); }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.RedirectToRoute("Bookings", new { room = 1, date = Date });
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            Response.RedirectToRoute("Bookings", new { room = 2, date = Date });
        }

        protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
        {
            Response.RedirectToRoute("Bookings", new { room = 3, date = Date });
        }

        protected void ImageButton11_Click(object sender, ImageClickEventArgs e)
        {
            Response.RedirectToRoute("Bookings", new { room = 11, date = Date });
        }
    }
}