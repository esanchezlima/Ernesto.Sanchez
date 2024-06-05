CREATE TABLE [dbo].[Order]
 (
    [OrderId]    UNIQUEIDENTIFIER   DEFAULT (newid()) NOT NULL,
    [Client]   VARCHAR (50)       NOT NULL,
    [DateofOrder] DATETIMEOFFSET (7) NULL,
    [AdressClient]       VARCHAR (50)       NOT NULL,
    [District] varchar(30) not null,
    CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([OrderId] ASC)
);