using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

/// <summary>
/// ユーザデータ
/// </summary>
public class DataBillAccess {
    private string _tableNm = "data_bill";
    public class DataBill {
        /// <summary> ビルID </summary>
        public int BillId { get; private set; }
        /// <summary> 範囲 </summary>
        public int Div { get; private set; }
        /// <summary> 範囲 </summary>
        public int Area { get; private set; }
        /// <summary> 名前 </summary>
        public string Name { get; private set; }
        public DataBill(Dictionary<string, object> result) {
            BillId = int.Parse(result["bill_id"].ToString());
            Div = int.Parse(result["div"].ToString());
            Area = int.Parse(result["area"].ToString());
            Name = result["name"].ToString();
        }
    }

    /// <summary>
    /// 一覧情報を取得
    /// </summary>
    public Dictionary<int, DataBill> GetDataList() {
        List<Dictionary<string,object>> csvList = CsvAccess.getMasterDataList(_tableNm);
        Dictionary<int, DataBill> dtList = new Dictionary<int, DataBill>();

        foreach (Dictionary<string, object> csvLine in csvList) {
            DataBill dt = new DataBill(csvLine);
            dtList.Add(dt.BillId, dt);
        }

        return dtList;
    }

    /// <summary>
    /// キーから情報を取得
    /// </summary>
    /// <param name="todoId"></param>
    /// <returns></returns>
    public DataBill GetDataByKey(int keys) {
        return GetDataList()[keys];
    }
}
