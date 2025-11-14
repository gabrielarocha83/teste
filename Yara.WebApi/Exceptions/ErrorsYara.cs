using System;
using System.Data.Entity.Validation;

namespace Yara.WebApi
{
    public  class ErrorsYara
    {
        public  string PrepareExceptionString(Exception ex)
        {

            string exceptionMessage = string.Format("{0}\r\n\t{1}\r\n---------------------------\r\n", ex.Message, ex.ToString());

            if (ex.InnerException != null)
                exceptionMessage += string.Format("\t{0}", PrepareExceptionString(ex.InnerException));

            return exceptionMessage;

        }

        public string PrepareEntityValidationErrors(DbEntityValidationException dbEx)
        {

            string exceptionMessage = "";

            foreach (var iEx in dbEx.EntityValidationErrors)
            {
                foreach (var iiEx in iEx.ValidationErrors)
                {
                    exceptionMessage += string.Format("- {0}\t{1}:{2}\r\n", iEx.Entry, iiEx.PropertyName, iiEx.ErrorMessage);
                }
                
            }

            return exceptionMessage;

        }

        public void ErrorYara(Exception e)
        {
          
            var logger = log4net.LogManager.GetLogger("YaraLog");

            if (e is DbEntityValidationException)
            {
                logger.Error(PrepareEntityValidationErrors(e as DbEntityValidationException));
            }
            else
            {
                logger.Error(PrepareExceptionString(e));
            }

        }
    }
}