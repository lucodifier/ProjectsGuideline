namespace Guideline.Infra.Data.Queries
{
    public static class UserQuery
    {
        public const string SELECT = @"SELECT [Id]
              ,[Name]
              ,[Login]
              ,[Email]
              ,[Document]
              ,[Pass]
              ,[Created]
            FROM  [dbo].[User]";

        public const string INSERT = @"INSERT INTO [dbo].[User]
           ([Id]
           ,[Name]
           ,[Login]
           ,[Email]
           ,[Document]
           ,[Pass]
           ,[Created])
             VALUES
                   (@Id
                   ,@Name
                   ,@Login
                   ,@Email
                   ,@Document
                   ,@Pass
                   ,@Created)";

        public const string UPDATE = @"UPDATE [dbo].[User]
           SET [Name] = @name
              ,[Login] = @login
              ,[Email] = @email
              ,[Document] = @document
         WHERE id = @id";

        public const string DELETE = @"DELETE FROM [dbo].[User] WHERE Id = @id";
    }
}
