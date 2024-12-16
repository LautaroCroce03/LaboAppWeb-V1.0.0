--primero se hace el DELETE
delete from comandas
delete from pedidos
delete from estado_mesas
delete from mesas
delete from estado_pedidos
delete from empleados
delete from roles
delete from productos
delete from sectores


--ALTER
ALTER TABLE Empleados 
ADD estado BIT NOT NULL default 1;

--reiniciamos las claves
DBCC CHECKIDENT('estado_mesas' , RESEED, 0)
DBCC CHECKIDENT('mesas' , RESEED, 0)
DBCC CHECKIDENT('comandas' , RESEED, 0)
DBCC CHECKIDENT('pedidos' , RESEED, 0)
DBCC CHECKIDENT('estado_mesas' , RESEED, 0)
DBCC CHECKIDENT('empleados' , RESEED, 0)
DBCC CHECKIDENT('roles' , RESEED, 0)
DBCC CHECKIDENT('sectores' , RESEED, 0)
DBCC CHECKIDENT('productos' , RESEED, 0)
DBCC CHECKIDENT('estado_pedidos' , RESEED, 0)

--se hace el insert

INSERT INTO estado_mesas (Descripcion)
VALUES('Libre'),
    ('Reservada'),
    ('Ocupada'),
    ('En Uso - Comiendo'),
    ('Esperando Limpieza'),
    ('Limpieza en Proceso'),
    ('Inhabilitada'),
    ('Finalizado');
	
INSERT INTO estado_pedidos(Descripcion)
VALUES
    ('Pendiente'),
    ('En Preparación'),
    ('Listo para Servir'),
    ('Servido'),
    ('Cancelado'),
    ('Devuelto'),
    ('En Facturación'),
    ('Finalizado');	

INSERT INTO roles(descripcion) 
VALUES	('Bartender'),
		('Cervecero'),
		('Cocinero'),
		('Mozo'),
		('Socio'),
		('Administrador');

INSERT INTO Sectores (Descripcion)
VALUES
    ('Barra Tragos Y Vino'),
    ('Cerveza Artesanal'),
    ('Cocina'),
    ('Candybar'),
	('General');


INSERT INTO Productos (id_sector, Descripcion, Stock, Precio)
VALUES
    (1, 'Vino tinto Malbec', 50, 14000.00),
    (1, 'Vino tinto Cabernet', 40, 14000.00),
    (2, 'Cerveza artesanal IPA Roja', 200, 3700.00),
    (2, 'Cerveza artesanal Negra', 150, 3700.00),
    (3, 'Empanadas de Carne', 200, 1500.00),
    (3, 'Empanadas de Verdura', 100, 1500.00),
    (3, 'Empanadas de Pollo', 150, 1500.00),
    (4, 'Postre Tiramisú', 40, 5000.00),
    (4, 'Café', 400, 2500.00);	
	

	INSERT INTO Empleados (Nombre, Usuario, Password, id_sector, id_rol, estado)
VALUES
    ('Lucas González', 'bartender1', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 1, 1, 1),
    ('Sofía Martínez', 'cervecero1', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 2, 2, 1),
    ('Matías Romero', 'cocinero1', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 3, 3, 1),
    ('Camila Torres', 'mozo1', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 4, 4, 1),
    ('Ignacio Fernández', 'socio1', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 5, 5, 1),
    ('Valentina López', 'bartender2', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 1, 1, 0),
    ('Joaquín Díaz', 'socio2', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 5, 5, 1),
    ('Emilia Sánchez', 'cocinero2', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 3, 3, 1),
    ('Tomás Herrera', 'socio3', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 5, 5, 0);	