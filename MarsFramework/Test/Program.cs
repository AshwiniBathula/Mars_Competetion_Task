using MarsFramework.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsFramework
{
    public class Program
    {
        [TestFixture]
        [Category("Sprint1")]
        class Tenant : Global.Base
        {

            [Test]
            public void UserAccount()
            {
                // Creates a toggle for the given test, adds all log events under it    
                test = extent.StartTest("Adding profile");

                // Create an object to call the method
                Profile obj = new Profile();
                obj.EditProfile();

            }
            [Test]
            public void Validate_Add_Service()
            {
                test = extent.StartTest("Starting Add Service test");
                ServiceListing service = new ServiceListing();

                service.AddService();
                Global.GlobalDefinitions.wait(5000);
                try
                {
                    string Expected = "Manage Listings";
                    
                    WebDriverWait wait = new WebDriverWait(Global.GlobalDefinitions.driver, TimeSpan.FromSeconds(10));
                    wait.Until(ExpectedConditions.ElementExists((By.XPath("//h2[contains(text(),'Manage Listings')]"))));

                    Assert.AreEqual(Expected, Global.GlobalDefinitions.driver.FindElement(By.XPath("//h2[contains(text(),'Manage Listings')]")).Text);
                    Global.Base.test.Log(LogStatus.Pass, "Service is added successfully");
                }
                catch (Exception e) {
                    Global.Base.test.Log(LogStatus.Fail, "Service is not added"+ e);
                }
            }

            [Test]
            public void Validate_View_Service()
            {
                test = extent.StartTest("Starting View Service test");
                ListingManagement service = new ListingManagement();

                service.ViewServiceDetails();
                Global.GlobalDefinitions.wait(3000);

                try
                {
                    string Expected = "Service Detail";
                    Assert.AreEqual(Expected, Global.GlobalDefinitions.driver.Title);
                    Global.Base.test.Log(LogStatus.Pass, "Service is displayed successfully");
                }
                catch (Exception e)
                {
                    Global.Base.test.Log(LogStatus.Fail, "Service is not displayed" + e);
                }
            }

            [Test]
            public void Validate_Edit_Service()
            {
                test = extent.StartTest("Starting edit Service test");
                ListingManagement service = new ListingManagement();

                service.EditServiceDetails();
                Global.GlobalDefinitions.wait(2000);

                try
                {
                    string Expected = "ListingManagement";
                    Assert.AreEqual(Expected, Global.GlobalDefinitions.driver.Title);
                    Global.Base.test.Log(LogStatus.Pass, "Service is edited successfully");
                }
                catch (Exception e)
                {
                    Global.Base.test.Log(LogStatus.Fail, "Service is not Edited" + e);
                }
            }
            [Test]
            public void Validate_Remove_Service()
            {
                try
                {
                test = extent.StartTest("Starting Remove Service test");
                ListingManagement service = new ListingManagement();
                service.RemoveServiceDetails();
                Global.GlobalDefinitions.wait(2000);
                IWebElement NoListings = Global.GlobalDefinitions.driver.FindElement(By.XPath("//h3[contains(text(),'You do not have any service listings!')]"));
                    if (NoListings.Displayed)
                    {
                        Assert.AreEqual(NoListings.Text, "You do not have any service listings!");
                        Global.Base.test.Log(LogStatus.Pass, "Service is removed successfully");
                    }
                    else
                    {
                        Global.GlobalDefinitions.wait(2000);
                        IList<IWebElement> Title = Global.GlobalDefinitions.driver.FindElements(By.XPath("//table//tbody//tr//td[3]"));
                        IList<IWebElement> Category = Global.GlobalDefinitions.driver.FindElements(By.XPath("//table//tbody//tr//td[2]"));
                        Global.Base.test.Log(LogStatus.Info, "Total languages are:" + Title.Count);
                        bool recordFound = true;

                        for (int i = 0; i < Title.Count; i++)
                        {
                            if ((Global.GlobalDefinitions.ExcelLib.ReadData(2, "Title") == Title[i].Text) && (Global.GlobalDefinitions.ExcelLib.ReadData(2, "Category") == Category[i].Text))
                                recordFound = true;
                            else
                                recordFound = false;
                        }
                        Assert.True(recordFound == false);
                        Global.Base.test.Log(LogStatus.Pass, "Service is removed successfully");
                    }
                }
                catch (Exception e)
                {
                    Global.Base.test.Log(LogStatus.Fail, "Service is not removed"+e);
                }
            }
            [Test]
            public void Validate_Remove_AllServices()
            {
                test = extent.StartTest("Starting Remove Service test");
                ListingManagement service = new ListingManagement();

                try
                {
                    IWebElement NoListings = Global.GlobalDefinitions.driver.FindElement(By.XPath("//h3[contains(text(),'You do not have any service listings!')]"));
                    if (NoListings.Displayed)
                    {
                        Assert.AreEqual(NoListings.Text, "You do not have any service listings!");
                        Global.Base.test.Log(LogStatus.Pass, "Service is removed successfully");
                    }
                }
                catch (Exception e)
                {
                    Global.Base.test.Log(LogStatus.Fail, "Services are not removed" +e);
                }
            }
        }
    }
}
