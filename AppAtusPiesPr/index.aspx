﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="AppAtusPiesPr.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset='utf-8' />
    <meta http-equiv='X-UA-Compatible' content='IE=edge' />
    <title>ATP </title>
    <meta name='viewport' content='width=device-width, initial-scale=1' />
    <link rel='stylesheet' type='text/css' media='screen' href='vista/css/main.css' />

    <link rel="shortcut icon" href="vista/recursos/ATP.png" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="container">
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

                        <a href="index.html" class="logo">A TUS PIES</a>

                        <div class="right-section">
                            <div class="search-bar">
                                <input type="text" placeholder="Search...">
                                <i class="fas fa-search"></i>
                            </div>
                            <a href="Vista/modulos/moduloCarrito.aspx" class="save-button2">
                                <img src="https://cdn-icons-png.flaticon.com/512/6165/6165217.png" alt="Guardar" class="save-icon" />
                            </a>
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
                                            <a href='<%# "vista/moduloCatalogoFiltrado.aspx?id=" + Eval("idCategoria") %>'>
                                                <%# Eval("descripcion") %>
                                            </a>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </nav>
                    </div>

                </center>

                <!-- CARROUSEL -->
                <div class="carrusel">
                    <div id="carouselExample" class="carousel slide" data-bs-ride="carousel">
                        <div class="carousel-inner">
                            <div class="carousel-item active">
                                <img src="https://i.pinimg.com/originals/d1/95/58/d19558c9c55fd228ab81852369dbe4a8.gif"
                                    alt="Imagen 1">
                            </div>
                        </div>
                    </div>
                </div>
                <br>

                <center>
                <p>Conoce Nuestros Productos</p>
                    </center>
                <br>
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
                                        <a class="buy-button" href='Vista/moduloCompra.aspx?id=<%# Eval("idProdctoEmpresa") %>'>Ver más..</a>
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

            </div>
        </div>
    </form>
</body>
</html>
