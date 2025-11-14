using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yara.Service.Serasa
{
    public class SerasaException: Exception
    {

        public SerasaException(string message) : base(message) { }

    }
}
