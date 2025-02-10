<!DOCTYPE html>
<html>
<head>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" rel="stylesheet">
    <style>
        :root {
            --primary-color: #004AAD;
            --secondary-color: #2563eb;
            --accent-color: #60a5fa;
            --text-primary: #1f2937;
            --text-secondary: #4b5563;
            --background-light: #f3f4f6;
        }

        .modern-about {
            max-width: 1200px;
            margin: 6rem auto;
            padding: 0 2rem;
        }

        .about-header {
            text-align: center;
            margin-bottom: 5rem;
            position: relative;
        }

        .about-header h2 {
            font-size: 3.5rem;
            color: var(--text-primary);
            margin-bottom: 1.5rem;
            position: relative;
            display: inline-block;
        }

        .about-header h2::after {
            content: '';
            position: absolute;
            bottom: -10px;
            left: 50%;
            transform: translateX(-50%);
            width: 100px;
            height: 4px;
            background: var(--primary-color);
            border-radius: 2px;
        }

        .about-layout {
            display: grid;
            grid-template-columns: 1.2fr 0.8fr;
            gap: 6rem;
            align-items: center;
            background: white;
            border-radius: 30px;
            padding: 4rem;
            box-shadow: 0 20px 40px rgba(0, 0, 0, 0.1);
        }

        .about-content {
            display: flex;
            flex-direction: column;
            gap: 2rem;
        }

        .welcome-text {
            font-size: 1.5rem;
            color: var(--primary-color);
            font-weight: 600;
            letter-spacing: -0.5px;
        }

        .about-paragraph {
            font-size: 1.1rem;
            line-height: 1.8;
            color: var(--text-secondary);
        }

        .about-image {
            position: relative;
        }

        .about-image img {
            width: 100%;
            height: auto;
            border-radius: 20px;
            box-shadow: 0 20px 40px rgba(0, 0, 0, 0.15);
            transform: rotate(-3deg);
            transition: transform 0.5s ease;
        }

        .about-image:hover img {
            transform: rotate(0deg);
        }

        /* Team Section Styles */
        .team-section {
            max-width: 1200px;
            margin: 8rem auto;
            padding: 0 2rem;
        }

        .team-header {
            text-align: center;
            margin-bottom: 5rem;
        }

        .team-header h2 {
            font-size: 3.5rem;
            color: var(--text-primary);
            margin-bottom: 1.5rem;
            position: relative;
            display: inline-block;
        }

        .team-header h2::after {
            content: '';
            position: absolute;
            bottom: -10px;
            left: 50%;
            transform: translateX(-50%);
            width: 100px;
            height: 4px;
            background: var(--primary-color);
            border-radius: 2px;
        }

        .team-grid {
            display: grid;
            grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
            gap: 3rem;
            padding: 2rem;
        }

        .team-card {
            background: white;
    border-radius: 20px;
    overflow: hidden;
    box-shadow: 0 15px 30px rgba(0, 0, 0, 0.1);
    transition: all 0.3s ease;
    position: relative;
    height: 100%; /* Asegura que todas las tarjetas tengan la misma altura */
    display: flex;
    flex-direction: column;
        }

        .team-card:hover {
            transform: translateY(-10px);
            box-shadow: 0 20px 40px rgba(0, 0, 0, 0.2);
        }

        .card-image {
            width: 100%;
            height: 300px;
            position: relative;
            overflow: hidden;
        }

        .card-image img {
            width: 100%;
            height: 100%;
            object-fit: cover;
            transition: transform 0.5s ease;
        }

        .team-card:hover .card-image img {
            transform: scale(1.1);
        }

        .card-content {
            padding: 2rem;
            background: white;
            position: relative;
        }

        .member-name {
            font-size: 1.8rem;
            color: var(--text-primary);
            margin-bottom: 0.5rem;
            font-weight: 600;
        }

        .member-role {
            font-size: 1.1rem;
            color: var(--primary-color);
            margin-bottom: 1.5rem;
            font-weight: 500;
            text-transform: uppercase;
            letter-spacing: 1px;
        }

        .member-description {
            color: var(--text-secondary);
            line-height: 1.7;
            margin-bottom: 1.5rem;
            font-size: 1.1rem;
        }

        .social-links {
            display: flex;
            gap: 1rem;
        }

        .social-link {
            width: 40px;
            height: 40px;
            border-radius: 50%;
            background: var(--background-light);
            display: flex;
            align-items: center;
            justify-content: center;
            color: var(--primary-color);
            font-size: 1.2rem;
            transition: all 0.3s ease;
        }

        .social-link:hover {
            background: var(--primary-color);
            color: white;
            transform: translateY(-3px);
        }

        .pie-pagina {
            background: var(--primary-color);
            color: white;
            text-align: center;
            padding: 3rem 0;
            margin-top: 4rem;
        }

        .pie-pagina p {
            margin: 0.5rem 0;
            font-size: 1.1rem;
        }

        .pie-pagina a {
            color: white;
            text-decoration: none;
            border-bottom: 1px solid rgba(255, 255, 255, 0.3);
            transition: border-color 0.3s ease;
        }

        .pie-pagina a:hover {
            border-color: white;
        }

        @media (max-width: 968px) {
            .about-layout {
                grid-template-columns: 1fr;
                gap: 3rem;
                padding: 2rem;
            }

            .about-image {
                order: -1;
            }

            .about-header h2,
            .team-header h2 {
                font-size: 2.5rem;
            }

            .team-grid {
                grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
            }
        }

        @media (max-width: 576px) {
            .modern-about,
            .team-section {
                margin: 3rem auto;
                padding: 1rem;
            }

            .about-header h2,
            .team-header h2 {
                font-size: 2rem;
            }
        }
    </style>
</head>
<body>
    <div class="modern-about">
        <div class="about-header">
            <h2>¿Quiénes Somos?</h2>
        </div>
        
        <div class="about-layout">
            <div class="about-content">
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
            </div>
            
            <div class="about-image">
               <img src="/Vista/imagenes/logo-del-sena-verde.jpg" alt="Logo SENA">
        
            </div>
        </div>
    </div>

    <div class="team-section">
        <div class="team-header">
            <h2>Nuestro Equipo</h2>
        </div>
        
        <div class="team-grid">
            

            <div class="team-card">
                <div class="card-image">
                    <img src="/Vista/imagenes/j.jpg" alt="Julian">
                </div>
                <div class="card-content">
                    <h3 class="member-name">Julian Angarita</h3>
                    <div class="member-role">Desarrollador Frontend</div>
                    <p class="member-description">
                        Especializado en crear interfaces de usuario intuitivas y responsivas. Apasionado por el diseño UI/UX y las últimas tecnologías web.
                    </p>
                    <div class="social-links">
                        <a href="" class="social-link">
                            <i class="fab fa-github"></i>
                        </a>
                        <a href="#" class="social-link">
                            <i class="fab fa-linkedin"></i>
                        </a>
                    </div>
                </div>
            </div>

       
            <div class="team-card">
                <div class="card-image">
                    <img src="/Vista/imagenes/Captura de pantalla 2025-02-09 220357.png" alt="Luis">
                </div>
                <div class="card-content">
                    <h3 class="member-name">Luis Salamanca </h3>
                    <div class="member-role">Desarrollador Backend</div>
                    <p class="member-description">
                        Experta en desarrollo de APIs y bases de datos. Enfocada en crear soluciones escalables y eficientes.
                    </p>
                    <div class="social-links">
                        <a href="https://github.com/papuluiszzz" class="social-link">
                            <i class="fab fa-github"></i>
                        </a>
                        <a href="#" class="social-link">
                            <i class="fab fa-linkedin"></i>
                        </a>
                    </div>
                </div>
            </div>

            <div class="team-card">
                <div class="card-image">
                    <img src="/Vista/imagenes/Captura de pantalla 2025-02-09 214344.png" alt="San">
                </div>
                <div class="card-content">
                    <h3 class="member-name">Santiago Granados </h3>
                    <div class="member-role">Diseñador UX/UI</div>
                    <p class="member-description">
                        Creativo y detallista en el diseño de experiencias de usuario. Especialista en interacción y usabilidad.
                    </p>
                    <div class="social-links">
                        <a href="https://github.com/granadillos0130" class="social-link">
                            <i class="fab fa-github"></i>
                        </a>
                        <a href="#" class="social-link">
                            <i class="fab fa-linkedin"></i>
                        </a>
                    </div>
                </div>
            </div>

            <div class="team-card">
                <div class="card-image">
                      <img src="/Vista/imagenes/Captura de pantalla 2025-02-09 214745.png" alt="B">
                </div>
                <div class="card-content">
                    <h3 class="member-name">Brayam Ojeda</h3>
                    <div class="member-role">QA Engineer</div>
                    <p class="member-description">
                        Encargada de asegurar la calidad del software. Experta en pruebas automatizadas y control de calidad.
                    </p>
                    <div class="social-links">
                        <a href="https://github.com/Brayam96" class="social-link">
                            <i class="fab fa-github"></i>
                        </a>
                        <a href="#" class="social-link">
                            <i class="fab fa-linkedin"></i>
                        </a>
                    </div>
                </div>
            </div>

            <div class="team-card">
                <div class="card-image">
                 <img src="/Vista/imagenes/Captura de pantalla 2025-02-09 214620.png" alt="W">
                </div>
                <div class="card-content">
                    <h3 class="member-name">Wilmar Perez</h3>
                    <div class="member-role">DevOps Engineer</div>
                    <p class="member-description">
                        Especialista en infraestructura y despliegue. Apasionado por la automatización y la mejora continua.
                    </p>
                    <div class="social-links">
                        <a href="https://github.com/wilmarpere" class="social-link">
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

    <div class="pie-pagina">
        <p>&copy; 2024 A TUS PIES. Todos los derechos reservados.</p>
        <p>Diseñado con amor para brindar estilo y comodidad.</p>
        <p>Contáctanos: <a href="mailto:contacto@atuspies.com">contacto@atuspies.com</a></p>
    </div>
</body>
</html>