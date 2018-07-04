using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AuthorizeNET.Api.Controllers;
using AuthorizeNET.Api.Contracts.V1;
using AuthorizeNET.Api.Controllers.Bases;
using LumenWorks.Framework.IO.Csv;
using System.IO;
using NUnit.Framework;
using System.Diagnostics;

namespace net.authorize.sample
{
    public class CapturePreviouslyAuthorizedAmount
    {
        /// <summary>
        /// Capture a Transaction Previously Submitted Via CaptureOnly
        /// </summary>
        /// <param name="ApiLoginID">Your ApiLoginID</param>
        /// <param name="ApiTransactionKey">Your ApiTransactionKey</param>
        /// <param name="TransactionAmount">The amount submitted with CaptureOnly</param>
        /// <param name="TransactionID">The TransactionID of the previous CaptureOnly operation</param>
      //  public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey, decimal TransactionAmount, string TransactionID)
      //  {
      //      Console.WriteLine("Capture Previously Authorized Amount");

      //      ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;

      //      // define the merchant information (authentication / transaction id)
      //      ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
      //      {
      //          name = ApiLoginID,
      //          ItemElementName = ItemChoiceType.transactionKey,
      //          Item = ApiTransactionKey
      //      };


      //      var transactionRequest = new transactionRequestType
      //      {
      //          transactionType = transactionTypeEnum.priorAuthCaptureTransaction.ToString(),    // capture prior only
      //          amount      = TransactionAmount,
      //          refTransId  = TransactionID
      //      };

      //      var request = new createTransactionRequest { transactionRequest = transactionRequest };

      //      // instantiate the contoller that will call the service
      //      var controller = new createTransactionController(request);
      //      controller.Execute();

      //      // get the response from the service (errors contained if any)
      //      var response = controller.GetApiResponse();

      //      //validate
      //      if (response != null)
      //      {
      //          if (response.messages.resultCode == messageTypeEnum.Ok)
      //          {
      //              if(response.transactionResponse.messages != null)
      //              {
      //                  Console.WriteLine("Successfully created transaction with Transaction ID: " + response.transactionResponse.transId);
      //                  Console.WriteLine("Response Code: " + response.transactionResponse.responseCode);
      //                  Console.WriteLine("Message Code: " + response.transactionResponse.messages[0].code);
      //                  Console.WriteLine("Description: " + response.transactionResponse.messages[0].description);
						//Console.WriteLine("Success, Auth Code : " + response.transactionResponse.authCode);
      //              }
      //              else
      //              {
      //                  Console.WriteLine("Failed Transaction.");
      //                  if (response.transactionResponse.errors != null)
      //                  {
      //                      Console.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
      //                      Console.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
      //                  }
      //              }
      //          }
      //          else
      //          {
      //              Console.WriteLine("Failed Transaction.");
      //              if (response.transactionResponse != null && response.transactionResponse.errors != null)
      //              {
      //                  Console.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
      //                  Console.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
      //              }
      //              else
      //              {
      //                  Console.WriteLine("Error Code: " + response.messages.message[0].code);
      //                  Console.WriteLine("Error message: " + response.messages.message[0].text);
      //              }
      //          }
      //      }
      //      else
      //      {
      //          Console.WriteLine("Null Response.");
      //      }

      //      return response;
      //  }

        public static void CapturePreviouslyAuthorizedAmountExec(String ApiLoginID, String ApiTransactionKey)
        {
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/CapturePreviouslyAuthorizedAmount.csv", FileMode.Open)), true))
            {
                Console.WriteLine("Capture Previously Authorized Amount");
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
                        string TestcaseID = null;
                        string TransactionAmount = null;
                        string TransactionId = null;


                        for (int i = 0; i < fieldCount; i++)
                        {
                            switch (headers[i])
                            {

                                case "TestcaseID":
                                    TestcaseID = csv[i];
                                    break;
                                case "TransactionAmount":
                                    TransactionAmount = csv[i];
                                    break;
                                case "TransactionId":
                                    TransactionId = csv[i];
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

                            var transactionRequest = new transactionRequestType
                            {
                                transactionType = transactionTypeEnum.priorAuthCaptureTransaction.ToString(),    // capture prior only
                                amount = Convert.ToDecimal(TransactionAmount),
                                refTransId = TransactionId
                            };

                            var request = new createTransactionRequest { transactionRequest = transactionRequest };

                            // instantiate the contoller that will call the service
                            var controller = new createTransactionController(request);
                            controller.Execute();

                            // get the response from the service (errors contained if any)
                            var response = controller.GetApiResponse();



                            if (response != null && response.messages.resultCode == messageTypeEnum.Ok
                                && response.transactionResponse.messages != null)
                            {
                                try
                                {
                                    //Assert.AreEqual(response.Id, customerProfileId);
                                    Console.WriteLine("Assertion Succeed! Valid CustomerId fetched.");
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("CPAA_00" + flag.ToString());
                                    row1.Add("CapturePreviouslyAuthorizedAmount");
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
                                    row1.Add("CPAA_00" + flag.ToString());
                                    row1.Add("CapturePreviouslyAuthorizedAmount");
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
                                row1.Add("CPAA_00" + flag.ToString());
                                row1.Add("CapturePreviouslyAuthorizedAmount");
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
                            row2.Add("CPAA_00" + flag.ToString());
                            row2.Add("CapturePreviouslyAuthorizedAmount");
                            row2.Add("Fail");
                            row2.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                            writer.WriteRow(row2);
                            flag = flag + 1;
                            Console.WriteLine(TestcaseID + " Error Message " + e.Message);
                        }
                    }
                }
            }
        }
    }
}
