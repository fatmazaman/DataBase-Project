USE [HumanMicroRNA]
GO
/****** Object:  User [human_micro_rna]    Script Date: 11/20/2014 12:51:24 PM ******/
CREATE USER [human_micro_rna] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [RJF\NBOUZEIDAN]    Script Date: 11/20/2014 12:51:24 PM ******/
CREATE USER [RJF\NBOUZEIDAN] FOR LOGIN [RJF\NBOUZEIDAN] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [human_micro_rna]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [human_micro_rna]
GO
ALTER ROLE [db_owner] ADD MEMBER [RJF\NBOUZEIDAN]
GO
/****** Object:  Schema [dbvar]    Script Date: 11/20/2014 12:51:25 PM ******/
CREATE SCHEMA [dbvar]
GO
/****** Object:  Schema [rna]    Script Date: 11/20/2014 12:51:25 PM ******/
CREATE SCHEMA [rna]
GO
/****** Object:  Schema [snp]    Script Date: 11/20/2014 12:51:25 PM ******/
CREATE SCHEMA [snp]
GO
/****** Object:  Table [dbo].[exception_log]    Script Date: 11/20/2014 12:51:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[exception_log](
	[exception_log_id] [int] IDENTITY(1,1) NOT NULL,
	[exception_log_text] [xml] NOT NULL,
	[exception_from_method_text] [varchar](200) NOT NULL,
	[exception_dtm] [datetime] NOT NULL CONSTRAINT [DF_exception_log_exception_dtm]  DEFAULT (getdate()),
	[exception_by_name] [varchar](200) NOT NULL,
 CONSTRAINT [PK_exception_log] PRIMARY KEY CLUSTERED 
(
	[exception_log_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[hmrna_var_assn]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[hmrna_var_assn](
	[human_micro_rna_id] [int] NOT NULL,
	[var_id] [varchar](200) NOT NULL,
	[chromosome_num] [varchar](20) NULL,
	[chromosome_range_from_num] [bigint] NULL,
	[chromosome_range_to_num] [bigint] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[human_micro_rna]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[human_micro_rna](
	[human_micro_rna_id] [int] IDENTITY(1,1) NOT NULL,
	[auto_mirna] [int] NOT NULL,
	[mirna_acc_text] [varchar](9) NOT NULL,
	[mirna_id] [varchar](40) NOT NULL,
	[mirna_desc] [varchar](max) NULL,
	[xsome_num] [varchar](50) NULL,
	[range_from_num] [bigint] NULL,
	[range_to_num] [bigint] NULL,
	[mature_xsome_localization_text] [varchar](300) NULL,
	[strand_text] [char](2) NULL,
	[pre_mature_sequence_text] [varchar](max) NOT NULL,
	[pre_mature_comment] [varchar](max) NULL,
	[mature_5p_id] [varchar](40) NOT NULL,
	[mature_5p_acc] [varchar](300) NULL,
	[mature_5p_sequence_text] [varchar](200) NOT NULL,
	[mature_5p_from_num] [int] NOT NULL,
	[mature_5p_to_num] [int] NOT NULL,
	[mature_3p_id] [varchar](40) NULL,
	[mature_3p_acc] [varchar](300) NULL,
	[mature_3p_sequence_text] [varchar](200) NULL,
	[mature_3p_from_num] [int] NULL,
	[mature_3p_to_num] [int] NULL,
 CONSTRAINT [PK_human_micro_rna] PRIMARY KEY CLUSTERED 
(
	[human_micro_rna_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[password_reset_request]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[password_reset_request](
	[password_reset_process_id] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [varchar](100) NOT NULL,
	[email_address_text] [varchar](500) NOT NULL,
	[password_reset_request_dtm] [datetime] NOT NULL CONSTRAINT [DF_Table_1_password_request_dtm]  DEFAULT (getdate()),
	[password_reset_request_by_user_id] [varchar](200) NOT NULL,
	[password_reset_request_code] [varchar](max) NOT NULL,
	[password_reset_request_complete_flag] [bit] NOT NULL,
	[password_reset_request_complete_dtm] [datetime] NOT NULL CONSTRAINT [DF_password_reset_request_password_reset_request_complete_dtm]  DEFAULT (getdate()),
 CONSTRAINT [PK_password_reset_request] PRIMARY KEY CLUSTERED 
(
	[password_reset_process_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[user_lku]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[user_lku](
	[user_lku_id] [int] IDENTITY(1,1) NOT NULL,
	[user_first_name] [varchar](200) NOT NULL,
	[user_last_name] [varchar](200) NOT NULL,
	[user_email_address_text] [varchar](200) NOT NULL,
	[user_name] [varchar](200) NOT NULL,
	[user_password_hash] [varchar](1000) NOT NULL,
	[user_password_salt] [varchar](500) NOT NULL,
	[user_act_flag] [bit] NOT NULL CONSTRAINT [DF_user_lku_user_act_flag]  DEFAULT ((1)),
	[user_created_dtm] [datetime] NOT NULL CONSTRAINT [DF_user_lku_user_created_dtm]  DEFAULT (getdate()),
	[user_created_by_name] [varchar](200) NOT NULL,
 CONSTRAINT [PK_user_lku] PRIMARY KEY CLUSTERED 
(
	[user_lku_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[user_role_assn]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user_role_assn](
	[user_lku_id] [int] NOT NULL,
	[user_role_lku_id] [int] NOT NULL,
 CONSTRAINT [PK_user_role_assn] PRIMARY KEY CLUSTERED 
(
	[user_lku_id] ASC,
	[user_role_lku_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[user_role_lku]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[user_role_lku](
	[user_role_lku_id] [int] IDENTITY(1,1) NOT NULL,
	[user_role_display_text] [varchar](200) NOT NULL,
	[user_role_desc_text] [varchar](600) NOT NULL,
	[user_role_act_flag] [bit] NOT NULL,
	[user_role_created_by_name] [varchar](200) NOT NULL,
	[user_role_created_dtm] [datetime] NOT NULL,
 CONSTRAINT [PK_user_role_lku] PRIMARY KEY CLUSTERED 
(
	[user_role_lku_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbvar].[db_var]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbvar].[db_var](
	[var_id] [varchar](100) NOT NULL,
	[var_symbol_name] [varchar](20) NOT NULL,
	[var_type_id] [varchar](200) NOT NULL,
	[xsome_num] [varchar](100) NOT NULL,
	[range_from_num] [bigint] NOT NULL,
	[range_to_num] [bigint] NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [rna].[mir_family_info]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [rna].[mir_family_info](
	[mir_family_id] [varchar](200) NOT NULL,
	[seed_plus_m8] [varchar](200) NOT NULL,
	[species_id] [int] NOT NULL,
	[mirna_id] [varchar](200) NOT NULL,
	[mature_sequence_text] [varchar](200) NULL,
	[family_conservation_id] [int] NULL,
	[mature_accession_id] [varchar](200) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [rna].[mirna]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [rna].[mirna](
	[auto_mirna] [int] NOT NULL,
	[mirna_acc] [varchar](100) NOT NULL CONSTRAINT [DF__mirna__mirna_acc__2B3F6F97]  DEFAULT (''),
	[mirna_id] [varchar](300) NOT NULL CONSTRAINT [DF__mirna__mirna_id__2C3393D0]  DEFAULT (''),
	[mirna_l_id] [varchar](300) NULL,
	[description] [varchar](max) NULL CONSTRAINT [DF__mirna__descripti__2D27B809]  DEFAULT (NULL),
	[sequence] [varchar](max) NULL,
	[comment] [varchar](max) NULL,
	[auto_species] [int] NOT NULL CONSTRAINT [DF__mirna__auto_spec__2E1BDC42]  DEFAULT ('0'),
 CONSTRAINT [PK__mirna__F72FA7EE29572725] PRIMARY KEY CLUSTERED 
(
	[auto_mirna] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [rna].[mirna_chromosome_build]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [rna].[mirna_chromosome_build](
	[auto_mirna] [int] NOT NULL CONSTRAINT [DF__mirna_chr__auto___35BCFE0A]  DEFAULT ('0'),
	[xsome] [varchar](20) NOT NULL CONSTRAINT [DF__mirna_chr__xsome__36B12243]  DEFAULT (NULL),
	[contig_start] [bigint] NULL CONSTRAINT [DF__mirna_chr__conti__37A5467C]  DEFAULT (NULL),
	[contig_end] [bigint] NULL CONSTRAINT [DF__mirna_chr__conti__38996AB5]  DEFAULT (NULL),
	[strand] [char](2) NULL CONSTRAINT [DF__mirna_chr__stran__398D8EEE]  DEFAULT (NULL)
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [rna].[mirna_excel]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [rna].[mirna_excel](
	[mirna_acc] [varchar](20) NOT NULL,
	[mirna_id] [varchar](300) NOT NULL,
	[status] [varchar](100) NOT NULL,
	[sequence] [varchar](max) NOT NULL,
	[mature_1_acc] [varchar](100) NULL,
	[mature_1_id] [varchar](20) NULL,
	[mature_1_sequence] [varchar](max) NULL,
	[mature_2_acc] [varchar](100) NULL,
	[mature_2_id] [varchar](20) NULL,
	[mature_2_sequence] [varchar](max) NULL,
 CONSTRAINT [PK_mirna_excel] PRIMARY KEY CLUSTERED 
(
	[mirna_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [rna].[mirna_mature]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [rna].[mirna_mature](
	[auto_mature] [int] NOT NULL,
	[mature_name] [varchar](40) NOT NULL,
	[mature_acc] [varchar](100) NOT NULL,
	[mature_from] [varchar](4) NULL,
	[mature_to] [varchar](4) NULL,
	[evidence] [varchar](max) NULL,
	[experiment] [varchar](max) NULL,
	[similarity] [varchar](max) NULL,
 CONSTRAINT [PK__mirna_ma__2F2EACEC49C3F6B7] PRIMARY KEY CLUSTERED 
(
	[auto_mature] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [rna].[predicted_target]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [rna].[predicted_target](
	[mir_family_id] [varchar](300) NOT NULL,
	[gene_id] [varchar](300) NOT NULL,
	[gene_symbol_id] [varchar](300) NOT NULL,
	[transcript_id] [varchar](300) NOT NULL,
	[species_id] [varchar](300) NOT NULL,
	[utr_start_num] [bigint] NOT NULL,
	[utr_end_num] [bigint] NOT NULL,
	[msa_start_num] [bigint] NOT NULL,
	[msa_end_num] [bigint] NOT NULL,
	[seed_match_text] [varchar](300) NULL,
	[pct_text] [varchar](300) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [snp].[SNPContigLoc]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [snp].[SNPContigLoc](
	[snp_type] [varchar](2) NULL,
	[snp_id] [varchar](20) NOT NULL,
	[phys_pos_from] [varchar](50) NULL,
	[phys_pos] [varchar](50) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[user_role_lku] ADD  CONSTRAINT [DF_user_role_lku_user_role_created_by_name]  DEFAULT ((1)) FOR [user_role_created_by_name]
GO
ALTER TABLE [dbo].[user_role_lku] ADD  CONSTRAINT [DF_Table_1_[user_role_created_dtm]  DEFAULT (getdate()) FOR [user_role_created_dtm]
GO
ALTER TABLE [rna].[mirna_mature] ADD  CONSTRAINT [DF__mirna_mat__matur__4BAC3F29]  DEFAULT ('') FOR [mature_name]
GO
ALTER TABLE [rna].[mirna_mature] ADD  CONSTRAINT [DF__mirna_mat__matur__4CA06362]  DEFAULT ('') FOR [mature_acc]
GO
ALTER TABLE [rna].[mirna_mature] ADD  CONSTRAINT [DF__mirna_mat__matur__4D94879B]  DEFAULT (NULL) FOR [mature_from]
GO
ALTER TABLE [rna].[mirna_mature] ADD  CONSTRAINT [DF__mirna_mat__matur__4E88ABD4]  DEFAULT (NULL) FOR [mature_to]
GO
ALTER TABLE [rna].[mirna]  WITH CHECK ADD  CONSTRAINT [FK_mirna_mirna_excel] FOREIGN KEY([mirna_id])
REFERENCES [rna].[mirna_excel] ([mirna_id])
GO
ALTER TABLE [rna].[mirna] CHECK CONSTRAINT [FK_mirna_mirna_excel]
GO
ALTER TABLE [rna].[mirna_chromosome_build]  WITH CHECK ADD  CONSTRAINT [FK_mirna_chromosome_build_mirna] FOREIGN KEY([auto_mirna])
REFERENCES [rna].[mirna] ([auto_mirna])
GO
ALTER TABLE [rna].[mirna_chromosome_build] CHECK CONSTRAINT [FK_mirna_chromosome_build_mirna]
GO
/****** Object:  StoredProcedure [dbo].[drop_recreate_all_tables_sp]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =================================================
-- Author:		<Bou Zeidan, Nadim>
-- Create date: <2012-05-25>
-- Description:	<Drop All Tables from the database>
-- =================================================
CREATE PROCEDURE [dbo].[drop_recreate_all_tables_sp] 
AS
BEGIN
	DECLARE tables_cursor CURSOR
	FOR SELECT name FROM sysobjects WHERE type = 'U'
		OPEN tables_cursor
			DECLARE @tablename sysname
			FETCH NEXT FROM tables_cursor INTO @tablename
			WHILE (@@FETCH_STATUS <> -1)
				BEGIN
				EXEC ('DROP TABLE ' + 'HumanMicroRNA.rna.' + @tablename)
				FETCH NEXT FROM tables_cursor INTO @tablename
				END
	DEALLOCATE tables_cursor
END

GO
/****** Object:  StoredProcedure [dbo].[hmrna_cash_homo_sapiens_mirna_all_sp]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Author:		<Bou Zeidan, Nadim>
-- Create date: <2012-05-26>
-- Description:	<Get Homo Sapiens MicroRNA Report>
-- ================================================
CREATE PROCEDURE [dbo].[hmrna_cash_homo_sapiens_mirna_all_sp]
AS
BEGIN
   /*First task would be to delete all of the records from the human_micro_rna before
   we are able to insert the newly queried records. The reson this table exists is to
   cash the data in the table to maximize efficiency when the user is searching for MiRNA-SNP/VAR
   */
   DELETE  [HumanMicroRNA].[dbo].[human_micro_rna]
   DBCC CHECKIDENT ("[HumanMicroRNA].[dbo].[human_micro_rna]", RESEED, 0);
   INSERT 
	 INTO  [HumanMicroRNA].[dbo].[human_micro_rna]
		  ([auto_mirna]
		  ,[mirna_acc_text]
		  ,[mirna_id]
		  ,[mirna_desc]
		  ,[xsome_num]
		  ,[range_from_num]
		  ,[range_to_num]
		  ,[mature_xsome_localization_text]
		  ,[strand_text]
		  ,[pre_mature_sequence_text]
		  ,[pre_mature_comment]
		  ,[mature_5p_id]
		  ,[mature_5p_acc]
		  ,[mature_5p_sequence_text]
		  ,[mature_5p_from_num]
		  ,[mature_5p_to_num]
		  ,[mature_3p_id]
		  ,[mature_3p_acc]
		  ,[mature_3p_sequence_text]
		  ,[mature_3p_from_num]
		  ,[mature_3p_to_num])
				 
   SELECT   m.auto_mirna, 
            m.mirna_acc, 
            m.mirna_id,
            m.description,
            --'chr' + 
            mcb.xsome,
            mcb.contig_start,
            mcb.contig_end,
            --'chr' + 
            mcb.xsome + ':' + 
            CAST(mcb.contig_start AS varchar(MAX)) + '-' + 
            CAST(mcb.contig_end AS varchar(MAX)) AS mature_xsome_localization, 
            mcb.strand, 
            m.sequence AS pre_mature_sequence, 
            m.comment,
            me.mature_1_id AS mature_5p_id, 
            me.mature_1_acc as mature_5p_acc,
            me.mature_1_sequence AS mature_5p_sequence, 
            PATINDEX('%' + me.mature_1_sequence + '%', m.sequence) AS mature_5p_from,
            CAST(LEN(me.mature_1_sequence) AS int) + 
				CAST(PATINDEX('%' + me.mature_1_sequence + '%', m.sequence) AS int) - 1 AS mature_5p_to,
            me.mature_2_id AS mature_3p_id, 
            me.mature_2_acc as mature_3p_acc,
            me.mature_2_sequence AS mature_3p_sequence,
            PATINDEX('%' + me.mature_2_sequence + '%', m.sequence) AS mature_3p_from,
            CAST(LEN(me.mature_2_sequence) AS int) + 
				CAST(PATINDEX('%' + me.mature_2_sequence + '%', m.sequence) AS int) - 1 AS mature_3p_to
            
	 FROM   [HumanMicroRNA].[rna].[mirna] m
	 LEFT 
	 JOIN   [HumanMicroRNA].[rna].[mirna_chromosome_build] AS mcb ON m.auto_mirna = mcb.auto_mirna
     LEFT 
     JOIN   [HumanMicroRNA].[rna].[mirna_excel] AS me ON m.mirna_acc = me.mirna_acc
     LEFT 
     JOIN   [HumanMicroRNA].[rna].[mirna_mature] AS mm ON m.mirna_id = mm.mature_name
     
    WHERE   (m.mirna_id LIKE 'hsa%')		
    
    ORDER
       BY	mirna_acc ASC;											
END

GO
/****** Object:  StoredProcedure [dbo].[hmrna_check_if_email_address_exists_sp]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================================
-- Author:		<Nadim Bou Zeidan>
-- Create date: <2012-07-03>
-- Description:	<Check if email address exists in the database.>
-- =============================================================
CREATE PROCEDURE [dbo].[hmrna_check_if_email_address_exists_sp]
	@email_address varchar(100) = NULL
AS
BEGIN
	SET NOCOUNT OFF;
	IF EXISTS(SELECT *
			    FROM [HumanMicroRNA].[dbo].[user_lku]
			   WHERE [user_email_address_text] = @email_address)

		SELECT 1
	ELSE
		SELECT 0
END

GO
/****** Object:  StoredProcedure [dbo].[hmrna_check_if_user_exists_sp]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================================
-- Author:		<Nadim Bou Zeidan>
-- Create date: <2012-07-03>
-- Description:	<Check if usernae exists in the database.>
-- =============================================================
CREATE PROCEDURE [dbo].[hmrna_check_if_user_exists_sp]
	@username varchar(100) = NULL
AS
BEGIN
	SET NOCOUNT OFF;
	IF EXISTS(SELECT *
			    FROM [HumanMicroRNA].[dbo].[user_lku]
			   WHERE [user_name] = @username)

		SELECT 1
	ELSE
		SELECT 0
END

GO
/****** Object:  StoredProcedure [dbo].[hmrna_create_user_sp]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =======================================================
-- Author:		<Bou Zeidan, Nadim>
-- Create date: <2012-12-01>
-- Description:	<Create user>
-- =======================================================
CREATE PROCEDURE [dbo].[hmrna_create_user_sp]
	@user_first_name varchar(MAX) = NULL,
	@user_last_name varchar(MAX) = NULL,
	@user_email_address_text varchar(MAX) = NULL,
	@user_name varchar(MAX) = NULL,
	@user_password_hash varchar(MAX) = NULL,
	@user_password_salt varchar(MAX) = NULL,
	@user_act_flag bit,
	@user_created_dtm datetime,
	@user_created_by_name varchar(MAX) = NULL
AS
BEGIN
	SET NOCOUNT OFF;
	INSERT 
	  INTO [HumanMicroRNA].[dbo].[user_lku]
		   ([user_first_name]
		   ,[user_last_name]
		   ,[user_email_address_text]
		   ,[user_name]
		   ,[user_password_hash]
		   ,[user_password_salt]
		   ,[user_act_flag]
		   ,[user_created_dtm]
		   ,[user_created_by_name])
		   		 
	VALUES (@user_first_name
		   ,@user_last_name
		   ,@user_email_address_text
		   ,@user_name
		   ,@user_password_hash
		   ,@user_password_salt
		   ,@user_act_flag
		   ,@user_created_dtm
		   ,@user_created_by_name)
END

GO
/****** Object:  StoredProcedure [dbo].[hmrna_delete_all_db_var_sp]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =================================================
-- Author:		<Bou Zeidan, Nadim>
-- Create date: <2012-06-220>
-- Description:	<Delete all the records from db_var>
-- =================================================
CREATE PROCEDURE [dbo].[hmrna_delete_all_db_var_sp]
AS
BEGIN
	SET NOCOUNT OFF;
	DELETE [HumanMicroRNA].[dbvar].[db_var]
END

GO
/****** Object:  StoredProcedure [dbo].[hmrna_delete_records_from_table_sp]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =====================================================
-- Author:		<Nadim,Bou Zeidan>
-- Create date: <2012-06-22>
-- Description:	Delete all rRecords from specific tables
-- =====================================================
CREATE PROCEDURE [dbo].[hmrna_delete_records_from_table_sp]
	@tbl_name varchar(200) = NULL
AS
BEGIN
	SET NOCOUNT OFF;
    DECLARE @tblname varchar(200)
	SET @tblname = RTRIM(@tbl_name)
	DECLARE @cmd AS NVARCHAR(max)
	SET @cmd = N'DELETE ' + @tblname
	EXEC sp_executesql @cmd 
END

GO
/****** Object:  StoredProcedure [dbo].[hmrna_get_all_db_var_by_chromosome_sp]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================================
-- Author:		<Nadim Bou Zeidan>
-- Create date: <2012-06-23>
-- Description:	<Get all db var from the table by chromosome id>
-- =============================================================
CREATE PROCEDURE [dbo].[hmrna_get_all_db_var_by_chromosome_sp]
	@xsome_num varchar(100) = NULL
AS
BEGIN
	SET NOCOUNT OFF;
	SELECT  [var_id]
		   ,[var_symbol_name]
		   ,[var_type_id]
		   ,[xsome_num]
		   ,[range_from_num]
		   ,[range_to_num]
	  FROM  [HumanMicroRNA].[dbvar].[db_var]
	 WHERE	[xsome_num] = @xsome_num
END

GO
/****** Object:  StoredProcedure [dbo].[hmrna_get_all_db_var_sp]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Nadim Bou Zeidan>
-- Create date: <2012-06-23>
-- Description:	<Get all db var from the table>
-- =============================================
CREATE PROCEDURE [dbo].[hmrna_get_all_db_var_sp]
AS
BEGIN
	SET NOCOUNT OFF;
	SELECT  [var_id]
		   ,[var_symbol_name]
		   ,[var_type_id]
		   ,[xsome_num]
		   ,[range_from_num]
		   ,[range_to_num]
	  FROM  [HumanMicroRNA].[dbvar].[db_var]
END

GO
/****** Object:  StoredProcedure [dbo].[hmrna_get_all_gene_by_mir_family_id_sp]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Nadim Bou Zeidan>
-- Create date: <2012-08-10>
-- Description:	<Get all gene by mir family id.
-- =============================================
CREATE PROCEDURE [dbo].[hmrna_get_all_gene_by_mir_family_id_sp]
	@mirna_id VARCHAR(500) = NULL
AS
BEGIN
	SET NOCOUNT OFF;
	SELECT 
	DISTINCT  pt.[mir_family_id]
			 ,pt.[gene_id]
			 ,pt.[gene_symbol_id]
			 ,pt.[transcript_id]
			 ,pt.[species_id]
			 ,pt.[utr_start_num]
			 ,pt.[utr_end_num]
			 ,pt.[msa_start_num]
			 ,pt.[msa_end_num]
			 ,pt.[seed_match_text]
			 ,pt.[pct_text]
		FROM [HumanMicroRNA].[rna].[mir_family_info] mfi
	  
	   INNER
		JOIN [HumanMicroRNA].[rna].[predicted_target] pt ON pt.mir_family_id = mfi.mir_family_id
	   WHERE  mfi.mirna_id = @mirna_id
	ORDER BY  pt.gene_symbol_id
END

GO
/****** Object:  StoredProcedure [dbo].[hmrna_get_all_mir_family_info_sp]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ========================================================
-- Author:		<Nadim Bou Zaidan>
-- Create date: <2012-08-05>
-- Description:	<Return all of the mir_family_info records>
-- ========================================================
CREATE PROCEDURE [dbo].[hmrna_get_all_mir_family_info_sp]
AS
BEGIN
		SELECT  mir_family_id, 
		        seed_plus_m8, 
		        species_id, 
		        mirna_id, 
		        mature_sequence_text, 
		        family_conservation_id, 
		        mature_accession_id
		  FROM	rna.mir_family_info
END

GO
/****** Object:  StoredProcedure [dbo].[hmrna_get_all_mirna_records_sp]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Nadim Bou Zeidan
-- Create date: 2012-06-22
-- Description:	Return all MiRNA records
-- =============================================
CREATE PROCEDURE [dbo].[hmrna_get_all_mirna_records_sp]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  human_micro_rna_id, 
			xsome_num,
			range_from_num,
			range_to_num,
			mature_xsome_localization_text as xsome
	  FROM  human_micro_rna
	 WHERE	mature_xsome_localization_text IS NOT NULL
  ORDER BY  mature_xsome_localization_text
END

GO
/****** Object:  StoredProcedure [dbo].[hmrna_get_all_users_sp]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Nadim Bou Zeidan>
-- Create date: <2012-06-23>
-- Description:	<Gets all users from the table.>
-- =============================================
CREATE PROCEDURE [dbo].[hmrna_get_all_users_sp]
AS
BEGIN
	SET NOCOUNT OFF;
	SELECT [user_lku_id]
		  ,[user_first_name]
		  ,[user_last_name]
		  ,[user_email_address_text]
		  ,[user_name]
		  ,[user_act_flag]
		  ,[user_created_dtm]
		  ,[user_created_by_name]
      FROM [HumanMicroRNA].[dbo].[user_lku]
END

GO
/****** Object:  StoredProcedure [dbo].[hmrna_get_all_var_by_human_mirna_id_sp]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================
-- Author:		Nadim Bou Zeidan
-- Create date: 2012-06-27
-- Description:	Return all var for unique MiRNAID
-- ===============================================
CREATE PROCEDURE [dbo].[hmrna_get_all_var_by_human_mirna_id_sp]
	@human_micro_rna_id int = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  [human_micro_rna_id]
		   ,[var_id]
		   ,[chromosome_num]
		   ,[chromosome_range_from_num]
		   ,[chromosome_range_to_num]
	  FROM  hmrna_var_assn WITH(NOLOCK)
	 WHERE	human_micro_rna_id = @human_micro_rna_id
END

GO
/****** Object:  StoredProcedure [dbo].[hmrna_get_mirna_accession_by_param_sp]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================================
-- Author:		<Nadim Bou Zeidan>
-- Create date: <2012-06-25>
-- Description:	<Return MiRNA Accession ID based on param>
-- =============================================================
CREATE PROCEDURE [dbo].[hmrna_get_mirna_accession_by_param_sp]
	@mirna_accession varchar(200) = NULL
AS
BEGIN
	SET NOCOUNT OFF;
	SELECT mirna_acc_text 
	  FROM [HumanMicroRNA].[dbo].[human_micro_rna]
	 WHERE	mirna_acc_text LIKE '%' + @mirna_accession + '%'
END

GO
/****** Object:  StoredProcedure [dbo].[hmrna_get_mirna_all_by_auto_mirna_or_mirna_acc]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===================================================
-- Author:		<Bou Zeidan, Nadim>
-- Create date: <2012-05-26>
-- Description:	<Get MiRNA records all by parameters>
-- ===================================================
CREATE PROCEDURE [dbo].[hmrna_get_mirna_all_by_auto_mirna_or_mirna_acc]
	@auto_mirna bit = NULL,
	@mirna_acc bit = NULL
AS
BEGIN
	IF(@auto_mirna = 1 AND @mirna_acc = 0)
		BEGIN
			SELECT rna.mirna.auto_mirna
			  FROM rna.mirna
		END
	ELSE IF (@mirna_acc = 1 AND @auto_mirna = 0)
		BEGIN
			SELECT rna.mirna.mirna_acc 
			  FROM rna.mirna
		END
	ELSE IF (@auto_mirna = 1 AND @mirna_acc = 1)
		BEGIN
			SELECT rna.mirna.auto_mirna, rna.mirna.mirna_acc 
			  FROM rna.mirna
		END
END

GO
/****** Object:  StoredProcedure [dbo].[hmrna_get_mirna_id_by_param_sp]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================================
-- Author:		<Nadim Bou Zeidan>
-- Create date: <2012-06-25>
-- Description:	<Return MiRNA ID based on param>
-- =============================================================
CREATE PROCEDURE [dbo].[hmrna_get_mirna_id_by_param_sp]
	@mirna_id varchar(200) = NULL
AS
BEGIN
	SET NOCOUNT OFF;
	SELECT mirna_id 
	  FROM [HumanMicroRNA].[dbo].[human_micro_rna]
	 WHERE	mirna_id LIKE '%' + @mirna_id + '%'
END

GO
/****** Object:  StoredProcedure [dbo].[hmrna_get_mirna_report_by_search_criteria_sp]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Nadim Bou Zeidan>
-- Create date: <2012-06-25>
-- Description:	<Get MiRNA Report>
-- =============================================
CREATE PROCEDURE [dbo].[hmrna_get_mirna_report_by_search_criteria_sp]
	@mirna_id varchar(150) = NULL,
	@mirna_acc varchar(150) = NULL,
	@xsome varchar(150) = NULL,
	@var_id varchar(150) = NULL
AS
BEGIN
	SET NOCOUNT OFF;
	SELECT 
  DISTINCT  hmr.mirna_id, 
	        hmr.mirna_acc_text, 
	        REPLACE(hmr.xsome_num, 'chr', '') as xsome_num, 
	        hmr.range_from_num, 
	        hmr.range_to_num, 
	        hmr.strand_text
	  FROM	human_micro_rna hmr
	  LEFT 
	  JOIN  hmrna_var_assn hva ON hmr.human_micro_rna_id = hva.human_micro_rna_id 
	 WHERE  (@mirna_id IS NULL OR hmr.mirna_id = @mirna_id) 
	   AND	(@mirna_acc IS NULL OR hmr.mirna_acc_text = @mirna_acc)
	   AND	(@xsome IS NULL OR hmr.xsome_num = 'chr' + @xsome)
	   AND	(@var_id IS NULL OR hva.var_id = @var_id)
END

GO
/****** Object:  StoredProcedure [dbo].[hmrna_get_unique_mirna_by_mirna_id_sp]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ===============================================
-- Author:		Nadim Bou Zeidan
-- Create date: 2012-06-27
-- Description:	Return unique MiRNA records by id
-- ===============================================
CREATE PROCEDURE [dbo].[hmrna_get_unique_mirna_by_mirna_id_sp]
	@mirna_id varchar(100) = NULL
AS
BEGIN
	SET NOCOUNT ON;
	SELECT  [human_micro_rna_id]
		   ,[auto_mirna]
		   ,[mirna_acc_text]
		   ,[mirna_id]
		   ,[mirna_desc]
		   ,[xsome_num]
		   ,[range_from_num]
		   ,[range_to_num]
		   ,[mature_xsome_localization_text]
		   ,[strand_text]
		   ,[pre_mature_sequence_text]
		   ,[pre_mature_comment]
		   ,[mature_5p_id]
		   ,[mature_5p_acc]
		   ,[mature_5p_sequence_text]
		   ,[mature_5p_from_num]
		   ,[mature_5p_to_num]
		   ,[mature_3p_id]
		   ,[mature_3p_acc]
		   ,[mature_3p_sequence_text]
		   ,[mature_3p_from_num]
		   ,[mature_3p_to_num]
	  FROM  human_micro_rna WITH(NOLOCK)
	 WHERE	mature_xsome_localization_text IS NOT NULL
	   AND	mirna_id =  @mirna_id
END

GO
/****** Object:  StoredProcedure [dbo].[hmrna_get_user_info_by_email_address_sp]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================================
-- Author:		<Nadim Bou Zeidan>
-- Create date: <2012-07-07>
-- Description:	<Return users basic info based on email address>
-- =============================================================
CREATE PROCEDURE [dbo].[hmrna_get_user_info_by_email_address_sp]
	@email_address varchar(100) = NULL
AS
BEGIN
	SET NOCOUNT OFF;
	SELECT user_lku_id, 
	       user_first_name, 
	       user_last_name, 
	       user_email_address_text, 
	       [user_name]
      FROM [HumanMicroRNA].[dbo].[user_lku] usr WITH(NOLOCK)
	 WHERE usr.user_email_address_text = @email_address

END

GO
/****** Object:  StoredProcedure [dbo].[hmrna_get_user_info_by_user_name_sp]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================================
-- Author:		<Nadim Bou Zeidan>
-- Create date: <2012-07-07>
-- Description:	<Return users basic info based on username>
-- =============================================================
CREATE PROCEDURE [dbo].[hmrna_get_user_info_by_user_name_sp]
	@username varchar(100) = NULL
AS
BEGIN
	SET NOCOUNT OFF;
	SELECT user_lku_id, 
	       user_first_name, 
	       user_last_name, 
	       user_email_address_text, 
	       [user_name]
      FROM [HumanMicroRNA].[dbo].[user_lku] usr WITH(NOLOCK)
	 WHERE usr.[user_name] = @username

END

GO
/****** Object:  StoredProcedure [dbo].[hmrna_get_var_id_by_param_sp]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================================
-- Author:		<Nadim Bou Zeidan>
-- Create date: <2012-06-25>
-- Description:	<Return Var ID based on param>
-- =============================================================
CREATE PROCEDURE [dbo].[hmrna_get_var_id_by_param_sp]
	@var_id varchar(200) = NULL
AS
BEGIN
	SET NOCOUNT OFF;
	SELECT DISTINCT var_id 
	  FROM [HumanMicroRNA].[dbo].[hmrna_var_assn]
	 WHERE	var_id LIKE '%' + @var_id + '%'
END

GO
/****** Object:  StoredProcedure [dbo].[hmrna_log_application_errors_sp]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =======================================================
-- Author:		<Bou Zeidan, Nadim>
-- Create date: <2012-05-26>
-- Description:	<Log exceptions thrown by the application>
-- =======================================================
CREATE PROCEDURE [dbo].[hmrna_log_application_errors_sp]
	@exception_log_text xml = NULL,
	@exception_from_method_text varchar(100) = NULL,
	@exception_dtm datetime = NULL,
	@exception_by_name varchar(200) = NULL
AS
BEGIN
	SET NOCOUNT OFF;
	INSERT 
	  INTO exception_log
		   ([exception_log_text]
		   ,[exception_from_method_text]
		   ,[exception_dtm]
		   ,[exception_by_name])
		   		 
	VALUES (@exception_log_text,
		    @exception_from_method_text,
	        @exception_dtm,
	        @exception_by_name)
END

GO
/****** Object:  StoredProcedure [dbo].[hmrna_validate_user_credentials_sp]    Script Date: 11/20/2014 12:51:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================================
-- Author:		<Nadim Bou Zeidan>
-- Create date: <2012-07-03>
-- Description:	<Validate user credentials>
-- =============================================================
CREATE PROCEDURE [dbo].[hmrna_validate_user_credentials_sp]
	@username varchar(100) = NULL
AS
BEGIN
	SET NOCOUNT OFF;
	SELECT user_lku_id, 
	       user_first_name, 
	       user_last_name, 
	       user_email_address_text, 
	       [user_name], 
	       user_password_hash, 
	       user_password_salt, 
	       user_act_flag, 
           user_created_dtm, 
           user_created_by_name
      FROM [HumanMicroRNA].[dbo].[user_lku] usr WITH(NOLOCK)
	 WHERE usr.[user_name] = @username

END

GO
