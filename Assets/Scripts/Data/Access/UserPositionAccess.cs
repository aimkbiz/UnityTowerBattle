using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

/// <summary>
/// ユーザデータ
/// </summary>
public class UserPositionAccess {
    private string _tableNm = "user_position";
    public class UserPosition {
        /// <summary> X座標 </summary>
        public int PosX { get; private set; }
        /// <summary> Y座標 </summary>
        public int PosY { get; private set; }
        /// <summary> 建物ID </summary>
        public int BillId { get; private set; }
        /// <summary> レベル </summary>
        public int Level;
        /// <summary> 回収時間 </summary>
        public string CollectionTime;

        public UserPosition(Dictionary<string, object> result) {

            PosX = int.Parse(result["pos_x"].ToString());
            PosY = int.Parse(result["pos_y"].ToString());
            BillId = int.Parse(result["bill_id"].ToString());
            Level = int.Parse(result["level"].ToString());
            CollectionTime = result["collection_time"].ToString();
        }
    }

    /// <summary>
    /// 一覧情報を取得
    /// </summary>
    public Dictionary<string, UserPosition> GetDataList() {
        List<Dictionary<string, object>> csvList = CsvAccess.getUserDataList(_tableNm);
        Dictionary<string, UserPosition> dtList = new Dictionary<string, UserPosition>();

        foreach (Dictionary<string, object> csvLine in csvList) {
            UserPosition dt = new UserPosition(csvLine);
            dtList.Add(dt.PosX + ConstCode.VALUE_SPLIT + dt.PosY, dt);
        }

        return dtList;
    }

    /// <summary>
    /// キーから情報を取得
    /// </summary>
    /// <param name="todoId"></param>
    /// <returns></returns>
    public UserPosition GetDataByKey(string keys) {
        return GetDataList()[keys];
    }

    /// <summary>
    /// 情報を保存
    /// </summary>
    /// <param name="todoId"></param>
    /// <returns></returns>
    public void SetDataList(UserPosition insertDt) {
        Dictionary<string, UserPosition> dtList = GetDataList();
        StringCollection insertSql = new StringCollection();

        foreach (KeyValuePair<string, UserPosition> dt in dtList) {
            if (dt.Value.PosX == insertDt.PosX && dt.Value.PosY == insertDt.PosY) {
                insertSql.Add(insertDt.PosX + "," + insertDt.PosY + "," + insertDt.BillId + "," + insertDt.Level + "," + insertDt.CollectionTime);
            } else {
                insertSql.Add(dt.Value.PosX + "," + dt.Value.PosY + "," + dt.Value.BillId + "," + dt.Value.Level + "," + dt.Value.CollectionTime);
            }
        }

        if (!dtList.ContainsKey(insertDt.PosX + ConstCode.VALUE_SPLIT + insertDt.PosY)) {
            insertSql.Add(insertDt.PosX + "," + insertDt.PosY + "," + insertDt.BillId + "," + insertDt.Level + "," + insertDt.CollectionTime);
        }

        DBAccess.saveUserDataList(_tableNm, insertSql);
    }

    /// <summary>
    /// 情報を削除
    /// </summary>
    /// <param name="todoId"></param>
    /// <returns></returns>
    public void DeleteDataList(string index) {
        Dictionary<string, UserPosition> dtList = GetDataList();
        StringCollection insertSql = new StringCollection();

        StringCollection keyList = new StringCollection();
        foreach (KeyValuePair<string, UserPosition> dt in dtList) {
            if (dt.Value.PosX + ConstCode.VALUE_SPLIT + dt.Value.PosY != index) {
                insertSql.Add(dt.Value.PosX + "," + dt.Value.PosY + "," + dt.Value.BillId + "," + dt.Value.Level + "," + dt.Value.CollectionTime);
            }
        }

        DBAccess.saveUserDataList(_tableNm, insertSql);
    }
}
