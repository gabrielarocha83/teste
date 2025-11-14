using System;
using System.Collections.Generic;
using Yara.Servicos.LiberacaoAutomatica.Entidades;

namespace Yara.Servicos.LiberacaoAutomatica.Repositorio
{
    public interface IProcessamentoCarteiras
    {
        IEnumerable<ProcessamentoCarteira> ListaCarteiras(ProcessamentoCarteiraStatus Status);

        void AtualizarStatus(Guid id, ProcessamentoCarteiraStatus Status);
    }
}