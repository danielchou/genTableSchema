using FluentValidation;
using FluentValidation.Results;

namespace ESUN.AGD.WebApi.Application.Txn.Contract
{
     public class  TxnInsertValidator : AbstractValidator<TxnInsertRequest>
    {
        public  TxnInsertValidator()
        {
			RuleFor(x => x.txnType).NotEmpty().WithMessage("交易執行類別為必填");
			RuleFor(x => x.txnType).MaximumLength(20).WithMessage("交易執行類別長度超過系統限制");
			RuleFor(x => x.txnID).NotEmpty().WithMessage("交易執行代碼為必填");
			RuleFor(x => x.txnID).MaximumLength(50).WithMessage("交易執行代碼長度超過系統限制");
			RuleFor(x => x.txnName).NotEmpty().WithMessage("交易執行名稱為必填");
			RuleFor(x => x.txnName).MaximumLength(50).WithMessage("交易執行名稱長度超過系統限制");
			RuleFor(x => x.txnScript).MaximumLength(2000).WithMessage("交易執行話術長度超過系統限制");
			RuleFor(x => x.isEnable).NotEmpty().WithMessage("是否啟用?為必填");
        }
    }
}
