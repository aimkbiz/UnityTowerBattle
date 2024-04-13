using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// バウンドさせる
/// </summary>
public class UISlide : MonoBehaviour {
    public enum SlideType {
        None,// 効果なし
        Top, // 上から
        Bottom, // 下から
        Left, // 左から
        Right, // 右から
    }
    /// <summary>
    /// スライドイン/アウト
    /// </summary>
    [SerializeField]
    private SlideInOut SlideInOutType = SlideInOut.In;

    public enum SlideInOut {
        In, // インのみ
        InOut, // インアウト(往復)
    }

    /// <summary>
    /// タイプを選択
    /// </summary>
    [SerializeField]
    public SlideType Type = SlideType.Top;
    /// <summary>
    /// バウンドのサイズ
    /// </summary>
    [SerializeField]
    public float MoveSize = 1f;
    /// <summary>
    /// 開始位置(枠外から来た場合)
    /// </summary>
    [SerializeField]
    public float StartSize = 0;
    /// <summary>
    /// スピード
    /// </summary>
    [SerializeField]
    public float Speed = 0.1f;

    private float _minPos = 0;      // 最小の位置
    private float _maxPos = 0;      // 最大の位置
    private bool _isBlink = true;   // 切り替え
    private bool _isStart = true;   // 開始切替

    void OnEnable() {
        if (Type != SlideType.None) {
            if (Type == SlideType.Top || Type == SlideType.Bottom) {
                transform.position = (Type == SlideType.Top) ? new Vector3(transform.position.x, transform.position.y - StartSize, transform.position.z) : new Vector3(transform.position.x, transform.position.y + StartSize, transform.position.z);
                _minPos = transform.position.y;
                _maxPos = (Type == SlideType.Top) ? transform.position.y + MoveSize : transform.position.y - MoveSize;
            } else if (Type == SlideType.Left || Type == SlideType.Right) {
                transform.position = (Type == SlideType.Right) ? new Vector3(transform.position.x - StartSize, transform.position.y, transform.position.z) : new Vector3(transform.position.x + StartSize, transform.position.y, transform.position.z);
                _minPos = transform.position.x;
                _maxPos = (Type == SlideType.Right) ? transform.position.x + MoveSize : transform.position.x - MoveSize;
            }
        }
    }

    void FixedUpdate() {
        if (_isStart && Type != SlideType.None) {
            // 上限に行った場合
            if ((Type == SlideType.Top && transform.position.y >= _maxPos) || (Type == SlideType.Bottom && transform.position.y <= _maxPos)
                || (Type == SlideType.Right && transform.position.x >= _maxPos) || (Type == SlideType.Left && transform.position.x <= _maxPos)) {
                _isBlink = false;
                if (SlideInOutType == SlideInOut.In) {
                    _isStart = false;
                }
                
            }
            if (!_isBlink) {
                if ((Type == SlideType.Top && transform.position.y <= _minPos) || (Type == SlideType.Bottom && transform.position.y >= _minPos)
                || (Type == SlideType.Right && transform.position.x <= _minPos) || (Type == SlideType.Left && transform.position.x >= _minPos)) { 
                    _isStart = false;
                }
            }

            if (_isBlink) {
                if (Type == SlideType.Top || Type == SlideType.Bottom) { 
                    transform.position = (Type == SlideType.Top) ? new Vector3(transform.position.x, transform.position.y + (Speed * Time.deltaTime), transform.position.z) : new Vector3(transform.position.x, transform.position.y - (Speed * Time.deltaTime), transform.position.z);
                }
                if (Type == SlideType.Left || Type == SlideType.Right) {
                    transform.position = (Type == SlideType.Right) ? new Vector3(transform.position.x + (Speed * Time.deltaTime), transform.position.y, transform.position.z) : new Vector3(transform.position.x - (Speed * Time.deltaTime), transform.position.y, transform.position.z);
                }
            } else {
                if (Type == SlideType.Top || Type == SlideType.Bottom) {
                    transform.position = (Type == SlideType.Bottom) ? new Vector3(transform.position.x, transform.position.y + (Speed * Time.deltaTime), transform.position.z) : new Vector3(transform.position.x, transform.position.y - (Speed * Time.deltaTime), transform.position.z);
                }
                if (Type == SlideType.Left || Type == SlideType.Right) {
                    transform.position = (Type == SlideType.Left) ? new Vector3(transform.position.x + (Speed * Time.deltaTime), transform.position.y, transform.position.z) : new Vector3(transform.position.x - (Speed * Time.deltaTime), transform.position.y, transform.position.z);
                }
            }
        }
    }
}