using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 共通GridUI.
/// </summary>
public class CommonGridUI : MonoBehaviour {
    public delegate void OnClickGridListener(int index);
    public delegate void OnLongDragListener(int index);
    public delegate void OnClickGridListenerString(string index);

    private OnClickGridListener _listener;
    private OnClickGridListenerString _listenerString;
    private OnLongDragListener _onLongDragListener;

    private DateTime _dragTime;
    private bool _isDown = false;
    private int _index;
    private string _strIndex;

    public void Awake() {
        transform.GetComponent<Button>().onClick.AddListener(() => {
            if (this._listener != null) {
                this._listener(this._index);
            }

            if (this._listenerString != null) {
                this._listenerString(this._strIndex);
            }
        });
    }

    /// <summary>
    /// メインループ
    /// </summary>
    void Update() {
        if (_isDown) {
            TimeSpan diffTime = DateTime.Now - _dragTime;
            if (diffTime.Seconds >= 1 || diffTime.Milliseconds >= 500) {
                if (_index != null && _onLongDragListener != null) {
                    _onLongDragListener(this._index);
                }
            }
        }
    }

    /// <summary>
    /// クリック後
    /// </summary>
    public void OnClick() {
        if (this._listener != null) {
            this._listener(this._index);
        }
    }

    /// <summary>
    /// 押し続けてる
    /// </summary>
    /// <param name="isDown"></param>
    void OnPress(bool isDown) {
        _isDown = isDown;
        if (isDown) {
            _dragTime = DateTime.Now;
        }
    }

    /// <summary>
    /// クリックリスナの登録
    /// </summary>
    /// <param name="listener"></param>
    /// <param name="index"></param>
    public void SetOnClickGridListener(OnClickGridListener listener, int index) {
        this._listener = listener;
        this._index = index;
    }

    /// <summary>
    /// クリックリスナの登録
    /// </summary>
    /// <param name="listener"></param>
    /// <param name="index"></param>
    public void SetOnClickGridListenerString(OnClickGridListenerString listener, string index) {
        this._listenerString = listener;
        this._strIndex = index;
    }

    /// <summary>
    /// 長押し
    /// </summary>
    /// <param name="listener"></param>
    /// <param name="index"></param>
    public void SetOnLongDragListener(OnLongDragListener listener, int index) {
        _onLongDragListener = listener;
        this._index = index;
    }
}
