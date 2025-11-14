using System;
using System.Collections.Generic;
using Yara.DomainService.Interfaces;

namespace Yara.DomainService.IoC
{
    public static class Resolver
    {

        public static Dictionary<Type, Type> GetTypes()
        {
            var ioc = new Dictionary<Type, Type>
            {
                {typeof(IDomainServiceGrupo), typeof(DomainServiceGrupo)},
                {typeof(IDomainServicePermissao), typeof(DomainServicePermissao)},
                {typeof(IDomainServiceLog), typeof(DomainServiceLog)},
                {typeof(IDomainServiceGrupoPermissao), typeof(DomainServiceGrupoPermissao)}
            };

            return ioc;
        }

    }
}