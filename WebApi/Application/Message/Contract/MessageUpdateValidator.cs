﻿using FluentValidation;
namespace ESUN.AGD.WebApi.Application.Message.Contract
{
    public class MessageUpdateValidator:AbstractValidator<MessageUpdateRequest>
    {
        public MessageUpdateValidator()
        {
			RuleFor(x => x.messageSheetID).NotEmpty().WithMessage("訊息傳送頁籤代碼不能為空!!");
			RuleFor(x => x.messageTemplateID).NotEmpty().WithMessage("訊息傳送範本代碼不能為空!!");
			RuleFor(x => x.messageSheetID).NotEmpty().WithMessage("訊息傳送頁籤代碼為必填");
			RuleFor(x => x.messageSheetID).MaximumLength(20).WithMessage("訊息傳送頁籤代碼長度超過系統限制");
			RuleFor(x => x.messageTemplateID).NotEmpty().WithMessage("訊息傳送範本代碼為必填");
			RuleFor(x => x.messageTemplateID).MaximumLength(20).WithMessage("訊息傳送範本代碼長度超過系統限制");
			RuleFor(x => x.messageName).NotEmpty().WithMessage("訊息傳送名稱為必填");
			RuleFor(x => x.messageName).MaximumLength(50).WithMessage("訊息傳送名稱長度超過系統限制");
			RuleFor(x => x.isEnable).NotEmpty().WithMessage("是否啟用?為必填");
        }
    }
}
