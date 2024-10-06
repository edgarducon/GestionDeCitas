-- Crear la base de datos de Personas
CREATE DATABASE PersonasDB;
GO

-- Usar la base de datos
USE PersonasDB;
GO

-- Crear la tabla TipoPersona (Médico o Paciente)
CREATE TABLE TipoPersonas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Descripcion VARCHAR(50) NOT NULL
);
GO

-- Crear la tabla Personas
CREATE TABLE Personas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    TipoPersonaId INT NOT NULL,
    CONSTRAINT FK_TipoPersona FOREIGN KEY (TipoPersonaId) REFERENCES TipoPersonas(Id)
);
GO


-- Insertar datos en la tabla TipoPersona
INSERT INTO TipoPersonas (Descripcion) VALUES ('Médico');
INSERT INTO TipoPersonas (Descripcion) VALUES ('Paciente');
GO


-- creación de base de datos CitasBD
CREATE DATABASE CitasDB;
GO

-- Usar la base de datos
USE CitasDB;
GO

-- Crear la tabla EstadoCita (Pendiente, En proceso, Finalizada)
CREATE TABLE EstadoCitas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Descripcion VARCHAR(50) NOT NULL
);
GO

-- Crear la tabla Citas
CREATE TABLE Citas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FechaCita DATETIME NOT NULL,
    Lugar VARCHAR(200) NOT NULL,
    EstadoCitaId INT NOT NULL,
    MedicoId INT NOT NULL,  -- Referencia a Personas del microservicio de Personas
    PacienteId INT NOT NULL,  -- Referencia a Personas del microservicio de Personas
    CONSTRAINT FK_EstadoCita FOREIGN KEY (EstadoCitaId) REFERENCES EstadoCita(Id)
);
GO
alter table Citas drop constraint FK_EstadoCita
ALTER TABLE Citas
ADD CONSTRAINT FK_EstadoCita
FOREIGN KEY  (EstadoCitaId) REFERENCES EstadoCitas(Id)


-- Insertar datos en la tabla EstadoCita
INSERT INTO EstadoCitas (Descripcion) VALUES ('Pendiente');
INSERT INTO EstadoCitas (Descripcion) VALUES ('En proceso');
INSERT INTO EstadoCitas (Descripcion) VALUES ('Finalizada');
GO

-- Crear la base de datos de Recetas
CREATE DATABASE RecetasDB;
GO

-- Usar la base de datos
USE RecetasDB;
GO

-- Crear la tabla EstadoReceta (Activa, Vencida, Entregada)
CREATE TABLE EstadoRecetas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Descripcion VARCHAR(50) NOT NULL
);
GO

-- Crear la tabla Recetas
CREATE TABLE Recetas (
    Id INT PRIMARY KEY IDENTITY(1,1),
    CodigoUnico VARCHAR(50) NOT NULL,
    FechaEmision DATETIME NOT NULL,
    EstadoRecetaId INT NOT NULL,
    MedicoId INT NOT NULL,  -- Referencia al microservicio de Personas (ID del Médico)
    PacienteId INT NOT NULL,  -- Referencia al microservicio de Personas (ID del Paciente)
    CitaId INT NOT NULL,  -- Referencia al microservicio de Citas (ID de la Cita)
    Descripcion TEXT NOT NULL,
    CONSTRAINT FK_EstadoReceta FOREIGN KEY (EstadoRecetaId) REFERENCES EstadoRecetas(Id)
);
GO

-- Insertar datos en la tabla EstadoReceta
INSERT INTO EstadoRecetas (Descripcion) VALUES ('Activa');
INSERT INTO EstadoRecetas (Descripcion) VALUES ('Vencida');
INSERT INTO EstadoRecetas (Descripcion) VALUES ('Entregada');
GO
