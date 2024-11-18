<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="moduloCompra.aspx.cs" Inherits="AppAtusPiesPr.Vista.moduloCompra" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>ATP</title>
    <meta name='viewport' content='width=device-width, initial-scale=1' />
    <link rel='stylesheet' type='text/css' media='screen' href='css/main.css' />
    <link rel="shortcut icon" href="recursos/ATP.png" />
</head>
<body>
    <form runat="server">

        <header>

            <!-- MENÚ -->
            <div class="navbar">
                <nav>
                    <ul class="menu">
                        <li><a href="#">WINTER 24</a></li>
                        <li><a href="#">WOMEN</a></li>
                        <li><a href="#">MEN</a></li>
                        <li><a href="#">GIFTS</a></li>
                        <li><a href="#">COUTURE</a></li>
                        <li><a href="#">EXPLORE</a></li>
                    </ul>
                </nav>

                <a href="../index.aspx" class="logo">A TUS PIES</a>

                <div class="right-section">
                    <div class="search-bar">
                        <input type="text" placeholder="Search..."/>
                        <i class="fas fa-search"></i>
                    </div>
                    <a href="vista/modulos/login.html" class="login">LOGIN</a>

                    <a href="#" class="bookmark"><i class="fas fa-bookmark"></i></a>
                </div>
            </div>
            <hr />

        </header>

        <!--Filtros-->
        <center>

            <div class="navbarFiltros">
                <nav>
                    <ul class="menuFiltros">
                        <asp:Repeater ID="Repeater2" runat="server">
                            <ItemTemplate>
                                <li>
                                    <a href='<%# "Vista/moduloCatalogoFiltrado.aspx?id=" + Eval("idCategoria") %>'>
                                        <%# Eval("descripcion") %>
                                    </a>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </nav>
            </div>

        </center>

        <hr/>
        <br />

        <div class="alert alert-danger" role="alert" runat="server" visible="false" id="lblMensaje"></div>
        <!-- Imagen del producto -->
        <div class="producto-detalle">
            <!-- Imagen del producto -->
            <div class="producto-imagen">
                <asp:Image alt="Imagen 1" ID="ImgProducto" runat="server" />
            </div>

            <!-- Información del producto -->
            <div class="producto-info">
                <h1>
                    <asp:Label ID="lblTituloProducto" runat="server"></asp:Label></h1>
                <ul>
                    <li><strong>Vendedor:</strong>
                        <asp:Label ID="nombreVendedor" runat="server"></asp:Label></li>
                    <li><strong>Stock:</strong>
                        <asp:Label ID="stockProducto" runat="server"></asp:Label></li>
                    <li><strong>Num. Referencia:</strong>
                        <asp:Label ID="referenciaProducto" runat="server"></asp:Label></li>
                    <li><strong>Descuento:</strong>
                        <asp:Label ID="descuento" runat="server"></asp:Label></li>
                    <li><strong>Descripcion Producto:</strong>
                        <asp:Label ID="productoDescripcion" runat="server"></asp:Label></li>
                    <li><strong>Tallas Disponibles: </strong>
                        <asp:DropDownList ID="ddlTallas" runat="server" CssClass="dropdown"></asp:DropDownList></li>
                    <li><strong>Marca:</strong>
                        <asp:Label ID="marcaProducto" runat="server"></asp:Label></li>
                    <li><strong>Precio:</strong>
                        <asp:Label ID="PrecioProducto" runat="server"></asp:Label></li>
                </ul>
                <p class="devoluciones">Devoluciones y envíos gratuitos</p>
                <!-- Botón para agregar al carrito -->
                <asp:Button ID="btnAgregarCarrito" runat="server" Text="Agregar al carrito" CssClass="btn-agregar-carrito" OnClick="btnAgregarCarrito_Click" />
            </div>

        </div>

        <br />

        <!-- Contenedor para las tarjetas -->
        <div class="cards-container">
            <!-- Tarjeta 1 -->
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <div class="card">
                        <img src='<%# ResolveUrl(Eval("imagen").ToString()) %>' alt="Chaqueta" class="card-image" />
                        <p class="card-title"><%# Eval("nombreProducto") %></p>

                        <div class="card-info">
                            <div class="card-details">
                                <a class="buy-button" href='moduloCompra.aspx?id=<%# Eval("idProdctoEmpresa") %>'>Ver más..</a>
                                <a href="#" class="save-button">
                                    <img src="https://cdn-icons-png.flaticon.com/512/6165/6165217.png" alt="Guardar" class="save-icon" />
                                </a>
                                <center>
                                </center>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <!-- PIE DE PAGINA -->
        <div class="pie-pagina">
            <br />
            <p>&copy; 2024 A TUS PIES. Todos los derechos reservados.</p>
            <p>Diseñado con amor para brindar estilo y comodidad.</p>
            <p>Contáctanos: <a href="mailto:contacto@atuspies.com">contacto@atuspies.com</a></p>
            <br />
        </div>
    </form>
</body>
</html>
