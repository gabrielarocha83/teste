using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    public class GrupoPermissaoValidator: AbstractValidator<GrupoPermissaoDto>
    {
        public GrupoPermissaoValidator()
        {
            RuleFor(c => c.GrupoID).NotNull().NotEmpty().WithMessage(Resources.Resources.codePermissionGroupRequired);
            RuleFor(c => c.PermissaoID).NotNull().NotEmpty().WithMessage(Resources.Resources.codePermissionRequired);
        }
    }
}