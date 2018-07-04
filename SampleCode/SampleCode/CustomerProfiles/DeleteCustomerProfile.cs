using System;
using System.Collections.Generic;
using AuthorizeNET.Api.Controllers;
using AuthorizeNET.Api.Contracts.V1;
using AuthorizeNET.Api.Controllers.Bases;
using LumenWorks.Framework.IO.Csv;
using System.IO;
using NUnit.Framework;
using System.Diagnostics;

namespace net.authorize.sample
{
    public class DeleteCustomerProfile
    {
        //public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey, string customerProfileId)
        ////{
        ////    Console.WriteLine("DeleteCustomerProfile Sample");
        ////    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;
        ////    ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        ////    {
        ////        name = ApiLoginID,
        ////        ItemElementName = ItemChoiceType.transactionKey,
        ////        Item = ApiTransactionKey,
        ////    };

        ////    please update the subscriptionId according to your sandbox credentials
        ////    var request = new deleteCustomerProfileRequest
        ////    {
        ////        customerProfileId = customerProfileId
        ////    };

        ////    Prepare Request
        ////    var controller = new deleteCustomerProfileController(request);
        ////    controller.Execute();

        ////    Send Request to EndPoint
        ////    deleteCustomerProfileResponse response = controller.GetApiResponse();
        ////    if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
        ////    {
        ////        if (response != null && response.messages.message != null)
        ////        {
        ////            Console.WriteLine("Success, ResultCode : " + response.messages.resultCode.ToString());
        ////        }
        ////    }
        ////    else if (response != null && response.messages.message != null)
        ////    {
        ////        Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
        ////    }

        ////    return response;
        ////}


        public static void DeleteCustomerProfileExec(String ApiLoginID, String ApiTransactionKey)
        {

            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/DeleteCustomerProfile.csv", FileMode.Open)), true))
            {
                Console.WriteLine("DeleteCustomerProfile Sample");
                int flag = 0;
                int fieldCount = csv.FieldCount;
                string[] headers = csv.GetFieldHeaders();
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

                            

                            var request = new deleteCustomerProfileRequest();
                            request.customerProfileId = customerProfileId;

                            // instantiate the controller that will call the service
                            var controller = new deleteCustomerProfileController(request);
                            controller.Execute();
                           deleteCustomerProfileResponse response = controller.GetApiResponse();

                            if (response != null && response.messages.resultCode == messageTypeEnum.Ok
                                && response.messages.message != null)
                            {
                                try
                                {
                                    //Assert.AreEqual(response.Id, customerProfileId);
                                    //Console.WriteLine("Assertion Succeed! Valid CustomerId fetched.");
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("DCP_00" + flag.ToString());
                                    row1.Add("DeleteCustomerProfile");
                                    row1.Add("Pass");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //  Console.WriteLine("Success " + TestcaseID + " CustomerID : " + response.Id);
                                    flag = flag + 1;
                                    Console.WriteLine("Success, ResultCode : " + response.messages.resultCode.ToString());
                                }
                                catch
                                {
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("DCP_00" + flag.ToString());
                                    row1.Add("DeleteCustomerProfile");
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
                                row1.Add("DCP_00" + flag.ToString());
                                row1.Add("DeleteCustomerProfile");
                                row1.Add("Assertion Failed!");
                                row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                writer.WriteRow(row1);
                                //Console.WriteLine("Assertion Failed! Invalid CustomerId fetched.");
                                flag = flag + 1;
                            }
                        }
                        catch (Exception e)
                        {
                            CsvRow row2 = new CsvRow();
                            row2.Add("DCP_00" + flag.ToString());
                            row2.Add("DeleteCustomerProfile");
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
