using System.Collections.Generic;

public enum TableType
{
    Stage,
    Tower
}
public class CsvTableManager
{
    private static CsvTableManager instance;

    public static CsvTableManager Instance
    {
        get
        {
            if (instance == null)
                instance = new CsvTableManager();

            return instance;
        }
    }

    private Dictionary<TableType, ICsvDataInterface> tableDic = new Dictionary<TableType, ICsvDataInterface>();  

    private StageInfoTable StageInfoTable = new StageInfoTable();
    private TowerInfoTable TowerInfoTable = new TowerInfoTable();

    public void LoadData()
    {
        tableDic.Add(TableType.Stage, StageInfoTable);
        tableDic.Add(TableType.Tower, TowerInfoTable);
        
        foreach (ICsvDataInterface table in tableDic.Values)
        {
            table.LoadData();
        }
    }

    public T GetData<T>(TableType tabletype, string key)
    {
        return tableDic[tabletype].GetData<T>(key);
    }
}
