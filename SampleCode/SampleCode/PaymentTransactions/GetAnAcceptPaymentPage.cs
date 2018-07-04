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

namespace net.authorize.sample.CustomerProfiles
{
    public class GetAnAcceptPaymentPage
    {
        //public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey, decimal amount)
        //{
        //    Console.WriteLine("GetAnAcceptPaymentPage Sample");
        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;
        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        //    {
        //        name = ApiLoginID,
        //        ItemElementName = ItemChoiceType.transactionKey,
        //        Item = ApiTransactionKey,
        //    };

        //    settingType[] settings = new settingType[2];

        //    settings[0] = new settingType();
        //    settings[0].settingName = settingNameEnum.hostedPaymentButtonOptions.ToString();
        //    settings[0].settingValue = "{\"text\": \"Pay\"}";

        //    settings[1] = new settingType();
        //    settings[1].settingName = settingNameEnum.hostedPaymentOrderOptions.ToString();
        //    settings[1].settingValue = "{\"show\": false}";

        //    var transactionRequest = new transactionRequestType
        //    {
        //        transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // authorize capture only
        //        amount = amount
        //    };

        //    var request = new getHostedPaymentPageRequest();
        //    request.transactionRequest = transactionRequest;
        //    request.hostedPaymentSettings = settings;

        //    // instantiate the contoller that will call the service
        //    var controller = new getHostedPaymentPageController(request);
        //    controller.Execute();

        //    // get the response from the service (errors contained if any)
        //    var response = controller.GetApiResponse();

        //    //validate
        //    if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
        //    {
        //        Console.WriteLine("Message code : " + response.messages.message[0].code);
        //        Console.WriteLine("Message text : " + response.messages.message[0].text);
        //        Console.WriteLine("Token : " + response.token);
        //    }
        //    else if (response != null)
        //    {
        //        Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
        //        Console.WriteLine("Failed to get hosted payment page");
        //    }

        //    return response;
        //}
        public static void GetAnAcceptPaymentPageExec(String ApiLoginID, String ApiTransactionKey)
        {
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/GetAnAcceptPaymentPage.csv", FileMode.Open)), true))
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
                        string Amount = null;
                        
                        string TestCaseId = null;

                        for (int i = 0; i < fieldCount; i++)
                        {
                            switch (headers[i])
                            {
                               
                                case "Amount":
                                    Amount = csv[i];
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

                            settingType[] settings = new settingType[2];

                            settings[0] = new settingType();
                            settings[0].settingName = settingNameEnum.hostedPaymentButtonOptions.ToString();
                            settings[0].settingValue = "{\"text\": \"Pay\"}";

                            settings[1] = new settingType();
                            settings[1].settingName = settingNameEnum.hostedPaymentOrderOptions.ToString();
                            settings[1].settingValue = "{\"show\": false}";

                            var transactionRequest = new transactionRequestType
                            {
                                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // authorize capture only
                                amount = Convert.ToDecimal(Amount)
                            };

                            var request = new getHostedPaymentPageRequest();
                            request.transactionRequest = transactionRequest;
                            request.hostedPaymentSettings = settings;

                            // instantiate the contoller that will call the service
                            var controller = new getHostedPaymentPageController(request);
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
                                    row1.Add("GAPP-00" + flag.ToString());
                                    row1.Add("GetAnAcceptPaymentPage");
                                    row1.Add("Pass");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //  Console.WriteLine("Success " + TestcaseID + " CustomerID : " + response.Id);
                                    flag = flag + 1;
                                    Console.WriteLine("Message code : " + response.messages.message[0].code);
                                    Console.WriteLine("Message text : " + response.messages.message[0].text);
                                    Console.WriteLine("Token : " + response.token);
                                }
                                catch
                                {
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("GAPP_00" + flag.ToString());
                                    row1.Add("GetAnAcceptPaymentPage");
                                    row1.Add("Assertion Failed!");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //Console.WriteLine("Assertion Failed! Invalid CustomerId fetched.");
                                    flag = flag + 1;
                                }
                            }
                            else
                            {
                                CsvRow row1 = new CsvRow();
                                row1.Add("GAPP_00" + flag.ToString());
                                row1.Add("GetAnAcceptPaymentPage");
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
                            row2.Add("GAPP_00" + flag.ToString());
                            row2.Add("GetAnAcceptPaymentPage");
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
