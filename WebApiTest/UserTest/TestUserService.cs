using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.User;
using ESUN.AGD.WebApi.Application.User.Contract;
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


namespace ESUN.AGD.WebApi.Test.UserTest
{
    [TestFixture]
    public class TestUserService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetUserTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbUser, object>("agdSp.uspUserGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbUser { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            UserService UserService = 
                new UserService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserService.GetUser(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspUserGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetUserTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbUser, object>("agdSp.uspUserGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            UserService UserService = new UserService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserService.GetUser(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbUser, object>("agdSp.uspUserGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryUserTest()
        {  //arrange
            var queryReq = new UserQueryRequest { 						userID = "10021",
						userName = "姜仁杰",
						agentLoginID = "",
						empDeptCode = "D01",
						empDept = "業務單位",
						empSectCode = "SE01",
						empSectDept = "金融組",
						officeEmail = "A0021@gmail.com",
						employedStatusCode = "2",
						groupID = "",
						isEnable = True, };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbUser, object>
                (storeProcedure: "agdSp.uspUserQuery", Arg.Any<object>())
               .Returns(new TbUser[] { 
                    new TbUser { 
						userID = "10021",
						userName = "姜仁杰",
						agentLoginID = "",
						empDeptCode = "D01",
						empDept = "業務單位",
						empSectCode = "SE01",
						empSectDept = "金融組",
						officeEmail = "A0021@gmail.com",
						employedStatusCode = "2",
						groupID = "",
						isEnable = True,
                        Total = 2
                    },
                    new TbUser { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/User/query";
            
            UserService UserService = new 
                UserService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserService.QueryUser(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryUserTestFailed()
        {  //arrange
            var queryReq = new UserQueryRequest {  
						userID = "10021",
						userName = "姜仁杰",
						agentLoginID = "",
						empDeptCode = "D01",
						empDept = "業務單位",
						empSectCode = "SE01",
						empSectDept = "金融組",
						officeEmail = "A0021@gmail.com",
						employedStatusCode = "2",
						groupID = "",
						isEnable = True,
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbUser, object>("agdSp.uspUserQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/User/query";
            UserService UserService = new UserService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserService.QueryUser(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspUserGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertUserTest()
        {  //arrange
            var insertReq = new UserInsertRequest
            {
				userID = "10021",
				userName = "姜仁杰",
				nickName = "",
				agentLoginID = "",
				agentLoginCode = "",
				empDeptCode = "D01",
				empDept = "業務單位",
				empSectCode = "SE01",
				empSectDept = "金融組",
				officeEmail = "A0021@gmail.com",
				employedStatusCode = "2",
				groupID = "",
				isSupervisor = False,
				rACECode1 = "",
				rACECode2 = "",
				rACECode3 = "",
				rACECode4 = "",
				rACECode5 = "",
				isEnable = True,
				creator = "10046",
				creatorName = "陳麗杰鰻魚飯",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserService UserService = new
                UserService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserService.InsertUser(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertUserTestFailed()
        {  //arrange
            var insertReq = new UserInsertRequest
            {
				userID = "10021",
				userName = "姜仁杰",
				nickName = "",
				agentLoginID = "",
				agentLoginCode = "",
				empDeptCode = "D01",
				empDept = "業務單位",
				empSectCode = "SE01",
				empSectDept = "金融組",
				officeEmail = "A0021@gmail.com",
				employedStatusCode = "2",
				groupID = "",
				isSupervisor = False,
				rACECode1 = "",
				rACECode2 = "",
				rACECode3 = "",
				rACECode4 = "",
				rACECode5 = "",
				isEnable = True,
				creator = "10046",
				creatorName = "陳麗杰鰻魚飯",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserService UserService = new
                UserService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserService.InsertUser(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateUserTest()
        {  //arrange
            var updateReq = new UserUpdateRequest
            {
				userID = "sys",
				userName = "系統",
				nickName = "MR SYS",
				agentLoginID = "1234",
				agentLoginCode = "1234",
				empDeptCode = "DAA",
				empDept = "行政一單位",
				empSectCode = "SEAA",
				empSectDept = "金融一組",
				officeEmail = "A0046AAA@gmail.com",
				employedStatusCode = "3",
				groupID = "None",
				isSupervisor = True,
				rACECode1 = "RACE1",
				rACECode2 = "RACE2",
				rACECode3 = "RACE3",
				rACECode4 = "RACE4",
				rACECode5 = "RACE5",
				isEnable = True,
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserService UserService = new
                UserService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserService.UpdateUser(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateUserTestFailed()
        {  //arrange
            var updateReq = new UserUpdateRequest
            {
				userID = "sys",
				userName = "系統",
				nickName = "MR SYS",
				agentLoginID = "1234",
				agentLoginCode = "1234",
				empDeptCode = "DAA",
				empDept = "行政一單位",
				empSectCode = "SEAA",
				empSectDept = "金融一組",
				officeEmail = "A0046AAA@gmail.com",
				employedStatusCode = "3",
				groupID = "None",
				isSupervisor = True,
				rACECode1 = "RACE1",
				rACECode2 = "RACE2",
				rACECode3 = "RACE3",
				rACECode4 = "RACE4",
				rACECode5 = "RACE5",
				isEnable = True,
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspUserUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            UserService UserService = new
                UserService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await UserService.UpdateUser(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
