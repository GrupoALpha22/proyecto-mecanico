
# BASES DE DATOS

En la siguiente documentación se podrá encontrar paso a paso las querys usadas en sql server.



## CREAR BASE DE DATOS

Se debe ejecutar la siguiente query para crear una nueva base de datos:

```bash
  CREATE database ALPHA
```


## CREAR TABLA

Se debe ejecutar la siguiente query para crear la tabla personas:

```bash
  CREATE TABLE persona(
    Idpersona int identity PRIMARY KEY,
    Identificacion VARCHAR(50),
    Nombre VARCHAR(50),
    Apellido VARCHAR(50),
    anacimiento VARCHAR(50)
)

```

Se debe ejecutar la siguiente query para crear la tabla propietario:
```bash
  CREATE TABLE propietario(
     Idpropietario int  PRIMARY KEY,
     Idpersona int unique not null,
     Ciudad VARCHAR(50) not null,
     Email VARCHAR(50) not null,
     Foreign key(Idpersona)
     References persona(Idpersona)
)

```

## CREAR PROCEDIMIENTOS ALMACENADOS (SP)

Se debe ejecutar la siguiente query para crear el procedimiento almacenado que **muestre la información de la tabla personas**:

```bash
    CREATE procedure sp_listar
    as
    begin
        select * from persona
    end
```

Se debe ejecutar la siguiente query para crear el procedimiento almacenado que **muestre la información de una persona en particular**:

```bash
    CREATE procedure sp_Obtener(
    @Idpersona int
    )
    as
    begin
        select * from persona where Idpersona=@Idpersona
    end

```

Se debe ejecutar la siguiente query para crear el procedimiento almacenado para **insertar un nuevo registro en la tabla personas**:

```bash
    CREATE procedure sp_Guardar(
    @Identificacion VARCHAR(50),
    @Nombre VARCHAR(50),
    @Apellido VARCHAR(50),
    @anacimiento VARCHAR(50)
    )
    as
    begin
            insert into persona(Identificacion,Nombre,Apellido,anacimiento) values(@Identificacion,
    @Nombre,@Apellido,@anacimiento)
    end
```

Se debe ejecutar la siguiente query para crear el procedimiento almacenado para **insertar un nuevo registro en la tabla propietario**:

```
create procedure sp_GuardarPropietario(
@Identificacion VARCHAR(50),
@Nombre VARCHAR(50),
@Apellido VARCHAR(50),
@anacimiento VARCHAR(50),
@Ciudad varchar(50),
@Email varchar(50)
)
as
begin
insert into persona(Identificacion,Nombre,Apellido,anacimiento)values(@Identificacion,@Nombre,@Apellido,@anacimiento)
DECLARE @id VARCHAR(10) =  (SELECT MAX(Idpersona) from persona)

INSERT into propietario(Idpersona,Ciudad,Email) VALUES (@id,@Ciudad,@Email)
end

```

Se debe ejecutar la siguiente query para crear el procedimiento almacenado para **editar un registro en la tabla personas**:

```bash
    CREATE procedure sp_Editar(
    @Idpersona int,
    @Identificacion VARCHAR(50),
    @Nombre VARCHAR(50),
    @Apellido VARCHAR(50),
    @anacimiento VARCHAR(50)
    )
    as
    begin
            update persona set Identificacion =@Identificacion, Nombre=
    @Nombre,Apellido=@Apellido,anacimiento=@anacimiento where Idpersona=@Idpersona
    end
```

Se debe ejecutar la siguiente query para crear el procedimiento almacenado para **eliminar un registro en la tabla personas**:

```bash
    CREATE procedure sp_Eliminar(
    @Idpersona int
    )
    as
    begin
            delete from persona where Idpersona =@Idpersona
    end
```
