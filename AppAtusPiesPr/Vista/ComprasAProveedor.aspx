<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="ComprasAProveedor.aspx.cs" Inherits="AppAtusPiesPr.Vista.ComprasAProveedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="container mt-5">
        <h2 class="text-center mb-4">Registrar Compra</h2>

        <!-- Selección de Proveedor -->
        <div class="mb-3">
            <label for="ddlProveedores" class="form-label">Seleccione un Proveedor</label>
            <asp:DropDownList ID="ddlProveedores" runat="server" CssClass="form-control">
                <asp:ListItem Text="Seleccione un proveedor" Value="0" />
            </asp:DropDownList>
        </div>
 <!-- Número de Factura -->
        <div class="mb-3">
            <label for="txtNumeroFactura" class="form-label">Número de Factura</label>
            <asp:TextBox ID="txtNumeroFactura" runat="server" CssClass="form-control" placeholder="Ingrese número de factura" />
        </div>
         <!-- Total de la Compra -->
        <div class="mb-3">
            <label for="txtTotalCompra" class="form-label">Total de la Compra</label>
            <asp:TextBox ID="txtTotalCompra" runat="server" CssClass="form-control" TextMode="Number" placeholder="Ingrese el total de la compra" />
        </div>

         <!-- Botón Guardar -->
        <div class="text-center">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar Compra" CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
        </div>

         <!-- Mensaje de alerta -->
        <script type="text/javascript">
            function mostrarAlerta(tipo, mensaje) {
                Swal.fire({
                    icon: tipo,
                    title: mensaje,
                    showConfirmButton: false,
                    timer: 2500
                });
            }
        </script>
    </div>
</asp:Content>

