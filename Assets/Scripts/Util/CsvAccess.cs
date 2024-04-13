using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System;

public class CsvAccess {

    private static string newLine = "\n";
    private static string csvUserPath = Application.persistentDataPath + "/";
    private static string csvResourcesPath = @"Csv/";

    /// <summary>
    /// マスタ情報を取得する
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static List<Dictionary<string, object>> getMasterDataList(string tableNm) {
        try {
            TextAsset csv = Resources.Load<TextAsset>(csvResourcesPath + tableNm);
            List<Dictionary<string, object>> csvList = new List<Dictionary<string, object>>();

            StringCollection headerCsv = new StringCollection();
            headerCsv.AddRange(spritText(csv.text)[0].Trim().Split(','));

            StringCollection dataList = spritText(csv.text);
            dataList.RemoveAt(0);// 1行目を削除
            foreach (string dataLine in dataList) {
                string[] rows = dataLine.Trim().Split(',');
                Dictionary<string, object> csvData = new Dictionary<string, object>();
                for (int i = 0; i < rows.Length; i++) {
                    csvData.Add(headerCsv[i], rows[i]);
                }
                csvList.Add(csvData);
            }
            return csvList;

        } catch (UnityException e) {
            Debug.Log("loadDataFromCSV Error! : " + e.Message);
        }

        return new List<Dictionary<string, object>>();
    }
    public static Dictionary<string, object> getUserData(string tableNm) {
        return getUserDataList(tableNm)[0];
    }
    /// <summary>
    /// ユーザ情報を取得する
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static List<Dictionary<string, object>> getUserDataList(string tableNm) {
        #if !UNITY_WEBPLAYER
        try {
            FileInfo fi = new FileInfo(csvUserPath + tableNm + ".csv");

            //存在しない場合
            if (!fi.Exists) {
                TextAsset csv = Resources.Load(csvResourcesPath + tableNm) as TextAsset;
                StringCollection csvResourcesList = new StringCollection();
                csvResourcesList.AddRange(csv.text.Split('\n'));

                StreamWriter swResources = new StreamWriter(csvUserPath + tableNm + ".csv", true, System.Text.Encoding.GetEncoding("utf-8"));

                foreach (String txtCsv in csvResourcesList) {
                    swResources.Write(txtCsv + newLine);
                }

                swResources.Flush();
                swResources.Close();
            }

            StreamReader sr = new StreamReader(csvUserPath + tableNm + ".csv");
            List<Dictionary<string, object>> csvList = new List<Dictionary<string, object>>();

            StringCollection headerCsv = new StringCollection();
            string line = sr.ReadLine();
            headerCsv.AddRange(line.Trim().Split(','));

            while (sr.Peek() != -1) {
                string csvText = sr.ReadLine();
                if (csvText != "") {
                    string[] rows = csvText.Trim().Split(',');
                    Dictionary<string, object> csvData = new Dictionary<string, object>();
                    for (int i = 0; i < rows.Length; i++) {
                        csvData.Add(headerCsv[i], rows[i]);
                    }
                    csvList.Add(csvData);
                }
            }
            sr.Close();
            return csvList;

        } catch (UnityException e) {
            Debug.Log("loadDataFromCSV Error! : " + e.Message);
        }
        #endif

        return new List<Dictionary<string, object>>();
    }

    /// <summary>
    /// ユーザ情報に書き込む
    /// </summary>
    /// <param name="fileName"></param>
    public static void saveUserData(string fileName, Dictionary<string, object> csvDate) {
        List<Dictionary<string, object>> csvList = new List<Dictionary<string, object>>();
        csvList.Add(csvDate);
        saveUserDataList(fileName, csvList);
    }
    /// <summary>
    /// ユーザ情報一覧に書き込む
    /// </summary>
    /// <param name="fileName"></param>
    public static void saveUserDataList(string fileName, List<Dictionary<string, object>> csvList) {
    #if !UNITY_WEBPLAYER
            StreamReader sr = new StreamReader(csvUserPath + fileName + ".csv");
            string loadCsvHeader = sr.ReadLine();
            sr.Close();

            StreamWriter sw = new StreamWriter(csvUserPath + fileName + ".csv", false);

            sw.Write(loadCsvHeader + newLine);

            foreach (Dictionary<string, object> csvDate in csvList) {
                string txtCsv = "";
                foreach(string value in csvDate.Values) {
                    if (txtCsv != "") {
                        txtCsv += ",";
                    }
                txtCsv += value;
                }
                sw.Write(txtCsv + newLine);
            }

            sw.Flush();
            sw.Close();
        #endif
    }


    /// <summary>
    /// ユーザ情報を削除
    /// </summary>
    public static void DeleteDirectory() {
        #if UNITY_EDITOR
        string[] filePathList = Directory.GetFiles(csvUserPath);
            foreach(string filePath in filePathList){
                File.Delete(filePath);
            }
        #endif
    }

    /// <summary>
    /// 改行コードで分割したstringの配列を取得する
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    private static StringCollection spritText(string text){
        StringCollection dataCsv = new StringCollection();
        dataCsv.AddRange(text.Split('\n'));
        return dataCsv;
    }
}