using System;
using System.Collections.Generic;
using AuthorizeNET.Api.Controllers;
using AuthorizeNET.Api.Contracts.V1;
using AuthorizeNET.Api.Controllers.Bases;
using LumenWorks.Framework.IO.Csv;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NUnit.Framework;
using System.Diagnostics;

namespace net.authorize.sample
{
    public class CreateCustomerProfile
    {
        //public static ANetApiResponse Run(string ApiLoginID, string ApiTransactionKey, string emailId)
        //{
        //    Console.WriteLine("CreateCustomerProfile Sample");

        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;
        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        //    {
        //        name            = ApiLoginID,
        //        ItemElementName = ItemChoiceType.transactionKey,
        //        Item            = ApiTransactionKey,
        //    };


        //    var creditCard = new creditCardType
        //    {
        //        cardNumber      = "4111111111111111",
        //        expirationDate  = "0718"
        //    };

        //    var bankAccount = new bankAccountType
        //    {
        //        accountNumber = "231323342",
        //        routingNumber = "000000224",
        //        accountType = bankAccountTypeEnum.checking,
        //        echeckType = echeckTypeEnum.WEB,
        //        nameOnAccount = "test",
        //        bankName = "Bank Of America"
        //    };

        //    //standard api call to retrieve response
        //    paymentType cc = new paymentType { Item = creditCard };
        //    paymentType echeck = new paymentType {Item = bankAccount};

        //    List<customerPaymentProfileType> paymentProfileList = new List<customerPaymentProfileType>();
        //    customerPaymentProfileType ccPaymentProfile = new customerPaymentProfileType();
        //    ccPaymentProfile.payment = cc;
            
        //    customerPaymentProfileType echeckPaymentProfile = new customerPaymentProfileType();
        //    echeckPaymentProfile.payment = echeck;

        //    paymentProfileList.Add(ccPaymentProfile);
        //    paymentProfileList.Add(echeckPaymentProfile);

        //    List<customerAddressType> addressInfoList = new List<customerAddressType>();
        //    customerAddressType homeAddress = new customerAddressType();
        //    homeAddress.address = "10900 NE 8th St";
        //    homeAddress.city = "Seattle";
        //    homeAddress.zip = "98006";


        //    customerAddressType officeAddress = new customerAddressType();
        //    officeAddress.address = "1200 148th AVE NE";
        //    officeAddress.city = "NorthBend";
        //    officeAddress.zip = "92101";

        //    addressInfoList.Add(homeAddress);
        //    addressInfoList.Add(officeAddress);


        //    customerProfileType customerProfile = new customerProfileType();
        //    customerProfile.merchantCustomerId = "Test CustomerID";
        //    customerProfile.email = emailId;
        //    customerProfile.paymentProfiles = paymentProfileList.ToArray();
        //    customerProfile.shipToList = addressInfoList.ToArray();

        //    var request = new createCustomerProfileRequest{ profile = customerProfile, validationMode = validationModeEnum.none};

        //    var controller = new createCustomerProfileController(request);          // instantiate the contoller that will call the service
        //    controller.Execute();

        //    createCustomerProfileResponse response = controller.GetApiResponse();   // get the response from the service (errors contained if any)
           
        //    //validate
        //    if (response != null && response.messages.resultCode == messageTypeEnum.Ok)
        //    {
        //        if (response != null && response.messages.message != null)
        //        {
        //            Console.WriteLine("Success, CustomerProfileID : " + response.customerProfileId);
        //            Console.WriteLine("Success, CustomerPaymentProfileID : " + response.customerPaymentProfileIdList[0]);
        //            Console.WriteLine("Success, CustomerShippingProfileID : " + response.customerShippingAddressIdList[0]);
        //        }
        //    }
        //    else if(response != null )
        //    {
        //        Console.WriteLine("Error: " + response.messages.message[0].code + "  " + response.messages.message[0].text);
        //    }

        //    return response;
        //}

        public static void CreateCustomerProfileExec(String ApiLoginID, String ApiTransactionKey)
        {
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/CreateCustomerProfile.csv", FileMode.Open)), true))
            {
                Console.WriteLine("CreateCustomerProfile Sample");
                int fieldCount = csv.FieldCount;
                int flag = 0;
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
                        //CustomersApi instance = new CustomersApi(EnvironmentSet.Sandbox);
                        // Customer Response Object           
                        // Customer response = null;
                        //initialization



                        string TestcaseID = null;
                        string description = null;
                        string email = null;
                       
                        

                        for (int i = 0; i < fieldCount; i++)
                        {
                            switch (headers[i])
                            {

                                case "TestcaseID":
                                    TestcaseID = csv[i];
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

                            var creditCard = new creditCardType
                            {
                                cardNumber = "4111111111111111",
                                expirationDate = "0718"
                            };

                            var bankAccount = new bankAccountType
                            {
                                accountNumber = "231323342",
                                routingNumber = "000000224",
                                accountType = bankAccountTypeEnum.checking,
                                echeckType = echeckTypeEnum.WEB,
                                nameOnAccount = "test",
                                bankName = "Bank Of America"
                            };
                            //standard api call to retrieve response
                            paymentType cc = new paymentType { Item = creditCard };
                            paymentType echeck = new paymentType { Item = bankAccount };

                            List<customerPaymentProfileType> paymentProfileList = new List<customerPaymentProfileType>();
                            customerPaymentProfileType ccPaymentProfile = new customerPaymentProfileType();
                            ccPaymentProfile.payment = cc;

                            customerPaymentProfileType echeckPaymentProfile = new customerPaymentProfileType();
                            echeckPaymentProfile.payment = echeck;

                            paymentProfileList.Add(ccPaymentProfile);
                            paymentProfileList.Add(echeckPaymentProfile);

                            List<customerAddressType> addressInfoList = new List<customerAddressType>();
                            customerAddressType homeAddress = new customerAddressType();
                            homeAddress.address = "10900 NE 8th St";
                            homeAddress.city = "Seattle";
                            homeAddress.zip = "98006";


                            customerAddressType officeAddress = new customerAddressType();
                            officeAddress.address = "1200 148th AVE NE";
                            officeAddress.city = "NorthBend";
                            officeAddress.zip = "92101";

                            addressInfoList.Add(homeAddress);
                            addressInfoList.Add(officeAddress);


                            customerProfileType customerProfile = new customerProfileType();
                            customerProfile.merchantCustomerId = "Test CustomerID";
                            customerProfile.email = email;
                            customerProfile.description = description;
                            customerProfile.paymentProfiles = paymentProfileList.ToArray();
                            customerProfile.shipToList = addressInfoList.ToArray();
                            var request = new createCustomerProfileRequest
                            { profile = customerProfile, validationMode = validationModeEnum.none };


                            // instantiate the controller that will call the service
                            var controller = new createCustomerProfileController(request);
                            controller.Execute();

                            // get the response from the service (errors contained if any)
                            createCustomerProfileResponse response = controller.GetApiResponse();

                            if (response != null && response.messages.resultCode == messageTypeEnum.Ok
                                && response.messages.message != null)
                            {
                                try
                                {
                                    //Assert.AreEqual(response.Id, customerProfileId);
                                    //Console.WriteLine("Assertion Succeed! Valid CustomerId fetched.");
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("CCP_00" + flag.ToString());
                                    row1.Add("CreateCustomerProfile");
                                    row1.Add("Pass");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //  Console.WriteLine("Success " + TestcaseID + " CustomerID : " + response.Id);
                                    flag = flag + 1;

                                    Console.WriteLine("Success, CustomerProfileID : " + response.customerProfileId);
                                    Console.WriteLine("Success, CustomerPaymentProfileID : " + response.customerPaymentProfileIdList[0]);
                                    Console.WriteLine("Success, CustomerShippingProfileID : " + response.customerShippingAddressIdList[0]);

                                }
                                catch
                                {
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("CCP_00" + flag.ToString());
                                    row1.Add("CreateCustomerProfile");
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
                                row1.Add("CCP_00" + flag.ToString());
                                row1.Add("CreateCustomerProfile");
                                row1.Add("Assertion Failed!");
                                row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                writer.WriteRow(row1);
                                Console.WriteLine("Assertion Failed! Invalid CustomerId fetched.");
                                flag = flag + 1;
                            }
                        }
                        catch (Exception e)
                        {
                            CsvRow row2 = new CsvRow();
                            row2.Add("CCP_00" + flag.ToString());
                            row2.Add("CreateCustomerProfile");
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
