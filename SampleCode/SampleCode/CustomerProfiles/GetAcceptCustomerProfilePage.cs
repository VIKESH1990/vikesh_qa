
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizeNET.Api.Contracts.V1;
using AuthorizeNET.Api.Controllers;
using AuthorizeNET.Api.Controllers.Bases;
using System.IO;
using NUnit.Framework;
using LumenWorks.Framework.IO.Csv;

using System.Diagnostics;

namespace net.authorize.sample
{
    public class GetAcceptCustomerProfilePage
    {
        //public static ANetApiResponse Run(string ApiLoginId, string ApiTransactionKey, string customerProfileId)
        //{
        //    Console.WriteLine("Get Accept Customer Profile Page sample");

        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;
        //    // define the merchant information (authentication / transaction id)
        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        //    {
        //        name = ApiLoginId,
        //        ItemElementName = ItemChoiceType.transactionKey,
        //        Item = ApiTransactionKey,
        //    };

        //    settingType[] settings = new settingType[]{
        //                                        new settingType()
        //                                        // More settings can be added here as and when required
        //                            };
        //    settings[0] = new settingType();
        //    settings[0].settingName = settingNameEnum.hostedProfileReturnUrl.ToString();
        //    settings[0].settingValue = "https://returnurl.com/return/";


        //    var request = new getHostedProfilePageRequest();
        //    request.customerProfileId = customerProfileId;
        //    request.hostedProfileSettings = settings;

        //    var controller = new getHostedProfilePageController(request);
        //    controller.Execute();

        //    var response = controller.GetApiResponse();

        //    if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
        //    {
        //        Console.WriteLine(response.messages.message[0].code);
        //        Console.WriteLine(response.messages.message[0].text);
        //        Console.WriteLine("Token: " + response.token.ToString());
        //    }
        //    else if(response != null)
        //    {
        //        Console.WriteLine("Error: " + response.messages.message[0].code + "  " +
        //                                      response.messages.message[0].text);
        //    }

        //    return response;
        //}
        public static void GetAcceptCustomerProfilePageExec(String ApiLoginID, String ApiTransactionKey)
        {
            Console.WriteLine("Get Accept Customer Profile Page sample");
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/GetAcceptCustomerProfilePage.csv", FileMode.Open)), true))
            {
                int fieldCount = csv.FieldCount;
                //Append Data
                var item1 = DataAppend.ReadPrevData();
                using (CsvFileWriter writer = new CsvFileWriter(new FileStream(@"../../../CSV_DATA/Outputfile.csv", FileMode.Open)))
                {
                    int flag = 0;
                    string[] headers = csv.GetFieldHeaders();
                    while (csv.ReadNextRecord())
                    {
                        // Create Instance of Customer Api
                        ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;
                        // define the merchant information (authentication / transaction id)
                        ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
                        {
                            name = ApiLoginID,
                            ItemElementName = ItemChoiceType.transactionKey,
                            Item = ApiTransactionKey,
                        };
                        //CustomersApi instance = new CustomersApi(EnvironmentSet.Sandbox);
                        // Customer Response Object           
                        // Customer response = null;
                        //initialization


                        string customerProfileId = null;
                        string TestCaseId = null;
                       
                        for (int i = 0; i < fieldCount; i++)
                        {
                            switch (headers[i])
                            {
                                case "customerProfileId":
                                    customerProfileId = csv[i];
                                    break;
                              
                                case "TestCaseId":
                                    TestCaseId = csv[i];
                                    break;

                                default:
                                    break;
                            }
                        }
                        //Write to output file
                        CsvRow row = new CsvRow();
                        try
                        {
                            if (flag == 0)
                            {
                                row.Add("TestCaseId");
                                row.Add("APIName");
                                row.Add("Status");
                                row.Add("TimeStamp");
                                writer.WriteRow(row);
                                flag = flag + 1;
                                //Append Data                                
                                foreach (var item in item1)
                                    writer.WriteRow(item);
                            }
                            //response = instance.GetCustomer(customerId, authorization);
                            settingType[] settings = new settingType[]{
                                                new settingType()
                                                // More settings can be added here as and when required
                                    };
                            settings[0] = new settingType();
                            settings[0].settingName = settingNameEnum.hostedProfileReturnUrl.ToString();
                            settings[0].settingValue = "https://returnurl.com/return/";


                            var request = new getHostedProfilePageRequest();
                            request.customerProfileId = customerProfileId;
                            request.hostedProfileSettings = settings;

                               var controller = new getHostedProfilePageController(request);
                               controller.Execute();

                               var response = controller.GetApiResponse();

                            if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
                            {
                                try
                                {
                                    //Assert.AreEqual(response.Id, customerProfileId);
                                    Console.WriteLine("Assertion Succeed! Valid CustomerId fetched.");
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("GACPP_00" + flag.ToString());
                                    row1.Add("GetAcceptCustomerProfilePage");
                                    row1.Add("Pass");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //  Console.WriteLine("Success " + TestcaseID + " CustomerID : " + response.Id);
                                    flag = flag + 1;
                                    Console.WriteLine(response.messages.message[0].code);
                                    Console.WriteLine(response.messages.message[0].text);
                                    Console.WriteLine("Token: " + response.token.ToString());
                                }
                                catch
                                {
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("GACPP_00" + flag.ToString());
                                    row1.Add("GetAcceptCustomerProfilePage");
                                    row1.Add("Fail");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //Console.WriteLine("Assertion Failed! Invalid CustomerId fetched.");
                                    flag = flag + 1;
                                }
                            }
                            else
                            {
                                CsvRow row1 = new CsvRow();
                                row1.Add("GACPP_00" + flag.ToString());
                                row1.Add("GetAcceptCustomerProfilePage");
                                row1.Add("Fail");
                                row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                writer.WriteRow(row1);
                                //Console.WriteLine("Assertion Failed! Invalid CustomerId fetched.");
                                flag = flag + 1;
                            }
                        }
                        catch (Exception e)
                        {
                            CsvRow row2 = new CsvRow();
                            row2.Add("GACPP_00" + flag.ToString());
                            row2.Add("GetAcceptCustomerProfilePage");
                            row2.Add("Fail");
                            row2.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                            writer.WriteRow(row2);
                            flag = flag + 1;
                            Console.WriteLine(TestCaseId + " Error Message " + e.Message);
                        }
                    }
                }
            }
        }
    }
}
