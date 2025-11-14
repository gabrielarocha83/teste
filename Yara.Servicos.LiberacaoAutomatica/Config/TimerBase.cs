using System;
using System.Configuration;

namespace Yara.Servicos.LiberacaoAutomatica
{
    public class TimerBase
    {
        public bool Rodando { get; set; }
        public int Tempo => Convert.ToInt32(ConfigurationManager.AppSettings["Tempo"]);
    }
}