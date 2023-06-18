using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StageInfoTable : MonoBehaviour
{

    public void LoadSheetData()
    {
        string folderPath = "Assets/DataSheets/";
        string[] csvFiles = Directory.GetFiles(folderPath,  "StageInfoTable.csv");

        foreach (string csvFile in csvFiles)
        {
            string fileName = Path.GetFileNameWithoutExtension(csvFile);
            string[] key = fileName.Split('_');

            string csvData = File.ReadAllText(csvFile);
        }
    }
}
