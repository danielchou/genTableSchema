using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.Cust;
using ESUN.AGD.WebApi.Application.Cust.Contract;
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


namespace ESUN.AGD.WebApi.Test.CustTest
{
    [TestFixture]
    public class TestCustService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetCustTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbCust, object>("agdSp.uspCustGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbCust { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            CustService CustService = 
                new CustService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustService.GetCust(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspCustGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetCustTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbCust, object>("agdSp.uspCustGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            CustService CustService = new CustService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustService.GetCust(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbCust, object>("agdSp.uspCustGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryCustTest()
        {  //arrange
            var queryReq = new CustQueryRequest { 						customerID = "NEW",
						circiKey = "None",
						creditCardKey = "None",
						customerName = "匿名者",
						customerBirthday = "None",
						principalBirthday = "None", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbCust, object>
                (storeProcedure: "agdSp.uspCustQuery", Arg.Any<object>())
               .Returns(new TbCust[] { 
                    new TbCust { 
						customerID = "NEW",
						circiKey = "None",
						creditCardKey = "None",
						customerName = "匿名者",
						customerBirthday = "None",
						principalBirthday = "None",
                        Total = 2
                    },
                    new TbCust { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/Cust/query";
            
            CustService CustService = new 
                CustService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustService.QueryCust(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryCustTestFailed()
        {  //arrange
            var queryReq = new CustQueryRequest {  
						customerID = "NEW",
						circiKey = "None",
						creditCardKey = "None",
						customerName = "匿名者",
						customerBirthday = "None",
						principalBirthday = "None",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbCust, object>("agdSp.uspCustQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/Cust/query";
            CustService CustService = new CustService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustService.QueryCust(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspCustGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertCustTest()
        {  //arrange
            var insertReq = new CustInsertRequest
            {
				custKey = 0,
				customerID = "NEW",
				personCertNo = "NEW",
				residencePermitCertNo = "",
				businessRegCertNo = "None",
				obuCertNo = "None",
				partyKind = "None",
				circiKey = "None",
				creditCardKey = "None",
				passportNo = "None",
				customerName = "匿名者",
				partyEnglishName = "None",
				customerBirthday = "None",
				residencePermitCertStartDate = "None",
				residencePermitCertEndDate = "None",
				genderCode = "None",
				industryTypeCode = "None",
				serveCompanyName = "None",
				principalPartyCertNo = "None",
				principalName = "None",
				principalBirthday = "None",
				foreignExchangeRoleCode = "None",
				residenceAddrPostalCode = "None",
				residenceAddrCountryCode = "None",
				residenceAddrLocality = "None",
				residenceAddrAdminArea = "None",
				residenceAddr = "None",
				contactAddrPostalCode = "None",
				contactAddrCountryCode = "None",
				contactAddrLocality = "None",
				contactAddrAdminArea = "None",
				contactAddr = "None",
				cardBillAddrPostalCode = "None",
				cardBillAddrLocality = "None",
				cardBillAddrAdminArea = "None",
				cardBillAddr = "None",
				companyAddrPostalCode = "None",
				companyAddrLocality = "None",
				companyAddrAdminArea = "None",
				companyAddr = "None",
				contactPhone = "None",
				txnPhone = "None",
				companyTel = "None",
				contactTel = "None",
				residenceTel = "None",
				email = "None",
				cardBillAddrEFlag = "None",
				stopUseCustomerDataFlag = "None",
				crossSellingFlag = "None",
				dmSaleFlag = "None",
				smsSaleFlag = "None",
				emailSaleFlag = "None",
				phoneSaleFlag = "None",
				cardStatusCode = "None",
				autoChargeFlag = "None",
				otpServiceFlag = "None",
				performanceDptCode = "None",
				fcBranchCode = "None",
				fcSupervisorEmpNo = "None",
				belongFcEmpNo = "None",
				pibVersionCode = "None",
				pibStatusCode = "None",
				efingoMemberCode = "None",
				customerStatusCode = "None",
				jobTitle = "None",
				gibVersionCode = "None",
				partyNameChangeFlag = "None",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCustInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CustService CustService = new
                CustService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustService.InsertCust(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertCustTestFailed()
        {  //arrange
            var insertReq = new CustInsertRequest
            {
				custKey = 0,
				customerID = "NEW",
				personCertNo = "NEW",
				residencePermitCertNo = "",
				businessRegCertNo = "None",
				obuCertNo = "None",
				partyKind = "None",
				circiKey = "None",
				creditCardKey = "None",
				passportNo = "None",
				customerName = "匿名者",
				partyEnglishName = "None",
				customerBirthday = "None",
				residencePermitCertStartDate = "None",
				residencePermitCertEndDate = "None",
				genderCode = "None",
				industryTypeCode = "None",
				serveCompanyName = "None",
				principalPartyCertNo = "None",
				principalName = "None",
				principalBirthday = "None",
				foreignExchangeRoleCode = "None",
				residenceAddrPostalCode = "None",
				residenceAddrCountryCode = "None",
				residenceAddrLocality = "None",
				residenceAddrAdminArea = "None",
				residenceAddr = "None",
				contactAddrPostalCode = "None",
				contactAddrCountryCode = "None",
				contactAddrLocality = "None",
				contactAddrAdminArea = "None",
				contactAddr = "None",
				cardBillAddrPostalCode = "None",
				cardBillAddrLocality = "None",
				cardBillAddrAdminArea = "None",
				cardBillAddr = "None",
				companyAddrPostalCode = "None",
				companyAddrLocality = "None",
				companyAddrAdminArea = "None",
				companyAddr = "None",
				contactPhone = "None",
				txnPhone = "None",
				companyTel = "None",
				contactTel = "None",
				residenceTel = "None",
				email = "None",
				cardBillAddrEFlag = "None",
				stopUseCustomerDataFlag = "None",
				crossSellingFlag = "None",
				dmSaleFlag = "None",
				smsSaleFlag = "None",
				emailSaleFlag = "None",
				phoneSaleFlag = "None",
				cardStatusCode = "None",
				autoChargeFlag = "None",
				otpServiceFlag = "None",
				performanceDptCode = "None",
				fcBranchCode = "None",
				fcSupervisorEmpNo = "None",
				belongFcEmpNo = "None",
				pibVersionCode = "None",
				pibStatusCode = "None",
				efingoMemberCode = "None",
				customerStatusCode = "None",
				jobTitle = "None",
				gibVersionCode = "None",
				partyNameChangeFlag = "None",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCustInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CustService CustService = new
                CustService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustService.InsertCust(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateCustTest()
        {  //arrange
            var updateReq = new CustUpdateRequest
            {
				custKey = 8,
				customerID = "Y223276666",
				personCertNo = "Y223276666",
				residencePermitCertNo = "None",
				businessRegCertNo = "None",
				obuCertNo = "None",
				partyKind = "None",
				circiKey = "Y223276666",
				creditCardKey = "Y223276666",
				passportNo = "None",
				customerName = "測試陳怡伶",
				partyEnglishName = "CHEN,YI-LING",
				customerBirthday = "1992-09-28",
				residencePermitCertStartDate = "None",
				residencePermitCertEndDate = "None",
				genderCode = "M",
				industryTypeCode = "08",
				serveCompanyName = "金財通商務科技股份有限公司",
				principalPartyCertNo = "None",
				principalName = "None",
				principalBirthday = "None",
				foreignExchangeRoleCode = "None",
				residenceAddrPostalCode = "105",
				residenceAddrCountryCode = "None",
				residenceAddrLocality = "台北市",
				residenceAddrAdminArea = "松山區",
				residenceAddr = "吉祥里１１鄰光北路１１巷９７號１１樓",
				contactAddrPostalCode = "105",
				contactAddrCountryCode = "None",
				contactAddrLocality = "台北市",
				contactAddrAdminArea = "松山區",
				contactAddr = "吉祥里１１鄰光復北路１１巷９７號１１樓",
				cardBillAddrPostalCode = "247",
				cardBillAddrLocality = "新北市",
				cardBillAddrAdminArea = "蘆洲區",
				cardBillAddr = "光明路１０６巷３６號６樓",
				companyAddrPostalCode = "105",
				companyAddrLocality = "台北市",
				companyAddrAdminArea = "松山區",
				companyAddr = "吉祥里１１鄰光北路１１巷９７號１１樓",
				contactPhone = "0919241414",
				txnPhone = "0919241414",
				companyTel = "02-85121313",
				contactTel = "02-81231313",
				residenceTel = "02-21236789",
				email = "maggyc242001@hotmail.com",
				cardBillAddrEFlag = "1",
				stopUseCustomerDataFlag = "N",
				crossSellingFlag = "None",
				dmSaleFlag = "N",
				smsSaleFlag = "Y",
				emailSaleFlag = "N",
				phoneSaleFlag = "Y",
				cardStatusCode = "1",
				autoChargeFlag = "Y",
				otpServiceFlag = "1",
				performanceDptCode = "0026",
				fcBranchCode = "0026",
				fcSupervisorEmpNo = "06074",
				belongFcEmpNo = "20191",
				pibVersionCode = "1",
				pibStatusCode = "1",
				efingoMemberCode = "None",
				customerStatusCode = "99",
				jobTitle = "None",
				gibVersionCode = "None",
				partyNameChangeFlag = "None",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCustUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CustService CustService = new
                CustService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustService.UpdateCust(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateCustTestFailed()
        {  //arrange
            var updateReq = new CustUpdateRequest
            {
				custKey = 8,
				customerID = "Y223276666",
				personCertNo = "Y223276666",
				residencePermitCertNo = "None",
				businessRegCertNo = "None",
				obuCertNo = "None",
				partyKind = "None",
				circiKey = "Y223276666",
				creditCardKey = "Y223276666",
				passportNo = "None",
				customerName = "測試陳怡伶",
				partyEnglishName = "CHEN,YI-LING",
				customerBirthday = "1992-09-28",
				residencePermitCertStartDate = "None",
				residencePermitCertEndDate = "None",
				genderCode = "M",
				industryTypeCode = "08",
				serveCompanyName = "金財通商務科技股份有限公司",
				principalPartyCertNo = "None",
				principalName = "None",
				principalBirthday = "None",
				foreignExchangeRoleCode = "None",
				residenceAddrPostalCode = "105",
				residenceAddrCountryCode = "None",
				residenceAddrLocality = "台北市",
				residenceAddrAdminArea = "松山區",
				residenceAddr = "吉祥里１１鄰光北路１１巷９７號１１樓",
				contactAddrPostalCode = "105",
				contactAddrCountryCode = "None",
				contactAddrLocality = "台北市",
				contactAddrAdminArea = "松山區",
				contactAddr = "吉祥里１１鄰光復北路１１巷９７號１１樓",
				cardBillAddrPostalCode = "247",
				cardBillAddrLocality = "新北市",
				cardBillAddrAdminArea = "蘆洲區",
				cardBillAddr = "光明路１０６巷３６號６樓",
				companyAddrPostalCode = "105",
				companyAddrLocality = "台北市",
				companyAddrAdminArea = "松山區",
				companyAddr = "吉祥里１１鄰光北路１１巷９７號１１樓",
				contactPhone = "0919241414",
				txnPhone = "0919241414",
				companyTel = "02-85121313",
				contactTel = "02-81231313",
				residenceTel = "02-21236789",
				email = "maggyc242001@hotmail.com",
				cardBillAddrEFlag = "1",
				stopUseCustomerDataFlag = "N",
				crossSellingFlag = "None",
				dmSaleFlag = "N",
				smsSaleFlag = "Y",
				emailSaleFlag = "N",
				phoneSaleFlag = "Y",
				cardStatusCode = "1",
				autoChargeFlag = "Y",
				otpServiceFlag = "1",
				performanceDptCode = "0026",
				fcBranchCode = "0026",
				fcSupervisorEmpNo = "06074",
				belongFcEmpNo = "20191",
				pibVersionCode = "1",
				pibStatusCode = "1",
				efingoMemberCode = "None",
				customerStatusCode = "99",
				jobTitle = "None",
				gibVersionCode = "None",
				partyNameChangeFlag = "None",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspCustUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            CustService CustService = new
                CustService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await CustService.UpdateCust(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
