-- Crear la base de datos
CREATE DATABASE MiGimnasioDB;
GO

-- Seleccionar la base de datos
USE MiGimnasioDB;
GO

-- Tabla TipoUsuarios
CREATE TABLE TipoUsuarios (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion NVARCHAR(50) NOT NULL
);

-- Tabla Planes
CREATE TABLE Planes (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion NVARCHAR(255) NOT NULL,
    Maquinas BIT NOT NULL,
    Seguimiento BIT NOT NULL,
    Locker BIT NOT NULL,
    DescuentoClases INT NOT NULL,
    Importe INT NOT NULL
);

-- Tabla Usuarios
CREATE TABLE Usuarios (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(50) NOT NULL,
    Apellido NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Clave NVARCHAR(255) NOT NULL,
    TipoUsuario INT,
    ID_Plan INT,
    Activo BIT NOT NULL,
    FOREIGN KEY (ID_Plan) REFERENCES Planes(ID)
);

-- Tabla Cupones
CREATE TABLE Cupones (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Codigo NVARCHAR(4) NOT NULL UNIQUE, -- C�digo de una letra y tres n�meros
    Descuento INT NOT NULL,
    FechaVencimiento DATE NOT NULL,
    Activo BIT NOT NULL
);

-- Tabla Clases
CREATE TABLE Clases (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    FechaHorario DATETIME NOT NULL,
    Capacidad INT NOT NULL,
    Importe INT NOT NULL,
    Descripcion NVARCHAR(255),
    Activo BIT NOT NULL
);

-- Tabla InscripcionesClases
CREATE TABLE InscripcionesClases (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    ID_Usuario INT,
    ID_Clase INT,
    ID_Plan INT,
    DescuentoPlan INT NOT NULL,
    Cancelado BIT NOT NULL,
    FOREIGN KEY (ID_Usuario) REFERENCES Usuarios(ID),
    FOREIGN KEY (ID_Clase) REFERENCES Clases(ID),
    FOREIGN KEY (ID_Plan) REFERENCES Planes(ID)
);

-- Tabla Pagos
CREATE TABLE Pagos (
    Mes INT NULL,
    Anio INT NULL,
    ID_Cupon INT NULL,
    ID_Usuario INT NOT NULL,
    Importe INT NOT NULL,
    FechaPago DATETIME NULL,
    ID_InscripcionClase INT NULL,
    Pago BIT NOT NULL,
    FOREIGN KEY (ID_Usuario) REFERENCES Usuarios(ID),
);



-- Insertar datos en la tabla TipoUsuarios
INSERT INTO TipoUsuarios (Descripcion) VALUES ('Admin');
INSERT INTO TipoUsuarios (Descripcion) VALUES ('Usuario');

-- Insertar datos en la tabla Planes
INSERT INTO Planes (Descripcion, Maquinas, Seguimiento, Locker, DescuentoClases, Importe) 
VALUES 
('Plan B�sico', 1, 1, 0, 0, 1000),
('Plan Est�ndar', 1, 1, 1, 15, 2000),
('Plan Premium', 1, 1, 1, 30, 3000);

-- Insertar datos en la tabla Usuarios
-- Aqu� puedes cambiar los valores de 'Email', 'Clave', y 'Nombre/Apellido' por valores reales
INSERT INTO Usuarios (Nombre, Apellido, Email, Clave, TipoUsuario, ID_Plan, Activo) 
VALUES 
('Juan', 'P�rez', 'juan.perez@example.com', '12345', 1, 1, 1),
('Ana', 'G�mez', 'ana.gomez@example.com', '67890', 2, 2, 1),
('Carlos', 'L�pez', 'carlos.lopez@example.com', 'abcde', 2, 3, 1);

-- Insertar datos en la tabla Cupones
INSERT INTO Cupones (Codigo, Descuento, FechaVencimiento, Activo) 
VALUES 
('A001', 10, '2024-12-31', 1),
('B022', 20, '2024-11-30', 1),
('G004', 20, '2024-11-30', 1),
('T012', 20, '2024-10-31', 0),
('W007', 30, '2024-11-24', 0);

-- Insertar datos en la tabla Clases
INSERT INTO Clases (FechaHorario, Capacidad, Importe, Descripcion, Activo) 
VALUES 
('2024-10-26 18:00', 5, 400, 'Clase de Danza', 0),
('2024-12-15 10:00', 20, 500, 'Clase de Yoga', 1),
('2024-12-31 15:00', 15, 700, 'Clase de Spinning', 1),
('2024-11-30 18:00', 25, 600, 'Clase de Crossfit', 1),
('2025-01-15 10:00', 20, 500, 'Clase de funcional', 1),
('2025-02-31 15:00', 15, 700, 'Clase de boxeo', 1),
('2024-11-25 18:00', 25, 600, 'Clase de zumba', 0);


-- Insertar datos en la tabla InscripcionesClases
INSERT INTO InscripcionesClases (ID_Usuario, ID_Clase, ID_Plan, DescuentoPlan, Cancelado) 
VALUES 
(7,26,1,0,0),
(9,25,3,30,0),
(7,25,1,0,0),
(8,25,2,15,0);


-- Insertar datos en la tabla Pagosvb 
INSERT INTO Pagos (Mes, Anio, ID_Cupon, ID_Usuario, Importe, FechaPago, ID_InscripcionClase, Pago) 
VALUES 


(10, 2024, 1, 7, 1000, '2024-11-01', null, 1),
(10, 2024, 2, 8, 1700, '2024-11-01', null, 1),
(10, 2024, 0, 9, 1700, '2024-11-01', null, 1),
(11, 2024, 0, 7, 1000, '9999-09-09', null, 0),
(11, 2024, 3, 8, 1700, '9999-09-09', null, 0),
(11, 2024, 0, 9, 1700, '9999-09-09', null, 0),
(NULL,NULL,0,9,420,'9999-09-09',24,0),
(NULL,NULL,0,8,510,'9999-09-09',23,0),
(NULL,NULL,0,7,600,'2024-11-26',22,1),
(NULL,NULL,0,7,400,'9999-09-09',25,1);


--SP


CREATE PROCEDURE insertarNuevoUsuario
  @clave VARCHAR(1000),
  @TipoUsuario INT,
  @Nombre VARCHAR(1000),
  @Apellido VARCHAR(1000),
  @Email VARCHAR(1000),
  @Id_plan INT -- Agregar el ID del plan
AS
BEGIN
    INSERT INTO USUARIOS
      (Clave, TipoUsuario, Nombre, Apellido, Email, Id_plan, Activo)
    OUTPUT
      inserted.Id
    VALUES
      (@clave, @TipoUsuario, @Nombre, @Apellido, @Email, @Id_plan, 1) -- Estado se establece en 1 (activo)
END

-----------

GO
CREATE PROCEDURE ModificarUsuario
  @id INT,
  @clave VARCHAR(1000),
  @TipoUsuario INT,
  @Nombre VARCHAR(1000),
  @Apellido VARCHAR(1000),
  @Email VARCHAR(1000),
  @ID_Plan INT,         
  @Activo BIT           
AS
BEGIN
  UPDATE USUARIOS
  SET Nombre = @Nombre,
      Apellido = @Apellido,
      Clave = @clave,
      TipoUsuario = @TipoUsuario,
      Email = @Email,
      ID_Plan = @ID_Plan,   
      Activo = @Activo   
  WHERE ID = @id;         
END

-----------

CREATE PROCEDURE insertarNuevaClase
  @FechaHorario DATETIME,
  @Capacidad INT,
  @Importe INT,
  @Descripcion NVARCHAR(255),
  @Activo BIT
AS
BEGIN
    INSERT INTO Clases
      (FechaHorario, Capacidad, Importe, Descripcion, Activo)
    OUTPUT
      inserted.ID
    VALUES
      (@FechaHorario, @Capacidad, @Importe, @Descripcion, @Activo)
END

-----------
GO
CREATE PROCEDURE ModificarClase
  @id INT,
  @FechaHorario DATETIME,
  @Capacidad INT,
  @Importe INT,
  @Descripcion NVARCHAR(255),
  @Activo BIT           
AS
BEGIN
  UPDATE Clases
  SET FechaHorario = @FechaHorario,
      Capacidad = @Capacidad,
      Importe = @Importe,
      Descripcion = @Descripcion,
      Activo = @Activo   
  WHERE ID = @id;         
END
GO
-----------
CREATE PROCEDURE ActualizarEstadoClases
AS
BEGIN
    -- Actualizar todas las clases cuya fecha y hora ya pasaron, estableciendo Activo en false
    UPDATE Clases
    SET Activo = 0
    WHERE FechaHorario < GETDATE();
END;

-----------
CREATE PROCEDURE insertarNuevaInscripcionClase
    @ID_Usuario INT,
    @ID_Clase INT,
    @ID_Plan INT,
    @DescuentoPlan INT,
    @Cancelado BIT
AS
BEGIN
    INSERT INTO InscripcionesClases
        (ID_Usuario, ID_Clase, ID_Plan, DescuentoPlan, Cancelado)
    OUTPUT
        inserted.ID
    VALUES
        (@ID_Usuario, @ID_Clase, @ID_Plan, @DescuentoPlan, @Cancelado)
END

-----------
CREATE PROCEDURE contarInscriptosPorClase
    @ID_Clase INT
AS
BEGIN
    SELECT COUNT(*) AS CantidadInscriptos
    FROM InscripcionesClases
    WHERE ID_Clase = @ID_Clase AND Cancelado = 0;
END

-----------
CREATE PROCEDURE verificarInscripcionClase
    @ID_Clase INT,
    @ID_Usuario INT
AS
BEGIN
    SELECT COUNT(*)
    FROM InscripcionesClases
    WHERE ID_Clase = @ID_Clase 
      AND ID_Usuario = @ID_Usuario
      AND Cancelado = 0;
END
-----------

CREATE PROCEDURE sp_ListarClasesPorUsuario
    @ID_Usuario INT
AS
BEGIN
    SELECT C.Id, C.FechaHorario, C.Capacidad, C.Importe, C.Descripcion, C.Activo
    FROM Clases C
    INNER JOIN InscripcionesClases IC ON C.Id = IC.ID_Clase
    WHERE IC.ID_Usuario = @ID_Usuario AND IC.Cancelado = 0 AND C.Activo = 1;
END;

-----------
CREATE PROCEDURE sp_CancelarInscripcionClase
    @IdUsuario INT,
    @IdClase INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Actualiza la inscripci�n para el usuario y la clase especificados, marc�ndola como cancelada.
    UPDATE InscripcionesClases
    SET Cancelado = 1
    WHERE Id_usuario = @IdUsuario AND Id_clase = @IdClase;
    
    -- Retorna el n�mero de filas afectadas (opcional, para verificar si se realiz� la actualizaci�n).
    SELECT @@ROWCOUNT AS FilasAfectadas;
END;

-----------

CREATE PROCEDURE insertarNuevoCupon
    @Codigo NVARCHAR(4),
    @Descuento INT,
    @FechaVencimiento DATE,
    @Activo BIT
AS
BEGIN
    INSERT INTO Cupones (Codigo, Descuento, FechaVencimiento, Activo)
    VALUES (@Codigo, @Descuento, @FechaVencimiento, @Activo);

    -- Devolver el ID del nuevo cup�n
    SELECT SCOPE_IDENTITY();
END;

-----------
CREATE PROCEDURE ActualizarEstadoCupones
AS
BEGIN
    -- Actualizar todos los cupones cuya fecha de vencimiento ya haya pasado, estableciendo Activo en false
    UPDATE Cupones
    SET Activo = 0
    WHERE FechaVencimiento < GETDATE();
END;

-----------
CREATE PROCEDURE DesactivarCuponPorId
    @CuponId INT
AS
BEGIN
    -- Actualiza el estado de Activo a false (0) para el cup�n con el ID especificado
    UPDATE Cupones
    SET Activo = 0
    WHERE ID = @CuponId;
END;


-----------


CREATE PROCEDURE ObtenerPagosMensuales
AS
BEGIN
    SELECT 
        p.Mes,
        p.Anio,
        p.ID_Cupon,
        p.ID_Usuario,
        p.Importe,
        p.FechaPago,
        p.Pago,
        u.Nombre,
        u.Apellido,
        pl.Descripcion AS PlanDescripcion
    FROM 
        Pagos p
    JOIN 
        Usuarios u ON p.ID_Usuario = u.ID
    LEFT JOIN 
        Planes pl ON u.ID_Plan = pl.ID
    WHERE 
        p.ID_InscripcionClase = 0;
END;

-----------

CREATE PROCEDURE ObtenerPagosMensuales
AS
BEGIN
    SELECT 
        p.Mes,
        p.Anio,
        p.ID_Cupon,
        p.ID_Usuario,
        p.Importe,
        p.FechaPago,
        p.Pago,
        p.ID_InscripcionClase,  -- A�ade esta l�nea para incluir el campo faltante
        u.Nombre,
        u.Apellido,
        pl.Descripcion AS PlanDescripcion
    FROM 
        Pagos p
    JOIN 
        Usuarios u ON p.ID_Usuario = u.ID
    LEFT JOIN 
        Planes pl ON u.ID_Plan = pl.ID
    WHERE 
         p.ID_InscripcionClase IS NULL;
END;


--------------------------
CREATE PROCEDURE FiltrarPagosMensuales
    @UsuarioId INT = NULL,
    @Mes TINYINT = NULL,
    @Anio SMALLINT = NULL,
    @Pago BIT = NULL,
    @PlanId INT = NULL
AS
BEGIN
    SELECT 
        p.Mes,
        p.Anio,
        p.ID_Cupon,
        p.ID_Usuario,
        p.Importe,
        p.FechaPago,
        p.Pago,
        u.Nombre,
        u.Apellido,
        pl.Descripcion AS PlanDescripcion
    FROM 
        Pagos p
    JOIN 
        Usuarios u ON p.ID_Usuario = u.ID
    LEFT JOIN 
        Planes pl ON u.ID_Plan = pl.ID
    WHERE 
        p.ID_InscripcionClase IS NULL
        AND (@UsuarioId IS NULL OR p.ID_Usuario = @UsuarioId)
        AND (@Mes IS NULL OR p.Mes = @Mes)
        AND (@Anio IS NULL OR p.Anio = @Anio)
        AND (@Pago IS NULL OR p.Pago = @Pago)
        AND (@PlanId IS NULL OR u.ID_Plan = @PlanId);
END;


---------------------------------
CREATE PROCEDURE FiltrarPagosPorClase
    @UsuarioId INT = NULL,
    @Pago BIT = NULL,
    @ClaseId INT = NULL
AS
BEGIN
    SELECT 
        p.ID_InscripcionClase,
        p.ID_Cupon,
        p.ID_Usuario,
        p.Importe,
        p.FechaPago,
        p.Pago,
        u.Nombre,
        u.Apellido,
        c.Descripcion AS ClaseDescripcion
    FROM 
        Pagos p
    JOIN 
        Usuarios u ON p.ID_Usuario = u.ID
    LEFT JOIN 
        InscripcionesClases ic ON p.ID_InscripcionClase = ic.ID
    LEFT JOIN 
        Clases c ON ic.ID_Clase = c.ID
    WHERE 
        p.Mes IS NULL
        AND p.Anio IS NULL
        AND (@UsuarioId IS NULL OR p.ID_Usuario = @UsuarioId)
        AND (@Pago IS NULL OR p.Pago = @Pago)
        AND (@ClaseId IS NULL OR c.ID = @ClaseId);
END;


----------------------------------------------
use MiGimnasioDB
CREATE PROCEDURE insertarNuevoPago
    @Mes INT,
    @Anio INT,
    @ID_Cupon INT,
    @ID_Usuario INT,
    @Importe INT,
    @FechaPago DATETIME,
    @ID_InscripcionClase INT,
    @Pagado BIT
AS
BEGIN
    INSERT INTO Pagos
        (Mes, Anio, ID_Cupon, ID_Usuario, Importe, FechaPago, ID_InscripcionClase, Pago)
    VALUES
        (@Mes, @Anio, @ID_Cupon, @ID_Usuario, @Importe, @FechaPago, @ID_InscripcionClase, @Pagado)
END


----------------------------------------------
use MiGimnasioDB


CREATE PROCEDURE PagarMes
    @Mes INT,
    @Anio INT,
    @ID_Usuario INT,
	@ImporteFinal INT,
	@ID_Cupon INT = NULL -- Par�metro opcional
AS
BEGIN
    -- Actualiza el estado de Pago a true para los registros que coincidan con Mes, A�o y Usuario
    UPDATE Pagos
    SET 
        Pago = 1, -- Cambia el estado de pago a true
        FechaPago = GETDATE(), -- Opcional: establece la fecha de pago como la fecha actual
		Importe = @ImporteFinal,
		ID_Cupon = COALESCE(@ID_Cupon, ID_Cupon) -- Actualiza ID_Cupon solo si se proporciona un valor
    WHERE 
        Mes = @Mes
        AND Anio = @Anio
        AND ID_Usuario = @ID_Usuario; -- Filtra por Mes, A�o y Usuario
END;

----------------------------------------------
use MiGimnasioDB

CREATE PROCEDURE PagarClase
    @ID_InscripcionClase INT,
    @ID_Usuario INT,
    @ImporteFinal INT,
    @ID_Cupon INT = NULL -- Par�metro opcional
AS
BEGIN
    -- Actualiza el estado de Pago a true para los registros que coincidan con ID_Usuario e ID_InscripcionClase
    UPDATE Pagos
    SET 
        Pago = 1, -- Cambia el estado de pago a true
        FechaPago = GETDATE(), -- Establece la fecha de pago como la fecha actual
        Importe = @ImporteFinal,
        ID_Cupon = COALESCE(@ID_Cupon, ID_Cupon) -- Actualiza ID_Cupon solo si se proporciona un valor
    WHERE 
        ID_Usuario = @ID_Usuario
        AND ID_InscripcionClase = @ID_InscripcionClase;
END;


