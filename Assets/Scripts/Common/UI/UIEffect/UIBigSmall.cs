using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 大きくしたり小さくしたりする
/// </summary>
public class UIBigSmall : MonoBehaviour {
    /// <summary>
    /// スピード
    /// </summary>
    [SerializeField]
    protected float Speed = 0.5f;
    /// <summary>
    /// 開始スケール
    /// </summary>
    [SerializeField]
    protected float StartScale = 0.5f;
    /// <summary>
    /// 終了スケール
    /// </summary>
    [SerializeField]
    protected float EndScale = 1f;
    /// <summary>
    /// ループするか
    /// </summary>
    [SerializeField]
    protected bool IsLoop = true;
    /// <summary>
    /// 起動時既に動いている状態にするか
    /// </summary>
    [SerializeField]
    protected bool IsStart = true;

    private float _totalScale = 0;    // 現在のスケール
    private bool _isChange = true;   // 切り替え
    private bool _isChangeOld = true;   // 切り替え
    private bool _isStart = true;
    private int _changeCnt = 0;

    void Awake() {
        transform.localScale = new Vector3(StartScale, StartScale, StartScale);
        _totalScale = StartScale;
        _isStart = IsStart;
    }

    void FixedUpdate() {
        if (_isStart && (IsLoop || !IsLoop && _changeCnt < 2)) {
            // 切り替え
            if (_totalScale > EndScale) {
                if (_isChangeOld) {
                    _changeCnt++;
                }
                _isChangeOld = _isChange = false;
                
            } else if (_totalScale < StartScale) {
                if (!_isChangeOld) {
                    _changeCnt++;
                }
                _isChangeOld = _isChange = true;
            }
            if (_isChange) {
                _totalScale += Time.deltaTime * Speed;
            } else {
                _totalScale -= Time.deltaTime * Speed;
            }
            transform.localScale = new Vector3(_totalScale, _totalScale, _totalScale);
        }
    }

    public void SetStart() {
        _isStart = true;
        _changeCnt = 0;
    }
}