using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomCodedUI.Application.WebAppDemo
{
    public static class TableExtension
    {
        public static HtmlCell GetCellByIndex(this HtmlTable @this, int rowIndex, int columnIndex)
        {
            HtmlCell cell = new HtmlCell(@this);
            cell.SearchProperties[HtmlCell.PropertyNames.RowIndex] = rowIndex.ToString();
            cell.SearchProperties[HtmlCell.PropertyNames.ColumnIndex] = columnIndex.ToString();
            cell.Find();
            return cell;
        }

        public static HtmlHeaderCell GetHeaderCellByColumnIndex(this HtmlTable @this, int columnIndex)
        {
            HtmlHeaderCell header = new HtmlHeaderCell(@this);
            header.SearchProperties[HtmlCell.PropertyNames.RowIndex] = "0";
            header.SearchProperties[HtmlCell.PropertyNames.ColumnIndex] = columnIndex.ToString();
            header.Find();
            return header;
        }

        public static int GetColIndexByColName(this HtmlTable @this, string colName)
        {
            for (int index = 0; index < @this.ColumnCount; index++)
            {
                string title = @this.GetHeaderCellByColumnIndex(index).InnerText;
                if (colName.Equals(title, StringComparison.CurrentCultureIgnoreCase))
                {
                    return index;
                }
            }
            return 0;
        }

        public static int GetRowIndexByColValue(this HtmlTable @this, int colIndex, string colValue)
        {
            for (int index = 0; index < @this.RowCount; index++)
            {
                string cellValue = @this.GetCellByIndex(index, colIndex).InnerText;
                if (colValue.Equals(cellValue, StringComparison.CurrentCultureIgnoreCase))
                {
                    return index;
                }
            }
            return 0;
        }

        public static int GetFirstCellIndexMatchedValue(this HtmlTable @this, string expectedCellValue, params Int32[] startRowIndex)
        {
            int startrow = 0;
            int retVal = 0;
            string actual = "";
            if (startRowIndex != null)
            {
                startrow = startRowIndex[0];
            }
            for (int rowIndex = startrow; rowIndex < @this.RowCount; rowIndex++)
            {
                for (int colIndex = 0; colIndex < @this.ColumnCount; colIndex++)
                {
                    actual = @this.GetCellByIndex(rowIndex, colIndex).InnerText;
                    if (expectedCellValue.Equals(actual, StringComparison.CurrentCultureIgnoreCase))
                    {
                        retVal = rowIndex * 10 + colIndex;
                        return retVal;
                    }
                }
            }
            return retVal;
        }

    }
}
