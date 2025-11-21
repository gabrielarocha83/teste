using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yara.AppService.Dtos;

namespace Yara.AppService.Interfaces
{
    public interface IAppServiceEstruturaPerfilUsuario : IAppServiceBase<EstruturaPerfilUsuarioDto>
    {
        Task<bool> InsertAsync(EstruturaPerfilUsuarioDto obj);
        Task<bool> InsertOrUpdateAsync(EstruturaPerfilUsuarioDto obj, Guid userId);
        Task<bool> InsertListAsync(List<EstruturaPerfilUsuarioDto> obj, Guid userId);
        Task<bool> UpdateListAsync(List<EstruturaPerfilUsuarioDto> obj, Guid userId);
        Task<IEnumerable<BuscaCTCPerfilUsuarioDto>> BuscaContaCliente(string Usuario, string CTC, string GC);
        Task<string> GetActiveProfileByCustomer(Guid customerId, Guid userId, string empresaId);
        Task<string> GetActiveProfileAlcadaByCustomer(Guid customerId, Guid userId, string empresaId);
        Task<Guid> GetActiveProfileByUser(Guid customerId, Guid userId, string empresaId);
        Task<Guid> GetUserPerfil(string codSAP, string Perfil);
    }
}
