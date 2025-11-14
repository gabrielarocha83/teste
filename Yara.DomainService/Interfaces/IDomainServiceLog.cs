using System.Threading.Tasks;
using Yara.Domain;

namespace Yara.DomainService.Interfaces
{
    public interface IDomainServiceLog
    {
       
        bool Create(Log obj);
       
    }
}