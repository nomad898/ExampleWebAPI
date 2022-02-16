using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Example.Database.EF.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserGroup",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "UserState",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserState", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserGroupID = table.Column<int>(type: "int", nullable: false),
                    UserStateID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                    table.ForeignKey(
                        name: "FK_User_UserGroup_UserGroupID",
                        column: x => x.UserGroupID,
                        principalTable: "UserGroup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_UserState_UserStateID",
                        column: x => x.UserStateID,
                        principalTable: "UserState",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "UserGroup",
                columns: new[] { "ID", "Code", "Description" },
                values: new object[,]
                {
                    { 1, "Admin", "Admin Role" },
                    { 2, "User", "User Role" }
                });

            migrationBuilder.InsertData(
                table: "UserState",
                columns: new[] { "ID", "Code", "Description" },
                values: new object[,]
                {
                    { 1, "Active", "Active State" },
                    { 2, "Blocked", "Blocked State" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "ID", "CreatedDate", "Login", "Password", "UserGroupID", "UserStateID" },
                values: new object[] { 1, new DateTime(2022, 2, 15, 13, 0, 35, 352, DateTimeKind.Local).AddTicks(8386), "admin", "admin", 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_User_Login",
                table: "User",
                column: "Login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserGroupID",
                table: "User",
                column: "UserGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserStateID",
                table: "User",
                column: "UserStateID");

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_Code",
                table: "UserGroup",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserState_Code",
                table: "UserState",
                column: "Code",
                unique: true);

            var createTrigInsertRestrictAdminRoleAmount
                = @"CREATE TRIGGER trigInsertRestrictAdminRoleAmount ON [dbo].[User] INSTEAD OF INSERT
                    AS
                    BEGIN
                        DECLARE @NewUserGroupID INT
                        SELECT @NewUserGroupID = i.UserGroupID
                        FROM inserted i
                        WHERE i.UserGroupID = 1;
                        IF (@NewUserGroupID IS NOT NULL
                            AND EXISTS (
                                SELECT 1
                                FROM [dbo].[User] u
                                WHERE u.UserGroupID = 1
                            )
                        )
                        BEGIN
                            ROLLBACK TRANSACTION;
                            RAISERROR ('Cannot INSERT the new user. Only one Admin is allowed.', 10, 1);
                            RETURN;
                        END;     
                        INSERT INTO [dbo].[User] (Login, Password, CreatedDate, UserGroupID, UserStateID)
                        SELECT i.Login, i.Password, i.CreatedDate, i.UserGroupID, i.UserStateID
                        FROM inserted i;

                        COMMIT TRANSACTION;
                    END
                ";
            migrationBuilder.Sql(createTrigInsertRestrictAdminRoleAmount);

            var createTrigUpdateRestrictAdminRoleAmount
                = @"CREATE TRIGGER trigUpdateRestrictAdminRoleAmount ON [dbo].[User] INSTEAD OF UPDATE
                    AS
                    BEGIN
                        DECLARE @AdminRoleID INT
                        SELECT @AdminRoleID = 1

                        DECLARE @NewGroupID INT
                        SELECT @NewGroupID = i.UserGroupID
                        FROM inserted i;

                        IF (UPDATE(UserGroupID)
                            AND @NewGroupID = @AdminRoleID
                            AND EXISTS (
                                SELECT 1
                                FROM [dbo].[User] u                               
                                WHERE u.UserGroupID = @AdminRoleID
                            )
                        )
                        BEGIN
                            DECLARE @IsAdmin INT
                            SELECT @IsAdmin = u.UserGroupID
                                       FROM [dbo].[User] u
                                       JOIN inserted i
                                       ON u.ID = i.ID
                                       WHERE u.UserGroupID = @AdminRoleID;                            

                            IF (@IsAdmin IS NULL)
                            BEGIN
                                ROLLBACK TRANSACTION;
                                RAISERROR ('Cannot UPDATE the user. Only one Admin is allowed.', 10, 1);
                                RETURN;
                            END;    
                        END;
                
                        DECLARE @countOfAdmins INT 
                        SELECT @countOfAdmins = COUNT(i.ID)
                        FROM inserted i
                        WHERE i.UserGroupID = @AdminRoleID;

                        IF (@countOfAdmins > 1) 
                        BEGIN
                                ROLLBACK TRANSACTION;
                                RAISERROR ('Cannot UPDATE the batch of users. Only one Admin is allowed.', 10, 1);
                                RETURN;
                        END;

                        UPDATE [dbo].[User] SET UserGroupID = i.UserGroupID  
                        FROM inserted i
                        JOIN [dbo].[User] u 
                        ON u.ID = i.ID;

                        COMMIT TRANSACTION;
                    END
                ";
            migrationBuilder.Sql(createTrigUpdateRestrictAdminRoleAmount);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserGroup");

            migrationBuilder.DropTable(
                name: "UserState");

            var dropTrigInsertRestrictAdminRoleAmount 
                = "DROP TRIGGER IF EXISTS trigInsertRestrictAdminRoleAmount";
            migrationBuilder.Sql(dropTrigInsertRestrictAdminRoleAmount);

            var dropTrigUpdateRestrictAdminRoleAmount 
                = "DROP TRIGGER IF EXISTS trigUpdateRestrictAdminRoleAmount";
            migrationBuilder.Sql(dropTrigUpdateRestrictAdminRoleAmount);
        }
    }
}
