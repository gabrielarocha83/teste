#pragma warning disable 1591
namespace Yara.WebApi.ViewModel
{
    public class GenericResult<TEntity> : Result
    {
        public TEntity Result { get; set; }
    }

    public class TotalGenericResult<TEntity> : GenericResult<TEntity>
    {
        public decimal Total { get; set; }
    }

    public class Result
    {
        public int Count { get; set; }
        public bool Success { get; set; }
        public string[] Errors { get; set; }
    }
}