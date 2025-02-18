<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="EstadisticaCategoria.aspx.cs" Inherits="AppAtusPiesPr.Vista.EstadisticaCategoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.0/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>

    <style>
        /* Estilo para la tabla */
        .table th {
            background-color: #007bff;
            color: white;
        }

        .table-striped tbody tr:nth-child(odd) {
            background-color: #f2f2f2;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="container">
        <h2 class="text-center mb-6" style="margin-top:10px;">Estadísticas de Categorías</h2>

        <div class="card mt-4">
            <div class="card-header">
                <h5 class="card-title">Gráfica de Productos por Categoría</h5>
            </div>
            <div class="card-body">
                <canvas id="myChart" width="400" height="200"></canvas>
            </div>
        </div>

        <div class="mb-3">
            <label for="ddlOrden" class="form-label">Ordenar por:</label>
            <asp:DropDownList ID="ddlOrden" runat="server" class="form-control">
                <asp:ListItem Text="Ascendente" Value="ASC" />
                <asp:ListItem Text="Descendente" Value="DESC" />
            </asp:DropDownList>
            <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>
            <br />


            <asp:Button ID="btnFiltrar" runat="server" Text="Aplicar Filtro" CssClass="btn btn-primary ml-2" OnClick="btnFiltrar_Click" />
        </div>

        <div class="card mt-4" >
            <div class="card-body" style="border-radius:5px;">
                <asp:GridView ID="gvEstadisticasCategoria" runat="server" AutoGenerateColumns="false" CssClass="table table-striped" EmptyDataText="No se encontraron resultados" GridLines="Both">
                    <HeaderStyle BackColor="#007bff" ForeColor="White" BorderColor="black" BorderWidth="2px" />
                    <RowStyle BorderColor="Black" BorderWidth="1px" />
                    <AlternatingRowStyle BackColor="#f2f2f2" />
                    <Columns>
                        <asp:BoundField DataField="descripcion" HeaderText="Nombre Categoría" />
                        <asp:BoundField DataField="TotalProductos" HeaderText="Total Productos" SortExpression="TotalProductos" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

     <script>
         // Función para obtener datos del GridView
         function getDataFromGridView() {
             const rows = document.querySelectorAll("#<%= gvEstadisticasCategoria.ClientID %> tr");
             const labels = [];
             const data = [];

             rows.forEach((row, index) => {
                 if (index > 0) { // Ignorar la fila de encabezado
                     const cells = row.querySelectorAll("td");
                     labels.push(cells[0].innerText); // Nombre de la categoría
                     data.push(parseInt(cells[1].innerText)); // Total de productos
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
                         label: 'Total de Productos',
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

         // Llamar a la función para renderizar la gráfica
         document.addEventListener("DOMContentLoaded", renderChart);
     </script>

</asp:Content>
