using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// バウンドさせる
/// </summary>
public class UIBound : MonoBehaviour {
    public enum BoundType {
        None,// 効果なし
        Vertical, // 垂直方向(上下)
        Horizontal, // 水平方向(左右)
    }

    public enum BoundStart {
        None,// 通常
        Reverse, // 逆方向
    }

    /// <summary>
    /// タイプを選択
    /// </summary>
    [SerializeField]
    public BoundType Type = BoundType.Vertical;
    /// <summary>
    /// 開始
    /// </summary>
    [SerializeField]
    public BoundStart Start = BoundStart.None;
    /// <summary>
    /// バウンドのサイズ
    /// </summary>
    [SerializeField]
    public float BoundSize = 1f;
    /// <summary>
    /// スピード
    /// </summary>
    [SerializeField]
    public float Speed = 0.1f;

    private float _minPos = 0;      // 最小の位置
    private float _maxPos = 0;      // 最大の位置
    private bool _isBlink = true;   // 切り替え

    private void Awake() {
        if (Type != BoundType.None) {
            if (Start == BoundStart.Reverse) {
                _minPos = (Type == BoundType.Vertical) ? transform.position.y - BoundSize : transform.position.x - BoundSize;
                _maxPos = (Type == BoundType.Vertical) ? transform.position.y : transform.position.x;
            } else {
                _minPos = (Type == BoundType.Vertical) ? transform.position.y : transform.position.x;
                _maxPos = (Type == BoundType.Vertical) ? transform.position.y + BoundSize : transform.position.x + BoundSize;
            }
        }
    }

    void FixedUpdate() {
        if (Type != BoundType.None) {
            // 上限に行った場合
            if ((Type == BoundType.Vertical && transform.position.y >= _maxPos) || (Type == BoundType.Horizontal && transform.position.x >= _maxPos)) {
                _isBlink = false;
            } else if ((Type == BoundType.Vertical && transform.position.y <= _minPos) || (Type == BoundType.Horizontal && transform.position.x <= _minPos)) {
                _isBlink = true;
            }

            if (_isBlink) {
                transform.position = (Type == BoundType.Vertical) ? new Vector3(transform.position.x, transform.position.y + (Speed * Time.deltaTime), transform.position.z) : new Vector3(transform.position.x + (Speed * Time.deltaTime), transform.position.y, transform.position.z);
            } else {
                transform.position = (Type == BoundType.Vertical) ? new Vector3(transform.position.x, transform.position.y - (Speed * Time.deltaTime), transform.position.z) : new Vector3(transform.position.x - (Speed * Time.deltaTime), transform.position.y, transform.position.z);
            }
        }
    }
}