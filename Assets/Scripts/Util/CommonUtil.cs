using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System;

/// <summary>
/// 共通関数
/// </summary>
public class CommonUtil {
    /// <summary>
    /// 昇順に並び替える
    /// </summary>
    /// <param name="deffA"></param>
    /// <param name="deffB"></param>
    /// <returns>Aの方が大きい場合1、AとBが同じ場合0、Bの方が多きい場合-1</returns>
    public static int GetAsk(int deffA, int deffB) {
        // nullチェック
        if (deffA == null) {
            if (deffB == null) {
                return 0;
            }
            return -1;
        } else {
            if (deffB == null) {
                return 1;
            }
            // aとbの比較
            return deffA.CompareTo(deffB);
        }
    }

    /// <summary>
    /// 降順に並び替える
    /// </summary>
    /// <param name="deffA">比較A</param>
    /// <param name="deffB">比較B</param>
    /// <returns>Bの方が大きい場合1、AとBが同じ場合0、Aの方が多きい場合-1</returns>
    public static int GetDesc(int deffA, int deffB) {
        // nullチェック
        if (deffB == null) {
            if (deffA == null) {
                return 0;
            }
            return -1;
        } else {
            if (deffA == null) {
                return 1;
            }
            // aとbの比較
            return deffB.CompareTo(deffA);
        }
    }

    /// <summary>
    /// 昇順に並び替える
    /// </summary>
    /// <param name="deffA"></param>
    /// <param name="deffB"></param>
    /// <returns>Aの方が大きい場合1、AとBが同じ場合0、Bの方が多きい場合-1</returns>
    public static int GetAskDateTime(DateTime deffA, DateTime deffB) {
        // nullチェック
        if (deffA == null) {
            if (deffB == null) {
                return 0;
            }
            return -1;
        } else {
            if (deffB == null) {
                return 1;
            }
            // aとbの比較
            return deffA.CompareTo(deffB);
        }
    }

    /// <summary>
    /// 降順に並び替える
    /// </summary>
    /// <param name="deffA">比較A</param>
    /// <param name="deffB">比較B</param>
    /// <returns>Bの方が大きい場合1、AとBが同じ場合0、Aの方が多きい場合-1</returns>
    public static int GetDescDateTime(DateTime deffA, DateTime deffB) {
        // nullチェック
        if (deffB == null) {
            if (deffA == null) {
                return 0;
            }
            return -1;
        } else {
            if (deffA == null) {
                return 1;
            }
            // aとbの比較
            return deffB.CompareTo(deffA);
        }
    }

    /// <summary>
    /// ゼロ埋め
    /// </summary>
    /// <param name="count"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetZero(int count, int value) {
        return String.Format("{0:D" + count + "}", value);
    }

    /// <summary>
    /// 3桁毎のカンマ区切りにする
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetFormatComma(int value) {
        return string.Format("{0:#,##0}", value);
    }

    /// <summary>
    /// 数値か判定する
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsNumber(string value) {
        if (value == "") {
            return false;
        }
        int num;
        return int.TryParse(value, out num);
    }

}