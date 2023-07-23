using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum MonsterPropertyType
{
    Fire,
    Zombie
}

public struct MonsterData
{
    public MonsterPropertyType monsterType;
    public string prefabName;
    public float speed;
    public float hp;
}

public class MonsterInfoTable : ICsvDataInterface
{
    private Dictionary<string, MonsterData> monsterDic =
        new Dictionary<string, MonsterData>();

    public void LoadData()
    {
        string folderPath = "Assets/DataSheets/";
        string[] csvFiles = Directory.GetFiles(folderPath, this.ToString() + ".csv");

        foreach (string csvFile in csvFiles)
        {
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
                    MonsterData monsterDB = new MonsterData()
                    {
                        monsterType = (MonsterPropertyType)int.Parse(_data[1]),
                        prefabName = _data[2],
                        speed = float.Parse(_data[3]),
                        hp = float.Parse(_data[4])
                    };
                    monsterDic.Add(_data[0], monsterDB);
                }
            }
        }
        catch
        {
            Debug.Log("MonsterInfoTable Parsing Error");
        }
    }

    public T GetData<T>(string key, int? dickey = null)
    {
        MonsterData monsterDB;

        monsterDic.TryGetValue(key, out monsterDB);

        if (!monsterDic.Equals(default(MonsterData)))
            return (T)Convert.ChangeType(monsterDB, typeof(T));
        else
            return default(T);
    }
}
