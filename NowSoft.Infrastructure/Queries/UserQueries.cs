using System.Diagnostics.CodeAnalysis;

namespace NowSoft.Sql.Queries
{
    [ExcludeFromCodeCoverage]
	public static class UserQueries
	{
		public static string AllUsers => "SELECT * FROM [User] (NOLOCK)";

        public static string GetBalance => "SELECT BalancAmount FROM [User] (NOLOCK) WHERE [Id] = @Id";
        public static string UserById => "SELECT * FROM [User] (NOLOCK) WHERE [Id] = @Id";

		public static string AddUser =>
            @"INSERT INTO [User] ([UserName],[Password],[FirstName], [LastName], [Device], [IpAddress]) 
				VALUES (@UserName,@Password,@FirstName, @LastName, @Device, @IpAddress)";
		public static string UpdateUser =>
            @"UPDATE [User] 
            SET [FirstName] = @FirstName, 
				[LastName] = @LastName, 
				[Device] = @Device, 
				[IpAddress] = @IpAddress
            WHERE [Id] = @UserId";

        public static string UpdateUserBalance =>
            @"UPDATE [User] 
            SET  
				[BalancAmount] = @BalancAmount
            WHERE [Id] = @Id";

        public static string UserLogin => "SELECT * FROM [User] (NOLOCK) WHERE [UserName] = @UserName AND [Password]= @Password";

        public static string DeleteUser => "DELETE FROM [User] WHERE [Id] = @UserId";
	}
}
