IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210819200840_local_data')
BEGIN
    CREATE TABLE [pension] (
        [Id] int NOT NULL IDENTITY,
        [AadharNumber] float NOT NULL,
        [PensionAmount] float NOT NULL,
        [BankCharges] float NOT NULL,
        CONSTRAINT [PK_pension] PRIMARY KEY ([Id], [AadharNumber])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210819200840_local_data')
BEGIN
    CREATE TABLE [userDetails] (
        [Id] int NOT NULL IDENTITY,
        [AadharNumber] float NOT NULL,
        [Name] nvarchar(max) NOT NULL,
        [DateOfBirth] datetime2 NOT NULL,
        [PanNo] nvarchar(max) NOT NULL,
        [BankAccountType] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_userDetails] PRIMARY KEY ([Id], [AadharNumber])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210819200840_local_data')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210819200840_local_data', N'5.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210819201309_local_data_2')
BEGIN
    ALTER TABLE [pension] ADD [DateOFWithdraw] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210819201309_local_data_2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210819201309_local_data_2', N'5.0.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210819204516_local_data_3')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210819204516_local_data_3', N'5.0.1');
END;
GO

COMMIT;
GO

