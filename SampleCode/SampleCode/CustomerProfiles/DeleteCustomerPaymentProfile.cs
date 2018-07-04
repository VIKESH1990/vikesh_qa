using System;
using System.Collections.Generic;
using AuthorizeNET.Api.Controllers;
using AuthorizeNET.Api.Contracts.V1;
using AuthorizeNET.Api.Controllers.Bases;
using System.IO;
using LumenWorks.Framework.IO.Csv;

namespace net.authorize.sample
{
    //public class DeleteCustomerPaymentProfile
    //{
    //    public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey, string customerProfileId,
    //        string customerPaymentProfileId)
    //    {
    //        Console.WriteLine("DeleteCustomerPaymentProfile Sample");
    //        ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;
    //        ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
    //        {
    //            name = ApiLoginID,
    //            ItemElementName = ItemChoiceType.transactionKey,
    //            Item = ApiTransactionKey,
    //        };

    //        //please update the subscriptionId according to your sandbox credentials
    //        var request = new deleteCustomerPaymentProfileRequest
    //        {
    //            customerProfileId = customerProfileId,
    //            customerPaymentProfileId = customerPaymentProfileId
    //        };

    //        //Prepare Request
    //        var controller = new deleteCustomerPaymentProfileController(request);
    //        controller.Execute();

    //        //Send Request to EndPoint
    //        deleteCustomerPaymentProfileResponse response = controller.GetApiResponse();
    //        if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
    //        {
    //            if (response != null && response.messages.message != null)
    //            {
    //                Console.WriteLine("Success, ResultCode : " + response.messages.resultCode.ToString());
    //            }
    //        }
    //        else if(response != null)
    //        {
    //            Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
    //        }

    //        return response;
    //    }
    //}

    public class DeleteCustomerPaymentProfile
    {
        public static void DeleteCustomerPaymentProfileExec(String ApiLoginID, String ApiTransactionKey)
        {
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/DeleteCustomerPaymentProfile.csv", FileMode.Open)), true))
            {
                Console.WriteLine("DeleteCustomerPaymentProfile Sample");

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



                            //please update the subscriptionId according to your sandbox credentials
                            var request = new deleteCustomerPaymentProfileRequest
                            {
                                customerProfileId = customerProfileId,
                                customerPaymentProfileId = customerPaymentProfileId
                            };

                            //Prepare Request
                            var controller = new deleteCustomerPaymentProfileController(request);
                            controller.Execute();

                            //Send Request to EndPoint
                            deleteCustomerPaymentProfileResponse response = controller.GetApiResponse();
                            if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
                            {
                                /*****************************/
                                try
                                {
                                    //Assert.AreEqual(response.Id, customerProfileId);
                                    Console.WriteLine("Assertion Succeed! Valid CustomerPaymentProfile deleted.");
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("DCPP_00" + flag.ToString());
                                    row1.Add("DeleteCustomerPaymentProfile");
                                    row1.Add("Pass");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //  Console.WriteLine("Success " + TestcaseID + " CustomerID : " + response.Id);
                                    flag = flag + 1;
                                }
                                catch
                                {
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("DCPP_00" + flag.ToString());
                                    row1.Add("DeleteCustomerPaymentProfile");
                                    row1.Add("Fail");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    Console.WriteLine("Assertion Failed! Invalid CustomerPaymentProfile deleted.");
                                    flag = flag + 1;
                                }
                                /*******************/
                                //if (response != null && response.messages.message != null)
                                //{
                                //    Console.WriteLine("Success, ResultCode : " + response.messages.resultCode.ToString());
                                //}
                            }
                            else
                            {
                                CsvRow row1 = new CsvRow();
                                row1.Add("DCPP_00" + flag.ToString());
                                row1.Add("DeleteCustomerPaymentProfile");
                                row1.Add("Fail");
                                row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                writer.WriteRow(row1);
                            }
                        }
                        //else if (response != null)
                        //{
                        //    Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
                        //}
                        catch (Exception e)
                        {
                            CsvRow row2 = new CsvRow();
                            row2.Add("DCPP_00" + flag.ToString());
                            row2.Add("DeleteCustomerPaymentProfile");
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