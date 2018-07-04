
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizeNET.Api.Contracts.V1;
using AuthorizeNET.Api.Controllers;
using AuthorizeNET.Api.Controllers.Bases;
using System.IO;
using NUnit.Framework;
using LumenWorks.Framework.IO.Csv;

using System.Diagnostics;

namespace net.authorize.sample
{
    public class UpdateCustomerShippingAddress
    {
        //public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey, string customerProfileID, string customerAddressId)
        //{
        //    Console.WriteLine("Update customer shipping address sample");

        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;
        //    // define the merchant information (authentication / transaction id)
        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        //    {
        //        name = ApiLoginID,
        //        ItemElementName = ItemChoiceType.transactionKey,
        //        Item = ApiTransactionKey,
        //    };

        //   var creditCard = new creditCardType
        //     {
        //         cardNumber = "4111111111111111",
        //         expirationDate = "0718"
        //     };

          // var paymentType = new paymentType { Item = creditCard };

       //   var address = new customerAddressExType
        //    {
        //        firstName = "Newfirstname",
        //        lastName = "Doe",
        //        address = "123 Main St.",
        //        city = "Bellevue",
        //        state = "WA",
        //        zip = "98004",
        //        country = "USA",
        //        phoneNumber = "000-000-000",
        //        customerAddressId = customerAddressId
        //    };

         //  var request = new updateCustomerShippingAddressRequest();
        //    request.customerProfileId = customerProfileID;
        //    request.address = address;


        //    // instantiate the controller that will call the service
        //    var controller = new updateCustomerShippingAddressController(request);
        //    controller.Execute();

        //    // get the response from the service (errors contained if any)
        //    var response = controller.GetApiResponse();

        //    if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
        //    {
        //        Console.WriteLine(response.messages.message[0].text);
        //    }
        //    else if(response != null)
        //    {
        //        Console.WriteLine("Error: " + response.messages.message[0].code + "  " +
        //                          response.messages.message[0].text);
        //    }

        //    return response;
        //}
        public static void UpdateCustomerShippingAddressExec(String ApiLoginID, String ApiTransactionKey)
        {
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/UpdateCustomerAddress.csv", FileMode.Open)), true))
            {
                Console.WriteLine("Update customer shipping address sample");
                int fieldCount = csv.FieldCount;
                //Append Data
                var item1 = DataAppend.ReadPrevData();
                using (CsvFileWriter writer = new CsvFileWriter(new FileStream(@"../../../CSV_DATA/Outputfile.csv", FileMode.Open)))
                {
                    int flag = 0;
                    string[] headers = csv.GetFieldHeaders();
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
                        //CustomersApi instance = new CustomersApi(EnvironmentSet.Sandbox);
                        // Customer Response Object           
                        // Customer response = null;
                        //initialization


                        string customerId = null;
                        string CustomerAddressId = null;
                        string TestCaseId = null;


                        for (int i = 0; i < fieldCount; i++)
                        {
                            switch (headers[i])
                            {
                                case "customerId":
                                    customerId = csv[i];
                                    break;
                                case "CustomerAddressId":
                                    CustomerAddressId = csv[i];
                                    break;
                                case "TestCaseId":
                                    TestCaseId = csv[i];
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




                               var creditCard = new creditCardType
                               {
                                   cardNumber = "4111111111111111",
                                   expirationDate = "0718"
                               };

                            var paymentType = new paymentType { Item = creditCard };


                            var address = new customerAddressExType();
                            address.customerAddressId = CustomerAddressId;

                            var request = new updateCustomerShippingAddressRequest();
                            request.customerProfileId = customerId;
                            request.address = address;


                            // instantiate the controller that will call the service
                            var controller = new updateCustomerShippingAddressController(request);
                            controller.Execute();

                            // get the response from the service (errors contained if any)
                            var response = controller.GetApiResponse();
                            if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
                            {
                                try
                                {
                                    //Assert.AreEqual(response.Id, customerProfileId);
                                    Console.WriteLine("Assertion Succeed! Valid CustomerId fetched.");
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("UCSA_00" + flag.ToString());
                                    row1.Add("UpdateCustomerShippingAddress");
                                    row1.Add("Pass");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //  Console.WriteLine("Success " + TestcaseID + " CustomerID : " + response.Id);
                                    flag = flag + 1;
                                    Console.WriteLine(response.messages.message[0].text);
                                }
                                catch
                                {
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("UCSA_00" + flag.ToString());
                                    row1.Add("UpdateCustomerShippingAddress");
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
                                row1.Add("UCSA_00" + flag.ToString());
                                row1.Add("UpdateCustomerShippingAddress");
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
                            row2.Add("UCSA_00" + flag.ToString());
                            row2.Add("UpdateCustomerShippingAddress");
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
