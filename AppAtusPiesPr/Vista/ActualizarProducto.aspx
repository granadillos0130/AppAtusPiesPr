<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="ActualizarProducto.aspx.cs" Inherits="AppAtusPiesPr.Vista.ActualizarProducto" %>


<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    
    <link href="css/FormRegistro.css" rel="stylesheet" />
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css">
<script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>


</asp:Content>

<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">

     <form runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <br/><br/>
     <h3 style="color:black">Actualizar  Producto</h3>
 
      <asp:TextBox ID="txtProducto" runat="server" placeholder="IdProducto"></asp:TextBox><br/><br/>

     <asp:TextBox ID="txtNombre" runat="server" placeholder="Nombre del Producto"></asp:TextBox><br/><br/>
     
     <asp:TextBox ID="txtCodigo" runat="server" placeholder="Codigo del Producto"></asp:TextBox><br/><br/>
         
     <asp:TextBox ID="txtStock" runat="server" placeholder="Cantidad Stock"></asp:TextBox><br/><br/>
     
     <asp:TextBox ID="txtPrecio" runat="server" placeholder="Precio"></asp:TextBox><br/><br/> 
         
     <asp:TextBox ID="txtTalla" runat="server" placeholder="Talla"></asp:TextBox><br/><br/>

     
     <asp:Label ID="lblRuta" runat="server" Text="Label">Añade una Imagen</asp:Label>
     <asp:FileUpload ID="inRuta" runat="server" />
     
     <asp:Button ID="btnActualizar" class="buttons" runat="server" Text="Actualizar" OnClick="btnActualizar_Click" /> <br /><br />
 </form>

</asp:Content>
