using System;
using System.ServiceProcess;
using System.Threading.Tasks;
using Yara.Servicos.LiberacaoAutomatica;
using Yara.Servicos.LiberacaoAutomatica.Servicos;

namespace Yara.Servicos.PortalFinanceiro
{
    public partial class YaraPortalFinanceiro : ServiceBase
    {

        ServicoLiberacaoAutomatica servicoLiberacaoAutomatica = new ServicoLiberacaoAutomatica();
        ServicoSAP SAP = new ServicoSAP();
        Logs registroLog = new Logs();
        private Task task;

        public YaraPortalFinanceiro()
        {
            InitializeComponent();
        }

        internal void Start()
        {
            this.OnStart(new string[0]);

        }

        protected override void OnStart(string[] args)
        {


            registroLog.LogInformacao($"Serviço Iniciado as {DateTime.Now.ToShortDateString()}");

             task = new Task(servicoLiberacaoAutomatica.Iniciar);
            task.Start();

        }

        protected override void OnStop()
        {
            registroLog.LogInformacao($"Serviço foi Parado as {DateTime.Now.ToShortDateString()}");
            servicoLiberacaoAutomatica.Parar();

        }
    }
}
