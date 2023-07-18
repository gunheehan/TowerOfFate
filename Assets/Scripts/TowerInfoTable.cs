using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public enum TowerType
{
    Normal,
    Freeze
}

public enum AttackType
{
    Hit,
    Slow,
    Fire
}

public struct TowerData
{
    public string name;
    public TowerType TowerType;
    public AttackType AttackType;
    public int Level;
    public float Power;
    public float Speed;
    public int AttackArea;
    public int Price;
}

public class TowerInfoTable : ICsvDataInterface
{
    private List<TowerData> towerDataList = new List<TowerData>();
    private Dictionary<TowerType, List<TowerData>> towerDic = new FlexibleDictionary<TowerType, List<TowerData>>();

    public void LoadData()
    {
        string folderPath = "Assets/DataSheets/";
        string[] csvFiles = Directory.GetFiles(folderPath, this.ToString() + ".csv");

        foreach (string csvFile in csvFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(csvFile);
            string[] key = fileName.Split('_');

            string csvData = File.ReadAllText(csvFile);
            DataParsing(csvData);
        }
    }

    private void DataParsing(string data)
    {
        string[] downloadData_split = data.Split("\r\n");
        int i = 1;
        try
        {
            for (i = 1; i < downloadData_split.Length; i++)
            {
                string[] _data = downloadData_split[i].Split(",");

                if (!string.IsNullOrEmpty(_data[0]))
                {
                    List<TowerData> towerList;
                    if (!towerDic.TryGetValue((TowerType)int.Parse(_data[1]), out towerList))
                    {
                        towerList = new List<TowerData>();
                        towerDic.Add((TowerType)int.Parse(_data[1]), towerList);
                    }
                    
                    towerList.Add(new TowerData()
                    {
                        name = _data[0],
                        TowerType = (TowerType)int.Parse(_data[1]),
                        Level = int.Parse(_data[2]),
                        Power = float.Parse(_data[3]),
                        Speed = float.Parse(_data[4]),
                        AttackArea = int.Parse(_data[5]),
                        Price = int.Parse(_data[6]),
                        AttackType = (AttackType)int.Parse(_data[7])
                    });
                }
            }
        }
        catch
        {
            Debug.Log("StageInfoTable Parsing Error");
        }
    }

    public T GetData<T>(string key, int? dickey = null)
    {
        if (typeof(T) == typeof(List<TowerData>))
        {
            T returnData = (T)(object)Convert.ChangeType(towerDic.Values.SelectMany(x=>x).ToList(), typeof(T));
            return returnData;
        }
        if (dickey == null)
        {
            return default(T);
        }

        List<TowerData> towerList;
        if (towerDic.TryGetValue((TowerType)dickey,out towerList))
        {
            TowerData towerdata = towerList.Find(o => o.name.Equals(key));
            
            if(!towerdata.Equals(default(TowerData)))
                return (T)Convert.ChangeType(towerdata, typeof(T));
        }
        else
        {
            Debug.LogWarning("Invalid key format.");
        }

        return default(T);
    }
}
