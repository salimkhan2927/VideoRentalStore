namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'4f78f1b5-915c-423b-8f03-b918346a11a1', N'guest@vidly.com', 0, N'AL4dX+5OXnPadlAJVar8olRj53qhKI8jW6tyB03/79ZPdaT2N2iiI8EgMcpgGgcKGg==', N'66b190a2-0a45-4e99-95df-98f1bad8ad46', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
             INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'c6b8d356-fe55-4f83-bbd2-1f2f033d0ae6', N'admin@vidly.com', 0, N'AMLtz/2jVt+b2hZvFUaY791k0vEwO+/isMPzLcleReC7R1iiRC2Oc8MjD2dV6kKecA==', N'bbffeee3-5759-41cd-8b48-caf1416f7a0e', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'c2a9398d-a2e6-4ff7-9ef9-7019933734b8', N'CanManageMovies')
            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c6b8d356-fe55-4f83-bbd2-1f2f033d0ae6', N'c2a9398d-a2e6-4ff7-9ef9-7019933734b8')
");
        }

        public override void Down()
        {
        }
    }
}
