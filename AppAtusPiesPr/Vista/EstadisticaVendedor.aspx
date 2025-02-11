<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="EstadisticaVendedor.aspx.cs" Inherits="AppAtusPiesPr.Vista.EstadisticaVendedor" %>

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
        <h2 class="text-center mb-6">Listado de Vendedores</h2>

        <!-- Formulario de Búsqueda -->
        <div class="mb-3">
            <div class="row">

                <!-- Campo de Documento -->
                <div class="col-md-3">
                    <label for="txtDocumento" class="form-label">Documento</label>
                    <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" />
                </div>

                <!-- Dropdown de Año -->
                <div class="col-md-2">
                    <label for="ddlAño" class="form-label">Año</label>
                    <asp:DropDownList ID="ddlAño" runat="server" CssClass="form-control" class="form-label">
                        <asp:ListItem Text="Seleccione un Año" Value="" />
                        <asp:ListItem Text="2024" Value="2024" />
                        <asp:ListItem Text="2023" Value="2023" />
                        <asp:ListItem Text="2022" Value="2022" />
                    </asp:DropDownList>
                </div>

                <!-- Dropdown de Mes -->
                <div class="col-md-2">
                    <label for="ddlMes" class="form-label">Mes</label>
                    <asp:DropDownList ID="ddlMes" runat="server"  CssClass="form-control" class="form-label">
                        <asp:ListItem Text="Seleccione un Mes" Value="" />
                        <asp:ListItem Text="Enero" Value="1" />
                        <asp:ListItem Text="Febrero" Value="2" />
                        <asp:ListItem Text="Marzo" Value="3" />
                        <asp:ListItem Text="Abril" Value="4" />
                        <asp:ListItem Text="Mayo" Value="5" />
                        <asp:ListItem Text="Junio" Value="6" />
                        <asp:ListItem Text="Julio" Value="7" />
                        <asp:ListItem Text="Agosto" Value="8" />
                        <asp:ListItem Text="Septiembre" Value="9" />
                        <asp:ListItem Text="Octubre" Value="10" />
                        <asp:ListItem Text="Noviembre" Value="11" />
                        <asp:ListItem Text="Diciembre" Value="12" />
                    </asp:DropDownList>
                </div>

                <!-- Botón de búsqueda -->
                <div class="col-md-2">
                    <label>&nbsp;</label><br />
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
                </div>
            </div>
        </div>

        <!-- Resultados -->
        <div class="card mt-4">
            <div class="card-body">
                <asp:GridView ID="gvEstadisticasVendedor" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" EmptyDataText="No se encontraron resultados">
                    <Columns>
                        <asp:BoundField DataField="Documento" HeaderText="Documento Vendedor" SortExpression="Documento" />
                        <asp:BoundField DataField="Nombres" HeaderText="Nombre" SortExpression="Nombres" />
                        <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" SortExpression="Apellidos" />
                        <asp:BoundField DataField="Año" HeaderText="Año" SortExpression="Año" />
                        <asp:BoundField DataField="Mes" HeaderText="Mes" SortExpression="Mes" />
                        <asp:BoundField DataField="TotalClientes" HeaderText="Total Clientes" SortExpression="TotalClientes" />
                    </Columns>
                </asp:GridView>

            </div>
        </div>
    </div>
</asp:Content>
