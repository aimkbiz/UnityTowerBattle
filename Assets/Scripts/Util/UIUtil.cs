using UnityEngine;

/// <summary>
/// UIの関数
/// </summary>
public class UIUtil : MonoBehaviour {
    /// <summary>
    /// 子のオブジェクトを削除
    /// </summary>
    /// <param name="parent"></param>
    public static void DestroyChild(Transform parent) {
        foreach (Transform child in parent.transform) {
            Destroy(child.gameObject);
        }
    }
}
