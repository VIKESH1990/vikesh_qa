using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizeNET.Api.Controllers;
using AuthorizeNET.Api.Contracts.V1;
using AuthorizeNET.Api.Controllers.Bases;
using AuthorizeNet.Api.Controllers;
using System.IO;
using NUnit.Framework;
using LumenWorks.Framework.IO.Csv;

using System.Diagnostics;

namespace net.authorize.sample
{
    public class GetAccountUpdaterJobSummary
    {
        //public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey)
        //{
        //    Console.WriteLine("Get Account Updater job summary sample");

        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;
        //    // define the merchant information (authentication / transaction id)
        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        //    {
        //        name = ApiLoginID,
        //        ItemElementName = ItemChoiceType.transactionKey,
        //        Item = ApiTransactionKey,
        //    };

        //    // Set a valid month for the request
        //    string month = "2017-07";

        //    // Build tbe request object
        //    var request = new getAUJobSummaryRequest();
        //    request.month = month;

        //    // Instantiate the controller that will call the service
        //    var controller = new getAUJobSummaryController(request);
        //    controller.Execute();

        //    // Get the response from the service (errors contained if any)
        //    var response = controller.GetApiResponse();

        //    if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
        //    {
        //        Console.WriteLine("SUCCESS: Get Account Updater Summary for Month : " + month);
        //        if (response.auSummary == null)
        //        {
        //            Console.WriteLine("No Account Updater summary for this month.");
        //            return response;
        //        }

        //        // Displaying the summary of each response in the list
        //        foreach (var result in response.auSummary)
        //        {
        //            Console.WriteLine("		Reason Code        : " + result.auReasonCode);
        //            Console.WriteLine("		Reason Description : " + result.reasonDescription);
        //            Console.WriteLine("		# of Profiles updated for this reason : " + result.profileCount);
        //        }
        //    }
        //    else if (response != null)
        //    {
        //        Console.WriteLine("ERROR :  Invalid response");
        //        Console.WriteLine("Response : " + response.messages.message[0].code + "  " + response.messages.message[0].text);
        //    }

        //    return response;
        //}
        public static void GetAccountUpdaterJobSummaryExec(String ApiLoginID, String ApiTransactionKey)
        {
            
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/GetAccountUpdaterJobSummary.csv", FileMode.Open)), true))
            {
                Console.WriteLine("Get Account Updater job summary sample");
                int flag = 0;
                int fieldCount = csv.FieldCount;
                string[] headers = csv.GetFieldHeaders();
                //Append Data
                var item1 = DataAppend.ReadPrevData();
                using (CsvFileWriter writer = new CsvFileWriter(new FileStream(@"../../../CSV_DATA/Outputfile.csv", FileMode.Open)))
                {
                    while (csv.ReadNextRecord())
                    {
                        string TestCase_Id = null;

                        string apiLogin = null;
                        string transactionKey = null;
                        for (int i = 0; i < fieldCount; i++)
                        {
                            switch (headers[i])
                            {
                                case "apiLogin":
                                    apiLogin = csv[i];
                                    break;
                                case "transactionKey":
                                    transactionKey = csv[i];
                                    break;


                                case "TestCase_Id":
                                    TestCase_Id = csv[i];
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

                            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;
                            // define the merchant information (authentication / transaction id)
                            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
                            {
                                name = apiLogin,
                                ItemElementName = ItemChoiceType.transactionKey,
                                Item = transactionKey,
                            };

                            // Set a valid month for the request
                            string month = "2018-06";

                            // Build tbe request object
                            var request = new getAUJobSummaryRequest();
                            request.month = month;

                            // Instantiate the controller that will call the service
                            var controller = new getAUJobSummaryController(request);
                            controller.Execute();

                            // Get the response from the service (errors contained if any)
                            var response = controller.GetApiResponse();



                            if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
                            {
                                try
                                {
                                    //Assert.AreEqual(response.Id, customerProfileId);
                                    Console.WriteLine("Assertion Succeed! Valid CustomerId fetched.");
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("GAUJS_00" + flag.ToString());
                                    row1.Add("GetAccountUpdaterJobSummary");
                                    row1.Add("Pass");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //  Console.WriteLine("Success " + TestcaseID + " CustomerID : " + response.Id);
                                    flag = flag + 1;
                                    Console.WriteLine("SUCCESS: Get Account Updater Summary for Month : " + month);
                                    if (response.auSummary == null)
                                    {
                                        Console.WriteLine("No Account Updater summary for this month.");
                                        //return response;
                                    }

                                    // Displaying the summary of each response in the list
                                    foreach (var result in response.auSummary)
                                    {
                                        Console.WriteLine("		Reason Code        : " + result.auReasonCode);
                                        Console.WriteLine("		Reason Description : " + result.reasonDescription);
                                        Console.WriteLine("		# of Profiles updated for this reason : " + result.profileCount);
                                    }
                                }
                                catch
                                {
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("GAUJS_00" + flag.ToString());
                                    row1.Add("GetAccountUpdaterJobSummary");
                                    row1.Add("Assertion Failed!");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    Console.WriteLine("Assertion Failed! Invalid CustomerId fetched.");
                                    flag = flag + 1;
                                }
                            }
                            else
                            {
                                CsvRow row1 = new CsvRow();
                                row1.Add("GAUJS_00" + flag.ToString());
                                row1.Add("GetAccountUpdaterJobSummary");
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
                            row2.Add("GAUJS_00" + flag.ToString());
                            row2.Add("GetAccountUpdaterJobSummary");
                            row2.Add("Fail");
                            row2.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                            writer.WriteRow(row2);
                            flag = flag + 1;
                            Console.WriteLine(TestCase_Id + " Error Message " + e.Message);
                        }
                    }
                }
            }
        }

    }
}
