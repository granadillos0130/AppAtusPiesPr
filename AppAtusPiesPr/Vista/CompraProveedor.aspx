<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="CompraProveedor.aspx.cs" Inherits="AppAtusPiesPr.Vista.CompraProveedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/FormRegistro.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="container mt-4">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div class="card">
            <div class="card-header bg-primary text-white">
                <h3 class="mb-0">Registro de Compra</h3>
            </div>

            <div class="card-body">
                <!-- Selector de Proveedor -->
                <div class="mb-3">
                    <label for="ddlProveedores" class="form-label">Seleccione un Proveedor</label>
                    <div class="input-group">
                        <span class="input-group-text">👤</span>
                        <asp:DropDownList ID="ddlProveedores" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Seleccione un proveedor" Value="0" />
                        </asp:DropDownList>
                    </div>
                </div>

                <!-- Número de Factura -->
                <div class="mb-3">
                    <label for="txtNumeroFactura" class="form-label">Número de Factura</label>
                    <div class="input-group">
                        <span class="input-group-text">📄</span>
                        <asp:TextBox ID="txtNumeroFactura" runat="server" CssClass="form-control"
                            placeholder="Ingrese número de factura"></asp:TextBox>
                    </div>
                </div>

                <!-- Total de Compra -->
                <div class="mb-3">
                    <label for="txtTotalCompra" class="form-label">Total de Compra</label>
                    <div class="input-group">
                        <span class="input-group-text">💲</span>
                        <asp:TextBox ID="txtTotalCompra" runat="server" CssClass="form-control"
                            TextMode="Number" step="0.01" min="0"
                            placeholder="Ingrese el total de la compra"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="card-footer text-center">
                <asp:Button ID="btnGuardar" runat="server" Text="Guardar Compra" 
                    CssClass="btn btn-primary" OnClick="btnGuardar_Click" />
            </div>
        </div>
    </div>
</asp:Content>