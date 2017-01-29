using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Generes : System.Web.UI.Page
{
    #region Atributs
    private string ordenacio;
    private string ascdesc;
    private HyperLink lnkGenere;
    private List<Dictionary<string, string>> llistatGeneres;
    private Label lblNumPelicules;
    #endregion

    #region Mètode pseudo-constructor
    protected void Page_Load(object sender, EventArgs e)
    {
        // Recollim els parametres pel mètode GET
        ordenacio = Request.QueryString["ordenacio"];
        ascdesc = Request.QueryString["ascdesc"];

        // Si ordenacio esta vuit o es null li assignem per defecte ordenacio per genere
        if (String.IsNullOrEmpty(ordenacio))
        {
            ordenacio = "genere";
        }

        // De la mateixa manera si ascdesc es nul o vuit li asignem asc (ascendent) per defecte
        if (String.IsNullOrEmpty(ascdesc))
        {
            ascdesc = "asc";
        }
        MostrarGeneres();
    }
    #endregion

    #region Mètodes
    public void MostrarGeneres()
    {
        llistatGeneres = clsGeneres.ObtenirGeneres(ordenacio, ascdesc);
        phLlistaGeneres.Controls.Add(new LiteralControl("<div id=\"llista_generes\"><h2>Llistat de gèneres</h2>"));
        if (llistatGeneres != null && llistatGeneres.Count > 0)
        {
            phLlistaGeneres.Controls.Add(new LiteralControl("<ul>"));
            for (int i = 0; i < llistatGeneres.Count; i++)
            {
                lnkGenere = new HyperLink();
                lblNumPelicules = new Label();

                lnkGenere.NavigateUrl = "~/Pelicules.aspx?genere=" + llistatGeneres.ElementAt(i)["genere"];
                lnkGenere.Text = llistatGeneres.ElementAt(i)["genere"];
                lblNumPelicules.Text = llistatGeneres.ElementAt(i)["num_pelicules"];

                phLlistaGeneres.Controls.Add(new LiteralControl("<li><h3>"));
                phLlistaGeneres.Controls.Add(lnkGenere);
                phLlistaGeneres.Controls.Add(new LiteralControl("</h3><p>Hi ha "));
                phLlistaGeneres.Controls.Add(lblNumPelicules);
                phLlistaGeneres.Controls.Add(new LiteralControl(" pel·licula/es</p>"));
            }
            phLlistaGeneres.Controls.Add(new LiteralControl("</ul>"));
        }
        else
        {
            phLlistaGeneres.Controls.Add(new LiteralControl("<p>No hi ha gèneres</p>"));
        }
        phLlistaGeneres.Controls.Add(new LiteralControl("</div><!-- fi llista_generes -->"));
    }
    #endregion
}