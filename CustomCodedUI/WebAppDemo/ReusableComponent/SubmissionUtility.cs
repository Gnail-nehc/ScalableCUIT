using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomCodedUI.Application.WebAppDemo
{
    public static class SubmissionUtility
    {
        public static bool ImportSubmission(this TestBase @this, out string submissionNo)
        {
            submissionNo = "";
            return true;
            //List<PropertyFinder> listp=new List<PropertyFinder>();
            //listp.Add(new PropertyFinder(HtmlProperty.InnerText, @this.HtmlHyperlink["AvailableSubmissions.InnerText"]));
            //listp.Add(new PropertyFinder(HtmlProperty.TagInstance, @this.HtmlHyperlink["AvailableSubmissions.TagInstance"]));
            //Mouse.Click(@this.Anh.HyperlinkFinder(listp));
            //@this.Anh.WaitForControlReady(20 * 1000);
            //HtmlTable tb = @this.Anh.TableFinder(@this.HtmlTable["SubmissionTabel.Id"]);
            //if (tb.Exists)
            //{
            //    List<PropertyFinder> lp = new List<PropertyFinder>();
            //    lp.Add(new PropertyFinder(HtmlProperty.Type, @this.HtmlEdit["SearchBox.Type"]));
            //    lp.Add(new PropertyFinder(HtmlProperty.TagInstance, @this.HtmlEdit["SearchBox.TagInstance"]));
            //    lp.Add(new PropertyFinder(HtmlProperty.TagName, @this.HtmlEdit["SearchBox.TagName"]));
            //    HtmlEdit editbox = @this.Anh.EditFinder(lp);
            //    @this.Anh.Page.SetFocus();
            //    while (editbox.Text == "")
            //    {
            //        editbox.SetFocus();
            //        Keyboard.SendKeys(editbox, @this.TestData["AvailableSubmission"]);
            //    }
            //    if (tb.RowCount > 1 && tb.GetCellByIndex(1, 6).Exists)
            //    {
            //        Mouse.Click(tb.GetCellByIndex(1, 6).GetChildren()[0]);
            //        @this.Anh.WaitForControlReady(20 * 1000);
            //        HtmlEdit sic = @this.Anh.EditFinder(@this.HtmlEdit["AvailableSubmission.SIC.Id"]);
            //        sic.SetFocus();
            //        Keyboard.SendKeys(sic, "A",System.Windows.Input.ModifierKeys.Control);
            //        Thread.Sleep(50);
            //        Keyboard.SendKeys(sic, @this.TestData["SIC"]);
            //        Thread.Sleep(50);
            //        Keyboard.SendKeys(sic, "{ENTER}");
            //        while (@this.Anh.PopupWindow.Exists)
            //        {
            //            @this.Anh.PopupWindow.SetFocus();
            //            Mouse.Click(@this.Anh.PopupWindow.WinButtonFinder(@this.WinButton["OK.Name"]));
            //        }
            //        //Mouse.Click(@this.Anh.InputbuttonFinder(@this.HtmlInputButton["AvailableSubmission.ImportSubmission.Id"]));
            //        @this.Anh.SetFocus();
            //        HtmlInputButton importSubmission = new HtmlInputButton(@this.Anh);
            //        importSubmission.SearchProperties[HtmlControl.PropertyNames.Id] = "importBtn";
            //        Mouse.Click(importSubmission);
            //        //while (@this.Anh.PopupWindow.Exists)
            //        //{
            //        //    @this.Anh.PopupWindow.SetFocus();
            //        //    Mouse.Click(@this.Anh.PopupWindow.WinButtonFinder(@this.WinButton["OK.Name"]));
            //        //}
            //        @this.Anh.WaitForControlReady(20 * 1000);
            //        //string content = @this.Anh.DivFinder(@this.HtmlDiv["SearchPane"]).InnerText;
            //        string content = @this.Anh.DivFinder("anh-portal-content").InnerText;
            //        string checkstr=@this.TestData["CheckString"];
            //        string findstr=@this.TestData["FindString"];
            //        int startPos=content.IndexOf(findstr, StringComparison.CurrentCultureIgnoreCase);
            //        int endPos=content.IndexOf('.',startPos+findstr.Length);
            //        if (content.IndexOf(checkstr, StringComparison.CurrentCultureIgnoreCase) > -1 & startPos > -1 & endPos > 0)
            //        {
            //            submissionNo = content.Substring(startPos + findstr.Length, endPos - startPos - findstr.Length).Trim();
            //            //Mouse.Click(@this.Anh.InputbuttonFinder(@this.HtmlInputButton["AvailableSubmission.Continue.Id"]));
            //            HtmlInputButton continueBtn = new HtmlInputButton(@this.Anh);
            //            continueBtn.SearchProperties[HtmlControl.PropertyNames.Id] = "messageBtn";
            //            Mouse.Click(continueBtn);
            //            return true;
            //        }
            //        else
            //        {
            //            @this.Reporter.LogError("Import submission failed!");
            //            return false;
            //        }
            //    }
            //    else 
            //    {
            //        @this.Reporter.LogError("submission " + @this.TestData["AvailableSubmission"] + " not found!");
            //        return false;
            //    }
            //}
            //else
            //{
            //    @this.Reporter.LogError("No available submission found!");
            //    return false;
            //}
        }

        public static void NewQuote(this TestBase @this, string submissionNo)
        {
            //List<PropertyFinder> listp = new List<PropertyFinder>();
            //listp.Add(new PropertyFinder(HtmlProperty.InnerText, @this.HtmlHyperlink["ExistingSubmissions.InnerText"]));
            //listp.Add(new PropertyFinder(HtmlProperty.TagInstance, @this.HtmlHyperlink["ExistingSubmissions.TagInstance"]));
            //@this.Anh.NavigateToUrl(new Uri(@this.GlobalData["URI"]));
            //Mouse.Click(@this.Anh.HyperlinkFinder(listp));
            //@this.Anh.WaitForControlReady(20 * 1000);
            //List<PropertyFinder> lp = new List<PropertyFinder>();
            //lp.Add(new PropertyFinder(HtmlProperty.Type, @this.HtmlEdit["SearchBox.Type"]));
            //lp.Add(new PropertyFinder(HtmlProperty.TagInstance, @this.HtmlEdit["SearchBox.TagInstance"]));
            //lp.Add(new PropertyFinder(HtmlProperty.TagName, @this.HtmlEdit["SearchBox.TagName"]));
            //HtmlEdit editbox = @this.Anh.EditFinder(lp);
            //@this.Anh.Page.SetFocus();
            //while (editbox.Text == "")
            //{
            //    editbox.SetFocus();
            //    Keyboard.SendKeys(editbox, submissionNo);
            //}
            //Thread.Sleep(1000);
            //HtmlTable tb = @this.Anh.TableFinder(@this.HtmlTable["ResultTabel.Id"]);
            //if (tb.RowCount > 1)
            //{
            //    Mouse.Click(tb.GetCellByIndex(1, 6).GetChildren()[0]);
            //    @this.Anh.WaitForControlReady(20 * 1000);
            //    Mouse.Click(@this.Anh.InputbuttonFinder(@this.HtmlInputButton["Home.NewQuote.Id"]));
            //    @this.Anh.WaitForControlReady(20 * 1000);
            //    Mouse.Click(@this.Anh.InputbuttonFinder(@this.HtmlInputButton["Home.Continue.Id"]));
            //    @this.Anh.WaitForControlReady(20 * 1000);
            //    @this.Anh.EditFinder(@this.HtmlEdit["Home.AggLimit.Id"]).Text = @this.TestData["AggLimit"];
            //    @this.Anh.EditFinder(@this.HtmlEdit["Home.SubAggLimit.Id"]).Text = @this.TestData["SubAggLimit"];
            //    Mouse.Click(@this.Anh.InputbuttonFinder(@this.HtmlInputButton["Home.NewClass.Id"]));
            //    @this.Anh.WaitForControlReady(20 * 1000);
            //    @this.Anh.EditFinder(@this.HtmlEdit["ClassDetails.Salary.Id"]).Text = @this.TestData["Salary"];
            //    @this.Anh.EditFinder(@this.HtmlEdit["ClassDetails.Hourly.Id"]).Text = @this.TestData["Hourly"];
            //    @this.Anh.EditFinder(@this.HtmlEdit["ClassDetails.AnnualTravelers.Id"]).Text = @this.TestData["AnnualTravelers"];
            //    @this.Anh.EditFinder(@this.HtmlEdit["ClassDetails.AdditionalTravelDays.Id"]).Text = @this.TestData["AdditionalTravelDays"];
            //    @this.Anh.EditFinder(@this.HtmlEdit["ClassDetails.FlatAmount.Id"]).Text = @this.TestData["FlatAmount"];
            //    @this.Anh.CheckBoxFinder(@this.HtmlCheckBox["COC.BombScare.Id"]).Checked = Convert.ToBoolean(@this.TestData["CheckedBombScare"]);
            //    Mouse.Click(@this.Anh.InputbuttonFinder(@this.HtmlInputButton["ClassDetails.Continue.Id"]));
            //    if (@this.Anh.PopupWindow.Exists)
            //    {
            //        Mouse.Click(@this.Anh.PopupWindow.WinButtonFinder(@this.WinButton["OK.Name"]));
            //        @this.Anh.WaitForControlReady(20 * 1000);
            //    }
            //    @this.Anh.CheckBoxFinder(@this.HtmlCheckBox["Optional.AccidentMedical.Id"]).Checked = Convert.ToBoolean(@this.TestData["CheckedAccidentMedical"]);
            //    Mouse.Click(@this.Anh.InputbuttonFinder(@this.HtmlInputButton["SelectAdditionalClassBenefits.Continue.Id"]));
            //    @this.Anh.WaitForControlReady(20 * 1000);
            //    Mouse.Click(@this.Anh.InputbuttonFinder(@this.HtmlInputButton["ClassAdditionalBenefitetails.Continue.Id"]));
            //    @this.Anh.WaitForControlReady(20 * 1000);
            //}
            //else
            //    @this.Reporter.LogError("not found submission no: "+submissionNo);
        }

        public static bool VerifyQuote(this TestBase @this, string inputSIC, int inputPolicyAggregate, int inputPolicySubAggregate)
        {
            return true;
            //Mouse.Click(@this.Anh.InputbuttonFinder(@this.HtmlInputButton["QuoteSummary.Rate.Id"]));
            //@this.Anh.WaitForControlReady(20 * 1000);
            //if (@this.Anh.InputbuttonFinder(@this.HtmlInputButton["QuoteSummary.SaveQuote.Id"]).Enabled)
            //{
            //    Mouse.Click(@this.Anh.InputbuttonFinder(@this.HtmlInputButton["QuoteSummary.SaveQuote.Id"]));
            //    @this.Anh.WaitForControlReady(1000*10);
            //    Mouse.Click(@this.Anh.InputbuttonFinder(@this.HtmlInputButton["QuoteSaved.OK.Id"]));
            //    @this.Anh.WaitForControlReady(1000 * 10);
            //}
            //else
            //    return false;
            //List<PropertyFinder> listp = new List<PropertyFinder>();
            //listp.Add(new PropertyFinder(HtmlProperty.InnerText, @this.HtmlHyperlink["RatingWorksheet.InnerText"]));
            //listp.Add(new PropertyFinder(HtmlHyperlink.PropertyNames.AbsolutePath, @this.HtmlHyperlink["RatingWorksheet.AbsolutePath"]));
            //listp.Add(new PropertyFinder(HtmlProperty.TagInstance, @this.HtmlHyperlink["RatingWorksheet.TagInstance"]));
            //if (@this.Anh.HyperlinkFinder(listp).Exists)
            //{
            //    @this.Reporter.LogInfo("The Rating Worksheet Hyperlink is existing.");
            //    Mouse.Click(@this.Anh.HyperlinkFinder(listp));
            //    @this.Anh.WindowRatingWorksheet.WaitForControlReady(20 * 1000);
            //    List<PropertyFinder> list = new List<PropertyFinder>();
            //    list.Add(new PropertyFinder(HtmlProperty.TagInstance,"2"));
            //    HtmlTable t2 = @this.Anh.WindowRatingWorksheet.TableFinder(list);
            //    string actualSIC=t2.GetCellByIndex(0,1).InnerText;
            //    string strPolicyAggregate = t2.GetCellByIndex(0, 3).InnerText;
            //    string strPolicySubAggregate = t2.GetCellByIndex(0, 5).InnerText;
            //    int actualPolicyAggregate=Convert.ToInt32(strPolicyAggregate.Replace("$","").Replace(",",""));
            //    int actualPolicySubAggregate = Convert.ToInt32(strPolicySubAggregate.Replace("$", "").Replace(",", ""));
            //    try
            //    {
            //        @this.VerifyMatch(inputSIC, actualSIC);
            //        @this.VerifyEqual(inputPolicyAggregate, actualPolicyAggregate);
            //        @this.VerifyEqual(inputPolicySubAggregate, actualPolicySubAggregate);
            //        return true;
            //    }
            //    catch {
            //        return false;
            //    }
            //}
            //else
            //{
            //    @this.Reporter.LogError("The Rating Worksheet Hyperlink doen't exist.");
            //    return false;
            //}
        }

    }
}
