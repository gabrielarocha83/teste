using System;
using FluentValidation;
using Yara.AppService.Dtos;

namespace Yara.WebApi.Validations
{
    public class CustoHaRegiaoValidator : AbstractValidator<CustoHaRegiaoDto>
    {
        public CustoHaRegiaoValidator()
        {

            RuleFor(c => c.CidadeID).NotNull().NotEmpty().WithMessage("O campo Cidade é obrigatório");
            RuleFor(c => c.ValorHaCultivavel).NotNull().NotEmpty().WithMessage("O campo Valor/Ha Cultivável é obrigatório");
            RuleFor(c => c.ValorHaNaoCultivavel).NotNull().NotEmpty().WithMessage("O campo Valor/Ha Não Cultivável é obrigatório");
            RuleFor(c => c.ModuloRural).NotNull().NotEmpty().WithMessage("O campo Módulo Rural é obrigatório");
            

        }
    }
}