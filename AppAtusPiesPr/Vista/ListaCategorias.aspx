<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="ListaCategorias.aspx.cs" Inherits="AppAtusPiesPr.Vista.ListaCategorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Enlace a Bootstrap -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" rel="stylesheet">
    
    <!-- Estilos personalizados -->
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
    <div class="container mt-4">
        <h2>Lista de Categorías</h2>

        <asp:GridView ID="gvCategorias" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" 
            OnRowCommand="gvCategorias_RowCommand">
            <Columns>
                <asp:BoundField DataField="descripcion" HeaderText="Descripción" SortExpression="descripcion" />
                <asp:BoundField DataField="idCategoria" HeaderText="idCategoria" SortExpression="idCategoria" Visible="false" />
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <a href="ActualizarCategoria.aspx?id=<%# Eval("idCategoria") %>" class="btn btn-warning btn-sm">Actualizar</a>
                        <a href="EliminarCategoria.aspx?id=<%# Eval("idCategoria") %>" class="btn btn-danger btn-sm" 
                            onclick="return confirm('¿Estás seguro de que deseas eliminar esta categoría?');">Eliminar</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
