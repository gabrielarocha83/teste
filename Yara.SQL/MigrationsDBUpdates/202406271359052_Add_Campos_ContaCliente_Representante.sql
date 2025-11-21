ALTER TABLE ContaCliente_Representante ADD DataAlteracao DATETIME
GO
ALTER TABLE ContaCliente_Representante ADD UsuarioIDAlteracao UNIQUEIDENTIFIER
GO
ALTER TABLE ContaCliente_Representante ADD Ativo BIT NOT NULL CONSTRAINT [DF_ContaCliente_Representante_Ativo] DEFAULT 1
GO