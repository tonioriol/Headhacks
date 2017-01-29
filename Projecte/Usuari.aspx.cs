using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Usuari : System.Web.UI.Page
{
    #region atributs
    private clsUsuari usuariVisualitzat;
    private string nom;
    private string mode;
    private Image imgFoto;
    private Button btnEliminarContacte;
    private Button btnAfegirContacte;
    private Button btnModeEdicio;
    private Button btnActualitzarDadesUsuari;
    private Button btnCancelarActualitzarDadesUsuari;
    private Button btnNouComentari;
    private Label lblNom;
    private Label lblCognoms;
    private HyperLink lnkNomComentador;
    private Label lblHoraComentari;
    private Label lblTextComentari;
    private Label lblDataNaixement;
    private Label lblCorreuElectronic;
    private HyperLink lnkNomContacte;
    private HyperLink lnkNomPelicula;
    private Image imgPortada;
    private Label lblDirector;
    private FileUpload fupFoto;
    private Label lblPrestigi;
    private TextBox txtNom;
    private TextBox txtCognoms;
    private DropDownList drdDiaNaixement;
    private DropDownList drdMesNaixement;
    private DropDownList drdAnyNaixement;
    private TextBox txtCorreuElectronic;
    private TextBox txtNouComentari;
    private List<Dictionary<string, string>> llistaContactes;
    private List<Dictionary<string, string>> llistaPeliculesVistes;
    private List<Dictionary<string, string>> llistaPeliculesVeure;
    private List<Dictionary<string, string>> llistaComentaris;
    private clsUsuari usuariAutenticat;
    private string error;
    private RequiredFieldValidator rfvNom;
    private RequiredFieldValidator rfvCognoms;
    private RequiredFieldValidator rfvDia;
    private RequiredFieldValidator rfvMes;
    private RequiredFieldValidator rfvAny;
    private RequiredFieldValidator rfvCorreu;
    private RequiredFieldValidator rfvNouComentari;
    private RangeValidator rnvDia;
    private RangeValidator rnvMes;
    private RangeValidator rnvAny;
    private HyperLink lnkContrasenya;
    #endregion

    #region Metode Pseudo-constructor
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Recollim els parametres pel mètode GET
            nom = Request.QueryString["nom"];
            mode = Request.QueryString["mode"];
            error = Request.QueryString["error"];

            // url correcta
            // per a poder fer referència al propi usuari des del site.master
            if (!String.IsNullOrEmpty(nom) && User.Identity.IsAuthenticated && nom.Equals("usuariPropi"))
            {
                nom = User.Identity.Name;
            }

            if (clsUsuari.Existeix(nom)) // usuari existent
            {
                usuariVisualitzat = new clsUsuari(nom);

                if (User.Identity.IsAuthenticated) // si l'usuari esta autenticat
                {
                    usuariAutenticat = new clsUsuari(User.Identity.Name);

                    if (nom.Equals(usuariAutenticat.NomUsuari) && String.IsNullOrEmpty(mode)) // si visualitza el seu propi perfil
                    {
                        MostrarDadesUsuariPropi();
                        MostrarContactes();
                        MostrarPeliculesVistesVeure();
                        MostrarComentarisPerfil();
                        MostrarNouComentari();
                    }
                    else if (nom.Equals(usuariAutenticat.NomUsuari) && mode.Equals("edicio")) // si vol editar el seu perfil
                    {
                        MostrarEditarPerfil();
                    }
                    else
                    {
                        if (usuariVisualitzat.EsContacte(usuariAutenticat.NomUsuari)) // si l'usuari autenticat es contacte del visualitzat
                        {
                            MostrarDadesUsuariContacte();
                            MostrarContactes();
                            MostrarComentarisPerfil();
                            MostrarNouComentari();
                        }
                        else // si no es contacte
                        {
                            MostrarDadesUsuariNoContacte();
                        }
                        MostrarPeliculesVistesVeure();
                    }
                }
                else // si es anonim
                {
                    MostrarDadesUsuariAnonim();
                }
            }
            else // usuari inexistent o url incorrecta
            {
                Response.Redirect("~/"); // Redireccionem al inici
            }
        }
        catch (Exception ex)
        {
            phDadesUsuari.Controls.Add(new LiteralControl("<div id=\"error_load\"><p>Error fatal. Missatge d'error:  " + ex.Message + "</p></div>"));
        }
    }
    #endregion

    #region Mètodes
    private void MostrarPeliculesVistesVeure()
    {
        try
        {
            // pelicules vistes
            int i = 0;
            llistaPeliculesVistes = usuariVisualitzat.ObtenirPeliculesVistes();

            phPeliculesVistesVeure.Controls.Add(new LiteralControl("<div id=\"pelicules_vistes_veure\">"));


            if (usuariVisualitzat.Error)
            {
                phPeliculesVistesVeure.Controls.Add(new LiteralControl("<div id=\"error_pelicules_vistes\"><p>Error al consultar les pelicules vistes. Missatge d'error: " + usuariVisualitzat.MsgError + "</p></div><!-- fi error_pelicules_vistes -->"));
            }
            else
            {
                phPeliculesVistesVeure.Controls.Add(new LiteralControl("<div id=\"pelicules_vistes\"><h3>Pelicules vistes</h3>"));

                if (llistaPeliculesVistes != null && llistaPeliculesVistes.Count > 0)
                {
                    phPeliculesVistesVeure.Controls.Add(new LiteralControl("<ul>"));

                    while (i < llistaPeliculesVistes.Count && i < 6)
                    {
                        lnkNomPelicula = new HyperLink();
                        lblDirector = new Label();
                        imgPortada = new Image();

                        lnkNomPelicula.Text = llistaPeliculesVistes.ElementAt(i)["titol"];
                        lnkNomPelicula.NavigateUrl = "~/Pelicula.aspx?titol=" + llistaPeliculesVistes.ElementAt(i)["titol"];
                        lblDirector.Text = llistaPeliculesVistes.ElementAt(i)["director"];
                        imgPortada.ImageUrl = llistaPeliculesVistes.ElementAt(i)["portada"];

                        imgPortada.Width = 100;

                        phPeliculesVistesVeure.Controls.Add(new LiteralControl("<li>"));
                        phPeliculesVistesVeure.Controls.Add(imgPortada);
                        phPeliculesVistesVeure.Controls.Add(new LiteralControl("<h4>"));
                        phPeliculesVistesVeure.Controls.Add(lnkNomPelicula);
                        phPeliculesVistesVeure.Controls.Add(new LiteralControl("</h4>"));
                        phPeliculesVistesVeure.Controls.Add(lblDirector);
                        phPeliculesVistesVeure.Controls.Add(new LiteralControl("</li><div class=\"clear\"></div>"));

                        i++;
                    }
                    phPeliculesVistesVeure.Controls.Add(new LiteralControl("</ul>"));
                }
                else
                {
                    phPeliculesVistesVeure.Controls.Add(new LiteralControl("<p>No hi ha pel·licules vistes</p>"));
                }
                phPeliculesVistesVeure.Controls.Add(new LiteralControl("</ul></div><!-- fi pelicules_vistes -->"));
            }


            //Pelicules a veure
            i = 0;
            llistaPeliculesVeure = usuariVisualitzat.ObtenirPeliculesVeure();

            if (usuariVisualitzat.Error)
            {
                phPeliculesVistesVeure.Controls.Add(new LiteralControl("<div id=\"error_pelicules_veure\"><p>Error al consultar les pelicules a veure. Missatge d'error: " + usuariVisualitzat.MsgError + "</p></div><!-- fi error_pelicules_veure -->"));
            }
            else
            {
                phPeliculesVistesVeure.Controls.Add(new LiteralControl("<div id=\"pelicules_veure\"><h3>Pelicules a Veure</h3>"));

                if (llistaPeliculesVeure != null && llistaPeliculesVeure.Count > 0)
                {
                    phPeliculesVistesVeure.Controls.Add(new LiteralControl("<ul>"));

                    while (i < llistaPeliculesVeure.Count && i < 6)
                    {
                        lnkNomPelicula = new HyperLink();
                        lblDirector = new Label();
                        imgPortada = new Image();

                        lnkNomPelicula.Text = llistaPeliculesVeure.ElementAt(i)["titol"];
                        lnkNomPelicula.NavigateUrl = "~/Pelicula.aspx?titol=" + llistaPeliculesVeure.ElementAt(i)["titol"];
                        lblDirector.Text = llistaPeliculesVeure.ElementAt(i)["director"];
                        imgPortada.ImageUrl = llistaPeliculesVeure.ElementAt(i)["portada"];

                        imgPortada.Width = 100;

                        phPeliculesVistesVeure.Controls.Add(new LiteralControl("<li>"));
                        phPeliculesVistesVeure.Controls.Add(imgPortada);
                        phPeliculesVistesVeure.Controls.Add(new LiteralControl("<h4>"));
                        phPeliculesVistesVeure.Controls.Add(lnkNomPelicula);
                        phPeliculesVistesVeure.Controls.Add(new LiteralControl("</h4>"));
                        phPeliculesVistesVeure.Controls.Add(lblDirector);
                        phPeliculesVistesVeure.Controls.Add(new LiteralControl("</li><div class=\"clear\"></div>"));

                        i++;
                    }
                    phPeliculesVistesVeure.Controls.Add(new LiteralControl("</ul>"));
                }
                else
                {
                    phPeliculesVistesVeure.Controls.Add(new LiteralControl("<p>No hi ha pel·licules per veure</p>"));
                }
                phPeliculesVistesVeure.Controls.Add(new LiteralControl("</ul></div><!-- fi pelicules_veure -->"));
            }

            phPeliculesVistesVeure.Controls.Add(new LiteralControl("</div><!-- fi pelicules_vistes_veure -->"));
        }
        catch (Exception ex)
        {
            phPeliculesVistesVeure.Controls.Add(new LiteralControl("<div id=\"error_pelicules_veure\"><p>Error al consultar les pelicules a veure. Missatge d'error: " + ex.Message + "</p></div><!-- fi error_pelicules_veure -->"));
        }
    }

    private void MostrarContactes()
    {
        try
        {
            int i = 0;
            llistaContactes = usuariVisualitzat.ObtenirContactes();

            if (usuariVisualitzat.Error)
            {
                phDadesUsuari.Controls.Add(new LiteralControl("<div id=\"error_contactes\"><p>Error al consultar els contactes. Missatge d'error: " + usuariVisualitzat.MsgError + "</p></div><!-- fi error_contactes -->"));
            }
            else
            {
                phContactes.Controls.Add(new LiteralControl("<div id=\"contactes\"><h3>Contactes</h3>"));
                if (llistaContactes != null && llistaContactes.Count > 0)
                {
                    phContactes.Controls.Add(new LiteralControl("<table>"));

                    while (i < llistaContactes.Count && i < 6)
                    {
                        lnkNomContacte = new HyperLink();
                        imgFoto = new Image();

                        lnkNomContacte.Text = llistaContactes.ElementAt(i)["nom"] + " " + llistaContactes.ElementAt(i)["cognoms"];
                        lnkNomContacte.NavigateUrl = "~/Usuari.aspx?nom=" + llistaContactes.ElementAt(i)["username"];
                        imgFoto.ImageUrl = llistaContactes.ElementAt(i)["foto"];

                        imgFoto.Width = 50;

                        if (i == 0 || i == 3)
                        {
                            phContactes.Controls.Add(new LiteralControl("<tr>"));
                        }
                        phContactes.Controls.Add(new LiteralControl("<td>"));
                        phContactes.Controls.Add(imgFoto);
                        phContactes.Controls.Add(new LiteralControl("<h4>"));
                        phContactes.Controls.Add(lnkNomContacte);
                        phContactes.Controls.Add(new LiteralControl("</h4></td>"));

                        if (i == 2 || i == 5 || i == llistaContactes.Count - 1)
                        {
                            phContactes.Controls.Add(new LiteralControl("</tr>"));
                        }
                        i++;
                    }
                    phContactes.Controls.Add(new LiteralControl("</table>"));
                }
                else
                {
                    phContactes.Controls.Add(new LiteralControl("<p>No hi ha contactes</p>"));
                }
                phContactes.Controls.Add(new LiteralControl("</div><!-- fi contactes -->"));
            }
        }
        catch (Exception ex)
        {
            phDadesUsuari.Controls.Add(new LiteralControl("<div id=\"error_contactes\"><p>Error al consultar els contactes. Missatge d'error: " + ex.Message + "</p></div><!-- fi error_contactes -->"));
        }
    }

    private void MostrarEditarPerfil()
    {
        try
        {
            fupFoto = new FileUpload();
            txtNom = new TextBox();
            txtCognoms = new TextBox();
            drdDiaNaixement = new DropDownList();
            drdMesNaixement = new DropDownList();
            drdAnyNaixement = new DropDownList();
            txtCorreuElectronic = new TextBox();
            //
            rfvNom = new RequiredFieldValidator();
            rfvCognoms = new RequiredFieldValidator();
            rfvDia = new RequiredFieldValidator();
            rfvMes = new RequiredFieldValidator();
            rfvAny = new RequiredFieldValidator();
            rfvCorreu = new RequiredFieldValidator();
            //
            rnvDia = new RangeValidator();
            rnvMes = new RangeValidator();
            rnvAny = new RangeValidator();

            txtNom.CssClass = "camps_editar_usuari";
            txtCognoms.CssClass = "camps_editar_usuari";
            txtCorreuElectronic.CssClass = "camps_editar_usuari";

            // Coloquem el primer item fora del for per que volem qyue sigui "selecciona"
            drdDiaNaixement.Items.Add(new ListItem("Selecciona", "0"));
            for (int i = 1; i <= 31; i++)
            {
                drdDiaNaixement.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            //idem
            drdMesNaixement.Items.Add(new ListItem("Selecciona", "0"));
            for (int i = 1; i <= 12; i++)
            {
                drdMesNaixement.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            //idem
            drdAnyNaixement.Items.Add(new ListItem("Selecciona", "0"));
            for (int i = 1900; i <= DateTime.Now.Year; i++)
            {
                drdAnyNaixement.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }

            // inicialitzem els valors als que ja te l'usuari
            txtNom.Text = usuariVisualitzat.Nom;
            txtCognoms.Text = usuariVisualitzat.Cognoms;
            drdDiaNaixement.SelectedValue = Convert.ToString(usuariVisualitzat.DataNaixement.Day);
            drdMesNaixement.SelectedValue = Convert.ToString(usuariVisualitzat.DataNaixement.Month);
            drdAnyNaixement.SelectedValue = Convert.ToString(usuariVisualitzat.DataNaixement.Year);
            txtCorreuElectronic.Text = usuariVisualitzat.CorreuElectronic;

            // Validar que el nom  no estigui vuit
            rfvNom.ErrorMessage = "El nom es requerit.";
            rfvNom.ToolTip = "El nom es requerit.";
            rfvNom.CssClass = "failureNotification";
            rfvNom.SetFocusOnError = true;
            rfvNom.ValidationGroup = "vgEditarPerfil";
            txtNom.ID = "txtNom";
            rfvNom.ControlToValidate = txtNom.ID;

            // Validar que els cognoms no estiguin vuits
            rfvCognoms.ErrorMessage = "Els cognoms son requerits.";
            rfvCognoms.ToolTip = "Els cognoms son requerits.";
            rfvCognoms.CssClass = "failureNotification";
            rfvCognoms.SetFocusOnError = true;
            rfvCognoms.ValidationGroup = "vgEditarPerfil";
            txtCognoms.ID = "txtCognoms";
            rfvCognoms.ControlToValidate = txtCognoms.ID;

            // Validar que el dia  no estigui vuit
            rfvDia.ErrorMessage = "El dia es requerit.";
            rfvDia.ToolTip = "El dia es requerit.";
            rfvDia.CssClass = "failureNotification";
            rfvDia.SetFocusOnError = true;
            rfvDia.ValidationGroup = "vgEditarPerfil";
            drdDiaNaixement.ID = "drdDiaNaixement";
            rfvDia.ControlToValidate = txtNom.ID;
            rfvDia.InitialValue = "0";

            // Validar que el mes  no estigui vuit
            rfvMes.ErrorMessage = "El mes es requerit.";
            rfvMes.ToolTip = "El mes es requerit.";
            rfvMes.CssClass = "failureNotification";
            rfvMes.SetFocusOnError = true;
            rfvMes.ValidationGroup = "vgEditarPerfil";
            drdMesNaixement.ID = "drdMesNaixement";
            rfvMes.ControlToValidate = txtNom.ID;
            rfvMes.InitialValue = "0";

            // Validar que el any  no estigui vuit
            rfvAny.ErrorMessage = "L'any es requerit.";
            rfvAny.ToolTip = "L'any es requerit.";
            rfvAny.CssClass = "failureNotification";
            rfvAny.SetFocusOnError = true;
            rfvAny.ValidationGroup = "vgEditarPerfil";
            drdAnyNaixement.ID = "txtNomNaixement";
            rfvAny.ControlToValidate = txtNom.ID;
            rfvAny.InitialValue = "0";

            // Validar que el correu no estigui vuit
            rfvCorreu.ErrorMessage = "Els correu es requerit.";
            rfvCorreu.ToolTip = "Els correu es requerit.";
            rfvCorreu.CssClass = "failureNotification";
            rfvCorreu.SetFocusOnError = true;
            rfvCorreu.ValidationGroup = "vgEditarPerfil";
            txtCorreuElectronic.ID = "txtCorreu";
            rfvCorreu.ControlToValidate = txtCorreuElectronic.ID;

            // Validar rang de valors del dia
            rnvDia.ErrorMessage = "Dia erroni. Nomes es prermet des del 1 fins al 31";
            rnvDia.ToolTip = "Dia erroni. Nomes es prermet des del 1 fins al 31";
            rnvDia.CssClass = "failureNotification";
            rnvDia.MinimumValue = "1";
            rnvDia.MaximumValue = "31";
            rnvDia.Type = ValidationDataType.Integer;
            rnvDia.SetFocusOnError = true;
            rnvDia.ValidationGroup = "vgEditarPerfil";
            drdDiaNaixement.ID = "drdDiaNaixement";
            rnvDia.ControlToValidate = drdDiaNaixement.ID;

            // Validar rang de valors del mes
            rnvMes.ErrorMessage = "Mes erroni. Nomes es prermet des del 1 fins al 12";
            rnvMes.ToolTip = "Mes erroni. Nomes es prermet des del 1 fins al 12";
            rnvMes.CssClass = "failureNotification";
            rnvMes.MinimumValue = "1";
            rnvMes.MaximumValue = "12";
            rnvMes.Type = ValidationDataType.Integer;
            rnvMes.SetFocusOnError = true;
            rnvMes.ValidationGroup = "vgEditarPerfil";
            drdMesNaixement.ID = "drdMesNaixement";
            rnvMes.ControlToValidate = drdMesNaixement.ID;

            // Validar rang de valors del any
            rnvAny.ErrorMessage = "Any erroni. Nomes es prermet des del 1900 fins l'any actual";
            rnvAny.ToolTip = "Any erroni. Nomes es prermet des del 1900 fins l'any actual";
            rnvAny.CssClass = "failureNotification";
            rnvAny.MinimumValue = "1900";
            rnvAny.MaximumValue = Convert.ToString(DateTime.Now.Year);
            rnvAny.Type = ValidationDataType.Integer;
            rnvAny.SetFocusOnError = true;
            rnvAny.ValidationGroup = "vgEditarPerfil";
            drdAnyNaixement.ID = "drdAnyNaixement";
            rnvAny.ControlToValidate = drdAnyNaixement.ID;

            phDadesUsuari.Controls.Add(new LiteralControl("<div id=\"dades_usuari_mode_edicio\"><h2>Usuari: " + usuariVisualitzat.Nom + "</h2><ul><li><h3>Foto:</h3>"));
            phDadesUsuari.Controls.Add(fupFoto);
            phDadesUsuari.Controls.Add(new LiteralControl("</li><li><h3>Nom:</h3>"));
            phDadesUsuari.Controls.Add(txtNom);
            phDadesUsuari.Controls.Add(rfvNom);
            phDadesUsuari.Controls.Add(new LiteralControl("</li><li><h3>Cognoms:</h3>"));
            phDadesUsuari.Controls.Add(txtCognoms);
            phDadesUsuari.Controls.Add(rfvCognoms);
            phDadesUsuari.Controls.Add(new LiteralControl("</li><li><h3>Data de naixement:</h3>"));
            phDadesUsuari.Controls.Add(new LiteralControl("Dia:"));
            phDadesUsuari.Controls.Add(drdDiaNaixement);
            phDadesUsuari.Controls.Add(rfvDia);
            phDadesUsuari.Controls.Add(new LiteralControl("Mes:"));
            phDadesUsuari.Controls.Add(drdMesNaixement);
            phDadesUsuari.Controls.Add(rfvDia);
            phDadesUsuari.Controls.Add(new LiteralControl("Any:"));
            phDadesUsuari.Controls.Add(drdAnyNaixement);
            phDadesUsuari.Controls.Add(rfvDia);
            phDadesUsuari.Controls.Add(new LiteralControl("</li><li><h3>Correu electrònic</h3>"));
            phDadesUsuari.Controls.Add(txtCorreuElectronic);
            phDadesUsuari.Controls.Add(new LiteralControl("</li><ul>"));

            // Afegim el boto finalitzar i l'associem a l'esdeveniment corresponent
            btnActualitzarDadesUsuari = new Button();
            btnActualitzarDadesUsuari.Text = "Actualitzar";
            btnActualitzarDadesUsuari.ValidationGroup = "vgEditarPerfil";
            btnActualitzarDadesUsuari.CssClass = "btn_editar_usuari";
            btnActualitzarDadesUsuari.Click += new EventHandler(this.btnActualitzarDadesUsuari_Click);
            phDadesUsuari.Controls.Add(btnActualitzarDadesUsuari);

            // Afegim el boto cancelar i l'associem a l'esdeveniment corresponent
            btnCancelarActualitzarDadesUsuari = new Button();
            btnCancelarActualitzarDadesUsuari.Text = "Cancelar";
            btnCancelarActualitzarDadesUsuari.CssClass = "btn_editar_usuari";
            btnCancelarActualitzarDadesUsuari.Click += new EventHandler(this.btnCancelarActualitzarDadesUsuari_Click);
            phDadesUsuari.Controls.Add(btnCancelarActualitzarDadesUsuari);

            phDadesUsuari.Controls.Add(new LiteralControl("</div><!-- fi dades_usuari -->"));
        }
        catch (Exception ex)
        {
            phDadesUsuari.Controls.Add(new LiteralControl("<div id=\"error_editar_perfil\"><p>Error a l'editar perfil. Missatge d'error: " + ex.Message + "</p></div><!-- fi error_editar_perfil -->"));
        }
    }

    private void MostrarDadesUsuariPropi()
    {
        try
        {
            imgFoto = new Image();
            lblNom = new Label();
            lblCognoms = new Label();
            lblDataNaixement = new Label();
            lblCorreuElectronic = new Label();
            lblPrestigi = new Label();
            btnModeEdicio = new Button();
            lnkContrasenya = new HyperLink();

            imgFoto.ImageUrl = usuariVisualitzat.Foto;
            lblNom.Text = usuariVisualitzat.Nom;
            lblCognoms.Text = usuariVisualitzat.Cognoms;
            lblDataNaixement.Text = Convert.ToString(usuariVisualitzat.DataNaixement).Split(' ')[0];
            lblCorreuElectronic.Text = usuariVisualitzat.CorreuElectronic;

            PrestigiNom(usuariVisualitzat.Prestigi);

            lblPrestigi.Text = PrestigiNom(usuariVisualitzat.Prestigi) + " (" + usuariVisualitzat.Prestigi + ")";


            lnkContrasenya.Text = "Canviar la Contrasenya";
            lnkContrasenya.NavigateUrl = "~/Account/ChangePassword.aspx";

            btnModeEdicio.Text = "Editar";
            btnModeEdicio.Click += new EventHandler(this.btnModeEdicio_Click);

            imgFoto.Width = 100;

            phDadesUsuari.Controls.Add(new LiteralControl("<div id=\"dades_usuari\"><h2>Usuari: " + usuariVisualitzat.Nom + "</h2><ul><li>"));
            phDadesUsuari.Controls.Add(imgFoto);
            phDadesUsuari.Controls.Add(new LiteralControl("</li><li><h3>Nom:</h3>"));
            phDadesUsuari.Controls.Add(lblNom);
            phDadesUsuari.Controls.Add(new LiteralControl("</li><li><h3>Cognoms:</h3>"));
            phDadesUsuari.Controls.Add(lblCognoms);
            phDadesUsuari.Controls.Add(new LiteralControl("</li><li><h3>Data de naixement:</h3>"));
            phDadesUsuari.Controls.Add(lblDataNaixement);
            phDadesUsuari.Controls.Add(new LiteralControl("</li><li><h3>Correu electrònic</h3>"));
            phDadesUsuari.Controls.Add(lblCorreuElectronic);
            phDadesUsuari.Controls.Add(new LiteralControl("</li><li><h3>Prestigi:</h3>"));
            phDadesUsuari.Controls.Add(lblPrestigi);
            phDadesUsuari.Controls.Add(new LiteralControl("</li><li><h3>Contrasenya:</h3>"));
            phDadesUsuari.Controls.Add(lnkContrasenya);
            phDadesUsuari.Controls.Add(new LiteralControl("</li></ul>"));
            phDadesUsuari.Controls.Add(btnModeEdicio);
            phDadesUsuari.Controls.Add(new LiteralControl("</div><!-- fi dades_usuari -->"));
        }
        catch (Exception ex)
        {
            phDadesUsuari.Controls.Add(new LiteralControl("<div id=\"error_mostrar_dades_usuari_propi\"><p>Error al mostrar dades usuari propi. Missatge d'error: " + ex.Message + "</p></div><!-- fi error_mostrar_dades_usuari_propi -->"));
        }
    }

    private string PrestigiNom(string prest)
    {
        int prestigi = Convert.ToInt32(prest);

        if (prestigi >= 0 && prestigi < 5)
        {
            return "Expulsat";
        }
        else if (prestigi >= 5 && prestigi < 10)
        {
            return "Lector";
        }
        else if (prestigi >= 10 && prestigi < 15)
        {
            return "Editor";
        }
        else if (prestigi >= 15 && prestigi < 100)
        {
            return "Creador";
        }
        else if (prestigi >= 100)
        {
            return "Borrador";
        }
        else
        {
            return "";
        }
    }

    private void MostrarComentarisPerfil()
    {
        try
        {
            // Obtenim els comentaris
            llistaComentaris = usuariVisualitzat.ObtenirComentarisPerfil();

            phComentaris.Controls.Add(new LiteralControl("<div id=\"comentaris\"><h3>Comentaris</h3>"));

            if (usuariVisualitzat.Error)
            {
                phComentaris.Controls.Add(new LiteralControl("<div id=\"error_comentaris\"><p>Error al consultar els comentaris.Missatge d'error: " + usuariVisualitzat.MsgError + "</p></div><!-- fi error_comentaris -->"));
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
                    lnkNomComentador.Text = llistaComentaris.ElementAt(i)["nom"] + " " + llistaComentaris.ElementAt(i)["cognoms"];
                    lnkNomComentador.NavigateUrl = "~/Usuari.aspx?nom=" + llistaComentaris.ElementAt(i)["username"];
                    lblHoraComentari.Text = llistaComentaris.ElementAt(i)["data_hora"];
                    lblTextComentari.Text = llistaComentaris.ElementAt(i)["comentari"];

                    imgFoto.Width = 50;

                    phComentaris.Controls.Add(new LiteralControl("<div class=\"comentari\">"));
                    phComentaris.Controls.Add(new LiteralControl("<h4>"));
                    phComentaris.Controls.Add(lnkNomComentador);
                    phComentaris.Controls.Add(new LiteralControl(" - "));
                    phComentaris.Controls.Add(lblHoraComentari);
                    phComentaris.Controls.Add(new LiteralControl("</h4>"));
                    phComentaris.Controls.Add(imgFoto);
                    phComentaris.Controls.Add(new LiteralControl("<p>"));
                    phComentaris.Controls.Add(lblTextComentari);
                    phComentaris.Controls.Add(new LiteralControl("</p></div><!-- fi comentari --><div id=\"clear\"></div>"));
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
            phComentaris.Controls.Add(new LiteralControl("<div id=\"error_comentaris\"><p>Error al consultar els comentaris.Missatge d'error: " + ex.Message + "</p></div><!-- fi error_comentaris -->"));
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

            imgFoto.ImageUrl = usuariAutenticat.Foto;
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
        catch (Exception ex)
        {
            phComentaris.Controls.Add(new LiteralControl("<div id=\"error_comentari_nou\"><p>Error al mostrar nou comentari.Missatge d'error: " + ex.Message + "</p></div><!-- fi error__mostrar_nou_comentari -->"));
        }
    }

    private void MostrarDadesUsuariContacte()
    {
        try
        {
            imgFoto = new Image();
            lblNom = new Label();
            lblCognoms = new Label();
            lblDataNaixement = new Label();
            lblCorreuElectronic = new Label();
            lblPrestigi = new Label();
            btnEliminarContacte = new Button();

            imgFoto.ImageUrl = usuariVisualitzat.Foto;
            lblNom.Text = usuariVisualitzat.Nom;
            lblCognoms.Text = usuariVisualitzat.Cognoms;
            lblPrestigi.Text = usuariVisualitzat.Prestigi;

            btnEliminarContacte.Text = "Eliminar Contacte";
            btnEliminarContacte.Click += new EventHandler(this.btnEliminarContacte_Click);

            imgFoto.Width = 100;

            phDadesUsuari.Controls.Add(new LiteralControl("<div id=\"dades_usuari\"><h2>Usuari: " + usuariVisualitzat.Nom + "</h2><ul><li>"));
            phDadesUsuari.Controls.Add(imgFoto);
            phDadesUsuari.Controls.Add(new LiteralControl("</li><li><h3>Nom:</h3>"));
            phDadesUsuari.Controls.Add(lblNom);
            phDadesUsuari.Controls.Add(new LiteralControl("</li><li><h3>Cognoms:</h3>"));
            phDadesUsuari.Controls.Add(lblCognoms);
            phDadesUsuari.Controls.Add(new LiteralControl("</li><li><h3>Prestigi:</h3>"));
            phDadesUsuari.Controls.Add(lblPrestigi);
            phDadesUsuari.Controls.Add(new LiteralControl("</li></ul>"));
            phDadesUsuari.Controls.Add(btnEliminarContacte);
            phDadesUsuari.Controls.Add(new LiteralControl("</div><!-- fi dades_usuari -->"));
        }
        catch (Exception ex)
        {
            phDadesUsuari.Controls.Add(new LiteralControl("<div id=\"error_mostrar_dades_usuari_contacte\"><p>Error al mostrar dades usuari contacte.Missatge d'error: " + ex.Message + "</p></div><!-- fi error_mostrar_dades_usuari_contacte -->"));
        }
    }

    private void MostrarDadesUsuariNoContacte()
    {
        try
        {
            imgFoto = new Image();
            lblNom = new Label();
            lblCognoms = new Label();
            btnAfegirContacte = new Button();

            imgFoto.ImageUrl = usuariVisualitzat.Foto;
            lblNom.Text = usuariVisualitzat.Nom;
            lblCognoms.Text = usuariVisualitzat.Cognoms;

            
            btnAfegirContacte.Text = "Afegir Contacte";
            btnAfegirContacte.Click += new EventHandler(this.btnAfegirContacte_Click);
            

            imgFoto.Width = 100;

            phDadesUsuari.Controls.Add(new LiteralControl("<div id=\"dades_usuari\"><h2>Usuari: " + usuariVisualitzat.Nom + "</h2><ul><li>"));
            phDadesUsuari.Controls.Add(imgFoto);
            phDadesUsuari.Controls.Add(new LiteralControl("</li><li><h3>Nom:</h3>"));
            phDadesUsuari.Controls.Add(lblNom);
            phDadesUsuari.Controls.Add(new LiteralControl("</li><li><h3>Cognoms:</h3>"));
            phDadesUsuari.Controls.Add(lblCognoms);
            phDadesUsuari.Controls.Add(new LiteralControl("</li></ul>"));
            phDadesUsuari.Controls.Add(btnAfegirContacte);
            phDadesUsuari.Controls.Add(new LiteralControl("</div><!-- fi dades_usuari -->"));
        }
        catch (Exception ex)
        {
            phDadesUsuari.Controls.Add(new LiteralControl("<div id=\"error_mostrar_dades_usuari_nocontacte\"><p>Error al mostrar dades usuari no contacte.Missatge d'error: " + ex.Message + "</p></div><!-- fi error_mostrar_dades_usuari_nocontacte -->"));
        }
    }

    private void MostrarDadesUsuariAnonim()
    {
        try
        {
            imgFoto = new Image();
            lblNom = new Label();
            lblCognoms = new Label();

            imgFoto.ImageUrl = usuariVisualitzat.Foto;
            lblNom.Text = usuariVisualitzat.Nom;
            lblCognoms.Text = usuariVisualitzat.Cognoms;

            imgFoto.Width = 100;

            phDadesUsuari.Controls.Add(new LiteralControl("<div id=\"dades_usuari\"><h2>Usuari: " + usuariVisualitzat.Nom + "</h2><ul><li>"));
            phDadesUsuari.Controls.Add(imgFoto);
            phDadesUsuari.Controls.Add(new LiteralControl("</li><li><h3>Nom:</h3>"));
            phDadesUsuari.Controls.Add(lblNom);
            phDadesUsuari.Controls.Add(new LiteralControl("</li><li><h3>Cognoms:</h3>"));
            phDadesUsuari.Controls.Add(lblCognoms);
            phDadesUsuari.Controls.Add(new LiteralControl("</li><ul></div><!-- fi dades_usuari -->"));
        }
        catch (Exception ex)
        {
            phDadesUsuari.Controls.Add(new LiteralControl("<div id=\"error_mostrar_dades_usuari_anonim\"><p>Error al mostrar dades usuari anonim.Missatge d'error: " + ex.Message + "</p></div><!-- fi error_mostrar_dades_usuari_anonim -->"));
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
            Response.Redirect("~/Usuari.aspx?nom=" + nom + "&mode=edicio");
        }
        catch (Exception)
        {
        }
    }

    protected void btnEliminarContacte_Click(object sender, EventArgs e)
    {
        try
        {
            usuariVisualitzat.EliminarContacte(usuariAutenticat.NomUsuari);
            Response.Redirect("~/Usuari.aspx?nom=" + nom);
        }
        catch (Exception)
        {
        }
    }

    protected void btnAfegirContacte_Click(object sender, EventArgs e)
    {
        try
        {
            usuariVisualitzat.AfegirContacte(usuariAutenticat.NomUsuari);
            Response.Redirect("~/Usuari.aspx?nom=" + nom);
        }
        catch (Exception)
        {
        }
    }

    protected void btnActualitzarDadesUsuari_Click(object sender, EventArgs e)
    {
        try
        {
            // Longitud en Kb, per defecte asp.net te 4MB, aixi que ho deixem, per que es suficient
            // fupPortada.PostedFile.ContentLength / 1024;

            // Actualitzem, si es que la imatge es valida, la propietat de la ruta de la imatge, i guardem la mateixa al servidor
            // Comprovem el tipus de dada que es, i si no es cap format d'imatge correcta
            if (fupFoto.PostedFile.ContentType == "image/gif")
            {
                // MIME correcte

                // Guardar el resultat cambient-li el nom pel de la pelicula substituint els espais per quions baixos (que ja hem fet) i passant-lo a minuscules
                fupFoto.PostedFile.SaveAs(Server.MapPath("~/Imatges/Fotos") + "/" + FormatarNomFitxer(nom) + ".gif");
                usuariVisualitzat.Foto = "~/Imatges/Fotos/" + FormatarNomFitxer(nom) + ".gif";
            }
            else if (fupFoto.PostedFile.ContentType == "image/jpeg" || fupFoto.PostedFile.ContentType == "image/pjpeg")
            {
                // MIME correcte

                // Guardar el resultat (idem pero en gif)
                fupFoto.PostedFile.SaveAs(Server.MapPath("~/Imatges/Fotos") + "/" + FormatarNomFitxer(nom) + ".jpg");
                usuariVisualitzat.Foto = "~/Imatges/Fotos/" + FormatarNomFitxer(nom) + ".jpg";
            }
            else if (fupFoto.PostedFile.ContentType == "image/png")
            {
                // MIME correcte

                // Guardar el resultat (idem pero en png)
                fupFoto.PostedFile.SaveAs(Server.MapPath("~/Imatges/Fotos") + "/" + FormatarNomFitxer(nom) + ".png");
                usuariVisualitzat.Foto = "~/Imatges/Fotos/" + FormatarNomFitxer(nom) + ".png";
            }
            else
            {
                // MIME incorrecte
            }

            usuariVisualitzat.Nom = txtNom.Text;
            usuariVisualitzat.Cognoms = txtCognoms.Text;
            usuariVisualitzat.DataNaixement = DateTime.Parse(drdDiaNaixement.SelectedValue + "/" + drdMesNaixement.SelectedValue + "/" + drdAnyNaixement.SelectedValue);

            usuariVisualitzat.CorreuElectronic = txtCorreuElectronic.Text;

            usuariVisualitzat.ActualitzarDadesUsuari();

            // I redireccionem al usuari (amb el nou nom, si es el cas) per a reflexar tots els canvis fets
            Response.Redirect("~/Usuari.aspx?nom=" + usuariVisualitzat.NomUsuari);
        }
        catch (Exception)
        {
        }
    }

    protected void btnCancelarActualitzarDadesUsuari_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/Usuari.aspx?nom=" + usuariVisualitzat.NomUsuari); // Tornem a la pagina d'usuari sense realitzar canvis
        }
        catch (Exception)
        {
        }
    }

    protected void btnNouComentari_Click(object sender, EventArgs e)
    {
        try
        {
            if (usuariVisualitzat.Error)
            {
                phComentaris.Controls.Add(new LiteralControl("Error: " + usuariVisualitzat.MsgError));
            }
            usuariVisualitzat.AfegirComentariPerfil(usuariAutenticat.NomUsuari, txtNouComentari.Text);
            Response.Redirect("~/Usuari.aspx?nom=" + usuariVisualitzat.NomUsuari);
        }
        catch (Exception)
        {
        }
    }
    #endregion  
}