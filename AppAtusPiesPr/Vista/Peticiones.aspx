﻿<%@ Page Title="Solicitudes" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="Peticiones.aspx.cs" Inherits="AppAtusPiesPr.Vista.Peticiones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <!-- Añadir CSS directamente en el archivo -->
    <style>
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
        <h2 class="mb-4">Solicitudes Pendientes</h2>
        <asp:Label ID="lblError" runat="server" CssClass="text-danger"></asp:Label>

        <asp:GridView ID="gvSolicitudes" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
            <Columns>
                <asp:BoundField DataField="idVendedor" HeaderText="ID Vendedor" Visible="false" />
                <asp:BoundField DataField="Documento" HeaderText="Documento" />
                <asp:BoundField DataField="Nombres" HeaderText="Nombres" />
                <asp:BoundField DataField="Apellidos" HeaderText="Apellidos" />
                <asp:BoundField DataField="Email" HeaderText="Email" />
                <asp:BoundField DataField="Telefono" HeaderText="Teléfono" />
                <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" CssClass="btn btn-primary btn-sm"
                            CommandArgument='<%# Eval("idVendedor") %>' OnCommand="btnAceptar_Command" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Acciones">
                    <ItemTemplate>
                        <asp:Button ID="btnDenegar" runat="server" Text="Denegar" CssClass="btn btn-danger btn-sm"
                            CommandArgument='<%# Eval("idVendedor") %>' OnCommand="btnDenegar_Command" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
