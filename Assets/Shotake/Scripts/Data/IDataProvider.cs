namespace Shotake
{
    interface IDataProvider
    {
        void SaveData(IData data);

        void LoadData(IData data);
    }
}
