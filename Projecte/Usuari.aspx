﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Usuari.aspx.cs" Inherits="Usuari" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:PlaceHolder ID="phDadesUsuari" runat="server"></asp:PlaceHolder>
    <asp:PlaceHolder ID="phContactes" runat="server"></asp:PlaceHolder>
    <div class="clear"></div>
    <asp:PlaceHolder ID="phPeliculesVistesVeure" runat="server"></asp:PlaceHolder>
    <div class="clear"></div>   
    <asp:PlaceHolder ID="phComentaris" runat="server"></asp:PlaceHolder>
</asp:Content>
