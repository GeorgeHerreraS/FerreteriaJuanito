using FerreteriaJuanito.Clases.Utils;

namespace FerreteriaJuanito.Interface
{
    public interface logsInterface
    {
        void Log(string Clase,DateTime FechaLog,string LogName,string LogDescription);
        public Logs AddLogs(Logs logs) {
            logs.LogName = logs.LogName; 
            logs.LogDescription = logs.LogDescription;
            logs.FechaLog = logs.FechaLog;
            logs.Clase = logs.Clase;
            return logs;
        }
    }
}
