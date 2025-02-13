<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="Pruebaa.aspx.cs" Inherits="AppAtusPiesPr.Vista.Pruebaa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="container mt-4">
        <h2>Seleccione un Producto</h2>
        <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-control"></asp:DropDownList>
    </div>
</asp:Content>
