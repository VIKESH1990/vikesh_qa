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
    public class GetAccountUpdaterJobDetails
    {
        //public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey)
        //{
        //    Console.WriteLine("Get Account Updater job details sample");

        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;
        //    // define the merchant information (authentication / transaction id)
        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        //    {
        //        name = ApiLoginID,
        //        ItemElementName = ItemChoiceType.transactionKey,
        //        Item = ApiTransactionKey,
        //    };

        //    // parameters for request
        //    string month = "2018-05";
        //    //AUJobTypeEnum.all = "all";

        //    var request = new getAUJobDetailsRequest();
        //    request.month = month;
        //    request.modifiedTypeFilter = AUJobTypeEnum.all;
        //    request.paging = new Paging
        //    {
        //        limit = 1000,
        //        offset = 1
        //    };

        //    // instantiate the controller that will call the service
        //    var controller = new getAUJobDetailsController(request);
        //    controller.Execute();

        //    // get the response from the service (errors contained if any)
        //    var response = controller.GetApiResponse();

        //    if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
        //    {
        //        if (response.auDetails == null)
        //            return response;

        //        foreach (var update in response.auDetails)
        //        {
        //            Console.WriteLine("Profile ID / Payment Profile ID: {0} / {1}", update.customerProfileID, update.customerPaymentProfileID);
        //            Console.WriteLine("Update Time (UTC): {0}", update.updateTimeUTC);
        //            Console.WriteLine("Reason Code: {0}", update.auReasonCode);
        //            Console.WriteLine("Reason Description: {0}", update.reasonDescription);

        //            ////Fetching Subscription ID for AU Update
        //            //if (update.subscriptionIds != null && update.subscriptionIds.Length > 0)
        //            //{
        //            //    Console.WriteLine("List of subscriptions : ");
        //            //    for (int i = 0; i < update.subscriptionIds.Length; i++)
        //            //        Console.WriteLine(update.subscriptionIds[i]);
        //            //}
        //        }

        //        foreach (var delete in response.auDetails)
        //        {
        //            Console.WriteLine("Profile ID / Payment Profile ID: {0} / {1}", delete.customerProfileID, delete.customerPaymentProfileID);
        //            Console.WriteLine("Update Time (UTC): {0}", delete.updateTimeUTC);
        //            Console.WriteLine("Reason Code: {0}", delete.auReasonCode);
        //            Console.WriteLine("Reason Description: {0}", delete.reasonDescription);

        //            ////Fetching Subscription ID for AU Delete
        //            //if (delete.subscriptionIds != null && delete.subscriptionIds.Length > 0)
        //            //{
        //            //    Console.WriteLine("List of subscriptions : ");
        //            //    for (int i = 0; i < delete.subscriptionIds.Length; i++)
        //            //        Console.WriteLine(delete.subscriptionIds[i]);
        //            //}
        //        }
        //    }
        //    else if (response != null)
        //    {
        //        Console.WriteLine("Error: " + response.messages.message[0].code + "  " +
        //                            response.messages.message[0].text);
        //    }

        //    return response;
        //}
        public static void GetAccountUpdaterJobDetailsExec(String ApiLoginID, String ApiTransactionKey)
        {
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/GetAccountUpdateJobDetails.csv", FileMode.Open)), true))
            {
                Console.WriteLine("Get Account Updater job details sample");
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
                                Item = ApiTransactionKey,
                            };

                            // parameters for request
                            string month = "2018-05";
                            //AUJobTypeEnum.all = "all";

                            var request = new getAUJobDetailsRequest();
                            request.month = month;
                            request.modifiedTypeFilter = AUJobTypeEnum.all;
                            request.paging = new Paging
                            {
                                limit = 1000,
                                offset = 1
                            };

                            // instantiate the controller that will call the service
                            var controller = new getAUJobDetailsController(request);
                            controller.Execute();

                            // get the response from the service (errors contained if any)
                            var response = controller.GetApiResponse();



                            if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
                            {
                                try
                                {
                                    //Assert.AreEqual(response.Id, customerProfileId);
                                    Console.WriteLine("Assertion Succeed! Valid CustomerId fetched.");
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("GAUJD_00" + flag.ToString());
                                    row1.Add("GetAccountUpdaterJobDetails");
                                    row1.Add("Pass");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //  Console.WriteLine("Success " + TestcaseID + " CustomerID : " + response.Id);
                                    flag = flag + 1;
                                    
                                        foreach (var update in response.auDetails)
                                        {
                                            Console.WriteLine("Profile ID / Payment Profile ID: {0} / {1}", update.customerProfileID, update.customerPaymentProfileID);
                                            Console.WriteLine("Update Time (UTC): {0}", update.updateTimeUTC);
                                            Console.WriteLine("Reason Code: {0}", update.auReasonCode);
                                            Console.WriteLine("Reason Description: {0}", update.reasonDescription);
                                        }
                                        foreach (var delete in response.auDetails)
                                        {
                                            Console.WriteLine("Profile ID / Payment Profile ID: {0} / {1}", delete.customerProfileID, delete.customerPaymentProfileID);
                                            Console.WriteLine("Update Time (UTC): {0}", delete.updateTimeUTC);
                                            Console.WriteLine("Reason Code: {0}", delete.auReasonCode);
                                            Console.WriteLine("Reason Description: {0}", delete.reasonDescription);
                                        }
                                    
                                }
                                catch
                                {
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("GAUJD_00" + flag.ToString());
                                    row1.Add("GetAccountUpdaterJobDetails");
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
                                row1.Add("GAUJD_00" + flag.ToString());
                                row1.Add("GetAccountUpdaterJobDetails");
                                row1.Add("Fail");
                                row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                writer.WriteRow(row1);
                                Console.WriteLine("Assertion Failed! Invalid CustomerId fetched.");
                                flag = flag + 1;
                            }
                        }
                        catch (Exception e)
                        {
                            CsvRow row2 = new CsvRow();
                            row2.Add("GAUJD_00" + flag.ToString());
                            row2.Add("GetAccountUpdaterJobDetails");
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
