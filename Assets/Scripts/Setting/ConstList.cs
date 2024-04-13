using UnityEngine;
using System.Collections;

/// <summary>
/// 共通定数.
/// </summary>
public class ConstList {
    public class DebugDiv {
        /// <summary> リリース </summary>
        public const int RELEASE = 1;
        /// <summary> デバック </summary>
        public const int DEBUG = 2;

    }

    /// <summary>
    /// PlayerPrefs保存名
    /// </summary>
    public class PlayerPrefsName {
        /// <summary> ゴールド </summary>
        public const string GOLD_VALUE = "GOLD_VALUE";
        /// <summary> 人口 </summary>
        public const string POPULATION = "POPULATION";
        /// <summary> エリア範囲 </summary>
        public const string AREA_UNLOCK = "AREA_UNLOCK";
    }


    public class BillId {
        public const int BILL_WAY = 1001;
    }

    /// <summary>
    /// 建物区分
    /// </summary>
    public class BillDiv {
        /// <summary> 道 </summary>
        public const int BILL_DIV_LOAD = 1;
        /// <summary> 住宅 </summary>
        public const int BILL_DIV_HOUSING = 2;
        /// <summary> 商業 </summary>
        public const int BILL_DIV_BUSINESS = 3;
        /// <summary> 商業 </summary>
        public const int BILL_DIV_FACTORY = 4;
    }

}

