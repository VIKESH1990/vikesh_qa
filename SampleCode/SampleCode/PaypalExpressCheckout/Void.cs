using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AuthorizeNET.Api.Controllers;
using AuthorizeNET.Api.Contracts.V1;
using AuthorizeNET.Api.Controllers.Bases;

using System.IO;
using LumenWorks.Framework.IO.Csv;

namespace net.authorize.sample
{
    public class PayPalVoid
    {
        //public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey, string TransactionID)
        //{
        //    Console.WriteLine("PayPal Void Transaction");

        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;

        //    // define the merchant information (authentication / transaction id)
        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        //    {
        //        name            = ApiLoginID,
        //        ItemElementName = ItemChoiceType.transactionKey,
        //        Item            = ApiTransactionKey
        //    };

        //    var payPalType = new payPalType
        //    {
        //        cancelUrl   = "",
        //        successUrl  = "",     // the url where the user will be returned to            
        //    };

        //    //standard api call to retrieve response
        //    var paymentType = new paymentType { Item = payPalType };

        //    var transactionRequest = new transactionRequestType
        //    {
        //        transactionType = transactionTypeEnum.voidTransaction.ToString(),    // refund type
        //        payment         = paymentType,
        //        refTransId      = TransactionID
        //    };

        //    var request = new createTransactionRequest { transactionRequest = transactionRequest };

        //    // instantiate the contoller that will call the service
        //    var controller = new createTransactionController(request);
        //    controller.Execute();

        //    // get the response from the service (errors contained if any)
        //    var response = controller.GetApiResponse();

        //    //validate
        //    if (response != null)
        //    {
        //        if (response.messages.resultCode == messageTypeEnum.Ok)
        //        {
        //            if(response.transactionResponse.messages != null)
        //            {
        //                Console.WriteLine("Successfully created transaction with Transaction ID: " + response.transactionResponse.transId);
        //                Console.WriteLine("Response Code: " + response.transactionResponse.responseCode);
        //                Console.WriteLine("Message Code: " + response.transactionResponse.messages[0].code);
        //                Console.WriteLine("Description: " + response.transactionResponse.messages[0].description);
        //            }
        //            else
        //            {
        //                Console.WriteLine("Failed Transaction.");
        //                if (response.transactionResponse.errors != null)
        //                {
        //                    Console.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
        //                    Console.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("Failed Transaction.");
        //            if (response.transactionResponse != null && response.transactionResponse.errors != null)
        //            {
        //                Console.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
        //                Console.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
        //            }
        //            else
        //            {
        //                Console.WriteLine("Error Code: " + response.messages.message[0].code);
        //                Console.WriteLine("Error message: " + response.messages.message[0].text);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Null Response.");
        //    }

        //    return response;
        //}

        public static void PayPalVoidExec(String ApiLoginID, String ApiTransactionKey)
        {
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/PayPalVoid.csv", FileMode.Open)), true))
            {
                Console.WriteLine("PayPal Void Transaction");
                int flag = 0;
                int fieldCount = csv.FieldCount;
                string[] headers = csv.GetFieldHeaders();
                //Append Data
                var item1 = DataAppend.ReadPrevData();
                using (CsvFileWriter writer = new CsvFileWriter(new FileStream(@"../../../CSV_DATA/Outputfile.csv", FileMode.Open)))
                {
                    while (csv.ReadNextRecord())
                    {
                        ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;

                        // define the merchant information (authentication / transaction id)
                        ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
                        {
                            name = ApiLoginID,
                            ItemElementName = ItemChoiceType.transactionKey,
                            Item = ApiTransactionKey
                        };

                        var payPalType = new payPalType
                        {
                            cancelUrl = "",
                            successUrl = "",     // the url where the user will be returned to            
                        };
                        string TransactionID = null;
                        string TestCaseId = null;
                        for (int i = 0; i < fieldCount; i++)
                        {
                            switch (headers[i])
                            {
                                case "TestCaseId":
                                    TestCaseId = csv[i];
                                    break;
                                case "TransactionID":
                                    TransactionID = csv[i];
                                    break;
                                default:
                                    break;
                            }
                        }
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


                            //standard api call to retrieve response
                            var paymentType = new paymentType { Item = payPalType };

                            var transactionRequest = new transactionRequestType
                            {
                                transactionType = transactionTypeEnum.voidTransaction.ToString(),    // refund type
                                payment = paymentType,
                                refTransId = TransactionID
                            };

                            var request = new createTransactionRequest { transactionRequest = transactionRequest };

                            // instantiate the contoller that will call the service
                            var controller = new createTransactionController(request);
                            controller.Execute();

                            // get the response from the service (errors contained if any)
                            var response = controller.GetApiResponse();

                            //validate
                            if (response != null && response.messages.resultCode == messageTypeEnum.Ok
                                && response.transactionResponse.messages != null)
                            {
                                try
                                {
                                    //Assert.AreEqual(response.Id, customerProfileId);
                                    //Console.WriteLine("Assertion Succeed! Valid customerProfileId fetched.");
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("PPV_00" + flag.ToString());
                                    row1.Add("PayPalVoid");
                                    row1.Add("Pass");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //  Console.WriteLine("Success " + TestcaseID + " CustomerID : " + response.Id);
                                    flag = flag + 1;
                                    Console.WriteLine("Successfully created transaction with Transaction ID: " + response.transactionResponse.transId);
                                    Console.WriteLine("Response Code: " + response.transactionResponse.responseCode);
                                    Console.WriteLine("Message Code: " + response.transactionResponse.messages[0].code);
                                    Console.WriteLine("Description: " + response.transactionResponse.messages[0].description);
                                }
                                catch
                                {
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("PPV_00" + flag.ToString());
                                    row1.Add("PayPalVoid");
                                    row1.Add("Fail");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //Console.WriteLine("Assertion Failed! Invalid CustomerPaymentProfile fetched.");
                                    flag = flag + 1;
                                }      
                            }
                            else
                            {
                                //Console.WriteLine("Null Response.");
                                CsvRow row1 = new CsvRow();
                                row1.Add("PPV_00" + flag.ToString());
                                row1.Add("PayPalVoid");
                                row1.Add("Fail");
                                row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                writer.WriteRow(row1);
                                //Console.WriteLine("Assertion Failed! Invalid CustomerPaymentProfile fetched.");
                                flag = flag + 1;
                            }

                            //return response;
                        }
                        catch (Exception e)
                        {
                            CsvRow row2 = new CsvRow();
                            row2.Add("PPV_00" + flag.ToString());
                            row2.Add("PayPalVoid");
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
