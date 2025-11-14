using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Yara.Servicos.LiberacaoAutomatica.Entidades;
using Yara.Servicos.LiberacaoAutomatica.Repositorio;

namespace Yara.Servicos.LiberacaoAutomatica.Servicos
{
    public class ServicoLiberacaoAutomatica : TimerBase
    {
        private readonly Logs registroLog;

        public ServicoLiberacaoAutomatica()
        {
            registroLog = new Logs();
            this.Rodando = false;
        }
        public async void Iniciar()
        {
            while (!this.Rodando)
            {
                Thread.Sleep(10000);

                 await ProcessarPassos(); 
            }

        }

        public void Parar()
        {
            this.Rodando = true;

        }

        public async Task ProcessarPassos()
        {
            try
            {

                var carteiras = new ProcessamentoCarteiras();
                var limiteCarteiras = new LimiteCreditoClientes();
                var dividas = new DividaClientes();
                var vigencias = new VigenciaCarteiras();
                var conceitocobranca = new ConceitoCobrancaClientes();
                var serasa = new SerasaClientes();
                var clientebloqueado = new ClientesBloqueados();
                //var liberacaomanual = new LiberacoesManuais();
                var liberacaoprazoentrega = new LiberacoesPrazosEntregas();
                registroLog.LogInformacao("Resgatando informaçoes do Processamento das Carteiras");

                var processarcarteiras = carteiras.ListaCarteiras(ProcessamentoCarteiraStatus.Aguardando);
                registroLog.LogInformacao($"Total de registros para Processamento das Carteiras {processarcarteiras.Count()}");

                foreach (var carteira in processarcarteiras)
                {

                    registroLog.LogInformacao($"Iniciado o Processamento da Carteira do cliente {carteira.Cliente}");

                    carteiras.AtualizarStatus(carteira.ID, ProcessamentoCarteiraStatus.EmExecucao);
                    registroLog.LogInformacao($"Status em Execução (2) da tabela de Processamento da Carteira do cliente {carteira.Cliente}");


                    //PASSO 1
                    registroLog.LogInformacao($"Iniciando o Passo 01 Vigencia em carteira do cliente {carteira.Cliente}");
                    var retorno = await vigencias.ProcessarVigencias(carteira.Cliente, carteira.ID);
                    registroLog.LogInformacao($"Finalizado o Passo 01 Vigencia em carteiras do cliente {carteira.Cliente}");

                    if (retorno == 3)
                        registroLog.LogInformacao($"Finalizado o Processamento da Carteira do cliente {carteira.Cliente} por não possuir limite em vigência");
                    else
                    {
                        //PASSO 2
                        registroLog.LogInformacao($"Iniciando o Passo 02 Limite de Crédito do cliente {carteira.Cliente}");
                        await limiteCarteiras.ProcessarLimiteCreditoCliente(carteira.Cliente, carteira.ID);
                        registroLog.LogInformacao($"Finalizado o Passo 02 Limite de Crédito do cliente {carteira.Cliente}");

                        //PASSO 3
                        registroLog.LogInformacao($"Iniciando o Passo 03 Dividas do cliente {carteira.Cliente}");
                        await dividas.ProcessarDividas(carteira.Cliente, carteira.ID);
                        registroLog.LogInformacao($"Finalizado o Passo 03 Dividas do cliente {carteira.Cliente}");

                        //PASSO 4
                        registroLog.LogInformacao($"Iniciando o Passo 04 Conceito de Cobrança do cliente {carteira.Cliente}");
                        await conceitocobranca.ProcessarConceitoCobranca(carteira.Cliente, carteira.ID);
                        registroLog.LogInformacao($"Finalizado o Passo 04 Conceito de Cobrança do cliente {carteira.Cliente}");

                        //PASSO 5
                        registroLog.LogInformacao($"Iniciando o Passo 05 Serasa do cliente {carteira.Cliente}");
                        await serasa.ProcessarSerasa(carteira.Cliente, carteira.ID);
                        registroLog.LogInformacao($"Finalizado o Passo 05 Serasa do cliente {carteira.Cliente}");
                        //PASSO 6
                        registroLog.LogInformacao($"Iniciando o Passo 06 Cliente Bloqueado do cliente {carteira.Cliente}");
                        await clientebloqueado.ProcessarClienteBloqueado(carteira.Cliente, carteira.ID);
                        registroLog.LogInformacao($"Finalizado o Passo 06 Cliente Bloqueado do cliente {carteira.Cliente}");

                        //PASSO 7
                        //Passo Comentado pois será inserido para excluir em casa passo
                        //registroLog.LogInformacao($"Iniciando o Passo 07 Liberação Manual do cliente {carteira.Cliente}");
                        //await liberacaomanual.ProcessarLiberacaoManual(carteira.Cliente, carteira.ID);

                        //PASSO 8
                        registroLog.LogInformacao($"Iniciando o Passo 08 Liberação Prazo de Entrega do cliente {carteira.Cliente}");
                        await liberacaoprazoentrega.ProcessarLiberacaoPrazoEntrega(carteira.Cliente, carteira.ID);
                        registroLog.LogInformacao($"Finalizado o Passo 08 Liberação Prazo de Entrega do cliente {carteira.Cliente}");

                        registroLog.LogInformacao($"Atualiza o status para Concluido (3) da tabela de Processamento da Carteira do cliente {carteira.Cliente}");

                        carteiras.AtualizarStatus(carteira.ID, ProcessamentoCarteiraStatus.Concluido);

                        registroLog.LogInformacao($"Finalizado o Processamento da Carteira do cliente {carteira.Cliente}");
                    }
                }
                registroLog.LogInformacao($"Finalizado o Processamento da Carteiras");
            }
            catch (Exception e)
            {
                registroLog.LogError(e);
            }
        }

    }
}