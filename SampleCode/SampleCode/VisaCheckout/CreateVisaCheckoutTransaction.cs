using AuthorizeNET.Api.Contracts.V1;
using AuthorizeNET.Api.Controllers;
using AuthorizeNET.Api.Controllers.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using LumenWorks.Framework.IO.Csv;

namespace net.authorize.sample
{
    public class CreateVisaCheckoutTransaction
    {
        //  public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey)
        //  {
        //      Console.WriteLine("Running VisaCheckoutTransaction Sample ...");
        //      // The test setup.
        //      ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        //      {
        //          name = ApiLoginID,
        //          ItemElementName = ItemChoiceType.transactionKey,
        //          Item = ApiTransactionKey,
        //      };

        //      ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;


        //      //set up data based on transaction
        //      var transactionAmount = Convert.ToDecimal(25.60);
        //      var opaqueDataType = new opaqueDataType
        //      {
        //          dataDescriptor = "COMMON.VCO.ONLINE.PAYMENT",
        //          dataKey = "NQzcMISSxLX789w+CGX+tXi3lKntO1dpZbZaREOUprVRByJkg1xnpc2Wx9aT5/BLOxQmHqmIsjjy+tF6HqKKGwovvXjIS3fE3y3tBRNbz8D7y6vYMup+AWbEvZqDEBSi",
        //          dataValue = "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"
        //      };

        //      //standard api call to retrieve response
        //      var paymentType = new paymentType { Item = opaqueDataType };
        //      var transactionRequest = new transactionRequestType
        //      {
        //          transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
        //          payment = paymentType,
        //          amount = transactionAmount,
        //          callId = "1482912778237697701"
        //      };
        //      var request = new createTransactionRequest { transactionRequest = transactionRequest };
        //      var controller = new createTransactionController(request);
        //      controller.Execute();
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
        public static void CreateVisaCheckoutTransactionExec(String ApiLoginID, String ApiTransactionKey)
        {
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/CreateVisaCheckoutTransaction.csv", FileMode.Open)), true))
            {
                Console.WriteLine("Running VisaCheckoutTransaction Sample ...");
                int flag = 0;
                int fieldCount = csv.FieldCount;
                string[] headers = csv.GetFieldHeaders();
                //Append data
                var item1 = DataAppend.ReadPrevData();
                using (CsvFileWriter writer = new CsvFileWriter(new FileStream(@"../../../CSV_DATA/Outputfile.csv", FileMode.Open)))
                {
                    while (csv.ReadNextRecord())
                    {
                        // The test setup.
                        

                        ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;


                        //set up data based on transaction
                        var transactionAmount = Convert.ToDecimal(25.60);
                        var opaqueDataType = new opaqueDataType
                        {
                            dataDescriptor = "COMMON.VCO.ONLINE.PAYMENT",
                            dataKey = "NQzcMISSxLX789w+CGX+tXi3lKntO1dpZbZaREOUprVRByJkg1xnpc2Wx9aT5/BLOxQmHqmIsjjy+tF6HqKKGwovvXjIS3fE3y3tBRNbz8D7y6vYMup+AWbEvZqDEBSi",
                            dataValue = "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"
                        };

                        //standard api call to retrieve response
                        var paymentType = new paymentType { Item = opaqueDataType };
                        var transactionRequest = new transactionRequestType
                        {
                            transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),
                            payment = paymentType,
                            amount = transactionAmount,
                            callId = "1482912778237697701"
                        };
                        string apiLogin = null;
                        string transactionKey = null;
                        string TestcaseID = null;
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
                                //Append Result                                
                                foreach (var item in item1)
                                    writer.WriteRow(item);
                            }

                        var request = new createTransactionRequest { transactionRequest = transactionRequest };
                        var controller = new createTransactionController(request);
                        controller.Execute();
                        var response = controller.GetApiResponse();

                            //validate
                            //if (response != null)
                            //{
                            if (response != null && response.messages.resultCode == messageTypeEnum.Ok
                                && response.transactionResponse.messages != null)
                            {
                                /*****************************/
                                try
                                {
                                    //Assert.AreEqual(response.Id, customerProfileId);
                                    //Console.WriteLine("Assertion Succeed! Valid customerProfileId fetched.");
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("CVCT_00" + flag.ToString());
                                    row1.Add("CreateVisaCheckoutTransaction");
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
                                    row1.Add("CVCT_00" + flag.ToString());
                                    row1.Add("CreateVisaCheckoutTransaction");
                                    row1.Add("Fail");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //Console.WriteLine("Assertion Failed! Invalid CustomerPaymentProfile fetched.");
                                    flag = flag + 1;
                                }
                            }
                            /*******************/
                            else
                            {
                                Console.WriteLine("Failed Transaction.");
                                CsvRow row2 = new CsvRow();
                                row2.Add("CVCT_00" + flag.ToString());
                                row2.Add("CreateVisaCheckoutTransaction");
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
                            //}
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
                        //}
                        //else
                        //{
                        //    Console.WriteLine("Null Response.");
                        //}

                            //return response;
                        }

                        //return response;
                        catch (Exception e)
                        {
                            CsvRow row2 = new CsvRow();
                            row2.Add("CVCT_00" + flag.ToString());
                            row2.Add("CreateVisaCheckoutTransaction");
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