using FluentValidation;
namespace ESUN.AGD.WebApi.Application.Code.Contract
{
    public class CodeUpdateValidator:AbstractValidator<CodeUpdateRequest>
    {
        public CodeUpdateValidator()
        {
			RuleFor(x => x.codeType).NotEmpty().WithMessage("共用碼類別不能為空!!");
			RuleFor(x => x.codeID).NotEmpty().WithMessage("共用碼代碼不能為空!!");
			RuleFor(x => x.codeType).NotEmpty().WithMessage("共用碼類別為必填");
			RuleFor(x => x.codeType).MaximumLength(20).WithMessage("共用碼類別長度超過系統限制");
			RuleFor(x => x.codeID).NotEmpty().WithMessage("共用碼代碼為必填");
			RuleFor(x => x.codeID).MaximumLength(20).WithMessage("共用碼代碼長度超過系統限制");
			RuleFor(x => x.codeName).NotEmpty().WithMessage("共用碼名稱為必填");
			RuleFor(x => x.codeName).MaximumLength(50).WithMessage("共用碼名稱長度超過系統限制");
			RuleFor(x => x.content).MaximumLength(500).WithMessage("共用碼內容長度超過系統限制");
			RuleFor(x => x.memo).MaximumLength(200).WithMessage("備註長度超過系統限制");
			RuleFor(x => x.isEnable).NotEmpty().WithMessage("是否啟用?為必填");
        }
    }
}
