﻿
namespace UsoComum.interfaces
{
    public interface IStoreProperties
    {
        object GetPropriedade(string key);
        float GetPropriedadeAsFloat(string propriedade);
        void SetPropriedade(string key, object node);
        void RemovePropriedade(string key);
    }
}
