CREATE DATABASE SistemaMatriculas

CREATE TABLE Estudiante (
    IdEstudiante INT IDENTITY PRIMARY KEY,
    NumeroDocumento VARCHAR(20) NOT NULL,
    Nombres VARCHAR(100) NOT NULL,
    Apellidos VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    FechaCreacion DATETIME NOT NULL,
    FechaModificacion DATETIME NULL,
    UsuarioCreacion VARCHAR(50) NOT NULL,
    UsuarioModificacion VARCHAR(50) NULL,
	Estado BIT NOT NULL
);

CREATE TABLE Profesor (
    IdProfesor INT IDENTITY PRIMARY KEY,
	NumeroDocumento VARCHAR(20) NOT NULL,    
    Nombres VARCHAR(100) NOT NULL,
    Apellidos VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
	Especialidad VARCHAR(100) NOT NULL,
    FechaCreacion DATETIME NOT NULL,
    FechaModificacion DATETIME NULL,
    UsuarioCreacion VARCHAR(50) NOT NULL,
    UsuarioModificacion VARCHAR(50) NULL,
	Estado BIT NOT NULL
);

CREATE TABLE Curso (
    IdCurso INT IDENTITY PRIMARY KEY,
    Codigo VARCHAR(10) NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    IdProfesor INT NOT NULL,
    FechaCreacion DATETIME NOT NULL,
    FechaModificacion DATETIME NULL,
    UsuarioCreacion VARCHAR(50) NOT NULL,
    UsuarioModificacion VARCHAR(50) NULL,
    Estado BIT NOT NULL

    FOREIGN KEY (IdProfesor) REFERENCES Profesor(IdProfesor)
);

CREATE TABLE EstadoMatricula (
    IdEstadoMatricula INT IDENTITY PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL,
	Descripcion VARCHAR(200) NOT NULL,
	FechaCreacion DATETIME NOT NULL,
    FechaModificacion DATETIME NULL,
    UsuarioCreacion VARCHAR(50) NOT NULL,
    UsuarioModificacion VARCHAR(50) NULL,
    Estado BIT NOT NULL
);

CREATE TABLE Matricula (
    IdMatricula INT IDENTITY PRIMARY KEY,
    IdEstudiante INT NOT NULL,
    IdCurso INT NOT NULL,
	IdEstadoMatricula INT NOT NULL,
    FechaMatricula DATE NOT NULL,
    FechaCreacion DATETIME NOT NULL,
    FechaModificacion DATETIME NULL,
    UsuarioCreacion VARCHAR(50) NOT NULL,
    UsuarioModificacion VARCHAR(50) NULL,
    Estado BIT NOT NULL

    FOREIGN KEY (IdEstudiante) REFERENCES Estudiante(IdEstudiante),
    FOREIGN KEY (IdCurso) REFERENCES Curso(IdCurso),
    FOREIGN KEY (IdEstadoMatricula) REFERENCES EstadoMatricula(IdEstadoMatricula),
);


INSERT INTO Estudiante (NumeroDocumento, Nombres, Apellidos, Email, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion, Estado)
VALUES 
('12345678', 'Juan', 'Pérez', 'juan.perez@mail.com', GETDATE(), NULL, 'admin', NULL, 1),
('87654321', 'Ana', 'García', 'ana.garcia@mail.com', GETDATE(), NULL, 'admin', NULL, 1),
('11223344', 'Carlos', 'Ramírez', 'carlos.ramirez@mail.com', GETDATE(), NULL, 'admin', NULL, 1),
('44332211', 'Lucía', 'Torres', 'lucia.torres@mail.com', GETDATE(), NULL, 'admin', NULL, 1),
('99887766', 'Pedro', 'Gómez', 'pedro.gomez@mail.com', GETDATE(), NULL, 'admin', NULL, 1),
('66778899', 'María', 'López', 'maria.lopez@mail.com', GETDATE(), NULL, 'admin', NULL, 1),
('55667788', 'Jorge', 'Fernández', 'jorge.fernandez@mail.com', GETDATE(), NULL, 'admin', NULL, 1),
('33445566', 'Elena', 'Castro', 'elena.castro@mail.com', GETDATE(), NULL, 'admin', NULL, 1),
('22113344', 'Raúl', 'Mendoza', 'raul.mendoza@mail.com', GETDATE(), NULL, 'admin', NULL, 1),
('11002233', 'Paula', 'Morales', 'paula.morales@mail.com', GETDATE(), NULL, 'admin', NULL, 1);


INSERT INTO Profesor (NumeroDocumento, Nombres, Apellidos, Email, Especialidad, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion, Estado)
VALUES 
('12312312', 'Alberto', 'Sánchez', 'alberto.sanchez@mail.com', 'Matemáticas', GETDATE(), NULL, 'admin', NULL, 1),
('32132132', 'Beatriz', 'Rojas', 'beatriz.rojas@mail.com', 'Historia', GETDATE(), NULL, 'admin', NULL, 1),
('45645645', 'Cristina', 'Valdez', 'cristina.valdez@mail.com', 'Ciencias', GETDATE(), NULL, 'admin', NULL, 1),
('65465465', 'Daniel', 'Ruiz', 'daniel.ruiz@mail.com', 'Lenguaje', GETDATE(), NULL, 'admin', NULL, 1),
('78978978', 'Eva', 'Luna', 'eva.luna@mail.com', 'Física', GETDATE(), NULL, 'admin', NULL, 1),
('98798798', 'Francisco', 'Delgado', 'francisco.delgado@mail.com', 'Química', GETDATE(), NULL, 'admin', NULL, 1),
('74185296', 'Gabriela', 'Navarro', 'gabriela.navarro@mail.com', 'Biología', GETDATE(), NULL, 'admin', NULL, 1),
('36925814', 'Héctor', 'Salas', 'hector.salas@mail.com', 'Educación Física', GETDATE(), NULL, 'admin', NULL, 1),
('96385274', 'Isabel', 'Vega', 'isabel.vega@mail.com', 'Arte', GETDATE(), NULL, 'admin', NULL, 1),
('85274196', 'Joaquín', 'Campos', 'joaquin.campos@mail.com', 'Música', GETDATE(), NULL, 'admin', NULL, 1);


INSERT INTO Curso (Codigo, Nombre, IdProfesor, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion, Estado)
VALUES 
('MAT101', 'Álgebra I', 1, GETDATE(), NULL, 'admin', NULL, 1),
('HIS201', 'Historia del Perú', 2, GETDATE(), NULL, 'admin', NULL, 1),
('CIE301', 'Biología General', 3, GETDATE(), NULL, 'admin', NULL, 1),
('LEN401', 'Redacción Avanzada', 4, GETDATE(), NULL, 'admin', NULL, 1),
('FIS501', 'Física I', 5, GETDATE(), NULL, 'admin', NULL, 1),
('QUI601', 'Química Orgánica', 6, GETDATE(), NULL, 'admin', NULL, 1),
('BIO701', 'Microbiología', 7, GETDATE(), NULL, 'admin', NULL, 1),
('EDF801', 'Atletismo', 8, GETDATE(), NULL, 'admin', NULL, 1),
('ART901', 'Dibujo Técnico', 9, GETDATE(), NULL, 'admin', NULL, 1),
('MUS1010', 'Teoría Musical', 10, GETDATE(), NULL, 'admin', NULL, 1);


INSERT INTO EstadoMatricula (Nombre, Descripcion, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion, Estado)
VALUES 
('Activa', 'El estudiante está matriculado activamente', GETDATE(), NULL, 'admin', NULL, 1),
('Cancelada', 'La matrícula fue cancelada por el estudiante o la institución', GETDATE(), NULL, 'admin', NULL, 1),
('Finalizada', 'La matrícula ha concluido satisfactoriamente', GETDATE(), NULL, 'admin', NULL, 1);


INSERT INTO Matricula (IdEstudiante, IdCurso, IdEstadoMatricula, FechaMatricula, FechaCreacion, FechaModificacion, UsuarioCreacion, UsuarioModificacion, Estado)
VALUES 
(1, 1, 1, '2025-03-01', GETDATE(), NULL, 'admin', NULL, 1),
(2, 2, 1, '2025-03-02', GETDATE(), NULL, 'admin', NULL, 1),
(3, 3, 2, '2025-03-03', GETDATE(), NULL, 'admin', NULL, 1),
(4, 4, 3, '2025-03-04', GETDATE(), NULL, 'admin', NULL, 1),
(5, 5, 1, '2025-03-05', GETDATE(), NULL, 'admin', NULL, 1),
(6, 6, 2, '2025-03-06', GETDATE(), NULL, 'admin', NULL, 1),
(7, 7, 3, '2025-03-07', GETDATE(), NULL, 'admin', NULL, 1),
(8, 8, 1, '2025-03-08', GETDATE(), NULL, 'admin', NULL, 1),
(9, 9, 1, '2025-03-09', GETDATE(), NULL, 'admin', NULL, 1),
(10, 10, 3, '2025-03-10', GETDATE(), NULL, 'admin', NULL, 1);

