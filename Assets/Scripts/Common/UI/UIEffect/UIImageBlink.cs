using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 画像を点滅させる
/// </summary>
public class UIImageBlink : MonoBehaviour {
    /// <summary>
    /// 点滅間隔
    /// </summary>
    [SerializeField]
    public float BlinkBetween = 2.0f;
    private Image _imgBlink;
    private float _time = 0.0f;
    private Color _color = default;
    void Awake() {
        _imgBlink = gameObject.GetComponent<Image>();
        _color = _imgBlink.color;
    }

    void FixedUpdate() {
        _time += Time.deltaTime;
        if(_time >= BlinkBetween) {
            _time -= BlinkBetween;
        }

        _color.a = 1f * Mathf.Sin(2.0f * Mathf.PI * _time / BlinkBetween);
        _imgBlink.color = _color;
    }
}