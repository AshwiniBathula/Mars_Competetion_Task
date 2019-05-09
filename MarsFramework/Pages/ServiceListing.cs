using AutoItX3Lib;
using MarsFramework.Global;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MarsFramework.Pages
{
    class ServiceListing
    {
        public ServiceListing()
        {
            PageFactory.InitElements(Global.GlobalDefinitions.driver,this);
        }

        //ShareSkillBtn
        [FindsBy(How=How.XPath, Using= "//a[@class='ui basic green button']")]
        private IWebElement ShareSkillBtn { get; set; }

        //Title
        [FindsBy(How = How.XPath, Using = "//input[contains(@type,'text') and @name='title']")]
        private IWebElement Title { get; set; }

        //Description
        [FindsBy(How = How.XPath, Using = "//textarea[@name='description']")]
        private IWebElement Description { get; set; }

        //category
        [FindsBy(How = How.XPath, Using = "//select[@name='categoryId']")]
        private IWebElement Category { get; set; }

        //CategoryOptions
        [FindsBy(How = How.XPath, Using = "//select[@name='categoryId']/option[@value]")]
        private IList<IWebElement> CategoryOptions { get; set; }

        //subcategory
        [FindsBy(How = How.XPath, Using = "//select[@name='subcategoryId']")]
        private IWebElement SubCategory { get; set; }

        //SubCategoryOptions
        [FindsBy(How = How.XPath, Using = "//select[@name='subcategoryId']/option[@value]")]
        private IList<IWebElement> SubCategoryOptions { get; set; }

        //Tags
        [FindsBy(How = How.XPath, Using = "//h3[contains(text(),'Tags')]//parent::div[@class='four wide column']//following-sibling::div//descendant::input")]
        private IWebElement Tags { get; set; }

        //HourlyBasisServiceRadio button
        [FindsBy(How = How.XPath, Using = "//input[@name='serviceType' and @value='0']")]
        private IWebElement HourlybasisServiceRadioBtn { get; set; }

        //OneoffService
        [FindsBy(How = How.XPath, Using = "//input[@name='serviceType' and @value='1']")]
        private IWebElement OneOffServiceRadioBtn { get; set; }

        //Service Types-labels
        [FindsBy(How = How.XPath, Using = "//input[@name='serviceType']//following-sibling::label")]
        private IList<IWebElement> ServiceTypes { get; set; }

        //Location Type-labels
        [FindsBy(How = How.XPath, Using = "//input[@name='locationType']//following-sibling::label")]
        private IList<IWebElement> LocationType { get; set; }

        //onsite
        [FindsBy(How = How.XPath, Using = "//input[@name='locationType' and @value='0']")]
        private IWebElement OnSiteRadioBtn { get; set; }

        //online
        [FindsBy(How = How.XPath, Using = "//input[@name='locationType' and @value='1']")]
        private IWebElement OnlineRadioBtn { get; set; }

        //startDate
        [FindsBy(How = How.XPath, Using = "//input[@name='startDate']")]
        private IWebElement StartDate { get; set; }

        //endDate
        [FindsBy(How = How.XPath, Using = "//input[@name='endDate']")]
        private IWebElement EndDate { get; set; }
        
        //Start time
        [FindsBy(How = How.XPath, Using = "//div//child::input[@name='StartTime']")]
        private IList<IWebElement> StartTime { get; set; }

        //End time
        [FindsBy(How = How.XPath, Using = "//div//child::input[@name='EndTime']")]
        private IList<IWebElement> EndTime { get; set; }

        //Days Checkboxes
        [FindsBy(How = How.XPath, Using = "//div//child::input[@name='Available']")]
        private IList<IWebElement> DaysCheckbox { get; set; }

        //Days-labels 
        [FindsBy(How = How.XPath, Using = "//label//parent::div[@class='ui checkbox']")]
        private IList<IWebElement> DaysLabel { get; set; }

        //SkillTrade-labels
        [FindsBy(How = How.XPath, Using = "//input[@name='skillTrades']//following-sibling::label")]
        private IList<IWebElement> SkillTrade { get; set; }

        //Skill trade-skillexchange radio 
        [FindsBy(How = How.XPath, Using = "//input[@name='skillTrades' and @value='true']")]
        private IWebElement SkillExchangeRadioBtn { get; set; }

        //Skill trade-Credit radio 
        [FindsBy(How = How.XPath, Using = "//input[@name='skillTrades' and @value='false']")]
        private IWebElement CreditRadioBtn { get; set; }

        //skillexchange-tags
        [FindsBy(How = How.XPath, Using = "//h3[contains(text(),'Skill-Exchange')]//parent::div[@class='four wide column']//following-sibling::div//descendant::input")]
        private IWebElement SkillExchange { get; set; }

        //Credit  
        [FindsBy(How = How.XPath, Using = "//input[@name='charge' and @placeholder='Amount']")]
        private IWebElement Credit { get; set; }

        //work samples  
        [FindsBy(How = How.XPath, Using = "//div[@class ='ui grid']/span/i")]
        private IWebElement WorkSamples { get; set; }

        //Active Status
        [FindsBy(How = How.XPath, Using = "//input[@name='isActive']//following-sibling::label")]
        private IList<IWebElement> ActiveStatus { get; set; }

        //Active  
        [FindsBy(How = How.XPath, Using = "//input[@name='isActive' and @value='true']")]
        private IWebElement ActiveradioBtn { get; set; }

        //Hidden  
        [FindsBy(How = How.XPath, Using = "//input[@name='isActive' and @value='false']")]
        private IWebElement HiddenRadioBtn { get; set; }

        //save
        [FindsBy(How = How.XPath, Using = "//input[@type='button' and @value='Save']")]
        private IWebElement Save { get; set; }

        public void AddService() {

            Global.GlobalDefinitions.ExcelLib.PopulateInCollection(Base.ExcelPath,"ServiceListing");

            //click Shareskill button
            ShareSkillBtn.Click();
            GlobalDefinitions.wait(2000);

            Base.test.Log(LogStatus.Info, "Starting adding Service");

            //enter Title
            Title.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Title"));

            //enter Description
            Description.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2,"Description"));

            //select Category
            if (Category.Displayed)
                Category.Click();
            else
                GlobalDefinitions.wait(2000);

            for (int i = 0; i < CategoryOptions.Count; i++)
            {
                if (GlobalDefinitions.ExcelLib.ReadData(2, "Category") == CategoryOptions[i].Text)
                {
                    CategoryOptions[i].Click();
                    Base.test.Log(LogStatus.Info, "Selected Category successfully");
                }
            }

            GlobalDefinitions.wait(2000);

            //select SubCategory
            SubCategory.Click();
            for (int i = 0; i < SubCategoryOptions.Count; i++)
            {
                if (GlobalDefinitions.ExcelLib.ReadData(2, "Sub Category") == SubCategoryOptions[i].Text)
                {
                    SubCategoryOptions[i].Click();
                    Base.test.Log(LogStatus.Info, "Selected Sub Category successfully");
                }
            }

            //Enter Tags
            Tags.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2,"Tags"));
            Tags.SendKeys(Keys.Enter);

            //Click Service Type

            if (GlobalDefinitions.ExcelLib.ReadData(2, "Service Type") == "One-off service")
            {
                OneOffServiceRadioBtn.Click();
                Base.test.Log(LogStatus.Info, "Selected Service Type successfully");
            }

             //Click Location Type
            if (GlobalDefinitions.ExcelLib.ReadData(2, "Location Type") == "On-site")
            {
                OnSiteRadioBtn.Click();
                Base.test.Log(LogStatus.Info, "Selected Location Type successfully");
            }
            
            //enter startDate
            StartDate.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2,"Start Date"));
            Global.GlobalDefinitions.wait(1000);

            //enter EndDate
            if (!(string.IsNullOrEmpty(GlobalDefinitions.ExcelLib.ReadData(2, "End Date"))))
            {
                EndDate.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "End Date"));
            }

            //enter start time and end time and 
            for(int i=0;i< DaysLabel.Count; i++)
            {
                switch (DaysLabel[i].Text)
                {
                    case "Mon":
                        if (!(string.IsNullOrEmpty(GlobalDefinitions.ExcelLib.ReadData(2, "Mon Start Time"))))
                        {
                            DaysCheckbox[i].Click();
                            StartTime[i].SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Mon Start Time"));
                        }
                        if (!(string.IsNullOrEmpty(GlobalDefinitions.ExcelLib.ReadData(2, "Mon End Time"))))
                            EndTime[i].SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Mon End Time"));
                        break;
                    case "Tue":
                        if (!(string.IsNullOrEmpty(GlobalDefinitions.ExcelLib.ReadData(2, "Tue Start Time"))))
                        {
                            DaysCheckbox[i].Click();
                            Console.WriteLine("Inside tue if"+ i + DaysLabel[i].Text);
                            StartTime[i].SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Tue Start Time"));
                        }
                        if (!(string.IsNullOrEmpty(GlobalDefinitions.ExcelLib.ReadData(2, "Tue End Time"))))
                            EndTime[i].SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Tue End Time"));
                        break;
                    case "Wed":
                        //string.IsNullOrEmpty(var)
                        if (!(string.IsNullOrEmpty(GlobalDefinitions.ExcelLib.ReadData(2, "Wed Start Time"))))
                        {
                            DaysCheckbox[i].Click();
                            StartTime[i].SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Wed Start Time"));
                        }
                        if (!(string.IsNullOrEmpty(GlobalDefinitions.ExcelLib.ReadData(2, "Wed End Time"))))
                            EndTime[i].SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Wed End Time"));
                        break;
                    case "Thu":
                        if (!(string.IsNullOrEmpty(GlobalDefinitions.ExcelLib.ReadData(2, "Thu Start Time"))))
                        {
                            DaysCheckbox[i].Click();
                            StartTime[i].SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Thu Start Time"));
                        }
                        if (!(string.IsNullOrEmpty(GlobalDefinitions.ExcelLib.ReadData(2, "Thu Start Time"))))
                            EndTime[i].SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Thu End Time"));
                        break;
                    case "Fri":
                        if (!(string.IsNullOrEmpty(GlobalDefinitions.ExcelLib.ReadData(2, "Fri Start Time"))))
                        {
                            DaysCheckbox[i].Click();
                            StartTime[i].SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Fri Start Time"));
                        }
                        if (!(string.IsNullOrEmpty(GlobalDefinitions.ExcelLib.ReadData(2, "Fri End Time"))))
                            EndTime[i].SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Fri End Time"));
                        break;
                    case "Sat":
                        if (!(string.IsNullOrEmpty(GlobalDefinitions.ExcelLib.ReadData(2, "Sat Start Time"))))
                        {
                            DaysCheckbox[i].Click();
                            StartTime[i].SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Sat Start Time"));
                        }
                        if (!(string.IsNullOrEmpty(GlobalDefinitions.ExcelLib.ReadData(2, "Sat End Time"))))
                            EndTime[i].SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Sat End Time"));
                        break;
                    case "Sun":
                        if (!(string.IsNullOrEmpty(GlobalDefinitions.ExcelLib.ReadData(2, "sun Start Time"))))
                        {
                            DaysCheckbox[i].Click();
                            StartTime[i].SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Sun Start Time"));
                        }
                        if (!(string.IsNullOrEmpty(GlobalDefinitions.ExcelLib.ReadData(2, "Sun End Time"))))
                            EndTime[i].SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Sun End Time"));
                        break;
                    default:break;
                        
                }
            }
            

            //Click SkillTrade

            if (GlobalDefinitions.ExcelLib.ReadData(2, "Skill Trade") == "Skill-exchange")
            {
                if (SkillExchangeRadioBtn.Selected == true)
                {
                    Console.WriteLine("we are in if SkillExchangeRadioBtn.Selected == true");
                    SkillExchange.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Skill Exchange"));
                    SkillExchange.SendKeys(Keys.Enter);
                    Base.test.Log(LogStatus.Info, "Added skill Exchange successfully");
                }
            }
            else if (GlobalDefinitions.ExcelLib.ReadData(2, "Skill Trade") == "Credit")
            {
                CreditRadioBtn.Click();
                Credit.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2, "Credit"));
                Base.test.Log(LogStatus.Info, "Added credit successfully");
            }

            //Approach 1: upload worksamples- if input field(text box) is present
            //WorkSamples.SendKeys(GlobalDefinitions.ExcelLib.ReadData(2,"Work Samples"));
            //GlobalDefinitions.wait(3000);

            //Approach 2: Using AutoIT
            
            WorkSamples.Click();
            AutoItX3 autoIT = new AutoItX3();
            autoIT.WinActivate("Open");
            Thread.Sleep(1000);
            //autoIT.Send("C:\\Users\\gredd\\Desktop\\test.txt");
            autoIT.Send(GlobalDefinitions.ExcelLib.ReadData(2, "Work Samples"));
            Thread.Sleep(1000);
            autoIT.Send("{ENTER}");

            //Click Active status
            if (GlobalDefinitions.ExcelLib.ReadData(2, "Active") == "Hidden")
            {
                HiddenRadioBtn.Click();
                Base.test.Log(LogStatus.Info, "Selected status successfully");
            }

            //click save
            Save.Click();
            Base.test.Log(LogStatus.Info, "Saved Service successfully");
        }
    }
}
