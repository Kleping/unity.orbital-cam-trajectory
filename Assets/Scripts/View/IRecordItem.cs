using UnityEngine.Events;

namespace View
{
    public interface IRecordItem
    {
        void Init(long binary, UnityAction onClick);

        void Clear();
    }
}