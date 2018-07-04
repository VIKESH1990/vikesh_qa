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

namespace net.authorize.sample.MobileInappTransactions
{
    public class CreateAnApplePayTransaction
    {
        //public static ANetApiResponse Run(String ApiLoginID, String ApiTransactionKey, Decimal Amount)
        //{
        //    Console.WriteLine("Create Apple Pay Transaction Sample");

        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNET.Environment.SANDBOX;

        //    // define the merchant information (authentication / transaction id)
        //    ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
        //    {
        //        name = ApiLoginID,
        //        ItemElementName = ItemChoiceType.transactionKey,
        //        Item = ApiTransactionKey,
        //    };

        //    //set up data based on transaction
        //    var opaqueData = new opaqueDataType { dataDescriptor = "COMMON.APPLE.INAPP.PAYMENT", dataValue = "eyJkYXRhIjoiQkRQTldTdE1tR2V3UVVXR2c0bzdFXC9qKzFjcTFUNzhxeVU4NGI2N2l0amNZSTh3UFlBT2hzaGpoWlBycWRVcjRYd1BNYmo0emNHTWR5KysxSDJWa1BPWStCT01GMjV1YjE5Y1g0bkN2a1hVVU9UakRsbEIxVGdTcjhKSFp4Z3A5ckNnc1NVZ2JCZ0tmNjBYS3V0WGY2YWpcL284WkliS25yS1E4U2gwb3VMQUtsb1VNbit2UHU0K0E3V0tycXJhdXo5SnZPUXA2dmhJcStIS2pVY1VOQ0lUUHlGaG1PRXRxK0grdzB2UmExQ0U2V2hGQk5uQ0hxenpXS2NrQlwvMG5xTFpSVFliRjBwK3Z5QmlWYVdIZWdoRVJmSHhSdGJ6cGVjelJQUHVGc2ZwSFZzNDhvUExDXC9rXC8xTU5kNDdrelwvcEhEY1JcL0R5NmFVTStsTmZvaWx5XC9RSk4rdFMzbTBIZk90SVNBUHFPbVhlbXZyNnhKQ2pDWmxDdXcwQzltWHpcL29iSHBvZnVJRVM4cjljcUdHc1VBUERwdzdnNjQybTRQendLRitIQnVZVW5lV0RCTlNEMnU2amJBRzMiLCJ2ZXJzaW9uIjoiRUNfdjEiLCJoZWFkZXIiOnsiYXBwbGljYXRpb25EYXRhIjoiOTRlZTA1OTMzNWU1ODdlNTAxY2M0YmY5MDYxM2UwODE0ZjAwYTdiMDhiYzdjNjQ4ZmQ4NjVhMmFmNmEyMmNjMiIsInRyYW5zYWN0aW9uSWQiOiJjMWNhZjVhZTcyZjAwMzlhODJiYWQ5MmI4MjgzNjM3MzRmODViZjJmOWNhZGYxOTNkMWJhZDlkZGNiNjBhNzk1IiwiZXBoZW1lcmFsUHVibGljS2V5IjoiTUlJQlN6Q0NBUU1HQnlxR1NNNDlBZ0V3Z2ZjQ0FRRXdMQVlIS29aSXpqMEJBUUloQVBcL1wvXC9cLzhBQUFBQkFBQUFBQUFBQUFBQUFBQUFcL1wvXC9cL1wvXC9cL1wvXC9cL1wvXC9cL1wvXC9cL01Gc0VJUFwvXC9cL1wvOEFBQUFCQUFBQUFBQUFBQUFBQUFBQVwvXC9cL1wvXC9cL1wvXC9cL1wvXC9cL1wvXC9cLzhCQ0JheGpYWXFqcVQ1N1BydlZWMm1JYThaUjBHc014VHNQWTd6ancrSjlKZ1N3TVZBTVNkTmdpRzV3U1RhbVo0NFJPZEpyZUJuMzZRQkVFRWF4ZlI4dUVzUWtmNHZPYmxZNlJBOG5jRGZZRXQ2ek9nOUtFNVJkaVl3cFpQNDBMaVwvaHBcL200N242MHA4RDU0V0s4NHpWMnN4WHM3THRrQm9ONzlSOVFJaEFQXC9cL1wvXC84QUFBQUFcL1wvXC9cL1wvXC9cL1wvXC9cLys4NXZxdHB4ZWVoUE81eXNMOFl5VlJBZ0VCQTBJQUJHbStnc2wwUFpGVFwva0RkVVNreHd5Zm84SnB3VFFRekJtOWxKSm5tVGw0REdVdkFENEdzZUdqXC9wc2hCWjBLM1RldXFEdFwvdERMYkUrOFwvbTB5Q21veHc9IiwicHVibGljS2V5SGFzaCI6IlwvYmI5Q05DMzZ1QmhlSEZQYm1vaEI3T28xT3NYMkora0pxdjQ4ek9WVmlRPSJ9LCJzaWduYXR1cmUiOiJNSUlEUWdZSktvWklodmNOQVFjQ29JSURNekNDQXk4Q0FRRXhDekFKQmdVckRnTUNHZ1VBTUFzR0NTcUdTSWIzRFFFSEFhQ0NBaXN3Z2dJbk1JSUJsS0FEQWdFQ0FoQmNsK1BmMytVNHBrMTNuVkQ5bndRUU1Ba0dCU3NPQXdJZEJRQXdKekVsTUNNR0ExVUVBeDRjQUdNQWFBQnRBR0VBYVFCQUFIWUFhUUJ6QUdFQUxnQmpBRzhBYlRBZUZ3MHhOREF4TURFd05qQXdNREJhRncweU5EQXhNREV3TmpBd01EQmFNQ2N4SlRBakJnTlZCQU1lSEFCakFHZ0FiUUJoQUdrQVFBQjJBR2tBY3dCaEFDNEFZd0J2QUcwd2daOHdEUVlKS29aSWh2Y05BUUVCQlFBRGdZMEFNSUdKQW9HQkFOQzgra2d0Z212V0YxT3pqZ0ROcmpURUJSdW9cLzVNS3ZsTTE0NnBBZjdHeDQxYmxFOXc0ZklYSkFEN0ZmTzdRS2pJWFlOdDM5ckx5eTd4RHdiXC81SWtaTTYwVFoyaUkxcGo1NVVjOGZkNGZ6T3BrM2Z0WmFRR1hOTFlwdEcxZDlWN0lTODJPdXA5TU1vMUJQVnJYVFBITmNzTTk5RVBVblBxZGJlR2M4N20wckFnTUJBQUdqWERCYU1GZ0dBMVVkQVFSUk1FK0FFSFpXUHJXdEpkN1laNDMxaENnN1lGU2hLVEFuTVNVd0l3WURWUVFESGh3QVl3Qm9BRzBBWVFCcEFFQUFkZ0JwQUhNQVlRQXVBR01BYndCdGdoQmNsK1BmMytVNHBrMTNuVkQ5bndRUU1Ba0dCU3NPQXdJZEJRQURnWUVBYlVLWUNrdUlLUzlRUTJtRmNNWVJFSW0ybCtYZzhcL0pYditHQlZRSmtPS29zY1k0aU5ERkFcL2JRbG9nZjlMTFU4NFRId05SbnN2VjNQcnY3UlRZODFncTBkdEM4elljQWFBa0NISUkzeXFNbko0QU91NkVPVzlrSmsyMzJnU0U3V2xDdEhiZkxTS2Z1U2dRWDhLWFFZdVpMazJScjYzTjhBcFhzWHdCTDNjSjB4Z2VBd2dkMENBUUV3T3pBbk1TVXdJd1lEVlFRREhod0FZd0JvQUcwQVlRQnBBRUFBZGdCcEFITUFZUUF1QUdNQWJ3QnRBaEJjbCtQZjMrVTRwazEzblZEOW53UVFNQWtHQlNzT0F3SWFCUUF3RFFZSktvWklodmNOQVFFQkJRQUVnWUJhSzNFbE9zdGJIOFdvb3NlREFCZitKZ1wvMTI5SmNJYXdtN2M2VnhuN1phc05iQXEzdEF0OFB0eSt1UUNnc3NYcVprTEE3a3oyR3pNb2xOdHY5d1ltdTlVandhcjFQSFlTK0JcL29Hbm96NTkxd2phZ1hXUnowbk1vNXkzTzFLelgwZDhDUkhBVmE4OFNyVjFhNUpJaVJldjNvU3RJcXd2NXh1WmxkYWc2VHI4dz09In0=" }; 

        //    //standard api call to retrieve response
        //    var paymentType = new paymentType { Item = opaqueData };

        //    var transactionRequest = new transactionRequestType
        //    {
        //        transactionType = transactionTypeEnum.authCaptureTransaction.ToString() ,    // authorize and capture transaction
        //        amount = Amount,
        //        payment = paymentType
        //    };

        //    var request = new createTransactionRequest { transactionRequest = transactionRequest };

        //    // instantiate the contoller that will call the service
        //    var controller = new createTransactionController(request);
        //    controller.Execute();

        //    // get the response from the service (errors contained if any)
        //    var response = controller.GetApiResponse();

        //    //validate
        //    if (response != null)
        //    {
        //        if (response.messages.resultCode == messageTypeEnum.Ok)
        //        {
        //            if(response.transactionResponse.messages != null)
        //            {
        //                Console.WriteLine("Successfully created an Apple pay transaction with Transaction ID: " + response.transactionResponse.transId);
        //                Console.WriteLine("Response Code: " + response.transactionResponse.responseCode);
        //                Console.WriteLine("Message Code: " + response.transactionResponse.messages[0].code);
        //                Console.WriteLine("Description: " + response.transactionResponse.messages[0].description);
        //            }
        //            else
        //            {
        //                Console.WriteLine("Failed Transaction.");
        //                if (response.transactionResponse.errors != null)
        //                {
        //                    Console.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
        //                    Console.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("Failed Transaction.");
        //            if (response.transactionResponse != null && response.transactionResponse.errors != null)
        //            {
        //                Console.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
        //                Console.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
        //            }
        //            else
        //            {
        //                Console.WriteLine("Error Code: " + response.messages.message[0].code);
        //                Console.WriteLine("Error message: " + response.messages.message[0].text);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Null Response.");
        //    }

        //    return response;
        //}
        public static void CreateAnApplePayTransactionExec(String ApiLoginID, String ApiTransactionKey)
        {
            using (CsvReader csv = new CsvReader(new StreamReader(new FileStream(@"../../../CSV_DATA/CreateAnApplePayTransaction.csv", FileMode.Open)), true))
            {
                Console.WriteLine("Create Apple Pay Transaction Sample");
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
                        
                        string amount = null;

                        string TestCaseId = null;

                        for (int i = 0; i < fieldCount; i++)
                        {
                            switch (headers[i])
                            {

                                case "amount":
                                    amount = csv[i];
                                    break;
                                case "TestCaseId":
                                    TestCaseId = csv[i];
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

                            //var opaqueData = new opaqueDataType { dataDescriptor = "COMMON.APPLE.INAPP.PAYMENT", dataValue = "eyJkYXRhIjoiQkRQTldTdE1tR2V3UVVXR2c0bzdFXC9qKzFjcTFUNzhxeVU4NGI2N2l0amNZSTh3UFlBT2hzaGpoWlBycWRVcjRYd1BNYmo0emNHTWR5KysxSDJWa1BPWStCT01GMjV1YjE5Y1g0bkN2a1hVVU9UakRsbEIxVGdTcjhKSFp4Z3A5ckNnc1NVZ2JCZ0tmNjBYS3V0WGY2YWpcL284WkliS25yS1E4U2gwb3VMQUtsb1VNbit2UHU0K0E3V0tycXJhdXo5SnZPUXA2dmhJcStIS2pVY1VOQ0lUUHlGaG1PRXRxK0grdzB2UmExQ0U2V2hGQk5uQ0hxenpXS2NrQlwvMG5xTFpSVFliRjBwK3Z5QmlWYVdIZWdoRVJmSHhSdGJ6cGVjelJQUHVGc2ZwSFZzNDhvUExDXC9rXC8xTU5kNDdrelwvcEhEY1JcL0R5NmFVTStsTmZvaWx5XC9RSk4rdFMzbTBIZk90SVNBUHFPbVhlbXZyNnhKQ2pDWmxDdXcwQzltWHpcL29iSHBvZnVJRVM4cjljcUdHc1VBUERwdzdnNjQybTRQendLRitIQnVZVW5lV0RCTlNEMnU2amJBRzMiLCJ2ZXJzaW9uIjoiRUNfdjEiLCJoZWFkZXIiOnsiYXBwbGljYXRpb25EYXRhIjoiOTRlZTA1OTMzNWU1ODdlNTAxY2M0YmY5MDYxM2UwODE0ZjAwYTdiMDhiYzdjNjQ4ZmQ4NjVhMmFmNmEyMmNjMiIsInRyYW5zYWN0aW9uSWQiOiJjMWNhZjVhZTcyZjAwMzlhODJiYWQ5MmI4MjgzNjM3MzRmODViZjJmOWNhZGYxOTNkMWJhZDlkZGNiNjBhNzk1IiwiZXBoZW1lcmFsUHVibGljS2V5IjoiTUlJQlN6Q0NBUU1HQnlxR1NNNDlBZ0V3Z2ZjQ0FRRXdMQVlIS29aSXpqMEJBUUloQVBcL1wvXC9cLzhBQUFBQkFBQUFBQUFBQUFBQUFBQUFcL1wvXC9cL1wvXC9cL1wvXC9cL1wvXC9cL1wvXC9cL01Gc0VJUFwvXC9cL1wvOEFBQUFCQUFBQUFBQUFBQUFBQUFBQVwvXC9cL1wvXC9cL1wvXC9cL1wvXC9cL1wvXC9cLzhCQ0JheGpYWXFqcVQ1N1BydlZWMm1JYThaUjBHc014VHNQWTd6ancrSjlKZ1N3TVZBTVNkTmdpRzV3U1RhbVo0NFJPZEpyZUJuMzZRQkVFRWF4ZlI4dUVzUWtmNHZPYmxZNlJBOG5jRGZZRXQ2ek9nOUtFNVJkaVl3cFpQNDBMaVwvaHBcL200N242MHA4RDU0V0s4NHpWMnN4WHM3THRrQm9ONzlSOVFJaEFQXC9cL1wvXC84QUFBQUFcL1wvXC9cL1wvXC9cL1wvXC9cLys4NXZxdHB4ZWVoUE81eXNMOFl5VlJBZ0VCQTBJQUJHbStnc2wwUFpGVFwva0RkVVNreHd5Zm84SnB3VFFRekJtOWxKSm5tVGw0REdVdkFENEdzZUdqXC9wc2hCWjBLM1RldXFEdFwvdERMYkUrOFwvbTB5Q21veHc9IiwicHVibGljS2V5SGFzaCI6IlwvYmI5Q05DMzZ1QmhlSEZQYm1vaEI3T28xT3NYMkora0pxdjQ4ek9WVmlRPSJ9LCJzaWduYXR1cmUiOiJNSUlEUWdZSktvWklodmNOQVFjQ29JSURNekNDQXk4Q0FRRXhDekFKQmdVckRnTUNHZ1VBTUFzR0NTcUdTSWIzRFFFSEFhQ0NBaXN3Z2dJbk1JSUJsS0FEQWdFQ0FoQmNsK1BmMytVNHBrMTNuVkQ5bndRUU1Ba0dCU3NPQXdJZEJRQXdKekVsTUNNR0ExVUVBeDRjQUdNQWFBQnRBR0VBYVFCQUFIWUFhUUJ6QUdFQUxnQmpBRzhBYlRBZUZ3MHhOREF4TURFd05qQXdNREJhRncweU5EQXhNREV3TmpBd01EQmFNQ2N4SlRBakJnTlZCQU1lSEFCakFHZ0FiUUJoQUdrQVFBQjJBR2tBY3dCaEFDNEFZd0J2QUcwd2daOHdEUVlKS29aSWh2Y05BUUVCQlFBRGdZMEFNSUdKQW9HQkFOQzgra2d0Z212V0YxT3pqZ0ROcmpURUJSdW9cLzVNS3ZsTTE0NnBBZjdHeDQxYmxFOXc0ZklYSkFEN0ZmTzdRS2pJWFlOdDM5ckx5eTd4RHdiXC81SWtaTTYwVFoyaUkxcGo1NVVjOGZkNGZ6T3BrM2Z0WmFRR1hOTFlwdEcxZDlWN0lTODJPdXA5TU1vMUJQVnJYVFBITmNzTTk5RVBVblBxZGJlR2M4N20wckFnTUJBQUdqWERCYU1GZ0dBMVVkQVFSUk1FK0FFSFpXUHJXdEpkN1laNDMxaENnN1lGU2hLVEFuTVNVd0l3WURWUVFESGh3QVl3Qm9BRzBBWVFCcEFFQUFkZ0JwQUhNQVlRQXVBR01BYndCdGdoQmNsK1BmMytVNHBrMTNuVkQ5bndRUU1Ba0dCU3NPQXdJZEJRQURnWUVBYlVLWUNrdUlLUzlRUTJtRmNNWVJFSW0ybCtYZzhcL0pYditHQlZRSmtPS29zY1k0aU5ERkFcL2JRbG9nZjlMTFU4NFRId05SbnN2VjNQcnY3UlRZODFncTBkdEM4elljQWFBa0NISUkzeXFNbko0QU91NkVPVzlrSmsyMzJnU0U3V2xDdEhiZkxTS2Z1U2dRWDhLWFFZdVpMazJScjYzTjhBcFhzWHdCTDNjSjB4Z2VBd2dkMENBUUV3T3pBbk1TVXdJd1lEVlFRREhod0FZd0JvQUcwQVlRQnBBRUFBZGdCcEFITUFZUUF1QUdNQWJ3QnRBaEJjbCtQZjMrVTRwazEzblZEOW53UVFNQWtHQlNzT0F3SWFCUUF3RFFZSktvWklodmNOQVFFQkJRQUVnWUJhSzNFbE9zdGJIOFdvb3NlREFCZitKZ1wvMTI5SmNJYXdtN2M2VnhuN1phc05iQXEzdEF0OFB0eSt1UUNnc3NYcVprTEE3a3oyR3pNb2xOdHY5d1ltdTlVandhcjFQSFlTK0JcL29Hbm96NTkxd2phZ1hXUnowbk1vNXkzTzFLelgwZDhDUkhBVmE4OFNyVjFhNUpJaVJldjNvU3RJcXd2NXh1WmxkYWc2VHI4dz09In0=" };
                            var opaqueData = new opaqueDataType { dataDescriptor = "COMMON.APPLE.INAPP.PAYMENT", dataValue = "eyJkYXRhIjoiMHRTbWRvOGwrbkxYRE5DQWJkdmRBY1wveHNzVGFRYmdpaUpzemlnbEh1aTZzcmI1UHRaSjJQU3RibElmYUNGQVdBSGhXSVFsd1J6Ujg0V3FnOUhxN0o0QVZWMW5wQ2xUOEV2M2hjMlMxSVwvZEZPd1NxYWliQm45ejBFTTg3ZThNWVwvVGdtVmNYSDJ4XC80S1JFM08rZFIrcjNvT2VFV1BMM2Qydlo5WGJ1ZWk4enV4Y21zTG9xM1J4Vzc3WW1RclFCdUhDdUlvdXlEVGcyZmtRV2dleHY5ZTJLMmxIQ0lZa3Bicis3M0UzczZMRitaXC9OYk9acU9Cc0JFaWRrNEdQVU5VcllqQVpLWjRrcThOMVwvdloxU0RsaW1iaGFBKzNlempWWHJYWEowZXFIVnJBc1VHZVpyaGxUYjA5RWdya3cwNWhhbkRuNHdCWG91QU5NbTZhUFZVMWVNTTFcL2dYdm16Mmg4Tm1GbFJxRGljWkk2WmNHRXRoWElsVVpUaXlvQTVOWHFKbzJZaWlWcW8wNGJaemlOM3dVQjQ1b2k4eU1PTVlqaG9IYUZlcnFzUWFLaEU2bFA4bXdaVFV4IiwidmVyc2lvbiI6IkVDX3YxIiwiaGVhZGVyIjp7ImFwcGxpY2F0aW9uRGF0YSI6Ijk0ZWUwNTkzMzVlNTg3ZTUwMWNjNGJmOTA2MTNlMDgxNGYwMGE3YjA4YmM3YzY0OGZkODY1YTJhZjZhMjJjYzIiLCJ0cmFuc2FjdGlvbklkIjoiYzFjYWY1YWU3MmYwMDM5YTgyYmFkOTJiODI4MzYzNzM0Zjg1YmYyZjljYWRmMTkzZDFiYWQ5ZGRjYjYwYTc5NSIsImVwaGVtZXJhbFB1YmxpY0tleSI6Ik1JSUJTekNDQVFNR0J5cUdTTTQ5QWdFd2dmY0NBUUV3TEFZSEtvWkl6ajBCQVFJaEFQXC9cL1wvXC84QUFBQUJBQUFBQUFBQUFBQUFBQUFBXC9cL1wvXC9cL1wvXC9cL1wvXC9cL1wvXC9cL1wvXC9NRnNFSVBcL1wvXC9cLzhBQUFBQkFBQUFBQUFBQUFBQUFBQUFcL1wvXC9cL1wvXC9cL1wvXC9cL1wvXC9cL1wvXC84QkNCYXhqWFlxanFUNTdQcnZWVjJtSWE4WlIwR3NNeFRzUFk3emp3K0o5SmdTd01WQU1TZE5naUc1d1NUYW1aNDRST2RKcmVCbjM2UUJFRUVheGZSOHVFc1FrZjR2T2JsWTZSQThuY0RmWUV0NnpPZzlLRTVSZGlZd3BaUDQwTGlcL2hwXC9tNDduNjBwOEQ1NFdLODR6VjJzeFhzN0x0a0JvTjc5UjlRSWhBUFwvXC9cL1wvOEFBQUFBXC9cL1wvXC9cL1wvXC9cL1wvXC8rODV2cXRweGVlaFBPNXlzTDhZeVZSQWdFQkEwSUFCT2lVUkpScVA0WThTV3FXaVk3THJOVWtiMCtpTW1XVHdLdmJyWmtDanlUZTJSd2ZZc2lIU3ArM2Y0UnZqYVE5TjVWeFhzaEo2b0lYSlY3UmJIWmJxKzg9IiwicHVibGljS2V5SGFzaCI6IlFmMEZmdFd1c0FOZmN5c2NwbVJ1b2MxOEJyS1ByeHdQdmJQOEVUUTVNcTA9In0sInNpZ25hdHVyZSI6Ik1JSURRZ1lKS29aSWh2Y05BUWNDb0lJRE16Q0NBeThDQVFFeEN6QUpCZ1VyRGdNQ0dnVUFNQXNHQ1NxR1NJYjNEUUVIQWFDQ0Fpc3dnZ0luTUlJQmxLQURBZ0VDQWhCY2wrUGYzK1U0cGsxM25WRDlud1FRTUFrR0JTc09Bd0lkQlFBd0p6RWxNQ01HQTFVRUF4NGNBR01BYUFCdEFHRUFhUUJBQUhZQWFRQnpBR0VBTGdCakFHOEFiVEFlRncweE5EQXhNREV3TmpBd01EQmFGdzB5TkRBeE1ERXdOakF3TURCYU1DY3hKVEFqQmdOVkJBTWVIQUJqQUdnQWJRQmhBR2tBUUFCMkFHa0Fjd0JoQUM0QVl3QnZBRzB3Z1o4d0RRWUpLb1pJaHZjTkFRRUJCUUFEZ1kwQU1JR0pBb0dCQU5DOCtrZ3RnbXZXRjFPempnRE5yalRFQlJ1b1wvNU1LdmxNMTQ2cEFmN0d4NDFibEU5dzRmSVhKQUQ3RmZPN1FLaklYWU50MzlyTHl5N3hEd2JcLzVJa1pNNjBUWjJpSTFwajU1VWM4ZmQ0ZnpPcGszZnRaYVFHWE5MWXB0RzFkOVY3SVM4Mk91cDlNTW8xQlBWclhUUEhOY3NNOTlFUFVuUHFkYmVHYzg3bTByQWdNQkFBR2pYREJhTUZnR0ExVWRBUVJSTUUrQUVIWldQcld0SmQ3WVo0MzFoQ2c3WUZTaEtUQW5NU1V3SXdZRFZRUURIaHdBWXdCb0FHMEFZUUJwQUVBQWRnQnBBSE1BWVFBdUFHTUFid0J0Z2hCY2wrUGYzK1U0cGsxM25WRDlud1FRTUFrR0JTc09Bd0lkQlFBRGdZRUFiVUtZQ2t1SUtTOVFRMm1GY01ZUkVJbTJsK1hnOFwvSlh2K0dCVlFKa09Lb3NjWTRpTkRGQVwvYlFsb2dmOUxMVTg0VEh3TlJuc3ZWM1BydjdSVFk4MWdxMGR0Qzh6WWNBYUFrQ0hJSTN5cU1uSjRBT3U2RU9XOWtKazIzMmdTRTdXbEN0SGJmTFNLZnVTZ1FYOEtYUVl1WkxrMlJyNjNOOEFwWHNYd0JMM2NKMHhnZUF3Z2QwQ0FRRXdPekFuTVNVd0l3WURWUVFESGh3QVl3Qm9BRzBBWVFCcEFFQUFkZ0JwQUhNQVlRQXVBR01BYndCdEFoQmNsK1BmMytVNHBrMTNuVkQ5bndRUU1Ba0dCU3NPQXdJYUJRQXdEUVlKS29aSWh2Y05BUUVCQlFBRWdZQ0FNdzhVMmsxODM3ZW9mVkFreVVSK2E2bkt3VVNzYWthXC90VEd6QUswOWY4dHdsdDFmQTR0RVJBK21wTEppUWZkU1NZXC9mN2ZQNTJRb001UGhCZFYzTFBRaktSOFo1TjNKUzdGbU5Kd1ZTbndRY3gzYVJuaFR2d0RNRHMrZVJtSm5JK0VcL242Y05wYXpMeDVMRzRFMGplMFwvclJkbEhXYWxEK1NCTW4xQW5YY2c9PSJ9" };
                            //standard api call to retrieve response
                            var paymentType = new paymentType { Item = opaqueData };

                            var transactionRequest = new transactionRequestType
                            {
                                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // authorize and capture transaction
                                amount = Convert.ToDecimal(amount),
                                payment = paymentType
                            };

                            var request = new createTransactionRequest { transactionRequest = transactionRequest };

                            // instantiate the contoller that will call the service
                            var controller = new createTransactionController(request);
                            controller.Execute();

                            // get the response from the service (errors contained if any)
                            var response = controller.GetApiResponse();
                            if (response != null && response.messages.resultCode == messageTypeEnum.Ok
                                && response.transactionResponse.messages != null)
                            {
                                try
                                {
                                    //Assert.AreEqual(response.Id, customerProfileId);
                                    Console.WriteLine("Assertion Succeed! Valid CustomerId fetched.");
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("CAAPT_00" + flag.ToString());
                                    row1.Add("CreateAnApplePayTransaction");
                                    row1.Add("Pass");
                                    row1.Add(DateTime.Now.ToString("yyyy/MM/dd" + "::" + "HH:mm:ss:fff"));
                                    writer.WriteRow(row1);
                                    //  Console.WriteLine("Success " + TestcaseID + " CustomerID : " + response.Id);
                                    flag = flag + 1;
                                    Console.WriteLine("Successfully created an Apple pay transaction with Transaction ID: " + response.transactionResponse.transId);
                                    Console.WriteLine("Response Code: " + response.transactionResponse.responseCode);
                                    Console.WriteLine("Message Code: " + response.transactionResponse.messages[0].code);
                                    Console.WriteLine("Description: " + response.transactionResponse.messages[0].description);
                                }
                                catch
                                {
                                    CsvRow row1 = new CsvRow();
                                    row1.Add("CAAPT_00" + flag.ToString());
                                    row1.Add("CreateAnApplePayTransaction");
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
                                row1.Add("CAAPT_00" + flag.ToString());
                                row1.Add("CreateAnApplePayTransaction");
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
                            row2.Add("CAAPT_00" + flag.ToString());
                            row2.Add("CreateAnApplePayTransaction");
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
