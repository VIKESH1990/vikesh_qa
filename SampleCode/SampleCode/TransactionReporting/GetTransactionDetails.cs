using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizeNET.Api.Contracts.V1;
using AuthorizeNET.Api.Controllers;
using AuthorizeNET.Api.Controllers.Bases;
using System.IO;
using LumenWorks.Framework.IO.Csv;
//using NUnit.Framework;

namespace net.authorize.sample
{
    public class GetTransactionDetails
    {
        //  public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey, string transactionId)
        //{
        //    Console.WriteLine("Get transaction details sample");

        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;
        //    // define the merchant information (authentication / transaction id)
        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        //    {
        //        name = ApiLoginID,
        //        ItemElementName = ItemChoiceType.transactionKey,
        //        Item = ApiTransactionKey,
        //    };

        //    var request = new getTransactionDetailsRequest();
        //    request.transId = transactionId;

        //    // instantiate the controller that will call the service
        //    var controller = new getTransactionDetailsController(request);
        //    controller.Execute();

        //    // get the response from the service (errors contained if any)
        //    var response = controller.GetApiResponse();

        //    if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
        //    {
        //        if (response.transaction == null)
        //            return response;

        //        Console.WriteLine("Transaction Id: {0}", response.transaction.transId);
        //        Console.WriteLine("Transaction type: {0}", response.transaction.transactionType);
        //        Console.WriteLine("Transaction status: {0}", response.transaction.transactionStatus);
        //        Console.WriteLine("Transaction auth amount: {0}", response.transaction.authAmount);
        //        Console.WriteLine("Transaction settle amount: {0}", response.transaction.settleAmount);
        //    }
        //    else if (response != null)
        //    {
        //        Console.WriteLine("Error: " + response.messages.message[0].code + "  " +
        //                          response.messages.message[0].text);
        //    }

        //    return response;
        //}

        public static void GetTransactionDetailsExec(String ApiLoginID, String ApiTransactionKey)
        {
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/GetTransactionDetails.csv", FileMode.Open)), true))
            {
                Console.WriteLine("Get transaction details sample");

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
                        
                        string apiLogin = null;
                        string transactionKey = null;
                        string TestcaseID = null;
                        string transactionId = null;
                        //int count = 0;
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
                                case "transactionId":
                                    transactionId = csv[i];
                                    break;
                                case "TestcaseID":
                                    TestcaseID = csv[i];
                                    //count++;
                                    break;
                                default:
                                    break;
                            }
                        }
                        ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
                        {
                            name = apiLogin,
                            ItemElementName = ItemChoiceType.transactionKey,
                            Item = transactionKey,
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
                                flag = flag + 1;

                                //Append Data                                
                                foreach (var item in item1)
                                    writer.WriteRow(item);
                                
                            }
                        var request = new getTransactionDetailsRequest();
                        request.transId = transactionId;

                        // instantiate the controller that will call the service
                        var controller = new getTransactionDetailsController(request);
                        controller.Execute();

                        // get the response from the service (errors contained if any)
                        var response = controller.GetApiResponse();

                        if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
                            {
                            /*****************************/
                            try
                            {
                                    //Assert.AreEqual(response.transaction.transId, transactionId);
                                    //Console.WriteLine("Assertion Succeed! Valid TransactionDetails fetched.");
                                    CsvRow row1 = new CsvRow();
                                row1.Add("GTD_00" + flag.ToString());
                                row1.Add("GetTransactionDetails");
                                row1.Add("Pass");
                                row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                writer.WriteRow(row1);
                                //  Console.WriteLine("Success " + TestcaseID + " CustomerID : " + response.Id);
                                flag = flag + 1;

                                Console.WriteLine("Transaction Id: {0}", response.transaction.transId);
                                Console.WriteLine("Transaction type: {0}", response.transaction.transactionType);
                                Console.WriteLine("Transaction status: {0}", response.transaction.transactionStatus);
                                Console.WriteLine("Transaction auth amount: {0}", response.transaction.authAmount);
                                Console.WriteLine("Transaction settle amount: {0}", response.transaction.settleAmount);
                            }
                            catch
                            {
                                CsvRow row1 = new CsvRow();
                                row1.Add("GTD_00" + flag.ToString());
                                row1.Add("GetTransactionDetails");
                                row1.Add("Fail");
                                row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                writer.WriteRow(row1);
                                //Console.WriteLine("Assertion Failed! Invalid CustomerPaymentProfile fetched.");
                                flag = flag + 1;
                            }
                            /*******************/
                            //if (response.transaction == null)
                            //return response;

                            
                        }
                        else
                        {
                            Console.WriteLine("Null response");
                            CsvRow row2 = new CsvRow();
                            row2.Add("GTD_00" + flag.ToString());
                            row2.Add("GetTransactionDetails");
                            row2.Add("Fail");
                            row2.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                            writer.WriteRow(row2);
                            flag = flag + 1;
                        }
                        //else if (response != null)
                        //{
                        //    Console.WriteLine("Error: " + response.messages.message[0].code + "  " +
                        //                      response.messages.message[0].text);
                        //}
                        }

                        //return response;
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            CsvRow row2 = new CsvRow();
                            row2.Add("GTD_00" + flag.ToString());
                            row2.Add("GetTransactionDetails");
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
