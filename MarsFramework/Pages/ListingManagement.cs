using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsFramework.Global;
using OpenQA.Selenium.Support.UI;

namespace MarsFramework.Pages
{
    class ListingManagement
    {
        public ListingManagement()
        {
            PageFactory.InitElements(Global.GlobalDefinitions.driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//a[contains(text(),'Manage Listings')]")]
        private IWebElement ManageListing { get; set; }

        [FindsBy(How=How.XPath, Using= "//table//tbody//tr//td[8]//i[1]")]
        private IList<IWebElement> view { get; set; }

        [FindsBy(How = How.XPath, Using = "//table//tbody//tr//td[8]//i[2]")]
        private IList<IWebElement> Edit { get; set; }

        [FindsBy(How = How.XPath, Using = "//table//tbody//tr//td[8]//i[3]")]
        private IList<IWebElement> Remove { get; set; }

        [FindsBy(How = How.XPath, Using = "//table//tbody//tr//td[2]")]
        private IList<IWebElement> Category { get; set; }

        [FindsBy(How = How.XPath, Using = "//table//tbody//tr//td[3][@class='two wide']")]
        private IList<IWebElement> Title { get; set; }

        public void ViewServiceDetails()
        {
            Global.GlobalDefinitions.ExcelLib.PopulateInCollection(Global.Base.ExcelPath,"ListingManagement");
            ManageListing.Click();
            Base.test.Log(LogStatus.Info, "Starting Viewing Service");
            Global.GlobalDefinitions.wait(2000);
            
            for (int i = 0; i < Title.Count; i++)
            {
                ManageListing.Click();
                if ((Global.GlobalDefinitions.ExcelLib.ReadData(2, "Title") == Title[i].Text) && (Global.GlobalDefinitions.ExcelLib.ReadData(2, "Category") == Category[i].Text))
                {
                    view[i].Click();
                    Console.WriteLine(i);
                    Global.GlobalDefinitions.wait(2000);
                }
            }
        }

        public void EditServiceDetails()
        {
            Global.GlobalDefinitions.ExcelLib.PopulateInCollection(Global.Base.ExcelPath, "ListingManagement");
            ManageListing.Click();
            Base.test.Log(LogStatus.Info, "Starting editing Service");
            Global.GlobalDefinitions.wait(2000);

            for (int i = 0; i < Title.Count; i++)
            {
                if ((Global.GlobalDefinitions.ExcelLib.ReadData(2, "Title") == Title[i].Text) && (Global.GlobalDefinitions.ExcelLib.ReadData(2, "Category") == Category[i].Text))
                {
                    Edit[i].Click();
                    ServiceListing service = new ServiceListing();
                    service.AddService();
                    Global.GlobalDefinitions.wait(2000);
                }
            }
        }

        public void RemoveServiceDetails()
        {
            Global.GlobalDefinitions.ExcelLib.PopulateInCollection(Global.Base.ExcelPath, "ListingManagement");
            ManageListing.Click();
            Base.test.Log(LogStatus.Info, "Starting removing Service");
            Global.GlobalDefinitions.wait(2000);
             
            for (int i = 0; i < Title.Count; i++)
            {  
                if ((Global.GlobalDefinitions.ExcelLib.ReadData(2, "Title") == Title[i].Text) && (Global.GlobalDefinitions.ExcelLib.ReadData(2, "Category") == Category[i].Text))
                {
                    Remove[i].Click();
                    WebDriverWait wait = new WebDriverWait(Global.GlobalDefinitions.driver, TimeSpan.FromSeconds(10));
                    wait.Until(ExpectedConditions.AlertIsPresent());
                    IAlert alert = Global.GlobalDefinitions.driver.SwitchTo().Alert() ;                    
                    alert.Accept();
                }
            }
        }
    }
}
