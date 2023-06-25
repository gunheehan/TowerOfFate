using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum TowerType
{
    Normal
}

public struct TowerData
{
    public string name;
    public TowerType TowerType;
    public int Level;
    public float Power;
    public float Speed;
    public int AttackArea;
    public int Price;
}

public class TowerInfoTable : ICsvDataInterface
{
    private List<TowerData> towerDataList = new List<TowerData>();

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
                    towerDataList.Add(new TowerData()
                    {
                        name = _data[0],
                        TowerType = (TowerType)int.Parse(_data[1]),
                        Level = int.Parse(_data[2]),
                        Power = float.Parse(_data[3]),
                        Speed = float.Parse(_data[4]),
                        AttackArea = int.Parse(_data[5]),
                        Price = int.Parse(_data[6])
                    });
                }
            }
        }
        catch
        {
            Debug.Log("StageInfoTable Parsing Error");
        }
    }

    public T GetData<T>(string key)
    {
        if (int.TryParse(key, out int index))
        {
            if (index >= 0 && index < towerDataList.Count)
            {
                return (T)Convert.ChangeType(towerDataList[index], typeof(T));
            }
            else
            {
                Debug.LogWarning("Index out of range.");
            }
        }
        else
        {
            Debug.LogWarning("Invalid key format.");
        }

        return default(T);
    }
}
