CREATE DATABASE OrderDb
GO

USE OrderDb
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Orders]') AND type in (N'U'))
DROP TABLE [dbo].[Orders]
GO

CREATE TABLE Orders(
	[Id] [int] IDENTITY(1,1) NOT NULL,
    [UserName] [nvarchar](75) NOT NULL,
	[TotalPrice] [decimal] NOT NULL,

    [FirstName] [nvarchar](75) NULL,
    [LastName] [nvarchar](75) NULL,
    [EmailAddress] [nvarchar](75) NULL,
    [AddressLine] [nvarchar](75) NULL,
    [Country] [nvarchar](75) NULL,
    [State] [nvarchar](75) NULL,
    [ZipCode] [nvarchar](75) NULL,
    [CardName] [nvarchar](75) NULL,
    [CardNumber] [nvarchar](75) NULL,
    [Expiration] [nvarchar](75) NULL,
    [CVV] [nvarchar](75) NULL,
	[PaymentMethod] [int] NULL,

	[CreatedBy] [nvarchar](75) NOT NULL,
	[CreatedDate] [DateTime] NOT NULL,
	[LastModifiedBy] [nvarchar](75) NULL,
	[LastModifiedDate] [DateTime] NULL

 CONSTRAINT [PK_dbo.Orders] PRIMARY KEY CLUSTERED 
        ([Id] ASC )
        WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO