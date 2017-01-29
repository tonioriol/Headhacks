using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnCerca_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Busqueda.aspx?paramCerca=" + txtCerca.Text);
    }
}
