<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/IndexMaestra.Master" AutoEventWireup="true" CodeBehind="moduloSobreNosotros.aspx.cs" Inherits="AppAtusPiesPr.Vista.moduloSobreNosotros" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>ATP </title>
    <meta name='viewport' content='width=device-width, initial-scale=1' />
    <link rel='stylesheet' type='text/css' media='screen' href='css/main.css' />
    <link rel="shortcut icon" href="vista/recursos/ATP.png" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <!-- Filtros -->
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
        </div>
    </center>
    <style>
<style>
.modern-about {
    max-width: 1200px;
    margin: 4rem auto;
    padding: 2rem;
    position: relative;
    overflow: hidden;
}

.modern-about::before {
    content: '';
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: linear-gradient(135deg, rgba(255,255,255,0.1) 0%, rgba(255,255,255,0.05) 100%);
    backdrop-filter: blur(10px);
    border-radius: 20px;
    z-index: -1;
}

.about-header {
    text-align: center;
    margin-bottom: 3rem;
    position: relative;
    margin-top: 10px;
}

.about-layout {
    display: grid;
    grid-template-columns: 1.2fr 0.8fr;
    gap: 4rem;
    align-items: center;
}

.about-content {
    display: flex;
    flex-direction: column;
    gap: 1.5rem;
}

.about-card {
    background: rgba(255, 255, 255, 0.9);
    padding: 2rem;
    border-radius: 15px;
    box-shadow: 0 10px 30px rgba(0, 0, 0, 0.05);
    transform: translateY(0);
    transition: all 0.3s ease;
}

.about-card:hover {
    transform: translateY(-5px);
    box-shadow: 0 15px 40px rgba(0, 0, 0, 0.1);
}

.about-image {
    position: relative;
    padding: 1rem;
}

.about-image img {
    width: 100%;
    height: auto;
    border-radius: 20px;
    box-shadow: 0 20px 40px rgba(0, 0, 0, 0.1);
    transition: transform 0.3s ease;
}

.about-image::before {
    content: '';
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: linear-gradient(45deg, #004AAD22, transparent);
    border-radius: 20px;
    z-index: 1;
}

.about-image:hover img {
    transform: scale(1.02);
}

.welcome-text {
    font-size: 1.2rem;
    color: rgb(0, 0, 0, 0,10);
    font-weight: 500;
    margin-bottom: 1rem;
}

.about-paragraph {
    line-height: 1.8;
    margin-bottom: 1rem;
}

@media (max-width: 968px) {
    .about-layout {
        grid-template-columns: 1fr;
        gap: 2rem;
    }
    
    .about-image {
        order: -1;
        max-width: 500px;
        margin: 0 auto;
    }
}

@media (max-width: 576px) {
    .modern-about {
        margin: 2rem auto;
        padding: 1rem;
    }
    
    .about-header h2 {
        font-size: 2rem;
    }
}
</style>

<div class="modern-about">
    <div class="about-header">
        <h2>¿Quienes Somos?</h2>
    </div>
    
    <div class="about-layout">
        <div class="about-content">
            <div class="about-card">
                <div class="welcome-text">¡Bienvenidos a nuestra página!</div>
                <p class="about-paragraph">
                    Somos un grupo de estudiantes apasionados del programa de Análisis y Desarrollo de Software del SENA, ubicado en Sogamoso, Boyacá, Colombia.
                </p>
                <p class="about-paragraph">
                    Nuestro equipo está conformado por cinco integrantes, quienes trabajamos juntos para desarrollar proyectos innovadores que combinan creatividad y tecnología. Durante nuestra formación, hemos adquirido conocimientos en programación, bases de datos, diseño de software y metodologías ágiles, habilidades que aplicamos en cada uno de nuestros desarrollos.
                </p>
                <p class="about-paragraph">
                    Este espacio refleja nuestra dedicación y compromiso con el aprendizaje continuo. Aquí encontrarás proyectos, herramientas y recursos diseñados para mostrar lo que somos capaces de hacer como futuros desarrolladores de software.
                </p>
                <p class="about-paragraph">
                    Gracias por visitarnos y ser parte de este emocionante viaje. ¡Esperamos que encuentres inspiración en nuestro trabajo!
                </p>
            </div>
        </div>
        
        <div class="about-image">
            <img src="recursos/logo sena.png" alt="Logo SENA" class="img-thumbnail">
        </div>
    </div>
</div>

    <style>
.team-section {
    max-width: 1200px;
    margin: 4rem auto;
    padding: 2rem;
}

.team-header {
    text-align: center;
    margin-bottom: 3rem;
}

.team-header h2 {
    font-size: 2.5rem;
    color: rgb(0, 0, 0, 0,15);
    margin-bottom: 1rem;
}

.team-grid {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
    gap: 2rem;
    padding: 1rem;
}

.team-card {
    background: white;
    border-radius: 15px;
    overflow: hidden;
    box-shadow: 0 10px 20px rgb(0, 0, 0, 0,10);
    transition: transform 0.3s ease, box-shadow 0.3s ease;
}

.team-card:hover {
    transform: translateY(-10px);
    box-shadow: 0 15px 30px rgba(0, 0, 0, 0.15);
}

.card-image {
    width: 100%;
    height: 250px;
    position: relative;
    overflow: hidden;
}

.card-image img {
    width: 100%;
    height: 100%;
    object-fit: cover;
    transition: transform 0.3s ease;
}

.team-card:hover .card-image img {
    transform: scale(1.1);
}

.card-content {
    padding: 1.5rem;
}

.member-name {
    font-size: 1.5rem;
    color: rgb(0, 0, 0, 0,15);
    margin-bottom: 0.5rem;
}

.member-role {
    font-size: 1rem;
    color: #666;
    margin-bottom: 1rem;
    font-weight: 500;
}

.member-description {
    color: #444;
    line-height: 1.6;
    margin-bottom: 1rem;
}

.social-links {
    display: flex;
    gap: 1rem;
    margin-top: 1rem;
}

.social-link {
    width: 35px;
    height: 35px;
    border-radius: 50%;
    background: #f0f0f0;
    display: flex;
    align-items: center;
    justify-content: center;
    color: #004AAD;
    transition: all 0.3s ease;
}

.social-link:hover {
    background: #004AAD;
    color: white;
}

@media (max-width: 768px) {
    .team-grid {
        grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
    }
}
</style>

<div class="team-section">
    <div class="team-header">
        <h2>Nuestro Equipo</h2>
    </div>
    
    <div class="team-grid">
        <!-- Miembro 1 -->
        <div class="team-card">
            <div class="card-image">
                <img src="/api/placeholder/300/300" alt="Miembro 1">
            </div>
            <div class="card-content">
                <h3 class="member-name">Juan Pérez</h3>
                <div class="member-role">Desarrollador Frontend</div>
                <p class="member-description">
                    Especializado en crear interfaces de usuario intuitivas y responsivas. Apasionado por el diseño UI/UX y las últimas tecnologías web.
                </p>
                <div class="social-links">
                    <a href="#" class="social-link">
                        <i class="fab fa-github"></i>
                    </a>
                    <a href="#" class="social-link">
                        <i class="fab fa-linkedin"></i>
                    </a>
                </div>
            </div>
        </div>

        <!-- Miembro 2 -->
        <div class="team-card">
            <div class="card-image">
                <img src="/api/placeholder/300/300" alt="Miembro 2">
            </div>
            <div class="card-content">
                <h3 class="member-name">Ana García</h3>
                <div class="member-role">Desarrolladora Backend</div>
                <p class="member-description">
                    Experta en desarrollo de APIs y bases de datos. Enfocada en crear soluciones escalables y eficientes.
                </p>
                <div class="social-links">
                    <a href="#" class="social-link">
                        <i class="fab fa-github"></i>
                    </a>
                    <a href="#" class="social-link">
                        <i class="fab fa-linkedin"></i>
                    </a>
                </div>
            </div>
        </div>

        <!-- Miembro 3 -->
        <div class="team-card">
            <div class="card-image">
                <img src="/api/placeholder/300/300" alt="Miembro 3">
            </div>
            <div class="card-content">
                <h3 class="member-name">Carlos Rodríguez</h3>
                <div class="member-role">Diseñador UX/UI</div>
                <p class="member-description">
                    Creativo y detallista en el diseño de experiencias de usuario. Especialista en interacción y usabilidad.
                </p>
                <div class="social-links">
                    <a href="#" class="social-link">
                        <i class="fab fa-github"></i>
                    </a>
                    <a href="#" class="social-link">
                        <i class="fab fa-linkedin"></i>
                    </a>
                </div>
            </div>
        </div>

        <!-- Miembro 4 -->
        <div class="team-card">
            <div class="card-image">
                <img src="/api/placeholder/300/300" alt="Miembro 4">
            </div>
            <div class="card-content">
                <h3 class="member-name">Laura Martínez</h3>
                <div class="member-role">QA Engineer</div>
                <p class="member-description">
                    Encargada de asegurar la calidad del software. Experta en pruebas automatizadas y control de calidad.
                </p>
                <div class="social-links">
                    <a href="#" class="social-link">
                        <i class="fab fa-github"></i>
                    </a>
                    <a href="#" class="social-link">
                        <i class="fab fa-linkedin"></i>
                    </a>
                </div>
            </div>
        </div>

        <!-- Miembro 5 -->
        <div class="team-card">
            <div class="card-image">
                <img src="/api/placeholder/300/300" alt="Miembro 5">
            </div>
            <div class="card-content">
                <h3 class="member-name">Miguel Torres</h3>
                <div class="member-role">DevOps Engineer</div>
                <p class="member-description">
                    Especialista en infraestructura y despliegue. Apasionado por la automatización y la mejora continua.
                </p>
                <div class="social-links">
                    <a href="#" class="social-link">
                        <i class="fab fa-github"></i>
                    </a>
                    <a href="#" class="social-link">
                        <i class="fab fa-linkedin"></i>
                    </a>
                </div>
            </div>
        </div>
    </div>
</div>

    <!-- Pie de página -->
    <div class="pie-pagina">
        <br />
        <p>&copy; 2024 A TUS PIES. Todos los derechos reservados.</p>
        <p>Diseñado con amor para brindar estilo y comodidad.</p>
        <p>Contáctanos: <a href="mailto:contacto@atuspies.com">contacto@atuspies.com</a></p>
        <br />
    </div>

</asp:Content>
