using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// テキストを点滅させる
/// </summary>
public class UITextBlink : MonoBehaviour {
    /// <summary>
    /// 点滅間隔
    /// </summary>
    [SerializeField]
    public float BlinkBetween = 2.0f;
    private Text _txtBlink;
    private float _time = 0.0f;

    void Awake() {
        _txtBlink = gameObject.GetComponent<Text>();
    }

    void FixedUpdate() {
        _time += Time.deltaTime;
        if(_time >= BlinkBetween) {
            _time -= BlinkBetween;
        }
        Color c = _txtBlink.color;
        c.a = 0.75f + 0.25f * Mathf.Sin(2.0f * Mathf.PI * _time / BlinkBetween);
        _txtBlink.color = c;
    }
}