﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="agregarMarca.aspx.cs" Inherits="AppAtusPiesPr.Vista.agregarMarca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Incluyendo SweetAlert -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="container mt-4">
        <h2>Agregar Nueva Marca</h2>
        <div class="mb-3">
            <label for="txtNombre" class="form-label">Nombre Marca</label>
             <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ingresa el nombre" required></asp:TextBox>
            <label for="txtDescripcion" class="form-label">Descripción</label>
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" placeholder="Ingresa una descripción" TextMode="MultiLine" Rows="4" required></asp:TextBox>
        </div>

        <!-- Botón para enviar los datos -->
        <button type="submit" class="btn btn-primary" id="btnAgregarMarca" runat="server" onserverclick="btnAgregarCategoria_ServerClick">Agregar Marca</button>
    </div>

    <!-- Incluyendo los scripts de Bootstrap -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>
