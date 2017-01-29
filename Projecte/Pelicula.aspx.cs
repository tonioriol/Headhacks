using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
// Afegits
using System.Web.Security;


// Arreglar valor per defecte desplegable aqui i a afegir pelicula (fet!!!)
// Arreglar el metode eliminar pelicula de la clsPelicula (fet!!!)
// Arreglar createuserwizard:(fet!!!)
//      http://msdn.microsoft.com/es-es/library/system.web.ui.webcontrols.createuserwizard.aspx
//      http://paulosay.spaces.live.com/blog/cns!7CC9F2B7406F44D0!728.entry?sa=977972350
//      http://social.msdn.microsoft.com/Forums/es-ES/netfxwebes/thread/933969c0-c58a-4d8c-b1a6-a6da78d63435
// Validar La edició dusuari (fet!!!)
// Dissenyar i implementar la Cerca de pelicules, per titol, director,... (fet!!!)
// Mostrar/utilitzar registre_edicions (fet!!!)
// Afegir contingut al Default.aspx
// Eliminar codi duplicat mitjançant mètodes, (estatics o no, o amb classes a especifiques per a mostrar...)
// Retallar imatges d'usuari
// Documentar, UML, documentar i documentar !!!
public partial class Pelicula : System.Web.UI.Page
{
    #region Atributs
    private clsPelicula pelicula;
    private clsUsuari usuari;
    private string titol;
    private string mode;
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
    private Button btnModeEdicio;
    private List<Dictionary<string, string>> llistaComentaris;
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
    private TextBox txtNouComentari;
    private HyperLink lnkNomComentador;
    private Label lblHoraComentari;
    private Label lblTextComentari;
    private Button btnNouComentari;
    private Button btnAfegirPelicula;
    private Button btnActualitzarPelicula;
    private Button btnCancelarCrearPelicula;
    private Button btnCancelarActualitzarPelicula;
    private Button btnMarcarVistaVeure;
    private Button btnRegEdicions;
    private Label lblVistaVeure;
    private Button btnEliminarPelicula;
    private Image imgFoto;
    private DropDownList drdGenere;
    private string error;
    private RequiredFieldValidator rfvTitol;
    private RequiredFieldValidator rfvGenere;
    private RangeValidator rnvAny;
    private ValidationSummary vsAfegirPelicula;
    private ValidationSummary vsEditarPelicula;
    private RequiredFieldValidator rfvNouComentari;
    #endregion

    #region Mètode Pseudo-constructor
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Recollim els parametres pel mètode GET
            titol = Request.QueryString["titol"];
            mode = Request.QueryString["mode"];
            error = Request.QueryString["error"];

            if (User.Identity.IsAuthenticated)
            {
                usuari = new clsUsuari(User.Identity.Name);
            }

            if (!String.IsNullOrEmpty(titol))
            {
                if (clsPelicula.Existeix(titol)) // Si la pelicula existeix...
                {
                    // Emplenem l'objecte pelicula a traves del constructor adequat a una pelicula existent
                    InicialitzarObjectePelicula();

                    // I li posem el <title> adequat
                    Title += pelicula.Titol;

                    if (String.IsNullOrEmpty(mode)) // Si el parametre mode es nul o esta vuit...
                    {
                        MostrarPelicula();

                        // Si l'usuari esta autenticat i te privilegis d'edició, afegim el boto per a accedir al mode edició de la pelicula
                        if (User.Identity.IsAuthenticated && Roles.IsUserInRole("Editor") || Roles.IsUserInRole("Creador") || Roles.IsUserInRole("Borrador"))
                        {
                            MostrarBotoEditarPelicula();
                            MostrarBotoRegEdicions();
                        }

                        if (User.Identity.IsAuthenticated)
                        {
                            MostrarBotoVistaVeure();
                            MostrarComentarisPelicula();

                            if (Roles.IsUserInRole("Lector") || Roles.IsUserInRole("Editor") || Roles.IsUserInRole("Creador") || Roles.IsUserInRole("Borrador"))
                            {
                                MostrarNouComentari();
                            }
                        }
                    }
                    else if (mode.Equals("edicio")) //  Si el parametre mode es igual a edició
                    {
                        MostrarEditarPelicula();
                    }
                }
                else // si no existeix la pelicula...
                {
                    // Fer rollo wikipedia, i oferir crear la fitxa de pelicula (sempre que l'usuari tingui els privilegis suficients)

                    // Si l'usuari esta autenticat i te els suficients privilegis
                    if (User.Identity.IsAuthenticated && Roles.IsUserInRole("Creador") || Roles.IsUserInRole("Borrador"))
                    {
                        // Es permet la creació de nova pelicula
                        MostrarAfegirPelicula();
                    }
                    else
                    {
                        // Si no te suficients privilegis per a la creació redirigim automaticament a l'inici de l'aplcicació
                        Response.Redirect("~/");
                    }
                }
            }
            else // Pelicula.aspx sense cap parametre
            {
                Response.Redirect("~/"); // Si no passem cap tipus de parametre, redirigim automaticament a l'inici de l'aplcicació
            }
        }
        catch (Exception ex)
        {
            phPelicula.Controls.Add(new LiteralControl("<div id=\"error_load\"><p>Error fatal. Missatge d'error:  " + ex.Message + "</p></div>"));
        }
    }
    #endregion

    #region Mètodes
    private void MostrarBotoVistaVeure()
    {
        try
        {
            btnMarcarVistaVeure = new Button();
            lblVistaVeure = new Label();
            if (usuari.HaVist(pelicula.Id))
            {
                lblVistaVeure.Text = "Ja has vist aquesta pelicula";
                btnMarcarVistaVeure.Text = "Desmarcar com a vista";
            }
            else if (usuari.VolVeure(pelicula.Id))
            {
                lblVistaVeure.Text = "Vols veure aquesta pelicula";
                btnMarcarVistaVeure.Text = "Marcar com a vista";
            }
            else
            {
                lblVistaVeure.Text = "No has vist ni vols veure aquesta pelicula";
                btnMarcarVistaVeure.Text = "Marcar per a veure";
            }
            btnMarcarVistaVeure.Click += new EventHandler(this.btnMarcarVistaVeure_Click);

            phPelicula.Controls.Add(new LiteralControl("<div id=\"boto_marcar\">"));
            phPelicula.Controls.Add(new LiteralControl("<p>"));
            phPelicula.Controls.Add(lblVistaVeure);
            phPelicula.Controls.Add(new LiteralControl("</p>"));
            phPelicula.Controls.Add(btnMarcarVistaVeure);
            phPelicula.Controls.Add(new LiteralControl("</div><!-- fi boto_marcar -->"));
        }
        catch (Exception ex)
        {
            phPelicula.Controls.Add(new LiteralControl("<div id=\"error_boto_vista_veure\"><p>Error al mostrar boto vista veure. Missatge d'error:  " + ex.Message + "</p></div>"));
        }
    }

    private void InicialitzarObjectePelicula()
    {
        try
        {
            // Intercanviem els guions baixos per espais en blanc per a recollir correctament la pelicula al fer la consulta
            titol = titol.Replace("_", " ");

            // passem el nom de la pelicula al constructor quan creem l'objecte, que ens omplira totes les propietats amb les dades corresponents a traves d'una consulta
            if (User.Identity.IsAuthenticated)
            {
                pelicula = new clsPelicula(User.Identity.Name, titol);
            }
            else
            {
                pelicula = new clsPelicula(null, titol);
            }
        }
        catch (Exception)
        {
        }
    }

    private void MostrarPelicula()
    {
        try
        {
            //Inicialitzem els controls
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
            imgPortada.ImageUrl = pelicula.Portada;
            lblTitol.Text = pelicula.Titol;
            lblDirector.Text = pelicula.Director;
            lblAny.Text = pelicula.Any;
            lblDuracio.Text = pelicula.Duracio;
            lblPais.Text = pelicula.Pais;
            lblGuio.Text = pelicula.Guio;
            lblMusica.Text = pelicula.Musica;
            lblGenere.Text = pelicula.Genere;
            lblInterprets.Text = pelicula.Interprets;
            lblTrama.Text = pelicula.Trama;
            lnkEnllaçEnLinia.NavigateUrl = pelicula.EnllaçEnLinia;
            lnkEnllaçEnLinia.Text = pelicula.EnllaçEnLinia;
            lnkEnllaçDescarrega.NavigateUrl = pelicula.EnllaçDescarrega;
            lnkEnllaçDescarrega.Text = pelicula.EnllaçDescarrega;

            // Li donem una amplada sempre igual, l'altura sera proporcional a l'aplada
            //imgPortada.Width = 200;

            // els afegim al placeholder, juntament amb totes les etiquetes html necessaries
            phPelicula.Controls.Add(new LiteralControl("<div id=\"pelicula\"><h2>Pelicula: " + pelicula.Titol + "</h2>"));
            phPelicula.Controls.Add(imgPortada);
            phPelicula.Controls.Add(new LiteralControl("<div id=\"dades_pelicula\"><ul><li><h3>Titol:</h3>"));
            phPelicula.Controls.Add(lblTitol);
            phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Director:</h3>"));
            phPelicula.Controls.Add(lblDirector);
            phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Any:</h3>"));
            phPelicula.Controls.Add(lblAny);
            phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Duració:</h3>"));
            phPelicula.Controls.Add(lblDuracio);
            phPelicula.Controls.Add(new LiteralControl("</li><li><h3>País:</h3>"));
            phPelicula.Controls.Add(lblPais);
            phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Guio:</h3>"));
            phPelicula.Controls.Add(lblGuio);
            phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Música:</h3>"));
            phPelicula.Controls.Add(lblMusica);
            phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Gènere:</h3>"));
            phPelicula.Controls.Add(lblGenere);
            phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Interprets:</h3>"));
            phPelicula.Controls.Add(lblInterprets);
            phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Trama:</h3>"));
            phPelicula.Controls.Add(lblTrama);
            phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Veure en linia:</h3>"));
            phPelicula.Controls.Add(lnkEnllaçEnLinia);
            phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Descarregar:</h3>"));
            phPelicula.Controls.Add(lnkEnllaçDescarrega);
            phPelicula.Controls.Add(new LiteralControl("</li></ul></div><!-- fi dades_pelicula --></div><!-- fi pelicula -->"));

        }
        catch (Exception ex)
        {
            phPelicula.Controls.Add(new LiteralControl("<div id=\"error_mostrar_pelicula\"><p>Error al mostrar pelicula. Missatge d'error:  " + ex.Message + "</p></div>"));
        }

    }

    private void MostrarBotoEditarPelicula()
    {
        try
        {
            btnModeEdicio = new Button();
            btnModeEdicio.Text = "Editar Pel·licula";
            btnModeEdicio.Click += new EventHandler(this.btnModeEdicio_Click);

            phPelicula.Controls.Add(new LiteralControl("<div id=\"boto_edicio\">"));
            phPelicula.Controls.Add(btnModeEdicio);
            phPelicula.Controls.Add(new LiteralControl("</div><!-- fi boto_edicio -->"));
        }
        catch (Exception)
        {
        }
    }

    private void MostrarBotoRegEdicions()
    {
        try
        {
            btnRegEdicions = new Button();
            btnRegEdicions.Text = "Registre d'Edicions";
            btnRegEdicions.Click += new EventHandler(this.btnRegEdicions_Click);


            phPelicula.Controls.Add(new LiteralControl("<div id=\"boto_regEdicions\">"));
            phPelicula.Controls.Add(btnRegEdicions);
            phPelicula.Controls.Add(new LiteralControl("</div><!-- fi boto_regEdicions -->"));

        }
        catch (Exception)
        {

        }
    }

    private void MostrarComentarisPelicula()
    {
        try
        {
            // Obtenim els comentaris
            llistaComentaris = pelicula.ObtenirComentarisPelicula();

            phComentaris.Controls.Add(new LiteralControl("<div id=\"comentaris\"><h3>Comentaris</h3>"));

            if (pelicula.Error)
            {
                phComentaris.Controls.Add(new LiteralControl("<div id=\"error_comentaris\"><p>Error al consultar els comentaris.Missatge d'error: " + pelicula.MsgError + "</p></div><!-- fi error_comentaris -->"));
            }
            else if (llistaComentaris != null && llistaComentaris.Count > 0)
            {
                // Creem els labels que contindran els comentaris


                for (int i = 0; i < llistaComentaris.Count; i++)
                {
                    // fem "new Label()" a cada iteració per a que no surtin nomes els ultims comentaris
                    imgFoto = new Image();
                    lnkNomComentador = new HyperLink();
                    lblHoraComentari = new Label();
                    lblTextComentari = new Label();

                    imgFoto.ImageUrl = llistaComentaris.ElementAt(i)["foto"];
                    lnkNomComentador.NavigateUrl = "~/Usuari.aspx?nom=" + llistaComentaris.ElementAt(i)["username"];
                    lnkNomComentador.Text = llistaComentaris.ElementAt(i)["nom"] + " " + llistaComentaris.ElementAt(i)["cognoms"];
                    lblHoraComentari.Text = llistaComentaris.ElementAt(i)["data_hora"];
                    lblTextComentari.Text = llistaComentaris.ElementAt(i)["comentari"];

                    imgFoto.Width = 50;

                    phComentaris.Controls.Add(new LiteralControl("<div class=\"comentari\">"));
                    phComentaris.Controls.Add(imgFoto);
                    phComentaris.Controls.Add(new LiteralControl("<h4>"));
                    phComentaris.Controls.Add(lnkNomComentador);
                    phComentaris.Controls.Add(new LiteralControl(" - "));
                    phComentaris.Controls.Add(lblHoraComentari);
                    phComentaris.Controls.Add(new LiteralControl("</h4>"));
                    phComentaris.Controls.Add(imgFoto);
                    phComentaris.Controls.Add(new LiteralControl("<p>"));
                    phComentaris.Controls.Add(lblTextComentari);
                    phComentaris.Controls.Add(new LiteralControl("</p></div><!-- fi comentari --><div class=\"clear\"></div>"));
                }
            }
            else
            {
                phComentaris.Controls.Add(new LiteralControl("<div id=\"sense_comentaris\"><p>No hi han comentaris</p></div><!-- fi sense_comentaris -->"));
            }

            phComentaris.Controls.Add(new LiteralControl("</div><!-- fi comentaris -->"));
        }
        catch (Exception ex)
        {
            phPelicula.Controls.Add(new LiteralControl("<div id=\"error_mostrar_comentaris_pelicula\"><p>Error al mostrar comentaris pelicula. Missatge d'error:  " + ex.Message + "</p></div>"));
        }
    }

    private void MostrarNouComentari()
    {
        try
        {
            // inicialitzem els controls per a afegir un nou comentari
            imgFoto = new Image();
            txtNouComentari = new TextBox();
            txtNouComentari.TextMode = TextBoxMode.MultiLine;


            rfvNouComentari = new RequiredFieldValidator();

            // Validar que el comentari  no estigui vuit
            rfvNouComentari.ErrorMessage = "El comentari es requerit.";
            rfvNouComentari.ToolTip = "El comentari es requerit.";
            rfvNouComentari.CssClass = "failureNotification";
            rfvNouComentari.SetFocusOnError = true;
            rfvNouComentari.ValidationGroup = "vgNouComentari";
            txtNouComentari.ID = "txtNouComentari";
            rfvNouComentari.ControlToValidate = txtNouComentari.ID;

            btnNouComentari = new Button();
            btnNouComentari.Text = "Afegir comentari";
            btnNouComentari.ValidationGroup = "vgNouComentari";
            btnNouComentari.Click += new EventHandler(this.btnNouComentari_Click);

            imgFoto.ImageUrl = usuari.Foto;

            imgFoto.Width = 50;

            // els afegim juntament amb les corresponents etiquetes html
            phComentaris.Controls.Add(new LiteralControl("<div id=\"nou_comentari\"><h3>Afegeix un nou comentari</h3>"));
            phComentaris.Controls.Add(imgFoto);
            phComentaris.Controls.Add(txtNouComentari);
            phComentaris.Controls.Add(new LiteralControl("<br />"));
            phComentaris.Controls.Add(rfvNouComentari);
            phComentaris.Controls.Add(btnNouComentari);
            phComentaris.Controls.Add(new LiteralControl("</div><!-- fi nou_comentari -->"));
        }
        catch (Exception)
        {
        }
    }

    private void MostrarEditarPelicula()
    {
        try
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
            vsEditarPelicula = new ValidationSummary();

            // Apliquem una mica d'estils
            txtInterprets.TextMode = TextBoxMode.MultiLine;
            txtTrama.TextMode = TextBoxMode.MultiLine;

            txtTitol.CssClass = "camps_editar_pelicula";
            txtDirector.CssClass = "camps_editar_pelicula";
            txtAny.CssClass = "camps_editar_pelicula";
            txtDuracio.CssClass = "camps_editar_pelicula";
            txtPais.CssClass = "camps_editar_pelicula";
            txtGuio.CssClass = "camps_editar_pelicula";
            txtMusica.CssClass = "camps_editar_pelicula";
            txtGenere.CssClass = "camps_editar_pelicula";
            txtInterprets.CssClass = "camps_editar_pelicula";
            txtTrama.CssClass = "camps_editar_pelicula";
            txtEnllaçEnLinia.CssClass = "camps_editar_pelicula";
            txtEnllaçDescarrega.CssClass = "camps_editar_pelicula";

            // Validacio
            vsEditarPelicula.ValidationGroup = "vgEditarPelicula";

            // Validar que el nom de la pelicula no estigui vuit          
            rfvTitol.ErrorMessage = "El titol es requerit";
            rfvTitol.ToolTip = "El titol es requerit";
            rfvTitol.CssClass = "failureNotification";
            rfvTitol.SetFocusOnError = true;
            rfvTitol.ValidationGroup = "vgEditarPelicula";
            txtTitol.ID = "txtTitol";
            rfvTitol.ControlToValidate = txtTitol.ID;

            // Validar rang de valors del any
            rnvAny.ErrorMessage = "Any erroni. Nomes es prermet des del 1895 fins l'any actual";
            rnvAny.ToolTip = "Any erroni. Nomes es prermet des del 1895 fins l'any actual";
            rnvAny.CssClass = "failureNotification";
            rnvAny.MinimumValue = "1895";
            rnvAny.MaximumValue = Convert.ToString(DateTime.Now.Year);
            rnvAny.Type = ValidationDataType.Integer;
            rnvAny.SetFocusOnError = true;
            rnvAny.ValidationGroup = "vgEditarPelicula";
            txtAny.ID = "txtAny";
            rnvAny.ControlToValidate = txtAny.ID;

            // Validar que el gènere estigui seleccionat            
            rfvGenere.ErrorMessage = "El gènere es requerit";
            rfvGenere.ToolTip = "El gènere es requerit";
            rfvGenere.CssClass = "failureNotification";
            rfvGenere.SetFocusOnError = true;
            rfvGenere.ValidationGroup = "vgAfegirPelicula";
            drdGenere.ID = "drdGenere";
            rfvGenere.ControlToValidate = drdGenere.ID;
            rfvGenere.InitialValue = "0";

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

            // Els inicialitzem amb els valors actuals de la pelicula en questió
            txtTitol.Text = pelicula.Titol;
            txtDirector.Text = pelicula.Director;
            txtAny.Text = pelicula.Any;
            txtDuracio.Text = pelicula.Duracio;
            txtPais.Text = pelicula.Pais;
            txtGuio.Text = pelicula.Guio;
            txtMusica.Text = pelicula.Musica;
            drdGenere.SelectedItem.Text = pelicula.Genere;
            txtInterprets.Text = pelicula.Interprets;
            txtTrama.Text = pelicula.Trama;
            txtEnllaçEnLinia.Text = pelicula.EnllaçEnLinia;
            txtEnllaçDescarrega.Text = pelicula.EnllaçDescarrega;

            // els afegim al placeholder, juntament amb totes les etiquetes html necessaries            
            phPelicula.Controls.Add(new LiteralControl("<div id=\"pelicula_mode_edicio\"><h2>Editar pel·licula " + pelicula.Titol + "</h2><ul><li><h3>Portada:</h3>"));
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
            phPelicula.Controls.Add(new LiteralControl("<br />"));
            phPelicula.Controls.Add(rfvGenere);
            phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Interprets:</h3>"));
            phPelicula.Controls.Add(txtInterprets);
            phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Trama:</h3>"));
            phPelicula.Controls.Add(txtTrama);
            phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Veure en linia:</h3>"));
            phPelicula.Controls.Add(txtEnllaçEnLinia);
            phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Descarregar:</h3>"));
            phPelicula.Controls.Add(txtEnllaçDescarrega);
            phPelicula.Controls.Add(new LiteralControl("</li><ul></div><!-- fi pelicula_mode_creacio -->"));

            // Afegim el boto finalitzar i l'associem a l'esdeveniment corresponent
            btnActualitzarPelicula = new Button();
            btnActualitzarPelicula.Text = "Actualitzar";
            btnActualitzarPelicula.CssClass = "btn_editar_pelicula";
            btnActualitzarPelicula.ValidationGroup = "vgEditarPelicula";
            btnActualitzarPelicula.Click += new EventHandler(this.btnActualitzarPelicula_Click);
            phPelicula.Controls.Add(btnActualitzarPelicula);

            // Afegim el boto cancelar i l'associem a l'esdeveniment corresponent
            btnCancelarActualitzarPelicula = new Button();
            btnCancelarActualitzarPelicula.Text = "Cancelar";
            btnCancelarActualitzarPelicula.CssClass = "btn_editar_pelicula";
            btnCancelarActualitzarPelicula.Click += new EventHandler(this.btnCancelarActualitzarPelicula_Click);
            phPelicula.Controls.Add(btnCancelarActualitzarPelicula);

            btnEliminarPelicula = new Button();
            btnEliminarPelicula.Text = "Eliminar";
            btnEliminarPelicula.Click += new EventHandler(this.btnEliminarPelicula_Click);
            phPelicula.Controls.Add(btnEliminarPelicula);

            phPelicula.Controls.Add(new LiteralControl("</div><!-- fi pelicula_mode_edicio -->"));

        }
        catch (Exception ex)
        {
            phPelicula.Controls.Add(new LiteralControl("<div id=\"error_editar_pelicula\"><p>Error a l'editar pelicula. Missatge d'error: " + ex.Message + "</p></div><!-- fi error_editar_pelicula -->"));
        }
    }

    private void MostrarAfegirPelicula()
    {
        try
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
            vsAfegirPelicula = new ValidationSummary();

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
            vsAfegirPelicula.ValidationGroup = "vgAfegirPelicula";

            // Validar que el nom de la pelicula no estigui vuit          
            rfvTitol.ErrorMessage = "El titol es requerit";
            rfvTitol.ToolTip = "El titol es requerit";
            rfvTitol.CssClass = "failureNotification";
            rfvTitol.SetFocusOnError = true;
            rfvTitol.ValidationGroup = "vgAfegirPelicula";
            txtTitol.ID = "txtTitol";
            rfvTitol.ControlToValidate = txtTitol.ID;

            // Validar rang de valors del any
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
            rfvGenere.ErrorMessage = "El gènere es requerit";
            rfvGenere.ToolTip = "El gènere es requerit";
            rfvGenere.CssClass = "failureNotification";
            rfvGenere.SetFocusOnError = true;
            rfvGenere.ValidationGroup = "vgAfegirPelicula";
            drdGenere.ID = "drdGenere";
            rfvGenere.ControlToValidate = drdGenere.ID;
            rfvGenere.InitialValue = "0";

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
            phPelicula.Controls.Add(new LiteralControl("<br />"));
            phPelicula.Controls.Add(rfvGenere);
            phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Interprets:</h3>"));
            phPelicula.Controls.Add(txtInterprets);
            phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Trama:</h3>"));
            phPelicula.Controls.Add(txtTrama);
            phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Veure en linia:</h3>"));
            phPelicula.Controls.Add(txtEnllaçEnLinia);
            phPelicula.Controls.Add(new LiteralControl("</li><li><h3>Descarregar:</h3>"));
            phPelicula.Controls.Add(txtEnllaçDescarrega);
            phPelicula.Controls.Add(new LiteralControl("</li><ul></div><!-- fi pelicula_mode_creacio -->"));

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
        }
        catch (Exception ex)
        {
            phPelicula.Controls.Add(new LiteralControl("<div id=\"error_afegir_pelicula\"><p>Error a l'afegir pelicula. Missatge d'error: " + ex.Message + "</p></div><!-- fi error_afegir_pelicula -->"));
        }
    }

    private string FormatarNomFitxer(string cadena)
    {
        try
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
        catch (Exception)
        {
            return null;
        }
    }
    #endregion

    #region Esdeveniments
    protected void btnModeEdicio_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/Pelicula.aspx?titol=" + titol + "&mode=edicio");
        }
        catch (Exception)
        {
        }
    }

    protected void btnNouComentari_Click(object sender, EventArgs e)
    {
        try
        {
            if (pelicula.Error)
            {
                phComentaris.Controls.Add(new LiteralControl("Error: " + pelicula.MsgError));
            }
            pelicula.AfegirComentariPelicula(txtNouComentari.Text);
            Response.Redirect("~/Pelicula.aspx?titol=" + titol);
        }
        catch (Exception)
        {
        }
    }

    protected void btnActualitzarPelicula_Click(object sender, EventArgs e)
    {
        try
        {
            // Longitud en Kb, per defecte asp.net te 4MB, aixi que ho deixem, per que es suficient
            // fupPortada.PostedFile.ContentLength / 1024;

            // Actualitzem, si es que la imatge es valida, la propietat de la ruta de la imatge, i guardem la mateixa al servidor
            // Comprovem el tipus de dada que es, i si no es cap format d'imatge correcta
            if (fupPortada.HasFile)
            {
                if (fupPortada.PostedFile.ContentType == "image/gif")
                {
                    // MIME correcte

                    // Guardar el resultat cambient-li el nom pel de la pelicula substituint els espais per quions baixos (que ja hem fet) i passant-lo a minuscules
                    fupPortada.PostedFile.SaveAs(Server.MapPath("~/Imatges/Portades") + "/" + FormatarNomFitxer(titol) + ".gif");
                    pelicula.Portada = "~/Imatges/Portades/" + FormatarNomFitxer(titol) + ".gif";
                }
                else if (fupPortada.PostedFile.ContentType == "image/jpeg" || fupPortada.PostedFile.ContentType == "image/pjpeg")
                {
                    // MIME correcte

                    // Guardar el resultat (idem pero en gif)
                    fupPortada.PostedFile.SaveAs(Server.MapPath("~/Imatges/Portades") + "/" + FormatarNomFitxer(titol) + ".jpg");
                    pelicula.Portada = "~/Imatges/Portades/" + FormatarNomFitxer(titol) + ".jpg";
                }
                else if (fupPortada.PostedFile.ContentType == "image/png")
                {
                    // MIME correcte

                    // Guardar el resultat (idem pero en png)
                    fupPortada.PostedFile.SaveAs(Server.MapPath("~/Imatges/Portades") + "/" + FormatarNomFitxer(titol) + ".png");
                    pelicula.Portada = "~/Imatges/Portades/" + FormatarNomFitxer(titol) + ".png";
                }
            }

            // Actualitzem les altres propietats del objecte pelicula
            pelicula.Titol = txtTitol.Text;
            pelicula.Director = txtDirector.Text;
            pelicula.Any = txtAny.Text;
            pelicula.Duracio = txtDuracio.Text;
            pelicula.Pais = txtPais.Text;
            pelicula.Guio = txtGuio.Text;
            pelicula.Musica = txtMusica.Text;
            pelicula.Genere = drdGenere.SelectedItem.Text;
            pelicula.Interprets = txtInterprets.Text;
            pelicula.Trama = txtTrama.Text;
            pelicula.EnllaçEnLinia = txtEnllaçEnLinia.Text;
            pelicula.EnllaçDescarrega = txtEnllaçDescarrega.Text;

            // Actualitzem la base de dades a traves d'aquest mètode
            pelicula.ActualitzarDadesPelicula();

            // es prèmia al usuari per cada pelicula editada amb èxit (tambe per cada creada)
            usuari.ActualitzarPrestigi();

            // I redireccionem a la pelicula (amb el nou nom, si es el cas) per a reflexar tots els canvis fets
            Response.Redirect("~/Pelicula.aspx?titol=" + pelicula.Titol);
        }
        catch (Exception)
        {
        }
    }

    protected void btnAfegirPelicula_Click(object sender, EventArgs e)
    {
        try
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

            // es prèmia al usuari per cada pelicula afegida amb èxit (tambe per cada edició)
            usuari.ActualitzarPrestigi();

            // I redireccionem a la pelicula (amb el nou nom, si es el cas) per a reflexar tots els canvis fets
            Response.Redirect("~/Pelicula.aspx?titol=" + pelicula.Titol);

        }
        catch (Exception)
        {
        }
    }

    protected void btnCancelarAfegirPelicula_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/");
        }
        catch (Exception)
        {
        }
    }

    protected void btnCancelarActualitzarPelicula_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/Pelicula.aspx?titol=" + pelicula.Titol);
        }
        catch (Exception)
        {
        }
    }

    protected void btnMarcarVistaVeure_Click(object sender, EventArgs e)
    {
        try
        {
            if (usuari.HaVist(pelicula.Id))
            {
                usuari.EliminarPeliculaVista(pelicula.Id);
            }
            else if (usuari.VolVeure(pelicula.Id))
            {
                usuari.AfegirPeliculaVista(pelicula.Id);
            }
            else
            {
                usuari.AfegirPeliculaVeure(pelicula.Id);
            }
            Response.Redirect("~/Pelicula.aspx?titol=" + pelicula.Titol);
        }
        catch (Exception)
        {
        }
    }

    protected void btnEliminarPelicula_Click(object sender, EventArgs e)
    {
        try
        {
            pelicula.EliminarPelicula();
            Response.Redirect("~/"); // Redireccionem al inici despres d'eliminbar la pelicula
        }
        catch (Exception)
        {
        }
    }

    protected void btnRegEdicions_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/RegEdicions.aspx?titol=" + pelicula.Titol);

    }
    #endregion
}