/* Check whether the database exists and drop it if it does */
IF EXISTS(SELECT 1 FROM master.dbo.sysdatabases WHERE name = 'PharmacyDB_AM')
BEGIN
	DROP DATABASE [PharmacyDB_AM];
	print '' print '*** Dropping database PharmacyDB_AM'
END
GO

print '' print '*** Creating database PharmacyDB_AM'
GO
CREATE DATABASE [PharmacyDB_AM]
GO

print '' print '*** Using database PharmacyDB_AM'
GO
Use [PharmacyDB_AM]
GO



print '' print '*** Creating EmployeeRole Table'
GO
CREATE TABLE [dbo].[EmployeeRole] (
	[EmployeeRoleID]    [nvarchar](50)	NOT NULL,
	[Description]		[nvarchar](250)			,
	
	CONSTRAINT 	[pk_EmployeeRoleID] PRIMARY KEY([EmployeeRoleID] ASC)
)
GO

print '' print '*** Inserting Role Records'
GO
INSERT INTO [dbo].[EmployeeRole]
		([EmployeeRoleID], [Description])
	VALUES
        ('Checkout', 'Checks Items Out'),
		('Manager', 'Manages Item Inventory'),
		('Admin', 'Administers Employee and manages Items')
GO


print '' print '*** Creating Employee Table'
GO
CREATE TABLE [dbo].[Employee] (
	[EmployeeID]		[int] IDENTITY(100000, 1)   NOT NULL,
	[FirstName]			[nvarchar](50)				NOT NULL,
	[LastName]			[nvarchar](50)				NOT NULL,
	[PhoneNumber]		[nvarchar](11)				NOT NULL,
	[Email]				[nvarchar](50)				NOT NULL,
	[Role]              [nvarchar](50)				NOT NULL,
	[PasswordHash]		[nvarchar](100)				NOT NULL, 
	[Active]			[bit]				        NOT NULL DEFAULT 1,
	
	CONSTRAINT [pk_EmployeeID] PRIMARY KEY([EmployeeID] ASC),
	CONSTRAINT [ak_Email] UNIQUE([Email] ASC)
)
GO

print '' print '*** Inserting Employee Test Records'
GO
INSERT INTO [dbo].[Employee]
		([FirstName], [LastName], [PhoneNumber], [Email],[Role],[PasswordHash])
	VALUES
		('Mariem', 'Abidi', '13191236578', 'mariem@pharmarx.com','Manager','80A92B8E019B7E838123B1694246CFDB84D7C3EA76CDEB75BEA68BEEB03C360E'),
        ('Alina', 'Parshyna', '13195479688', 'alina@pharmarx.com','Checkout','E2681685DCA865122100F2D1F5F533833C94C554F5834DE847D298DC4501858E'),
		('Mary', 'Garcia',  '13191236891', 'mary@pharmarx.com','Manager','B0A1645AB4C178B580B121F42519613D2F0809C4BD6950D5A015BA11F962427E'),
		('Ana', 'Paula', '13196985281', 'ana@parmarx.com','Checkout','5629C4A2DFA90F56431A45DEF993A12D6B115C98051E28886DE44BCD7065EF16')
GO

print '' print '*** Adding Foreign Key Role for Employee'
GO
ALTER TABLE [dbo].[Employee] WITH NOCHECK
	ADD CONSTRAINT [fk_EmployeeRole] FOREIGN KEY([Role])
	REFERENCES [dbo].[EmployeeRole]([EmployeeRoleID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating ItemType Table'
GO
CREATE TABLE [dbo].[ItemType] (
	[ItemTypeID]		    [nvarchar](50)             NOT NULL
	
	CONSTRAINT [pk_ItemTypeID] PRIMARY KEY([ItemTypeID] ASC)
	)
GO

print '' print '*** Inserting ItemType Records'
GO
INSERT INTO [dbo].[ItemType]
		([ItemTypeID])
	VALUES
		('Stimulant'),
		('Depressant'),
		('hallucinogenic'),
		('Opioid'),
		('Cannabis'),
		('Inhalant')

GO


print '' print '*** Creating Item Table'
GO
CREATE TABLE [dbo].[Item] (
	[ItemID]		    [int]                       NOT NULL,
	[Name]			    [nvarchar](50)				NOT NULL,
	[Price]	            [money]                     NOT NULL,
	[Type]              [nvarchar](50)	            NOT NULL,
	[Strength]			[nvarchar](50)				NOT NULL,
	[Picture]			[nvarchar](250)				NOT NULL,
	[Description]		[nvarchar](500)		        NOT NULL,
	
	[Exist]			    [bit]				        NOT NULL DEFAULT 1,
	
	CONSTRAINT [pk_ItemID] PRIMARY KEY([ItemID] ASC)
	
)
GO

print '' print '*** Inserting Item Test Records'
GO
INSERT INTO [dbo].[Item]
		([ItemID],[Name],[Price],[Type],[Strength],[Picture],[Description])
	VALUES
		('10001','ABILIFY MAINTENA','40','Stimulant','400MG/VIAL','ABILIFYMAINTENA.jpg','Extended-release aripiprazole injection is used to treat a mental/mood disorder called schizophrenia. This medication can decrease hallucinations (hearing/seeing things that are not there) and improve your concentration. It also helps you to think more clearly, feel less nervous, and take a more active part in everyday life. Some brands of this medication are also used to treat bipolar disorder.'),
		('10002','KALLIGA','10','Stimulant','0.15MG, 0.03MG','KALLIGA.jpg','KalligaTM tablets provide an oral contraceptive regimen of 21 white to off-white round tablets .Inactive ingredients include colloidal silicon dioxide, lactose monohydrate, potato starch, povidone, stearic acid, and vitamin E. Each green tablet contains the following inactive ingredients: anhydrous lactose, croscarmellose sodium, magnesium stearate, microcrystalline cellulose, and povidone.'),
		('10003','EDLUAR','40','Stimulant','30-1153mCi','EDLUAR.jpg','Edluar (zolpidem tartrate) sublingual tablets are indicated for the short-term treatment of insomnia characterized by difficulties with sleep initiation.The clinical trials performed with Zolpidem tartrate in support of efficacy were 4-5 weeks in duration with the final formal assessments of sleep latency performed at the end of treatment.'),
		('10004','ABACAVIR','20','hallucinogenic','EQ 300MG BASE','ABACAVIR.jpg','Abacavir is an antiviral medicine that prevents human immunodeficiency virus (HIV) from multiplying in your body. Abacavir is used to treat HIV, the virus that can cause acquired immunodeficiency syndrome (AIDS). abacavir is for adults and children who are at least 3 months old. Abacavir is not a cure for HIV or AIDS.'), 
		('10005','IBUPROFEN SODIUM','12','hallucinogenic','EQ 200MG BASE','IBUPROFENSODIUM.jpg','Ibuprofen is a nonsteroidal anti-inflammatory drug (NSAID). It works by reducing hormones that cause inflammation and pain in the body. Ibuprofen is used to reduce fever and treat pain or inflammation caused by many conditions such as headache, toothache, back pain, arthritis, menstrual cramps, or minor injury.'),
		('10006','DANOCRINE','18','hallucinogenic','200MG','DANOCRINE.jpg','Danocrine is a man-made form of a steroid. This medicine affects the ovaries and pituitary gland and prevents the release of certain hormones in the body. Danocrine is used to treat endometriosis and fibrocystic breast disease. This medicine is also used to prevent attacks of angioedema in people with an inherited form of this disorder (hereditary angioedema).'),
		('10007','MANDOL','36','Opioid','EQ 500MG BASE/VIAL','MANDOL.jpg','Mandol has been assigned to pregnancy category B by the FDA. Animal studies failed to reveal evidence of fetal harm. There are no controlled data in human pregnancies. Cefamandole should only be given during pregnancy when need has been clearly established.'),
		('10008','OLUX','100','Opioid','200MG','OLUX.jpg','Olux is a highly potent steroid. It reduces the actions of chemicals in the body that cause inflammation. Olux (for the skin) is used to treat inflammation and itching of the skin caused by conditions such as eczema and psoriasis.'),
		('10009','SANCUSO','87','Cannabis','3.1MG/24HR','SANCUSO.jpg','Sancuso (granisetron) blocks the actions of chemicals in the body that may cause nausea and vomiting.Sancuso skin patches are used to prevent nausea and vomiting caused by cancer chemotherapy. Sancuso may also be used for purposes not listed in this medication g uide.'),
		('10010','QUINIDEX','17','Cannabis','300MG','QUINIDEX.jpg','Pill with imprint QUINIDEX AHR is White, Round and has been identified as Quinidex extentabs 300 MG. It is supplied by Robins Pharm. Quinidex Extentabs is used in the treatment of malaria; arrhythmia and belongs to the drug class group I antiarrhythmics. Risk cannot be ruled out during pregnancy. Quinidex Extentabs 300 MG is not a controlled substance under the Controlled Substances Act (CSA).'),
		('10011','PARSIDOL','48','Inhalant','100MG','PARSIDOL.png','Profenamine is a phenothiazine derivative used as an antiparkinsonian agent[1] that has anticholinergic, antihistamine, and antiadrenergic actions. It is also used in the alleviation of the extrapyramidal syndrome induced by drugs such as other phenothiazine compounds, but, like other compounds with antimuscarinic properties, is of no value against tardive dyskinesia.'),
		('10012','HALDOL','28','Inhalant','EQ 50MG BASE/ML','HALDOL.jpg','Haldol is an antipsychotic medicine. It works by changing the actions of chemicals in your brain. Haldol is used to treat schizophrenia. It is also used to ontrol motor and speech tics in people with Tourette"s syndrome. Haldol is not approved for use in psychotic conditions related to dementia. This medicine may increase the risk of death in older adults with dementia-related conditions.'),
		('10013','ULTRACET','50','Depressant','325MG, 37.5MG','ULTRACET.jpg','Ultracet contains a combination of tramadol and acetaminophen. Tramadol is an pain medicine similar to an opioid (sometimes called, a narcotic). Acetaminophen is a less potent pain reliever that increases the effects of tramadol. Ultracet is used to treat moderate to severe pain.')
GO


print '' print '*** Adding Foreign Key Type for Item'
GO
ALTER TABLE [dbo].[Item] WITH NOCHECK
	ADD CONSTRAINT [fk_ItemType] FOREIGN KEY([Type]) REFERENCES [dbo].[ItemType]([ItemTypeID])
	ON UPDATE CASCADE
GO

print '' print '*** Creating sp_authenticate_user'
GO
CREATE PROCEDURE [DBO].[sp_authenticate_user]
	(
		@Email				[nvarchar](50),
		@PasswordHash		[nvarchar](100)
	)
AS
	BEGIN
		SELECT COUNT([EmployeeID])
		FROM [Employee]
		WHERE [Email] = @Email
		  AND [PasswordHash] = @PasswordHash
		  AND [Active] = 1
	END
GO


print '' print '*** Creating sp_retrieve_user_by_email'
GO
CREATE PROCEDURE sp_retrieve_user_by_email
	(
		@Email				[nvarchar](50)
	)
AS
	BEGIN
		SELECT 	[EmployeeID], [FirstName], [LastName],[Role]
		FROM  	[Employee]
		WHERE	[Email] = @Email
	END
GO

print '' print '*** Creating sp_retrieve_user_by_email_and_password'
GO
CREATE PROCEDURE sp_retrieve_user_by_email_and_password
	(
		@Email				[nvarchar](50),
		@Password		    [nvarchar](100)
	) 
	AS
	BEGIN

		SELECT 	[EmployeeID], [FirstName], [LastName],[Role]
		FROM  	[Employee]
		WHERE	[Email] = @Email
		AND     [PasswordHash]= @Password
END
GO


print '' print '*** Creating sp_verify_password'

GO
CREATE PROCEDURE sp_verify_password
	(
		@Email				[nvarchar](50),
		@Password		    [nvarchar](100)
	) 
AS
BEGIN

SELECT [EmployeeID] 
	       FROM [Employee] 
           WHERE	[Email] = @Email
		   AND     [PasswordHash]= @Password
END
GO



print '' print '*** Creating sp_update_password_hash'
GO
CREATE PROCEDURE [DBO].[sp_update_password_hash]
	(
		@Email				[nvarchar](50),
		@NewPasswordHash	[nvarchar](100),
		@OldPasswordHash	[nvarchar](100)
	)
AS
	BEGIN
		IF @NewPasswordHash != @OldPasswordHash
		BEGIN
			UPDATE [Employee]
				SET [PasswordHash] = @NewPasswordHash
				WHERE [Email] = @Email
				  AND [PasswordHash] = @OldPasswordHash
			RETURN @@ROWCOUNT
		END
	END
GO


print '' print '*** Creating sp_update_employee_info'
GO
CREATE PROCEDURE [DBO].[sp_update_employee_info]
	(
		@EmployeeID				[int],
		@OldEmail               [nvarchar](50),
        @NewEmail               [nvarchar](50),
        @OldFirstName           [nvarchar](50),
        @NewFirstName           [nvarchar](50),
        @OldLastName            [nvarchar](50),
        @NewLastName            [nvarchar](50),
        @NewRole                [nvarchar](50),
        @OldRole                [nvarchar](50),
        @NewPasswordHash	    [nvarchar](100),
		@OldPasswordHash	    [nvarchar](100),
        @OldPhoneNumber         [nvarchar](11),
        @NewPhoneNumber         [nvarchar](11)
	)
AS
	BEGIN
	        UPDATE [Employee]

				SET [Email]	       =@NewEmail     ,
                    [FirstName]    =@NewFirstName ,
	                [LastName]     =@NewLastName  ,
	                [Role]         =@NewRole      ,     
                    [PhoneNumber]  =@NewPhoneNumber,
                    [PasswordHash] =@NewPasswordHash 
	                	
                   
				WHERE    [EmployeeID]   =@EmployeeID
				AND      [Email]	    =@OldEmail    
                AND      [FirstName]    =@OldFirstName 
	            AND      [LastName]     =@OldLastName  
	            AND      [Role]         =@OldRole           
                AND      [PhoneNumber]  =@OldPhoneNumber
				AND      [PasswordHash] =@OldPasswordHash

			RETURN @@ROWCOUNT
		
	END
GO
print '' print '*** Creating sp_retrieve_items_by_type'
GO
CREATE PROCEDURE sp_retrieve_items_by_type
	(
		@TypeID				[nvarchar](50)
	)
AS
	BEGIN
	
       SELECT [ItemID], [Name], [Price], [Type],[Strength],[Picture], 
			  [Description], [Exist]
		FROM [Item]
		WHERE 	[Type] = @TypeID
	END
GO
print '' print '*** Creating sp_retrieve_items_by_name'
GO
CREATE PROCEDURE sp_retrieve_items_by_name
	(
		@Name				[nvarchar](50)
	)
AS
	BEGIN
	
       SELECT [ItemID], [Name], [Price], [Type],[Strength],[Picture], 
			  [Description], [Exist]
		FROM [Item]
		WHERE 	[Name] = @Name
	END
GO

GO
print '' print '*** Creating sp_retrieve_items_by_id'
GO
CREATE PROCEDURE sp_retrieve_items_by_id
	(
		@ItemID				[int]
	)
AS
	BEGIN
	
       SELECT [ItemID], [Name], [Price], [Type],[Strength],[Picture], 
			  [Description], [Exist]
		FROM [Item]
		WHERE 	[ItemID] = @ItemID
	END
GO

print '' print '*** Creating sp_retrieve_all_items'
GO
CREATE PROCEDURE sp_retrieve_all_items
AS
	BEGIN
	
       SELECT [ItemID], [Name], [Price], [Type],[Strength],[Picture], 
			  [Description], [Exist]
		FROM [Item]
	END
GO




print '' print '*** Creating sp_retreive_all_itemtypeid	'
GO
CREATE PROCEDURE sp_retreive_all_itemtypeid
AS
BEGIN
 
 SELECT [ItemTypeID]
 from  [ItemType]
 order by  [ItemTypeID] 
 END
 GO

 print '' print '*** Creating sp_insert_item'
GO
CREATE PROCEDURE sp_insert_item
	(
    @ItemID             [int]           ,
    @Name			    [nvarchar](50)	,
	@Price	            [money]         ,
	@Type               [nvarchar](50)	,
	@Strength			[nvarchar](50)  ,
	@Picture			[nvarchar](250) ,
	@Description		[nvarchar](500)	,
	@Exist			    [bit]
					
	)
AS
	BEGIN
		INSERT into   [Item]
		              ([ItemID],[Name],[Price],[Type],[Strength], [Picture], [Description],[Exist])
		VALUES        (@ItemID,@Name,@Price,@Type,@Strength,@Picture, @Description,@Exist)
             
	RETURN @@ROWCOUNT
END	
GO

print '' print '*** Creating sp_update_item_by_id'
GO
CREATE PROCEDURE sp_update_item_by_id
	(
    @ItemID             [int]           ,
	@Name			    [nvarchar](50)	,
	@Price	            [money]         ,
	@Type               [nvarchar](50)	,
	@Strength			[nvarchar](50)  ,
	@Description		[nvarchar](500)	,	
	@Picture            [nvarchar](250) ,
    @Exist    		    [bit]	        ,
	@OldName			    [nvarchar](50)	,
	@OldPrice	            [money]         ,
	@OldType                [nvarchar](50)	,
	@OldStrength			[nvarchar](50)  ,
	@OldDescription		    [nvarchar](500)	,
	@OldPicture                [nvarchar](250) ,
	@OldExist		        [bit]				
	)
AS
	BEGIN
	  Update [Item]
		 SET [Name] =@Name,
             [Price]=@Price,
             [Type]=@Type,
             [Strength]=@Strength,
             [Description]=@Description,
             [Exist]=@Exist,
             [Picture]=@Picture 
		  
	WHERE 	 [Name] =@OldName
     AND     [Price]=@OldPrice
     AND     [Type]=@OldType
     AND     [Strength]=@OldStrength
     AND     [Description]=@OldDescription
     AND     [Exist]=@OldExist 
     And     [Picture]=@OldPicture

END			
GO
print '' print '*** Creating sp_delete_items_by_id	'
GO
CREATE PROCEDURE sp_delete_items_by_id
	(	@ItemID			[int]	)
	
AS
BEGIN
	    Delete from  [Item]
		 WHERE 	 [ItemID] =@ItemID	
		

END
GO

print '' print '*** Creating sp_activate_items_by_id'
GO
CREATE PROCEDURE sp_activate_items_by_id
	(	@ItemID			[int]	)
	
AS
BEGIN
	     Update  [Item]
		 SET     [Exist] = 1
		 WHERE 	 [ItemID] =@ItemID
		

END
GO

print '' print '*** Creating sp_deactivate_items_by_id'
GO
CREATE PROCEDURE sp_deactivate_items_by_id
	(	@ItemID			[int]	)
	
AS
BEGIN
	     Update  [Item]
		 SET     [Exist] = 0
		 WHERE 	 [ItemID] =@ItemID
		

END
GO