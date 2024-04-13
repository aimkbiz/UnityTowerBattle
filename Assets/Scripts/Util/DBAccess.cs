using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System;

public class DBAccess {
    //public static DBResult MasterData = null;
    private static string newLine = "\n";
    private static string csvUserPath = Application.persistentDataPath + "/";
    private static string csvResourcesPath = @"Csv/";

    /// <summary>
    /// csvのデータの一覧を取得する
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static StringCollection loadDataList(string fileName) {
        try {
            TextAsset csv = Resources.Load<TextAsset>(csvResourcesPath + fileName);
            StringCollection csvList = new StringCollection();
            csvList.AddRange(spritText(csv.text));
            csvList.RemoveAt(0);// 1行目を削除

            // 最後の行が空白の場合
            if ("" == csvList[csvList.Count - 1]){
                csvList.RemoveAt(csvList.Count - 1);
            }
            
            return csvList;

        } catch (UnityException e) {
            Debug.Log("loadDataFromCSV Error! : " + e.Message);
        }

        return new StringCollection();
    }
    /*
    /// <summary>
    /// データの一覧を取得する(通信の入った正規処理。loadDataListは全て移行する)
    /// </summary>
    /// <param name="tableNm"></param>
    /// <returns></returns>
    public static List<object> LoadDataList(string tableNm) {
        try {
            List<object> dtList = new List<object>();

            if (!Setting.COMMUNICATION_FLG) {
                TextAsset csv = Resources.Load<TextAsset>(csvResourcesPath + tableNm);
                StringCollection csvList = new StringCollection();
                csvList.AddRange(spritText(csv.text));
                string[] headerCsv = csvList[0].Trim().Split(',');
                csvList.RemoveAt(0);// 1行目を削除

                // 最後の行が空白の場合
                if ("" == csvList[csvList.Count - 1]) {
                    csvList.RemoveAt(csvList.Count - 1);
                }

                foreach (string csvLine in csvList) {
                    string[] rows = csvLine.Trim().Split(',');
                    Dictionary<string, object> dt = new Dictionary<string, object>();
                    for (int i = 0; i < rows.Length; i++) {
                        dt.Add(headerCsv[i], rows[i]);
                    }
                    dtList.Add(dt);
                }
                return dtList;
            }
            return (List<object>)DBManager.Instance.MasterData[tableNm];

        } catch (UnityException e) {
            Debug.Log("LoadDataList table : " + tableNm + " Error! : " + e.Message);
        }

        return new List<object>();
    }*/

    /// <summary>
    /// csvのデータを取得する
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string loadData(string fileName) {
        try {
            TextAsset csv = Resources.Load<TextAsset>(csvResourcesPath + fileName);
            string csvList = "";
            csvList = spritText(csv.text)[1];
            return csvList;

        } catch (UnityException e) {
            Debug.Log("loadDataFromCSV Error! : " + e.Message);
        }

        return "";
    }

    /// <summary>
    /// csvのデータを取得する
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static Dictionary<string, object> LoadData(string tableNm) {
        try {
            TextAsset csv = Resources.Load<TextAsset>(csvResourcesPath + tableNm);
            Dictionary<string, object> csvList = new Dictionary<string,object>();

            StringCollection headerCsv = new StringCollection();
            headerCsv.AddRange(spritText(csv.text)[0].Trim().Split(','));

            string[] rows = spritText(csv.text)[1].Trim().Split(',');
            for (int i = 0; i < rows.Length; i++) {
                csvList.Add(headerCsv[i], rows[i]);
            }
            return csvList;

        } catch (UnityException e) {
            Debug.Log("loadDataFromCSV Error! : " + e.Message);
        }

        return new Dictionary<string,object>();
    }

    /// <summary>
    /// データの一覧を取得する
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static StringCollection loadUserDataList(string fileName) {
        #if UNITY_WEBPLAYER
            return loadDataList(fileName);
        #else
            try {
                StringCollection csvList = new StringCollection();
                FileInfo fi = new FileInfo(csvUserPath + fileName + ".csv");

                //存在しない場合
                if (!fi.Exists) {
                    TextAsset csvResources = Resources.Load<TextAsset>(csvResourcesPath + fileName);
                    StringCollection csvResourcesList = new StringCollection();
                    csvResourcesList.AddRange(spritText(csvResources.text));

                    FileStream fsResources = File.Create(csvUserPath + fileName + ".csv");
                    fsResources.Close();
                    StreamWriter swResources = new StreamWriter(csvUserPath + fileName + ".csv", false, System.Text.Encoding.GetEncoding("utf-8"));
                
                    foreach (String txtCsv in csvResourcesList) {
                        swResources.Write(txtCsv + newLine);
                    }

                    swResources.Flush();
                    swResources.Close();
                }
        
                StreamReader sr = new StreamReader(csvUserPath + fileName + ".csv");

                while (sr.Peek() != -1) {
                    string csvText = sr.ReadLine();
                    if (csvText != "") {
                        csvList.Add(csvText);
                    }
                }
                sr.Close();
                if (csvList.Count > 0) { 
                    csvList.RemoveAt(0);// 1行目を削除
                }
                return csvList;
            } catch (UnityException e) {
                Debug.Log("loadDataFromCSV Error! : " + e.Message);
            }
            return new StringCollection();
        #endif
    }
    /*
    /// <summary>
    /// データの一覧を取得する(通信の入った正規処理。loadUserDataListは全て移行する)
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static List<object> LoadUserDataList(string tableNm) {
        if (Setting.COMMUNICATION_FLG) {
            return (List<object>)DBManager.Instance.UserData[tableNm];
        } else {
            #if UNITY_WEBPLAYER
                return LoadDataList(tableNm);
            #else
            try {
                    List<object> dtList = new List<object>();
                    FileInfo fi = new FileInfo(csvUserPath + tableNm + ".csv");

                    //存在しない場合
                    if (!fi.Exists) {
                        TextAsset csvResources = Resources.Load<TextAsset>(csvResourcesPath + tableNm);
                        StringCollection csvResourcesList = new StringCollection();
                        csvResourcesList.AddRange(spritText(csvResources.text));

                        FileStream fsResources = File.Create(csvUserPath + tableNm + ".csv");
                        fsResources.Close();
                        StreamWriter swResources = new StreamWriter(csvUserPath + tableNm + ".csv", false, System.Text.Encoding.GetEncoding("utf-8"));
                
                        foreach (String txtCsv in csvResourcesList) {
                            swResources.Write(txtCsv + newLine);
                        }

                        swResources.Flush();
                        swResources.Close();
                    }

                    StreamReader sr = new StreamReader(csvUserPath + tableNm + ".csv");
                    StringCollection headerCsv = new StringCollection();
                    int srIndex = 0;
                    while (sr.Peek() != -1) {
                        string csvText = sr.ReadLine();
                        if (csvText != "") {
                            if (0 == srIndex) {
                                headerCsv.AddRange(csvText.Trim().Split(','));
                            } else {
                                string[] rows = csvText.Trim().Split(',');
                                Dictionary<string, object> dt = new Dictionary<string, object>();
                                for (int i = 0; i < rows.Length; i++) {
                                    dt.Add(headerCsv[i], rows[i]);
                                }
                                dtList.Add(dt);
                            }
                        }
                        srIndex++;
                    }
                    sr.Close();
                    if (csvList.Count > 0) { 
                        csvList.RemoveAt(0);// 1行目を削除
                    }
                    return dtList;
                } catch (UnityException e) {
                    Debug.Log("loadDataFromCSV Error! : " + e.Message);
                }
                return new List<object>();
            #endif
        }
    }*/

    /// <summary>
    /// csvのデータを取得する
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static string loadUserData(string fileName) {
        #if UNITY_WEBPLAYER
            return loadData(fileName);
        #else
            try {
                StringCollection csvList = loadUserDataList(fileName);
                if (csvList.Count == 0) {
                    return "";
                }
                return csvList[0];

            } catch (UnityException e) {
                Debug.Log("loadDataFromCSV Error! : " + e.Message);
            }

            return "";
        #endif
    }

    /*
    /// <summary>
    /// データを取得する(通信の入った正規処理。loadUserDataListは全て移行する)
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static Dictionary<string, object> LoadUserData(string tableNm) {
        if (Setting.COMMUNICATION_FLG) {
            return (Dictionary<string, object>)DBManager.Instance.UserData[tableNm];
        } else { 
            #if UNITY_WEBPLAYER
                return LoadData(tableNm);
            #else
                try {
                    List<object> csvList = LoadUserDataList(tableNm);
                    if (csvList.Count == 0) {
                        return new Dictionary<string,object>();
                    }
                    return (Dictionary<string, object>)csvList[0];

                } catch (UnityException e) {
                    Debug.Log("LoadUserData Error! : " + e.Message);
                }

                return new Dictionary<string, object>();
            #endif
        }
    }*/

    /// <summary>
    /// ヘッターを取得する
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    private static string loadUserDataHeader(string fileName) {
        try {
            StringCollection csvList = new StringCollection();
            StreamReader sr = new StreamReader(csvUserPath + fileName + ".csv");
            string csvHeader = sr.ReadLine();
            sr.Close();
            return csvHeader;
        } catch (UnityException e) {
            Debug.Log("loadDataFromCSV Error! : " + e.Message);
        }
        return "";
    }


    /// <summary>
    /// csvにデータを書き込む(一覧)
    /// </summary>
    /// <param name="fileName"></param>
    public static void saveUserDataList(string fileName, StringCollection csvList) {
        #if !UNITY_WEBPLAYER
            string loadCsvHeader = loadUserDataHeader(fileName);

            StreamWriter sw = new StreamWriter(csvUserPath + fileName + ".csv", false, System.Text.Encoding.GetEncoding("utf-8"));

            sw.Write(loadCsvHeader + newLine);

            foreach (String txtCsv in csvList) {
                sw.Write(txtCsv + newLine);
            }

            sw.Flush();
            sw.Close();
        #endif
    }

    /// <summary>
    /// csvにデータを書き込む
    /// </summary>
    /// <param name="fileName"></param>
    public static void saveUserData(string fileName, String csvData) {
        #if !UNITY_WEBPLAYER
            string loadCsvHeader = loadUserDataHeader(fileName);

            StreamWriter sw = new StreamWriter(csvUserPath + fileName + ".csv", false, System.Text.Encoding.GetEncoding("utf-8"));

            sw.Write(loadCsvHeader + newLine);
            sw.Write(csvData + newLine);

            sw.Flush();
            sw.Close();
        #endif
    }

    /// <summary>
    /// データを削除
    /// </summary>
    public static void DeleteDirectory() {
        #if UNITY_EDITOR
        string[] filePathList = Directory.GetFiles(csvUserPath);
            foreach(string filePath in filePathList){
                File.Delete(filePath);
            }
        #endif
    }

    /*
    /// <summary>
    /// データバージョンを判定してバージョンが低い場合はデータを削除
    /// </summary>
    public void DeleteDataVersion() {
        #if UNITY_EDITOR
            string fileName = "data_version";
            try{
                FileInfo fi = new FileInfo(csvUserPath + fileName + ".csv");

                //存在しない場合
                if (!fi.Exists) {
                    DeleteDirectory();
                    FileStream fsResources = File.Create(csvUserPath + fileName + ".csv");
                    fsResources.Close();
                } else {
                    StreamReader sr = new StreamReader(csvUserPath + fileName + ".csv");
                    
                    string csvText = sr.ReadLine();
                    sr.Close();
                    if (int.Parse(csvText) < Setting.DATA_VERSION) {
                        DeleteDirectory();
                    }
                }

                StreamWriter swResources = new StreamWriter(csvUserPath + fileName + ".csv", false, System.Text.Encoding.GetEncoding("utf-8"));
                swResources.Write(Setting.DATA_VERSION);
                swResources.Flush();
                swResources.Close();
            } catch (UnityException e) {
                Debug.Log("loadDataFromCSV Error! : " + e.Message);
            }
        #endif
    }*/

    /// <summary>
    /// 改行コードで分割したstringの配列を取得する
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    private static string[] spritText(string text){
        return text.Split('\n');
    }
}