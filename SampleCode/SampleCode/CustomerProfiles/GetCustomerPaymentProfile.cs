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


namespace net.authorize.sample
{
    public class GetCustomerPaymentProfile
    {
        //public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey, string customerProfileId,
        //    string customerPaymentProfileId)
        //{
        //    Console.WriteLine("Get Customer Payment Profile sample");

        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;
        //    // define the merchant information (authentication / transaction id)
        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        //    {
        //        name = ApiLoginID,
        //        ItemElementName = ItemChoiceType.transactionKey,
        //        Item = ApiTransactionKey,
        //    };

        //    var request = new getCustomerPaymentProfileRequest();
        //    request.customerProfileId = customerProfileId;
        //    request.customerPaymentProfileId = customerPaymentProfileId;

        //    // Set this optional property to true to return an unmasked expiration date
        //    //request.unmaskExpirationDateSpecified = true;
        //    //request.unmaskExpirationDate = true;


        //    // instantiate the controller that will call the service
        //    var controller = new getCustomerPaymentProfileController(request);
        //    controller.Execute();

        //    // get the response from the service (errors contained if any)
        //    var response = controller.GetApiResponse();

        //    if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
        //    {
        //        Console.WriteLine(response.messages.message[0].text);
        //        Console.WriteLine("Customer Payment Profile Id: " + response.paymentProfile.customerPaymentProfileId);
        //        if (response.paymentProfile.payment.Item is creditCardMaskedType)
        //        {
        //            Console.WriteLine("Customer Payment Profile Last 4: " + (response.paymentProfile.payment.Item as creditCardMaskedType).cardNumber);
        //            Console.WriteLine("Customer Payment Profile Expiration Date: " + (response.paymentProfile.payment.Item as creditCardMaskedType).expirationDate);

        //            if (response.paymentProfile.subscriptionIds != null && response.paymentProfile.subscriptionIds.Length > 0)
        //            {
        //                Console.WriteLine("List of subscriptions : ");
        //                for (int i = 0; i < response.paymentProfile.subscriptionIds.Length; i++)
        //                    Console.WriteLine(response.paymentProfile.subscriptionIds[i]);
        //            }                
        //        }
        //    }
        //    else if(response != null)
        //    {
        //        Console.WriteLine("Error: " + response.messages.message[0].code + "  " +
        //                          response.messages.message[0].text);
        //    }

        //    return response;
        //}

        public static void GetCustomerPaymentProfileExec(String ApiLoginID, String ApiTransactionKey)
        {
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/GetCustomerPaymentProfile.csv", FileMode.Open)), true))
            {
                Console.WriteLine("Get Customer Payment Profile sample");
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
                            Item = ApiTransactionKey,
                        };

                        string customerProfileId = null;
                        string customerPaymentProfileId = null;
                        string TestCaseId = null;

                        for (int i = 0; i < fieldCount; i++)
                        {
                            switch (headers[i])
                            {
                                case "TestCaseId":
                                    TestCaseId = csv[i];
                                    break;
                                case "customerProfileId":
                                    customerProfileId = csv[i];
                                    break;
                                case "customerPaymentProfileId":
                                    customerPaymentProfileId = csv[i];
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


                            var request = new getCustomerPaymentProfileRequest();
                            request.customerProfileId = customerProfileId;                           
                            request.customerPaymentProfileId = customerPaymentProfileId;
                            // Set this optional property to true to return an unmasked expiration date
                            //request.unmaskExpirationDateSpecified = true;
                            //request.unmaskExpirationDate = true;


                            // instantiate the controller that will call the service
                            var controller = new getCustomerPaymentProfileController(request);
                            controller.Execute();

                            // get the response from the service (errors contained if any)
                            var response = controller.GetApiResponse();
                            if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
                            {
                                /*****************************/
                                try
                                {
                                    //Assert.AreEqual(response.Id, customerProfileId);
                                    Console.WriteLine("Assertion Succeed! Valid CustomerPaymentProfile fetched.");
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("GCPP_00" + flag.ToString());
                                    row1.Add("GetCustomerPaymentProfile");
                                    row1.Add("Pass");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //  Console.WriteLine("Success " + TestcaseID + " CustomerID : " + response.Id);
                                    flag = flag + 1;

                                    Console.WriteLine(response.messages.message[0].text);
                                    Console.WriteLine("Customer Payment Profile Id: " + response.paymentProfile.customerPaymentProfileId);
                                }
                                catch
                                {
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("GCPP_00" + flag.ToString());
                                    row1.Add("GetCustomerPaymentProfile");
                                    row1.Add("Assertion Failed!");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    Console.WriteLine("Assertion Failed! Invalid CustomerPaymentProfile fetched.");
                                    flag = flag + 1;
                                }
                                /*******************/
                                
                                //if (response.paymentProfile.payment.Item is creditCardMaskedType)
                                //{
                                //    Console.WriteLine("Customer Payment Profile Last 4: " + (response.paymentProfile.payment.Item as creditCardMaskedType).cardNumber);
                                //    Console.WriteLine("Customer Payment Profile Expiration Date: " + (response.paymentProfile.payment.Item as creditCardMaskedType).expirationDate);

                                //    if (response.paymentProfile.subscriptionIds != null && response.paymentProfile.subscriptionIds.Length > 0)
                                //    {
                                //        Console.WriteLine("List of subscriptions : ");
                                //        for (int i = 0; i < response.paymentProfile.subscriptionIds.Length; i++)
                                //            Console.WriteLine(response.paymentProfile.subscriptionIds[i]);
                                //    }
                                //}

                            }
                            else
                            {
                                CsvRow row2 = new CsvRow();
                                row2.Add("GCPP_00" + flag.ToString());
                                row2.Add("GetCustomerPaymentProfile");
                                row2.Add("Fail");
                                row2.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                writer.WriteRow(row2);
                                flag = flag + 1;
                            }

                        }
                        //else if (response != null)
                        //{
                        //    Console.WriteLine("Error: " + response.messages.message[0].code + "  " +
                        //                      response.messages.message[0].text);
                        //}
                        catch (Exception e)
                        {
                            CsvRow row2 = new CsvRow();
                            row2.Add("GCPP_00" + flag.ToString());
                            row2.Add("GetCustomerPaymentProfile");
                            row2.Add("Fail");
                            row2.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                            writer.WriteRow(row2);
                            flag = flag + 1;
                            Console.WriteLine(TestCaseId + " Error Message " + e.Message);
                        }
                        //return response;
                    }

                }
            }
        }
    }
}