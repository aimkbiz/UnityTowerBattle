using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class TopBillSetUI : MonoBehaviour {
    [SerializeField]
    protected Transform _panalBill = default;
    [SerializeField]
    protected Transform _panalBillSet = default;
    [SerializeField]
    protected Button _btnUpdate = default;
    [SerializeField]
    protected Button _btnMove = default;
    [SerializeField]
    protected Button _btnDelete = default;
    [SerializeField]
    protected Button _btnClose = default;

    public delegate void OnBtnUpdateClickListener();
    private OnBtnUpdateClickListener _ltnBtnUpdateClick;
    public delegate void OnBtnDeleteClickListener();
    private OnBtnDeleteClickListener _ltnBtnDeleteClick;

    /// <summary>
    /// 初期設定
    /// </summary>
    void Awake () {
        _btnDelete.onClick.AddListener(() => { if (this._ltnBtnDeleteClick != null) { this._ltnBtnDeleteClick(); } });
        _btnUpdate.onClick.AddListener(() => { if (this._ltnBtnUpdateClick != null) { this._ltnBtnUpdateClick(); } });
        _btnMove.onClick.AddListener(() => { _panalBillSet.gameObject.SetActive(false); });
        _btnClose.onClick.AddListener(() => { _panalBillSet.gameObject.SetActive(false); });
    }

    /// <summary>
    /// 更新
    /// </summary>
    public void OnSetUpdateClickListener(OnBtnUpdateClickListener listener) {
        this._ltnBtnUpdateClick = listener;
    }

    /// <summary>
    /// 撤去
    /// </summary>
    public void OnSetDeteleClickListener(OnBtnDeleteClickListener listener) {
        this._ltnBtnDeleteClick = listener;
    }
}
