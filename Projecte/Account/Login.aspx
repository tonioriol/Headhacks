<%@ Page Title="Inicia La Sessió" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Account_Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Inicia La Sessió
    </h2>
    <p>
        Introdueix el teu nom d'usuari i contrasenya.
        <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false">Registrat</asp:HyperLink> si no tens cap compte.
    </p>
    <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false" FailureText="L'intent d'iniciar la sessió ha sigut insatisfactori. Torna-ho a intentar." LoginButtonText="Inicia la sessió" PasswordLabelText="Contrasenya:" RememberMeText="Mantin-me connectat." UserNameLabelText="Nom d'usuari:" RememberMeSet="True" PasswordRequiredErrorMessage="La contrasenya es requerida." UserNameRequiredErrorMessage="El nom d'usuari es requerit.">
        <LayoutTemplate>
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>
            <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification" 
                 ValidationGroup="LoginUserValidationGroup"/>
            <div class="accountInfo">
                <fieldset class="login">
                    <legend>Informació del compte</legend>
                    <p>
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Nom d'usuari:</asp:Label>
                        <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                             CssClass="failureNotification" ErrorMessage="El nom d'usuari es requerit." ToolTip="El nom d'usuari es requerit." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Contrasenya:</asp:Label>
                        <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                             CssClass="failureNotification" ErrorMessage="La contrasenya es requerida." ToolTip="La contrasenya es requerida." 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:CheckBox ID="RememberMe" runat="server"/>
                        <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Mantin-me connectat</asp:Label>
                    </p>
                </fieldset>
                <p class="submitButton">
                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Inicia la sessió" ValidationGroup="LoginUserValidationGroup"/>
                </p>
            </div>
        </LayoutTemplate>
    </asp:Login>
</asp:Content>
