
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/27/2016 11:25:02
-- Generated from EDMX file: C:\Work\ErrorCollectDuplex\ErrorCollectEntities\ErrorCollectEntities.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ErrorCollectionNew];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_DataDataEntry]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExceptionDataEntries] DROP CONSTRAINT [FK_DataDataEntry];
GO
IF OBJECT_ID(N'[dbo].[FK_MessageExceptionLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExceptionLogs] DROP CONSTRAINT [FK_MessageExceptionLog];
GO
IF OBJECT_ID(N'[dbo].[FK_HelpLinkExceptionLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExceptionLogs] DROP CONSTRAINT [FK_HelpLinkExceptionLog];
GO
IF OBJECT_ID(N'[dbo].[FK_SourceExceptionLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExceptionLogs] DROP CONSTRAINT [FK_SourceExceptionLog];
GO
IF OBJECT_ID(N'[dbo].[FK_DataExceptionLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExceptionLogs] DROP CONSTRAINT [FK_DataExceptionLog];
GO
IF OBJECT_ID(N'[dbo].[FK_ExceptionTypeExceptionLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExceptionLogs] DROP CONSTRAINT [FK_ExceptionTypeExceptionLog];
GO
IF OBJECT_ID(N'[dbo].[FK_OccuranceExceptionLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Occurances] DROP CONSTRAINT [FK_OccuranceExceptionLog];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Occurances]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Occurances];
GO
IF OBJECT_ID(N'[dbo].[ExceptionLogs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExceptionLogs];
GO
IF OBJECT_ID(N'[dbo].[ExceltionDatas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExceltionDatas];
GO
IF OBJECT_ID(N'[dbo].[ExceptionDataEntries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExceptionDataEntries];
GO
IF OBJECT_ID(N'[dbo].[Messages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Messages];
GO
IF OBJECT_ID(N'[dbo].[HelpLinks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[HelpLinks];
GO
IF OBJECT_ID(N'[dbo].[Sources]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sources];
GO
IF OBJECT_ID(N'[dbo].[ExceptionTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExceptionTypes];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Occurances'
CREATE TABLE [dbo].[Occurances] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ExceptionLogId] int  NOT NULL,
    [Time] datetime  NOT NULL,
    [StackTrace] nvarchar(max)  NULL,
    [SessionId] int  NOT NULL
);
GO

-- Creating table 'ExceptionLogs'
CREATE TABLE [dbo].[ExceptionLogs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MessageId] int  NULL,
    [HelpLinkId] int  NULL,
    [SourceId] int  NULL,
    [DataId] int  NULL,
    [ExceptionTypeId] int  NOT NULL
);
GO

-- Creating table 'ExceptionDatas'
CREATE TABLE [dbo].[ExceptionDatas] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'ExceptionDataEntries'
CREATE TABLE [dbo].[ExceptionDataEntries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DataId] int  NOT NULL,
    [Key] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Messages'
CREATE TABLE [dbo].[Messages] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Text] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'HelpLinks'
CREATE TABLE [dbo].[HelpLinks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Link] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Sources'
CREATE TABLE [dbo].[Sources] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Application] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ExceptionTypes'
CREATE TABLE [dbo].[ExceptionTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Sessions'
CREATE TABLE [dbo].[Sessions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PlatformId] int  NOT NULL,
    [DeviceId] int  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'Platforms'
CREATE TABLE [dbo].[Platforms] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Devices'
CREATE TABLE [dbo].[Devices] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DeviceGUID] nvarchar(max)  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Occurances'
ALTER TABLE [dbo].[Occurances]
ADD CONSTRAINT [PK_Occurances]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExceptionLogs'
ALTER TABLE [dbo].[ExceptionLogs]
ADD CONSTRAINT [PK_ExceptionLogs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExceptionDatas'
ALTER TABLE [dbo].[ExceptionDatas]
ADD CONSTRAINT [PK_ExceptionDatas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExceptionDataEntries'
ALTER TABLE [dbo].[ExceptionDataEntries]
ADD CONSTRAINT [PK_ExceptionDataEntries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [PK_Messages]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'HelpLinks'
ALTER TABLE [dbo].[HelpLinks]
ADD CONSTRAINT [PK_HelpLinks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sources'
ALTER TABLE [dbo].[Sources]
ADD CONSTRAINT [PK_Sources]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExceptionTypes'
ALTER TABLE [dbo].[ExceptionTypes]
ADD CONSTRAINT [PK_ExceptionTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sessions'
ALTER TABLE [dbo].[Sessions]
ADD CONSTRAINT [PK_Sessions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Platforms'
ALTER TABLE [dbo].[Platforms]
ADD CONSTRAINT [PK_Platforms]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Devices'
ALTER TABLE [dbo].[Devices]
ADD CONSTRAINT [PK_Devices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [DataId] in table 'ExceptionDataEntries'
ALTER TABLE [dbo].[ExceptionDataEntries]
ADD CONSTRAINT [FK_DataDataEntry]
    FOREIGN KEY ([DataId])
    REFERENCES [dbo].[ExceptionDatas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DataDataEntry'
CREATE INDEX [IX_FK_DataDataEntry]
ON [dbo].[ExceptionDataEntries]
    ([DataId]);
GO

-- Creating foreign key on [MessageId] in table 'ExceptionLogs'
ALTER TABLE [dbo].[ExceptionLogs]
ADD CONSTRAINT [FK_MessageExceptionLog]
    FOREIGN KEY ([MessageId])
    REFERENCES [dbo].[Messages]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MessageExceptionLog'
CREATE INDEX [IX_FK_MessageExceptionLog]
ON [dbo].[ExceptionLogs]
    ([MessageId]);
GO

-- Creating foreign key on [HelpLinkId] in table 'ExceptionLogs'
ALTER TABLE [dbo].[ExceptionLogs]
ADD CONSTRAINT [FK_HelpLinkExceptionLog]
    FOREIGN KEY ([HelpLinkId])
    REFERENCES [dbo].[HelpLinks]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_HelpLinkExceptionLog'
CREATE INDEX [IX_FK_HelpLinkExceptionLog]
ON [dbo].[ExceptionLogs]
    ([HelpLinkId]);
GO

-- Creating foreign key on [SourceId] in table 'ExceptionLogs'
ALTER TABLE [dbo].[ExceptionLogs]
ADD CONSTRAINT [FK_SourceExceptionLog]
    FOREIGN KEY ([SourceId])
    REFERENCES [dbo].[Sources]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SourceExceptionLog'
CREATE INDEX [IX_FK_SourceExceptionLog]
ON [dbo].[ExceptionLogs]
    ([SourceId]);
GO

-- Creating foreign key on [DataId] in table 'ExceptionLogs'
ALTER TABLE [dbo].[ExceptionLogs]
ADD CONSTRAINT [FK_DataExceptionLog]
    FOREIGN KEY ([DataId])
    REFERENCES [dbo].[ExceptionDatas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DataExceptionLog'
CREATE INDEX [IX_FK_DataExceptionLog]
ON [dbo].[ExceptionLogs]
    ([DataId]);
GO

-- Creating foreign key on [ExceptionTypeId] in table 'ExceptionLogs'
ALTER TABLE [dbo].[ExceptionLogs]
ADD CONSTRAINT [FK_ExceptionTypeExceptionLog]
    FOREIGN KEY ([ExceptionTypeId])
    REFERENCES [dbo].[ExceptionTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ExceptionTypeExceptionLog'
CREATE INDEX [IX_FK_ExceptionTypeExceptionLog]
ON [dbo].[ExceptionLogs]
    ([ExceptionTypeId]);
GO

-- Creating foreign key on [ExceptionLogId] in table 'Occurances'
ALTER TABLE [dbo].[Occurances]
ADD CONSTRAINT [FK_OccuranceExceptionLog]
    FOREIGN KEY ([ExceptionLogId])
    REFERENCES [dbo].[ExceptionLogs]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OccuranceExceptionLog'
CREATE INDEX [IX_FK_OccuranceExceptionLog]
ON [dbo].[Occurances]
    ([ExceptionLogId]);
GO

-- Creating foreign key on [SessionId] in table 'Occurances'
ALTER TABLE [dbo].[Occurances]
ADD CONSTRAINT [FK_OccuranceSession]
    FOREIGN KEY ([SessionId])
    REFERENCES [dbo].[Sessions]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OccuranceSession'
CREATE INDEX [IX_FK_OccuranceSession]
ON [dbo].[Occurances]
    ([SessionId]);
GO

-- Creating foreign key on [PlatformId] in table 'Sessions'
ALTER TABLE [dbo].[Sessions]
ADD CONSTRAINT [FK_SessionPlatform]
    FOREIGN KEY ([PlatformId])
    REFERENCES [dbo].[Platforms]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SessionPlatform'
CREATE INDEX [IX_FK_SessionPlatform]
ON [dbo].[Sessions]
    ([PlatformId]);
GO

-- Creating foreign key on [DeviceId] in table 'Sessions'
ALTER TABLE [dbo].[Sessions]
ADD CONSTRAINT [FK_DeviceSession]
    FOREIGN KEY ([DeviceId])
    REFERENCES [dbo].[Devices]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DeviceSession'
CREATE INDEX [IX_FK_DeviceSession]
ON [dbo].[Sessions]
    ([DeviceId]);
GO

-- Creating foreign key on [UserId] in table 'Sessions'
ALTER TABLE [dbo].[Sessions]
ADD CONSTRAINT [FK_SessionUser]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SessionUser'
CREATE INDEX [IX_FK_SessionUser]
ON [dbo].[Sessions]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------