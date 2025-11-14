using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    public class GrupoEconomicoValidator : AbstractValidator<NovoGrupoEconomicoDto>
    {
        public GrupoEconomicoValidator()
        {
            RuleFor(c => c.Nome).NotNull().WithMessage(Resources.Resources.nameRequired);
            RuleFor(c => c.Nome).MaximumLength(120).WithMessage(Resources.Resources.fieldLengthPattern120);
            RuleFor(c => c.Membros).Must(c => c.Count > 1).WithMessage(Resources.Resources.validationgroup);
            RuleFor(c => c.Membros).SetCollectionValidator(new ContaClienteValidator());
        }
    }

    public class GrupoEconomicoDeleteValidator : AbstractValidator<GrupoEconomicoFluxoDto>
    {
        public GrupoEconomicoDeleteValidator()
        {
            RuleFor(c => c.GrupoID).NotNull().NotEmpty().WithMessage(Resources.Resources.requesterID);
            RuleFor(c => c.ClassificacaoGrupoEconomicoID).NotEqual(0).NotNull().WithMessage(Resources.Resources.classificationValid);
            RuleFor(c => c.ContaClienteID).NotNull().WithMessage(Resources.Resources.requesterID);
            RuleFor(c => c.Nome).NotNull().WithMessage(Resources.Resources.nameRequired);
        }
    }
}

    
