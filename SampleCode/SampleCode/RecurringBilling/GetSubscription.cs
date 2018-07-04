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
    public class GetSubscription
    {
        //public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey, string subscriptionId)
        //{
        //    Console.WriteLine("Get Subscription Sample");

        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;

        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        //    {
        //        name = ApiLoginID,
        //        ItemElementName = ItemChoiceType.transactionKey,
        //        Item = ApiTransactionKey
        //    };

        //    var request = new ARBGetSubscriptionRequest { subscriptionId = subscriptionId };    

        //    var controller = new ARBGetSubscriptionController(request);          // instantiate the contoller that will call the service
        //    controller.Execute();

        //    ARBGetSubscriptionResponse response = controller.GetApiResponse();   // get the response from the service (errors contained if any)

        //    //validate
        //    if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
        //    {
        //        if (response.subscription != null)
        //        {
        //            Console.WriteLine("Subscription returned : " + response.subscription.name);
        //        }
        //    }
        //    else if (response != null)
        //    {
        //        if (response.messages.message.Length > 0)
        //        {
        //            Console.WriteLine("Error: " + response.messages.message[0].code + "  " +
        //                              response.messages.message[0].text);
        //        }
        //    }
        //    else
        //    {
        //        if (controller.GetErrorResponse().messages.message.Length > 0)
        //        {
        //            Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
        //        }
        //    }

        //    return response;
        //}
        public static void GetSubscriptionExec(String ApiLoginID, String ApiTransactionKey)
        {
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/GetASubscription.csv", FileMode.Open)), true))
            {
                Console.WriteLine("Get Subscription Sample");
                int flag = 0;
                int fieldCount = csv.FieldCount;
                string[] headers = csv.GetFieldHeaders();
                //Append Data
                var item1 = DataAppend.ReadPrevData();
                using (CsvFileWriter writer = new CsvFileWriter(new FileStream(@"../../../CSV_DATA/Outputfile.csv", FileMode.Open)))
                {
                    
                    
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
                        string subscriptionId = null;
                        string TestCase_Id = null;
                        for (int i = 0; i < fieldCount; i++)
                        {
                            switch (headers[i])
                            {
                                case "subscriptionId":
                                    subscriptionId = csv[i];
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
                            //response = instance.GetCustomer(customerId, authorization);

                            var request = new ARBGetSubscriptionRequest { subscriptionId = subscriptionId };

                            var controller = new ARBGetSubscriptionController(request);          // instantiate the contoller that will call the service
                            controller.Execute();

                            ARBGetSubscriptionResponse response = controller.GetApiResponse();
                            if (response != null && response.messages.resultCode == messageTypeEnum.Ok
                                && response.subscription != null)
                            {
                                try
                                {
                                    //Assert.AreEqual(response.Id, customerProfileId);
                                    Console.WriteLine("Assertion Succeed! Valid CustomerId fetched.");
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("GAS_00" + flag.ToString());
                                    row1.Add("GetASubscription");
                                    row1.Add("Pass");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //  Console.WriteLine("Success " + TestcaseID + " CustomerID : " + response.Id);
                                    flag = flag + 1;
                                    Console.WriteLine("Subscription returned : " + response.subscription.name);
                                }
                                catch
                                {
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("GAS_00" + flag.ToString());
                                    row1.Add("GetASubscription");
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
                                row1.Add("GAS_00" + flag.ToString());
                                row1.Add("GetASubscription");
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
                            row2.Add("GAS_00" + flag.ToString());
                            row2.Add("GetASubscription");
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
