using ClosedXML.Excel;
using IronPdf;
using CsvHelper;
using EmptyProject.Areas.CMSCore.Entities;
using EmptyProject.Areas.CMSCore.Interfaces;
using System.Data;
using System.Globalization;

namespace EmptyProject.Areas.CMSCore.Services
{
    public class RoleMenuService : IRoleMenuService
    {
        #region Exportations
        public void ExportToExcel(string path, DataTable dtRoleMenu)
        {
            using var Book = new XLWorkbook();

            DataTable dtRoleMenuOriginal = new()
            {
                TableName = "RoleMenu"
            };

            //We define another DataTable dtRoleMenuCopy to avoid issue related to DateTime conversion
            DataTable dtRoleMenuCopy = new()
            {
                TableName = "RoleMenu"
            };

            #region Define columns for dtRoleMenuCopy
            DataColumn dtColumnRoleMenuIdFordtRoleMenuCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "RoleMenuId"
            };
            dtRoleMenuCopy.Columns.Add(dtColumnRoleMenuIdFordtRoleMenuCopy);

            DataColumn dtColumnActiveFordtRoleMenuCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "Active"
            };
            dtRoleMenuCopy.Columns.Add(dtColumnActiveFordtRoleMenuCopy);

            DataColumn dtColumnDateTimeCreationFordtRoleMenuCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "DateTimeCreation"
            };
            dtRoleMenuCopy.Columns.Add(dtColumnDateTimeCreationFordtRoleMenuCopy);

            DataColumn dtColumnDateTimeLastModificationFordtRoleMenuCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "DateTimeLastModification"
            };
            dtRoleMenuCopy.Columns.Add(dtColumnDateTimeLastModificationFordtRoleMenuCopy);

            DataColumn dtColumnUserCreationIdFordtRoleMenuCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "UserCreationId"
            };
            dtRoleMenuCopy.Columns.Add(dtColumnUserCreationIdFordtRoleMenuCopy);

            DataColumn dtColumnUserLastModificationIdFordtRoleMenuCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "UserLastModificationId"
            };
            dtRoleMenuCopy.Columns.Add(dtColumnUserLastModificationIdFordtRoleMenuCopy);

            DataColumn dtColumnMenuIdFordtRoleMenuCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "MenuId"
            };
            dtRoleMenuCopy.Columns.Add(dtColumnMenuIdFordtRoleMenuCopy);

            DataColumn dtColumnRoleIdFordtRoleMenuCopy = new()
            {
                DataType = typeof(string),
                ColumnName = "RoleId"
            };
            dtRoleMenuCopy.Columns.Add(dtColumnRoleIdFordtRoleMenuCopy);

            
            #endregion

            dtRoleMenuOriginal = dtRoleMenu;

            foreach (DataRow DataRow in dtRoleMenuOriginal.Rows)
            {
                dtRoleMenuCopy.Rows.Add(DataRow.ItemArray);
            }

            var Sheet = Book.Worksheets.Add(dtRoleMenuCopy);

            Sheet.ColumnsUsed().AdjustToContents();

            Book.SaveAs(path);
        }

        public void ExportToCSV(string path, List<RoleMenu> lstRoleMenu)
        {
            using var Writer = new StreamWriter(path);

            using var CsvWriter = new CsvWriter(Writer, CultureInfo.InvariantCulture);

            CsvWriter.WriteRecords(lstRoleMenu);
        }

        public void ExportToPDF(string path, List<RoleMenu> lstRoleMenu)
        {
            string ProjectName = "EmptyProject";
            string Table = "RoleMenu";
            var Renderer = new HtmlToPdf();
            string RowsAsHTML = "";


            foreach (RoleMenu? RoleMenu in lstRoleMenu)
            {
                RowsAsHTML += $@"{RoleMenu?.ToStringOnlyValuesForHTML()}";
            }

            Renderer.RenderHtmlAsPdf($@"<table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""88%"" style=""width: 88% !important; min-width: 88%; max-width: 88%;"">
    <tr>
    <td align=""left"" valign=""top"">
        <font face=""'Source Sans Pro', sans-serif"" color=""#1a1a1a"" style=""font-size: 52px; line-height: 55px; font-weight: 300; letter-spacing: -1.5px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #1a1a1a; font-size: 52px; line-height: 55px; font-weight: 300; letter-spacing: -1.5px;"">{ProjectName}</span>
        </font>
        <div style=""height: 25px; line-height: 25px; font-size: 23px;"">&nbsp;</div>
        <font face=""'Source Sans Pro', sans-serif"" color=""#4c4c4c"" style=""font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">
            <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #4c4c4c; font-size: 36px; line-height: 45px; font-weight: 300; letter-spacing: -1px;"">Registers of {Table}</span>
        </font>
        <div style=""height: 35px; line-height: 35px; font-size: 33px;"">&nbsp;</div>
    </td>
    </tr>
</table>
<br>
<table cellpadding=""0"" cellspacing=""0"" border=""0"" width=""100%"" style=""width: 100% !important; min-width: 100%; max-width: 100%;"">
    <tr>
        <th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">RoleMenuId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Active&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">DateTimeCreation&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">DateTimeLastModification&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">UserCreationId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">UserLastModificationId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Boolean&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">DateTime&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Decimal&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">Integer&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">TextArea&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">TextBasic&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">TextEditor&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">TextEmail&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">TextFile&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">TextHexColour&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">TextPassword&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">TextPhoneNumber&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">TextTag&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">TextURL&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th><th align=""left"" valign=""top"" style=""border-width: 1px; border-style: solid; border-color: #e8e8e8; border-top: none; border-left: none; border-right: none;"">
            <font face=""'Source Sans Pro', sans-serif"" color=""#000000"" style=""font-size: 20px; line-height: 28px; font-weight: 600;"">
                <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #000000; font-size: 20px; line-height: 28px; font-weight: 600;"">RoleMenuStatusId&nbsp;&nbsp;&nbsp;</span>
            </font>
            <div style=""height: 10px; line-height: 10px; font-size: 8px;"">&nbsp;</div>
        </th>
    </tr>
    {RowsAsHTML}
</table>
<br>
<font face=""'Source Sans Pro', sans-serif"" color=""#868686"" style=""font-size: 17px; line-height: 20px;"">
    <span style=""font-family: 'Source Sans Pro', Arial, Tahoma, Geneva, sans-serif; color: #868686; font-size: 17px; line-height: 20px;"">Printed on: {DateTime.Now}</span>
</font>
").SaveAs(path);
        }
        #endregion

        #region Importations
        public List<RoleMenu> ImportExcel(string path, int userId)
        {
            List<RoleMenu> lstRoleMenu = [];

            var WorkBook = new XLWorkbook(path);
            var Rows = WorkBook.Worksheet(1).RangeUsed().RowsUsed();

            foreach (var row in Rows)
            {
                var rowNumber = row.RowNumber();

                if (rowNumber > 1)
                {
                    int MenuId = Convert.ToInt32(row.Cell(7).GetString());
                    int RoleId = Convert.ToInt32(row.Cell(8).GetString());
                    

                    RoleMenu RoleMenu = new()
                    {
                        RoleMenuId = 0,
                        Active = true,
                        DateTimeCreation = DateTime.Now,
                        DateTimeLastModification = DateTime.Now,
                        UserCreationId = userId,
                        UserLastModificationId = userId,
                        MenuId = MenuId,
                        RoleId = RoleId,
                        
                    };

                    lstRoleMenu.Add(RoleMenu);
                }
            }

            return lstRoleMenu;
        }
        #endregion
    }
}
