using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.PhoneBook;
using ESUN.AGD.WebApi.Application.PhoneBook.Contract;
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


namespace ESUN.AGD.WebApi.Test.PhoneBookTest
{
    [TestFixture]
    public class TestPhoneBookService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetPhoneBookTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbPhoneBook, object>("agdSp.uspPhoneBookGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbPhoneBook { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            PhoneBookService PhoneBookService = 
                new PhoneBookService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await PhoneBookService.GetPhoneBook(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspPhoneBookGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetPhoneBookTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbPhoneBook, object>("agdSp.uspPhoneBookGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            PhoneBookService PhoneBookService = new PhoneBookService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await PhoneBookService.GetPhoneBook(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbPhoneBook, object>("agdSp.uspPhoneBookGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryPhoneBookTest()
        {  //arrange
            var queryReq = new PhoneBookQueryRequest { 						phoneBookID = "BB",
						phoneBookName = "BBo名稱",
						parentPhoneBookID = "ROOT",
						phoneBookNumber = "",
						level = 1, };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbPhoneBook, object>
                (storeProcedure: "agdSp.uspPhoneBookQuery", Arg.Any<object>())
               .Returns(new TbPhoneBook[] { 
                    new TbPhoneBook { 
						phoneBookID = "BB",
						phoneBookName = "BBo名稱",
						parentPhoneBookID = "ROOT",
						phoneBookNumber = "",
						level = 1,
                        Total = 2
                    },
                    new TbPhoneBook { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/PhoneBook/query";
            
            PhoneBookService PhoneBookService = new 
                PhoneBookService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await PhoneBookService.QueryPhoneBook(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryPhoneBookTestFailed()
        {  //arrange
            var queryReq = new PhoneBookQueryRequest {  
						phoneBookID = "BB",
						phoneBookName = "BBo名稱",
						parentPhoneBookID = "ROOT",
						phoneBookNumber = "",
						level = 1,
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbPhoneBook, object>("agdSp.uspPhoneBookQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/PhoneBook/query";
            PhoneBookService PhoneBookService = new PhoneBookService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await PhoneBookService.QueryPhoneBook(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspPhoneBookGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertPhoneBookTest()
        {  //arrange
            var insertReq = new PhoneBookInsertRequest
            {
				phoneBookID = "BB",
				phoneBookName = "BBo名稱",
				parentPhoneBookID = "ROOT",
				phoneBookNumber = "",
				level = 1,
				memo = "",
				displayOrder = 0,
				creator = "10046",
				creatorName = "陳麗杰鰻魚飯",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspPhoneBookInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            PhoneBookService PhoneBookService = new
                PhoneBookService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await PhoneBookService.InsertPhoneBook(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertPhoneBookTestFailed()
        {  //arrange
            var insertReq = new PhoneBookInsertRequest
            {
				phoneBookID = "BB",
				phoneBookName = "BBo名稱",
				parentPhoneBookID = "ROOT",
				phoneBookNumber = "",
				level = 1,
				memo = "",
				displayOrder = 0,
				creator = "10046",
				creatorName = "陳麗杰鰻魚飯",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspPhoneBookInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            PhoneBookService PhoneBookService = new
                PhoneBookService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await PhoneBookService.InsertPhoneBook(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdatePhoneBookTest()
        {  //arrange
            var updateReq = new PhoneBookUpdateRequest
            {
				phoneBookID = "YUL1",
				phoneBookName = "斗六分行",
				parentPhoneBookID = "YUL",
				phoneBookNumber = "05-532-1313640",
				level = 3,
				memo = "None",
				displayOrder = 1,
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspPhoneBookUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            PhoneBookService PhoneBookService = new
                PhoneBookService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await PhoneBookService.UpdatePhoneBook(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdatePhoneBookTestFailed()
        {  //arrange
            var updateReq = new PhoneBookUpdateRequest
            {
				phoneBookID = "YUL1",
				phoneBookName = "斗六分行",
				parentPhoneBookID = "YUL",
				phoneBookNumber = "05-532-1313640",
				level = 3,
				memo = "None",
				displayOrder = 1,
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspPhoneBookUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            PhoneBookService PhoneBookService = new
                PhoneBookService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await PhoneBookService.UpdatePhoneBook(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
