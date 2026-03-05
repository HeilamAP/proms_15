-- Script de creación de base de datos y tablas para UTM Market
CREATE DATABASE UTMMarket;
GO

USE UTMMarket;
GO

CREATE TABLE Clientes (
    ClienteId INT PRIMARY KEY IDENTITY(1,1),
    NombreCompleto NVARCHAR(200) NOT NULL,
    Email NVARCHAR(150) NOT NULL UNIQUE,
    Activo BIT DEFAULT 1,
    FechaRegistro DATETIME DEFAULT GETDATE(),
    Telefono NVARCHAR(20),
    Direccion NVARCHAR(MAX)
);

CREATE TABLE Productos (
    ProductoId INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(MAX),
    Precio DECIMAL(18,2) NOT NULL,
    Stock INT NOT NULL,
    Categoria NVARCHAR(50),
    Activo BIT DEFAULT 1
);

CREATE TABLE Ventas (
    VentaId INT PRIMARY KEY IDENTITY(1,1),
    Folio NVARCHAR(50) NOT NULL UNIQUE,
    Fecha DATETIME DEFAULT GETDATE(),
    Total DECIMAL(18,2) NOT NULL,
    ClienteId INT NOT NULL,
    CONSTRAINT FK_Ventas_Clientes FOREIGN KEY (ClienteId) REFERENCES Clientes(ClienteId)
);

CREATE TABLE DetallesVenta (
    DetalleVentaId INT PRIMARY KEY IDENTITY(1,1),
    VentaId INT NOT NULL,
    ProductoId INT NOT NULL,
    Cantidad INT NOT NULL,
    PrecioUnitario DECIMAL(18,2) NOT NULL,
    Subtotal AS (Cantidad * PrecioUnitario),
    CONSTRAINT FK_Detalles_Ventas FOREIGN KEY (VentaId) REFERENCES Ventas(VentaId),
    CONSTRAINT FK_Detalles_Productos FOREIGN KEY (ProductoId) REFERENCES Productos(ProductoId)
);
GO
