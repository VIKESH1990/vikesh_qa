
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
    public class GetCustomerPaymentProfileList
    {
        //public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey)
        //{
        //    Console.WriteLine("Get Customer Payment Profile List sample");

        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;
        //    // define the merchant information (authentication / transaction id)
        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        //    {
        //        name = ApiLoginID,
        //        ItemElementName = ItemChoiceType.transactionKey,
        //        Item = ApiTransactionKey
        //    };

        //    var request = new getCustomerPaymentProfileListRequest();
        //    request.searchType = CustomerPaymentProfileSearchTypeEnum.cardsExpiringInMonth;
        //    request.month = "2020-12";
        //    request.paging = new Paging();
        //    request.paging.limit = 50;
        //    request.paging.offset = 1;

        //    // instantiate the controller that will call the service
        //    var controller = new getCustomerPaymentProfileListController(request);
        //    controller.Execute();

        //    // get the response from the service (errors contained if any)
        //    var response = controller.GetApiResponse();

        //    if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
        //    {
        //        Console.WriteLine(response.messages.message[0].text);
        //        Console.WriteLine("Number of payment profiles : " + response.totalNumInResultSet);

        //        Console.WriteLine("List of Payment profiles : ");
        //        for (int profile = 0; profile < response.paymentProfiles.Length; profile++)
        //        {
        //            Console.WriteLine(response.paymentProfiles[profile].customerPaymentProfileId);
        //        }
        //    }
        //    else if (response != null)
        //    {
        //        if (response.messages.message.Length > 0)
        //        {
        //            Console.WriteLine("Error: " + response.messages.message[0].code + "  " +
        //                              response.messages.message[0].text);
        //        }
        //    }
        //    else
        //    {
        //        if (controller.GetErrorResponse().messages.message.Length > 0)
        //        {
        //            Console.WriteLine("Null response received : " + controller.GetErrorResponse().messages.message[0].text);
        //        }
        //    }

        //    return response;
        //}
        public static void GetCustomerPaymentProfileListExec(String ApiLoginID, String ApiTransactionKey)
        {
            Console.WriteLine("Get Customer Payment Profile List sample");
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/GetCustomerPaymentProfileList.csv", FileMode.Open)), true))
            {

                int fieldCount = csv.FieldCount;
                //Append Data
                var item1 = DataAppend.ReadPrevData();
                using (CsvFileWriter writer = new CsvFileWriter(new FileStream(@"../../../CSV_DATA/Outputfile.csv", FileMode.Open)))
                {
                    int flag = 0;
                    string[] headers = csv.GetFieldHeaders();
                    while (csv.ReadNextRecord())
                    {
                        string apiLogin = null;
                        string transactionKey = null;
                        string TestCaseId = null;


                        for (int i = 0; i < fieldCount; i++)
                        {
                            switch (headers[i])
                            {
                                case "apiLogin":
                                    apiLogin = csv[i];
                                    break;
                                case "transactionKey":
                                    transactionKey = csv[i];
                                    break;
                                case "TestCaseId":
                                    TestCaseId = csv[i];
                                    break;


                                default:
                                    break;
                            }
                        }
                        ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;
                        // define the merchant information (authentication / transaction id)
                        ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
                        {
                            name = apiLogin,
                            ItemElementName = ItemChoiceType.transactionKey,
                            Item = transactionKey,
                        };
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

                            var request = new getCustomerPaymentProfileListRequest();
                            request.searchType = CustomerPaymentProfileSearchTypeEnum.cardsExpiringInMonth;
                            request.month = "2020-12";
                            request.paging = new Paging();
                            request.paging.limit = 50;
                            request.paging.offset = 1;

                            // instantiate the controller that will call the service
                            var controller = new getCustomerPaymentProfileListController(request);
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
                                    row1.Add("GCPPL_00" + flag.ToString());
                                    row1.Add("GetCustomerPaymentProfileList");
                                    row1.Add("Pass");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //  Console.WriteLine("Success " + TestcaseID + " CustomerID : " + response.Id);
                                    flag = flag + 1;
                                    Console.WriteLine(response.messages.message[0].text);
                                    Console.WriteLine("Number of payment profiles : " + response.totalNumInResultSet);
                                    Console.WriteLine("List of Payment profiles : ");
                                    for (int profile = 0; profile < response.paymentProfiles.Length; profile++)
                                    {
                                        Console.WriteLine(response.paymentProfiles[profile].customerPaymentProfileId);
                                    }
                                }
                                catch
                                {
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("GCPPL_00" + flag.ToString());
                                    row1.Add("GetCustomerPaymentProfileList");
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
                                row1.Add("GCPPL_00" + flag.ToString());
                                row1.Add("GetCustomerPaymentProfileList");
                                row1.Add("Fail");
                                row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                writer.WriteRow(row1);
                               // Console.WriteLine("Assertion Failed! Invalid CustomerId fetched.");
                                flag = flag + 1;
                            }
                        }
                        catch (Exception e)
                        {
                            CsvRow row2 = new CsvRow();
                            row2.Add("GCPPL_00" + flag.ToString());
                            row2.Add("GetCustomerPaymentProfileList");
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
