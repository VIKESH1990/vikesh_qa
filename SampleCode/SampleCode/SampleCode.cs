using net.authorize.sample.MobileInappTransactions;
using net.authorize.sample.PaymentTransactions;
using System;
using net.authorize.sample.CustomerProfiles;
using net.authorize.sample;


namespace net.authorize.sample
{
    //===============================================================================================
    //
    //  NOTE:  If you add a new sample update the following methods here:
    //
    //              ShowMethods()   -  Add the method name
    //              RunMethod(String methodName)   -   Update the switch statement to run the method
    //
    //===============================================================================================
    class SampleCode
    {
        //static void Main(string[] args)
        //{

        //    if (args.Length == 0)
        //    {
        //        SelectMethod();
        //    }
        //    else if (args.Length == 1)
        //    {
        //        RunMethod(args[0]);
        //        return;
        //    }
        //    else
        //    {
        //        ShowUsage();
        //    }

        //    Console.WriteLine("");
        //    Console.Write("Press <Return> to finish ...");
        //    Console.ReadLine();

        //}

        private static void ShowUsage()
        {
            Console.WriteLine("Usage : SampleCode [CodeSampleName]");
            Console.WriteLine("");
            Console.WriteLine("Run with no parameter to select a method.  Otherwise pass a method name.");
            Console.WriteLine("");
            Console.WriteLine("Code Sample Names: ");
            ShowMethods();
            Console.WriteLine("Code Sample Names: ");

        }

        private static void SelectMethod()
        {
            Console.WriteLine("Code Sample Names: ");
            Console.WriteLine("");
            ShowMethods();
            Console.WriteLine("");
            Console.Write("Type a sample name & then press <Return> : ");
            RunMethod(Console.ReadLine());
        }

        private static void ShowMethods()
        {
            //DataAppend.ReadPrevData();
            Console.WriteLine("    ChargeCreditCard");
            Console.WriteLine("    AuthorizeCreditCard");
            Console.WriteLine("    CapturePreviouslyAuthorizedAmount");
            Console.WriteLine("    CaptureFundsAuthorizedThroughAnotherChannel");
            Console.WriteLine("    RefundTransaction");
            Console.WriteLine("    Void");
            Console.WriteLine("    DebitBankAccount");
            Console.WriteLine("    CreditBankAccount");
            Console.WriteLine("    ChargeCustomerProfile");
            Console.WriteLine("    ChargeTokenizedCard");
            Console.WriteLine("    ChargeTokenizedCreditCard");            
            Console.WriteLine("    ChargeEncryptedTrackData");
            Console.WriteLine("    ChargeTrackData");
            Console.WriteLine("    CreateAnApplePayTransaction");
            Console.WriteLine("    CreateAnAndroidPayTransaction");
            Console.WriteLine("    CreateAnAcceptTransaction");
            Console.WriteLine("    DecryptVisaCheckoutData");
            Console.WriteLine("    CreateVisaCheckoutTransaction");
            Console.WriteLine("    PayPalAuthorizeCapture");
            Console.WriteLine("    PayPalAuthorizeCaptureContinue");
            Console.WriteLine("    PayPalAuthorizeOnly");
            Console.WriteLine("    PayPalCredit");
            Console.WriteLine("    PayPalGetDetails");
            Console.WriteLine("    PayPalPriorAuthorizationCapture");
            Console.WriteLine("    PayPalVoid");         
            Console.WriteLine("    CancelSubscription");
            Console.WriteLine("    CreateSubscription");
            Console.WriteLine("    CreateSubscriptionFromCustomerProfile");
            Console.WriteLine("    GetListOfSubscriptions");
            Console.WriteLine("    GetSubscriptionStatus");
            Console.WriteLine("    GetSubscription");
            Console.WriteLine("    GetUnsettledTransactionList");
            Console.WriteLine("    UpdateSubscription");
            Console.WriteLine("    CreateCustomerProfile");
            Console.WriteLine("    CreateCustomerPaymentProfile");
            Console.WriteLine("    CreateCustomerShippingAddress");
            Console.WriteLine("    DeleteCustomerProfile");
            Console.WriteLine("    DeleteCustomerPaymentProfile");
            Console.WriteLine("    DeleteCustomerShippingAddress");
            Console.WriteLine("    ValidateCustomerPaymentProfile");
            Console.WriteLine("    UpdateCustomerShippingAddress");
            Console.WriteLine("    UpdateCustomerProfile");
            Console.WriteLine("    UpdateCustomerPaymentProfile");
            Console.WriteLine("    GetCustomerShippingAddress");
            Console.WriteLine("    GetCustomerProfileId");
            Console.WriteLine("    GetCustomerProfile");
            Console.WriteLine("    GetAccountUpdaterJobDetails");
            Console.WriteLine("    GetAcceptCustomerProfilePage");
            Console.WriteLine("    GetCustomerPaymentProfile");
            Console.WriteLine("    GetCustomerPaymentProfileList");
            Console.WriteLine("    DeleteCustomerShippingAddress");
            Console.WriteLine("    DeleteCustomerProfile");
            Console.WriteLine("    DeleteCustomerPaymentProfile");
            Console.WriteLine("    CreateCustomerShippingAddress");
            Console.WriteLine("    CreateCustomerProfileFromTransaction");
            Console.WriteLine("    GetBatchStatistics");
            Console.WriteLine("    GetSettledBatchList");
            Console.WriteLine("    GetTransactionDetails");
            Console.WriteLine("    GetTransactionList");
            Console.WriteLine("    UpdateSplitTenderGroup");
            Console.WriteLine("    GetHeldTransactionList");
            Console.WriteLine("    ApproveOrDeclineHeldTransaction");
            Console.WriteLine("    GetMerchantDetails");
            Console.WriteLine("    GetAnAcceptPaymentPage");
        }

        public static void RunMethod(String methodName)
        {
            // These are default transaction keys.
            // You can create your own keys in seconds by signing up for a sandbox account here: https://developer.authorize.net/sandbox/
            const string apiLoginId = "5KP3u95bQpv";
            const string transactionKey = "346HZ32z3fP4hTG2";

            //For Apple Pay
            //const string apiLoginId = "2z7w5KGj8D";
            //const string transactionKey = "2ng238CM3v3WAnKr";

            //For Android Pay
            //const string apiLoginId = "735qYCdW";
            //const string transactionKey = "294A3Ldsr98hM42Q";

            //Update TransactionID for which you want to run the sample code
            //const string transactionId = "2249735976";

            //Update PayerID for which you want to run the sample code
            //const string payerId = "M8R9JRNJ3R28Y";

            //const string customerProfileId = "213213";
            //const string customerPaymentProfileId = "2132345";
            //const string shippingAddressId = "1223213";
            //const decimal amount = 12.34m;
            //const string subscriptionId = "1223213";
            //const short day = 45;
            //const string emailId = "test@test.com";

            switch (methodName)
            {
                case "ValidateCustomerPaymentProfile":
                    //ValidateCustomerPaymentProfile.Run(apiLoginId, transactionKey, customerProfileId, customerPaymentProfileId);
                    ValidateCustomerPaymentProfile.ValidateCustomerPaymentProfileExec(apiLoginId, transactionKey);
                    break;
                case "UpdateCustomerShippingAddress":
                    //UpdateCustomerShippingAddress.Run(apiLoginId, transactionKey, customerProfileId, shippingAddressId);
                    UpdateCustomerShippingAddress.UpdateCustomerShippingAddressExec(apiLoginId, transactionKey);
                    break;
                case "UpdateCustomerProfile":
                    //UpdateCustomerProfile.Run(apiLoginId, transactionKey, customerProfileId);
                    UpdateCustomerProfile.UpdateCustomerProfileExec(apiLoginId, transactionKey);
                    break;
                case "UpdateCustomerPaymentProfile":
                    //UpdateCustomerPaymentProfile.Run(apiLoginId, transactionKey, customerProfileId, customerPaymentProfileId);
                    UpdateCustomerPaymentProfile.UpdateCustomerPaymentProfileExec(apiLoginId, transactionKey);
                    break;
                case "GetCustomerShippingAddress":
                    //GetCustomerShippingAddress.Run(apiLoginId, transactionKey, customerProfileId, shippingAddressId);
                    GetCustomerShippingAddress.GetCustomerShippingAddressExec(apiLoginId, transactionKey);
                    break;
                case "GetCustomerProfileIds":
                    //GetCustomerProfileIds.Run(apiLoginId, transactionKey);
                    GetCustomerProfileIds.GetCustomerProfileIdsExec(apiLoginId, transactionKey);
                    break;
                case "GetCustomerProfile":
                    //GetCustomerProfile.Run(apiLoginId, transactionKey, customerProfileId);
                    GetCustomerProfile.GetCustomerProfileExec(apiLoginId, transactionKey);
                    break;
                case "GetAcceptCustomerProfilePage":
                    //GetAcceptCustomerProfilePage.Run(apiLoginId, transactionKey, customerProfileId);
                    GetAcceptCustomerProfilePage.GetAcceptCustomerProfilePageExec(apiLoginId, transactionKey);
                    break;
                case "GetCustomerPaymentProfile":
                    //GetCustomerPaymentProfile.Run(apiLoginId, transactionKey, customerProfileId, customerPaymentProfileId);
                    GetCustomerPaymentProfile.GetCustomerPaymentProfileExec(apiLoginId, transactionKey);
                    break;
                case "GetCustomerPaymentProfileList":
                    //GetCustomerPaymentProfileList.Run(apiLoginId, transactionKey);
                    GetCustomerPaymentProfileList.GetCustomerPaymentProfileListExec(apiLoginId, transactionKey);
                    break;
                case "DeleteCustomerShippingAddress":
                    //DeleteCustomerShippingAddress.Run(apiLoginId, transactionKey, customerProfileId, shippingAddressId);
                    DeleteCustomerShippingAddress.DeleteCustomerShippingAddressExec(apiLoginId, transactionKey);
                    break;
                case "DeleteCustomerProfile":
                    //DeleteCustomerProfile.Run(apiLoginId, transactionKey, customerProfileId);
                    DeleteCustomerProfile.DeleteCustomerProfileExec(apiLoginId, transactionKey);
                    break;
                case "DeleteCustomerPaymentProfile":
                    //DeleteCustomerPaymentProfile.Run(apiLoginId, transactionKey, customerProfileId, customerPaymentProfileId);
                    DeleteCustomerPaymentProfile.DeleteCustomerPaymentProfileExec(apiLoginId, transactionKey);
                    break;
                case "CreateCustomerShippingAddress":
                    //CreateCustomerShippingAddress.Run(apiLoginId, transactionKey, customerProfileId);
                    CreateCustomerShippingAddress.CreateCustomerShippingAddressExec(apiLoginId, transactionKey);
                    break;
                case "CreateCustomerProfileFromTransaction":
                    //CreateCustomerProfileFromTransaction.Run(apiLoginId, transactionKey, transactionId);
                    CreateCustomerProfileFromTransaction.CreateCustomerProfileFromTransactionExec(apiLoginId, transactionKey);
                    break;
                case "GetTransactionDetails":
                    //GetTransactionDetails.Run(apiLoginId, transactionKey, transactionId);
                    GetTransactionDetails.GetTransactionDetailsExec(apiLoginId, transactionKey);
                    break;
                case "GetTransactionList":
                    //GetTransactionList.Run(apiLoginId, transactionKey);
                    GetTransactionList.GetTransactionListExec(apiLoginId, transactionKey);
                    break;
                case "CreateAnApplePayTransaction":
                    //CreateAnApplePayTransaction.Run(apiLoginId, transactionKey, 12.23m);
                    CreateAnApplePayTransaction.CreateAnApplePayTransactionExec(apiLoginId, transactionKey);
                    break;
                case "CreateAnAndroidPayTransaction":
                    //CreateAnAndroidPayTransaction.Run(apiLoginId, transactionKey, 12.23m);
                    CreateAnAndroidPayTransaction.CreateAnAndroidPayTransactionExec(apiLoginId, transactionKey);
                    break;
                case "CreateAnAcceptTransaction":
                    //CreateAnAcceptTransaction.Run(apiLoginId, transactionKey, 12.23m);
                    CreateAnAcceptTransaction.CreateAnAcceptTransactionExec(apiLoginId, transactionKey);
                    break;
                case "DecryptVisaCheckoutData":
                    //DecryptVisaCheckoutData.Run(apiLoginId, transactionKey);
                    DecryptVisaCheckoutData.DecryptVisaCheckoutDataExec(apiLoginId, transactionKey);
                    break;
                case "CreateVisaCheckoutTransaction":
                    CreateVisaCheckoutTransaction.CreateVisaCheckoutTransactionExec(apiLoginId, transactionKey);
                    break;
                case "ChargeCreditCard":
                    //ChargeCreditCard.Run(apiLoginId, transactionKey, amount);
                    ChargeCreditCard.ChargeCreditCardExec(apiLoginId, transactionKey);
                    break;
                case "ChargeEncryptedTrackData":
                    //ChargeEncryptedTrackData.Run(apiLoginId, transactionKey, amount);
                    ChargeEncryptedTrackData.ChargeEncryptedTrackDataExec(apiLoginId, transactionKey);
                    break;
                case "ChargeTrackData":
                    //ChargeTrackData.Run(apiLoginId, transactionKey, amount);
                    ChargeTrackData.ChargeTrackDataExec(apiLoginId, transactionKey);
                    //ChargeTrackData.ChargeTrackDataExec(apiLoginId, transactionKey);
                    break;
                case "CapturePreviouslyAuthorizedAmount":
                    //CapturePreviouslyAuthorizedAmount.Run(apiLoginId, transactionKey, amount, transactionId);
                    CapturePreviouslyAuthorizedAmount.CapturePreviouslyAuthorizedAmountExec(apiLoginId, transactionKey);
                    break;
                case "CaptureFundsAuthorizedThroughAnotherChannel":
                    //CaptureFundsAuthorizedThroughAnotherChannel.Run(apiLoginId, transactionKey, amount);
                    CaptureFundsAuthorizedThroughAnotherChannel.CaptureFundsAuthorizedThroughAnotherChannelExec(apiLoginId, transactionKey);
                    break;
                case "AuthorizeCreditCard":
                    //AuthorizeCreditCard.Run(apiLoginId, transactionKey, amount);
                    AuthorizeCreditCard.AuthorizeCreditCardExec(apiLoginId, transactionKey);
                    break;
                case "RefundTransaction":
                    //RefundTransaction.Run(apiLoginId, transactionKey, amount, transactionId);
                    RefundTransaction.RefundTransactionExec(apiLoginId, transactionKey);
                    break;
                case "VoidTransaction":
                    //VoidTransaction.Run(apiLoginId, transactionKey, transactionId);
                    VoidTransaction.VoidTransactionExec(apiLoginId, transactionKey);
                    break;
                case "DebitBankAccount":
                    //DebitBankAccount.Run(apiLoginId, transactionKey, amount);
                    DebitBankAccount.DebitBankAccountExec(apiLoginId, transactionKey);
                    break;
                case "CreditBankAccount":
                    //CreditBankAccount.Run(apiLoginId, transactionKey, transactionId);
                    CreditBankAccount.CreditBankAccountExec(apiLoginId, transactionKey);
                    break;
                case "ChargeCustomerProfile":
                    //ChargeCustomerProfile.Run(apiLoginId, transactionKey, customerProfileId, customerPaymentProfileId, amount);
                    ChargeCustomerProfile.ChargeCustomerProfileExec(apiLoginId, transactionKey);
                    break;
                //case "ChargeTokenizedCard":
                //    ChargeTokenizedCreditCard.Run(apiLoginId, transactionKey);
                //    break;
                case "ChargeTokenizedCreditCard":
                    //ChargeTokenizedCreditCard.Run(apiLoginId, transactionKey);
                    ChargeTokenizedCreditCard.ChargeTokenizedCreditCardExec(apiLoginId, transactionKey);
                    break;
                case "PayPalVoid":
                    //PayPalVoid.Run(apiLoginId, transactionKey, transactionId);
                    PayPalVoid.PayPalVoidExec(apiLoginId, transactionKey);
                    break;
                case "PayPalAuthorizeCapture":
                    //PayPalAuthorizeCapture.Run(apiLoginId, transactionKey, amount);
                    PayPalAuthorizeCapture.PayPalAuthorizeCaptureExec(apiLoginId, transactionKey);
                    break;
                case "PayPalAuthorizeCaptureContinue":
                    //PayPalAuthorizeCaptureContinue.Run(apiLoginId, transactionKey, transactionId, payerId);
                    PayPalAuthorizeCaptureContinue.PayPalAuthorizeCaptureContinueExec(apiLoginId, transactionKey);
                    break;
                case "PayPalAuthorizeOnly":
                    //PayPalAuthorizeOnly.Run(apiLoginId, transactionKey, amount);
                    PayPalAuthorizeOnly.PayPalAuthorizeOnlyExec(apiLoginId, transactionKey);
                    break;
                case "PayPalAuthorizeOnlyContinue":
                    //PayPalAuthorizeOnlyContinue.Run(apiLoginId, transactionKey, transactionId, payerId);
                    PayPalAuthorizeOnlyContinue.PayPalAuthorizeOnlyContinueExec(apiLoginId, transactionKey);
                    break;
                case "PayPalCredit":
                    //PayPalCredit.Run(apiLoginId, transactionKey, transactionId);
                    PayPalCredit.PayPalCreditExec(apiLoginId, transactionKey);
                    break;
                case "PayPalGetDetails":
                    //PayPalGetDetails.Run(apiLoginId, transactionKey, transactionId);
                    PayPalGetDetails.PayPalGetDetailsExec(apiLoginId, transactionKey);
                    break;
                case "PayPalPriorAuthorizationCapture":
                    //PayPalPriorAuthorizationCapture.Run(apiLoginId, transactionKey, transactionId);
                    PayPalPriorAuthorizationCapture.PayPalPriorAuthorizationCaptureExec(apiLoginId, transactionKey);
                    break;
                case "CancelSubscription":
                    //CancelSubscription.Run(apiLoginId, transactionKey, subscriptionId);
                    CancelSubscription.CancelSubscriptionExec(apiLoginId, transactionKey);
                    break;
                case "CreateSubscription":
                    //CreateSubscription.Run(apiLoginId, transactionKey, day);
                    CreateSubscription.CreateSubscriptionExec(apiLoginId, transactionKey);
                    break;
                case "CreateSubscriptionFromCustomerProfile":
                    //CreateSubscriptionFromCustomerProfile.Run(apiLoginId, transactionKey, day, "12322", "232321", "123232");
                    CreateSubscriptionFromCustomerProfile.CreateSubscriptionFromCustomerProfileExec(apiLoginId, transactionKey);
                    break;
                case "GetListOfSubscriptions":
                    //GetListOfSubscriptions.Run(apiLoginId, transactionKey);
                    GetListOfSubscriptions.GetListOfSubscriptionsExec(apiLoginId, transactionKey);
                    break;
                case "GetSubscriptionStatus":
                    //GetSubscriptionStatus.Run(apiLoginId, transactionKey, subscriptionId);
                    GetSubscriptionStatus.GetSubscriptionStatusExec(apiLoginId, transactionKey);
                    break;
                case "GetSubscription":
                    //GetSubscription.Run(apiLoginId, transactionKey, subscriptionId);
                    GetSubscription.GetSubscriptionExec(apiLoginId, transactionKey);
                    break;
                case "UpdateSubscription":
                    //UpdateSubscription.Run(apiLoginId, transactionKey, subscriptionId);
                    UpdateSubscription.UpdateSubscriptionExec(apiLoginId, transactionKey);
                    break;
                case "CreateCustomerProfile":
                    //CreateCustomerProfile.Run(apiLoginId, transactionKey, emailId);
                    CreateCustomerProfile.CreateCustomerProfileExec(apiLoginId, transactionKey);
                    break;
                case "CreateCustomerPaymentProfile":
                    //CreateCustomerPaymentProfile.Run(apiLoginId, transactionKey, customerProfileId);
                    CreateCustomerPaymentProfile.CreateCustomerPaymentProfileExec(apiLoginId, transactionKey);
                    break;
                case "GetUnsettledTransactionList":
                    //GetUnsettledTransactionList.Run(apiLoginId, transactionKey);
                    GetUnsettledTransactionList.GetUnsettledTransactionListExec(apiLoginId, transactionKey);
                    break;
                case "GetBatchStatistics":
                    //GetBatchStatistics.Run(apiLoginId, transactionKey);
                    GetBatchStatistics.GetBatchStatisticsExec(apiLoginId, transactionKey);
                    break;
                case "GetAccountUpdaterJobDetails":
                    //GetAccountUpdaterJobDetails.Run(apiLoginId, transactionKey);
                    GetAccountUpdaterJobDetails.GetAccountUpdaterJobDetailsExec(apiLoginId, transactionKey);
                    break;
                case "GetAccountUpdaterJobSummary":
                    //GetAccountUpdaterJobSummary.Run(apiLoginId, transactionKey);
                    GetAccountUpdaterJobSummary.GetAccountUpdaterJobSummaryExec(apiLoginId, transactionKey);
                    break;
                case "GetSettledBatchList":
                    //GetSettledBatchList.Run(apiLoginId, transactionKey);
                    GetSettledBatchList.GetSettledBatchListExec(apiLoginId, transactionKey);
                    break;
                case "UpdateSplitTenderGroup":
                    //UpdateSplitTenderGroup.Run(apiLoginId, transactionKey);
                    UpdateSplitTenderGroup.UpdateSplitTenderGroupExec(apiLoginId, transactionKey);
                    break;
                case "GetHeldTransactionList":
                    //GetHeldTransactionList.Run(apiLoginId, transactionKey);
                    GetHeldTransactionList.GetHeldTransactionListExec(apiLoginId, transactionKey);
                    break;
                case "ApproveOrDeclineHeldTransaction":
                    //ApproveOrDeclineHeldTransaction.Run(apiLoginId, transactionKey);
                    ApproveOrDeclineHeldTransaction.ApproveOrDeclineHeldTransactionExec(apiLoginId, transactionKey);
                    break;
                case "GetMerchantDetails":
                    //GetMerchantDetails.Run(apiLoginId, transactionKey);
                    GetMerchantDetails.GetMerchantDetailsExec(apiLoginId, transactionKey);
                    break;
                case "GetAnAcceptPaymentPage":
                    //GetAnAcceptPaymentPage.Run(apiLoginId, transactionKey, 12.23m);
                    GetAnAcceptPaymentPage.GetAnAcceptPaymentPageExec(apiLoginId, transactionKey);
                    break;
                default:
                    ShowUsage();
                    break;
            }
        }
    }
}
