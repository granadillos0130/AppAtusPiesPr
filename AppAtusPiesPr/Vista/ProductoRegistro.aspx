<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="ProductoRegistro.aspx.cs" Inherits="AppAtusPiesPr.Vista.ProductoRegistro" %>


<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">

    <link href="css/FormRegistro.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>



</asp:Content>



<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">

    <br/><br/>

      <h3 style="color:black">Registrar Nuevo Producto</h3>
  <form id="formRegistrarProducto" runat="server"><br/>

  <asp:TextBox ID="txtNombre" runat="server" placeholder="Nombre del Producto"></asp:TextBox><br/><br/>
  
  <asp:TextBox ID="txtCodigo" runat="server" placeholder="Codigo del Producto"></asp:TextBox><br/><br/>
    
  <asp:TextBox ID="txtStock" runat="server" placeholder="Cantidad Stock"></asp:TextBox><br/><br/>
  
  <asp:TextBox ID="txtPrecio" runat="server" placeholder="Precio"></asp:TextBox><br/><br/> 

      
  <asp:TextBox ID="txtTalla" runat="server" placeholder="Talla"></asp:TextBox><br/><br/>

      <asp:TextBox ID="txtVendedor" runat="server" placeholder="IdVendedor"></asp:TextBox><br/><br/>

   

 <asp:Label ID="lblRuta" runat="server" Text="Label">Añade una Imagen</asp:Label>
<asp:FileUpload ID="inRuta" runat="server" />
  
  
  <asp:Button ID="btnRegistrar" class="buttons" runat="server" Text="Registrar" OnClick="btnRegistrar_Click" /> <br /><br />

  </form>

</asp:Content>
