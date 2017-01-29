/* ---------------------------------------------------------------------- */
/* Database                                                                 */
/* ---------------------------------------------------------------------- */

CREATE DATABASE headhacks;
GO

USE headhacks;

/* ---------------------------------------------------------------------- */
/* Tables                                                                 */
/* ---------------------------------------------------------------------- */

/* ---------------------------------------------------------------------- */
/* Add table "pelicules"                                                  */
/* ---------------------------------------------------------------------- */

CREATE TABLE [pelicules] (
    [id] UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL,
    [portada] VARCHAR(256),
    [titol] VARCHAR(256) NOT NULL,
    [director] VARCHAR(256),
    [guio] VARCHAR(256),
    [musica] VARCHAR(256),
    [interprets] VARCHAR(256),
    [trama] TEXT,
    [genere] VARCHAR(256),
    [any_estrena] VARCHAR(256),
    [duracio] VARCHAR(256),
    [pais] VARCHAR(256),
    [enllaç_en_linia] VARCHAR(256),
    [enllaç_descarrega] VARCHAR(256),
    CONSTRAINT [PK_pelicules] PRIMARY KEY ([id]),
    CONSTRAINT [TUC_pelicules_1] UNIQUE ([titol])
)
GO


/* ---------------------------------------------------------------------- */
/* Add table "registre_edicions"                                          */
/* ---------------------------------------------------------------------- */

CREATE TABLE [registre_edicions] (
    [id] UNIQUEIDENTIFIER CONSTRAINT [DEF_registre_edicions_id] DEFAULT NEWID() NOT NULL,
    [id_usuari] UNIQUEIDENTIFIER NOT NULL,
    [id_pelicula] UNIQUEIDENTIFIER NOT NULL,
    [data_hora] DATETIME,
    [portada] VARCHAR(256),
    [titol] VARCHAR(256),
    [director] VARCHAR(256),
    [guio] VARCHAR(256),
    [musica] VARCHAR(256),
    [interprets] VARCHAR(256),
    [trama] TEXT,
    [genere] VARCHAR(256),
    [any_estrena] VARCHAR(256),
    [duracio] VARCHAR(256),
    [pais] VARCHAR(256),
    [enllaç_en_linia] VARCHAR(256),
    [enllaç_descarrega] VARCHAR(256),
    CONSTRAINT [PK_registre_edicions] PRIMARY KEY ([id], [id_usuari], [id_pelicula])
)
GO


/* ---------------------------------------------------------------------- */
/* Add table "pelicules_vistes"                                           */
/* ---------------------------------------------------------------------- */

CREATE TABLE [pelicules_vistes] (
    [id_usuari] UNIQUEIDENTIFIER NOT NULL,
    [id_pelicula] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_pelicules_vistes] PRIMARY KEY ([id_usuari], [id_pelicula])
)
GO


/* ---------------------------------------------------------------------- */
/* Add table "pelicules_a_veure"                                          */
/* ---------------------------------------------------------------------- */

CREATE TABLE [pelicules_a_veure] (
    [id_usuari] UNIQUEIDENTIFIER NOT NULL,
    [id_pelicula] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_pelicules_a_veure] PRIMARY KEY ([id_usuari], [id_pelicula])
)
GO


/* ---------------------------------------------------------------------- */
/* Add table "comentaris_pelicules"                                       */
/* ---------------------------------------------------------------------- */

CREATE TABLE [comentaris_pelicules] (
    [id] UNIQUEIDENTIFIER CONSTRAINT [DEF_comentaris_pelicules_id] DEFAULT NEWID() NOT NULL,
    [id_usuari] UNIQUEIDENTIFIER NOT NULL,
    [id_pelicula] UNIQUEIDENTIFIER NOT NULL,
    [data_hora] DATETIME,
    [comentari] TEXT,
    CONSTRAINT [PK_comentaris_pelicules] PRIMARY KEY ([id], [id_usuari], [id_pelicula])
)
GO


/* ---------------------------------------------------------------------- */
/* Add table "administradors"                                             */
/* ---------------------------------------------------------------------- */

CREATE TABLE [administradors] (
    [id_usuari] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_administradors] PRIMARY KEY ([id_usuari])
)
GO


/* ---------------------------------------------------------------------- */
/* Add table "contactes"                                                  */
/* ---------------------------------------------------------------------- */

CREATE TABLE [contactes] (
    [id_usuari1] UNIQUEIDENTIFIER NOT NULL,
    [id_usuari2] UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_contactes] PRIMARY KEY ([id_usuari1], [id_usuari2])
)
GO


/* ---------------------------------------------------------------------- */
/* Add table "comentaris_perfil"                                          */
/* ---------------------------------------------------------------------- */

CREATE TABLE [comentaris_perfil] (
    [id] UNIQUEIDENTIFIER DEFAULT NEWID() NOT NULL,
    [id_comentador] UNIQUEIDENTIFIER NOT NULL,
    [id_comentat] UNIQUEIDENTIFIER NOT NULL,
    [comentari] TEXT,
    [data_hora] DATETIME,
    CONSTRAINT [PK_comentaris_perfil] PRIMARY KEY ([id], [id_comentador], [id_comentat])
)
GO


/* ---------------------------------------------------------------------- */
/* Foreign key constraints                                                */
/* ---------------------------------------------------------------------- */


ALTER TABLE [registre_edicions] ADD CONSTRAINT [pelicules_registre_edicions] 
    FOREIGN KEY ([id_pelicula]) REFERENCES [pelicules] ([id]) ON DELETE CASCADE
GO

ALTER TABLE [comentaris_pelicules] ADD CONSTRAINT [pelicules_comentaris_pelicules] 
    FOREIGN KEY ([id_pelicula]) REFERENCES [pelicules] ([id]) ON DELETE CASCADE
GO

ALTER TABLE [pelicules_vistes] ADD CONSTRAINT [pelicules_pelicules_vistes] 
    FOREIGN KEY ([id_pelicula]) REFERENCES [pelicules] ([id]) ON DELETE CASCADE
GO

ALTER TABLE [pelicules_a_veure] ADD CONSTRAINT [pelicules_pelicules_a_veure] 
    FOREIGN KEY ([id_pelicula]) REFERENCES [pelicules] ([id]) ON DELETE CASCADE
GO