CREATE DATABASE ProductosDb
GO
USE ProductosDb
GO
CREATE TABLE Producto
(
ProductoId int primary key identity,
Descripcion varchar(40),
Existencia int,
Costo decimal,
ValorInventario decimal
);