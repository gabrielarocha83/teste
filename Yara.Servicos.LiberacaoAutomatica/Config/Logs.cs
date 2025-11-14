using System;
using System.Diagnostics;
// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Local

namespace Yara.Servicos.LiberacaoAutomatica
{
    public class Logs
    {
        private string nomeArquivoLog = "YaraLog";

        private string ObterDetalhesException(Exception ex)
        {

            string exceptionMessage = $"{ex.Message}\r\n\t{ex}\r\n---------------------------\r\n";

            if (ex.InnerException != null)
                exceptionMessage += $"\t{ObterDetalhesException(ex.InnerException)}";

            return exceptionMessage;

        }

        private EventLogEntryType ObterTipoEventLog(TipoLog tipo)
        {
            EventLogEntryType eventLog;

            switch (tipo)
            {
              
                case TipoLog.Warning:
                    eventLog = EventLogEntryType.Warning;
                    break;

                default:
                    eventLog = EventLogEntryType.Information;
                    break;


            }

            return eventLog;


        }

        private void LogEventLog(TipoLog tipo, string detalhe)
        {

            try
            {

                var logSource = "Liberação Automática Yara";
                var logArea = "Application";

                if (!EventLog.SourceExists(logSource))
                    EventLog.CreateEventSource(logSource, logArea);

                var tipoEvento = ObterTipoEventLog(tipo);

                EventLog.WriteEntry(logSource, detalhe, tipoEvento, 111);
               

            }
            catch (Exception eex)
            {
                LogFileErro(eex);
            }



        }

        private void LogEventLogErro(Exception ex)
        {

            try
            {

                var logSource = "Liberação Automática Yara";
                var logArea = "Application";

                if (!EventLog.SourceExists(logSource))
                    EventLog.CreateEventSource(logSource, logArea);

                EventLog.WriteEntry(logSource, ObterDetalhesException(ex),EventLogEntryType.Error, 999);


            }
            catch (Exception eex)
            {
                LogFileErro(eex);
            }



        }

        private void LogFileErro(Exception e)
        {
            var logger = log4net.LogManager.GetLogger(nomeArquivoLog);

            logger.Error(ObterDetalhesException(e));
        }

        private void LogFile(string detalhe, TipoLog tipo)
        {
            var logger = log4net.LogManager.GetLogger(nomeArquivoLog);
            switch (tipo)
            {

                case TipoLog.Informacao:
                    logger.Info(detalhe);
                    break;

                case TipoLog.Warning:
                    logger.Warn(detalhe);
                    break;

            }

        }

        public void LogInformacao(string detalhe)
        {


            LogFile(detalhe, TipoLog.Informacao);
            //LogEventLog(TipoLog.Informacao, detalhe);
        }

        public void LogAtencao(string detalhe)
        {
            LogFile(detalhe, TipoLog.Warning);
            LogEventLog(TipoLog.Warning, detalhe);
        }

        public void LogError(Exception e)
        {
           LogFileErro(e);
            LogEventLogErro(e);
        }

    }

    public enum TipoLog : short
    {
        Informacao = 1,
        Warning = 2,
        Erro = 3
    }
}