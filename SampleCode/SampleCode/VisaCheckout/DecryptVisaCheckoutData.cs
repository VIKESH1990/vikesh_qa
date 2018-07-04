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
    public class DecryptVisaCheckoutData
    {
        //public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey)
        //{
        //    Console.WriteLine("Running VisaCheckoutDecrypt Sample ...");

        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        //    {
        //        name = ApiLoginID,
        //        ItemElementName = ItemChoiceType.transactionKey,
        //        Item = ApiTransactionKey,
        //    };

        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;

        //    //create a transaction
        //    var opaqueDataType = new opaqueDataType
        //    {
        //        dataDescriptor = "COMMON.VCO.ONLINE.PAYMENT",
        //        dataKey = "foFBEbhXljevQQasx5Q87hzj57xvUl4iBmXDdB1vs/Lm/M1uKJiF9V5QxI0A6NvAtIckMvutSl0Chz2SNoSeBuTRzK0y4IlfnfWKnJF7a1LV/bjZokTtFKINdZ+Ks9RB",
        //        dataValue = "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"
        //    };

        //    var decryptPaymentDataRequest = new decryptPaymentDataRequest()
        //    {
        //        opaqueData = opaqueDataType,
        //        callId = "1166739390571781401"
        //    };
        //    //create controller, execute and get response
        //    var decryptPaymentDataController = new decryptPaymentDataController(decryptPaymentDataRequest);
        //    decryptPaymentDataController.Execute();
        //    var decryptPaymentDataResponse = decryptPaymentDataController.GetApiResponse();

        //    if (decryptPaymentDataResponse != null)
        //    {
        //        //validate response
        //        Console.WriteLine("Result : "+decryptPaymentDataResponse.messages.message);
        //        Console.WriteLine("       : "+decryptPaymentDataResponse.messages.resultCode);
        //        Console.WriteLine("First Name : "+decryptPaymentDataResponse.billingInfo.firstName);
        //        Console.WriteLine("Last name  : "+decryptPaymentDataResponse.billingInfo.lastName);
        //        Console.WriteLine("Card Number : "+decryptPaymentDataResponse.cardInfo.cardNumber);
        //        Console.WriteLine("Amount : "+decryptPaymentDataResponse.paymentDetails.amount);
        //    }

        //    return decryptPaymentDataResponse;

        //}

        public static void DecryptVisaCheckoutDataExec(String ApiLoginID, String ApiTransactionKey)
        {
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/DecryptVisaCheckoutData.csv", FileMode.Open)), true))
            {
                Console.WriteLine("Running VisaCheckoutDecrypt Sample ...");

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

                        //create a transaction
                        var opaqueDataType = new opaqueDataType
                        {
                            dataDescriptor = "COMMON.VCO.ONLINE.PAYMENT",
                            dataKey = "foFBEbhXljevQQasx5Q87hzj57xvUl4iBmXDdB1vs/Lm/M1uKJiF9V5QxI0A6NvAtIckMvutSl0Chz2SNoSeBuTRzK0y4IlfnfWKnJF7a1LV/bjZokTtFKINdZ+Ks9RB",
                            dataValue = "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"
                        };

                        var decryptPaymentDataRequest = new decryptPaymentDataRequest()
                        {
                            opaqueData = opaqueDataType,
                            callId = "1166739390571781401"
                        };
                        //create controller, execute and get response
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

                            var decryptPaymentDataController = new decryptPaymentDataController(decryptPaymentDataRequest);
                            decryptPaymentDataController.Execute();
                            var decryptPaymentDataResponse = decryptPaymentDataController.GetApiResponse();

                            if (decryptPaymentDataResponse != null)
                            {
                                /*****************************/
                                try
                                {
                                    //Assert.AreEqual(response.Id, customerProfileId);
                                    //Console.WriteLine("Assertion Succeed! Valid customerProfileId fetched.");
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("DVCD_00" + flag.ToString());
                                    row1.Add("DecryptVisaCheckoutData");
                                    row1.Add("Pass");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //  Console.WriteLine("Success " + TestcaseID + " CustomerID : " + response.Id);
                                    flag = flag + 1;

                                    //validate response
                                    Console.WriteLine("Result : " + decryptPaymentDataResponse.messages.message);
                                    Console.WriteLine("       : " + decryptPaymentDataResponse.messages.resultCode);
                                    Console.WriteLine("First Name : " + decryptPaymentDataResponse.billingInfo.firstName);
                                    Console.WriteLine("Last name  : " + decryptPaymentDataResponse.billingInfo.lastName);
                                    Console.WriteLine("Card Number : " + decryptPaymentDataResponse.cardInfo.cardNumber);
                                    Console.WriteLine("Amount : " + decryptPaymentDataResponse.paymentDetails.amount);
                                }
                                catch
                                {
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("DVCD_00" + flag.ToString());
                                    row1.Add("DecryptVisaCheckoutData");
                                    row1.Add("Fail");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //Console.WriteLine("Assertion Failed! Invalid CustomerPaymentProfile fetched.");
                                    flag = flag + 1;
                                }
                                /*******************/
                                
                            }
                            else
                            {
                                CsvRow row1 = new CsvRow();
                                row1.Add("DVCD_00" + flag.ToString());
                                row1.Add("DecryptVisaCheckoutData");
                                row1.Add("Fail");
                                row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                writer.WriteRow(row1);
                                //Console.WriteLine("Assertion Failed! Invalid CustomerPaymentProfile fetched.");
                                flag = flag + 1;
                            }

                            //return decryptPaymentDataResponse;
                        }
                        catch (Exception e)
                        {
                            CsvRow row2 = new CsvRow();
                            row2.Add("DVCD_00" + flag.ToString());
                            row2.Add("DecryptVisaCheckoutData");
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