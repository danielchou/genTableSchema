using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.Group;
using ESUN.AGD.WebApi.Application.Group.Contract;
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


namespace ESUN.AGD.WebApi.Test.GroupTest
{
    [TestFixture]
    public class TestGroupService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetGroupTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbGroup, object>("agdSp.uspGroupGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbGroup { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            GroupService GroupService = 
                new GroupService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await GroupService.GetGroup(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspGroupGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetGroupTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbGroup, object>("agdSp.uspGroupGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            GroupService GroupService = new GroupService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await GroupService.GetGroup(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbGroup, object>("agdSp.uspGroupGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryGroupTest()
        {  //arrange
            var queryReq = new GroupQueryRequest { 						groupName = "主管群組",
						leader = "10046", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbGroup, object>
                (storeProcedure: "agdSp.uspGroupQuery", Arg.Any<object>())
               .Returns(new TbGroup[] { 
                    new TbGroup { 
						groupName = "主管群組",
						leader = "10046",
                        Total = 2
                    },
                    new TbGroup { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/Group/query";
            
            GroupService GroupService = new 
                GroupService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await GroupService.QueryGroup(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryGroupTestFailed()
        {  //arrange
            var queryReq = new GroupQueryRequest {  
						groupName = "主管群組",
						leader = "10046",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbGroup, object>("agdSp.uspGroupQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/Group/query";
            GroupService GroupService = new GroupService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await GroupService.QueryGroup(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspGroupGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertGroupTest()
        {  //arrange
            var insertReq = new GroupInsertRequest
            {
				groupID = "G01",
				groupName = "主管群組",
				leader = "10046",
				creator = "10046",
				creatorName = "陳麗杰鰻魚飯",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspGroupInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            GroupService GroupService = new
                GroupService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await GroupService.InsertGroup(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertGroupTestFailed()
        {  //arrange
            var insertReq = new GroupInsertRequest
            {
				groupID = "G01",
				groupName = "主管群組",
				leader = "10046",
				creator = "10046",
				creatorName = "陳麗杰鰻魚飯",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspGroupInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            GroupService GroupService = new
                GroupService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await GroupService.InsertGroup(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateGroupTest()
        {  //arrange
            var updateReq = new GroupUpdateRequest
            {
				groupID = "G05",
				groupName = "夜貓子組",
				leader = "ag001",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspGroupUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            GroupService GroupService = new
                GroupService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await GroupService.UpdateGroup(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateGroupTestFailed()
        {  //arrange
            var updateReq = new GroupUpdateRequest
            {
				groupID = "G05",
				groupName = "夜貓子組",
				leader = "ag001",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspGroupUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            GroupService GroupService = new
                GroupService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await GroupService.UpdateGroup(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
