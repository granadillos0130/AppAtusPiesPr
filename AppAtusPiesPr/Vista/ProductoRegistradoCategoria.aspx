<%@ Page Title="Productos por Categoría" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="ProductoRegistradoCategoria.aspx.cs" Inherits="AppAtusPiesPr.Vista.ProductoRegistradoCategoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body" runat="server">
    <h2>Productos Registrados por Categoría</h2>
    <asp:DropDownList ID="ddlCategorias" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategorias_SelectedIndexChanged">
        <asp:ListItem Text="Todas las categorías" Value="0" />
 
    </asp:DropDownList>

    <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="idProducto" HeaderText="ID Producto" />
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="CantidadStock" HeaderText="Stock" />
            <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
            <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
            <asp:BoundField DataField="Categoria" HeaderText="Categoría" />
            <asp:BoundField DataField="Marca" HeaderText="Marca" />
            <asp:BoundField DataField="NombreVendedor" HeaderText="Vendedor" />
        </Columns>
    </asp:GridView>
</asp:Content>
