using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System;

/// <summary>
/// 日付関数
/// </summary>
public class DateUtil {
    /// <summary>
    /// 型変換(yyyyMMDD→yyyy/MM/DDS)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string GetDateFormat(string value) {
        return value.Substring(0, 4) + "/"
            + value.Substring(4, 2) + "/"
            + value.Substring(6, 2);
    }

    /// <summary>
    /// yyyyMMDDの文字型から日付型に変換
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static DateTime GetDateFormatDateTime(string value) {
        return DateTime.Parse(GetDateFormat(value));
    }

    /// <summary>
    /// yyyyMMの文字型から日付型に変換(月頭に変換)
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static DateTime GetDateFormatDateTimeByYYYYMM(string value) {
        return DateTime.Parse(GetDateFormat(value+"01"));
    }

    /// <summary>
    /// yyyy/MM/dd HH:mm:ss を日付型に変換
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static DateTime GetDateFormatDateTimeYYYYMMDDHHMMSS(string value) {
        return DateTime.ParseExact(value, "yyyy/MM/dd HH:mm:ss", null);
    }

    /// <summary>
    /// タイムスタンプをmm:ssのゼロ埋めに変換する
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static string GetDateFormatMMSS(TimeSpan dt) {
        return CommonUtil.GetZero(2, dt.Minutes) + ":" + CommonUtil.GetZero(2, dt.Seconds);
    }

    /// <summary>
    /// タイムスタンプをmm分ss秒のゼロ埋めに変換する
    /// </summary>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static string GetDateFormatMMSSByString(TimeSpan dt) {
        return CommonUtil.GetZero(2, dt.Minutes) + "分" + CommonUtil.GetZero(2, dt.Seconds) + "秒";
    }

    /// <summary>
    /// 該当年月の日数を返す
    /// </summary>
    /// <param name="dt">DateTime</param>
    /// <returns>DateTime</returns>
    public static int GetDaysInMonth(DateTime dt) {
        return DateTime.DaysInMonth(dt.Year, dt.Month);
    }

    /// <summary>
    /// 月初日を返す
    /// </summary>
    /// <param name="dt">DateTime</param>
    /// <returns>Datetime</returns>
    public static DateTime GetBeginOfMonth(DateTime dt) {
        return dt.AddDays((dt.Day - 1) * -1);
    }

    /// <summary>
    /// 月末日を返す
    /// </summary>
    /// <param name="dt">DateTime</param>
    /// <returns>DateTime</returns>
    public static DateTime GetEndOfMonth(DateTime dt) {
        return new DateTime(dt.Year, dt.Month, GetDaysInMonth(dt));
    }



    /// <summary>
    /// 経過日数取得(総日数 99週99日(99カ月99週)
    /// </summary>
    /// <param name="date"></param>
    /// <param name="monthDiff"></param>
    /// <returns></returns>
    public static string GetDatePassage(int date, int monthDiff) {
        int month = (int)Math.Ceiling((double)(date + 1) / 28) - monthDiff;
        int monthWeek = (date - ((month - 1) * 28)) / 7;
        if (monthDiff > 0) {
            monthWeek = (date - (month * 28)) / 7;
        }
        int week = (int)Math.Floor((double)date / 7);
        int day = date - (week * 7);
        return date.ToString() + "日 " + week + "週" + day + "日(" + month + "ヶ月" + monthWeek + "週)";
    }

    /// <summary>
    /// 実働経過日数取得(総日数 99週99日(99カ月99週)
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public static string GetDatePassageWorking(int date) {
        int month = (int)Math.Ceiling((double)date / 20) - 1;
        int monthWeek = (date - (month * 28)) / 5;
        int week = (int)Math.Floor((double)date / 5);
        int day = date - (week * 5);
        return date.ToString() + "日 " + week + "週" + day + "日(" + month + "ヶ月" + monthWeek + "週)";
    }

    /// <summary>
    /// 日付のフォーマットチェック
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsDateFormat(string value) {
        if (value.Length != 8) {
            return false;
        }
        DateTime dt;
        return DateTime.TryParse(GetDateFormat(value), out dt);
    }

    /// <summary>
    /// 年月のフォーマットチェック
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static bool IsYearMontFormat(string value) {
        if (value.Length != 6) {
            return false;
        }
        DateTime dt;
        return DateTime.TryParse(GetDateFormat(value + "01"), out dt);
    }

    /// <summary>
    /// 未来かチェック(diff1が未来の場合true)
    /// </summary>
    /// <param name="diff1">比較1</param>
    /// <param name="diff2">比較2</param>
    /// <returns></returns>
    public static bool IsFutureYearMonth(DateTime diff1, DateTime diff2) {
        return diff1 > diff2;
    }

    /// <summary>
    /// 年月が一致しているか(一致している場合true)
    /// </summary>
    /// <param name="diff1">比較1</param>
    /// <param name="diff2">比較2</param>
    /// <returns></returns>
    public static bool IsDateYearMonth(int diff1, int diff2) {
        return int.Parse(diff1.ToString().Substring(0, 6)) == diff2;
    }
}