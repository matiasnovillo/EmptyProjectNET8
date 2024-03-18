using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmptyProject.Migrations
{
    /// <inheritdoc />
    public partial class CommonSPs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            #region SP1: DataBaseVersion
            migrationBuilder.Sql($@"
CREATE procedure [dbo].[CommonFunctions.MSSQLServer.DataBaseVersion]

AS

SELECT @@version");
            #endregion

            #region SP2: ExistDataBase
            migrationBuilder.Sql($@"
CREATE procedure [dbo].[CommonFunctions.MSSQLServer.ExistDataBase]
(
	@DataBaseName VARCHAR(MAX)
)

AS

IF DB_ID(@DataBaseName) IS NOT NULL
	SELECT 1
ELSE
	SELECT 0");
            #endregion

            #region SP3: ExistField
            migrationBuilder.Sql($@"
CREATE PROCEDURE [dbo].[CommonFunctions.MSSQLServer.ExistField]
(
	@TableArea VARCHAR(MAX),
	@TableName VARCHAR(MAX),
	@SchemeName VARCHAR(MAX),
	@FieldName VARCHAR(MAX)
)

AS

IF EXISTS (SELECT 1 FROM sys.columns 
          WHERE 1 = 1
			  AND Name = @FieldName
			  AND Object_ID = Object_ID('[' + @SchemeName + '].[' + @TableArea + '.' + @TableName + ']'))
	SELECT 1 
ELSE 
	SELECT 0");
            #endregion

            #region SP4: ExistStoredProcedure
            migrationBuilder.Sql($@"
CREATE procedure [dbo].[CommonFunctions.MSSQLServer.ExistStoredProcedure]
(
	@DataBaseName VARCHAR(MAX),
	@SchemeName VARCHAR(MAX),
	@TableName VARCHAR(MAX),
	@TableArea VARCHAR(MAX),
	@Action VARCHAR(MAX),
	@IsFiyiStackSP TINYINT
)

AS

IF EXISTS (SELECT 1 
			FROM INFORMATION_SCHEMA.ROUTINES
			WHERE 1 = 1
				AND ROUTINE_TYPE = 'PROCEDURE'
				AND ROUTINE_CATALOG = @DataBaseName
				AND ROUTINE_SCHEMA = @SchemeName
				AND ((ROUTINE_NAME = @TableArea + '.' + @TableName + '.' +@Action AND @IsFiyiStackSP = 1) OR (ROUTINE_NAME = @Action AND @IsFiyiStackSP = 0))
			)  
	SELECT 1 
ELSE 
	SELECT 0");
            #endregion

            #region SP5: ExistTable
            migrationBuilder.Sql($@"
CREATE PROCEDURE [dbo].[CommonFunctions.MSSQLServer.ExistTable]
(
	@TableArea VARCHAR(MAX),
	@TableName VARCHAR(MAX),
	@Scheme VARCHAR(MAX)
)

AS

IF EXISTS (SELECT 1 
           FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_TYPE = 'BASE TABLE' 
			   AND TABLE_NAME = @TableArea + '.' + @TableName
			   AND TABLE_SCHEMA = @Scheme) 
	SELECT 1 
ELSE 
	SELECT 0");
            #endregion

            #region SP6: GetAllFieldsByTableNameBySchemeName
            migrationBuilder.Sql($@"
CREATE procedure [dbo].[CommonFunctions.MSSQLServer.GetAllFieldsByTableNameBySchemeName]
(
	@TableName VARCHAR(MAX),
	@SchemeName VARCHAR(MAX)
)

AS

SELECT 
COLUMN_NAME AS [Name],
CASE
	WHEN DATA_TYPE = 'int' THEN DATA_TYPE 
	WHEN DATA_TYPE = 'varchar' THEN DATA_TYPE + '(' + (CASE WHEN CHARACTER_MAXIMUM_LENGTH = -1 THEN 'MAX' ELSE CONVERT(VARCHAR(MAX), CHARACTER_MAXIMUM_LENGTH) END) + ')'
	WHEN DATA_TYPE = 'numeric' THEN DATA_TYPE + '(' + CONVERT(VARCHAR(MAX), NUMERIC_PRECISION) + ',' + CONVERT(VARCHAR(MAX), NUMERIC_SCALE) + ')'
	WHEN DATA_TYPE = 'datetime' THEN DATA_TYPE
	WHEN DATA_TYPE = 'time' THEN DATA_TYPE + '(' + CONVERT(VARCHAR(MAX), DATETIME_PRECISION) + ')'
	ELSE DATA_TYPE
END AS [DataTypeName],
CASE WHEN IS_NULLABLE = 'YES' THEN 1 ELSE 0 END AS [Nullable]

FROM 
	INFORMATION_SCHEMA.COLUMNS
WHERE 1 = 1
	AND TABLE_NAME = @TableName
	AND TABLE_SCHEMA = @SchemeName");
            #endregion

            #region SP7: GetAllTablesByDataBaseName
            migrationBuilder.Sql($@"
CREATE procedure [dbo].[CommonFunctions.MSSQLServer.GetAllTablesByDataBaseName]
(
	@DataBaseName VARCHAR(MAX)
)

AS

SELECT 
TABLE_NAME AS [Name],
TABLE_SCHEMA AS [Scheme]
FROM 
	INFORMATION_SCHEMA.TABLES 
WHERE 1=1
	AND TABLE_TYPE = 'BASE TABLE' 
	AND TABLE_CATALOG = @DataBaseName");
            #endregion

            #region SP8: GetOneFieldByTableNameBySchemeNameByFieldName
            migrationBuilder.Sql($@"
CREATE procedure [dbo].[CommonFunctions.MSSQLServer.GetOneFieldByTableNameBySchemeNameByFieldName]
(
	@TableName VARCHAR(MAX),
	@SchemeName VARCHAR(MAX),
	@FieldName VARCHAR(MAX)
)

AS

SELECT 
COLUMN_NAME AS [Name],
CASE
	WHEN DATA_TYPE = 'int' THEN DATA_TYPE 
	WHEN DATA_TYPE = 'varchar' THEN DATA_TYPE + '(' + (CASE WHEN CHARACTER_MAXIMUM_LENGTH = -1 THEN 'MAX' ELSE CONVERT(VARCHAR(MAX), CHARACTER_MAXIMUM_LENGTH) END) + ')'
	WHEN DATA_TYPE = 'numeric' THEN DATA_TYPE + '(' + CONVERT(VARCHAR(MAX), NUMERIC_PRECISION) + ',' + CONVERT(VARCHAR(MAX), NUMERIC_SCALE) + ')'
	WHEN DATA_TYPE = 'datetime' THEN DATA_TYPE
	WHEN DATA_TYPE = 'time' THEN DATA_TYPE + '(' + CONVERT(VARCHAR(MAX), DATETIME_PRECISION) + ')'
	ELSE DATA_TYPE
END AS [DataTypeName],
CASE WHEN IS_NULLABLE = 'YES' THEN 1 ELSE 0 END AS [Nullable]

FROM 
	INFORMATION_SCHEMA.COLUMNS
WHERE 1 = 1
	AND TABLE_NAME = @TableName
	AND TABLE_SCHEMA = @SchemeName
	AND COLUMN_NAME = @FieldName");
            #endregion

            #region SP9: GetOneStoredProcedureByDataBaseNameBySchemeNameByName
            migrationBuilder.Sql($@"
CREATE procedure [dbo].[CommonFunctions.MSSQLServer.GetOneStoredProcedureByDataBaseNameBySchemeNameByName]
(
	@DataBaseName VARCHAR(MAX),
	@SchemeName VARCHAR(MAX),
	@Name VARCHAR(MAX)
)

AS

SELECT
	ROUTINE_NAME AS [Name],
	ROUTINE_DEFINITION AS [Content]
FROM 
	INFORMATION_SCHEMA.ROUTINES
WHERE 1 = 1
	AND ROUTINE_TYPE = 'PROCEDURE'
	AND ROUTINE_CATALOG = @DataBaseName
	AND ROUTINE_SCHEMA = @SchemeName
	AND ROUTINE_NAME = @Name");
            #endregion
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[CommonFunctions.MSSQLServer.DataBaseVersion]");
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[CommonFunctions.MSSQLServer.ExistDataBase]");
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[CommonFunctions.MSSQLServer.ExistField]");
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[CommonFunctions.MSSQLServer.ExistStoredProcedure]");
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[CommonFunctions.MSSQLServer.ExistTable]");
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[CommonFunctions.MSSQLServer.GetAllFieldsByTableNameBySchemeName]");
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[CommonFunctions.MSSQLServer.GetAllTablesByDataBaseName]");
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[CommonFunctions.MSSQLServer.GetOneFieldByTableNameBySchemeNameByFieldName]");
            migrationBuilder.Sql("DROP PROCEDURE [dbo].[CommonFunctions.MSSQLServer.GetOneStoredProcedureByDataBaseNameBySchemeNameByName]");
        }
    }
}
