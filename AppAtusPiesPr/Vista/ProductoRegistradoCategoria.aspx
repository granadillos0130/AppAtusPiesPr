<%@ Page Title="Productos por Categoría" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="ProductoRegistradoCategoria.aspx.cs" Inherits="AppAtusPiesPr.Vista.ProductoRegistradoCategoria" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="container" style="margin-top:0"><br />

    
    <h2 class="text-left mb-6">Productos Registrados por Categoría</h2>
    
    
    <div class="row mb-4">
        <div class="col-md-4">
    
     <asp:DropDownList ID="ddlCategorias" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategorias_SelectedIndexChanged" CssClass="form-control" class="form-label">
        <asp:ListItem Text="Todas las categorías" Value="0" />
 
    </asp:DropDownList>
            </div>
        </div>

        <div class="table-responsive">

    <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped mt-3">
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
            
        </div>
        </div>
</asp:Content>
