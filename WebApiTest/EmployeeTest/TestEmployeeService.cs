using AutoMapper;
using ESUN.AGD.DataAccess.DataService.DataAccess;
using ESUN.AGD.DataAccess.Type;
using ESUN.AGD.WebApi.Application.Auth;
using ESUN.AGD.WebApi.Application.Common;
using ESUN.AGD.WebApi.Application.Employee;
using ESUN.AGD.WebApi.Application.Employee.Contract;
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


namespace ESUN.AGD.WebApi.Test.EmployeeTest
{
    [TestFixture]
    public class TestEmployeeService
    {
        private static IDataAccessService stubDataAccessService = Substitute.For<IDataAccessService>();
        private static IGetTokenService stubGetTokenService = Substitute.For<IGetTokenService>();
        private static IHttpContextAccessor stubHttpContextAccessor = Substitute.For<IHttpContextAccessor>();
        private static IMapper stubMapper = Substitute.For<IMapper>();
        private static IUserService stubUserService = Substitute.For<IUserService>();
        private static IAutoNextNumService autoNextNumService = Substitute.For<IAutoNextNumService>();

        [Test]
        public async ValueTask GetEmployeeTest()
        {  //arrange
            int seqNo = 1;
            string resCode = "U200";
           
            stubDataAccessService
                .LoadSingleData<TbEmployee, object>("agdSp.uspEmployeeGet", Arg.Any<int>())
                .ReturnsForAnyArgs(new TbEmployee { SeqNo = 1 });

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            EmployeeService EmployeeService = 
                new EmployeeService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await EmployeeService.GetEmployee(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspEmployeeGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode,resCode);

        }

        [Test]
        public async ValueTask GetEmployeeTestFailed()
        {  //arrange
            int seqNo = 0;
            string resCode = "U999";
          
            stubDataAccessService.LoadSingleData<TbEmployee, object>("agdSp.uspEmployeeGet", Arg.Any<int>())
                .ReturnsNull();          

            stubHttpContextAccessor.HttpContext.Request.Method = "GET";

            EmployeeService EmployeeService = new EmployeeService
                (stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await EmployeeService.GetEmployee(seqNo);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<TbEmployee, object>("agdSp.uspEmployeeGet", Arg.Any<object>());
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryEmployeeTest()
        {  //arrange
            var queryReq = new EmployeeQueryRequest { 						employeeNo = "10001",
						fullName = "蔡欣蓁",
						employedStatusCode = "1",
						empDeptCode = "D01",
						empDeptName = "業務單位",
						empSectCode = "SE01",
						empSectName = "金融組",
						officeEmail = "A0001@gmail.com", };
            string resCode = "U200";
            
            stubDataAccessService.LoadData<TbEmployee, object>
                (storeProcedure: "agdSp.uspEmployeeQuery", Arg.Any<object>())
               .Returns(new TbEmployee[] { 
                    new TbEmployee { 
						employeeNo = "10001",
						fullName = "蔡欣蓁",
						employedStatusCode = "1",
						empDeptCode = "D01",
						empDeptName = "業務單位",
						empSectCode = "SE01",
						empSectName = "金融組",
						officeEmail = "A0001@gmail.com",
                        Total = 2
                    },
                    new TbEmployee { } 
                }.AsQueryable());
            
            stubHttpContextAccessor.HttpContext.Request.Path ="/api/Employee/query";
            
            EmployeeService EmployeeService = new 
                EmployeeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await EmployeeService.QueryEmployee(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
          
            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask QueryEmployeeTestFailed()
        {  //arrange
            var queryReq = new EmployeeQueryRequest {  
						employeeNo = "10001",
						fullName = "蔡欣蓁",
						employedStatusCode = "1",
						empDeptCode = "D01",
						empDeptName = "業務單位",
						empSectCode = "SE01",
						empSectName = "金融組",
						officeEmail = "A0001@gmail.com",
            };
            string resCode = "U999";
            
            stubDataAccessService.LoadData<TbEmployee, object>("agdSp.uspEmployeeQuery", Arg.Any<object>())
                .ReturnsNull();
            
            stubHttpContextAccessor.HttpContext.Request.Path = "/api/Employee/query";
            EmployeeService EmployeeService = new EmployeeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await EmployeeService.QueryEmployee(queryReq);

            //assert
            //Received判斷虛設常式是否有執行
            //await stubDataAccessService.Received(1).LoadSingleData<int, object>("agdSp.uspEmployeeGet", Arg.Any<object>());
            Assert.AreEqual(result.total, 0);

        }

        [Test]
        public async ValueTask InsertEmployeeTest()
        {  //arrange
            var insertReq = new EmployeeInsertRequest
            {
				serialNo = 1,
				employeeNo = "10001",
				fullName = "蔡欣蓁",
				employedStatusCode = "1",
				empDeptCode = "D01",
				empDeptName = "業務單位",
				empSectCode = "SE01",
				empSectName = "金融組",
				officeEmail = "A0001@gmail.com",
				creator = "SYS",
				creatorName = "系統觸發",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspEmployeeInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result:new SqlResponse() { ErrorMsg="",Msg="" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            EmployeeService EmployeeService = new
                EmployeeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await EmployeeService.InsertEmployee(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask InsertEmployeeTestFailed()
        {  //arrange
            var insertReq = new EmployeeInsertRequest
            {
				serialNo = 1,
				employeeNo = "10001",
				fullName = "蔡欣蓁",
				employedStatusCode = "1",
				empDeptCode = "D01",
				empDeptName = "業務單位",
				empSectCode = "SE01",
				empSectName = "金融組",
				officeEmail = "A0001@gmail.com",
				creator = "SYS",
				creatorName = "系統觸發",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspEmployeeInsert", insertReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            EmployeeService EmployeeService = new
                EmployeeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await EmployeeService.InsertEmployee(insertReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }


        [Test]
        public async ValueTask UpdateEmployeeTest()
        {  //arrange
            var updateReq = new EmployeeUpdateRequest
            {
				serialNo = 112,
				employeeNo = "88888",
				fullName = "新增測試",
				employedStatusCode = "4",
				empDeptCode = "D03",
				empDeptName = "玉山單位",
				empSectCode = "SE01",
				empSectName = "金融組",
				officeEmail = "A0100@gmail.com",
            };
            string resCode = "U200";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspEmployeeUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            EmployeeService EmployeeService = new
                EmployeeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await EmployeeService.UpdateEmployee(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }

        [Test]
        public async ValueTask UpdateEmployeeTestFailed()
        {  //arrange
            var updateReq = new EmployeeUpdateRequest
            {
				serialNo = 112,
				employeeNo = "88888",
				fullName = "新增測試",
				employedStatusCode = "4",
				empDeptCode = "D03",
				empDeptName = "玉山單位",
				empSectCode = "SE01",
				empSectName = "金融組",
				officeEmail = "A0100@gmail.com",
            };
            string resCode = "U999";

            var data = stubDataAccessService
               .OperateDataWithResMsg(storeProcedure: "agdSp.uspEmployeeUpdate", updateReq)
               .Returns(new ValueTask<SqlResponse>(result: new SqlResponse() { ErrorMsg = "error", Msg = "" }));

            stubHttpContextAccessor.HttpContext.Request.Method = "post";

            EmployeeService EmployeeService = new
                EmployeeService(stubDataAccessService, stubGetTokenService, stubHttpContextAccessor, stubMapper);

            //act
            var result = await EmployeeService.UpdateEmployee(updateReq);

            //assert
            //Received判斷虛設常式是否有執行

            Assert.AreEqual(result.resultCode, resCode);

        }




    }
}
