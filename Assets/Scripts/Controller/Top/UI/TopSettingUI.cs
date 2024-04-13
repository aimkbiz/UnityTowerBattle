using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TopSettingUI : MonoBehaviour {
    [SerializeField]
    protected Transform _panalSetting = default;
    [SerializeField]
    protected Button _btnClose = default;
    [SerializeField]
    protected Button _btnClear = default;
    [SerializeField]
    protected Button _btnReload = default;

    /// <summary>
    /// 初期設定
    /// </summary>
    void Awake () {
        _btnClose.onClick.AddListener(() => { OnBtnSettingClose(); });
        _btnClear.onClick.AddListener(() => { OnBtnSettingClear(); });
        _btnReload.onClick.AddListener(() => { OnBtnSettingReload(); });
        if (Setting.DEBUG_DIV.Equals(ConstList.DebugDiv.RELEASE)) {
            _btnReload.gameObject.SetActive(false);
        }
    }

    private void OnBtnSettingClose() {
        _panalSetting.gameObject.SetActive(false);
    }
    /// <summary>
    /// 全クリア
    /// </summary>
    private void OnBtnSettingClear() {
        DBAccess.DeleteDirectory();
        PlayerPrefs.SetInt(ConstList.PlayerPrefsName.GOLD_VALUE, 100000);
        PlayerPrefs.SetInt(ConstList.PlayerPrefsName.AREA_UNLOCK, 10);
        SceneManager.LoadScene("Top");
    }
    /// <summary>
    /// 全クリア
    /// </summary>
    private void OnBtnSettingReload() {
        SceneManager.LoadScene("Top");
    }
}
