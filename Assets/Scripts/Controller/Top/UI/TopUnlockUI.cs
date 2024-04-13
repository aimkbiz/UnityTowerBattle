using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TopUnlockUI : MonoBehaviour {
    [SerializeField]
    protected GameObject _areaUnlock = default;
    [SerializeField]
    protected Text _txtUnlock = default;
    [SerializeField]
    protected Button _btnUnlock = default;

    /// <summary>
    /// 初期設定
    /// </summary>
    void Awake () {
        int people = 0;
        if (CommonUIManager.Instance.AreaUnlock < 20) {
            people = ParamList.AreaUnlock[CommonUIManager.Instance.AreaUnlock+1];
            if (ParamList.AreaUnlock[CommonUIManager.Instance.AreaUnlock + 1] > CommonUIManager.Instance.GetPeople()) {
                _txtUnlock.text = "人口が"+ people + "を超えるとエリアを1マス増やせます";
            } else {
                _txtUnlock.text = "人口が" + people + "を超えました\nエリアを1マス増やしますか？";
            }
            _btnUnlock.onClick.AddListener(() => { OnBtnUnlock(); });

        }

    }

    private void OnBtnUnlock() {
        CommonUIManager.Instance.SetAreaUnlock(CommonUIManager.Instance.AreaUnlock + 1);
        _areaUnlock.SetActive(false);
        SceneManager.LoadScene("top");
    }
}
