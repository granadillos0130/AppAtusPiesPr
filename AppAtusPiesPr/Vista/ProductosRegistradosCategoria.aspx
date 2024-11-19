<%@ Page Title="Productos Registrados por Categoría" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="ProductosRegistradosCategoria.aspx.cs" Inherits="AppAtusPiesPr.Vista.ProductosRegistradosCategoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <form id="form1" runat="server">
        <h2>Productos por Categoría</h2>
        <div>
            <asp:Label ID="lblCategoria" runat="server" Text="Categoría:"></asp:Label>
            <asp:DropDownList ID="ddlCategoria" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged">
                <asp:ListItem Text="Seleccione una categoría" Value="" />
            </asp:DropDownList>
        </div>
        <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Codigo" HeaderText="Código" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre Producto" />
                <asp:BoundField DataField="CantidadStock" HeaderText="Cantidad Disponible" />
                <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Presentacion" HeaderText="Presentación" />
                <asp:BoundField DataField="Estado" HeaderText="Estado" />
                <asp:BoundField DataField="Descripcion" HeaderText="Categoría" />
            </Columns>
        </asp:GridView>
    </form>
</asp:Content>
