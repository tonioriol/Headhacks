<%@ Page Title="Registrarse" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Register.aspx.cs" Inherits="Account_Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:CreateUserWizard ID="RegisterUser" runat="server" EnableViewState="false" OnCreatedUser="RegisterUser_CreatedUser">
        <LayoutTemplate>
            <asp:PlaceHolder ID="wizardStepPlaceholder" runat="server"></asp:PlaceHolder>
            <asp:PlaceHolder ID="navigationPlaceholder" runat="server"></asp:PlaceHolder>
        </LayoutTemplate>
        <WizardSteps>
            <asp:CreateUserWizardStep ID="RegisterUserWizardStep" runat="server" Title="Crear un nou compte">
                <ContentTemplate>
                    <h2>
                        Crear un compte
                    </h2>
                    <p>
                        Utilitza aquest formulari per a crearte un compte.
                    </p>
                    <p>
                        La contrasenya ha de ser de un minim de <%= Membership.MinRequiredPasswordLength %> caracters de longitud.
                    </p>
                    <span class="failureNotification">
                        <asp:Literal ID="ErrorMessage" runat="server"></asp:Literal>
                    </span>
                    <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" CssClass="failureNotification" 
                         ValidationGroup="RegisterUserValidationGroup"/>
                    <div class="accountInfo">
                        <fieldset class="register">
                            <legend>Informació del compte</legend>
                            <p>
                                <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Nom d'usuari:</asp:Label>
                                <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" 
                                     CssClass="failureNotification" ErrorMessage="El nom d'usuari es requerit." ToolTip="El nom d'usuari es requerit." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="EmailLabel" runat="server" AssociatedControlID="Email">Correu electrònic:</asp:Label>
                                <asp:TextBox ID="Email" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" 
                                     CssClass="failureNotification" ErrorMessage="El Correu es requerit." ToolTip="El Correu es requerit." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="NomLabel" runat="server" AssociatedControlID="Nom">Nom:</asp:Label>
                                <asp:TextBox ID="Nom" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="NomRequerit" runat="server" ControlToValidate="Nom" 
                                     CssClass="failureNotification" ErrorMessage="El nom es requerit." ToolTip="El nom es requerit." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="CognomsLabel" runat="server" AssociatedControlID="Cognoms">Cognoms:</asp:Label>
                                <asp:TextBox ID="Cognoms" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="CognomsRequerits" runat="server" ControlToValidate="Cognoms" 
                                     CssClass="failureNotification" ErrorMessage="Els cognoms son requerits." ToolTip="Els cognoms son requerits." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="DataLabel" runat="server" AssociatedControlID="Dia">Data de naixement:</asp:Label>
                                <asp:Label ID="DiaLabel" runat="server" AssociatedControlID="Dia">Dia:</asp:Label>
                                <asp:TextBox ID="Dia" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="DiaRequerit" ControlToValidate="Dia"
                                     CssClass="failureNotification" ErrorMessage="El dia es requerit." ToolTip="El dia es requerit."
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:RangeValidator Type="Integer" ID="RangDia" MinimumValue="1" MaximumValue="31" runat="server" ControlToValidate="Dia"
                                     ErrorMessage="El dia ha de ser del 1 al 31." ToolTip="El dia ha de ser del 1 al 31." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RangeValidator>

                                <asp:Label ID="MesLabel" runat="server" AssociatedControlID="Mes">Mes:</asp:Label>
                                <asp:TextBox ID="Mes" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator  runat="server" ID="MesRequerit" ControlToValidate="Mes"
                                     CssClass="failureNotification" ErrorMessage="El mes es requerit." ToolTip="El mes es requerit."
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:RangeValidator Type="Integer" ID="RangMes"  MinimumValue="1" MaximumValue="12" runat="server" ControlToValidate="Mes"
                                     ErrorMessage="El mes ha de ser del 1 al 12." ToolTip="El mes ha de ser del 1 al 12." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RangeValidator>

                                <asp:Label ID="AnyLabel" runat="server" AssociatedControlID="Any">Any:</asp:Label>
                                <asp:TextBox ID="Any" runat="server" CssClass="textEntry"></asp:TextBox>
                                <asp:RequiredFieldValidator  runat="server" ID="AnyRequerit" ControlToValidate="Any"
                                     CssClass="failureNotification" ErrorMessage="L'any es requerit." ToolTip="L'any es requerit."
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:RangeValidator Type="Integer" ID="RangAny"  MinimumValue="1900" MaximumValue="2010" runat="server" ControlToValidate="Any"
                                     ErrorMessage="L'any ha de ser del 1900 al 2010." ToolTip="L'any ha de ser del 1900 al 2010." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RangeValidator>
                            </p>
                            <p>
                                <asp:Label ID="FotoLabel" runat="server" AssociatedControlID="Foto">Foto:</asp:Label>
                                <asp:FileUpload ID="Foto" CssClass="textEntry" runat="server" />
                                <asp:RequiredFieldValidator ID="FotoRequerida" runat="server" ControlToValidate="Foto" 
                                     CssClass="failureNotification" ErrorMessage="La foto es requerida." ToolTip="La foto es requerida." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>                                     
                            </p>
                            <p>
                                <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Contrasenya:</asp:Label>
                                <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" 
                                     CssClass="failureNotification" ErrorMessage="La contrasenya es requerida." ToolTip="La contrasenya es requerida." 
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <asp:Label ID="ConfirmPasswordLabel" runat="server" AssociatedControlID="ConfirmPassword">Confirma la contrasenya:</asp:Label>
                                <asp:TextBox ID="ConfirmPassword" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="ConfirmPassword" CssClass="failureNotification" Display="Dynamic" 
                                     ErrorMessage="La confirma la contrasenya es requerida." ID="ConfirmPasswordRequired" runat="server" 
                                     ToolTip="La confirma la contrasenya es requerida." ValidationGroup="RegisterUserValidationGroup">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" 
                                     CssClass="failureNotification" Display="Dynamic" ErrorMessage="La contrasenya i la confirmació de la contrasenya han de coincidir."
                                     ValidationGroup="RegisterUserValidationGroup">*</asp:CompareValidator>
                            </p>
                        </fieldset>
                        <p class="submitButton">
                            <asp:Button ID="CreateUserButton" runat="server" CommandName="MoveNext" Text="Crear usuari" 
                                 ValidationGroup="RegisterUserValidationGroup"/>
                        </p>
                    </div>
                </ContentTemplate>
                <CustomNavigationTemplate>
                </CustomNavigationTemplate>
            </asp:CreateUserWizardStep>
        </WizardSteps>
    </asp:CreateUserWizard>
</asp:Content>
