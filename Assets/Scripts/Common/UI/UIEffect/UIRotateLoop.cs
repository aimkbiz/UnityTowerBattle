using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UIを回転ループさせる
/// </summary>
public class UIRotateLoop : MonoBehaviour {
    // フュージョン状態
    public enum EnumRotateVector { x, y, z }


    /// <summary>
    /// 回転速度
    /// </summary>
    [SerializeField]
    public float RotateSpeed = 1f;
    /// <summary>
    /// 回転させる方向
    /// </summary>
    [SerializeField]
    public EnumRotateVector RotateVector = EnumRotateVector.x;
    /// <summary>
    /// 回転させる向き(右がtrue、左がfalse)
    /// </summary>
    [SerializeField]
    public bool RotateRigth = true;
    /// <summary>
    /// 起動時既に動いている状態にするか
    /// </summary>
    [SerializeField]
    protected bool IsStart = true;

    private float _deltaTotol = 0;    // 点滅切り替える時間を設定
    private bool _isStart = true;

    void Awake() {
        _isStart = IsStart;
        if (RotateRigth) {
            RotateSpeed = -RotateSpeed;
        }
    }

    void FixedUpdate() {
        if (_isStart) {
            _deltaTotol += Time.deltaTime;

            if (RotateVector == EnumRotateVector.x) {
                transform.Rotate(new Vector3(90, 0, 0) * Time.deltaTime * RotateSpeed, Space.Self);
            } else if (RotateVector == EnumRotateVector.y) {
                transform.Rotate(new Vector3(0, 90, 0) * Time.deltaTime * RotateSpeed, Space.Self);
            } else if (RotateVector == EnumRotateVector.z) {
                transform.Rotate(new Vector3(0, 0, 90) * Time.deltaTime * RotateSpeed, Space.Self);
            }
        }
    }

    public void SetStart() {
        _isStart = true;
    }
}