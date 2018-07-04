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
    public class UpdateSubscription
    {
        //public static ANetApiResponse Run(string ApiLoginID, string ApiTransactionKey, string subscriptionId)
        //{
        //    Console.WriteLine("Update Subscription Sample");

        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;
        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        //    {
        //        name = ApiLoginID,
        //        ItemElementName = ItemChoiceType.transactionKey,
        //        Item = ApiTransactionKey,
        //    };

        //    paymentScheduleType schedule = new paymentScheduleType
        //    {
        //        startDate = DateTime.Now.AddDays(1),      // start date should be tomorrow
        //        totalOccurrences = 9999                          // 999 indicates no end date
        //    };

        //    #region Payment Information
        //    var creditCard = new creditCardType
        //    {
        //        cardNumber = "4111111111111111",
        //        expirationDate = "0718"
        //    };

        //    //standard api call to retrieve response
        //    paymentType cc = new paymentType { Item = creditCard };
        //    #endregion

        //    nameAndAddressType addressInfo = new nameAndAddressType()
        //    {
        //        firstName = "Calvin",
        //        lastName = "Brown"
        //    };

        //    customerProfileIdType customerProfile = new customerProfileIdType()
        //    {
        //        customerProfileId = "1232312",
        //        customerPaymentProfileId = "2132132",
        //        customerAddressId = "1233432"
        //    };

        //    ARBSubscriptionType subscriptionType = new ARBSubscriptionType()
        //    {
        //        amount = 35.55m,
        //        paymentSchedule = schedule,
        //        billTo = addressInfo,
        //        payment = cc
        //        //You can pass a profile to update subscription
        //        //,profile = customerProfile
        //    };

        //    //Please change the subscriptionId according to your request
        //    var request = new ARBUpdateSubscriptionRequest { subscription = subscriptionType, subscriptionId = subscriptionId };
        //    var controller = new ARBUpdateSubscriptionController(request);         
        //    controller.Execute();

        //    ARBUpdateSubscriptionResponse response = controller.GetApiResponse(); 

        //    //validate
        //    if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
        //    {
        //        if (response != null && response.messages.message != null)
        //        {
        //            Console.WriteLine("Success, RefID Code : " + response.refId);
        //        }
        //    }
        //    else if(response != null)
        //    {
        //        Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
        //    }

        //    return response;
        //}
        public static void UpdateSubscriptionExec(String ApiLoginID, String ApiTransactionKey)
        {
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/CancleASubscription.csv", FileMode.Open)), true))
            {
                Console.WriteLine("Update Subscription Sample");
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
                        string subscriptionId = null;
                        string TestCase_Id = null;
                        string firstName = null;
                        string lastName = null;
                        string customerProfileId = null;
                        string customerPaymentProfileId = null;
                        string customerAddressId = null;
                        string amount = null;
                        for (int i = 0; i < fieldCount; i++)
                        {
                            switch (headers[i])
                            {
                                case "subscriptionId":
                                    subscriptionId = csv[i];
                                    break;
                                case "firstName":
                                    firstName = csv[i];
                                    break;
                                case "lastName":
                                    lastName = csv[i];
                                    break;
                                case "customerProfileId":
                                    customerProfileId = csv[i];
                                    break;
                                case "customerPaymentProfileId":
                                    customerPaymentProfileId = csv[i];
                                    break;
                                case "customerAddressId":
                                    customerAddressId = csv[i];
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

                            //response = instance.GetCustomer(customerId, authorization);
                            paymentScheduleType schedule = new paymentScheduleType
                            {
                                startDate = DateTime.Now.AddDays(1),      // start date should be tomorrow
                                totalOccurrences = 9999                          // 999 indicates no end date
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
                                firstName = firstName,
                                lastName = lastName
                            };

                            customerProfileIdType customerProfile = new customerProfileIdType()
                            {
                            customerProfileId = customerProfileId,
                            customerPaymentProfileId = customerPaymentProfileId,
                            customerAddressId = customerAddressId
                            };

                            ARBSubscriptionType subscriptionType = new ARBSubscriptionType()
                            {
                                amount = Convert.ToDecimal(amount),
                                paymentSchedule = schedule,
                                billTo = addressInfo,
                                payment = cc
                                //You can pass a profile to update subscription
                                //,profile = customerProfile
                            };

                            //Please change the subscriptionId according to your request
                            var request = new ARBUpdateSubscriptionRequest { subscription = subscriptionType, subscriptionId = subscriptionId };
                            var controller = new ARBUpdateSubscriptionController(request);
                            controller.Execute();

                            ARBUpdateSubscriptionResponse response = controller.GetApiResponse();
                            if (response != null && response.messages.resultCode == messageTypeEnum.Ok
                                && response.messages.message != null)
                            {
                                try
                                {
                                    //Assert.AreEqual(response.Id, customerProfileId);
                                    Console.WriteLine("Assertion Succeed! Valid CustomerId fetched.");
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("UAS_00" + flag.ToString());
                                    row1.Add("UpdateASubscription");
                                    row1.Add("Pass");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //  Console.WriteLine("Success " + TestcaseID + " CustomerID : " + response.Id);
                                    flag = flag + 1;
                                    Console.WriteLine("Success, RefID Code : " + response.refId);
                                }
                                catch
                                {
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("UAS_00" + flag.ToString());
                                    row1.Add("UpdateASubscription");
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
                                row1.Add("UAS_00" + flag.ToString());
                                row1.Add("UpdateASubscription");
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
                            row2.Add("UAS_00" + flag.ToString());
                            row2.Add("UpdateASubscription");
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
