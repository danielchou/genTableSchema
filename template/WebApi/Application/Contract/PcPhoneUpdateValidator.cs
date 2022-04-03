using FluentValidation;
namespace ESUN.AGD.WebApi.Application.$pt_TableName.Contract
{
    public class $pt_TableName$updateValidator:AbstractValidator<$pt_TableName$updateRequest>
    {
        public $pt_TableName$updateValidator()
        {
$pt_ValidateMustHave
$pt_insertValidator
            RuleFor(x => x.isEnable).NotEmpty().WithMessage("是否啟用?為必填");
        }
    }
}
