/*	Base De Datos Estacionamiento
	Clase: Programacion de Negocios
	II Parcial
	Integrantes: Diego Buezo
				 Mayra Guevara
				 Ada Sorto

*/
--Seleccionar la Base de Datos por defecto
USE tempdb
GO

--Crear la Base de Datos
IF NOT EXISTS(SELECT * FROM sys.databases WHERE [name]='Estacionamiento')
	BEGIN
		CREATE DATABASE Estacionamiento
	END
GO

--Seleccionar la Base de Datos Estacionamiento
USE Estacionamiento
GO

--Crear el esquema Est
CREATE SCHEMA Est
GO

--Crear tabla para el Tipo del Vehiculo
CREATE TABLE Est.Tipo_Vehiculo
(
	id INT IDENTITY(1, 1) NOT NULL UNIQUE,
		CONSTRAINT PK_Vehiculo_TipoVehiculo_id
		PRIMARY KEY CLUSTERED (id),
	tipo NVARCHAR (30) NOT NULL
)
GO

--Crear la tabla para los Vehiculos
CREATE TABLE Est.Vehiculo
(
	id INT IDENTITY (1, 1) NOT NULL
		CONSTRAINT PK_Vehiculo_Estacionamiento_id
		PRIMARY KEY CLUSTERED (id),
	tipoVehiculo INT NOT NULL,
	placa NVARCHAR(8) NOT NULL UNIQUE,
		CONSTRAINT CHK_Formato_Placa_Vehiculo
		CHECK (placa LIKE '[A-Z][A-Z][A-Z]-[0-9][0-9][0-9][0-9]'),
	estado BIT NOT NULL
)
GO

--Crear la tabla Para el Pago por Vehiculo
CREATE TABLE Est.Pago_Vehiculo
(
	id INT IDENTITY(1, 1) NOT NULL
		CONSTRAINT PK_Vehiculo_PagoVehiculo_id
		PRIMARY KEY CLUSTERED (id),
	vehiculo INT NOT NULL,
	fechaHoraEntrada DATETIME NOT NULL,
	fechaHoraSalida DATETIME NOT NULL,
	total DECIMAL NOT NULL
)
GO

