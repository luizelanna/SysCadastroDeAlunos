CREATE TABLE [AspNetRoles] (

[Id] nvarchar(128) COLLATE Latin1_General_CI_AS NOT NULL,

[Name] nvarchar(256) COLLATE Latin1_General_CI_AS NOT NULL,

CONSTRAINT [PK__AspNetRo__3214EC0774071F43] PRIMARY KEY ([Id]) 

)

GO



CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([Name] ASC)

GO

DBCC CHECKIDENT (N'[AspNetRoles]', RESEED, 1)

GO

ALTER TABLE [AspNetRoles] SET ( LOCK_ESCALATION = TABLE )

GO



CREATE TABLE [AspNetUserClaims] (

[Id] int NOT NULL IDENTITY(1,1),

[UserId] nvarchar(128) COLLATE Latin1_General_CI_AS NOT NULL,

[ClaimType] nvarchar(MAX) COLLATE Latin1_General_CI_AS NULL,

[ClaimValue] nvarchar(MAX) COLLATE Latin1_General_CI_AS NULL,

CONSTRAINT [PK__AspNetUs__3214EC07820CF2A6] PRIMARY KEY ([Id]) 

)

GO



CREATE INDEX [IX_UserId] ON [AspNetUserClaims] ([UserId] ASC)

GO

DBCC CHECKIDENT (N'[AspNetUserClaims]', RESEED, 1)

GO

ALTER TABLE [AspNetUserClaims] SET ( LOCK_ESCALATION = TABLE )

GO



CREATE TABLE [AspNetUserLogins] (

[LoginProvider] nvarchar(128) COLLATE Latin1_General_CI_AS NOT NULL,

[ProviderKey] nvarchar(128) COLLATE Latin1_General_CI_AS NOT NULL,

[UserId] nvarchar(128) COLLATE Latin1_General_CI_AS NOT NULL,

CONSTRAINT [PK__AspNetUs__663BD39E4FE9628E] PRIMARY KEY ([LoginProvider], [ProviderKey], [UserId]) 

)

GO



CREATE INDEX [IX_UserId] ON [AspNetUserLogins] ([UserId] ASC)

GO

DBCC CHECKIDENT (N'[AspNetUserLogins]', RESEED, 1)

GO

ALTER TABLE [AspNetUserLogins] SET ( LOCK_ESCALATION = TABLE )

GO



CREATE TABLE [AspNetUserRoles] (

[UserId] nvarchar(128) COLLATE Latin1_General_CI_AS NOT NULL,

[RoleId] nvarchar(128) COLLATE Latin1_General_CI_AS NOT NULL,

CONSTRAINT [PK__AspNetUs__AF2760AD89364F31] PRIMARY KEY ([UserId], [RoleId]) 

)

GO



CREATE INDEX [IX_UserId] ON [AspNetUserRoles] ([UserId] ASC)

GO

CREATE INDEX [IX_RoleId] ON [AspNetUserRoles] ([RoleId] ASC)

GO

DBCC CHECKIDENT (N'[AspNetUserRoles]', RESEED, 1)

GO

ALTER TABLE [AspNetUserRoles] SET ( LOCK_ESCALATION = TABLE )

GO



CREATE TABLE [AspNetUsers] (

[Id] nvarchar(128) COLLATE Latin1_General_CI_AS NOT NULL,

[Email] nvarchar(256) COLLATE Latin1_General_CI_AS NULL,

[EmailConfirmed] bit NOT NULL,

[PasswordHash] nvarchar(MAX) COLLATE Latin1_General_CI_AS NULL,

[SecurityStamp] nvarchar(MAX) COLLATE Latin1_General_CI_AS NULL,

[PhoneNumber] nvarchar(MAX) COLLATE Latin1_General_CI_AS NULL,

[PhoneNumberConfirmed] bit NOT NULL,

[TwoFactorEnabled] bit NOT NULL,

[LockoutEndDateUtc] datetime NULL,

[LockoutEnabled] bit NOT NULL,

[AccessFailedCount] int NOT NULL,

[UserName] nvarchar(256) COLLATE Latin1_General_CI_AS NOT NULL,

CONSTRAINT [PK__AspNetUs__3214EC072314F6A9] PRIMARY KEY ([Id]) 

)

GO



CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([UserName] ASC)

GO

DBCC CHECKIDENT (N'[AspNetUsers]', RESEED, 1)

GO

ALTER TABLE [AspNetUsers] SET ( LOCK_ESCALATION = TABLE )

GO



CREATE TABLE [Cidades] (

[id] int NOT NULL IDENTITY(1,1),

[NomeCidade] varchar(50) COLLATE Latin1_General_CI_AS NULL,

[Estado] varchar(50) COLLATE Latin1_General_CI_AS NULL,

[Cep] varchar(10) COLLATE Latin1_General_CI_AS NULL,

CONSTRAINT [PK__Cidades__3213E83F3C098FC3] PRIMARY KEY ([id]) 

)

GO



DBCC CHECKIDENT (N'[Cidades]', RESEED, 1001)

GO

ALTER TABLE [Cidades] SET ( LOCK_ESCALATION = TABLE )

GO



CREATE TABLE [Pessoas] (

[id] int NOT NULL IDENTITY(1,1),

[idPessoa] int NULL,

[idTpoPessoa] int NULL,

[idUsuarioUpdate] nvarchar(128) COLLATE Latin1_General_CI_AS NULL,

[Nome] varchar(100) COLLATE Latin1_General_CI_AS NULL,

[Rg] varchar(20) COLLATE Latin1_General_CI_AS NULL,

[Cpf] varchar(20) COLLATE Latin1_General_CI_AS NULL,

[DataNascimento] datetime NULL,

[Matricula] varchar(50) COLLATE Latin1_General_CI_AS NULL,

[Idade] int NULL,

[Sexo] varchar(20) COLLATE Latin1_General_CI_AS NULL,

[Profissao] varchar(100) COLLATE Latin1_General_CI_AS NULL,

[DataCadastro] datetime NULL,

[DataAlteração] datetime NULL,

[Telefone] varchar(20) COLLATE Latin1_General_CI_AS NULL,

[Celular1] varchar(20) COLLATE Latin1_General_CI_AS NULL,

[Celular2] varchar(20) COLLATE Latin1_General_CI_AS NULL,

[Logradouro] varchar(100) COLLATE Latin1_General_CI_AS NULL,

[Numero] varchar(10) COLLATE Latin1_General_CI_AS NULL,

[Bairro] varchar(100) COLLATE Latin1_General_CI_AS NULL,

[Complemento] varchar(100) COLLATE Latin1_General_CI_AS NULL,

[Cidade] varchar(50) COLLATE Latin1_General_CI_AS NULL,

[Estado] varchar(50) COLLATE Latin1_General_CI_AS NULL,

[Cep] varchar(10) COLLATE Latin1_General_CI_AS NULL,

CONSTRAINT [PK__Pessoas__3213E83F7FC17DE3] PRIMARY KEY ([id]) 

)

GO



DBCC CHECKIDENT (N'[Pessoas]', RESEED, 1015)

GO

ALTER TABLE [Pessoas] SET ( LOCK_ESCALATION = TABLE )

GO



CREATE TABLE [TipoPessoas] (

[id] int NOT NULL IDENTITY(1,1),

[Descricao] varchar(20) COLLATE Latin1_General_CI_AS NULL,

CONSTRAINT [PK__TipoPess__3213E83FDBC663A6] PRIMARY KEY ([id]) 

)

GO



DBCC CHECKIDENT (N'[TipoPessoas]', RESEED, 1001)

GO

ALTER TABLE [TipoPessoas] SET ( LOCK_ESCALATION = TABLE )

GO





ALTER TABLE [AspNetUserClaims] ADD CONSTRAINT [FK__AspNetUse__UserI__2D27B809] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION

GO

ALTER TABLE [AspNetUserLogins] ADD CONSTRAINT [FK__AspNetUse__UserI__300424B4] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION

GO

ALTER TABLE [AspNetUserRoles] ADD CONSTRAINT [FK__AspNetUse__RoleI__32E0915F] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION

GO

ALTER TABLE [AspNetUserRoles] ADD CONSTRAINT [FK__AspNetUse__UserI__33D4B598] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION

GO

ALTER TABLE [Pessoas] ADD CONSTRAINT [fk_Pessoas_AspNetUsers_1] FOREIGN KEY ([idUsuarioUpdate]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION

GO

ALTER TABLE [Pessoas] ADD CONSTRAINT [fk_Pessoas_TipoPessoas_1] FOREIGN KEY ([idTpoPessoa]) REFERENCES [TipoPessoas] ([id]) ON DELETE NO ACTION ON UPDATE NO ACTION

GO



