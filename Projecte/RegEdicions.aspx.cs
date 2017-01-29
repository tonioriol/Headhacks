using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
// Afegits
using System.Web.Security;

public partial class RegEdicions : System.Web.UI.Page
{
    #region Atributs
    private HyperLink lnkVersio;
    private HyperLink lnkUsuari;
    private List<Dictionary<string, string>> llistaEdicions;
    private string titol;
    private Image imgPortada;
    private Label lblTitol;
    private Label lblDirector;
    private Label lblAny;
    private Label lblDuracio;
    private Label lblPais;
    private Label lblGuio;
    private Label lblMusica;
    private Label lblGenere;
    private Label lblInterprets;
    private Label lblTrama;
    private HyperLink lnkEnllaçEnLinia;
    private HyperLink lnkEnllaçDescarrega;
    private clsPelicula pelicula;
    private clsUsuari usuariVersio;
    private clsUsuari usuariAutenticat;
    private string id_edicio;
    private Dictionary<string, string> dictVersio;
    private Label lblVersio;
    private Button btnRevertir;
    private HyperLink lnkPelicula;
    #endregion

    #region Mètode pseudo-constructor
    protected void Page_Load(object sender, EventArgs e)
    {
        // Recollim els parametres pel mètode GET
        titol = Request.QueryString["titol"];
        id_edicio = Request.QueryString["id_edicio"];

        if (User.Identity.IsAuthenticated && Roles.IsUserInRole("Editor") || Roles.IsUserInRole("Creador") || Roles.IsUserInRole("Borrador"))
        {
            pelicula = new clsPelicula(User.Identity.Name, titol);
            usuariAutenticat = new clsUsuari(User.Identity.Name);

            if (!String.IsNullOrEmpty(titol) && String.IsNullOrEmpty(id_edicio))
            {
                MostrarEdicions();
            }
            else if (!String.IsNullOrEmpty(id_edicio))
            {
                MostrarVersio();
            }
            else
            {
                Response.Redirect("~/");
            }
        }
        else
        {
            Response.Redirect("~/");
        }
    }
    #endregion

    #region Mètodes
    public void MostrarEdicions()
    {
        llistaEdicions = pelicula.ObtenirEdicions();

        if (pelicula.Error)
        {
            phLlistaEdicions.Controls.Add(new LiteralControl("<div id=\"error_llista_edicions\"><p>Error al consultar les edicions de " + titol + "</h2><p>" + pelicula.MsgError + "</p></div><!-- fi error_llista_edicions -->"));
        }
        else
        {
            phLlistaEdicions.Controls.Add(new LiteralControl("<div id=\"llista_generes\"><h2>Llistat d'Edicions de la pel·licula " + titol + "</h2><ul>"));

            for (int i = 0; i < llistaEdicions.Count; i++)
            {
                lnkVersio = new HyperLink();
                lnkUsuari = new HyperLink();
                usuariVersio = new clsUsuari(llistaEdicions.ElementAt(i)["id_usuari"], "");

                lnkVersio.Text = llistaEdicions.ElementAt(i)["data_hora"];
                lnkVersio.NavigateUrl = "~/RegEdicions.aspx?titol=" + llistaEdicions.ElementAt(i)["titol"] + "&id_edicio=" + llistaEdicions.ElementAt(i)["id"];

                lnkUsuari.NavigateUrl = "~/Usuari.aspx?nom=" + usuariVersio.NomUsuari;
                lnkUsuari.Text = usuariVersio.Nom + " " + usuariVersio.Cognoms;

                phLlistaEdicions.Controls.Add(new LiteralControl("<li>"));
                phLlistaEdicions.Controls.Add(new LiteralControl("<p>Versió " + (i + 1) + " amb data del "));
                phLlistaEdicions.Controls.Add(lnkVersio);
                phLlistaEdicions.Controls.Add(new LiteralControl("</p><p>Editada per "));
                phLlistaEdicions.Controls.Add(lnkUsuari);
                phLlistaEdicions.Controls.Add(new LiteralControl("</p></li>"));
            }
            phLlistaEdicions.Controls.Add(new LiteralControl("</ul></div><!-- fi llista_edicions -->"));
        }
    }

    private void MostrarVersio()
    {
        try
        {
            dictVersio = pelicula.ObtenirEdicio(id_edicio);
            usuariVersio = new clsUsuari(dictVersio["id_usuari"], "");

            //Inicialitzem els controls
            lnkUsuari = new HyperLink();
            lblVersio = new Label();

            lnkPelicula = new HyperLink();

            imgPortada = new Image();
            lblTitol = new Label();
            lblDirector = new Label();
            lblAny = new Label();
            lblDuracio = new Label();
            lblPais = new Label();
            lblGuio = new Label();
            lblMusica = new Label();
            lblGenere = new Label();
            lblInterprets = new Label();
            lblTrama = new Label();
            lnkEnllaçEnLinia = new HyperLink();
            lnkEnllaçDescarrega = new HyperLink();

            // els omplim
            lnkUsuari.Text = usuariVersio.Nom + " " + usuariVersio.Cognoms;
            lnkUsuari.NavigateUrl = "~/Usuari.aspx?" + usuariVersio.NomUsuari;
            lblVersio.Text = dictVersio["data_hora"];
            lnkPelicula.Text = dictVersio["titol"];
            lnkPelicula.NavigateUrl = "~/Pelicula.aspx?titol=" + dictVersio["titol"];

            imgPortada.ImageUrl = dictVersio["portada"];
            lblTitol.Text = dictVersio["titol"];
            lblDirector.Text = dictVersio["director"];
            lblAny.Text = dictVersio["any_estrena"];
            lblDuracio.Text = dictVersio["duracio"];
            lblPais.Text = dictVersio["pais"];
            lblGuio.Text = dictVersio["guio"];
            lblMusica.Text = dictVersio["musica"];
            lblGenere.Text = dictVersio["genere"];
            lblInterprets.Text = dictVersio["interprets"];
            lblTrama.Text = dictVersio["trama"];
            lnkEnllaçEnLinia.NavigateUrl = dictVersio["enllaç_en_linia"];
            lnkEnllaçEnLinia.Text = dictVersio["enllaç_en_linia"];
            lnkEnllaçDescarrega.NavigateUrl = dictVersio["enllaç_descarrega"];
            lnkEnllaçDescarrega.Text = dictVersio["enllaç_descarrega"];


            // els afegim al placeholder, juntament amb totes les etiquetes html necessaries

            phVersio.Controls.Add(new LiteralControl("<div id=\"pelicula\"><h2>Verisó del "));
            phVersio.Controls.Add(lblVersio);
            phVersio.Controls.Add(new LiteralControl(" de la pelicula "));
            phVersio.Controls.Add(lnkPelicula);
            phVersio.Controls.Add(new LiteralControl("</h2><p>Editada per: "));
            phVersio.Controls.Add(lnkUsuari);
            phVersio.Controls.Add(new LiteralControl("</p>"));
            phVersio.Controls.Add(imgPortada);
            phVersio.Controls.Add(new LiteralControl("<div id=\"dades_pelicula\"><ul><li><h3>Titol:</h3>"));
            phVersio.Controls.Add(lblTitol);
            phVersio.Controls.Add(new LiteralControl("</li><li><h3>Director:</h3>"));
            phVersio.Controls.Add(lblDirector);
            phVersio.Controls.Add(new LiteralControl("</li><li><h3>Any:</h3>"));
            phVersio.Controls.Add(lblAny);
            phVersio.Controls.Add(new LiteralControl("</li><li><h3>Duració:</h3>"));
            phVersio.Controls.Add(lblDuracio);
            phVersio.Controls.Add(new LiteralControl("</li><li><h3>País:</h3>"));
            phVersio.Controls.Add(lblPais);
            phVersio.Controls.Add(new LiteralControl("</li><li><h3>Guio:</h3>"));
            phVersio.Controls.Add(lblGuio);
            phVersio.Controls.Add(new LiteralControl("</li><li><h3>Música:</h3>"));
            phVersio.Controls.Add(lblMusica);
            phVersio.Controls.Add(new LiteralControl("</li><li><h3>Gènere:</h3>"));
            phVersio.Controls.Add(lblGenere);
            phVersio.Controls.Add(new LiteralControl("</li><li><h3>Interprets:</h3>"));
            phVersio.Controls.Add(lblInterprets);
            phVersio.Controls.Add(new LiteralControl("</li><li><h3>Trama:</h3>"));
            phVersio.Controls.Add(lblTrama);
            phVersio.Controls.Add(new LiteralControl("</li><li><h3>Veure en linia:</h3>"));
            phVersio.Controls.Add(lnkEnllaçEnLinia);
            phVersio.Controls.Add(new LiteralControl("</li><li><h3>Descarregar:</h3>"));
            phVersio.Controls.Add(lnkEnllaçDescarrega);
            phVersio.Controls.Add(new LiteralControl("</li></ul>"));

            btnRevertir = new Button();
            btnRevertir.Text = "Revertir aquestes dades";
            btnRevertir.Click += new EventHandler(this.btnRevertir_Click);

            phVersio.Controls.Add(btnRevertir);
            phVersio.Controls.Add(new LiteralControl("</div><!-- fi dades_pelicula --></div><!-- fi pelicula -->"));

        }
        catch (Exception ex)
        {
            phVersio.Controls.Add(new LiteralControl("<div id=\"error_mostrar_pelicula\"><p>Error al mostrar la versió. Missatge d'error:  " + ex.Message + "</p></div>"));
        }
    }
    #endregion

    #region Esdeveniments
    protected void btnRevertir_Click(object sender, EventArgs e)
    {
        try
        {
            // Actualitzem les altres propietats del objecte pelicula
            pelicula.Portada = dictVersio["portada"];
            pelicula.Titol = dictVersio["titol"];
            pelicula.Director = dictVersio["director"];
            pelicula.Any = dictVersio["any_estrena"];
            pelicula.Duracio = dictVersio["duracio"];
            pelicula.Pais = dictVersio["pais"];
            pelicula.Guio = dictVersio["guio"];
            pelicula.Musica = dictVersio["musica"];
            pelicula.Genere = dictVersio["genere"];
            pelicula.Interprets = dictVersio["interprets"];
            pelicula.Trama = dictVersio["trama"];
            pelicula.EnllaçEnLinia = dictVersio["enllaç_en_linia"];
            pelicula.EnllaçDescarrega = dictVersio["enllaç_descarrega"];

            // Actualitzem la base de dades a traves d'aquest mètode
            pelicula.ActualitzarDadesPelicula();

            // es prèmia al usuari per cada pelicula editada amb èxit (tambe per cada creada)
            usuariAutenticat.ActualitzarPrestigi();

            // I redireccionem a la pelicula (amb el nou nom, si es el cas) per a reflexar tots els canvis fets
            Response.Redirect("~/Pelicula.aspx?titol=" + pelicula.Titol);


        }
        catch (Exception)
        {
        }
    }

    #endregion
}