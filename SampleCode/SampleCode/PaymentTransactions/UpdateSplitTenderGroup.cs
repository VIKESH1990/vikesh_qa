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

namespace net.authorize.sample.PaymentTransactions
{
    public class UpdateSplitTenderGroup
    {
        //public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey)
        //{
        //    Console.WriteLine("Update Split Tender Group Sample");

        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;

        //    // define the merchant information (authentication / transaction id)
        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        //    {
        //        name = ApiLoginID,
        //        ItemElementName = ItemChoiceType.transactionKey,
        //        Item = ApiTransactionKey,
        //    };

        //    //provide a split tender Id
        //    //To get a split Tender ID in sandbox, authorize any transaction with amount = 462.25 [if card present] and set allowPartialAuth = true
        //    var splitTenderId = "115901";

        //    //Void or Complete the partial Authorization
        //    var splitTenderStatus = splitTenderStatusEnum.voided;

        //    var request = new updateSplitTenderGroupRequest { splitTenderId = splitTenderId, splitTenderStatus = splitTenderStatus };

        //    // instantiate the contoller that will call the service
        //    var controller = new updateSplitTenderGroupController(request);
        //    controller.Execute();
        //    var response = controller.GetApiResponse();

        //    //validate
        //    if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
        //    {
        //        Console.WriteLine("Successfully Updated ... ");
        //    }
        //    else if(response != null )
        //    {
        //        Console.WriteLine("Error : " + response.messages.message[0].code + "  " + response.messages.message[0].text);
        //    }

        //    return response;
        //}
        public static void UpdateSplitTenderGroupExec(String ApiLoginID, String ApiTransactionKey)
        {
            
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/UpdateSplitTenderGroup.csv", FileMode.Open)), true))
            {
                Console.WriteLine("Update Split Tender Group Sample");
                int flag = 0;
                int fieldCount = csv.FieldCount;
                string[] headers = csv.GetFieldHeaders();
                //Append Data
                var item1 = DataAppend.ReadPrevData();
                using (CsvFileWriter writer = new CsvFileWriter(new FileStream(@"../../../CSV_DATA/Outputfile.csv", FileMode.Open)))
                {
                                        
                    while (csv.ReadNextRecord())
                    {
                        string apiLogin = null;
                        string transactionKey = null;

                        string splitTenderId = null;
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
                                case "splitTenderId":
                                    splitTenderId = csv[i];
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
                        
                        var splitTenderStatus = splitTenderStatusEnum.voided;
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
                            var request = new updateSplitTenderGroupRequest { splitTenderId = splitTenderId, splitTenderStatus = splitTenderStatus };

                            var controller = new updateSplitTenderGroupController(request);
                            controller.Execute();
                            var response = controller.GetApiResponse();


                            // get the response from the service (errors contained if any)
                            
                            if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
                            {
                                try
                                {
                                    //Assert.AreEqual(response.Id, customerProfileId);
                                    Console.WriteLine("Assertion Succeed! Valid CustomerId fetched.");
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("USTC_00" + flag.ToString());
                                    row1.Add("UpdateSplitTenderCustomer");
                                    row1.Add("Pass");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //  Console.WriteLine("Success " + TestcaseID + " CustomerID : " + response.Id);
                                    flag = flag + 1;
                                    Console.WriteLine("Successfully Updated ... ");
                                }
                                catch
                                {
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("USTC_00" + flag.ToString());
                                    row1.Add("UpdateSplitTenderCustomer");
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
                                row1.Add("USTC_00" + flag.ToString());
                                row1.Add("UpdateSplitTenderCustomer");
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
                            row2.Add("USTC_00" + flag.ToString());
                            row2.Add("UpdateSplitTenderCustomer");
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
