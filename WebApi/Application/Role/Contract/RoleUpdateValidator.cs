using FluentValidation;
namespace ESUN.AGD.WebApi.Application.Role.Contract
{
    public class RoleUpdateValidator:AbstractValidator<RoleUpdateRequest>
    {
        public RoleUpdateValidator()
        {
			RuleFor(x => x.seqNo).NotEmpty().WithMessage("流水號不能為空!!");
			RuleFor(x => x.roleId).NotEmpty().WithMessage("角色代碼為必填");
			RuleFor(x => x.roleId).MaximumLength(20).WithMessage("角色代碼長度超過系統限制");
			RuleFor(x => x.roleName).NotEmpty().WithMessage("角色名稱為必填");
			RuleFor(x => x.roleName).MaximumLength(50).WithMessage("角色名稱長度超過系統限制");
			RuleFor(x => x.updator).NotEmpty().WithMessage("異動者為必填");
			RuleFor(x => x.updator).MaximumLength(20).WithMessage("異動者長度超過系統限制");
            RuleFor(x => x.isEnable).NotEmpty().WithMessage("是否啟用?為必填");
        }
    }
}
