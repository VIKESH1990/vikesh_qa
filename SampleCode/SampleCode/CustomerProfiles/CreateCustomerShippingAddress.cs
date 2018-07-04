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
    public class CreateCustomerShippingAddress
    {
        //public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey, string customerProfileId)
        //{
        //    Console.WriteLine("CreateCustomerShippingAddress Sample");
        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;
        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        //    {
        //        name            = ApiLoginID,
        //        ItemElementName = ItemChoiceType.transactionKey,
        //        Item            = ApiTransactionKey,
        //    };

        //    customerAddressType officeAddress = new customerAddressType();
        //    officeAddress.firstName = "Chris";
        //    officeAddress.lastName = "brown";
        //    officeAddress.address = "1200 148th AVE NE";
        //    officeAddress.city = "NorthBend";
        //    officeAddress.zip = "92101";


        //    var request = new createCustomerShippingAddressRequest
        //    {
        //        customerProfileId = customerProfileId,
        //        address = officeAddress,
        //    };

        //    //Prepare Request
        //    var controller = new createCustomerShippingAddressController(request);
        //    controller.Execute();

        //     //Send Request to EndPoint
        //    createCustomerShippingAddressResponse response = controller.GetApiResponse(); 
        //    if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
        //    {
        //        if (response != null && response.messages.message != null)
        //        {
        //            Console.WriteLine("Success, customerAddressId : " + response.customerAddressId);
        //        }
        //    }
        //    else if(response != null)
        //    {
        //        Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
        //    }

        //    return response;
        //}
        public static void CreateCustomerShippingAddressExec(String ApiLoginID, String ApiTransactionKey)
        {
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/CreateCustomerAddress.csv", FileMode.Open)), true))
            {
                Console.WriteLine("CreateCustomerShippingAddress Sample");
                int flag = 0;
                int fieldCount = csv.FieldCount;
                string[] headers = csv.GetFieldHeaders();
                // Writing to output CSV file
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
                            Item = ApiTransactionKey
                        };

                        string customerId = null;
                        string firstName = null;
                        string lastName = null;
                        string address = null;
                        string city = null;
                        string zip = null;
                        string TestcaseID = null;
                       
                        for (int i = 0; i < fieldCount; i++)
                        {
                            switch (headers[i])
                            {
                                case "customerId":
                                    customerId = csv[i];
                                    break;
                                case "firstName":
                                    firstName = csv[i];
                                    break;
                                case "lastName":
                                    lastName = csv[i];
                                    break;
                                case "address":
                                    address = csv[i];
                                    break;
                                case "city":
                                    city = csv[i];
                                    break;
                                case "zip":
                                    zip = csv[i];
                                    break;
                                case "TestcaseID":
                                    TestcaseID = csv[i];
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
                            customerAddressType officeAddress = new customerAddressType();
                            officeAddress.firstName = firstName;
                            officeAddress.lastName = lastName;
                            officeAddress.address = address;
                            officeAddress.city = city;
                            officeAddress.zip = zip;
                                
                            var request = new createCustomerShippingAddressRequest();
                            request.customerProfileId = customerId;
                            request.address = officeAddress;


                            // instantiate the controller that will call the service
                            var controller = new createCustomerShippingAddressController(request);
                            controller.Execute();

                            // get the response from the service (errors contained if any)
                            var response = controller.GetApiResponse();
                            if (response != null && response.messages.resultCode == messageTypeEnum.Ok
                                 && response.messages.message != null)
                            {
                                try
                                {
                                    //Assert.AreEqual(response.Id, customerProfileId);
                                    //Console.WriteLine("Assertion Succeed! Valid CustomerId fetched.");
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("CCSA_00" + flag.ToString());
                                    row1.Add("CreateCustomerShippingAddress");
                                    row1.Add("Pass");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //  Console.WriteLine("Success " + TestcaseID + " CustomerID : " + response.Id);
                                    flag = flag + 1;
                                    Console.WriteLine("Success, customerAddressId : " + response.customerAddressId);
                                }
                                catch
                                {
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("CCSA_00" + flag.ToString());
                                    row1.Add("CreateCustomerShippingAddress");
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
                                row1.Add("CCSA_00" + flag.ToString());
                                row1.Add("CreateCustomerShippingAddress");
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
                            row2.Add("CCSA_00" + flag.ToString());
                            row2.Add("createCustomerShippingAddress");
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
