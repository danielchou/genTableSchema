using FluentValidation;
namespace ESUN.AGD.WebApi.Application.AuxCode.Contract
{
    public class AuxCodeUpdateValidator:AbstractValidator<AuxCodeUpdateRequest>
    {
        public AuxCodeUpdateValidator()
        {
			RuleFor(x => x.auxID).NotEmpty().WithMessage("休息碼代碼不能為空!!");
			RuleFor(x => x.auxID).NotEmpty().WithMessage("休息碼代碼為必填");
			RuleFor(x => x.auxID).MaximumLength(20).WithMessage("休息碼代碼長度超過系統限制");
			RuleFor(x => x.auxName).NotEmpty().WithMessage("休息碼名稱為必填");
			RuleFor(x => x.auxName).MaximumLength(50).WithMessage("休息碼名稱長度超過系統限制");
			RuleFor(x => x.isLongTimeAux).NotEmpty().WithMessage("是否長時間離開?為必填");
        }
    }
}
