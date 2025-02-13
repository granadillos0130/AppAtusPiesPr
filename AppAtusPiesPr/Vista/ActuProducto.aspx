<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="ActuProducto.aspx.cs" Inherits="AppAtusPiesPr.Vista.ActuProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <style>
       .header-bg {
  
    color: white;
    padding: 20px;
    margin: 0 auto 30px auto; /* Centrar y mantener margen inferior */
    max-width: 800px; /* Igual que .form-container */
    border-radius: 8px; /* Opcional, para que coincida con el formulario */
    text-align: center;
}

        .form-container {
            background-color: #f8f9fa;
            padding: 25px;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
            max-width: 800px;
            margin: 0 auto;
        }
        .form-group {
            margin-bottom: 15px;
        }
        .btn-update {
            background-color: #007bff;
            color: white;
            padding: 10px 30px;
            border: none;
            border-radius: 5px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    
    <br /><br />
    
     <div class="card-header text-center bg-primary text-white">
     <h2>Registrar Nuevo Producto</h2>
 </div>

    <div class="form-container">
        <div class="form-group">
            <label for="ddlProducto">Seleccione un Producto</label>
            <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>

        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="txtNombre">Nombre del Producto</label>
                    <div class="input-group">
                        <span class="input-group-text">🏷️</span>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ingrese nombre del producto"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label for="txtStock">Cantidad en Stock</label>
                    <div class="input-group">
                        <span class="input-group-text">📦</span>
                        <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" TextMode="Number" placeholder="Ingrese cantidad"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="txtPrecio">Precio</label>
                    <div class="input-group">
                        <span class="input-group-text">💲</span>
                        <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" />
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label for="txtDescuento">Descuento</label>
                    <div class="input-group">
                        <span class="input-group-text">🔖</span>
                        <asp:TextBox ID="txtDescuento" runat="server" CssClass="form-control" />
                    </div>
                </div>
            </div>
        </div>

        <div class="form-group">
            <label for="txtDescripcionProduc">Descripción del Producto</label>
            <div class="input-group">
                <span class="input-group-text">✍️</span>
                <asp:TextBox ID="txtDescripcionProduc" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
            </div>
        </div>

        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="txtReferencia">Referencia</label>
                    <div class="input-group">
                        <span class="input-group-text">🔢</span>
                        <asp:TextBox ID="txtReferencia" runat="server" CssClass="form-control" />
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label for="txtEstado">Estado</label>
                    <asp:DropDownList ID="txtEstado" runat="server" CssClass="form-control">
                        <asp:ListItem Value="disponible">Disponible</asp:ListItem>
                        <asp:ListItem Value="no disponible">No Disponible</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label for="txtMarca">Marca</label>
                    <asp:DropDownList ID="txtMarca" runat="server" CssClass="form-control">
                        <asp:ListItem Value="0">Seleccione una Marca</asp:ListItem>
                        <asp:ListItem Value="Nike">Nike</asp:ListItem>
                        <asp:ListItem Value="Adidas">Adidas</asp:ListItem>
                        <asp:ListItem Value="Reebok">Reebok</asp:ListItem>
                        <asp:ListItem Value="Puma">Puma</asp:ListItem>
                        <asp:ListItem Value="New Balance">New Balance</asp:ListItem>
                        <asp:ListItem Value="Under Armour">Under Armour</asp:ListItem>
                        <asp:ListItem Value="Asics">Asics</asp:ListItem>
                        <asp:ListItem Value="Saucony">Saucony</asp:ListItem>
                        <asp:ListItem Value="Hoka">Hoka</asp:ListItem>
                        <asp:ListItem Value="Mizuno">Mizuno</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-6">
                <div class="form-group">
                    <label for="txtCategoria">Categoría</label>
                    <asp:DropDownList ID="txtCategoria" runat="server" CssClass="form-control">
                        <asp:ListItem Value="">Seleccione un Tipo de calzado</asp:ListItem>
                        <asp:ListItem Value="deportivo">Calzado deportivo</asp:ListItem>
                        <asp:ListItem Value="casual">Calzado casual</asp:ListItem>
                        <asp:ListItem Value="tecnico">Calzado técnico</asp:ListItem>
                        <asp:ListItem Value="correr">Calzado para correr</asp:ListItem>
                        <asp:ListItem Value="crossfit">Calzado para crossfit</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <div class="form-group">
            <label for="inRuta">Añade una imagen</label>
            <asp:FileUpload ID="inRuta" runat="server" CssClass="form-control" />
        </div>

        <div class="text-center mt-4">
            <asp:Button ID="btnActualizar" runat="server" Text="Actualizar Producto" 
                CssClass="btn-update" OnClick="btnActualizar_Click1" />
        </div>
    </div>
</asp:Content>