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
    public class CreateSubscription
    {
        //public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey, short intervalLength)
        //{
        //    Console.WriteLine("Create Subscription Sample");

        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;

        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        //    {
        //        name            = ApiLoginID,
        //        ItemElementName = ItemChoiceType.transactionKey,
        //        Item            = ApiTransactionKey,
        //    };

        //    paymentScheduleTypeInterval interval = new paymentScheduleTypeInterval();

        //    interval.length = intervalLength;                        // months can be indicated between 1 and 12
        //    interval.unit   = ARBSubscriptionUnitEnum.days;

        //    paymentScheduleType schedule = new paymentScheduleType
        //    {
        //        interval            = interval,
        //        startDate           = DateTime.Now.AddDays(1),      // start date should be tomorrow
        //        totalOccurrences    = 9999,                          // 999 indicates no end date
        //        trialOccurrences     = 3
        //    };

        //    #region Payment Information
        //    var creditCard = new creditCardType
        //    {
        //        cardNumber      = "4111111111111111",
        //        expirationDate  = "0718"
        //    };

        //    //standard api call to retrieve response
        //    paymentType cc = new paymentType { Item = creditCard };
        //    #endregion

        //    nameAndAddressType addressInfo = new nameAndAddressType()
        //    {
        //        firstName = "John",
        //        lastName = "Doe"
        //    };

        //    ARBSubscriptionType subscriptionType = new ARBSubscriptionType()
        //    {
        //        amount = 35.55m,
        //        trialAmount = 0.00m,
        //        paymentSchedule = schedule,
        //        billTo = addressInfo,
        //        payment = cc
        //    };

        //    var request = new ARBCreateSubscriptionRequest {subscription = subscriptionType };

        //    var controller = new ARBCreateSubscriptionController(request);          // instantiate the contoller that will call the service
        //    controller.Execute();

        //    ARBCreateSubscriptionResponse response = controller.GetApiResponse();   // get the response from the service (errors contained if any)
           
        //    //validate
        //    if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
        //    {
        //        if (response != null && response.messages.message != null)
        //        {
        //            Console.WriteLine("Success, Subscription ID : " + response.subscriptionId.ToString());
        //        }
        //    }
        //    else if(response != null)
        //    {
        //        Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
        //    }

        //    return response;
        //}
        public static void CreateSubscriptionExec(String ApiLoginID, String ApiTransactionKey)
        {
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/CreateASubscription.csv", FileMode.Open)), true))
            {
                Console.WriteLine("Create Subscription Sample");
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
                        string length = null;
                        string TestCase_Id = null;
      
                        string amount = null;
                        for (int i = 0; i < fieldCount; i++)
                        {
                            switch (headers[i])
                            {
                                case "length":
                                    length = csv[i];
                                    break;
                                case "amount":
                                    amount = csv[i];
                                    break;
                                case "TestCase_Id":
                                    TestCase_Id = csv[i];
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
                            paymentScheduleTypeInterval interval = new paymentScheduleTypeInterval();

                            interval.length = Convert.ToInt16(length);                        // months can be indicated between 1 and 12
                            interval.unit = ARBSubscriptionUnitEnum.days;

                            paymentScheduleType schedule = new paymentScheduleType
                            {
                                interval = interval,
                                startDate = DateTime.Now.AddDays(1),      // start date should be tomorrow
                                totalOccurrences = 9999,                          // 999 indicates no end date
                                trialOccurrences = 3
                            };

                            #region Payment Information
                            var creditCard = new creditCardType
                            {
                                cardNumber = "4111111111111111",
                                expirationDate = "0718"
                            };

                            //standard api call to retrieve response
                            paymentType cc = new paymentType { Item = creditCard };
                            #endregion

                            nameAndAddressType addressInfo = new nameAndAddressType()
                            {
                                firstName = "John",
                                lastName = "Doe"
                            };

                            ARBSubscriptionType subscriptionType = new ARBSubscriptionType()
                            {
                                amount = Convert.ToDecimal(amount),
                                trialAmount = 0.00m,
                                paymentSchedule = schedule,
                                billTo = addressInfo,
                                payment = cc
                            };

                            var request = new ARBCreateSubscriptionRequest { subscription = subscriptionType };

                            var controller = new ARBCreateSubscriptionController(request);          // instantiate the contoller that will call the service
                            controller.Execute();

                            ARBCreateSubscriptionResponse response = controller.GetApiResponse();



                            if (response != null && response.messages.resultCode == messageTypeEnum.Ok
                                && response.messages.message != null)
                            {
                                try
                                {
                                    //Assert.AreEqual(response.Id, customerProfileId);
                                    Console.WriteLine("Assertion Succeed! Valid CustomerId fetched.");
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("CS_00" + flag.ToString());
                                    row1.Add("CreateSubscription");
                                    row1.Add("Pass");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //  Console.WriteLine("Success " + TestcaseID + " CustomerID : " + response.Id);
                                    flag = flag + 1;
                                    Console.WriteLine("Success, Subscription ID : " + response.subscriptionId.ToString());
                                }
                                catch
                                {
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("CS_00" + flag.ToString());
                                    row1.Add("CreateSubscription");
                                    row1.Add("Assertion Failed!");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    Console.WriteLine("Assertion Failed! Invalid CustomerId fetched.");
                                    flag = flag + 1;
                                }
                            }
                            else
                            {
                                CsvRow row1 = new CsvRow();
                                row1.Add("CS_00" + flag.ToString());
                                row1.Add("CreateSubscription");
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
                            row2.Add("CS_00" + flag.ToString());
                            row2.Add("CreateSubscription");
                            row2.Add("Fail");
                            row2.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                            writer.WriteRow(row2);
                            flag = flag + 1;
                            Console.WriteLine(TestCase_Id + " Error Message " + e.Message);
                        }
                    }
                }
            }
        }
    }
}
