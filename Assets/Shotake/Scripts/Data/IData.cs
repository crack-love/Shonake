namespace Shotake
{
    interface IData
    {
        string Key { get; }

        bool IsNeedWrite { get; }
    }

    interface IData<T> : IData
    {
        T Value { get; set; }

        void ListenDataChanged(IDataChangeListener<T> listener);

        void UnListenDataChanged(IDataChangeListener<T> listener);
    }
}
