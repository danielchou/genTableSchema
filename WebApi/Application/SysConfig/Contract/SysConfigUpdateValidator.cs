using FluentValidation;
namespace ESUN.AGD.WebApi.Application.SysConfig.Contract
{
    public class SysConfigUpdateValidator:AbstractValidator<SysConfigUpdateRequest>
    {
        public SysConfigUpdateValidator()
        {
			RuleFor(x => x.sysConfigType).NotEmpty().WithMessage("系統參數類別不能為空!!");
			RuleFor(x => x.sysConfigID).NotEmpty().WithMessage("系統參數代碼不能為空!!");
			RuleFor(x => x.sysConfigType).NotEmpty().WithMessage("系統參數類別為必填");
			RuleFor(x => x.sysConfigType).MaximumLength(20).WithMessage("系統參數類別長度超過系統限制");
			RuleFor(x => x.sysConfigID).NotEmpty().WithMessage("系統參數代碼為必填");
			RuleFor(x => x.sysConfigID).MaximumLength(20).WithMessage("系統參數代碼長度超過系統限制");
			RuleFor(x => x.sysConfigName).NotEmpty().WithMessage("系統參數名稱為必填");
			RuleFor(x => x.sysConfigName).MaximumLength(50).WithMessage("系統參數名稱長度超過系統限制");
			RuleFor(x => x.content).NotEmpty().WithMessage("系統參數內容為必填");
			RuleFor(x => x.content).MaximumLength(200).WithMessage("系統參數內容長度超過系統限制");
			RuleFor(x => x.isVisible).NotEmpty().WithMessage("是否顯示?為必填");
        }
    }
}
