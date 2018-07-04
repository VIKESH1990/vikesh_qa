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
    public class CreditBankAccount
    {
        //  public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey, string TransactionID)
        //  {
        //      Console.WriteLine("Credit Bank Account");

        //      ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;

        //      // define the merchant information (authentication / transaction id)
        //      ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        //      {
        //          name = ApiLoginID,
        //          ItemElementName = ItemChoiceType.transactionKey,
        //          Item = ApiTransactionKey
        //      };

        //      var bankAccount = new bankAccountType
        //      {
        //          accountNumber = "4111111",
        //          routingNumber = "325070760",
        //          echeckType = echeckTypeEnum.WEB,   // change based on how you take the payment (web, telephone, etc)
        //          nameOnAccount = "Test Name"
        //      };

        //      //standard api call to retrieve response
        //      var paymentType = new paymentType { Item = bankAccount };

        //      var transactionRequest = new transactionRequestType
        //      {
        //          transactionType = transactionTypeEnum.refundTransaction.ToString(),    // refund type
        //          payment         = paymentType,
        //          amount          = 126.44m,
        //          refTransId      = TransactionID
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
        //Console.WriteLine("Success, Transaction Code : " + response.transactionResponse.transId);
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

        public static void CreditBankAccountExec(String ApiLoginID, String ApiTransactionKey)
        {
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/CreditBankAccount.csv", FileMode.Open)), true))
            {
                Console.WriteLine("Credit Bank Account");
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

                        // define the merchant information (authentication / transaction id)
                        ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
                        {
                            name = ApiLoginID,
                            ItemElementName = ItemChoiceType.transactionKey,
                            Item = ApiTransactionKey
                        };

                        var bankAccount = new bankAccountType
                        {
                            accountNumber = "4111111",
                            routingNumber = "325070760",
                            echeckType = echeckTypeEnum.WEB,   // change based on how you take the payment (web, telephone, etc)
                            nameOnAccount = "Test Name"
                        };

                        //standard api call to retrieve response
                        var paymentType = new paymentType { Item = bankAccount };

                        
                        string TransactionID = null;
                        string TestcaseID = null;
                        for (int i = 0; i < fieldCount; i++)
                        {
                            switch (headers[i])
                            {
                                case "TransactionID":
                                    TransactionID = csv[i];
                                    break;
                                case "TestcaseID":
                                    TestcaseID = csv[i];
                                    break;
                                default:
                                    break;
                            }
                        }
                        try
                        {
                            var transactionRequest = new transactionRequestType
                            {
                                transactionType = transactionTypeEnum.refundTransaction.ToString(),    // refund type
                                payment = paymentType,
                                amount = 126.44m,
                                refTransId = TransactionID
                            };
                        //Write to output file
                        CsvRow row = new CsvRow();
                        
                            if (flag == 0)
                            {
                                row.Add("TestCaseId");
                                row.Add("APIName");
                                row.Add("Status");
                                row.Add("TimeStamp");
                                writer.WriteRow(row);
                                //foreach (var item in records1)
                                //{
                                //    CsvRow row_prev = new CsvRow();
                                //    row_prev.Add(item.TestCaseId.ToString());
                                //    row_prev.Add(item.APIName.ToString());
                                //    row_prev.Add(item.Status.ToString());
                                //    row_prev.Add(item.TimeStamp.ToString());
                                //    writer.WriteRow(row_prev);
                                //}
                                //Append Results                                
                                foreach (var item in item1)
                                    writer.WriteRow(item);
                                flag = flag + 1;
                            }
                            var request = new createTransactionRequest { transactionRequest = transactionRequest };

                            // instantiate the contoller that will call the service
                            var controller = new createTransactionController(request);
                            controller.Execute();

                            // get the response from the service (errors contained if any)
                            var response = controller.GetApiResponse();

                            //validate
                            if (response != null)
                            {
                                if (response.messages.resultCode == messageTypeEnum.Ok)
                                {

                                    if (response.transactionResponse.messages != null)
                                    {
                                        try
                                        {
                                            //Assert.AreEqual(response.Id, customerProfileId);
                                            //Console.WriteLine("Assertion Succeed! Valid CustomerId fetched.");
                                            CsvRow row1 = new CsvRow();
                                            row1.Add("CBA_00" + flag.ToString());
                                            row1.Add("CreditBankAccount");
                                            row1.Add("Pass");
                                            row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                            writer.WriteRow(row1);
                                            //  Console.WriteLine("Success " + TestcaseID + " CustomerID : " + response.Id);
                                            flag = flag + 1;
                                            Console.WriteLine("Successfully created transaction with Transaction ID: " + response.transactionResponse.transId);
                                            Console.WriteLine("Response Code: " + response.transactionResponse.responseCode);
                                            Console.WriteLine("Message Code: " + response.transactionResponse.messages[0].code);
                                            Console.WriteLine("Description: " + response.transactionResponse.messages[0].description);
                                            Console.WriteLine("Success, Transaction Code : " + response.transactionResponse.transId);
                                        }
                                        catch
                                        {
                                            CsvRow row1 = new CsvRow();
                                            row1.Add("CBA_00" + flag.ToString());
                                            row1.Add("CreditBankAccount");
                                            row1.Add("Assertion Failed!");
                                            row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                            writer.WriteRow(row1);
                                            //Console.WriteLine("Assertion Failed! Invalid CustomerId fetched.");
                                            flag = flag + 1;
                                        }



                                    }
                                    else
                                    {
                                        Console.WriteLine("Failed Transaction.");
                                        CsvRow row2 = new CsvRow();
                                        row2.Add("GCP_00" + flag.ToString());
                                        row2.Add("CreditBankAccount");
                                        row2.Add("Fail");
                                        row2.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                        writer.WriteRow(row2);
                                        flag = flag + 1;
                                        //if (response.transactionResponse.errors != null)
                                        //{
                                        //    Console.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
                                        //    Console.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
                                        //}
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Failed Transaction.");
                                    CsvRow row2 = new CsvRow();
                                    row2.Add("GCP_00" + flag.ToString());
                                    row2.Add("CreditBankAccount");
                                    row2.Add("Fail");
                                    row2.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row2);
                                    flag = flag + 1;
                                    //else
                                    //{
                                    //    Console.WriteLine("Failed Transaction.");
                                    //    if (response.transactionResponse != null && response.transactionResponse.errors != null)
                                    //    {
                                    //        Console.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
                                    //        Console.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
                                    //    }
                                    //    else
                                    //    {
                                    //        Console.WriteLine("Error Code: " + response.messages.message[0].code);
                                    //        Console.WriteLine("Error message: " + response.messages.message[0].text);
                                    //    }
                                    //}
                                }
                            }
                            else
                            {
                                Console.WriteLine("Null Response.");
                                CsvRow row2 = new CsvRow();
                                row2.Add("GCP_00" + flag.ToString());
                                row2.Add("CreditBankAccount");
                                row2.Add("Fail");
                                row2.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                writer.WriteRow(row2);
                                flag = flag + 1;
                                
                            }
                        }
                        catch (Exception e)
                        {
                            CsvRow row2 = new CsvRow();
                            row2.Add("GCP_00" + flag.ToString());
                            row2.Add("CreditBankAccount");
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

                        

                        

    



