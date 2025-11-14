using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    public class FeriasValidator : AbstractValidator<FeriasDto>
    {
        public FeriasValidator()
        {
            RuleFor(c => c.UsuarioID).NotNull().WithMessage(Resources.Resources.userVacation);
            RuleFor(c => c.SubstitutoID).NotNull().WithMessage(Resources.Resources.changeVacation);
            RuleFor(c => c.FeriasInicio).NotNull().WithMessage(Resources.Resources.vacationDateBegin);
            RuleFor(c => c.FeriasFim).NotNull().WithMessage(Resources.Resources.vacationDateEnd);
            
        }
    }
}