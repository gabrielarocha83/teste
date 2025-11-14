using FluentValidation;
using Yara.AppService.Dtos;
#pragma warning disable 1591

namespace Yara.WebApi.Validations
{
    public class SegmentoValidator : AbstractValidator<SegmentoDto>
    {
        public SegmentoValidator()
        {
            RuleFor(c => c.Descricao).NotNull().NotEmpty().WithMessage(Resources.Resources.fieldDescriptionPattern);
            RuleFor(c => c.Descricao).MaximumLength(120).WithMessage(Resources.Resources.fieldLengthPattern120);
            //RuleFor(c => c.DataCriacao).NotNull().NotEmpty().WithMessage(Resources.Resources.dateRequired);
        }
    }
}