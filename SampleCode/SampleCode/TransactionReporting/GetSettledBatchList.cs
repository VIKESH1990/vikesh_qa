using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthorizeNET;
using AuthorizeNET.Api.Controllers;
using AuthorizeNET.Api.Contracts.V1;
using AuthorizeNET.Api.Controllers.Bases;
using System.IO;
using LumenWorks.Framework.IO.Csv;

namespace net.authorize.sample
{
    public class GetSettledBatchList
    {
        //public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey)
        //{
        //    Console.WriteLine("Get settled batch list sample");

        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;
        //    // define the merchant information (authentication / transaction id)
        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        //    {
        //        name = ApiLoginID,
        //        ItemElementName = ItemChoiceType.transactionKey,
        //        Item = ApiTransactionKey,
        //    };

        //    //Get a date 1 week in the past
        //    var firstSettlementDate = DateTime.Today.Subtract(TimeSpan.FromDays(31));
        //    //Get today's date
        //    var lastSettlementDate = DateTime.Today;
        //    Console.WriteLine("First settlement date: {0} Last settlement date:{1}", firstSettlementDate,
        //        lastSettlementDate);

        //    var request = new getSettledBatchListRequest();
        //    request.firstSettlementDate = firstSettlementDate;
        //    request.lastSettlementDate = lastSettlementDate;
        //    request.includeStatistics = true;

        //    // instantiate the controller that will call the service
        //    var controller = new getSettledBatchListController(request);
        //    controller.Execute();

        //    // get the response from the service (errors contained if any)
        //    var response = controller.GetApiResponse();


        //    if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
        //    {
        //        if (response.batchList == null)
        //            return response;

        //        foreach (var batch in response.batchList)
        //        {
        //            Console.WriteLine("Batch Id: {0}", batch.batchId);
        //            Console.WriteLine("Batch settled on (UTC): {0}", batch.settlementTimeUTC);
        //            Console.WriteLine("Batch settled on (Local): {0}", batch.settlementTimeLocal);
        //            Console.WriteLine("Batch settlement state: {0}", batch.settlementState);
        //            Console.WriteLine("Batch market type: {0}", batch.marketType);
        //            Console.WriteLine("Batch product: {0}", batch.product);
        //            foreach (var statistics in batch.statistics)
        //            {
        //                Console.WriteLine(
        //                    "Account type: {0} Total charge amount: {1} Charge count: {2} Refund amount: {3} Refund count: {4} Void count: {5} Decline count: {6} Error amount: {7}",
        //                    statistics.accountType, statistics.chargeAmount, statistics.chargeCount,
        //                    statistics.refundAmount, statistics.refundCount,
        //                    statistics.voidCount, statistics.declineCount, statistics.errorCount);
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

        public static void GetSettledBatchListExec(String ApiLoginID, String ApiTransactionKey)
        {
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/GetSettledBatchList.csv", FileMode.Open)), true))
            {
                Console.WriteLine("Get settled batch list sample");

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
                        

                        //Get a date 1 week in the past
                        var firstSettlementDate = DateTime.Today.Subtract(TimeSpan.FromDays(31));
                        //Get today's date
                        var lastSettlementDate = DateTime.Today;
                        Console.WriteLine("First settlement date: {0} Last settlement date:{1}", firstSettlementDate,
                            lastSettlementDate);
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
                        // define the merchant information (authentication / transaction id)
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
                        var request = new getSettledBatchListRequest();
                        request.firstSettlementDate = firstSettlementDate;
                        request.lastSettlementDate = lastSettlementDate;
                        request.includeStatistics = true;

                        // instantiate the controller that will call the service
                        var controller = new getSettledBatchListController(request);
                        controller.Execute();

                        // get the response from the service (errors contained if any)
                        var response = controller.GetApiResponse();


                        if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
                        {
                                /*****************************/
                                try
                                {
                                    //Assert.AreEqual(response.Id, customerProfileId);
                                    //Console.WriteLine("Assertion Succeed! Valid customerProfileId fetched.");
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("GSBL_00" + flag.ToString());
                                    row1.Add("GetSettledBatchList");
                                    row1.Add("Pass");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //  Console.WriteLine("Success " + TestcaseID + " CustomerID : " + response.Id);
                                    flag = flag + 1;

                                    foreach (var batch in response.batchList)
                                    {
                                        Console.WriteLine("Batch Id: {0}", batch.batchId);
                                        Console.WriteLine("Batch settled on (UTC): {0}", batch.settlementTimeUTC);
                                        Console.WriteLine("Batch settled on (Local): {0}", batch.settlementTimeLocal);
                                        Console.WriteLine("Batch settlement state: {0}", batch.settlementState);
                                        Console.WriteLine("Batch market type: {0}", batch.marketType);
                                        Console.WriteLine("Batch product: {0}", batch.product);
                                        foreach (var statistics in batch.statistics)
                                        {
                                            Console.WriteLine(
                                                "Account type: {0} Total charge amount: {1} Charge count: {2} Refund amount: {3} Refund count: {4} Void count: {5} Decline count: {6} Error amount: {7}",
                                                statistics.accountType, statistics.chargeAmount, statistics.chargeCount,
                                                statistics.refundAmount, statistics.refundCount,
                                                statistics.voidCount, statistics.declineCount, statistics.errorCount);
                                        }
                                    }
                                }
                                catch
                                {
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("GSBL_00" + flag.ToString());
                                    row1.Add("GetSettledBatchList");
                                    row1.Add("Assertion Failed!");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //Console.WriteLine("Assertion Failed! Invalid CustomerPaymentProfile fetched.");
                                    flag = flag + 1;
                                }
                                /*******************/
                                //if (response.batchList == null)
                                //return response;

                           
                        }
                        else
                        {
                            //Console.WriteLine("Null response");
                            CsvRow row2 = new CsvRow();
                            row2.Add("GSBL_00" + flag.ToString());
                            row2.Add("GetSettledBatchList");
                            row2.Add("Fail");
                            row2.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                            writer.WriteRow(row2);
                            flag = flag + 1;
                        }
                            //else if (response != null)
                            //{
                            //    Console.WriteLine("Error: " + response.messages.message[0].code + "  " +
                            //                      response.messages.message[0].text);
                            //}

                        }

                        //return response;
                        catch (Exception e)
                        {
                            CsvRow row2 = new CsvRow();
                            row2.Add("GSBL_00" + flag.ToString());
                            row2.Add("GetSettledBatchList");
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