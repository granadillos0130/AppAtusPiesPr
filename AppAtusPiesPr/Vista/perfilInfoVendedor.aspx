<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/IndexMaestra.Master" AutoEventWireup="true" CodeBehind="perfilInfoVendedor.aspx.cs" Inherits="AppAtusPiesPr.Vista.perfilInfoVendedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>ATP</title>
    <meta name='viewport' content='width=device-width, initial-scale=1' />
    <link rel='stylesheet' type='text/css' media='screen' href='css/main.css' />
    <link rel="shortcut icon" href="recursos/ATP.png" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">


    <center>

        <div class="navbarFiltros">
            <nav>
                <ul class="menuFiltros">
                    <asp:Repeater ID="Repeater2" runat="server">
                        <ItemTemplate>
                            <li>
                                <a href='<%# "moduloCatalogoFiltrado.aspx?id=" + Eval("idCategoria") %>'>
                                    <%# Eval("descripcion") %>
                                </a>
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </nav>
        </div><center>
    <div class="navbarFiltros">
        <nav>

            <ul class="menuFiltros">
                <asp:Repeater ID="RepeaterMarca" runat="server">
                    <itemtemplate>
                        <li>
                            <a href='<%# "vista/moduloCatalogoFiltrado.aspx?id=" + Eval("idMarca") %>'>
                                <%# Eval("nombreMarca") %>
                            </a>
                        </li>
                    </itemtemplate>
                </asp:Repeater>
            </ul>
        </nav>
    </div>
</center>

    </center>

    <br />

    <style>
        .producto-detalle {
            max-width: 800px;
            background-color: white;
            border-radius: 10px;
            box-shadow: 0 4px 6px rgba(0,0,0,0.1);
            display: flex;
            align-items: flex-start;
        }

        .producto-imagen {
            max-width: 200px;
            margin-right: 30px;
        }

            .producto-imagen img {
                border-radius: 10px;
                object-fit: cover;
            }

        .producto-info {
            flex: 1;
        }

            .producto-info h1 {
                margin-bottom: 20px;
            }

            .producto-info ul {
                list-style: none;
                padding: 0;
            }

                .producto-info ul li {
                    margin-bottom: 15px;
                }

                    .producto-info ul li strong {
                        display: inline-block;
                        width: 150px;
                    }
    </style>

    <center>
        <h2>Perfil del Vendedor</h2>
        <br>
    </center>

    <div class="alert alert-danger" role="alert" runat="server" visible="false" id="lblMensaje"></div>

    <!-- Foto del Vendedor -->
    <div class="producto-detalle">
        <div class="producto-imagen">
            <asp:Image alt="Imagen 1" ID="ImgVendedor" runat="server" />
        </div>

        <!-- Información del Vendedor -->
        <div class="producto-info">
            <h1>
                <asp:Label ID="lblTituloProducto" runat="server"></asp:Label>
            </h1>
            <ul>
                <li>
                    <strong>Nombres:</strong>
                    <asp:Label ID="nombreVendedor" runat="server"></asp:Label>
                    <asp:Label ID="apellidoVendedor" runat="server"></asp:Label>
                </li>
                <li>
                    <strong>Telefono:</strong>
                    <asp:Label ID="telVendedor" runat="server"></asp:Label>
                </li>
                <li>
                    <strong>Descripcion:</strong>
                    <asp:Label ID="descVendedor" runat="server"></asp:Label>
                </li>
                <li>
                    <strong>Total Productos:</strong>
                    <asp:Label ID="totalProductos" runat="server"></asp:Label>
                </li>
            </ul>
        </div>
    </div>
    <hr>
    <br />

    <!-- Contenedor para las tarjetas -->
    <div id="cardsContainer" class="cards-container">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <div class="card">
                    <img src='<%# ResolveUrl(Eval("imagen").ToString()) %>' alt="Producto" class="card-image" />
                    <h4 class="card-title"><%# Eval("nombreProducto") %></h4>
                    <h4 class="card-title"></h4>
                    <div class="card-info">
                        <div class="card-details">
                            <a class="cardseller" href='<%# "perfilInfoVendedor.aspx?id=" + Eval("idVendedor") %>'>
                                <%# Eval("nombres") %><br>
                            </a>
                            <div class="cardprice">
                                <p>$<%# Eval("precio") %></p>

                            </div>
                            <div class="cardButtons">
                                <a class="buy-button" href='moduloCompra.aspx?id=<%# Eval("idProdctoEmpresa") %>'>Ver más..</a>
                                <a href="#" class="save-button">
                                    <img src="https://cdn-icons-png.flaticon.com/512/6165/6165217.png" alt="Guardar" class="save-icon" />
                                </a>
                            </div>
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
</asp:Content>
