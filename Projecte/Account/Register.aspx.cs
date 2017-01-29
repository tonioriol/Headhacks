using System;
using System.Web.Security;
using System.Web.UI.WebControls;

public partial class Account_Register : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
    }

    protected void RegisterUser_CreatedUser(object sender, EventArgs e)
    {
        try
        {
            // This event is raised only when the user
            // has been successfully created. Therefore
            // when we call Membership.GetUser, we won't
            // get a null user

            MembershipUser oMembershipUser = Membership.GetUser(RegisterUser.UserName);

            // Next is we need to access the custom fields
            // added to the CreateUserWizard control (CUW).
            // Make sure that when we call the FindControl method,
            // we are calling the ID of these custom fields

            // Recollim els camps personalitzats del registeruserwizard
            TextBox Nom = (TextBox)RegisterUserWizardStep.ContentTemplateContainer.FindControl("Nom");
            TextBox Cognoms = (TextBox)RegisterUserWizardStep.ContentTemplateContainer.FindControl("Cognoms");
            TextBox Dia = (TextBox)RegisterUserWizardStep.ContentTemplateContainer.FindControl("Dia");
            TextBox Mes = (TextBox)RegisterUserWizardStep.ContentTemplateContainer.FindControl("Mes");
            TextBox Any = (TextBox)RegisterUserWizardStep.ContentTemplateContainer.FindControl("Any");

            FileUpload fupFoto = (FileUpload)RegisterUserWizardStep.ContentTemplateContainer.FindControl("Foto");

            // Creem l'objecte de l'usuari recent creat
            clsUsuari usuariCreat = new clsUsuari(RegisterUser.UserName);

            if (fupFoto.PostedFile.ContentType == "image/gif")
            {
                // MIME correcte

                // Guardar el resultat cambient-li el nom pel de la pelicula substituint els espais per quions baixos (que ja hem fet) i passant-lo a minuscules
                fupFoto.PostedFile.SaveAs(Server.MapPath("~/Imatges/Fotos") + "/" + FormatarNomFitxer(Nom.Text) + ".gif");
                usuariCreat.Foto = "~/Imatges/Fotos/" + FormatarNomFitxer(Nom.Text) + ".gif";
            }
            else if (fupFoto.PostedFile.ContentType == "image/jpeg" || fupFoto.PostedFile.ContentType == "image/pjpeg")
            {
                // MIME correcte

                // Guardar el resultat (idem pero en gif)
                fupFoto.PostedFile.SaveAs(Server.MapPath("~/Imatges/Fotos") + "/" + FormatarNomFitxer(Nom.Text) + ".jpg");
                usuariCreat.Foto = "~/Imatges/Fotos/" + FormatarNomFitxer(Nom.Text) + ".jpg";
            }
            else if (fupFoto.PostedFile.ContentType == "image/png")
            {
                // MIME correcte

                // Guardar el resultat (idem pero en png)
                fupFoto.PostedFile.SaveAs(Server.MapPath("~/Imatges/Fotos") + "/" + FormatarNomFitxer(Nom.Text) + ".png");
                usuariCreat.Foto = "~/Imatges/Fotos/" + FormatarNomFitxer(Nom.Text) + ".png";
            }
            else
            {
                // MIME incorrecte
                usuariCreat.Foto = "";
            }

            usuariCreat.Nom = Nom.Text;
            usuariCreat.Cognoms = Cognoms.Text;
            usuariCreat.DataNaixement = DateTime.Parse(Dia.Text + "/" + Mes.Text + "/" + Any.Text);


            usuariCreat.ActualitzarDadesUsuari();
        }
        catch (Exception)
        {
        }

        ////////////////////////////////////////////////////////////////////////

        FormsAuthentication.SetAuthCookie(RegisterUser.UserName, false /* createPersistentCookie */);

        string continueUrl = RegisterUser.ContinueDestinationPageUrl;
        if (String.IsNullOrEmpty(continueUrl))
        {
            continueUrl = "~/";
        }
        Response.Redirect(continueUrl);
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
}
