using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ボタンクリックでobjectを閉じるだけの処理
/// </summary>
public class UIAttachButtonAreaClose : MonoBehaviour {
    [SerializeField]
    protected GameObject _area;

    private Button _btnClose;

    private void Awake() {
        _btnClose = gameObject.GetComponent<Button>();
        _btnClose.onClick.AddListener(() => { _area.SetActive(false); });
    }
}
