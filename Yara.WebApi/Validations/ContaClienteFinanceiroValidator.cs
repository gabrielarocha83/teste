using FluentValidation;
using Yara.AppService.Dtos;
#pragma warning disable 1591

namespace Yara.WebApi.Validations
{
    public class ContaClienteFinanceiroValidator : AbstractValidator<ContaClienteFinanceiroDto>
    {
        public ContaClienteFinanceiroValidator()
        {
        }
    }
}