using System;
using System.Collections.Generic;
using AuthorizeNET.Api.Controllers;
using AuthorizeNET.Api.Contracts.V1;
using AuthorizeNET.Api.Controllers.Bases;
using System.IO;
using LumenWorks.Framework.IO.Csv;

namespace net.authorize.sample
{
    public class CreateCustomerPaymentProfile
    {
        //public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey, string customerProfileId)
        //{
        //    Console.WriteLine("CreateCustomerPaymentProfile Sample");
        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;
        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        //    {
        //        name            = ApiLoginID,
        //        ItemElementName = ItemChoiceType.transactionKey,
        //        Item            = ApiTransactionKey,
        //    };

        //    var bankAccount = new bankAccountType
        //    {
        //        accountNumber = "01245524321",
        //        routingNumber = "000000204",
        //        accountType = bankAccountTypeEnum.checking,
        //        echeckType = echeckTypeEnum.WEB,
        //        nameOnAccount = "test",
        //        bankName = "Bank Of America"
        //    };

        //    paymentType echeck = new paymentType {Item = bankAccount};

        //    var billTo = new customerAddressType
        //    {
        //        firstName = "John",
        //        lastName = "Snow"
        //    };
        //    customerPaymentProfileType echeckPaymentProfile = new customerPaymentProfileType();
        //    echeckPaymentProfile.payment = echeck;
        //    echeckPaymentProfile.billTo = billTo;

        //    var request = new createCustomerPaymentProfileRequest
        //    {
        //        customerProfileId = customerProfileId,
        //        paymentProfile = echeckPaymentProfile,
        //        validationMode = validationModeEnum.none
        //    };

        //    //Prepare Request
        //    var controller = new createCustomerPaymentProfileController(request);
        //    controller.Execute();

        //     //Send Request to EndPoint
        //    createCustomerPaymentProfileResponse response = controller.GetApiResponse(); 
        //    if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
        //    {
        //        if (response != null && response.messages.message != null)
        //        {
        //            Console.WriteLine("Success, createCustomerPaymentProfileID : " + response.customerPaymentProfileId);
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
        //        if (response.messages.message[0].code == "E00039")
        //        {
        //            Console.WriteLine("Duplicate ID: " + response.customerPaymentProfileId);
        //        }
        //    }

        //    return response;

        //}

        public static void CreateCustomerPaymentProfileExec(String ApiLoginID, String ApiTransactionKey)
        {
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/CreateCustomerPaymentProfile.csv", FileMode.Open)), true))
            {
                Console.WriteLine("CreateCustomerPaymentProfile Sample");

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

                        var bankAccount = new bankAccountType
                        {
                            accountNumber = "01245524321",
                            routingNumber = "000000204",
                            accountType = bankAccountTypeEnum.checking,
                            echeckType = echeckTypeEnum.WEB,
                            nameOnAccount = "test",
                            bankName = "Bank Of America"
                        };

                        paymentType echeck = new paymentType { Item = bankAccount };

                        var billTo = new customerAddressType
                        {
                            firstName = "John",
                            lastName = "Snow"
                        };
                        customerPaymentProfileType echeckPaymentProfile = new customerPaymentProfileType();
                        echeckPaymentProfile.payment = echeck;
                        echeckPaymentProfile.billTo = billTo;


                        string customerProfileId = null;
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

                            var request = new createCustomerPaymentProfileRequest
                            {
                                customerProfileId = customerProfileId,
                                paymentProfile = echeckPaymentProfile,
                                validationMode = validationModeEnum.none
                            };
                            //Prepare Request
                            var controller = new createCustomerPaymentProfileController(request);
                            controller.Execute();

                            //Send Request to EndPoint
                            createCustomerPaymentProfileResponse response = controller.GetApiResponse();
                            //var response = controller.GetApiResponse();
                            if (response != null && response.messages.resultCode == messageTypeEnum.Ok 
                                && response.messages.message != null)
                            {
                                /*****************************/
                                try
                                {
                                    //Assert.AreEqual(response.Id, customerProfileId);
                                    //Console.WriteLine("Assertion Succeed! Valid customerProfileId fetched.");
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("CCPP_00" + flag.ToString());
                                    row1.Add("CreateCustomerPaymentProfile");
                                    row1.Add("Pass");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //  Console.WriteLine("Success " + TestcaseID + " CustomerID : " + response.Id);
                                    flag = flag + 1;
                                    Console.WriteLine("Success, createCustomerPaymentProfileID : " + response.customerPaymentProfileId);
                                }
                                catch
                                {
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("CCPP_00" + flag.ToString());
                                    row1.Add("CreateCustomerPaymentProfile");
                                    row1.Add("Fail");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    Console.WriteLine("Assertion Failed! Invalid CustomerPaymentProfile fetched.");
                                    flag = flag + 1;
                                }
                                /*******************/
                                //if (response.messages.message != null)
                                //{
                                //    Console.WriteLine("Success, createCustomerPaymentProfileID : " + response.customerPaymentProfileId);
                                //}
                            }
                            else
                            {
                                //Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
                                CsvRow row1 = new CsvRow();
                                row1.Add("CCPP_00" + flag.ToString());
                                row1.Add("CreateCustomerPaymentProfile");
                                row1.Add("Fail");
                                row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                writer.WriteRow(row1);
                                //Console.WriteLine("Assertion Failed! Invalid CustomerPaymentProfile fetched.");
                                flag = flag + 1;
                                //if (response.messages.message[0].code == "E00039")
                                //{
                                //    Console.WriteLine("Duplicate ID: " + response.customerPaymentProfileId);
                                //}
                            }

                            //return response;

                        }
                        catch (Exception e)
                        {
                            CsvRow row2 = new CsvRow();
                            row2.Add("CCPP_00" + flag.ToString());
                            row2.Add("CreateCustomerPaymentProfile");
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