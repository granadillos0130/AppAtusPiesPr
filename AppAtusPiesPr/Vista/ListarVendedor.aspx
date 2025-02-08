<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="ListarVendedor.aspx.cs" Inherits="AppAtusPiesPr.Vista.ListarVendedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <style>
    /* Estilo para la tabla */
    .table th {
        background-color: #007bff;
        color: white;
    }
    .table-striped tbody tr:nth-child(odd) {
        background-color: #f2f2f2;
    }
    /* Cambiar el color del texto de las celdas a negro */
    .table-striped tbody td {
        color: black;
    }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="container">
        <h2 class="text-center mb-6">Listado de Vendedores</h2>

        <!-- Formulario de Búsqueda -->
        <div class="mb-3">
            <div class="row">
                <div class="col-md-4">
                    <label for="ddlEstado" class="form-label">Estado</label>
                    <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Seleccione un Estado" Value="" />
                        <asp:ListItem Text="Activo" Value="Activo" />
                        <asp:ListItem Text="Inactivo" Value="Inactivo" />
                        <asp:ListItem Text="Proceso" Value="Proceso" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-4">
                    <label for="txtDocumento" class="form-label">Documento</label>
                    <asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" />
                </div>

                <div class="col-md-4">
                    <label>&nbsp;</label><br />
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
                </div>
            </div>
        </div>

        <!-- GridView para mostrar los vendedores -->
        <div class="table-responsive">
            <asp:GridView ID="gvVendedores" runat="server" CssClass="table table-striped table-bordered table-sm" 
                AutoGenerateColumns="False" OnRowDataBound="gvVendedores_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="idUsuario" HeaderText="idVendedor" SortExpression="idVendedor" ItemStyle-Width="120px" />
                    <asp:BoundField DataField="nombres" HeaderText="Nombres" SortExpression="Nombres" ItemStyle-Width="180px" />
                    <asp:BoundField DataField="apellidos" HeaderText="Apellidos" SortExpression="Apellidos" ItemStyle-Width="180px" />
                    <asp:BoundField DataField="documento" HeaderText="Documento" SortExpression="Documento" ItemStyle-Width="160px" />
                    <asp:BoundField DataField="email" HeaderText="Email" SortExpression="Email" ItemStyle-Height="75" ItemStyle-Width="220px" />
                    <asp:BoundField DataField="telefono" HeaderText="Teléfono" SortExpression="Telefono" ItemStyle-Width="150px" />
                    <asp:BoundField DataField="direccion" HeaderText="Dirección" SortExpression="Direccion" ItemStyle-Width="250px" />
                    <asp:BoundField DataField="estado" HeaderText="Estado" SortExpression="Estado" ItemStyle-Width="120px" />
                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:Button ID="btnInactivar" runat="server" Text="Inactivar" CssClass="btn btn-primary btn-sm"
                                CommandArgument='<%# Eval("idUsuario")%>' OnCommand="btnInactivar_Command" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
