namespace Yara.Service.Serasa.Interface
{
    public interface ISerializar<T> where T:class
    {
         T Serasa(string retorno);
    }
}