using System;
using System.Web.UI;
using System.Web.UI.WebControls;
// Afegits
using System.Web.Security;

public partial class AfegirPelicula : System.Web.UI.Page
{
    #region Atributs
    private FileUpload fupPortada;
    private TextBox txtTitol;
    private TextBox txtDirector;
    private TextBox txtAny;
    private TextBox txtDuracio;
    private TextBox txtPais;
    private TextBox txtGuio;
    private TextBox txtMusica;
    private TextBox txtGenere;
    private TextBox txtInterprets;
    private TextBox txtTrama;
    private TextBox txtEnllaçEnLinia;
    private TextBox txtEnllaçDescarrega;
    private DropDownList drdGenere;
    private RequiredFieldValidator rfvTitol;
    private RangeValidator rnvAny;
    private Button btnAfegirPelicula;
    private Button btnCancelarCrearPelicula;
    private clsPelicula pelicula;
    private RequiredFieldValidator rfvGenere;
    private clsUsuari usuariAutenticat;
    #endregion

    #region Mètode pseudo-constructor
    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated && Roles.IsUserInRole("Editor") || Roles.IsUserInRole("Creador") || Roles.IsUserInRole("Borrador"))
        {
            usuariAutenticat = new clsUsuari(User.Identity.Name);
            MostrarAfegirPelicula();
        }
        else
        {
            Response.Redirect("~/");
        }
    }
    #endregion

    #region Mètodes
    private void MostrarAfegirPelicula()
    {
        // Definim els TextBox i demes controls per a creació (quasi igual al mode edició, reutilitzem controls i codi)
        fupPortada = new FileUpload();
        txtTitol = new TextBox();
        txtDirector = new TextBox();
        txtAny = new TextBox();
        txtDuracio = new TextBox();
        txtPais = new TextBox();
        txtGuio = new TextBox();
        txtMusica = new TextBox();
        txtGenere = new TextBox();
        txtInterprets = new TextBox();
        txtTrama = new TextBox();
        txtEnllaçEnLinia = new TextBox();
        txtEnllaçDescarrega = new TextBox();
        drdGenere = new DropDownList();
        rfvTitol = new RequiredFieldValidator();
        rfvGenere = new RequiredFieldValidator();
        rnvAny = new RangeValidator();
        //vsAfegirPelicula = new ValidationSummary();

        // Apliquem una mica d'estils
        txtInterprets.TextMode = TextBoxMode.MultiLine;
        txtTrama.TextMode = TextBoxMode.MultiLine;

        txtTitol.CssClass = "camps_afegir_pelicula";
        txtDirector.CssClass = "camps_afegir_pelicula";
        txtAny.CssClass = "camps_afegir_pelicula";
        txtDuracio.CssClass = "camps_afegir_pelicula";
        txtPais.CssClass = "camps_afegir_pelicula";
        txtGuio.CssClass = "camps_afegir_pelicula";
        txtMusica.CssClass = "camps_afegir_pelicula";
        txtGenere.CssClass = "camps_afegir_pelicula";
        txtInterprets.CssClass = "camps_afegir_pelicula";
        txtTrama.CssClass = "camps_afegir_pelicula";
        txtEnllaçEnLinia.CssClass = "camps_afegir_pelicula";
        txtEnllaçDescarrega.CssClass = "camps_afegir_pelicula";

        // Validacio
        //vsAfegirPelicula.ValidationGroup = "vgAfegirPelicula";
        //vsAfegirPelicula.CssClass = "failureNotification";

        // Validar que el nom de la pelicula no estigui vuit 
        //rfvTitol.Text = "*";
        rfvTitol.ErrorMessage = "El titol es requerit";
        rfvTitol.ToolTip = "El titol es requerit";
        rfvTitol.CssClass = "failureNotification";
        rfvTitol.SetFocusOnError = true;
        rfvTitol.ValidationGroup = "vgAfegirPelicula";
        txtTitol.ID = "txtTitol";
        rfvTitol.ControlToValidate = txtTitol.ID;

        // Validar rang de valors del any
        //rnvAny.Text = "*";
        rnvAny.ErrorMessage = "Any erroni. Nomes es prermet des del 1895 fins l'any actual";
        rnvAny.ToolTip = "Any erroni. Nomes es prermet des del 1895 fins l'any actual";
        rnvAny.CssClass = "failureNotification";
        rnvAny.MinimumValue = "1895";
        rnvAny.MaximumValue = Convert.ToString(DateTime.Now.Year);
        rnvAny.Type = ValidationDataType.Integer;
        rnvAny.SetFocusOnError = true;
        rnvAny.ValidationGroup = "vgAfegirPelicula";
        txtAny.ID = "txtAny";
        rnvAny.ControlToValidate = txtAny.ID;

        // Validar que el gènere estigui seleccionat
        //rfvGenere.Text = "*";
        rfvGenere.ErrorMessage = "El gènere es requerit";
        rfvGenere.ToolTip = "El gènere es requerit";
        rfvGenere.CssClass = "failureNotification";
        rfvGenere.SetFocusOnError = true;
        rfvGenere.ValidationGroup = "vgAfegirPelicula";
        drdGenere.ID = "drdGenere";
        rfvGenere.ControlToValidate = drdGenere.ID;
        rfvGenere.InitialValue = "0";

        // El primer el declarem de manera diferent per specifiar explicitament que el valor sera null, els 
        // altres queden implicitament amb els valors 0, 1 i seguents conforme els inicilezem
        drdGenere.Items.Add(new ListItem("Selecciona", "0"));
        drdGenere.Items.Add(new ListItem("Ciencia Ficció", "1"));
        drdGenere.Items.Add(new ListItem("Drama", "2"));
        drdGenere.Items.Add(new ListItem("Terror", "3"));
        drdGenere.Items.Add(new ListItem("Thriller", "4"));
        drdGenere.Items.Add(new ListItem("Policiac", "5"));
        drdGenere.Items.Add(new ListItem("Eròtic", "6"));
        drdGenere.Items.Add(new ListItem("X", "7"));
        drdGenere.Items.Add(new ListItem("Acció", "8"));
        drdGenere.Items.Add(new ListItem("Comèdia", "9"));
        drdGenere.Items.Add(new ListItem("Humor", "10"));
        drdGenere.Items.Add(new ListItem("Cine Negre", "11"));
        drdGenere.Items.Add(new ListItem("Gore", "12"));
        drdGenere.Items.Add(new ListItem("Musical", "13"));
        drdGenere.Items.Add(new ListItem("Sèrie B", "14"));
        drdGenere.Items.Add(new ListItem("Documental", "15"));
        drdGenere.Items.Add(new ListItem("Bèlic", "16"));
        drdGenere.Items.Add(new ListItem("Western", "17"));

        // els afegim al placeholder, juntament amb totes les etiquetes html necessaries            
        phPelicula.Controls.Add(new LiteralControl("<div id=\"pelicula_mode_creacio\"><h2>Afegir nova pel·licula</h2><ul><li><h3>Portada:</h3>"));
        phPelicula.Controls.Add(fupPortada);
        phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Titol:</h3>"));
        phPelicula.Controls.Add(txtTitol);
        phPelicula.Controls.Add(new LiteralControl("<br />"));
        phPelicula.Controls.Add(rfvTitol);
        phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Director:</h3>"));
        phPelicula.Controls.Add(txtDirector);
        phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Any:</h3>"));
        phPelicula.Controls.Add(txtAny);
        phPelicula.Controls.Add(new LiteralControl("<br />"));
        phPelicula.Controls.Add(rnvAny);
        phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Duració:</h3>"));
        phPelicula.Controls.Add(txtDuracio);
        phPelicula.Controls.Add(new LiteralControl("</li><li><h3>País:</h3>"));
        phPelicula.Controls.Add(txtPais);
        phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Guio:</h3>"));
        phPelicula.Controls.Add(txtGuio);
        phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Música:</h3>"));
        phPelicula.Controls.Add(txtMusica);
        phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Gènere:</h3>"));
        phPelicula.Controls.Add(drdGenere);
        phPelicula.Controls.Add(new LiteralControl(" "));
        phPelicula.Controls.Add(rfvGenere);
        phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Interprets:</h3>"));
        phPelicula.Controls.Add(txtInterprets);
        phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Trama:</h3>"));
        phPelicula.Controls.Add(txtTrama);
        phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Veure en linia:</h3>"));
        phPelicula.Controls.Add(txtEnllaçEnLinia);
        phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Descarregar:</h3>"));
        phPelicula.Controls.Add(txtEnllaçDescarrega);
        //phPelicula.Controls.Add(new LiteralControl("</li><li>"));
        //phPelicula.Controls.Add(vsAfegirPelicula);
        phPelicula.Controls.Add(new LiteralControl("</li></ul>"));


        // Afegim el boto finalitzar i l'associem a l'esdeveniment corresponent
        btnAfegirPelicula = new Button();
        btnAfegirPelicula.Text = "Crear";
        btnAfegirPelicula.CssClass = "btn_afegir_pelicula";
        btnAfegirPelicula.ValidationGroup = "vgAfegirPelicula";
        btnAfegirPelicula.Click += new EventHandler(this.btnAfegirPelicula_Click);
        phPelicula.Controls.Add(btnAfegirPelicula);

        // Afegim el boto cancelar i l'associem a l'esdeveniment corresponent
        btnCancelarCrearPelicula = new Button();
        btnCancelarCrearPelicula.Text = "Cancelar";
        btnCancelarCrearPelicula.CssClass = "btn_afegir_pelicula";
        btnCancelarCrearPelicula.Click += new EventHandler(this.btnCancelarAfegirPelicula_Click);
        phPelicula.Controls.Add(btnCancelarCrearPelicula);

        phPelicula.Controls.Add(new LiteralControl("</div><!-- fi pelicula_mode_creacio -->"));
    }

    private string FormatarNomFitxer(string cadena)
    {
        cadena = cadena.ToLowerInvariant(); // Passem a minuscula

        cadena = cadena.Replace(" ", "_");
        cadena = cadena.Replace("\\", "-");//   \
        cadena = cadena.Replace("/", "-");//    /
        cadena = cadena.Replace(":", "-");//    :
        cadena = cadena.Replace("*", "-");//    *
        cadena = cadena.Replace("?", "-");//    ?
        cadena = cadena.Replace("\"", "-");//   "
        cadena = cadena.Replace("<", "-");//    <
        cadena = cadena.Replace(">", "-");//    >

        return cadena;
    }
    #endregion

    #region Esdeveniments
    protected void btnAfegirPelicula_Click(object sender, EventArgs e)
    {
        string rutaPortada = null;

        // Comprovem el tipus de dada que es, i si no es cap format d'imatge correcta
        if (fupPortada.PostedFile.ContentType == "image/gif")
        {
            // MIME correcte

            // Guardar el resultat cambient-li el nom pel de la pelicula substituint els espais per quions baixos (que ja hem fet) i passant-lo a minuscules
            fupPortada.PostedFile.SaveAs(Server.MapPath("~/Imatges/Portades") + "/" + FormatarNomFitxer(txtTitol.Text) + ".gif");
            rutaPortada = "~/Imatges/Portades/" + FormatarNomFitxer(txtTitol.Text) + ".gif";
        }
        else if (fupPortada.PostedFile.ContentType == "image/jpeg" || fupPortada.PostedFile.ContentType == "image/pjpeg")
        {
            // MIME correcte

            // Guardar el resultat (idem pero en gif)
            fupPortada.PostedFile.SaveAs(Server.MapPath("~/Imatges/Portades") + "/" + FormatarNomFitxer(txtTitol.Text) + ".jpg");
            rutaPortada = "~/Imatges/Portades/" + FormatarNomFitxer(txtTitol.Text) + ".jpg";
        }
        else if (fupPortada.PostedFile.ContentType == "image/png")
        {
            // MIME correcte

            // Guardar el resultat (idem pero en png)
            fupPortada.PostedFile.SaveAs(Server.MapPath("~/Imatges/Portades") + "/" + FormatarNomFitxer(txtTitol.Text) + ".png");
            rutaPortada = "~/Imatges/Portades/" + FormatarNomFitxer(txtTitol.Text) + ".png";
        }
        else
        {
            // MIME incorrecte
            rutaPortada = "";
        }

        // Creem una nova pelicula amb les dades proporcionades
        pelicula = new clsPelicula(User.Identity.Name, txtTitol.Text, rutaPortada, txtAny.Text, txtDuracio.Text, txtPais.Text, txtGuio.Text, txtMusica.Text, txtDirector.Text, drdGenere.SelectedItem.Text, txtInterprets.Text, txtTrama.Text, txtEnllaçEnLinia.Text, txtEnllaçDescarrega.Text);

        // Premiem amb prestigi per cada edicio o creació
        usuariAutenticat.ActualitzarPrestigi();

        // I redireccionem a la pelicula (amb el nou nom, si es el cas) per a reflexar tots els canvis fets
        Response.Redirect("~/Pelicula.aspx?titol=" + pelicula.Titol);
    }

    protected void btnCancelarAfegirPelicula_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/");
    }
    #endregion
}