using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Busqueda : System.Web.UI.Page
{
    #region Atributs
    private string paramCerca;
    private string ordenacio;
    private string ascdesc;
    private HyperLink lnkTitol;
    private Label lblDirector;
    private Image imgPortada;
    private clsBusqueda cerca;
    private List<Dictionary<string, string>> llistaResultatsCerca;
    private List<Dictionary<string, string>> llistaResultatsTitol;
    private List<Dictionary<string, string>> llistaResultatsDirector;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        // Recollim els parametres pel mètode GET
        ordenacio = Request.QueryString["ordenacio"];
        ascdesc = Request.QueryString["ascdesc"];
        paramCerca = Request.QueryString["paramCerca"];

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
        if (!String.IsNullOrEmpty(paramCerca))
        {
            // mostrem els resultats
            MostrarResultatCerca();
        }
    }

    public void MostrarResultatCerca()
    {
        if (paramCerca.Length > 3)
        {
            cerca = new clsBusqueda();
            //llistaResultatsCerca = new List<Dictionary<string, string>>();

            // Busquem per titol  i per director
            llistaResultatsCerca = cerca.Cercar(ordenacio, ascdesc, paramCerca);
            //llistaResultatsCerca.AddRange(cerca.PerDirector(ordenacio, ascdesc, paramCerca));

            phBusqueda.Controls.Add(new LiteralControl("<div id=\"llista_pelicules\"><h2>Resultats de la búsqueda per a " + paramCerca + "</h2>"));
            if (llistaResultatsCerca != null && llistaResultatsCerca.Count > 0)
            {
                phBusqueda.Controls.Add(new LiteralControl("<ul>"));
                for (int i = 0; i < llistaResultatsCerca.Count; i++)
                {
                    lnkTitol = new HyperLink();
                    lblDirector = new Label();
                    imgPortada = new Image();

                    lnkTitol.Text = llistaResultatsCerca.ElementAt(i)["titol"];
                    lnkTitol.NavigateUrl = "~/Pelicula.aspx?titol=" + llistaResultatsCerca.ElementAt(i)["titol"];
                    lblDirector.Text = llistaResultatsCerca.ElementAt(i)["director"];
                    imgPortada.ImageUrl = llistaResultatsCerca.ElementAt(i)["portada"];

                    imgPortada.Width = 100;

                    phBusqueda.Controls.Add(new LiteralControl("<li>"));
                    phBusqueda.Controls.Add(imgPortada);
                    phBusqueda.Controls.Add(new LiteralControl("<h3>"));
                    phBusqueda.Controls.Add(lnkTitol);
                    phBusqueda.Controls.Add(new LiteralControl("</h3>"));
                    phBusqueda.Controls.Add(lblDirector);
                    phBusqueda.Controls.Add(new LiteralControl("</li><div class=\"clear\"></div>"));
                }
                phBusqueda.Controls.Add(new LiteralControl("</ul>"));
            }
            else
            {
                phBusqueda.Controls.Add(new LiteralControl("<p>No hi ha resultats per a la cerca</p>"));
            }
            phBusqueda.Controls.Add(new LiteralControl("</div><!-- fi resultats_busqueda -->"));
        }
        else
        {
            phBusqueda.Controls.Add(new LiteralControl("<div id=\"resultats_busqueda\"><h2>El parametre de cerca ha de tenir almenys 3 caracters</h2></div><!-- fi resultats_busqueda -->"));

        }
    }
}