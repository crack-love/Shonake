namespace Shotake
{
    interface IDataChangeListener<T>
    {
        void OnDataChanged(string key, T value);
    }
}
