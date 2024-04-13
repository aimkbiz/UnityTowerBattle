using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 共通GridUI.
/// </summary>
public class CommonUI : MonoBehaviour {
    public delegate void OnClickListener();
    public delegate void OnLongLongDragListener();
    public delegate void OnDragUpListener();
    public delegate void OnDragDownListener();
    public delegate void OnDragRightListener();
    public delegate void OnDragLeftListener();

    private OnClickListener _listener;
    private OnLongLongDragListener _onLongDragListener;
    private OnDragUpListener _onDragUp;
    private OnDragDownListener _onDragDown;
    private OnDragRightListener _onDragRight;
    private OnDragLeftListener _onDragLeft;

    private Vector3 _startPos;
    private DateTime _dragTime;
    private bool _isDown = false;
    private bool _isDownEnd = false;


    public void Awake() {
        Debug.LogError("Awake");
        transform.GetComponent<Button>().onClick.AddListener(() => {
            if (this._listener != null) {
                this._listener();
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
                _onLongDragListener();
            }
        }
    }

    /// <summary>
    /// クリック後
    /// </summary>
    public void OnClick() {
        if (this._listener != null) {
            Debug.LogError("click");
            this._listener();
        }
    }

    /// <summary>
    /// 押し続けてる
    /// </summary>
    /// <param name="isDown"></param>
    void OnPointerDown(PointerEventData eventData) {
        Debug.LogError("Awake2");
        /*
        _isDown = isDown;
        if (isDown) {
            Debug.LogError("Awake2");
            _dragTime = DateTime.Now;
            Touch touch = Input.GetTouch(0);
            _startPos =touch.position;
        }*/
    }

    /// <summary>
    /// クリックリスナの登録
    /// </summary>
    /// <param name="listener"></param>
    /// <param name="index"></param>
    public void OnSetClickListener(OnClickListener listener) {
        this._listener = listener;
    }

    /// <summary>
    /// 長押し
    /// </summary>
    /// <param name="listener"></param>
    /// <param name="index"></param>
    public void OnLongDragListener(OnLongLongDragListener listener) {
        _onLongDragListener = listener;
    }

    /// <summary>
    /// 上方向
    /// </summary>
    /// <param name="listener"></param>
    /// <param name="index"></param>
    public void OnDragUp(OnDragUpListener listener) {
        _onDragUp = listener;
    }

    /// <summary>
    /// 下方向
    /// </summary>
    /// <param name="listener"></param>
    /// <param name="index"></param>
    public void OnDragDown(OnDragDownListener listener) {
        _onDragDown = listener;
    }

    /// <summary>
    /// 左方向
    /// </summary>
    /// <param name="listener"></param>
    /// <param name="index"></param>
    public void OnDragRight(OnDragRightListener listener) {
        _onDragRight = listener;
    }

    /// <summary>
    /// 右方向
    /// </summary>
    /// <param name="listener"></param>
    /// <param name="index"></param>
    public void OnDragLeft(OnDragLeftListener listener) {
        _onDragLeft = listener;
    }
}
