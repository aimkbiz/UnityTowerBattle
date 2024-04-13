using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

/// <summary>
/// ユーザデータ
/// </summary>
public class DataBillLevelAccess {
    private string _tableNm = "data_bill_level";
    public class DataBillLevel {
        /// <summary> ビルID </summary>
        public int BillId { get; private set; }
        /// <summary> レベル </summary>
        public int Level { get; private set; }
        /// <summary> 値段 </summary>
        public int Price { get; private set; }
        /// <summary> 値 </summary>
        public int People { get; private set; }
        /// <summary> 最大値 </summary>
        public int Salary { get; private set; }

        public DataBillLevel(Dictionary<string, object> result) {
            BillId = int.Parse(result["bill_id"].ToString());
            Level = int.Parse(result["level"].ToString());
            People = int.Parse(result["people"].ToString());
            Salary = int.Parse(result["salary"].ToString());
            Price = int.Parse(result["price"].ToString());
        }
    }

    /// <summary>
    /// 一覧情報を取得
    /// </summary>
    public Dictionary<string, DataBillLevel> GetDataList() {
        List<Dictionary<string, object>> csvList = CsvAccess.getMasterDataList(_tableNm);
        Dictionary<string, DataBillLevel> dtList = new Dictionary<string, DataBillLevel>();
        foreach (Dictionary<string, object> csvLine in csvList) {
            DataBillLevel dt = new DataBillLevel(csvLine);
            dtList.Add(dt.BillId + ConstCode.VALUE_SPLIT + dt.Level, dt);
        }
        return dtList;
    }

    /// <summary>
    /// キーから情報を取得
    /// </summary>
    /// <param name="todoId"></param>
    /// <returns></returns>
    public DataBillLevel GetDataByKey(int billId,int level) {
        return GetDataList()[billId + ConstCode.VALUE_SPLIT + level];
    }
}
