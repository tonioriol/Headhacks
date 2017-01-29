using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
// Afegits
using System.Web.Security;

public partial class _Default : System.Web.UI.Page
{
    #region Atributs
    clsUsuari usuari;
    int prestigi;
    private List<Dictionary<string, string>> llistaUltimesPelicules;
    private HyperLink lnkTitol;
    private HyperLink lnkUsuari;
    private HyperLink lnkLogin;
    private HyperLink lnkRegistre;
    private Image imgPortada;
    #endregion

    #region Load
    protected void Page_Load(object sender, EventArgs e)
    {
        phResum.Controls.Add(new LiteralControl("<div id=\"resum_inici\">"));
        if (User.Identity.IsAuthenticated)
        {
            // creem un objecte del usuari autenticat, en mirem el prestigi i en base a aquest valor l'afegim al rol que li correspongui
            usuari = new clsUsuari(User.Identity.Name);
            prestigi = Convert.ToInt32(usuari.Prestigi);

            ActualitzarRolsPrestigi();
            MostrarUltimesPeliculesCreades();
            MostrarUltimsUsuarisCreats();
        }
        else
        {
            MostrarResumAnonim();
        }
        phResum.Controls.Add(new LiteralControl("</div><!-- fi resum_inici -->"));
    }

    private void MostrarResumAnonim()
    {
        lnkLogin = new HyperLink();
        lnkRegistre = new HyperLink();

        lnkLogin.Text = "Inicia la sessió";
        lnkLogin.NavigateUrl = "~/Account/Login.aspx";

        lnkRegistre.Text = "registrat";
        lnkRegistre.NavigateUrl = "~/Account/Register.aspx";

        phResum.Controls.Add(new LiteralControl("<div id=\"inici\"><h2>Benvinguts a headhacks</h2><p>Un lloc Web on podras crear, indexar i compartir tot el contingut referent al cinema que t'interessa. "));
        phResum.Controls.Add(lnkLogin);
        phResum.Controls.Add(new LiteralControl(", o "));
        phResum.Controls.Add(lnkRegistre);
        phResum.Controls.Add(new LiteralControl(" si no tens cap compte.</p>"));

        phResum.Controls.Add(new LiteralControl("<p>Explora <a href=\"Pelicules.aspx\">pel·lícules</a>.</p>"));
        phResum.Controls.Add(new LiteralControl("<p>Explora <a href=\"Generes.aspx\">Generes</a>.</p>"));
        phResum.Controls.Add(new LiteralControl("<p>O utilitza la cerca, amb el camp per a buscar que hi ha a la capçalera.</p> </div>"));
    }
    #endregion

    #region Mètodes
    private void ActualitzarRolsPrestigi()
    {
        // Creem els rols dusuari si no existeixen, aixo nomes s'hauria de fer una unica
        // vegada pero com que estem en continues proves en diferents maquines...

        if (!Roles.RoleExists("Expulsat"))
        {
            Roles.CreateRole("Expulsat");
        }

        if (!Roles.RoleExists("Lector"))
        {
            Roles.CreateRole("Lector");
        }

        if (!Roles.RoleExists("Editor"))
        {
            Roles.CreateRole("Editor");
        }

        if (!Roles.RoleExists("Creador"))
        {
            Roles.CreateRole("Creador");
        }

        if (!Roles.RoleExists("Borrador"))
        {
            Roles.CreateRole("Borrador");
        }

        // Afegim o actualitzem el rol de l'usuari (si es que esta autenticat) cada vegada que es fa log-in, ja que la pagina de login reditrecciona a qui sempre i es de pas obligat
        if (prestigi >= 0 && prestigi < 5)
        {
            if (Roles.GetRolesForUser().Count() > 0)
            {
                Roles.RemoveUserFromRoles(User.Identity.Name, Roles.GetRolesForUser());
            }
            Roles.AddUserToRole(User.Identity.Name, "Expulsat");
        }
        else if (prestigi >= 5 && prestigi < 10)
        {
            if (Roles.GetRolesForUser().Count() > 0)
            {
                Roles.RemoveUserFromRoles(User.Identity.Name, Roles.GetRolesForUser());
            }
            Roles.AddUserToRole(User.Identity.Name, "Editor");
        }
        else if (prestigi >= 10 && prestigi < 15)
        {
            if (Roles.GetRolesForUser().Count() > 0)
            {
                Roles.RemoveUserFromRoles(User.Identity.Name, Roles.GetRolesForUser());
            }
            Roles.AddUserToRole(User.Identity.Name, "Editor");
        }
        else if (prestigi >= 15 && prestigi < 100)
        {
            if (Roles.GetRolesForUser().Count() > 0)
            {
                Roles.RemoveUserFromRoles(User.Identity.Name, Roles.GetRolesForUser());
            }
            Roles.AddUserToRole(User.Identity.Name, "Creador");
        }
        else if (prestigi >= 100)
        {
            if (Roles.GetRolesForUser().Count() > 0)
            {
                Roles.RemoveUserFromRoles(User.Identity.Name, Roles.GetRolesForUser());
            }
            Roles.AddUserToRole(User.Identity.Name, "Borrador");
        }
    }

    private void MostrarUltimesPeliculesCreades()
    {
        phResum.Controls.Add(new LiteralControl("<div id=\"ultimes_pelicules\"><h3>Ultimes pelicules afegides</h3>"));

        llistaUltimesPelicules = clsPelicules.ObtenirUltimesPelicules();
        if (llistaUltimesPelicules != null && llistaUltimesPelicules.Count > 0)
        {
            phResum.Controls.Add(new LiteralControl("<table>"));
            for (int i = 0; i < llistaUltimesPelicules.Count && i < 6; i++)
            {
                // Inicialitzem
                imgPortada = new Image();
                lnkTitol = new HyperLink();
                lnkUsuari = new HyperLink();

                // Omplim
                imgPortada.ImageUrl = llistaUltimesPelicules[i]["portada"];

                lnkTitol.Text = llistaUltimesPelicules[i]["titol"];
                lnkTitol.NavigateUrl = "~/Pelicula.aspx?titol=" + llistaUltimesPelicules[i]["titol"];

                lnkUsuari.Text = llistaUltimesPelicules[i]["nom"] + " " + llistaUltimesPelicules[i]["cognoms"];
                lnkUsuari.NavigateUrl = "~/Usuari.aspx?nom=" + llistaUltimesPelicules[i]["username"];

                // Mostrem
                if (i == 0 || i == 3)
                {
                    phResum.Controls.Add(new LiteralControl("<tr>"));
                }
                phResum.Controls.Add(new LiteralControl("<td>"));
                phResum.Controls.Add(imgPortada);
                phResum.Controls.Add(new LiteralControl("<div class=\"pelicula_portada\"><h4>"));
                phResum.Controls.Add(lnkTitol);
                phResum.Controls.Add(new LiteralControl("</h4><p>Editada per "));
                phResum.Controls.Add(lnkUsuari);
                phResum.Controls.Add(new LiteralControl("</p></div><!-- fi pelicula_portada --></td>"));
                if (i == 2 || i == 5 || i == llistaUltimesPelicules.Count - 1)
                {
                    phResum.Controls.Add(new LiteralControl("</tr>"));
                }
            }
            phResum.Controls.Add(new LiteralControl("</table>"));
        }
        phResum.Controls.Add(new LiteralControl("</div><!-- ultimes_pelicules -->"));
    }

    private void MostrarUltimsUsuarisCreats()
    {
        //
    }
    #endregion
}
