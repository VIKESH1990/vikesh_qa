using AuthorizeNET.Api.Contracts.V1;
using AuthorizeNET.Api.Controllers;
using AuthorizeNET.Api.Controllers.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LumenWorks.Framework.IO.Csv;

namespace net.authorize.sample
{
    class GetMerchantDetails
    {
        //public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey)
        //{
        //    Console.WriteLine("Get Merchant Details Sample");

        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;

        //    var request = new getMerchantDetailsRequest
        //    {
        //        merchantAuthentication = new merchantAuthenticationType() { name = ApiLoginID, Item = ApiTransactionKey, ItemElementName = ItemChoiceType.transactionKey }
        //    };

        //    // instantiate the controller that will call the service
        //    var controller = new getMerchantDetailsController(request);
        //    controller.Execute();

        //    // get the response from the service (errors contained if any)
        //    var response = controller.GetApiResponse();

        //    // validate
        //    if (response != null)
        //    {
        //        if (response.messages.resultCode == messageTypeEnum.Ok)
        //        {
        //            Console.WriteLine("Merchant Name: " + response.merchantName);
        //            Console.WriteLine("Gateway ID: " + response.gatewayId);
        //            Console.Write("Processors: ");
        //            foreach (processorType processor in response.processors)
        //            {
        //                Console.Write(processor.name + "; ");
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("Failed to get merchant details.");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Null Response.");
        //    }

        //    return response;
        //}

        public static void GetMerchantDetailsExec(String ApiLoginID, String ApiTransactionKey)
        {
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/GetMerchantDetails.csv", FileMode.Open)), true))
            {
                Console.WriteLine("Get Merchant Details Sample");

                int flag = 0;
                int fieldCount = csv.FieldCount;
                string[] headers = csv.GetFieldHeaders();

                //Append data
                var item1 = DataAppend.ReadPrevData();
                using (CsvFileWriter writer = new CsvFileWriter(new FileStream(@"../../../CSV_DATA/Outputfile.csv", FileMode.Open)))
                {
                    while (csv.ReadNextRecord())
                    {
                        ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;

                        string apiLogin = null;
                        string transactionKey = null;
                        string TestcaseID = null;
                        int count = 0;
                        for (int i = 0; i < fieldCount; i++)
                        {
                            // Read the headers with values from the test data input file
                            switch (headers[i])
                            {
                                case "apiLogin":
                                    apiLogin = csv[i];
                                    break;
                                case "transactionKey":
                                    transactionKey = csv[i];
                                    break;
                                case "TestcaseID":
                                    TestcaseID = csv[i];
                                    count++;
                                    break;
                                default:
                                    break;
                            }
                        }
                        var request = new getMerchantDetailsRequest
                        {
                            merchantAuthentication = new merchantAuthenticationType()
                            {
                                name = apiLogin,
                                Item = transactionKey,
                                ItemElementName = ItemChoiceType.transactionKey
                            }
                        };
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
                                //Append Result                                
                                foreach (var item in item1)
                                    writer.WriteRow(item);
                                flag = flag + 1;
                            }
                            // instantiate the controller that will call the service
                        var controller = new getMerchantDetailsController(request);
                        controller.Execute();

                        // get the response from the service (errors contained if any)
                        var response = controller.GetApiResponse();

                        // validate
                        if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
                        {
                            //if (response.messages.resultCode == messageTypeEnum.Ok)
                            //{
                                    /*****************************/
                                    try
                                    {
                                        //Assert.AreEqual(response.Id, customerProfileId);
                                        //Console.WriteLine("Assertion Succeed! Valid customerProfileId fetched.");
                                        CsvRow row1 = new CsvRow();
                                        row1.Add("GMD_00" + flag.ToString());
                                        row1.Add("GetMerchantDetails");
                                        row1.Add("Pass");
                                        row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                        writer.WriteRow(row1);
                                        //  Console.WriteLine("Success " + TestcaseID + " CustomerID : " + response.Id);
                                        flag = flag + 1;

                                        //Console.WriteLine("Merchant Name: " + response.merchantName);
                                        //Console.WriteLine("Gateway ID: " + response.gatewayId);
                                        //Console.Write("Processors: ");
                                        //foreach (processorType processor in response.processors)
                                        //{
                                        //    Console.Write(processor.name + "; ");
                                        //}
                                    }
                                    catch
                                    {
                                        CsvRow row1 = new CsvRow();
                                        row1.Add("GMD_00" + flag.ToString());
                                        row1.Add("GetMerchantDetails");
                                        row1.Add("Assertion Failed!");
                                        row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                        writer.WriteRow(row1);
                                        //Console.WriteLine("Assertion Failed! Invalid CustomerPaymentProfile fetched.");
                                        flag = flag + 1;
                                    }
                                    /*******************/
                                
                            //}
                            //else
                            //{
                            //    Console.WriteLine("Failed to get merchant details.");
                            //}
                        }
                        else
                        {
                            Console.WriteLine("Null Response.");
                                CsvRow row2 = new CsvRow();
                                row2.Add("GMD_00" + flag.ToString());
                                row2.Add("GetMerchantDetails");
                                row2.Add("Fail");
                                row2.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                writer.WriteRow(row2);
                                flag = flag + 1;
                            }

                        }

                        //return response;
                        catch (Exception e)
                        {
                            CsvRow row2 = new CsvRow();
                            row2.Add("GMD_00" + flag.ToString());
                            row2.Add("GetMerchantDetails");
                            row2.Add("Fail");
                            row2.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                            writer.WriteRow(row2);
                            flag = flag + 1;
                            //Console.WriteLine(TestCaseId + " Error Message " + e.Message);
                        }
                    }
                }
            }
        }
    }
}
