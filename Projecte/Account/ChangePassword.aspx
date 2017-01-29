<%@ Page Title="Canviar Contrasenya" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="Account_ChangePassword" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Canviar la contrasenya
    </h2>
    <p>
        Utilitza el formulari seguent per a canviar la contrasenya.
    </p>
    <p>
        Les noves contrasenyes han de ser d"un minim de <%= Membership.MinRequiredPasswordLength %> caracters de longitud.
    </p>
    <asp:ChangePassword ID="ChangeUserPassword" runat="server" CancelDestinationPageUrl="~/" EnableViewState="false" RenderOuterTable="false" 
         SuccessPageUrl="ChangePasswordSuccess.aspx">
        <ChangePasswordTemplate>
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>
            <asp:ValidationSummary ID="ChangeUserPasswordValidationSummary" runat="server" CssClass="failureNotification" 
                 ValidationGroup="ChangeUserPasswordValidationGroup"/>
            <div class="accountInfo">
                <fieldset class="changePassword">
                    <legend>Informació del compte</legend>
                    <p>
                        <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword">Contresenya antiga:</asp:Label>
                        <asp:TextBox ID="CurrentPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword" 
                             CssClass="failureNotification" ErrorMessage="La antiga contrasenya es requerida." ToolTip="La antiga contrasenya es requerida." 
                             ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">Nova contrasenya:</asp:Label>
                        <asp:TextBox ID="NewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword" 
                             CssClass="failureNotification" ErrorMessage="La nova contrasenya es requerida." ToolTip="La nova contrasenya es requerida." 
                             ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                    </p>
                    <p>
                        <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">Confirma la nova contrasenya:</asp:Label>
                        <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" 
                             CssClass="failureNotification" Display="Dynamic" ErrorMessage="La Confirmació de contrasenya es requerida."
                             ToolTip="La Confirmació de contrasenya es requerida." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" 
                             CssClass="failureNotification" Display="Dynamic" ErrorMessage="La confirmació de la nova contrasenya ha de coinicidir amb la nova contrasenya entrada."
                             ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:CompareValidator>
                    </p>
                </fieldset>
                <p class="submitButton">
                    <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancelar"/>
                    <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword" Text="Canviar contrasenya" 
                         ValidationGroup="ChangeUserPasswordValidationGroup"/>
                </p>
            </div>
        </ChangePasswordTemplate>
    </asp:ChangePassword>
</asp:Content>
