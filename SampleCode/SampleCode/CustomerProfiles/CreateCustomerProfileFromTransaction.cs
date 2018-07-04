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
    public class CreateCustomerProfileFromTransaction
    {
        //public static ANetApiResponse Run(string ApiLoginID, string ApiTransactionKey, string transactionId)
        //{
        //    Console.WriteLine("CreateCustomerProfileFromTransaction Sample");

        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;
        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        //    {
        //        name            = ApiLoginID,
        //        ItemElementName = ItemChoiceType.transactionKey,
        //        Item            = ApiTransactionKey,
        //    };

        //    var customerProfile = new customerProfileBaseType
        //    {
        //        merchantCustomerId = "123212",
        //        email = "hello@castleblack.com",
        //        description = "This is a sample customer profile"
        //    };

        //    var request = new createCustomerProfileFromTransactionRequest 
        //    { 
        //        transId = transactionId,
        //        // You can either specify the customer information in form of customerProfileBaseType object
        //        customer = customerProfile
        //        //  OR   
        //        // You can just provide the customer Profile ID
        //        // customerProfileId = "123343"                
        //    };

        //    var controller = new createCustomerProfileFromTransactionController(request); 
        //    controller.Execute();

        //    createCustomerProfileResponse response = controller.GetApiResponse(); 

        //    //validate
        //    if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
        //    {
        //        if (response != null && response.messages.message != null)
        //        {
        //            Console.WriteLine("Success, CustomerProfileID : " + response.customerProfileId);
        //            Console.WriteLine("Success, CustomerPaymentProfileID : " + response.customerPaymentProfileIdList[0]);
        //            Console.WriteLine("Success, CustomerShippingProfileID : " + response.customerShippingAddressIdList[0]);
        //        }
        //    }
        //    else if(response != null)
        //    {
        //        Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
        //    }

        //    return response;
        //}
        public static void CreateCustomerProfileFromTransactionExec(String ApiLoginID, String ApiTransactionKey)
        {
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/CreateACustomerProfileFromATransaction.csv", FileMode.Open)), true))
            {
                Console.WriteLine("CreateCustomerProfileFromTransaction Sample");
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


                        string TestCaseId = null;
                        string CustomerProfileId = null;
                        string TransactionId = null;


                        for (int i = 0; i < fieldCount; i++)
                        {
                            switch (headers[i])
                            {
                                case "CustomerProfileId":
                                    CustomerProfileId = csv[i];
                                    break;
                                case "TransactionId":
                                    TransactionId = csv[i];
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
                            var customerProfile = new customerProfileBaseType
                            {
                                merchantCustomerId = "123212",
                                email = "hello@castleblack.com",
                                description = "This is a sample customer profile"
                            };

                            var request = new createCustomerProfileFromTransactionRequest();
                            request.customerProfileId = CustomerProfileId;
                            request.customer = customerProfile;

                            request.transId = TransactionId;


                            // instantiate the controller that will call the service
                            var controller = new createCustomerProfileFromTransactionController(request);
                            controller.Execute();

                            // get the response from the service (errors contained if any)
                            var response = controller.GetApiResponse();
                            if (response != null && response.messages.resultCode == messageTypeEnum.Ok
                                && response.messages.message != null)
                            {
                                try
                                {
                                    //Assert.AreEqual(response.Id, customerProfileId);
                                    Console.WriteLine("Assertion Succeed! Valid CustomerId fetched.");
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("CACFPT_00" + flag.ToString());
                                    row1.Add("createCustomerFromPaymentTransaction");
                                    row1.Add("Pass");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //  Console.WriteLine("Success " + TestcaseID + " CustomerID : " + response.Id);
                                    flag = flag + 1;
                                    Console.WriteLine("Success, CustomerProfileID : " + response.customerProfileId);
                                    Console.WriteLine("Success, CustomerPaymentProfileID : " + response.customerPaymentProfileIdList[0]);
                                    Console.WriteLine("Success, CustomerShippingProfileID : " + response.customerShippingAddressIdList[0]);
                                }
                                catch
                                {
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("CACFPT_00" + flag.ToString());
                                    row1.Add("createCustomerFromPaymentTransaction");
                                    row1.Add("Fail");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    Console.WriteLine("Assertion Failed! Invalid CustomerId fetched.");
                                    flag = flag + 1;
                                }
                            }
                            else
                            {
                                CsvRow row1 = new CsvRow();
                                row1.Add("CACFPT_00" + flag.ToString());
                                row1.Add("createCustomerFromPaymentTransaction");
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
                            row2.Add("CACFPT_00" + flag.ToString());
                            row2.Add("createCustomerFromPaymentTransaction");
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
