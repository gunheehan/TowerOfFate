public interface ICsvDataInterface
{
    public void LoadData();
    public T GetData<T>(string key, int? dickey = null);
}
