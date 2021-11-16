create database Proyecto1;
go
use Proyecto1;
go

create table Perfil(
Id_Perfil integer not null primary key,
nombre varchar(50) not null
);
GO

INSERT INTO Perfil(Id_Perfil, nombre) VALUES(1, 'Administrador'), (2, 'Operador') -- O LO QUE SEA xD
GO

create table Usuarios(
Id_Usuario integer not null primary key identity(1,1),
nombre varchar(50) not null,
apellidos varchar(75) not null,
userName varchar(50) not null UNIQUE,
clave varchar(50) not null,
Id_Perfil int not null foreign key references Perfil(Id_Perfil)
);
GO

INSERT INTO Usuarios(nombre, apellidos, userName, clave, Id_Perfil)
VALUES('Admin', 'Admin', 'admin', '12345', 1),
('Operador', 'Operador', 'operador', '12345', 2);
GO

create table Comercio(
Id_comercio integer not null primary key identity(1,1),
Nombre varchar(100) not null,
Descripcion varchar(255) not null,
Numero_Telefono integer not null, 
);
go

CREATE TABLE ComercioUsuario(
Id integer not null primary key identity(1,1),
idUsuario  integer not null foreign key references Usuarios(Id_Usuario),
idComercio  integer not null foreign key references Comercio(Id_comercio),
fechaRegistro datetime NOT NULL DEFAULT(GETDATE()),
estado bit not null default(1),


)

create table Productos(
Id_producto integer not null primary key identity(1,1),
Nombre varchar(50) not null,
Descripcion varchar(255) not null,
Cantidad integer not null,
Precio float not null,
Imagen image null,
);
go
create table Inventario(
id integer not null primary key identity(1,1),
producto integer not null foreign key references Productos(Id_producto),
Comercio integer not null foreign key references Comercio(Id_comercio),
cantidad integer not null,
estado bit default (1)--si esta agotado 0 sino 1
);

go
create table Venta(
codigo integer not null primary key identity(1,1),
fecha datetime not null,
total float not null,
comercio integer not null foreign key references Comercio(Id_comercio)
);
go
create table detalleVenta(
id integer not null primary key identity(1,1),
producto integer not null foreign key references Productos(Id_producto),
venta integer not null foreign key references Venta(codigo),
cantidad integer not null,
subtotal float not null,
);
