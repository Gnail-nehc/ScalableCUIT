using CustomCodedUI.Framework;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CustomCodedUI.Application.WebAppDemo
{
    public abstract class TestBase : TestModel
    {
        private DataTable controlInfo = new DataTable();

        public AppRoot DefaultBrowser
        {
            get
            {
                return new AppRoot();
            }
        }

        private void LoadControlUsed()
        {
            try
            {
                string sql = "select " + RetControl.UIType + "," + RetControl.UIProperty + "," + RetControl.PropValue + "," + RetControl.UIName + " from " + RetControl.FuncInDB + "(" + this.PID + ")";
                this.controlInfo = SqlHelper.GetDataTableBySql(sql);
            }
            catch (Exception ex)
            {
                Reporter.LogError("Fail to load controls in project [{0}].", PID);
                throw ex;
            }
        }

        public UITestControl Gui(string controlName)
        {
            return Gui(controlName, this.DefaultBrowser);
        }

        public UITestControl Gui(string controlName, BrowserWindow bw)
        {
            try
            {
                IEnumerable<DataRow> query = from row in controlInfo.AsEnumerable()
                                             where row.Field<string>(RetControl.UIName) == controlName
                                             select row;
                if (query.Count() != 0)
                {
                    HtmlControl target;
                    DataTable dt = query.CopyToDataTable<DataRow>();
                    string type = dt.Rows[0].Field<string>(RetControl.UIType);
                    target = ConvertStr2ControlObj(type, bw) as HtmlControl;
                    foreach (DataRow row in dt.Rows)
                    {
                        string property = row.Field<string>(RetControl.UIProperty);
                        string propValue = row.Field<string>(RetControl.PropValue);
                        target.SearchProperties[property] = propValue;
                    }
                    return target;
                }
                else
                {
                    throw new RetrieveControlFailException(controlName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private UITestControl ConvertStr2ControlObj(string type, BrowserWindow root)
        {
            switch (type)
            {
                case "BrowserWindow":
                    return new BrowserWindow();
                case "HtmlAreaHyperlink":
                    return new HtmlAreaHyperlink(root);
                case "HtmlButton":
                    return new HtmlButton(root);
                case "HtmlCell":
                    return new HtmlCell(root);
                case "HtmlCheckBox":
                    return new HtmlCheckBox(root);
                case "HtmlComboBox":
                    return new HtmlComboBox(root);
                case "HtmlControl":
                    return new HtmlControl(root);
                case "HtmlCustom":
                    return new HtmlCustom(root);
                case "HtmlDiv":
                    return new HtmlDiv(root);
                case "HtmlDocument":
                    return new HtmlDocument(root);
                case "HtmlEdit":
                    return new HtmlEdit(root);
                case "HtmlEditableDiv":
                    return new HtmlEditableDiv(root);
                case "HtmlEditableSpan":
                    return new HtmlEditableSpan(root);
                case "HtmlFileInput":
                    return new HtmlFileInput(root);
                case "HtmlFrame":
                    return new HtmlFrame(root);
                case "HtmlHeaderCell":
                    return new HtmlHeaderCell(root);
                case "HtmlHyperlink":
                    return new HtmlHyperlink(root);
                case "HtmlIFrame":
                    return new HtmlIFrame(root);
                case "HtmlImage":
                    return new HtmlImage(root);
                case "HtmlInputButton":
                    return new HtmlInputButton(root);
                case "HtmlLabel":
                    return new HtmlLabel(root);
                case "HtmlList":
                    return new HtmlList(root);
                case "HtmlListItem":
                    return new HtmlListItem(root);
                case "HtmlRadioButton":
                    return new HtmlRadioButton(root);
                case "HtmlRow":
                    return new HtmlRow(root);
                case "HtmlScrollBar":
                    return new HtmlScrollBar(root);
                case "HtmlSpan":
                    return new HtmlSpan(root);
                case "HtmlTable":
                    return new HtmlTable(root);
                case "HtmlTextArea":
                    return new HtmlTextArea(root);
                default:
                    return null;
            }
        }


        #region Initialization
        public override void Init()
        {
            //Load all Control info from DB
            this.LoadControlUsed();

            DefaultBrowser.CloseBrowser();
            //Clean Cache and Cookie
            //AppRoot.ClearCache();
            //AppRoot.ClearCookies();
            DefaultBrowser.LaunchUrl(new Uri(GlobalData["AnhUri"]));
            DefaultBrowser.WaitForControlReady();
            DefaultBrowser.Maximized = true;
        }
        #endregion


        public override void Run()
        {
            //Login
            ((HtmlEdit)Gui("UserLogIn.user")).Text = GlobalData["Username"];
            ((HtmlEdit)Gui("UserLogIn.pwd")).Text = GlobalData["Password"];
            Mouse.Click(((HtmlInputButton)Gui("LoginBtn")));
            DefaultBrowser.WaitForControlReady(10 * 1000);
        }


        #region Close Browser
        public override void End()
        {
            //Logout
            Mouse.Click(((HtmlHyperlink)Gui("LogoutBtn")));
            //DefaultBrowser.Close();
        }
        #endregion
    }

    public class RetrieveControlFailException : Exception
    {
        public RetrieveControlFailException(string controlName)
            : base(string.Format("Fail to load control! Name: [{0}].Please check DB first.", controlName))
        {

        }
    }
}
