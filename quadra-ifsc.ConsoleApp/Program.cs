using quadra_ifsc.Logs;
using quadra_ifsc.Orm;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace quadra_ifsc.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MigradorBancoDadosQuadraIfsc.AtualizarBancoDados();
            ConfiguracaoLogsQuadraIfsc.ConfigurarEscritaLogs();
        }
    }
}
