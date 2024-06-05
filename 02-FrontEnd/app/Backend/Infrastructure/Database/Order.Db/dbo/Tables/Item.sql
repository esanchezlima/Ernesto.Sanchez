CREATE TABLE [dbo].[Item]
(
	[ItemId]    UNIQUEIDENTIFIER   DEFAULT (newid()) NOT NULL,
	[OrderId]    UNIQUEIDENTIFIER  NOT NULL,
	[DescriptionProduct]       VARCHAR (50)       NOT NULL,
	[Cant] INT NOT NULL
	 CONSTRAINT [PK_Item] PRIMARY KEY CLUSTERED ([ItemId] ASC),
	  CONSTRAINT [FK_Item_Order] FOREIGN KEY ([OrderId]) REFERENCES [dbo].[Order] ([OrderId]) ON DELETE CASCADE
)
