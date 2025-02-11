<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="EditarPerfilVendeyAdmin.aspx.cs" Inherits="AppAtusPiesPr.Vista.EditarPerfilVendeyAdmin" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
     /* Estilos generales */
     body {
         font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
         background-color: #f4f4f9;
         color: #333;
         margin: 0;
         padding: 0;
     }

     .container {
         max-width: 800px; /* Aumenté el ancho para acomodar la cuadrícula */
         margin: 50px auto;
         padding: 20px;
         background-color: #fff;
         border-radius: 10px;
         box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
     }

     h2 {
         text-align: left; /* Alinea el título a la izquierda */
         color: #4a90e2;
         margin-bottom: 20px;
         font-size: 24px;
     }

     .form-grid {
         display: flex;
         flex-wrap: wrap;
         gap: 20px;
     }

     .form-group {
         flex: 1 1 calc(50% - 20px); /* 2 columnas con espacio entre ellas */
         box-sizing: border-box;
     }

     .form-group label {
         display: block;
         margin-bottom: 5px;
         font-weight: bold;
         color: #555;
     }

     .form-group .form-control {
         width: 100%;
         padding: 10px;
         border: 1px solid #ddd;
         border-radius: 5px;
         font-size: 14px;
         background-color: #f0f8ff; /* Color de fondo suave */
         transition: border-color 0.3s ease;
     }

     .form-group .form-control:focus {
         border-color: #4a90e2;
         outline: none;
         box-shadow: 0 0 5px rgba(74, 144, 226, 0.5);
     }

     /* Estilos para el botón de guardar */
     .btn-primary {
         grid-column: span 2; /* El botón ocupa 2 columnas */
         width: 100px; /* Botón más pequeño */
         padding: 10px;
         background-color: #4a90e2;
         border: none;
         border-radius: 5px;
         color: #fff;
         font-size: 16px;
         cursor: pointer;
         transition: background-color 0.3s ease;
     }

     .btn-primary:hover {
         background-color: #357abd;
     }

     /* Estilos para el mensaje de error */
     #lblMensaje {
         grid-column: span 2; /* El mensaje ocupa 2 columnas */
         text-align: center;
         margin-top: 10px;
         font-size: 14px;
         color: #e74c3c;
     }
 </style>
 <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css">
 <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
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
