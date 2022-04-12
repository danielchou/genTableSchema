using FluentValidation;
namespace ESUN.AGD.WebApi.Application.Func.Contract
{
    public class FuncUpdateValidator:AbstractValidator<FuncUpdateRequest>
    {
        public FuncUpdateValidator()
        {
			RuleFor(x => x.funcID).NotEmpty().WithMessage("功能代碼不能為空!!");
			RuleFor(x => x.funcID).NotEmpty().WithMessage("功能代碼為必填");
			RuleFor(x => x.funcID).MaximumLength(20).WithMessage("功能代碼長度超過系統限制");
			RuleFor(x => x.funcName).NotEmpty().WithMessage("功能名稱為必填");
			RuleFor(x => x.funcName).MaximumLength(50).WithMessage("功能名稱長度超過系統限制");
			RuleFor(x => x.parentFuncID).NotEmpty().WithMessage("上層功能代碼為必填");
			RuleFor(x => x.parentFuncID).MaximumLength(20).WithMessage("上層功能代碼長度超過系統限制");
			RuleFor(x => x.systemType).NotEmpty().WithMessage("系統類別為必填");
			RuleFor(x => x.systemType).MaximumLength(20).WithMessage("系統類別長度超過系統限制");
			RuleFor(x => x.iconName).MaximumLength(20).WithMessage("Icon名稱長度超過系統限制");
			RuleFor(x => x.routeName).MaximumLength(50).WithMessage("路由名稱長度超過系統限制");
        }
    }
}
