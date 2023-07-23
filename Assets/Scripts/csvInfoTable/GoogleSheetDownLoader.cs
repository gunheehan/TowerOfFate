using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

public enum VersionTableColumn
{
    Indexkey=0,
    GoogleSheetKey=1,
    DBName=2
}
public class GoogleSheetDownLoader : MonoBehaviour
{
     private static readonly string DataBaseDirectoryPath = "Assets/DataSheets/";
    private static readonly string Extend = ".csv";
      private static readonly string VersionTableDBUrl = "1u8bRwXy3sAaxTn9ywYKNiAcfmokQ0aP6sWAPauFQvTw";
    
    private static readonly string VersionTableName = "Version";
    
    [MenuItem("Window/DownLoadCSVFromGoogleSheet")]
    public async static void DownLoadCSVFromGoogleSheet()
    {
        if (!Directory.Exists(DataBaseDirectoryPath))
        {
            Directory.CreateDirectory(DataBaseDirectoryPath);
        }
        else
        {
            string[] files =  Directory.GetFiles(DataBaseDirectoryPath);
            if (files.Length>0)
            {
                foreach (var _file in files)
                {
                    var _extend = _file.Split('.');
                    if (_extend[_extend.Length - 1] == "csv")
                    {
                        File.Delete(_file);
                    }
                
                }
            }
        }
       
        string versionData = await DownloadData(VersionTableDBUrl, VersionTableName,0f);
        string[,] versionParseData = VersionLoader(versionData);
        //
        for (int i = 0; i < versionParseData.GetLength(0) ; i++)
        {
            string csvStrData = await DownloadData(versionParseData[i, (int)VersionTableColumn.GoogleSheetKey],versionParseData[i, (int)VersionTableColumn.DBName],(float)i/(float)versionParseData.GetLength(0));

            

            string DbName = versionParseData[i, (int)VersionTableColumn.DBName];
            EditorUtility.DisplayProgressBar("CSV DOWNLOADER",versionParseData[i, (int)VersionTableColumn.DBName],0.5f);
            if (!string.IsNullOrEmpty(DbName))
            {
                string csvPath = DataBaseDirectoryPath + DbName + Extend;
                using (StreamWriter sw = new StreamWriter(csvPath, false, Encoding.Unicode))
                {
               
                    await sw.WriteAsync(csvStrData);
                    await sw.FlushAsync();
                    sw.Close();
                    EditorUtility.DisplayProgressBar("CSV DOWNLOADER",versionParseData[i, (int)VersionTableColumn.DBName],1f);
                }
            }
        }
        EditorUtility.ClearProgressBar();
        Debug.Log("CSV Download Complete");
        AssetDatabase.Refresh();
    }
    static Task<string> DownloadData(string dbUrl, string dbName,float _progress)
    {
        var tcs = new TaskCompletionSource<string>();
        string url = "https://docs.google.com/spreadsheets/d/" + dbUrl + "/export?format=csv";
        string downloadData = null;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            var handle = webRequest.SendWebRequest();
            while (true)
            {
                if (handle.isDone == true)
                {
                    break;
                }
            }

            if (webRequest.isNetworkError)
            {
                Debug.LogError(dbName+"CSV Download Failed");
            }
            else
            {
                downloadData = webRequest.downloadHandler.text;
                tcs.SetResult(downloadData);
            }
        }
        EditorUtility.DisplayProgressBar("CSV DOWNLOADER",dbName,_progress);
        return tcs.Task;
    }
    static string[,] VersionLoader(string _recvdata)
    {
        string[,] result;
        string[] lines = _recvdata.Split("\n");
        string[] FirstLine = lines[0].Split(',');
        result = new string[lines.Length, FirstLine.Length];
        for (int i = 1; i < lines.Length; i++)
        {
            string[] lineData = lines[i].Split(',');
            for (int j = 0; j < lineData.Length; j++)
            {
                lineData[j] = lineData[j].TrimEnd('\r');
                result[i,j] = lineData[j];
            }
        }
        return result;
    }
}
