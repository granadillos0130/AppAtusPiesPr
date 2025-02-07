<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="EstadisticaCategoria.aspx.cs" Inherits="AppAtusPiesPr.Vista.EstadisticaCategoria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.5.0/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

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
        <h2 class="text-center mb-6">Estadísticas de Categorías</h2>

        <div class="mb-3">
            <label for="ddlOrden" class="form-label">Ordenar por:</label>
            <asp:DropDownList ID="ddlOrden" runat="server" class="form-control">
    <asp:ListItem Text="Ascendente" Value="ASC" />
    <asp:ListItem Text="Descendente" Value="DESC" />
</asp:DropDownList>
<asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="false"></asp:Label>

            <asp:Button ID="btnFiltrar" runat="server" Text="Aplicar Filtro" CssClass="btn btn-primary ml-2" OnClick="btnFiltrar_Click" />
        </div>

        <div class="card mt-4">
            <div class="card-body">
                <asp:GridView ID="gvEstadisticasCategoria" runat="server" AutoGenerateColumns="false" CssClass="table table-striped" EmptyDataText="No se encontraron resultados">
                    <Columns>
                        <asp:BoundField DataField="descripcion" HeaderText="Nombre Categoría" />
                        <asp:BoundField DataField="TotalProductos" HeaderText="Total Productos" SortExpression="TotalProductos" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
