<%@ Page Title="Productos Más Vendidos" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="ProductosMasVendidos.aspx.cs" Inherits="AppAtusPiesPr.Vista.ProductosMasVendidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
    <style>
        .flatpickr-wrapper {
            margin-bottom: 15px;
        }

        .btn-calendar {
            display: inline-block;
            padding: 10px 20px;
            font-size: 14px;
            color: white;
            background-color: #007bff;
            border-radius: 5px;
            border: none;
            cursor: pointer;
            margin-top: 5px;
        }

        .btn-calendar:hover {
            background-color: #0056b3;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <div class="container mt-4">
        <h2 class="mb-4">Productos Más Vendidos</h2>

        <div class="row">
            <!-- Fecha de Inicio -->
            <div class="col-md-6">
                <div class="flatpickr-wrapper">
                    <asp:Label ID="lblFechaInicio" runat="server" Text="Fecha de Inicio:" CssClass="mr-2" />
                    <input type="text" id="fechaInicio" class="form-control" placeholder="Seleccionar Fecha de Inicio" />
                </div>
            </div>

            <!-- Fecha de Fin -->
            <div class="col-md-6">
                <div class="flatpickr-wrapper">
                    <asp:Label ID="lblFechaFin" runat="server" Text="Fecha de Fin:" CssClass="mr-2" />
                    <input type="text" id="fechaFin" class="form-control" placeholder="Seleccionar Fecha de Fin" />
                </div>
            </div>
        </div>
        
        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary mt-3" OnClick="btnBuscar_Click" />

        <asp:GridView ID="gvProductos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped mt-3">
            <Columns>
                <asp:BoundField DataField="IdProducto" HeaderText="ID Producto" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                <asp:BoundField DataField="CantidadVendida" HeaderText="Cantidad Vendida" />
                <asp:BoundField DataField="TotalVentas" HeaderText="Total Ventas" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Marca" HeaderText="Marca" />
            </Columns>
        </asp:GridView>

        <asp:Label ID="lblMensaje" runat="server" Text="" CssClass="text-danger" Visible="False" />
    </div>

    <script src="https://cdn.jsdelivr.net/npm/flatpickr"></script>
    <script src="https://cdn.jsdelivr.net/npm/flatpickr/dist/l10n/es.js"></script>
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            // Configuración de Flatpickr con idioma en español
            flatpickr("#fechaInicio", {
                locale: "es",
                dateFormat: "Y-m-d",
                defaultDate: new Date(),
                onChange: function (selectedDates, dateStr) {
                    console.log("Fecha de inicio seleccionada: " + dateStr);
                    document.getElementById('<%= hfFechaInicio.ClientID %>').value = dateStr;
                }
            });

            flatpickr("#fechaFin", {
                locale: "es",
                dateFormat: "Y-m-d",
                defaultDate: new Date(),
                onChange: function (selectedDates, dateStr) {
                    console.log("Fecha de fin seleccionada: " + dateStr);
                    document.getElementById('<%= hfFechaFin.ClientID %>').value = dateStr;
                }
            });
        });
    </script>

    <!-- Campos ocultos para enviar las fechas seleccionadas al backend -->
    <asp:HiddenField ID="hfFechaInicio" runat="server" />
    <asp:HiddenField ID="hfFechaFin" runat="server" />
</asp:Content>
