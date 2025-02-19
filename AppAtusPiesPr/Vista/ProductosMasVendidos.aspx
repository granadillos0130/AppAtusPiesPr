<%@ Page Title="Productos Más Vendidos" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="ProductosMasVendidos.aspx.cs" Inherits="AppAtusPiesPr.Vista.ProductosMasVendidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/flatpickr/dist/flatpickr.min.css">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@10"></script>
        <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

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
        <h2 class="mb-4">Productos Vendidos</h2>

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
                
                <asp:BoundField DataField="NombreProducto" HeaderText="Nombre" />
                 <asp:TemplateField HeaderText="Imagen">
            <ItemTemplate>
                <asp:Image ID="imgProducto" runat="server" ImageUrl='<%# Eval("ImagenUrl") %>' Width="100px" Height="100px" />
            </ItemTemplate>
        </asp:TemplateField>
                <asp:BoundField DataField="CantidadVendida" HeaderText="Cantidad Vendida" />
                <asp:BoundField DataField="totalVentas" HeaderText="Total Ventas" DataFormatString="{0:C}" />
                <asp:BoundField DataField="marca" HeaderText="Marca" />
            </Columns>
        </asp:GridView>

        <asp:Label ID="lblMensaje" runat="server" Text="" CssClass="text-danger" Visible="False" />
    </div>

            <!-- Gráfica -->
        <div class="card mt-4">
            <div class="card-header">
                <h5 class="card-title">Gráfica de Productos Más Vendidos</h5>
            </div>
            <div class="card-body">
                <canvas id="myChart" width="400" height="200"></canvas>
            </div>
        </div>
    <br>
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

        <script>
            // Función para obtener datos del GridView
            function getDataFromGridView() {
                const rows = document.querySelectorAll("#<%= gvProductos.ClientID %> tr");
                const labels = [];
                const data = [];

                rows.forEach((row, index) => {
                    if (index > 0) { // Ignorar la fila de encabezado
                        const cells = row.querySelectorAll("td");
                        labels.push(cells[0].innerText); // Nombre del producto
                        data.push(parseInt(cells[2].innerText)); // Cantidad vendida
                    }
                });

                return { labels, data };
            }

            // Función para renderizar la gráfica
            function renderChart() {
                const { labels, data } = getDataFromGridView();

                const ctx = document.getElementById('myChart').getContext('2d');
                const myChart = new Chart(ctx, {
                    type: 'bar', // Tipo de gráfica
                    data: {
                        labels: labels,
                        datasets: [{
                            label: 'Cantidad Vendida',
                            data: data,
                            backgroundColor: 'rgba(75, 192, 192, 0.2)',
                            borderColor: 'rgba(75, 192, 192, 1)',
                            borderWidth: 1
                        }]
                    },
                    options: {
                        scales: {
                            y: {
                                beginAtZero: true
                            }
                        }
                    }
                });
            }

            // Llamar a la función para renderizar la gráfica después de la búsqueda
            document.addEventListener("DOMContentLoaded", renderChart);
        </script>

    <!-- Campos ocultos para enviar las fechas seleccionadas al backend -->
    <asp:HiddenField ID="hfFechaInicio" runat="server" />
    <asp:HiddenField ID="hfFechaFin" runat="server" />
</asp:Content>
