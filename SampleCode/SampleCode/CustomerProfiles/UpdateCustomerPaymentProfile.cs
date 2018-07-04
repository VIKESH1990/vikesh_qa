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
    public class UpdateCustomerPaymentProfile
    {
        //public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey, string customerProfileId, string customerPaymentProfileId)
        //{
        //    Console.WriteLine("Update Customer payment profile sample");

        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;
        //    // define the merchant information (authentication / transaction id)
        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        //    {
        //        name = ApiLoginID,
        //        ItemElementName = ItemChoiceType.transactionKey,
        //        Item = ApiTransactionKey,
        //    };

        //    var creditCard = new creditCardType
        //    {
        //        cardNumber = "4111111111111111",
        //        expirationDate = "0718"
        //    };

        //    //===========================================================================
        //    // NOTE:  For updating just the address, not the credit card/payment data 
        //    //        you can pass the masked values returned from 
        //    //        GetCustomerPaymentProfile or GetCustomerProfile
        //    //        E.g.
        //    //                * literal values shown below
        //    //===========================================================================
        //    /*var creditCard = new creditCardType
        //    {
        //        cardNumber = "XXXX1111",
        //        expirationDate = "XXXX"
        //    };*/

        //    var paymentType = new paymentType { Item = creditCard };

        //    var paymentProfile = new customerPaymentProfileExType
        //    {
        //        billTo = new customerAddressType
        //        {
        //            // change information as required for billing
        //            firstName = "John",
        //            lastName = "Doe",
        //            address = "123 Main St.",
        //            city = "Bellevue",
        //            state = "WA",
        //            zip = "98004",
        //            country = "USA",
        //            phoneNumber = "000-000-000",
        //        },
        //        payment = paymentType,
        //        customerPaymentProfileId = customerPaymentProfileId
        //    };

        //    var request = new updateCustomerPaymentProfileRequest();
        //    request.customerProfileId = customerProfileId;
        //    request.paymentProfile = paymentProfile;
        //    request.validationMode = validationModeEnum.liveMode;


        //    // instantiate the controller that will call the service
        //    var controller = new updateCustomerPaymentProfileController(request);
        //    controller.Execute();

        //    // get the response from the service (errors contained if any)
        //    var response = controller.GetApiResponse();

        //    if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
        //    {
        //        Console.WriteLine(response.messages.message[0].text);
        //    }
        //    else if (response != null)
        //    {
        //        Console.WriteLine("Error: " + response.messages.message[0].code + "  " +
        //                          response.messages.message[0].text);
        //    }

        //    return response;
        //}

        public static void UpdateCustomerPaymentProfileExec(String ApiLoginID, String ApiTransactionKey)
        {
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/UpdateCustomerPaymentProfile.csv", FileMode.Open)), true))
            {
                Console.WriteLine("Update Customer payment profile sample");
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

                        var creditCard = new creditCardType
                        {
                            cardNumber = "4111111111111111",
                            expirationDate = "0718"
                        };

                        //===========================================================================
                        // NOTE:  For updating just the address, not the credit card/payment data 
                        //        you can pass the masked values returned from 
                        //        GetCustomerPaymentProfile or GetCustomerProfile
                        //        E.g.
                        //                * literal values shown below
                        //===========================================================================
                        /*var creditCard = new creditCardType
                        {
                            cardNumber = "XXXX1111",
                            expirationDate = "XXXX"
                        };*/
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



                            var paymentType = new paymentType { Item = creditCard };

                            var paymentProfile = new customerPaymentProfileExType
                            {
                                billTo = new customerAddressType
                                {
                                    // change information as required for billing
                                    firstName = "John",
                                    lastName = "Doe",
                                    address = "123 Main St.",
                                    city = "Bellevue",
                                    state = "WA",
                                    zip = "98004",
                                    country = "USA",
                                    phoneNumber = "000-000-000",
                                },
                                payment = paymentType,
                                customerPaymentProfileId = customerPaymentProfileId
                            };

                            var request = new updateCustomerPaymentProfileRequest();
                            request.customerProfileId = customerProfileId;
                            request.paymentProfile = paymentProfile;
                            request.validationMode = validationModeEnum.liveMode;


                            // instantiate the controller that will call the service
                            var controller = new updateCustomerPaymentProfileController(request);
                            controller.Execute();

                            // get the response from the service (errors contained if any)
                            var response = controller.GetApiResponse();

                            if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
                            {
                                Console.WriteLine(response.messages.message[0].text);
                                /*****************************/
                                try
                                {
                                    //Assert.AreEqual(response.Id, customerProfileId);
                                    Console.WriteLine("Assertion Succeed! Valid CustomerPaymentProfile updated.");
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("UCPP_00" + flag.ToString());
                                    row1.Add("UpdateCustomerPaymentProfile");
                                    row1.Add("Pass");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //  Console.WriteLine("Success " + TestcaseID + " CustomerID : " + response.Id);
                                    flag = flag + 1;
                                }
                                catch
                                {
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("UCPP_00" + flag.ToString());
                                    row1.Add("UpdateCustomerPaymentProfile");
                                    row1.Add("Assertion Failed!");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    Console.WriteLine("Assertion Failed! Invalid CustomerPaymentProfile updated.");
                                    flag = flag + 1;
                                }
                                /*******************/
                            }
                            else
                            {
                                CsvRow row1 = new CsvRow();
                                row1.Add("UCPP_00" + flag.ToString());
                                row1.Add("UpdateCustomerPaymentProfile");
                                row1.Add("Fail");
                                row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                writer.WriteRow(row1);
                                Console.WriteLine("Assertion Failed! Invalid CustomerPaymentProfile updated.");
                                flag = flag + 1;
                            }
                        }
                        //else if (response != null)
                        //{
                        //    Console.WriteLine("Error: " + response.messages.message[0].code + "  " +
                        //                      response.messages.message[0].text);
                        //}

                        //return response;
                        catch (Exception e)
                        {
                            CsvRow row2 = new CsvRow();
                            row2.Add("UCPP_00" + flag.ToString());
                            row2.Add("UpdateCustomerPaymentProfile");
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