using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ボタンクリックでobjectをon offするだけの処理
/// </summary>
public class UIAttachButtonAreaOnOff : MonoBehaviour {
    [SerializeField]
    protected GameObject _area;

    private Button _btnOnOff;

    private void Awake() {
        _btnOnOff = gameObject.GetComponent<Button>();
        _btnOnOff.onClick.AddListener(() => { _area.SetActive(!_area.activeInHierarchy); });
    }
}
