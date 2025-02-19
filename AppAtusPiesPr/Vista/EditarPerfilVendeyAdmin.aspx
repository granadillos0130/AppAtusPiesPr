<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="EditarPerfilVendeyAdmin.aspx.cs" Inherits="AppAtusPiesPr.Vista.EditarPerfilVendeyAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-group {
            margin-bottom: 15px;
        }

            .form-group label {
                font-weight: bold;
            }

            .form-group input, .form-group .form-control {
                border-radius: 5px;
                padding: 10px;
                border: 1px solid #ccc;
            }

        .container {
            max-width: 600px;
            margin: auto;
            background-color: #f9f9f9;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        .btn-primary {
            background-color: #007bff;
            border-color: #007bff;
            color: white;
            padding: 10px 20px;
            border-radius: 5px;
        }

        h2 {
            text-align: center;
            margin-bottom: 20px;
        }

        .img-thumbnail {
            display: block;
            margin: 10px auto;
            border-radius: 50%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="container">
        <h2>Editar Perfil</h2>
        <!-- Foto actual del perfil -->
        <div class="form-group text-center">
            <asp:Image ID="imgFotoPerfil" runat="server" CssClass="img-thumbnail" Width="150px" Height="150px" />
        </div>
        <!-- Campo para subir nueva foto -->
        <div class="form-group" id="gpCambiarFoto" runat="server">
            <label for="fileUploadFoto">Cambiar Foto:</label>
            <asp:FileUpload ID="fileUploadFoto" runat="server" CssClass="form-control" />
        </div>
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
        <div class="form-group" id="gpTelefono" runat="server">
            <label for="txtTelefono" id="lbltel">Teléfono:</label>
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group" id="gpDireccion" runat="server">
            <label id="lbldireccion" for="txtDireccion">Dirección:</label>
            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div class="form-group" id="gpDescripcion" runat="server">
            <label id="lbDes" for="txtDescripcion">Descripción:</label>
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
