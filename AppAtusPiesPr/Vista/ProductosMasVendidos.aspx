<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="ProductosMasVendidos.aspx.cs" Inherits="AppAtusPiesPr.Vista.ProductosMasVendidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <form id="form1" runat="server">
        <div class="container" style="margin-top=0">
        <h1 class="text-center mb-4">Productos más vendidos </h1>
        <div class="table-responsive">
       
             
        <div>
            <asp:Label ID="Label1" runat="server" Text="Fecha de Inicio:"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Label ID="Label2" runat="server" Text="Fecha de Fin:"></asp:Label>
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
            <asp:Label ID="Label3" runat="server" Text="" ForeColor="Red" Visible="False"></asp:Label>
        </div>
            
            <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered">
    <Columns>
        <asp:BoundField DataField="idProducto" HeaderText="ID Producto" />
        <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
        <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
        <asp:BoundField DataField="CantidadVendida" HeaderText="Cantidad Vendida" />
        <asp:BoundField DataField="TotalVentas" HeaderText="Total Ventas" DataFormatString="{0:C}" />
        <asp:BoundField DataField="Marca" HeaderText="Marca" />
    </Columns>
</asp:GridView>

       
        
        
        <div>
            <asp:Label ID="lblFechaInicio" runat="server" Text="Fecha de Inicio:"></asp:Label>
            <asp:TextBox ID="txtFechaInicio" runat="server"></asp:TextBox>
            <asp:Label ID="lblFechaFin" runat="server" Text="Fecha de Fin:"></asp:Label>
            <asp:TextBox ID="txtFechaFin" runat="server"></asp:TextBox>
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
            <asp:Label ID="lblMensaje" runat="server" Text="" ForeColor="Red" Visible="False"></asp:Label>
        </div>
        
             </div>
             </div>
    </form>
</asp:Content>
