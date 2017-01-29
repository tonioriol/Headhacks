using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pelicules : System.Web.UI.Page
{
    private string ordenacio;
    private string ascdesc;
    private HyperLink lnkTitol;
    private Label lblDirector;
    private Image imgPortada;
    private List<Dictionary<string, string>> llistatPelis;
    private string genere;

    protected void Page_Load(object sender, EventArgs e)
    {
        // Recollim els parametres pel mètode GET
        ordenacio = Request.QueryString["ordenacio"];
        ascdesc = Request.QueryString["ascdesc"];
        genere = Request.QueryString["genere"];

        // Si ordenacio esta vuit o es null li assignem per defecte ordenacio per titol de pelicula
        if (String.IsNullOrEmpty(ordenacio))
        {
            ordenacio = "titol";
        }

        // De la mateixa manera si ascdesc es nul o vuit li asignem asc (ascendent) per defecte
        if (String.IsNullOrEmpty(ascdesc))
        {
            ascdesc = "asc";
        }

        if (String.IsNullOrEmpty(genere))
        {
            phLlistaPelis.Controls.Add(new LiteralControl("<div id=\"llista_pelicules\"><h2>Llistat de Pel·lícules</h2>"));
            llistatPelis = clsPelicules.ObtenirPelis(ordenacio, ascdesc);
            MostrarPelis();
        }
        else
        {
            phLlistaPelis.Controls.Add(new LiteralControl("<div id=\"llista_pelicules\"><h2>Llistat de Pel·lícules del gènere " + genere + "</h2>"));
            llistatPelis = clsPelicules.ObtenirPeliculesGenere(ordenacio, ascdesc, genere);
            MostrarPelis();
        }
    }

    public void MostrarPelis()
    {
        if (llistatPelis != null && llistatPelis.Count > 0)
        {
            phLlistaPelis.Controls.Add(new LiteralControl("<ul>"));
            for (int i = 0; i < llistatPelis.Count; i++)
            {
                lnkTitol = new HyperLink();
                lblDirector = new Label();
                imgPortada = new Image();

                lnkTitol.Text = llistatPelis.ElementAt(i)["titol"];
                lnkTitol.NavigateUrl = "~/Pelicula.aspx?titol=" + llistatPelis.ElementAt(i)["titol"];
                lblDirector.Text = llistatPelis.ElementAt(i)["director"];
                imgPortada.ImageUrl = llistatPelis.ElementAt(i)["portada"];

                imgPortada.Width = 100;

                phLlistaPelis.Controls.Add(new LiteralControl("<li>"));
                phLlistaPelis.Controls.Add(imgPortada);
                phLlistaPelis.Controls.Add(new LiteralControl("<h3>"));
                phLlistaPelis.Controls.Add(lnkTitol);
                phLlistaPelis.Controls.Add(new LiteralControl("</h3>"));
                phLlistaPelis.Controls.Add(lblDirector);
                phLlistaPelis.Controls.Add(new LiteralControl("</li><div class=\"clear\"></div>"));
            }
            phLlistaPelis.Controls.Add(new LiteralControl("</ul>"));
        }
        else
        {
            phLlistaPelis.Controls.Add(new LiteralControl("<p>No hi ha pel·licules</p>"));
        }
        phLlistaPelis.Controls.Add(new LiteralControl("</div><!-- fi llista_pelicules -->"));
    }
}