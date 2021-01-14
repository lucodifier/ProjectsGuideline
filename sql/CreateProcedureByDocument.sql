USE [Guideline]
GO
/***** Object: StoredProcedure [dbo].[Users with Document] Script Date: 13/01/2021 23:17:52 *****/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author: <Author,,Name>
-- Create date: <Create Date,,>
-- Description: <Description,,>
-- =============================================
--DROP Procedure [Users with Document]
create PROCEDURE [dbo].[Users by Doc]

@Document NVARCHAR(20)

AS
BEGIN
-- SET NOCOUNT ON added to prevent extra result sets from
-- interfering with SELECT statements.
SET NOCOUNT ON;

-- Insert statements for procedure here
/***** Script do comando SelectTopNRows de SSMS *****/
SELECT TOP (1000) [Id]
,[Name]
,[Login]
,[Email]
,[Document]
,[Pass]
,[Created]
FROM [Guideline].[dbo].[User]
WHERE
[Document] = @Document
END