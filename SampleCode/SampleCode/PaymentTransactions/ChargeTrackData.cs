using System;
using System.Linq;
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
    /// <summary>
    /// This sample demonstrates making a credit card charge using mag stripe track data. 
    /// 
    /// NOTE:  You must pass the retail fields of DeviceType and MarketType, e.g. 
    /// 
    ///         retail = new transRetailInfoType { deviceType = "1", marketType = "2" }
    ///         
    /// </summary>
    public class ChargeTrackData
    {
        //  public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey, decimal amount)
        //  {
        //      Console.WriteLine("Charge Track Data Sample");

        //      ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;

        //      // define the merchant information (authentication / transaction id)
        //      ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        //      {
        //          name = ApiLoginID,
        //          ItemElementName = ItemChoiceType.transactionKey,
        //          Item = ApiTransactionKey,
        //      };

        //      // You can pass either track 1 or track 2 but not both
        //      var trackData = new creditCardTrackType { ItemElementName = ItemChoiceType1.track2, Item = "4111111111111111=170310199999888" };

        //      //standard api call, simply use cardData in place of creditcard type
        //      var paymentType = new paymentType { Item = trackData };

        //      // Add line Items
        //      var lineItems = new lineItemType[2];
        //      lineItems[0] = new lineItemType { itemId = "1", name = "t-shirt", quantity = 2, unitPrice = new Decimal(15.00) };
        //      lineItems[1] = new lineItemType { itemId = "2", name = "snowboard", quantity = 1, unitPrice = new Decimal(450.00) };

        //      var transactionRequest = new transactionRequestType
        //      {
        //          transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // charge the card

        //          amount = amount,
        //          payment = paymentType,
        //          lineItems = lineItems,
        //          // Retail data required for POS transactions
        //          retail = new transRetailInfoType { deviceType = "1", marketType = "2" }
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

        //public static void ChargeTrackDataExec(String ApiLoginID, String ApiTransactionKey, decimal amount)
        public static void ChargeTrackDataExec(String ApiLoginID, String ApiTransactionKey)
        {
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/ChargeTrackData.csv", FileMode.Open)), true))
            {
                Console.WriteLine("Charge Track Data Sample");

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
                            Item = ApiTransactionKey,
                        };

                        // You can pass either track 1 or track 2 but not both
                        var trackData = new creditCardTrackType { ItemElementName = ItemChoiceType1.track2, Item = "4111111111111111=170310199999888" };

                        //standard api call, simply use cardData in place of creditcard type
                        var paymentType = new paymentType { Item = trackData };

                        // Add line Items
                        var lineItems = new lineItemType[2];
                        lineItems[0] = new lineItemType { itemId = "1", name = "t-shirt", quantity = 2, unitPrice = new Decimal(15.00) };
                        lineItems[1] = new lineItemType { itemId = "2", name = "snowboard", quantity = 1, unitPrice = new Decimal(450.00) };


                        
                        string amount = null;
                        string TestCaseId = null;
                        for (int i = 0; i < fieldCount; i++)
                        {
                            switch (headers[i])
                            {
                                case "TestCaseId":
                                    TestCaseId = csv[i];
                                    break;
                                case "amount":
                                    amount = csv[i];
                                    break;
                                default:
                                    break;
                            }
                        }
                        try
                        {

                        
                        var transactionRequest = new transactionRequestType
                        {
                            transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // charge the card

                            amount = Convert.ToDecimal(amount),
                            payment = paymentType,
                            lineItems = lineItems,
                            // Retail data required for POS transactions
                            retail = new transRetailInfoType { deviceType = "1", marketType = "2" }
                        };

                        CsvRow row = new CsvRow();
                       // try
                        //{
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
                                            //Console.WriteLine("Assertion Succeed! Valid customerProfileId fetched.");
                                            CsvRow row1 = new CsvRow();
                                            row1.Add("CTD_00" + flag.ToString());
                                            row1.Add("ChargeTrackData");
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
                                            row1.Add("CTD_00" + flag.ToString());
                                            row1.Add("ChargeTrackData");
                                            row1.Add("Assertion Failed!");
                                            row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                            writer.WriteRow(row1);
                                            Console.WriteLine("Assertion Failed! Invalid CustomerPaymentProfile fetched.");
                                            flag = flag + 1;
                                        }
                                        /*******************/
                                        
                                    }
                                    else
                                    {
                                        Console.WriteLine("Failed Transaction.");
                                        CsvRow row2 = new CsvRow();
                                        row2.Add("CTD_00" + flag.ToString());
                                        row2.Add("ChargeTrackData");
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
                                    row2.Add("CTD_00" + flag.ToString());
                                    row2.Add("ChargeTrackData");
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
                                row2.Add("CTD_00" + flag.ToString());
                                row2.Add("ChargeTrackData");
                                row2.Add("Fail");
                                row2.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                writer.WriteRow(row2);
                                flag = flag + 1;
                            }

                            //return response;
                        }
                        catch (Exception e)
                        {
                            CsvRow row2 = new CsvRow();
                            row2.Add("CTD_00" + flag.ToString());
                            row2.Add("ChargeTrackData");
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
