namespace CodeFirstDatabase.Utils
{
    public class RoleUtils
    {
        public const string RoleSuperAdmin = "SuperAdmin";
        public const string RoleAdmin = "Admin";
        public const string RoleAuthor = "Author";
        public const string RoleUser = "User";
        public class Options
        {
            public const string RoleSuperOrAdmin = RoleSuperAdmin + "," + RoleAdmin;
        }
    }
}
