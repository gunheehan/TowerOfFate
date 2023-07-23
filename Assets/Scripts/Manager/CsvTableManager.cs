using System.Collections.Generic;

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
    
    private StageInfoTable StageInfoTable = new StageInfoTable();
    private TowerInfoTable TowerInfoTable = new TowerInfoTable();

    public void LoadData()
    {
        StageInfoTable.LoadData();
        TowerInfoTable.LoadData();
    }

    public TowerData GetTowerData(string key, int? dickey)
    {
        return TowerInfoTable.GetData<TowerData>(key, dickey);
    }

    public List<TowerData> GetTowerList()
    {
        return TowerInfoTable.GetData<List<TowerData>>(null);
    }

    public StageData GetStageData(string key)
    {
        return StageInfoTable.GetData<StageData>(key);
    }
}
