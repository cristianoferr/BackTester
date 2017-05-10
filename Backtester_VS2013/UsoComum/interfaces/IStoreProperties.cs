
namespace UsoComum.interfaces
{
    public interface IStoreProperties
    {
        object GetPropriedade(string key);
        void SetPropriedade(string key, object node);
        void RemovePropriedade(string key);
    }
}
