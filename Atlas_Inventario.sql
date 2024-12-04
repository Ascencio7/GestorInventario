-- Crear la base de ATLAS
CREATE DATABASE ATLAS_INVENTARIO
GO
USE ATLAS_INVENTARIO
GO

-- Tabla Estados
CREATE TABLE Estados(
    EstadoID INT PRIMARY KEY IDENTITY(1,1),
    Estado varchar(10) NOT NULL UNIQUE
);

-- Insertar Estados
INSERT INTO Estados (Estado) VALUES ('Activo'), ('Inactivo');

SELECT * FROM Estados


CREATE TABLE RolesUsuarios(
	RolID int primary key identity(1,1),
	Roles varchar(20) NOT NULL UNIQUE
);


-- Insertar datos
INSERT INTO RolesUsuarios(Roles) VALUES ('Administrador'),('Auxiliar');

SELECT * FROM RolesUsuarios


-- Crear la tabla de los usuarios
CREATE TABLE Usuarios(
	UserID INT IDENTITY(1,1) PRIMARY KEY,
	NombreUsuario VARCHAR(100) UNIQUE NOT NULL,
	Correo VARCHAR(150) NOT NULL UNIQUE,
	Contra VARBINARY(1000) NOT NULL,
	RolID INT NOT NULL,
	EstadoID INT NOT NULL,
	FOREIGN KEY (RolID) REFERENCES RolesUsuarios(RolID),
	FOREIGN KEY (EstadoID) REFERENCES Estados(EstadoID)
);

INSERT INTO Usuarios (NombreUsuario, Correo, Contra, RolID, EstadoID)  
VALUES('Vladimir Ascencio','vladi@atlas.com',ENCRYPTBYPASSPHRASE('atlas24', 'atlas'),1,1), -- Administrador
('Ruth Vaquerano','ruth@atlas.com',ENCRYPTBYPASSPHRASE('atlas24', 'atlas'),2,1); -- Auxiliar

SELECT * FROM Usuarios

-- Tabla de los productos
CREATE TABLE Productos(
	ProductoID INT PRIMARY KEY IDENTITY(1,1),
	Nombre_Producto VARCHAR(100) NOT NULL,
	Precio_Producto DECIMAL(10,2) NOT NULL,
	Cantidad_Producto INT NOT NULL,
	Total_Producto AS (Precio_Producto * Cantidad_Producto) PERSISTED,
	Proveedor VARCHAR(100) NOT NULL,
);


INSERT INTO Productos(Nombre_Producto,Precio_Producto,Cantidad_Producto,Proveedor) 
VALUES('Coca Cola', 78.23,78,'Coca-Cola Corp');

select * from Productos

---------------------------------------------------    PROCEDIMIENTOS ALMACENADOS DE USUARIOS -------------------------------------------------------------------


-- 1. Procedimiento para Iniciar Sesión
CREATE PROCEDURE spLogin1
    @correo VARCHAR(150),  -- Tamaño acorde a la columna Correo
    @pass VARCHAR(100)     -- Tamaño ajustado a la longitud descifrada
AS
BEGIN
    -- Evitar casos donde múltiples resultados puedan romper la lógica
    SELECT TOP 1 
        u.UserID,
        r.Roles AS Rol  -- Recupera el nombre del rol desde RolesUsuarios
    FROM Usuarios u
    INNER JOIN RolesUsuarios r ON u.RolID = r.RolID
    WHERE u.Correo = @correo
      AND CONVERT(VARCHAR(1000), DECRYPTBYPASSPHRASE('atlas24', u.Contra)) = @pass;
END;



-- 2. Mostrar los Usuarios: Solo mostrará los usuarios ACTIVOS
CREATE PROCEDURE MostrarUsuarios
AS
BEGIN
    SELECT
        U.UserID,
        U.NombreUsuario,
        U.Correo,
        U.Contra,
        R.Roles AS Rol,
        E.Estado AS Estado
    FROM Usuarios U
    INNER JOIN RolesUsuarios R ON U.RolID = R.RolID
    INNER JOIN Estados E ON U.EstadoID = E.EstadoID
    WHERE U.EstadoID = 1; -- Filtro correcto
END;


-- Ejecutar el procedimiento
EXEC MostrarUsuarios


-- 3. Mostrar los Usuarios: Solo mostrará los usuarios INACTIVOS
CREATE PROCEDURE MostrarUsuariosInactivos
AS
BEGIN 
	 SELECT
        U.UserID,
        U.NombreUsuario,
        U.Correo,
        U.Contra,
        R.Roles AS Rol,
        E.Estado AS Estado
    FROM Usuarios U
    INNER JOIN RolesUsuarios R ON U.RolID = R.RolID
    INNER JOIN Estados E ON U.EstadoID = E.EstadoID
    WHERE U.EstadoID = 2; -- Filtro correcto
END;


-- Ejecutar el procedimiento
EXEC MostrarUsuariosInactivos


-- 4. Obtener la contrasena desencriptada
CREATE PROCEDURE spObtenerUsuarioContrasenaDesencriptada
    @UserID INT
AS
BEGIN
    SELECT 
        U.UserID,
        U.NombreUsuario,
        U.Correo,
        CONVERT(VARCHAR(1000), DECRYPTBYPASSPHRASE('atlas24', U.Contra)) AS ContrasenaDesencriptada,
        RolID,
        EstadoID
    FROM Usuarios U
    WHERE U.UserID = @UserID;
END;


-- Ejecutar el procedimiento
EXEC spObtenerUsuarioContrasenaDesencriptada
	@UserID = 1;


-- 5. Procedimiento para Registrarse
CREATE PROCEDURE spRegistrarUsuario
    @NombreUsuario VARCHAR(100),
    @Correo VARCHAR(150),
    @Contra VARCHAR(100), -- Contraseña en texto plano
    @RolID INT,
    @EstadoID INT
AS
BEGIN
    BEGIN TRY
        -- Validar si ya existe un usuario con el mismo correo
        IF EXISTS (SELECT 1 FROM Usuarios WHERE Correo = @Correo)
        BEGIN
            THROW 50001, 'El correo ya está registrado.', 1;
        END

        -- Validar si ya existe un usuario con el mismo nombre de usuario
        IF EXISTS (SELECT 1 FROM Usuarios WHERE NombreUsuario = @NombreUsuario)
        BEGIN
            THROW 50002, 'El nombre de usuario ya está registrado.', 1;
        END

        -- Insertar el nuevo usuario con la contraseña cifrada
        INSERT INTO Usuarios (NombreUsuario, Correo, Contra, RolID, EstadoID)
        VALUES (
            @NombreUsuario, 
            @Correo, 
            ENCRYPTBYPASSPHRASE('atlas24', @Contra), -- Cifrado de la contraseña
            @RolID, 
            @EstadoID
        );

        PRINT 'Usuario registrado exitosamente.';
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        PRINT ERROR_MESSAGE();
    END CATCH
END;




-- 6. Editar el usuario
CREATE PROCEDURE spEditarUsuario
    @UserID INT,
    @NombreUsuario VARCHAR(100),
    @Correo VARCHAR(150),
    @Contra VARCHAR(100), -- Contraseña en texto plano (se cifrará)
    @RolID INT,
    @EstadoID INT
AS
BEGIN
    BEGIN TRY
        -- Validar si existe otro usuario con el mismo correo
        IF EXISTS (SELECT 1 FROM Usuarios WHERE Correo = @Correo AND UserID != @UserID)
        BEGIN
            THROW 50001, 'El correo ya está registrado para otro usuario.', 1;
        END

        -- Validar si existe otro usuario con el mismo nombre de usuario
        IF EXISTS (SELECT 1 FROM Usuarios WHERE NombreUsuario = @NombreUsuario AND UserID != @UserID)
        BEGIN
            THROW 50002, 'El nombre de usuario ya está registrado para otro usuario.', 1;
        END

        -- Actualizar el usuario
        UPDATE Usuarios
        SET 
            NombreUsuario = @NombreUsuario,
            Correo = @Correo,
            Contra = ENCRYPTBYPASSPHRASE('atlas24', @Contra), -- Cifrado de la contraseña
            RolID = @RolID,
            EstadoID = @EstadoID
        WHERE UserID = @UserID;

        PRINT 'Usuario editado exitosamente.';
    END TRY
    BEGIN CATCH
        -- Manejo de errores
        PRINT ERROR_MESSAGE();
    END CATCH
END;


-- 7. Activar a Usuarios Inactivos
CREATE PROCEDURE ActivarUsuario
    @UserID INT -- ID del usuario que se desea activar
AS
BEGIN
    -- Manejo de errores y transacción (opcional, pero recomendado)
    BEGIN TRY
        BEGIN TRANSACTION;

        -- Actualizar el estado del usuario
        UPDATE Usuarios
        SET EstadoID = 1 -- Supongamos que 1 significa "activo"
        WHERE UserID = @UserID;

        -- Confirmar la transacción
        COMMIT TRANSACTION;

        PRINT 'Usuario activado correctamente.';
    END TRY
    BEGIN CATCH
        -- Si ocurre un error, revertir la transacción
        ROLLBACK TRANSACTION;

        -- Mostrar el error
        PRINT 'Ocurrió un error al intentar activar al usuario.';
        PRINT ERROR_MESSAGE();
    END CATCH
END;




-- 8. Obtener la contrasena desencriptada con el ROL y ESTADO sin el ID
CREATE PROCEDURE ObtenerUsuarioContrasenaDesencriptada
    @UserID INT
AS
BEGIN
    SELECT 
        U.UserID,
        U.NombreUsuario,
        U.Correo,
        CONVERT(VARCHAR(1000), DECRYPTBYPASSPHRASE('atlas24', U.Contra)) AS ContrasenaDesencriptada,
        R.Roles AS Rol,
        E.Estado AS Estado
    FROM Usuarios U
	INNER JOIN RolesUsuarios R ON U.RolID = R.RolID
	INNER JOIN Estados E ON U.EstadoID = E.EstadoID
    WHERE U.UserID = @UserID;
END;



--------------- PROCEDIMIENTOS ALMACENADOS PARA LOS PRODUCTOS

-- 1. Mostrar los Productos
CREATE PROCEDURE MostrarProductos
AS
BEGIN
	SELECT
		P.ProductoID,
		P.Nombre_Producto,
		P.Precio_Producto,
		P.Cantidad_Producto,
		P.Proveedor,
		P.Total_Producto
	FROM Productos P
END;

EXEC MostrarProductos


-- 2. Insertar los productos
CREATE PROCEDURE InsertarProducto
	@Nombre_Producto VARCHAR(100),
    @Precio_Producto DECIMAL(10, 2),
    @Cantidad_Producto INT,
    @Proveedor VARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO Productos (Nombre_Producto, Precio_Producto, Cantidad_Producto, Proveedor)
    VALUES (@Nombre_Producto, @Precio_Producto, @Cantidad_Producto, @Proveedor);
    
    PRINT 'Producto insertado exitosamente.';
END;


-- 3. Editar los productos
CREATE PROCEDURE EditarProducto
    @ProductoID INT,
    @Nombre_Producto VARCHAR(100),
    @Precio_Producto DECIMAL(10, 2),
    @Cantidad_Producto INT,
    @Proveedor VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Productos WHERE ProductoID = @ProductoID)
    BEGIN
        UPDATE Productos
        SET Nombre_Producto = @Nombre_Producto,
            Precio_Producto = @Precio_Producto,
            Cantidad_Producto = @Cantidad_Producto,
            Proveedor = @Proveedor
        WHERE ProductoID = @ProductoID;

        PRINT 'Producto actualizado exitosamente.';
    END
    ELSE
    BEGIN
        PRINT 'El ProductoID no existe.';
    END
END;