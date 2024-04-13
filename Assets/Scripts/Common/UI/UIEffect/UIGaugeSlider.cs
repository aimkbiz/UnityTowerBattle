using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲージ
/// </summary>
public class UIGaugeSlider : MonoBehaviour {
    /// <summary>
    /// スピード
    /// </summary>
    [SerializeField]
    public Slider sldGauge = default;
    /// <summary>
    /// スピード
    /// </summary>
    [SerializeField]
    public float Speed = 0.1f;
    /// <summary>
    /// 起動時既に動いている状態にするか
    /// </summary>
    [SerializeField]
    public bool IsStart = true;

    private bool _isStart = false;   // 開始するか判定する(true 開始)
    private float _start = 0;  // 開始位置
    private float _end = 0;  // 終了位置

    void Awake() {
    }
    void OnEnable() {
        _isStart = IsStart;
    }
    public void SetInit(float start) {
        _start = start;
        sldGauge.value = _start / 100;
    }

    public void SetStart(float start, float end) {
        _isStart = true;
        _start = start;
        _end = end;
    }

    void FixedUpdate() {
        if (_isStart) {
            // サイズになったら止める
            if (_end <= sldGauge.value*100) {
                _isStart = false;
            }
            //sldGauge.value = sldGauge.value + ((float)_start / 100);
            sldGauge.value = sldGauge.value + Time.deltaTime * Speed;
        }
    }
}