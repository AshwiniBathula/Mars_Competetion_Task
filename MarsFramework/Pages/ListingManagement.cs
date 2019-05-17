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

        [FindsBy(How = How.XPath, Using = "//button[@class='ui icon positive right labeled button']")]
        private IWebElement ModalYesButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@class='ui negative button']")]
        private IWebElement ModalNoButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//h3[contains(text(),'You do not have any service listings!')]")]
        private IWebElement NoListing { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[@role='button' and not(contains(text(),'>')) and not(contains(text(),'<'))]")]
        private IList<IWebElement> NoOfPages { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[ contains(text(),'>')]")]
        private IWebElement NextPage { get; set; }


        public void ViewServiceDetails()
        {
            Global.GlobalDefinitions.ExcelLib.PopulateInCollection(Global.Base.ExcelPath, "ListingManagement");
            ManageListing.Click();
            Base.test.Log(LogStatus.Info, "Starting editing Service");
            Global.GlobalDefinitions.wait(5000);
            bool RecordFound = false;

            try
            {
                for (int p = 0; p < NoOfPages.Count; p++)
                {
                    for (int i = 0; i < Title.Count; i++)
                    {
                        if ((Global.GlobalDefinitions.ExcelLib.ReadData(2, "Title") == Title[i].Text) && (Global.GlobalDefinitions.ExcelLib.ReadData(2, "Category") == Category[i].Text))
                        {
                            RecordFound = true;
                            view[i].Click();
                            Global.GlobalDefinitions.wait(2000);
                            ManageListing.Click();
                        }
                    }
                    if (NextPage.Enabled)
                        NextPage.Click();
                    else if (RecordFound == false)
                    {
                        Global.Base.test.Log(LogStatus.Info, "There are no matching listings!");
                    }
                }
            }
            catch (Exception e)
            {
                IWebElement NoListings = Global.GlobalDefinitions.driver.FindElement(By.XPath("//h3[contains(text(),'You do not have any service listings!')]"));
                if (NoListings.Displayed)
                {
                    Global.Base.test.Log(LogStatus.Info, "You do not have any service listings to display");
                }
                else
                    Global.Base.test.Log(LogStatus.Info, "Something went wrong!" + e);
            }
           
        }

        public void EditServiceDetails()
        {
            Global.GlobalDefinitions.ExcelLib.PopulateInCollection(Global.Base.ExcelPath, "ListingManagement");
            ManageListing.Click();
            Base.test.Log(LogStatus.Info, "Starting editing Service");
            Global.GlobalDefinitions.wait(5000);
            bool RecordFound = false;
            try
            {
                for (int p = 0; p < NoOfPages.Count; p++)
                { 
                    for (int i = 0; i < Title.Count; i++)
                    {
                        if ((Global.GlobalDefinitions.ExcelLib.ReadData(2, "Title") == Title[i].Text) && (Global.GlobalDefinitions.ExcelLib.ReadData(2, "Category") == Category[i].Text))
                        {
                            RecordFound = true;
                            Edit[i].Click();
                            ServiceListing service = new ServiceListing();
                            service.AddService();
                            Global.GlobalDefinitions.wait(2000);
                            i = i + 1;
                        }   
                    }
                    if (NextPage.Enabled)
                        NextPage.Click();
                    else if(RecordFound == false)
                    {
                        Global.Base.test.Log(LogStatus.Info, "There are no matching listings!");
                    }
                }
            }
            catch (Exception e)
            {
                IWebElement NoListings = Global.GlobalDefinitions.driver.FindElement(By.XPath("//h3[contains(text(),'You do not have any service listings!')]"));
                if (NoListings.Displayed)
                {
                    Global.Base.test.Log(LogStatus.Info, "You do not have any service listings to display");
                }
                else
                    Global.Base.test.Log(LogStatus.Info, "Something went wrong!" + e);
            }
        }

        public void RemoveServiceDetails()
        {
            Global.GlobalDefinitions.ExcelLib.PopulateInCollection(Global.Base.ExcelPath, "ListingManagement");
            Global.GlobalDefinitions.wait(2000);
            ManageListing.Click();
            Base.test.Log(LogStatus.Info, "Starting removing Service");
            Global.GlobalDefinitions.wait(5000);
            bool RecordFound = false;
            int flag = 0;

            //try
            //{
            //    for (int p = 0; p < NoOfPages.Count; p++)
            //    {
            //        for (int i = 0; i < Title.Count; i++)
            //        {
            //            Console.WriteLine("i value:" + i);
            //            if ((Global.GlobalDefinitions.ExcelLib.ReadData(2, "Title") == Title[i].Text) && (Global.GlobalDefinitions.ExcelLib.ReadData(2, "Category") == Category[i].Text))
            //            {
            //                RecordFound = true;
            //                WebDriverWait wait = new WebDriverWait(Global.GlobalDefinitions.driver, TimeSpan.FromSeconds(10));
            //                wait.Until(ExpectedConditions.ElementToBeClickable((Remove[i])));
            //                Remove[i].Click();
            //                Console.WriteLine("removing row:"+i);
            //                Global.GlobalDefinitions.wait(2000);
            //                ModalYesButton.Click();
            //                i = i - 1;
            //            }
            //        }
            //        if (NextPage.Enabled)
            //            NextPage.Click();
            //        else if (RecordFound == false)
            //        {
            //            Global.Base.test.Log(LogStatus.Info, "There are no matching listings!");
            //        }
            //    }
            //}
            try
            {
                for (int p = 0; p < NoOfPages.Count; p++)
                {
                    for (int i = 0; i < Title.Count;i++)
                    {
                        Global.GlobalDefinitions.wait(7000);
                        if (flag == Title.Count)
                            break;
                        else
                        if ((Global.GlobalDefinitions.ExcelLib.ReadData(2, "Title") == Title[i].Text) && (Global.GlobalDefinitions.ExcelLib.ReadData(2, "Category") == Category[i].Text))
                        {
                            RecordFound = true;
                            WebDriverWait wait = new WebDriverWait(Global.GlobalDefinitions.driver, TimeSpan.FromSeconds(10));
                            wait.Until(ExpectedConditions.ElementToBeClickable((Remove[i])));
                            Remove[i].Click();
                            ModalYesButton.Click();
                            Global.GlobalDefinitions.wait(2000);
                            i = i-1;
                            flag++;
                        }
                    }
                    if (NextPage.Enabled)
                        NextPage.Click();
                    else if (RecordFound == false)
                    {
                        Global.Base.test.Log(LogStatus.Info, "There are no matching listings!");
                    }
                }
            }
            catch (Exception e)
            {
                IWebElement NoListings = Global.GlobalDefinitions.driver.FindElement(By.XPath("//h3[contains(text(),'You do not have any service listings!')]"));
                if (NoListing.Displayed)
                {
                    Global.Base.test.Log(LogStatus.Info, "There are no services to remove");
                }
                else
                    Global.Base.test.Log(LogStatus.Info, "Something went wrong!" + e);
            }
        }

        public void RemoveAllServices()
        {
            Global.GlobalDefinitions.ExcelLib.PopulateInCollection(Global.Base.ExcelPath, "ListingManagement");
            Global.GlobalDefinitions.wait(2000);
            ManageListing.Click();
            Base.test.Log(LogStatus.Info, "Starting removing Service");
            Global.GlobalDefinitions.wait(5000);
            
            try
            {
                foreach (IWebElement ele in Remove)
                { 
                    WebDriverWait wait = new WebDriverWait(Global.GlobalDefinitions.driver, TimeSpan.FromSeconds(10));
                    wait.Until(ExpectedConditions.ElementToBeClickable((ele)));
                    ele.Click();
                    ModalYesButton.Click();
                    Global.GlobalDefinitions.wait(5000);
                }
            }
            catch (Exception e)
            {
                IWebElement NoListings = Global.GlobalDefinitions.driver.FindElement(By.XPath("//h3[contains(text(),'You do not have any service listings!')]"));
                if (NoListing.Displayed)
                {
                    Global.Base.test.Log(LogStatus.Info, "There are no services to remove");
                }
                else
                    Global.Base.test.Log(LogStatus.Info, "Something went wrong!" + e);
            }
           
        }
    }
}
