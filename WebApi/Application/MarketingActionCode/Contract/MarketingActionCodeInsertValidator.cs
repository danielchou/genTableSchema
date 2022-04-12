using FluentValidation;
using FluentValidation.Results;

namespace ESUN.AGD.WebApi.Application.MarketingActionCode.Contract
{
     public class  MarketingActionCodeInsertValidator : AbstractValidator<MarketingActionCodeInsertRequest>
    {
        public  MarketingActionCodeInsertValidator()
        {
			RuleFor(x => x.actionCodeType).NotEmpty().WithMessage("客群方案類別為必填");
			RuleFor(x => x.actionCodeType).MaximumLength(20).WithMessage("客群方案類別長度超過系統限制");
			RuleFor(x => x.marketingID).NotEmpty().WithMessage("行銷方案代碼為必填");
			RuleFor(x => x.marketingID).MaximumLength(20).WithMessage("行銷方案代碼長度超過系統限制");
			RuleFor(x => x.actionCode).NotEmpty().WithMessage("行銷結果代碼為必填");
			RuleFor(x => x.actionCode).MaximumLength(20).WithMessage("行銷結果代碼長度超過系統限制");
			RuleFor(x => x.content).NotEmpty().WithMessage("行銷結果說明為必填");
			RuleFor(x => x.content).MaximumLength(200).WithMessage("行銷結果說明長度超過系統限制");
			RuleFor(x => x.isAccept).NotEmpty().WithMessage("是否接受?為必填");
			RuleFor(x => x.isEnable).NotEmpty().WithMessage("是否啟用?為必填");
        }
    }
}
