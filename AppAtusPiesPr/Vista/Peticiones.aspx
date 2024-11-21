<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="Peticiones.aspx.cs" Inherits="AppAtusPiesPr.Vista.Peticiones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <form id="formPeticiones" runat="server">
        <div class="container mt-4">
            <h2 class="mb-4">Solicitudes Pendientes</h2>

            <asp:Label ID="lblError" runat="server" CssClass="text-danger"></asp:Label>

            <asp:Repeater ID="rptSolicitudes" runat="server">
                <ItemTemplate>
                    <div class="card mb-3 shadow-sm" style="width: 100%;">
                        <div class="card-body">
                            <h5 class="card-title">
                                <%# Eval("Nombres") %> <%# Eval("Apellidos") %>
                            </h5>
                            <p class="card-text">
                                <strong>Email:</strong> <%# Eval("Email") %><br />
                                <strong>Documento:</strong> <%# Eval("Documento") %>
                            </p>
                            <asp:Button ID="btnVerInformacion" runat="server" Text="Ver Información"
                                CssClass="btn btn-primary"
                                CommandArgument='<%# Eval("idVendedor") %>'
                                OnCommand="btnVerInformacion_Command" />
                            <br />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </form>
</asp:Content>
