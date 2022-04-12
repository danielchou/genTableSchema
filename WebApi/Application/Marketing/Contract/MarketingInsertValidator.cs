using FluentValidation;
using FluentValidation.Results;

namespace ESUN.AGD.WebApi.Application.Marketing.Contract
{
     public class  MarketingInsertValidator : AbstractValidator<MarketingInsertRequest>
    {
        public  MarketingInsertValidator()
        {
			RuleFor(x => x.marketingID).NotEmpty().WithMessage("行銷方案代碼為必填");
			RuleFor(x => x.marketingID).MaximumLength(20).WithMessage("行銷方案代碼長度超過系統限制");
			RuleFor(x => x.marketingType).NotEmpty().WithMessage("行銷方案類別為必填");
			RuleFor(x => x.marketingType).MaximumLength(1).WithMessage("行銷方案類別長度超過系統限制");
			RuleFor(x => x.marketingName).NotEmpty().WithMessage("行銷方案名稱為必填");
			RuleFor(x => x.marketingName).MaximumLength(50).WithMessage("行銷方案名稱長度超過系統限制");
			RuleFor(x => x.content).NotEmpty().WithMessage("行銷方案內容為必填");
			RuleFor(x => x.content).MaximumLength(100).WithMessage("行銷方案內容長度超過系統限制");
			RuleFor(x => x.marketingScript).MaximumLength(2000).WithMessage("行銷方案話術長度超過系統限制");
			RuleFor(x => x.offerCode).MaximumLength(20).WithMessage("專案識別碼長度超過系統限制");
			RuleFor(x => x.isEnable).NotEmpty().WithMessage("是否啟用?為必填");
        }
    }
}
