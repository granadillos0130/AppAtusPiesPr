<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Maestra.Master" AutoEventWireup="true" CodeBehind="RegistroProducto.aspx.cs" Inherits="AppAtusPiesPr.Vista.RegistroProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/sweetalert2@11/dist/sweetalert2.min.css">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <style>
   .tallas-container {
    margin-top: 8px;
}

.tallas-list {
    display: flex;
    flex-wrap: wrap;
    gap: 8px;
}

.tallas-list tr {
    display: flex;
    gap: 8px;
    flex-wrap: wrap;
}

.tallas-list td {
    margin: 0;
    padding: 0;
}

.tallas-list label {
    display: inline-block;
    min-width: 45px;
    padding: 8px;
    background: white;
    border: 1px solid #ced4da;
    border-radius: 4px;
    text-align: center;
    cursor: pointer;
    user-select: none;
}

.tallas-list input[type="checkbox"] {
    display: none;
}

.tallas-list input[type="checkbox"]:checked + label {
    background-color: #e9ecef;
    border-color: #6c757d;
}

.tallas-list label:hover {
    background-color: #f8f9fa;
}


    </style>
    
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-8">
                <asp:Panel ID="pnlRegistrarVendedor" runat="server" CssClass="card shadow">
                    <div class="card-header text-center bg-primary text-white">
                        <h2>Registrar Nuevo Producto</h2>
                    </div>
                    <div class="card-body">
                        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                        <div class="mb-3">
                            <label for="txtNombre" class="form-label">Nombre del Producto</label>
                            <div class="input-group">
                                <span class="input-group-text">🏷️</span>
                                <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="mb-3">
                            <label for="txtStock" class="form-label">Cantidad Stock</label>
                            <div class="input-group">
                                <span class="input-group-text">📦</span>
                                <asp:TextBox ID="txtStock" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="mb-3">
                            <label for="txtPrecio" class="form-label">Precio</label>
                            <div class="input-group">
                                <span class="input-group-text">💲</span>
                                <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="mb-3">
                            <label for="txtDescripcionProduc" class="form-label">Descripción del Producto</label>
                            <div class="input-group">
                                <span class="input-group-text">✍️</span>
                                <asp:TextBox ID="txtDescripcionProduc" runat="server" CssClass="form-control" TextMode="MultiLine" />
                            </div>
                        </div>
<div class="mb-3">
    <label class="form-label">Tallas Disponibles</label>
    <div
        <asp:CheckBoxList ID="chkTallas" runat="server" CssClass="tallas-list" RepeatDirection="Horizontal" RepeatLayout="Flow">
    <asp:ListItem Value="1">34</asp:ListItem>
    <asp:ListItem Value="2">35</asp:ListItem>
    <asp:ListItem Value="3">36</asp:ListItem>
    <asp:ListItem Value="4">37</asp:ListItem>
    <asp:ListItem Value="5">38</asp:ListItem>
    <asp:ListItem Value="6">39</asp:ListItem>
    <asp:ListItem Value="7">40</asp:ListItem>
    <asp:ListItem Value="8">41</asp:ListItem>
</asp:CheckBoxList>
    </div>
</div>
                        <div class="mb-3">
                            <label for="txtReferencia" class="form-label">Referencia</label>
                            <div class="input-group">
                                <span class="input-group-text">🔢</span>
                                <asp:TextBox ID="txtReferencia" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="mb-3">
                            <label for="ddlCategoria" class="form-label">Categoría</label>
                            <div class="input-group">
                                <span class="input-group-text">👞</span>
                                <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="mb-3">
                            <label for="txtDescuento" class="form-label">Descuento</label>
                            <div class="input-group">
                                <span class="input-group-text">🔖</span>
                                <asp:TextBox ID="txtDescuento" runat="server" CssClass="form-control" />
                            </div>
                        </div>

                        <div class="mb-3">
                            <label for="txtMarca" class="form-label">Marca</label>
                            <asp:DropDownList ID="txtMarca" runat="server" CssClass="form-control">
                                <asp:ListItem Value="">Seleccione una Marca</asp:ListItem>
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
                                <asp:ListItem Value="Balenciaga">Balenciaga</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                        <div class="mb-3">
                            <label for="inRuta" class="form-label">Añade una Imagen</label><br />
                            <asp:FileUpload ID="inRuta" runat="server" />
                        </div>

                        <div class="d-grid">
                            <asp:Button ID="btnRegistrar" runat="server" Text="Registrar" CssClass="btn btn-primary btn-block" OnClick="btnRegistrar_Click" />
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
