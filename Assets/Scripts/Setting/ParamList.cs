using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 多次元配列の設定値.
/// </summary>
public class ParamList {
    /// <summary> 撤去費用 </summary>
    public static Dictionary<int, int> BillDeletePriceList = new Dictionary<int, int>() {
    {1,1000} ,{2,2000},{3,4000},{4,7000},{5,12000},{6,15000},{7,20000},{8,25000},{9,30000},{10,50000}};

    /// <summary> アンロック </summary>
    public static Dictionary<int, int> AreaUnlock = new Dictionary<int, int>() {
    {11,1000} ,{12,2000},{13,4000},{14,7000},{15,12000},{16,15000},{17,20000},{18,25000},{19,30000},{20,50000}};
}