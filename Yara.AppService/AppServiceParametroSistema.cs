using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Yara.AppService.Dtos;
using Yara.AppService.Extensions;
using Yara.AppService.Interfaces;
using Yara.Domain.Entities;
using Yara.Domain.Repository;

namespace Yara.AppService
{
    public class AppServiceParametroSistema : IAppServiceParametroSistema
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppServiceParametroSistema(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ParametroSistemaDto> GetAsync(Expression<Func<ParametroSistemaDto, bool>> expression)
        {
            var param = await _unitOfWork.ParametroSistemaRepository.GetAsync(Mapper.Map<Expression<Func<ParametroSistema, bool>>>(expression));
            return Mapper.Map<ParametroSistemaDto>(param);
        }

        public async Task<IEnumerable<ParametroSistemaDto>> GetAllFilterAsync(Expression<Func<ParametroSistemaDto, bool>> expression)
        {
            var param = await _unitOfWork.ParametroSistemaRepository.GetAllFilterAsync(Mapper.Map<Expression<Func<ParametroSistema, bool>>>(expression));
            return Mapper.Map<IEnumerable<ParametroSistemaDto>>(param);
        }

        public async Task<IEnumerable<ParametroSistemaDto>> GetAllAsync(string empresaID)
        {
            var param = await _unitOfWork.ParametroSistemaRepository.GetAllFilterAsync(c => c.EmpresasID.Equals(empresaID));
            return Mapper.Map<IEnumerable<ParametroSistemaDto>>(param);
        }

        public async Task<bool> InsertAsync(ParametroSistemaDto obj)
        {
            var exist = await _unitOfWork.ParametroSistemaRepository.GetAsync(c => c.Chave.Equals(obj.Chave) && c.EmpresasID.Equals(obj.EmpresasID));
            if (exist != null)
                throw new Exception("Parametro já esta cadastrado.");

            var param = obj.MapTo<ParametroSistema>();
            param.DataCriacao = DateTime.Now;
            param.UsuarioIDCriacao = obj.UsuarioIDCriacao;

            _unitOfWork.ParametroSistemaRepository.Insert(param);

            return _unitOfWork.Commit();
        }

        public async Task<bool> Inactive(Guid id)
        {
            var param = await _unitOfWork.ParametroSistemaRepository.GetAsync(c => c.ID.Equals(id));
            param.Ativo = false;

            _unitOfWork.ParametroSistemaRepository.Update(param);

            return _unitOfWork.Commit();
        }

        public bool Insert(ParametroSistemaDto obj)
        {
            var param = obj.MapTo<ParametroSistema>();
            param.ID = Guid.NewGuid();
            param.DataCriacao = DateTime.Now;
            param.UsuarioIDCriacao = obj.UsuarioIDCriacao;

            _unitOfWork.ParametroSistemaRepository.Insert(param);

            return _unitOfWork.Commit();
        }

        public async Task<bool> Update(ParametroSistemaDto obj)
        {
            var param = await _unitOfWork.ParametroSistemaRepository.GetAsync(c => c.ID.Equals(obj.ID));
            //param.Grupo = obj.Grupo;
            //param.Tipo = obj.Tipo;
            //param.Chave = obj.Chave;
            param.Valor = obj.Valor;
           // param.Ativo = obj.Ativo;
            param.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
            param.DataAlteracao = obj.DataAlteracao;

            _unitOfWork.ParametroSistemaRepository.Update(param);

            await ReplicaValorParametro(param.Chave, (param.EmpresasID.Equals("Y")  ? "G" : "Y"), obj);

            return _unitOfWork.Commit();
        }

        private async Task ReplicaValorParametro(string chave, string empresaID, ParametroSistemaDto obj)
        {
            switch (chave)
            {
                case "gruposClienteExportacao":
                    var cloneParam = await _unitOfWork.ParametroSistemaRepository.GetAsync(c => c.Chave.Equals("gruposClienteExportacao") && c.EmpresasID.Equals(empresaID));
                    //cloneParam.Grupo = obj.Grupo;
                    //cloneParam.Tipo = obj.Tipo;
                    //cloneParam.Chave = obj.Chave;
                    cloneParam.Valor = obj.Valor;
                    // param.Ativo = obj.Ativo;
                    cloneParam.UsuarioIDAlteracao = obj.UsuarioIDAlteracao;
                    cloneParam.DataAlteracao = obj.DataAlteracao;

                    _unitOfWork.ParametroSistemaRepository.Update(cloneParam);
                    break;

                default:
                    break;
            }
        }

        //public async Task<bool> Update(List<ParametroSistemaDto> obj)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
