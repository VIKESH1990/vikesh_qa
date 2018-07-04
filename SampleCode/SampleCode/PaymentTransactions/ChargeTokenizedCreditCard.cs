﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using AuthorizeNET.Api.Controllers;
using AuthorizeNET.Api.Contracts.V1;
using AuthorizeNET.Api.Controllers.Bases;
using LumenWorks.Framework.IO.Csv;
using System.IO;

namespace net.authorize.sample
{
    public class ChargeTokenizedCreditCard
    {
        //  public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey)
        //  {
        //      Console.WriteLine("Charge Tokenized Credit Card Sample");

        //      ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;

        //      // define the merchant information (authentication / transaction id)
        //      ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        //      {
        //          name = ApiLoginID,
        //          ItemElementName = ItemChoiceType.transactionKey,
        //          Item = ApiTransactionKey,
        //      };

        //      var creditCard = new creditCardType
        //      {
        //          cardNumber = "4111111111111111",
        //          expirationDate = "0718",
        //          cryptogram = Guid.NewGuid().ToString()             // Set this to the value of the cryptogram received from the token provide
        //      };

        //      //standard api call to retrieve response
        //      var paymentType = new paymentType { Item = creditCard };

        //      var transactionRequest = new transactionRequestType
        //      {
        //          transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // charge the card
        //          amount = 133.45m,
        //          payment = paymentType
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

        public static void ChargeTokenizedCreditCardExec(String ApiLoginID, String ApiTransactionKey)
        {
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/ChargeTokenizedCreditCard.csv", FileMode.Open)), true))
            {
                Console.WriteLine("Charge Tokenized Credit Card Sample");
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

                        

                        var creditCard = new creditCardType
                        {
                            cardNumber = "4111111111111111",
                            expirationDate = "0718",
                            cryptogram = Guid.NewGuid().ToString()             // Set this to the value of the cryptogram received from the token provide
                        };

                        //standard api call to retrieve response
                        var paymentType = new paymentType { Item = creditCard };

                        var transactionRequest = new transactionRequestType
                        {
                            transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // charge the card
                            amount = 133.45m,
                            payment = paymentType
                        };

                        string apiLogin = null;
                        string transactionKey = null;
                        string TestCaseId = null;
                        for (int i = 0; i < fieldCount; i++)
                        {
                            switch (headers[i])
                            {
                                case "TestCaseId":
                                    TestCaseId = csv[i];
                                    break;
                                case "apiLogin":
                                    apiLogin = csv[i];
                                    break;
                                case "transactionKey":
                                    transactionKey = csv[i];
                                    break;
                                default:
                                    break;
                            }
                        }
                        // define the merchant information (authentication / transaction id)
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
                                //Append Results                                
                                foreach (var item in item1)
                                    writer.WriteRow(item);
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
                                        /*****************************/
                                        try
                                        {
                                            //Assert.AreEqual(response.Id, customerProfileId);
                                            CsvRow row1 = new CsvRow();
                                            row1.Add("CTCC_00" + flag.ToString());
                                            row1.Add("ChargeTokenizedCreditCard");
                                            row1.Add("Pass");
                                            row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                            writer.WriteRow(row1);
                                            //  Console.WriteLine("Success " + TestcaseID + " CustomerID : " + response.Id);
                                            flag = flag + 1;
                                            Console.WriteLine("Successfully created transaction with Transaction ID: " + response.transactionResponse.transId);
                                            Console.WriteLine("Response Code: " + response.transactionResponse.responseCode);
                                            Console.WriteLine("Message Code: " + response.transactionResponse.messages[0].code);
                                            Console.WriteLine("Description: " + response.transactionResponse.messages[0].description);
                                            Console.WriteLine("Success, Auth Code : " + response.transactionResponse.authCode);
                                        }
                                        catch
                                        {
                                            CsvRow row1 = new CsvRow();
                                            row1.Add("CTCC_00" + flag.ToString());
                                            row1.Add("ChargeTokenizedCreditCard");
                                            row1.Add("Assertion Failed!");
                                            row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                            writer.WriteRow(row1);
                                            flag = flag + 1;
                                        }
                                        /*******************/
                                        
                                    }
                                    else
                                    {
                                        Console.WriteLine("Failed Transaction.");
                                        if (response.transactionResponse.errors != null)
                                        {
                                            Console.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
                                            Console.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
                                        }
                                    }
                                }
                                else
                                {

                                    Console.WriteLine("Failed Transaction.");
                                    CsvRow row2 = new CsvRow();
                                    row2.Add("CTCC_00" + flag.ToString());
                                    row2.Add("ChargeTokenizedCreditCard");
                                    row2.Add("Fail");
                                    row2.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row2);
                                    flag = flag + 1;
                                    //if (response.transactionResponse != null && response.transactionResponse.errors != null)
                                    //{
                                    //    Console.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
                                    //    Console.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
                                    //}
                                    //else
                                    //{
                                    //    Console.WriteLine("Error Code: " + response.messages.message[0].code);
                                    //    Console.WriteLine("Error message: " + response.messages.message[0].text);
                                    //}
                                }
                            }
                            else
                            {
                                Console.WriteLine("Null Response.");
                                CsvRow row2 = new CsvRow();
                                row2.Add("CTCC_00" + flag.ToString());
                                row2.Add("ChargeTokenizedCreditCard");
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
                            row2.Add("CTCC_00" + flag.ToString());
                            row2.Add("ChargeTokenizedCreditCard");
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


                    

                   