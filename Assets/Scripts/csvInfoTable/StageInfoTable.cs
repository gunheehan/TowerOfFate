using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public struct StageData
{
    public int Level;
    public int FloorLevel;
    public float PlayTime;
    public float ReLoadTime;
    public MonsterPropertyType MonsterType;
    public int MonsterAmount;
}

public class StageInfoTable :  ICsvDataInterface
{
    private List<StageData> stageDataList = new List<StageData>();

    public void LoadData()
    {
        string folderPath = "Assets/DataSheets/";
        string[] csvFiles = Directory.GetFiles(folderPath, this.ToString() +".csv");

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
                    stageDataList.Add(new StageData()
                    {
                        Level = int.Parse(_data[0]),
                        FloorLevel = int.Parse(_data[1]),
                        PlayTime = float.Parse(_data[2]),
                        ReLoadTime = float.Parse(_data[3]),
                        MonsterType = (MonsterPropertyType)int.Parse(_data[4]),
                        MonsterAmount = int.Parse(_data[5])
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
        if (int.TryParse(key, out int index))
        {
            if (index >= 0 && index < stageDataList.Count)
            {
                return (T)Convert.ChangeType(stageDataList[index], typeof(T));
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
