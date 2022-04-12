using FluentValidation;
using FluentValidation.Results;

namespace ESUN.AGD.WebApi.Application.ReasonTxn.Contract
{
     public class  ReasonTxnInsertValidator : AbstractValidator<ReasonTxnInsertRequest>
    {
        public  ReasonTxnInsertValidator()
        {
			RuleFor(x => x.txnItem).NotEmpty().WithMessage("Txn交易類型為必填");
			RuleFor(x => x.txnItem).MaximumLength(20).WithMessage("Txn交易類型長度超過系統限制");
			RuleFor(x => x.reasonID).MaximumLength(20).WithMessage("原因代碼長度超過系統限制");
        }
    }
}
