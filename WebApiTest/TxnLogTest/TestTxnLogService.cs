using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.TxnLog;
using ESUN.AGD.WebApi.Application.TxnLog.Contract;
using ESUN.AGD.WebApi.Models;
using ESUN.AGD.WebApi.Application.User;
using ESUN.AGD.WebApi.Application.AutoNextNum;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Linq;
using System.Threading.Tasks;


namespace ESUN.AGD.WebApi.Test.TxnLogTest
{
    [TestFixture]
    public class TestTxnLogService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetTxnLogTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbTxnLog, object>("agdSp.uspTxnLogGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbTxnLog { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            TxnLogService TxnLogService = 
                new TxnLogService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await TxnLogService.GetTxnLog(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspTxnLogGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetTxnLogTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbTxnLog, object>("agdSp.uspTxnLogGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            TxnLogService TxnLogService = new TxnLogService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await TxnLogService.GetTxnLog(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbTxnLog, object>("agdSp.uspTxnLogGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryTxnLogTest()
        {  //arrange
            var queryReq = new TxnLogQueryRequest { 						contactID = 0,
						custKey = 0,
						customerID = "",
						txnApiCode = "FindCustomerInfo",
						creator = "13370", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbTxnLog, object>
                (storeProcedure: "agdSp.uspTxnLogQuery", Arg.Any<object>())
               .Returns(new TbTxnLog[] { 
                    new TbTxnLog { 
						contactID = 0,
						custKey = 0,
						customerID = "",
						txnApiCode = "FindCustomerInfo",
						creator = "13370",
                        Total = 2
                    },
                    new TbTxnLog { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/TxnLog/query";
            
            TxnLogService TxnLogService = new 
                TxnLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await TxnLogService.QueryTxnLog(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryTxnLogTestFailed()
        {  //arrange
            var queryReq = new TxnLogQueryRequest {  
						contactID = 0,
						custKey = 0,
						customerID = "",
						txnApiCode = "FindCustomerInfo",
						creator = "13370",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbTxnLog, object>("agdSp.uspTxnLogQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/TxnLog/query";
            TxnLogService TxnLogService = new TxnLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await TxnLogService.QueryTxnLog(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspTxnLogGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertTxnLogTest()
        {  //arrange
            var insertReq = new TxnLogInsertRequest
            {
				contactID = 0,
				custKey = 0,
				customerID = "",
				customerName = " ",
				txnApiCode = "FindCustomerInfo",
				txnName = "顧客資訊查詢",
				accountNumber = "None",
				txnResultID = "UP0090_00_0000",
				txnResultDescr = "成功",
				txnRequest = "{"requestBody":{"criteria":{"customerName":" "},"fieldSet":["personCertNo","residencePermitCertNo","businessRegCertNo","passportNo","customerName","partyEnglishName","customerBirthday","residencePermitCertStartDate","residencePermitCertEndDate","genderCode","industryTypeCode","serveCompanyName","principalPartyCertNo","principalName","principalBirthday","foreignExchangeRoleCode","customerKycTypeCode","circiKey","residenceAddrPostalCode","residenceAddrCountryCode","residenceAddrLocality","residenceAddrAdminArea","residenceAddr","contactAddrPostalCode","contactAddrCountryCode","contactAddrLocality","contactAddrAdminArea","contactAddr","cardBillAddr","companyAddrPostalCode","companyAddrLocality","companyAddrAdminArea","companyAddr","contactPhone","txnPhone","companyTel","companyTelExtension","contactTel","residenceTel","email","cardBillAddrPostalCode","cardBillAddrLocality","cardBillAddrAdminArea","cardBillAddrEFlag","stopUseCustomerDataFlag","crossSellingFlag","dmSaleFlag","smsSaleFlag","emailSaleFlag","phoneSaleFlag","cardStatusCode","highestCardLevelCode","autoChargeFlag","otpServiceFlag","gibVersionCode","performanceDptCode","fcBranchCode","fcSupervisorEmpNo","belongFcEmpNo","pibVersionCode","pibStatusCode","efingoMemberCode","customerStatusCode","creditCardKey","partyNameChangeFlag"]},"header":{"msgNo":"OA0105_01_20220830173104_AQG","txnCode":"findCustomerInfo","txnTime":"2022-08-30 17:31:04","senderCode":"OA0105_01","receiverCode":"UP0090_00","operatorCode":"13370","unitCode":"9912","authorizerCode":null}}",
				txnResponse = "{"requestModel":{"requestBody":{"criteria":{"customerName":" "},"fieldSet":["personCertNo","residencePermitCertNo","businessRegCertNo","passportNo","customerName","partyEnglishName","customerBirthday","residencePermitCertStartDate","residencePermitCertEndDate","genderCode","industryTypeCode","serveCompanyName","principalPartyCertNo","principalName","principalBirthday","foreignExchangeRoleCode","customerKycTypeCode","circiKey","residenceAddrPostalCode","residenceAddrCountryCode","residenceAddrLocality","residenceAddrAdminArea","residenceAddr","contactAddrPostalCode","contactAddrCountryCode","contactAddrLocality","contactAddrAdminArea","contactAddr","cardBillAddr","companyAddrPostalCode","companyAddrLocality","companyAddrAdminArea","companyAddr","contactPhone","txnPhone","companyTel","companyTelExtension","contactTel","residenceTel","email","cardBillAddrPostalCode","cardBillAddrLocality","cardBillAddrAdminArea","cardBillAddrEFlag","stopUseCustomerDataFlag","crossSellingFlag","dmSaleFlag","smsSaleFlag","emailSaleFlag","phoneSaleFlag","cardStatusCode","highestCardLevelCode","autoChargeFlag","otpServiceFlag","gibVersionCode","performanceDptCode","fcBranchCode","fcSupervisorEmpNo","belongFcEmpNo","pibVersionCode","pibStatusCode","efingoMemberCode","customerStatusCode","creditCardKey","partyNameChangeFlag"]},"header":{"msgNo":"OA0105_01_20220830173104_AQG","txnCode":"findCustomerInfo","txnTime":"2022-08-30 17:31:04","senderCode":"OA0105_01","receiverCode":"UP0090_00","operatorCode":"13370","unitCode":"9912","authorizerCode":null}},"resultBody":null,"tid":"592c115a152d0e80","resultCode":"UP0090_00_0000","resultDescription":"成功","errorMessages":[]}",
				creator = "13370",
				creatorName = "傅O芳",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspTxnLogInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            TxnLogService TxnLogService = new
                TxnLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await TxnLogService.InsertTxnLog(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertTxnLogTestFailed()
        {  //arrange
            var insertReq = new TxnLogInsertRequest
            {
				contactID = 0,
				custKey = 0,
				customerID = "",
				customerName = " ",
				txnApiCode = "FindCustomerInfo",
				txnName = "顧客資訊查詢",
				accountNumber = "None",
				txnResultID = "UP0090_00_0000",
				txnResultDescr = "成功",
				txnRequest = "{"requestBody":{"criteria":{"customerName":" "},"fieldSet":["personCertNo","residencePermitCertNo","businessRegCertNo","passportNo","customerName","partyEnglishName","customerBirthday","residencePermitCertStartDate","residencePermitCertEndDate","genderCode","industryTypeCode","serveCompanyName","principalPartyCertNo","principalName","principalBirthday","foreignExchangeRoleCode","customerKycTypeCode","circiKey","residenceAddrPostalCode","residenceAddrCountryCode","residenceAddrLocality","residenceAddrAdminArea","residenceAddr","contactAddrPostalCode","contactAddrCountryCode","contactAddrLocality","contactAddrAdminArea","contactAddr","cardBillAddr","companyAddrPostalCode","companyAddrLocality","companyAddrAdminArea","companyAddr","contactPhone","txnPhone","companyTel","companyTelExtension","contactTel","residenceTel","email","cardBillAddrPostalCode","cardBillAddrLocality","cardBillAddrAdminArea","cardBillAddrEFlag","stopUseCustomerDataFlag","crossSellingFlag","dmSaleFlag","smsSaleFlag","emailSaleFlag","phoneSaleFlag","cardStatusCode","highestCardLevelCode","autoChargeFlag","otpServiceFlag","gibVersionCode","performanceDptCode","fcBranchCode","fcSupervisorEmpNo","belongFcEmpNo","pibVersionCode","pibStatusCode","efingoMemberCode","customerStatusCode","creditCardKey","partyNameChangeFlag"]},"header":{"msgNo":"OA0105_01_20220830173104_AQG","txnCode":"findCustomerInfo","txnTime":"2022-08-30 17:31:04","senderCode":"OA0105_01","receiverCode":"UP0090_00","operatorCode":"13370","unitCode":"9912","authorizerCode":null}}",
				txnResponse = "{"requestModel":{"requestBody":{"criteria":{"customerName":" "},"fieldSet":["personCertNo","residencePermitCertNo","businessRegCertNo","passportNo","customerName","partyEnglishName","customerBirthday","residencePermitCertStartDate","residencePermitCertEndDate","genderCode","industryTypeCode","serveCompanyName","principalPartyCertNo","principalName","principalBirthday","foreignExchangeRoleCode","customerKycTypeCode","circiKey","residenceAddrPostalCode","residenceAddrCountryCode","residenceAddrLocality","residenceAddrAdminArea","residenceAddr","contactAddrPostalCode","contactAddrCountryCode","contactAddrLocality","contactAddrAdminArea","contactAddr","cardBillAddr","companyAddrPostalCode","companyAddrLocality","companyAddrAdminArea","companyAddr","contactPhone","txnPhone","companyTel","companyTelExtension","contactTel","residenceTel","email","cardBillAddrPostalCode","cardBillAddrLocality","cardBillAddrAdminArea","cardBillAddrEFlag","stopUseCustomerDataFlag","crossSellingFlag","dmSaleFlag","smsSaleFlag","emailSaleFlag","phoneSaleFlag","cardStatusCode","highestCardLevelCode","autoChargeFlag","otpServiceFlag","gibVersionCode","performanceDptCode","fcBranchCode","fcSupervisorEmpNo","belongFcEmpNo","pibVersionCode","pibStatusCode","efingoMemberCode","customerStatusCode","creditCardKey","partyNameChangeFlag"]},"header":{"msgNo":"OA0105_01_20220830173104_AQG","txnCode":"findCustomerInfo","txnTime":"2022-08-30 17:31:04","senderCode":"OA0105_01","receiverCode":"UP0090_00","operatorCode":"13370","unitCode":"9912","authorizerCode":null}},"resultBody":null,"tid":"592c115a152d0e80","resultCode":"UP0090_00_0000","resultDescription":"成功","errorMessages":[]}",
				creator = "13370",
				creatorName = "傅O芳",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspTxnLogInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            TxnLogService TxnLogService = new
                TxnLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await TxnLogService.InsertTxnLog(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateTxnLogTest()
        {  //arrange
            var updateReq = new TxnLogUpdateRequest
            {
				contactID = 54,
				custKey = 8,
				customerID = "Y223276666",
				customerName = "測試陳怡伶",
				txnApiCode = "UpdateCustContact",
				txnName = "銀行顧客聯絡方式變更",
				accountNumber = "None",
				txnResultID = "0000",
				txnResultDescr = "交易成功",
				txnRequest = "{"requestBody":{"header":{"infoAssetsNo":"OA0105","oriTradeSeqNo":"0AP1837","txIdInAP":"updateCustContact","txId":"updateCustContact","branch":{"branchCode":"9912"},"teller":{"tellerId":"admin"},"customerInfo":{"cik":"Y223276666"}},"model":{"customerCirciKey":"Y223276666","residentialPhoneNumber":"0281231313","residentialNumberExtension":"1234","companyPhoneNumber":"0285121313","companyExtensionNumber":"2277","mobilePhoneNumber":"0919241414","residentialAddressDetail":"臺北市松山區吉祥里１１鄰光復北路１１巷９７號１１樓","residentialCity":"","residentialArea":"","residentialZip":"999","permanentPhoneNumber":"0221236789"}},"header":{"msgNo":"OA0105_20220922214839_ppw","txnCode":"updateCustContact","txnTime":"2022-09-22 21:48:39","senderCode":"OA0105","receiverCode":"TS0116","operatorCode":"admin","unitCode":"9912","authorizerCode":null}}",
				txnResponse = "{"requestModel":{"requestBody":{"header":{"infoAssetsNo":"OA0105","oriTradeSeqNo":"0AP1837","txIdInAP":"updateCustContact","txId":"updateCustContact","branch":{"branchCode":"9912"},"teller":{"tellerId":"admin"},"customerInfo":{"cik":"Y223276666"}},"model":{"customerCirciKey":"Y223276666","residentialPhoneNumber":"0281231313","residentialNumberExtension":"1234","companyPhoneNumber":"0285121313","companyExtensionNumber":"2277","mobilePhoneNumber":"0919241414","residentialAddressDetail":"臺北市松山區吉祥里１１鄰光復北路１１巷９７號１１樓","residentialCity":"","residentialArea":"","residentialZip":"999","permanentPhoneNumber":"0221236789"}},"header":{"msgNo":"OA0105_20220922214839_ppw","txnCode":"updateCustContact","txnTime":"2022-09-22 21:48:39","senderCode":"OA0105","receiverCode":"TS0116","operatorCode":"admin","unitCode":"9912","authorizerCode":null}},"resultBody":null,"tid":"45e5919571e333a3","resultCode":"0000","resultDescription":"交易成功"}",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspTxnLogUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            TxnLogService TxnLogService = new
                TxnLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await TxnLogService.UpdateTxnLog(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateTxnLogTestFailed()
        {  //arrange
            var updateReq = new TxnLogUpdateRequest
            {
				contactID = 54,
				custKey = 8,
				customerID = "Y223276666",
				customerName = "測試陳怡伶",
				txnApiCode = "UpdateCustContact",
				txnName = "銀行顧客聯絡方式變更",
				accountNumber = "None",
				txnResultID = "0000",
				txnResultDescr = "交易成功",
				txnRequest = "{"requestBody":{"header":{"infoAssetsNo":"OA0105","oriTradeSeqNo":"0AP1837","txIdInAP":"updateCustContact","txId":"updateCustContact","branch":{"branchCode":"9912"},"teller":{"tellerId":"admin"},"customerInfo":{"cik":"Y223276666"}},"model":{"customerCirciKey":"Y223276666","residentialPhoneNumber":"0281231313","residentialNumberExtension":"1234","companyPhoneNumber":"0285121313","companyExtensionNumber":"2277","mobilePhoneNumber":"0919241414","residentialAddressDetail":"臺北市松山區吉祥里１１鄰光復北路１１巷９７號１１樓","residentialCity":"","residentialArea":"","residentialZip":"999","permanentPhoneNumber":"0221236789"}},"header":{"msgNo":"OA0105_20220922214839_ppw","txnCode":"updateCustContact","txnTime":"2022-09-22 21:48:39","senderCode":"OA0105","receiverCode":"TS0116","operatorCode":"admin","unitCode":"9912","authorizerCode":null}}",
				txnResponse = "{"requestModel":{"requestBody":{"header":{"infoAssetsNo":"OA0105","oriTradeSeqNo":"0AP1837","txIdInAP":"updateCustContact","txId":"updateCustContact","branch":{"branchCode":"9912"},"teller":{"tellerId":"admin"},"customerInfo":{"cik":"Y223276666"}},"model":{"customerCirciKey":"Y223276666","residentialPhoneNumber":"0281231313","residentialNumberExtension":"1234","companyPhoneNumber":"0285121313","companyExtensionNumber":"2277","mobilePhoneNumber":"0919241414","residentialAddressDetail":"臺北市松山區吉祥里１１鄰光復北路１１巷９７號１１樓","residentialCity":"","residentialArea":"","residentialZip":"999","permanentPhoneNumber":"0221236789"}},"header":{"msgNo":"OA0105_20220922214839_ppw","txnCode":"updateCustContact","txnTime":"2022-09-22 21:48:39","senderCode":"OA0105","receiverCode":"TS0116","operatorCode":"admin","unitCode":"9912","authorizerCode":null}},"resultBody":null,"tid":"45e5919571e333a3","resultCode":"0000","resultDescription":"交易成功"}",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspTxnLogUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            TxnLogService TxnLogService = new
                TxnLogService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await TxnLogService.UpdateTxnLog(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
