<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="EditarPerfilVendeyAdmin.aspx.cs" Inherits="AppAtusPiesPr.Vista.EditarPerfilVendeyAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="container">
        <h2>Editar Perfil</h2>
        <div class="form-group">
            <label for="txtNombres">Nombres:</label>
            <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtApellidos">Apellidos:</label>
            <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtEmail">Email:</label>
            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtTelefono">Teléfono:</label>
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label id="lbldireccion" for="txtDireccion">Dirección:</label>
            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label ID="lbDes" for="txtDescripcion">Descripción:</label>
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtContrasenaActual">Contraseña Actual:</label>
            <asp:TextBox ID="txtContrasenaActual" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="txtNuevaContrasena">Nueva Contraseña:</label>
            <asp:TextBox ID="txtNuevaContrasena" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
        </div>
        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
        <div class="form-group">
            <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary" Text="Guardar" OnClick="btnGuardar_Click" />
        </div>
    </div>
</asp:Content>
