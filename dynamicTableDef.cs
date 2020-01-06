using AventStack.ExtentReports;

using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Cucumbercsharp.DynamicData
{
    [Binding]
    public sealed class dynamicTableDef:Steps
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext context;
        
        protected new ExtentTest _test;
        XmlDocument xml = null;
        private const string XmlFileName = "data.xml";

        public dynamicTableDef(ScenarioContext injectedContext)
        {
            context = injectedContext;
            xml = new XmlDocument();
            xml.LoadXml(File.ReadAllText(XmlFileName));
        }
         ~ dynamicTableDef()
        {
            xml = null;
        }
        [Given(@"I have entered below numbers and verify all the numbers")]
        public void GivenIHaveEnteredBelowNumbersAndVerifyAllTheNumbers(Table table)
        {
            #region Extent Report




            ExtentHtmlReporter htmlReporter = new ExtentHtmlReporter("c:\\db\\test.html");

            ExtentReports _extent = new ExtentReports();
            _extent.AttachReporter(htmlReporter);


            //feature
            var features= _extent.CreateTest<Feature>("Login Features with various number ");
            //scenario
           var scenario= features.CreateNode<Scenario>("Create  A scenario for Pass or fail test");
            //steps

            scenario.CreateNode<Given>("I have entered below numbers and verify all the numbers");
       _extent.Flush();
            #endregion  


            var actualValues =table.CreateDynamicSet();
            foreach(var values in actualValues)
            {
                int result = Convert.ToInt32(values.num1) + Convert.ToInt32(values.num2);
                Assert.AreEqual(result.ToString(new CultureInfo("en-us")), values.result.ToString());
            }
        }
        [Given(@"i verifyed scenario example")]
        public void GivenIVerifyedScenarioExample()
        {
            context["valueStore"] = "Step Passed";
            Console.WriteLine(context["valueStore"].ToString());
        }


        [Given(@"i have login the application below credentail")]
        public void GivenIHaveLoginTheApplicationBelowCredentail(Table table)
        {
            dynamic singleData = table.CreateDynamicSet();// if you have multiple records use dynamicSet
            foreach(dynamic d in singleData)
                Console.WriteLine(d.username + "  "+ d.password);
            

        }

        [Given(@"i enterd below details in the form")]
        public void GivenIEnterdBelowDetailsInTheForm(Table table)
        {
            dynamic singleData = table.CreateDynamicInstance();//for single records
            
                Console.WriteLine("Mr."+singleData.fname + "  " + singleData.lname +"getting salary.."+ singleData.salary);
            
        }

        [Given(@"i logged in and entered user details")]
        public void GivenILoggedInAndEnteredUserDetails()
        {
            string[] colheader = { "username", "password" };
            string[] rows = {"HelloBoy","hellboy" };
            string[] rowss = { "dellboy", "cellboy" };
            var table = new Table(colheader);
            table.AddRow(rows);
            table.AddRow(rowss);
            Given("i have login the application below credentail", table);
           string[] colheaderUserdatacolumn ={"fname","lname","salary" };
            string[] datarows = {"Bean","Dean","250000" };
            var tables = new Table(colheaderUserdatacolumn);
            tables.AddRow(datarows);
            Given("i enterd below details in the form", tables);
           

        }

        [Given(@"i  update xml file by xpath")]
        public void GivenIUpdateXmlFileByXpath()
        {
           


            string xPath = "/bookstore/book/year";
            string Value = "dindong";
            ModifiedXML(xPath, Value);
            ModifyAttribute("/bookstore/book/title", "Hindi");

            Assert.IsTrue(VerifyinJson(ReadJson(), "['channel']['firstName']", Value));
        }
        private string ModifiedXML(string xpath,string ModifiedValue)
        {           
            XmlElement xelement = (XmlElement)xml.SelectSingleNode(xpath);
            if (xelement != null)
            {
                xelement.InnerText = ModifiedValue;
               return xml.InnerXml==null?null:xml.InnerXml;
            }
            return null;
        }
        private string ModifyAttribute(string xpath, string ModifiedValue)
        {
            string newValue = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(XmlFileName);

            XmlNode node = xmlDoc.SelectSingleNode(xpath);
            node.Attributes[0].Value = ModifiedValue;
            return xmlDoc.InnerXml;
           

        }

        private JObject ReadJson()
        {
            JObject o1 = JObject.Parse(File.ReadAllText(@"sample.json"));
           if(o1!=null)
            {
                return o1;
            }
            return null;
        }
        private static bool  VerifyinJson(JObject jObject,string jvalue,string verifyValue)
        {
            if(IsValidDate(verifyValue))
            {
                string distance = jObject.SelectToken(jvalue).ToString();
                return  CompareTwoDate(distance, verifyValue);
            }
            else
            {
                string distance = jObject.SelectToken(jvalue).ToString();
                return distance.ToLower(new CultureInfo("en-US", false)) == verifyValue.ToLower(new CultureInfo("en-US", false)) ? true : false;

            }           
        }

        private static bool CompareTwoDate(string Date1,string Date2)
        {           
            DateTime dt1;
            DateTime dt2;
            if (DateTime.TryParse(Date1, out dt1) && DateTime.TryParse(Date2, out dt2))
            {
                if (dt1.Date == dt2.Date)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        private static bool IsValidDate(string DateString)
        {
            if (DateTime.TryParse(DateString, out DateTime Temp) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
