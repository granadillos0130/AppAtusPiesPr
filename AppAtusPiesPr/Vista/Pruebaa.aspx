﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="Pruebaa.aspx.cs" Inherits="AppAtusPiesPr.Vista.Pruebaa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

      <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css">
  <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

</asp:Content>





<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="container mt-4">
        <h2>Seleccione un Producto</h2>
        <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-control"></asp:DropDownList>
    </div>

        <div class="mb-3">
        <label for="txtNombre" class="form-label">Nombre del Producto</label>
        <div class="input-group">
            <span class="input-group-text">🏷️ </span>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"
                placeholder="Ingrese nombre del producto"></asp:TextBox>
        </div>
    </div>

    <div class="mb-3">
        <label for="txtStock" class="form-label">Cantidad en Stock</label>
        <div class="input-group">
            <span class="input-group-text">📦</span>
            <asp:TextBox ID="txtStock" runat="server" CssClass="form-control"
                TextMode="Number" placeholder="Ingrese cantidad"></asp:TextBox>
        </div>
    </div>


    <div class="mb-3">
        <label for="txtPrecio" class="form-label">Precio</label>
        <div class="input-group">
            <span class="input-group-text">💲</span>
            <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" />
        </div>
    </div>
    <br />

    <div class="mb-3">
        <label for="txtDescripcionProduc" class="form-label">Descripción del Producto</label>
        <div class="input-group">
            <span class="input-group-text">✍️</span>
            <asp:TextBox ID="txtDescripcionProduc" runat="server" CssClass="form-control" TextMode="MultiLine" />
        </div>
    </div>


    <div class="mb-3">
        <label for="txtReferencia" class="form-label">Referencia</label>
        <div class="input-group">
            <span class="input-group-text">🔢</span>
            <asp:TextBox ID="txtReferencia" runat="server" CssClass="form-control" />
        </div>
    </div>
    <br />



    <div class="mb-3">
        <label for="txtEstado" class="form-label">Estado</label>
        <asp:DropDownList ID="txtEstado" runat="server" CssClass="form-control" class="form-label">

            <asp:ListItem Value="disponible">Disponible</asp:ListItem>
            <asp:ListItem Value="no disponible">No Disponible</asp:ListItem>
        </asp:DropDownList>
    </div>

    <div class="mb-3">
        <label for="txtMarca" class="form-label">Marca</label>
        <asp:DropDownList ID="txtMarca" runat="server" CssClass="form-control" class="form-label">
            <asp:ListItem Value="0">Seleccione una Marca</asp:ListItem>
            <asp:ListItem Value="Nike">Nike</asp:ListItem>
            <asp:ListItem Value="Adidas">Adidas</asp:ListItem>
            <asp:ListItem Value="Reebok">Reebok</asp:ListItem>
            <asp:ListItem Value="Puma">Puma</asp:ListItem>
            <asp:ListItem Value="New Balance">New Balance</asp:ListItem>
            <asp:ListItem Value="Under Armour">Under Armour</asp:ListItem>
            <asp:ListItem Value="Asics">Asics</asp:ListItem>
            <asp:ListItem Value="Saucony">Saucony</asp:ListItem>
            <asp:ListItem Value="Hoka">Hoka</asp:ListItem>
            <asp:ListItem Value="Mizuno">Mizuno</asp:ListItem>
        </asp:DropDownList>
    </div>


    <div class="mb-3">
        <label for="txtDescuento" class="form-label">Descuento</label>
        <div class="input-group">
            <span class="input-group-text">🔖</span>
            <asp:TextBox ID="txtDescuento" runat="server" CssClass="form-control" />
        </div>
    </div>

    <div class="mb-3">
        <label for="txtCategoria" class="form-label">Categoría</label>
        <asp:DropDownList ID="txtCategoria" runat="server" CssClass="form-control" class="form-label">
            <asp:ListItem Value="">Seleccione un Tipo de calzado</asp:ListItem>
            <asp:ListItem Value="deportivo">Calzado deportivo</asp:ListItem>
            <asp:ListItem Value="casual">Calzado casual</asp:ListItem>
            <asp:ListItem Value="tecnico">Calzado técnico</asp:ListItem>
            <asp:ListItem Value="correr">Calzado para correr</asp:ListItem>
            <asp:ListItem Value="crossfit">Calzado para crossfit</asp:ListItem>
        </asp:DropDownList>
    </div>


    <div class="mb-3">
        <label for="inRuta" class="form-label">Añade una imagen</label><br />
        <asp:FileUpload ID="inRuta" runat="server" />

    </div>


<div class="card-footer text-center">
    <asp:Button ID="btnActualizar" runat="server" Text="Actualizar Producto"
        CssClass="btn btn-primary" OnClick="btnActualizar_Click1" />
</div>

</asp:Content>
