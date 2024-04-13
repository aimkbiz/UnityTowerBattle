using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ズームする
/// </summary>
public class UIZoom : MonoBehaviour {
    public enum ZoomType {
        None,// 効果なし
        ZoomIn, // ズームイン(小さく⇒大きく)
        ZoomOut, // ズームアウト(大きく⇒小さく)
    }

    /// <summary>
    /// ズームタイプを選択
    /// </summary>
    [SerializeField]
    public ZoomType Type = ZoomType.ZoomOut;
    /// <summary>
    /// ズームさせる前のスケールサイズ
    /// </summary>
    [SerializeField]
    public float ScalePostion = 0.5f;
    /// <summary>
    /// ズームさせた後のスケールサイズ
    /// </summary>
    [SerializeField]
    public float ScalePostionEnd = 1f;
    /// <summary>
    /// </summary>
    [SerializeField]
    public float Speed = 1f;
    /// <summary>
    /// 起動時既に動いている状態にするか
    /// </summary>
    [SerializeField]
    public bool IsStart = true;
    [Header("[buttonのclick時に開始]")]
    public Button BtnClick = null;
    /// <summary>
    /// 大きくし終わったら元のサイズに戻す
    /// </summary>
    [SerializeField]
    public bool IsEndInit = false;

    //private Transform _traPopup;    // ズームさせるオブジェクト
    private float _deltaTotol = 0;  // 切り替え時間のトータル
    private bool _isStart = false;   // ズームを開始するか判定する(true 開始)

    void Awake() {
        if (BtnClick != null) {
            IsStart = false;
            BtnClick.GetComponent<Button>().onClick.AddListener(() => { _isStart = true;   });
        }
    }
    void OnEnable() {
        SetInit();
        _isStart = IsStart;
    }
    private void SetInit() {
        if (Type != ZoomType.None) {
            if (Type == ZoomType.ZoomOut) {
                _deltaTotol = ScalePostion;
            } else {
                _deltaTotol = ScalePostionEnd;
            }
            transform.localScale = new Vector3(_deltaTotol, _deltaTotol, _deltaTotol);
        }
    }

    void FixedUpdate() {
        if (_isStart) {
            // サイズになったら止める
            if ((Type == ZoomType.ZoomOut && transform.localScale.x >= ScalePostionEnd) || (Type == ZoomType.ZoomIn && transform.localScale.x <= ScalePostion)) {
                _isStart = false;
                if (IsEndInit) {
                    SetInit();
                }
                return;
            }
            if (Type == ZoomType.ZoomIn) {
                _deltaTotol -= Time.deltaTime * Speed;
            } else {
                _deltaTotol += Time.deltaTime * Speed;
            }
            transform.localScale = new Vector3(_deltaTotol, _deltaTotol, _deltaTotol);
        }
    }
    public void SetStart() {
        _isStart = true;
    }
}