using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public enum MonsterType
{
    Normal,
    Fire
}
public struct StageDate
{
    public int Level;
    public int FloorLevel;
    public float PlayTime;
    public float ReLoadTime;
    public MonsterType MonsterType;
    public int MonsterAmount;
}
public class StageInfoTable
{
    private List<StageDate> stageDataList = new List<StageDate>();

    public List<StageDate> StageDataList
    {
        get
        {
            if(stageDataList.Count <= 0)
                LoadData();

            return stageDataList;
        }
    }

    private void LoadData()
    {
        string folderPath = "Assets/DataSheets/";
        string[] csvFiles = Directory.GetFiles(folderPath,  "StageInfoTable.csv");

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
                    stageDataList.Add(new StageDate()
                    {
                        Level = int.Parse(_data[0]),
                        FloorLevel = int.Parse(_data[1]),
                        PlayTime = float.Parse(_data[2]),
                        ReLoadTime = float.Parse(_data[3]),
                        MonsterType = (MonsterType)int.Parse(_data[4]),
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
}
