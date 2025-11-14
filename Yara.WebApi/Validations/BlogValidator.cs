using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    public class BlogValidator : AbstractValidator<BlogDto>
    {
        public BlogValidator()
        {
            RuleFor(c => c.Mensagem).NotNull().WithMessage(Resources.Resources.fieldDescriptionPattern);
            RuleFor(c => c.ContaClienteID).NotNull().WithMessage(Resources.Resources.accountclienteID);
            RuleFor(c => c.Area).NotNull().WithMessage(Resources.Resources.areaRequired);
        }
    }
}
