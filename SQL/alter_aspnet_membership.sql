use headhacks
alter table aspnet_membership add
	[foto] VARCHAR(256),	
    [nom] VARCHAR(256),
    [cognoms] VARCHAR(256),
    [data_naixement] DATETIME,    
    [prestigi] VARCHAR(256) DEFAULT '5';
GO

ALTER TABLE [pelicules_a_veure] ADD CONSTRAINT [usuaris_pelicules_a_veure] 
    FOREIGN KEY ([id_usuari]) REFERENCES [aspnet_Users] ([UserId]) ON DELETE CASCADE
GO

ALTER TABLE [comentaris_pelicules] ADD CONSTRAINT [usuaris_comentaris_pelicules] 
    FOREIGN KEY ([id_usuari]) REFERENCES [aspnet_Users] ([UserId]) ON DELETE CASCADE
GO

ALTER TABLE [administradors] ADD CONSTRAINT [usuaris_administradors] 
    FOREIGN KEY ([id_usuari]) REFERENCES [aspnet_Users] ([UserId]) ON DELETE CASCADE
GO

ALTER TABLE [contactes] ADD CONSTRAINT [usuaris_contactes2] 
    FOREIGN KEY ([id_usuari1]) REFERENCES [aspnet_Users] ([UserId]) ON DELETE CASCADE
GO

ALTER TABLE [contactes] ADD CONSTRAINT [usuaris_contactes1] 
    FOREIGN KEY ([id_usuari2]) REFERENCES [aspnet_Users] ([UserId])
GO

ALTER TABLE [comentaris_perfil] ADD CONSTRAINT [usuaris_comentaris_perfil1] 
    FOREIGN KEY ([id_comentador]) REFERENCES [aspnet_Users] ([UserId]) ON DELETE CASCADE
GO

ALTER TABLE [comentaris_perfil] ADD CONSTRAINT [usuaris_comentaris_perfil2] 
    FOREIGN KEY ([id_comentat]) REFERENCES [aspnet_Users] ([UserId])
GO

ALTER TABLE [registre_edicions] ADD CONSTRAINT [usuaris_registre_edicions] 
    FOREIGN KEY ([id_usuari]) REFERENCES [aspnet_Users] ([UserId]) ON DELETE CASCADE
GO

ALTER TABLE [pelicules_vistes] ADD CONSTRAINT [usuaris_pelicules_vistes] 
    FOREIGN KEY ([id_usuari]) REFERENCES [aspnet_Users] ([UserId]) ON DELETE CASCADE
GO