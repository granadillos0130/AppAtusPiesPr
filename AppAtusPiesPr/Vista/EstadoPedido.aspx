<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="EstadoPedido.aspx.cs" Inherits="AppAtusPiesPr.Vista.EstadoPedido" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" />
      <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="container" style="margin-top:0">
        <h2 class="text-center mb-6">Estado Pedidos</h2>
        
       
        <div class="row mb-4">
            <div class="col-md-4">
                <label for="ddlEstado" class="form-label">Seleccionar Estado:</label>
                <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Pendiente" Value="Pendiente"></asp:ListItem>
                    <asp:ListItem Text="Entregado" Value="Entregado"></asp:ListItem>
                    <asp:ListItem Text="Procesado" Value="Enviado"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-2 align-self-end">
                <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" CssClass="btn btn-primary w-100" OnClick="btnFiltrar_Click" />
            </div>
        </div>

      
        <div class="table-responsive">
            <asp:GridView ID="gvPedidos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                <Columns>
                    <asp:BoundField DataField="nombreProducto" HeaderText="Nombre Producto" SortExpression="nombreProducto" />
                    <asp:BoundField DataField="cantidadStock" HeaderText="Cantidad Stock" SortExpression="cantidadStock" />
                    <asp:BoundField DataField="descripcionProducto" HeaderText="Descripción Producto" SortExpression="descripcionProducto" />
                    <asp:BoundField DataField="fechaPedido" HeaderText="Fecha Pedido" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="estado" HeaderText="Estado Pedido"  SortExpression="estadoProducto"/>

                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
