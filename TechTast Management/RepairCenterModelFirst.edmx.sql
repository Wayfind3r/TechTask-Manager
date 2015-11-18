
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/17/2015 22:04:22
-- Generated from EDMX file: D:\RepTrue\TechTask-Manager\TechTast Management\RepairCenterModelFirst.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ModelFirstRepairCenter];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ActivityTechnician]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Activities] DROP CONSTRAINT [FK_ActivityTechnician];
GO
IF OBJECT_ID(N'[dbo].[FK_UnitTypeActivity]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Activities] DROP CONSTRAINT [FK_UnitTypeActivity];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Technicians]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Technicians];
GO
IF OBJECT_ID(N'[dbo].[UnitTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UnitTypes];
GO
IF OBJECT_ID(N'[dbo].[Activities]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Activities];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Technicians'
CREATE TABLE [dbo].[Technicians] (
    [TechID] int IDENTITY(1,1) NOT NULL,
    [Alias] nvarchar(6)  NOT NULL,
    [FullName] nvarchar(max)  NOT NULL,
    [Status] int  NOT NULL,
    [TelephoneNumber] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UnitTypes'
CREATE TABLE [dbo].[UnitTypes] (
    [TypeID] int IDENTITY(1,1) NOT NULL,
    [Model] nvarchar(50)  NOT NULL,
    [Description] nvarchar(255)  NOT NULL
);
GO

-- Creating table 'Activities'
CREATE TABLE [dbo].[Activities] (
    [ActivityID] int IDENTITY(1,1) NOT NULL,
    [Priority] int  NOT NULL,
    [SerialNumber] nvarchar(max)  NOT NULL,
    [DateReceived] datetime  NOT NULL,
    [Age] int  NOT NULL,
    [CurrentStatus] int  NOT NULL,
    [TechnicianTechID] int  NOT NULL,
    [UnitTypeTypeID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [TechID] in table 'Technicians'
ALTER TABLE [dbo].[Technicians]
ADD CONSTRAINT [PK_Technicians]
    PRIMARY KEY CLUSTERED ([TechID] ASC);
GO

-- Creating primary key on [TypeID] in table 'UnitTypes'
ALTER TABLE [dbo].[UnitTypes]
ADD CONSTRAINT [PK_UnitTypes]
    PRIMARY KEY CLUSTERED ([TypeID] ASC);
GO

-- Creating primary key on [ActivityID] in table 'Activities'
ALTER TABLE [dbo].[Activities]
ADD CONSTRAINT [PK_Activities]
    PRIMARY KEY CLUSTERED ([ActivityID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [TechnicianTechID] in table 'Activities'
ALTER TABLE [dbo].[Activities]
ADD CONSTRAINT [FK_ActivityTechnician]
    FOREIGN KEY ([TechnicianTechID])
    REFERENCES [dbo].[Technicians]
        ([TechID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ActivityTechnician'
CREATE INDEX [IX_FK_ActivityTechnician]
ON [dbo].[Activities]
    ([TechnicianTechID]);
GO

-- Creating foreign key on [UnitTypeTypeID] in table 'Activities'
ALTER TABLE [dbo].[Activities]
ADD CONSTRAINT [FK_UnitTypeActivity]
    FOREIGN KEY ([UnitTypeTypeID])
    REFERENCES [dbo].[UnitTypes]
        ([TypeID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UnitTypeActivity'
CREATE INDEX [IX_FK_UnitTypeActivity]
ON [dbo].[Activities]
    ([UnitTypeTypeID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------