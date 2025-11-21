using System;

namespace Yara.Service.Serasa
{
    public class SerasaException: Exception
    {

        public SerasaException(string message) : base(message) { }

    }
}
