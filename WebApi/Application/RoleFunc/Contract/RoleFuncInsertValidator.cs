using FluentValidation;
using FluentValidation.Results;

namespace ESUN.AGD.WebApi.Application.RoleFunc.Contract
{
     public class  RoleFuncInsertValidator : AbstractValidator<RoleFuncInsertRequest>
    {
        public  RoleFuncInsertValidator()
        {
			RuleFor(x => x.roleID).NotEmpty().WithMessage("角色代碼為必填");
			RuleFor(x => x.roleID).MaximumLength(20).WithMessage("角色代碼長度超過系統限制");
			RuleFor(x => x.funcID).NotEmpty().WithMessage("功能代碼為必填");
			RuleFor(x => x.funcID).MaximumLength(20).WithMessage("功能代碼長度超過系統限制");
			RuleFor(x => x.updator).NotEmpty().WithMessage("更新者為必填");
			RuleFor(x => x.updator).MaximumLength(20).WithMessage("更新者長度超過系統限制");
        }
    }
}
