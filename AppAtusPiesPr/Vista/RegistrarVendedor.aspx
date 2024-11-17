<%@ Page Title="Registrar Vendedor" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="RegistrarVendedor.aspx.cs" Inherits="AppAtusPiesPr.Vista.RegistrarVendedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Registrar Vendedor</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <asp:Panel ID="pnlRegistrarVendedor" runat="server">
        <h2>Registrar Vendedor</h2>
        <form id="formRegistrar" runat="server">
            <table>
                <tr>
                    <td><asp:Label ID="lblNombres" runat="server" Text="Nombres:" /></td>
                    <td><asp:TextBox ID="txtNombres" runat="server" CssClass="form-control" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblApellidos" runat="server" Text="Apellidos:" /></td>
                    <td><asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblDocumento" runat="server" Text="Documento:" /></td>
                    <td><asp:TextBox ID="txtDocumento" runat="server" CssClass="form-control" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblCorreo" runat="server" Text="Correo Electrónico:" /></td>
                    <td><asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" TextMode="Email" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblPassword" runat="server" Text="Contraseña:" /></td>
                    <td><asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblTelefono" runat="server" Text="Teléfono:" /></td>
                    <td><asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblDireccion" runat="server" Text="Dirección:" /></td>
                    <td><asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" /></td>
                </tr>
                <tr>
                    <td><asp:Label ID="lblDescripcion" runat="server" Text="Descripción:" /></td>
                    <td><asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" /></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center;">
                        <asp:Button ID="btnRegistrar" runat="server" Text="Registrar Vendedor" CssClass="btn btn-primary" OnClick="btnRegistrar_Click" />
                    </td>
                </tr>
            </table>
        </form>
    </asp:Panel>
</asp:Content>
