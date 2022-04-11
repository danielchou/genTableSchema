using FluentValidation;
namespace ESUN.AGD.WebApi.Application.RoleFunc.Contract
{
    public class RoleFuncUpdateValidator:AbstractValidator<RoleFuncUpdateRequest>
    {
        public RoleFuncUpdateValidator()
        {
			RuleFor(x => x.roleID).NotEmpty().WithMessage("角色代碼不能為空!!");
			RuleFor(x => x.funcID).NotEmpty().WithMessage("功能代碼不能為空!!");
			RuleFor(x => x.roleID).NotEmpty().WithMessage("角色代碼為必填");
			RuleFor(x => x.roleID).MaximumLength(20).WithMessage("角色代碼長度超過系統限制");
			RuleFor(x => x.funcID).NotEmpty().WithMessage("功能代碼為必填");
			RuleFor(x => x.funcID).MaximumLength(20).WithMessage("功能代碼長度超過系統限制");
			RuleFor(x => x.updator).NotEmpty().WithMessage("更新者為必填");
			RuleFor(x => x.updator).MaximumLength(20).WithMessage("更新者長度超過系統限制");
        }
    }
}
