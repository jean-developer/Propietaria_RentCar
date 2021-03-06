USE [RentCar]
GO
/****** Object:  Table [dbo].[AppRole]    Script Date: 6/24/2020 7:40:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AppUser]    Script Date: 6/24/2020 7:40:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AppUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[IdRole] [int] NULL,
	[Estado] [varchar](20) NULL,
 CONSTRAINT [PK_AppUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 6/24/2020 7:40:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Apellido] [varchar](50) NULL,
	[TipoDocumento] [varchar](10) NULL,
	[NoDocumento] [varchar](30) NULL,
	[NoTarjeta] [varchar](20) NULL,
	[LimiteCredito] [money] NULL,
	[TipoPersona] [varchar](20) NULL,
	[Estado] [varchar](10) NULL,
 CONSTRAINT [PK__Clientes__3214EC07F233B62C] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empleados]    Script Date: 6/24/2020 7:40:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empleados](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Apellido] [varchar](50) NULL,
	[TipoDocumento] [varchar](10) NULL,
	[NoDocumento] [varchar](30) NULL,
	[TandaLaboral] [varchar](30) NULL,
	[Comision] [money] NULL,
	[FechaIngreso] [varchar](20) NULL,
	[Estado] [varchar](10) NULL,
 CONSTRAINT [PK__Empleado__3214EC07DD12D893] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inspeccion]    Script Date: 6/24/2020 7:40:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inspeccion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdVehiculo] [int] NULL,
	[IdCliente] [int] NULL,
	[Ralladuras] [bit] NULL,
	[CantidadCombustible] [varchar](30) NULL,
	[GomaRepuesta] [bit] NULL,
	[Gato] [bit] NULL,
	[RoturaCristal] [bit] NULL,
	[GomaEstado] [varchar](30) NULL,
	[Comentario] [varchar](100) NULL,
	[FechaInspeccion] [datetime] NULL,
	[IdEmpleado] [int] NULL,
	[Estado] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Marcas]    Script Date: 6/24/2020 7:40:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Marcas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Descripcion] [varchar](100) NULL,
	[Estado] [varchar](10) NULL,
 CONSTRAINT [PK__Marcas__3214EC07C411839F] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Modelos]    Script Date: 6/24/2020 7:40:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Modelos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdMarcas] [int] NULL,
	[Descripcion] [varchar](100) NULL,
	[Estado] [varchar](10) NULL,
	[Nombre] [varchar](50) NULL,
 CONSTRAINT [PK__Modelos__3214EC07FEF8A7FE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Renta]    Script Date: 6/24/2020 7:40:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Renta](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdEmpleado] [int] NULL,
	[IdVehiculo] [int] NULL,
	[IdCliente] [int] NULL,
	[Comentario] [varchar](100) NULL,
	[FechaRenta] [datetime] NULL,
	[FechaDevolucion] [datetime] NULL,
	[MontoDiario] [money] NULL,
	[Dias] [int] NULL,
	[Estado] [varchar](10) NULL,
 CONSTRAINT [PK__Renta__3214EC0777BA1D6E] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoCombustible]    Script Date: 6/24/2020 7:40:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoCombustible](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](100) NULL,
	[Estado] [varchar](10) NULL,
	[Nombre] [varchar](50) NULL,
 CONSTRAINT [PK__TipoComb__3214EC07DE3B66A7] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoVehiculo]    Script Date: 6/24/2020 7:40:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoVehiculo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Estado] [varchar](10) NULL,
	[Descripcion] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehiculos]    Script Date: 6/24/2020 7:40:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehiculos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](100) NULL,
	[NoChasis] [varchar](20) NULL,
	[NoMotor] [varchar](20) NULL,
	[NoPlaca] [varchar](20) NULL,
	[Estado] [varchar](10) NULL,
	[IdTipoVehiculo] [int] NULL,
	[IdMarca] [int] NULL,
	[IdModelo] [int] NULL,
	[IdTipoCombustible] [int] NULL,
	[Nombre] [varchar](50) NULL,
 CONSTRAINT [PK__Vehiculo__3214EC07943721F3] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Inspeccion]  WITH CHECK ADD  CONSTRAINT [FK__Inspeccio__IdCli__4BAC3F29] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[Clientes] ([Id])
GO
ALTER TABLE [dbo].[Inspeccion] CHECK CONSTRAINT [FK__Inspeccio__IdCli__4BAC3F29]
GO
ALTER TABLE [dbo].[Inspeccion]  WITH CHECK ADD  CONSTRAINT [FK__Inspeccio__IdEmp__4AB81AF0] FOREIGN KEY([IdEmpleado])
REFERENCES [dbo].[Empleados] ([Id])
GO
ALTER TABLE [dbo].[Inspeccion] CHECK CONSTRAINT [FK__Inspeccio__IdEmp__4AB81AF0]
GO
ALTER TABLE [dbo].[Inspeccion]  WITH CHECK ADD  CONSTRAINT [FK__Inspeccio__IdVeh__49C3F6B7] FOREIGN KEY([IdVehiculo])
REFERENCES [dbo].[Vehiculos] ([Id])
GO
ALTER TABLE [dbo].[Inspeccion] CHECK CONSTRAINT [FK__Inspeccio__IdVeh__49C3F6B7]
GO
ALTER TABLE [dbo].[Modelos]  WITH CHECK ADD  CONSTRAINT [FK__Modelos__IdMarca__3B75D760] FOREIGN KEY([IdMarcas])
REFERENCES [dbo].[Marcas] ([Id])
GO
ALTER TABLE [dbo].[Modelos] CHECK CONSTRAINT [FK__Modelos__IdMarca__3B75D760]
GO
ALTER TABLE [dbo].[Renta]  WITH CHECK ADD  CONSTRAINT [FK__Renta__IdCliente__5070F446] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[Clientes] ([Id])
GO
ALTER TABLE [dbo].[Renta] CHECK CONSTRAINT [FK__Renta__IdCliente__5070F446]
GO
ALTER TABLE [dbo].[Renta]  WITH CHECK ADD  CONSTRAINT [FK__Renta__IdEmplead__4F7CD00D] FOREIGN KEY([IdEmpleado])
REFERENCES [dbo].[Empleados] ([Id])
GO
ALTER TABLE [dbo].[Renta] CHECK CONSTRAINT [FK__Renta__IdEmplead__4F7CD00D]
GO
ALTER TABLE [dbo].[Renta]  WITH CHECK ADD  CONSTRAINT [FK__Renta__IdVehicul__4E88ABD4] FOREIGN KEY([IdVehiculo])
REFERENCES [dbo].[Vehiculos] ([Id])
GO
ALTER TABLE [dbo].[Renta] CHECK CONSTRAINT [FK__Renta__IdVehicul__4E88ABD4]
GO
ALTER TABLE [dbo].[Vehiculos]  WITH CHECK ADD  CONSTRAINT [FK__Vehiculos__IdMar__4222D4EF] FOREIGN KEY([IdMarca])
REFERENCES [dbo].[Marcas] ([Id])
GO
ALTER TABLE [dbo].[Vehiculos] CHECK CONSTRAINT [FK__Vehiculos__IdMar__4222D4EF]
GO
ALTER TABLE [dbo].[Vehiculos]  WITH CHECK ADD  CONSTRAINT [FK__Vehiculos__IdMod__4316F928] FOREIGN KEY([IdModelo])
REFERENCES [dbo].[Modelos] ([Id])
GO
ALTER TABLE [dbo].[Vehiculos] CHECK CONSTRAINT [FK__Vehiculos__IdMod__4316F928]
GO
ALTER TABLE [dbo].[Vehiculos]  WITH CHECK ADD  CONSTRAINT [FK__Vehiculos__IdTip__403A8C7D] FOREIGN KEY([IdTipoVehiculo])
REFERENCES [dbo].[TipoVehiculo] ([Id])
GO
ALTER TABLE [dbo].[Vehiculos] CHECK CONSTRAINT [FK__Vehiculos__IdTip__403A8C7D]
GO
ALTER TABLE [dbo].[Vehiculos]  WITH CHECK ADD  CONSTRAINT [FK__Vehiculos__IdTip__412EB0B6] FOREIGN KEY([IdTipoCombustible])
REFERENCES [dbo].[TipoCombustible] ([Id])
GO
ALTER TABLE [dbo].[Vehiculos] CHECK CONSTRAINT [FK__Vehiculos__IdTip__412EB0B6]
GO
