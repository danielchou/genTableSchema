using FluentValidation;
using FluentValidation.Results;

namespace ESUN.AGD.WebApi.Application.Code.Contract
{
     public class  CodeInsertValidator : AbstractValidator<CodeInsertRequest>
    {
        public  CodeInsertValidator()
        {
			RuleFor(x => x.codeType).NotEmpty().WithMessage("代碼分類為必填");
			RuleFor(x => x.codeType).MaximumLength(20).WithMessage("代碼分類長度超過系統限制");
			RuleFor(x => x.codeId).NotEmpty().WithMessage("系統代碼檔代碼為必填");
			RuleFor(x => x.codeId).MaximumLength(20).WithMessage("系統代碼檔代碼長度超過系統限制");
			RuleFor(x => x.codeName).NotEmpty().WithMessage("系統代碼檔名稱為必填");
			RuleFor(x => x.codeName).MaximumLength(50).WithMessage("系統代碼檔名稱長度超過系統限制");
			RuleFor(x => x.updator).NotEmpty().WithMessage("異動者為必填");
			RuleFor(x => x.updator).MaximumLength(20).WithMessage("異動者長度超過系統限制");
        }
    }

}
