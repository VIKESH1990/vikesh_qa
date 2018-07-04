using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizeNET.Api.Contracts.V1;
using AuthorizeNET.Api.Controllers;
using AuthorizeNET.Api.Controllers.Bases;
using LumenWorks.Framework.IO.Csv;
using System.IO;
using NUnit.Framework;
using System.Diagnostics;

namespace net.authorize.sample
{
    public class UpdateCustomerProfile
    {
        //public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey, string customerProfileId)
        //{
        //    Console.WriteLine("Update customer profile sample");

        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;
        //    // define the merchant information (authentication / transaction id)
        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        //    {
        //        name = ApiLoginID,
        //        ItemElementName = ItemChoiceType.transactionKey,
        //        Item = ApiTransactionKey,
        //    };

        //    var profile = new customerProfileExType
        //    {
        //        merchantCustomerId = "custId123",
        //        description = "some description",
        //        email = "newaddress@example.com",
        //        customerProfileId = customerProfileId
        //    };

        //    var request = new updateCustomerProfileRequest();
        //    request.profile = profile;

        //    // instantiate the controller that will call the service
        //    var controller = new updateCustomerProfileController(request);
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

        public static void UpdateCustomerProfileExec(String ApiLoginID, String ApiTransactionKey)
        {
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/UpdateCustomerProfile.csv", FileMode.Open)), true))
            {
                Console.WriteLine("Update customer profile sample");
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
                            Item = ApiTransactionKey,
                        };
                        //CustomersApi instance = new CustomersApi(EnvironmentSet.Sandbox);
                        // Customer Response Object           
                        // Customer response = null;
                        //initialization



                        string TestcaseID = null;
                        string customerProfileId = null;
                        string description = null;
                        string email = null;
                       

                        for (int i = 0; i < fieldCount; i++)
                        {
                            switch (headers[i])
                            {

                                case "TestcaseID":
                                    TestcaseID = csv[i];
                                    break;
                                case "customerProfileId":
                                    customerProfileId = csv[i];
                                    break;
                                case "description":
                                    description = csv[i];
                                    break;
                                case "email":
                                    email = csv[i];
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

                            var profile = new customerProfileExType
                            {
                                merchantCustomerId = "custId123",
                                description = description,
                                email = email,
                                customerProfileId = customerProfileId
                            };

                            var request = new updateCustomerProfileRequest();
                            request.profile = profile;
                            

                            // instantiate the controller that will call the service
                            var controller = new updateCustomerProfileController(request);
                            controller.Execute();
                            updateCustomerProfileResponse response = controller.GetApiResponse();

                            if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
                            {
                                try
                                {
                                    //Assert.AreEqual(response.Id, customerProfileId);
                                    //Console.WriteLine("Assertion Succeed! Valid CustomerId fetched.");
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("UCP_00" + flag.ToString());
                                    row1.Add("UpdateCustomerProfile");
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
                                    row1.Add("UCP_00" + flag.ToString());
                                    row1.Add("UpdateCustomerProfile");
                                    row1.Add("Assertion Failed!");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //Console.WriteLine("Assertion Failed! Invalid CustomerId fetched.");
                                    flag = flag + 1;
                                }
                            }
                            else
                            {
                                CsvRow row1 = new CsvRow();
                                row1.Add("UCP_00" + flag.ToString());
                                row1.Add("UpdateCustomerProfile");
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
                            row2.Add("UCP_00" + flag.ToString());
                            row2.Add("UpdateCustomerProfile");
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
