using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class CommonUIManager : SingletonMonoBehaviour<CommonUIManager> {
    public int Gold = 0;
    public int AreaUnlock = 10;

    [SerializeField]
    protected Text _txtPopulation = default;
    [SerializeField]
    protected Text _txtGold = default;

    public void Init() {
        Refresh();
        _txtGold.text = Gold.ToString();
        _txtPopulation.text = GetPeople().ToString();
    }

    public void Refresh() {
        if (PlayerPrefs.HasKey(ConstList.PlayerPrefsName.GOLD_VALUE)) {
            Gold = PlayerPrefs.GetInt(ConstList.PlayerPrefsName.GOLD_VALUE);
        }

        if (PlayerPrefs.HasKey(ConstList.PlayerPrefsName.AREA_UNLOCK)) {
            AreaUnlock = PlayerPrefs.GetInt(ConstList.PlayerPrefsName.AREA_UNLOCK);
        }
    }

    public void SetGold(int gold) {
        Gold = gold;
        PlayerPrefs.SetInt(ConstList.PlayerPrefsName.GOLD_VALUE, gold);
        _txtGold.text = Gold.ToString();
    }

    public void SetAreaUnlock(int unlock) {
        AreaUnlock = unlock;
        PlayerPrefs.SetInt(ConstList.PlayerPrefsName.AREA_UNLOCK, unlock);
    }

    public void SetPeople() {
        _txtPopulation.text = GetPeople().ToString();
    }
    public int GetPeople() {
        int people = 0;
        Dictionary<string, UserPositionAccess.UserPosition> userPosionList = new UserPositionAccess().GetDataList();
        foreach (UserPositionAccess.UserPosition dt in userPosionList.Values) {
            DataBillLevelAccess.DataBillLevel dataBillLevel = new DataBillLevelAccess().GetDataByKey(dt.BillId, dt.Level);
            people += dataBillLevel.People;
        }
        return people;
    }
}
