using FluentValidation;
namespace ESUN.AGD.WebApi.Application.User.Contract
{
    public class UserUpdateValidator:AbstractValidator<UserUpdateRequest>
    {
        public UserUpdateValidator()
        {
			RuleFor(x => x.userID).NotEmpty().WithMessage("使用者帳號不能為空!!");
			RuleFor(x => x.userID).NotEmpty().WithMessage("使用者帳號為必填");
			RuleFor(x => x.userID).MaximumLength(20).WithMessage("使用者帳號長度超過系統限制");
			RuleFor(x => x.userName).NotEmpty().WithMessage("使用者名稱為必填");
			RuleFor(x => x.userName).MaximumLength(60).WithMessage("使用者名稱長度超過系統限制");
			RuleFor(x => x.userCode).NotEmpty().WithMessage("使用者代碼為必填");
			RuleFor(x => x.userCode).MaximumLength(50).WithMessage("使用者代碼長度超過系統限制");
			RuleFor(x => x.agentLoginID).NotEmpty().WithMessage("CTI登入帳號為必填");
			RuleFor(x => x.agentLoginID).MaximumLength(20).WithMessage("CTI登入帳號長度超過系統限制");
			RuleFor(x => x.agentLoginCode).NotEmpty().WithMessage("CTI登入代碼為必填");
			RuleFor(x => x.agentLoginCode).MaximumLength(20).WithMessage("CTI登入代碼長度超過系統限制");
			RuleFor(x => x.employeeNo).NotEmpty().WithMessage("員工編號為必填");
			RuleFor(x => x.employeeNo).MaximumLength(11).WithMessage("員工編號長度超過系統限制");
			RuleFor(x => x.nickName).NotEmpty().WithMessage("使用者暱稱為必填");
			RuleFor(x => x.nickName).MaximumLength(50).WithMessage("使用者暱稱長度超過系統限制");
			RuleFor(x => x.empDept).NotEmpty().WithMessage("所屬單位為必填");
			RuleFor(x => x.empDept).MaximumLength(20).WithMessage("所屬單位長度超過系統限制");
			RuleFor(x => x.officeEmail).NotEmpty().WithMessage("公司Email為必填");
			RuleFor(x => x.officeEmail).MaximumLength(70).WithMessage("公司Email長度超過系統限制");
			RuleFor(x => x.employedStatusCode).NotEmpty().WithMessage("在職狀態代碼為必填");
			RuleFor(x => x.employedStatusCode).MaximumLength(1).WithMessage("在職狀態代碼長度超過系統限制");
			RuleFor(x => x.isSupervisor).NotEmpty().WithMessage("是否為主管?為必填");			RuleFor(x => x.isEnable).NotEmpty().WithMessage("是否啟用?為必填");			RuleFor(x => x.updator).NotEmpty().WithMessage("更新者為必填");
			RuleFor(x => x.updator).MaximumLength(20).WithMessage("更新者長度超過系統限制");
        }
    }
}
