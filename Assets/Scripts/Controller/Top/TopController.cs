using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class TopController : MonoBehaviour {
    [SerializeField]
    protected Transform _area3D = default;
    [SerializeField]
    protected Transform _areaFloor = default;
    [SerializeField]
    protected Transform _panalBill = default;
    [SerializeField]
    protected Transform _panalBillList = default;
    [SerializeField]
    protected Transform _panalSetting = default;
    [SerializeField]
    protected Transform _panalBillSet = default;
    [SerializeField]
    protected GameObject _popupMessage = default;
    [SerializeField]
    protected GameObject _popupYesNo = default;
    [SerializeField]
    protected Text _txtBillSetContents1 = default;
    [SerializeField]
    protected Text _txtBillSetContents2 = default;
    [SerializeField]
    protected TopBillSetUI _topBillSetUI = default;

    public Transform DireLight;
    public Transform TraCamera;


    private DateTime _goldSumTime = new System.DateTime(2000, 4, 1, 0, 0, 0);
    private DateTime _now = new System.DateTime(2000, 4, 1, 0, 0, 0);
    private Dictionary<string, UserPositionAccess.UserPosition> _posList = new Dictionary<string, UserPositionAccess.UserPosition>();
    private Dictionary<int, DataBillAccess.DataBill> _billList = new Dictionary<int, DataBillAccess.DataBill>();
    private Dictionary<string, DataBillLevelAccess.DataBillLevel> _billLevelList = new Dictionary<string, DataBillLevelAccess.DataBillLevel>();
    private Vector2 _touchStartPos;

	//private int _population = 0;
    //private int _gold = 100000;
    private int _resources = 1000;
    private float _touchStart = 0;
    private bool _isSingleStart = false;

	/// <summary>
	/// 初期設定
	/// </summary>
	void Awake () {
        _popupMessage.SetActive(false);

        _topBillSetUI.OnSetDeteleClickListener(() => { OnBtnBillSetDelete(); });
        _topBillSetUI.OnSetUpdateClickListener(() => { OnBtnBillSetUpdate(); });

        CommonUIManager.Instance.Init();

        // フィールド設定
        setPositionList();

        // アイコン設定
        _billDeletePriceList = ParamList.BillDeletePriceList;
        _billList = new DataBillAccess().GetDataList();
        _billLevelList = new DataBillLevelAccess().GetDataList();
        Sprite[] imageList = Resources.LoadAll<Sprite>("Images");

        int indexBill = 0;
        foreach (DataBillAccess.DataBill dt in _billList.Values) {
            indexBill++;
            GameObject billIcon = GameObject.Instantiate(Resources.Load("Prefabs/BillIcon"), _panalBillList) as GameObject;
            billIcon.transform.Find("BtnBill").GetComponent<CommonGridUI>().SetOnClickGridListener(OnBtnBill, dt.BillId);
            billIcon.transform.Find("BtnBill").GetComponent<Image>().sprite = System.Array.Find<Sprite>(imageList, (sprite) => sprite.name.Equals("Icon"+dt.BillId));

            //billIcon.transform.Find("TxtCoin").GetComponent<Text>().text = dt.s.ToString();
            billIcon.transform.Find("TxtArea").GetComponent<Text>().text = dt.Area.ToString();
        }
    }

	/// <summary>
	/// メインループ
	/// </summary>
	void Update () {
        if (Input.GetButtonDown("Fire1")) {
            Ray ray = new Ray();
            RaycastHit hit = new RaycastHit();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (EventSystem.current.IsPointerOverGameObject ()) {
                return;
            }
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity)) {

                if (_billId != 0 && hit.transform.name.Contains("floor")) {
                    setFloorTouch(hit);
                } else if (hit.transform.name.Contains("gold")) {
                    setGoldTouch(hit);
                } else if (hit.transform.name.Contains("bill")) {
                    setBillTouch(hit);
                }
            }
        }

        if (Input.touchCount == 2) {
            _isSingleStart = false;
            if (Input.touches[0].phase == TouchPhase.Began) {
                _touchStart = Vector3.Distance(Input.touches[0].position, Input.touches[1].position);
            } else if (Input.touches[0].phase == TouchPhase.Ended) {
                float touchEnd = Vector3.Distance(Input.touches[0].position, Input.touches[1].position);

                if (System.Math.Abs((_touchStart - touchEnd)) > 10){
                    Vector3 cameraPos = TraCamera.position;
                    if (_touchStart > touchEnd) {
                        cameraPos.y += 5;
                    } else {
                        cameraPos.y -= 5;
                    }
                    TraCamera.position = cameraPos;
                }
            }
        } else if (Input.touchCount == 1) {
            if (_billId == 0){
                if (Input.touches[0].phase == TouchPhase.Began) {
                    _touchStartPos = Input.touches[0].position;
                    _isSingleStart = true;
                } else if (Input.touches[0].phase == TouchPhase.Ended) {
                    if (_isSingleStart) {
                        Vector3 cameraPos = TraCamera.position;
                        Vector2 deltaPos = _touchStartPos - Input.touches[0].position;
                        if (System.Math.Abs(deltaPos.x) > System.Math.Abs(deltaPos.y)) {
                            if (deltaPos.x > 10) {
                                cameraPos.x += 2;
                            } else if (deltaPos.x < -10) {
                                cameraPos.x -= 2;
                            }
                        } else {
                            if (deltaPos.y > 10) {
                                cameraPos.z += 2;
                            } else if (deltaPos.y < -10) {
                                cameraPos.z -= 2;
                            }
                        }
                        TraCamera.position = cameraPos;
                        _isSingleStart = false;
                    }
                }
            }
        }
	}

    private Dictionary<string, GameObject> _objGoldList = new Dictionary<string, GameObject>();
    private float _calGoldTime = 0;
    void FixedUpdate() {
        _calGoldTime -= Time.deltaTime;
        if (_calGoldTime <= 0) {
            _calGoldTime = 60;
            foreach (UserPositionAccess.UserPosition dt in _posList.Values) {
                DataBillAccess.DataBill dtBill = default;
                if(_billList.ContainsKey(dt.BillId)){
                    dtBill = _billList[dt.BillId];
                }
                if (dtBill != default && ConstList.BillDiv.BILL_DIV_BUSINESS == dtBill.Div) {
                    TimeSpan nowDiff = DateTime.Now - DateUtil.GetDateFormatDateTimeYYYYMMDDHHMMSS(dt.CollectionTime);
                    if (nowDiff.Minutes > 1) {
                        if (!_objGoldList.ContainsKey(dt.PosX + ConstCode.VALUE_SPLIT + dt.PosY)) {
                            GameObject gold = GameObject.Instantiate(Resources.Load("Prefabs/Gold"), _areaFloor) as GameObject;
                            gold.transform.eulerAngles = new Vector3(90, 0, 0);
                            gold.transform.position = _areaFloor.position + new Vector3(5, 5, 5) + new Vector3(dt.PosX * 5, 7, dt.PosY * 5);
                            gold.name = "gold" + dt.PosX + "_" + dt.PosY;
                            _objGoldList.Add(dt.PosX + ConstCode.VALUE_SPLIT + dt.PosY, gold);
                        }
                    }
                }
            }
        }
    }

    private List<GameObject> _objFloorList = new List<GameObject>();
    /// <summary>
    /// フロア情報
    /// </summary>
    private void setPositionList() {
        _posList = new UserPositionAccess().GetDataList();
        Vector3 _floorPos = _areaFloor.position;

        foreach (GameObject obj in _objBillList.Values) {
            GameObject.Destroy(obj);
        }
        _objBillList.Clear();

        for (int x = 0; x < CommonUIManager.Instance.AreaUnlock; x++) {
            _floorPos.x += 5;
            _floorPos.z = _areaFloor.position.z;
            for (int z = 0; z < CommonUIManager.Instance.AreaUnlock; z++) {
                _floorPos.z += 5;
                GameObject floor = GameObject.Instantiate(Resources.Load("Prefabs/Floor"), _floorPos - new Vector3(0, 0.1f, 0), Quaternion.identity, _areaFloor) as GameObject;
                floor.name = "floor" + x + "_" + z;
                _objFloorList.Add(floor);

                if (_posList.ContainsKey(x + ConstCode.VALUE_SPLIT + z)) {
                    setBill(x, z, _floorPos);
                }
            }
        }
    }
    private Dictionary<string, GameObject> _objBillList = new Dictionary<string, GameObject>();

    /// <summary>
    /// ビルを設定する
    /// </summary>
    /// <param name="posX"></param>
    /// <param name="posZ"></param>
    /// <param name="setPos"></param>
    private void setBill(int posX, int posZ, Vector3 setPos) {
        UserPositionAccess.UserPosition posDt = _posList[posX + ConstCode.VALUE_SPLIT + posZ];
        GameObject bill = default;
        if (ConstList.BillId.BILL_WAY == posDt.BillId) {
            bool isUp = false;
            bool isDown = false;
            bool isLeft = false;
            bool isRight = false;
            if (_posList.ContainsKey((posX + 1) + ConstCode.VALUE_SPLIT + posZ)
                && ConstList.BillId.BILL_WAY == _posList[(posX + 1) + ConstCode.VALUE_SPLIT + posZ].BillId) {
                isRight = true;
            }

            if (_posList.ContainsKey((posX - 1) + ConstCode.VALUE_SPLIT + posZ)
                && ConstList.BillId.BILL_WAY == _posList[(posX - 1) + ConstCode.VALUE_SPLIT + posZ].BillId) {
                isLeft = true;
            }

            if (_posList.ContainsKey(posX + ConstCode.VALUE_SPLIT + (posZ + 1))
                && ConstList.BillId.BILL_WAY == _posList[posX + ConstCode.VALUE_SPLIT + (posZ + 1)].BillId) {
                isUp = true;
            }

            if (_posList.ContainsKey(posX + ConstCode.VALUE_SPLIT + (posZ - 1))
                && ConstList.BillId.BILL_WAY == _posList[posX + ConstCode.VALUE_SPLIT + (posZ - 1)].BillId) {
                isDown = true;
            }

            if (isUp && isDown && isRight && isLeft) {
                bill = GameObject.Instantiate(Resources.Load("Prefabs/Bill1004"), setPos, Quaternion.identity, _areaFloor) as GameObject;
            } else if (isUp && isDown && !isRight && isLeft) {
                bill = GameObject.Instantiate(Resources.Load("Prefabs/Bill1003"), setPos, Quaternion.identity, _areaFloor) as GameObject;
            } else if (!isUp && isDown && isRight && isLeft) {
                bill = GameObject.Instantiate(Resources.Load("Prefabs/Bill1003"), setPos, Quaternion.identity, _areaFloor) as GameObject;
                bill.transform.eulerAngles = new Vector3(0, 270, 0);
            } else if (isUp && !isDown && isRight && isLeft) {
                bill = GameObject.Instantiate(Resources.Load("Prefabs/Bill1003"), setPos, Quaternion.identity, _areaFloor) as GameObject;
                bill.transform.eulerAngles = new Vector3(0, 90, 0);
            } else if (isUp && isDown && isRight && !isLeft) {
                bill = GameObject.Instantiate(Resources.Load("Prefabs/Bill1003"), setPos, Quaternion.identity, _areaFloor) as GameObject;
                bill.transform.eulerAngles = new Vector3(0, 180, 0);
            } else if (!isUp && isDown && !isRight && isLeft) {
                bill = GameObject.Instantiate(Resources.Load("Prefabs/Bill1002"), setPos, Quaternion.identity, _areaFloor) as GameObject;
            } else if (!isUp && isDown && isRight && !isLeft) {
                bill = GameObject.Instantiate(Resources.Load("Prefabs/Bill1002"), setPos, Quaternion.identity, _areaFloor) as GameObject;
                bill.transform.eulerAngles = new Vector3(0, 270, 0);
            } else if (isUp && !isDown && !isRight && isLeft) {
                bill = GameObject.Instantiate(Resources.Load("Prefabs/Bill1002"), setPos, Quaternion.identity, _areaFloor) as GameObject;
                bill.transform.eulerAngles = new Vector3(0, 90, 0);
            } else if (isUp && !isDown && isRight && !isLeft) {
                bill = GameObject.Instantiate(Resources.Load("Prefabs/Bill1002"), setPos, Quaternion.identity, _areaFloor) as GameObject;
                bill.transform.eulerAngles = new Vector3(0, 180, 0);
            } else if (isRight || isLeft) {
                bill = GameObject.Instantiate(Resources.Load("Prefabs/Bill" + posDt.BillId), setPos, Quaternion.identity, _areaFloor) as GameObject;
                bill.transform.eulerAngles = new Vector3(0, 90, 0);
            } else {
                bill = GameObject.Instantiate(Resources.Load("Prefabs/Bill" + posDt.BillId), setPos, Quaternion.identity, _areaFloor) as GameObject;
            }
        } else {
            setPos.y += 5;
            bill = GameObject.Instantiate(Resources.Load("Prefabs/Bill" + posDt.BillId), setPos, Quaternion.identity, _areaFloor) as GameObject;
        }
        bill.name = "bill_" + posDt.BillId + ConstCode.VALUE_SPLIT + posX + ConstCode.VALUE_SPLIT + posZ;

        _objBillList.Add(posX + ConstCode.VALUE_SPLIT + posZ,bill);
    }

    private void setFloorTouch(RaycastHit hit) {
        string objName = hit.transform.name.Replace("floor", "");
        string posX = objName.Split('_')[0];
        string posY = objName.Split('_')[1];
        if (!_posList.ContainsKey(posX + ConstCode.VALUE_SPLIT + posY)) {
            //int.Parse(posX), int.Parse(posZ), _billId, 1, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"))
            new UserPositionAccess().SetDataList(new UserPositionAccess.UserPosition(
                new Dictionary<string, object>() {
                    { "pos_x" , int.Parse(posX) },
                    { "pos_y", int.Parse(posY) },
                    { "bill_id", _billId },
                    { "level", 1 },
                    {"collection_time" , DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") },}));
            _posList = new UserPositionAccess().GetDataList();
            DataBillAccess.DataBill dtBill = _billList[_billId];
            DataBillLevelAccess.DataBillLevel dataBillLevel = new DataBillLevelAccess().GetDataByKey(_billId, 1);// todo lv入れる
            CommonUIManager.Instance.SetPeople();

            // コインが足りない場合
            if (0 > (CommonUIManager.Instance.Gold - dataBillLevel.Price)) {
                _popupMessage.gameObject.SetActive(true);
                _popupMessage.transform.SetParent(transform);
                _popupMessage.transform.localPosition = Vector3.zero;
                _popupMessage.transform.localScale = Vector3.one;
                _popupMessage.transform.Find("BtnYes").GetComponent<Button>().onClick.AddListener(() => { OnBtnMessageYes(); });
            } else {
                setBill(int.Parse(posX), int.Parse(posY), hit.collider.transform.position + new Vector3(0, -hit.collider.transform.position.y, 0));
                setPositionList();
                CommonUIManager.Instance.SetGold(CommonUIManager.Instance.Gold- dataBillLevel.Price);
            }
        }
    }


    /// <summary>
    /// ゴールドタッチ
    /// </summary>
    /// <param name="hit"></param>
    private void setGoldTouch(RaycastHit hit) {
        string objName = hit.transform.name.Replace("gold", "");
        string posX = objName.Split('_')[0];
        string posZ = objName.Split('_')[1];

        UserPositionAccess.UserPosition pos = _posList[posX + ConstCode.VALUE_SPLIT + posZ];
        TimeSpan diffCollection = DateTime.Now - DateUtil.GetDateFormatDateTimeYYYYMMDDHHMMSS(pos.CollectionTime);
        DataBillLevelAccess.DataBillLevel dataBillLevel = new DataBillLevelAccess().GetDataByKey(pos.BillId, 1);// todo lv入れる

        //Debug.LogError("a " + diffCollection);
        // todo テストで何度でもとれるように if (diffCollection.Hours > 0) {
            pos.CollectionTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            new UserPositionAccess().SetDataList(pos);
            _posList = new UserPositionAccess().GetDataList();
            DataBillAccess.DataBill dtBill = _billList[pos.BillId];
            CommonUIManager.Instance.SetGold(CommonUIManager.Instance.Gold + dataBillLevel.Salary);
        //}
        GameObject.Destroy(hit.transform.gameObject);
        //}
    }

    private void OnBtnSetting() {
        _panalSetting.gameObject.SetActive(true);
    }
    private string _posX = "";
    private string _posZ = "";
    private void setBillTouch(RaycastHit hit) {
        string objName = hit.transform.name.Replace("bill_", "");
        int billId = int.Parse(objName.Split('_')[0]);
        _posX = objName.Split('_')[1];
        _posZ = objName.Split('_')[2];

        UserPositionAccess.UserPosition pos = _posList[_posX + ConstCode.VALUE_SPLIT + _posZ];
        DataBillLevelAccess.DataBillLevel dataBillLevel = new DataBillLevelAccess().GetDataByKey(billId,pos.Level);
        //DataBillLevel dataBillLevelNext = new DataBillLevelAccess().GetDataByKey(billId, pos.Level+1);

        _txtBillSetContents1.text = "レベル" + pos.Level + ConstCode.NEW_LINE
            + "売上" + dataBillLevel.Salary + ConstCode.NEW_LINE
            + "入居者" + dataBillLevel.People + ConstCode.NEW_LINE
            + "増築費用" + dataBillLevel.Price + ConstCode.NEW_LINE;

        if (_billLevelList.ContainsKey(billId + ConstCode.VALUE_SPLIT + (pos.Level + 1))) {
            DataBillLevelAccess.DataBillLevel dataBillLevelNext = _billLevelList[billId + ConstCode.VALUE_SPLIT + (pos.Level + 1)];
            _txtBillSetContents2.text = "次レベル" + dataBillLevelNext.Level + ConstCode.NEW_LINE
                + "売上" + dataBillLevel.Salary + ConstCode.NEW_LINE
                + "入居者" + dataBillLevel.People + ConstCode.NEW_LINE
                + "増築費用" + dataBillLevel.Price;
        } else {
            _txtBillSetContents2.text = "最大レベル";
        }


        _panalBillSet.gameObject.SetActive(true);
    }
    private void OnBtnBillSetClose() {
        _panalBillSet.gameObject.SetActive(false);
    }
    /// <summary>
    /// 増築
    /// </summary>
    private void OnBtnBillSetUpdate() {
        UserPositionAccess.UserPosition pos = _posList[_posX + ConstCode.VALUE_SPLIT + _posZ];
        pos.Level = pos.Level + 1;
        new UserPositionAccess().SetDataList(pos);
        DataBillLevelAccess.DataBillLevel dataBillLevel = new DataBillLevelAccess().GetDataByKey(pos.BillId, pos.Level);

        CommonUIManager.Instance.SetGold(CommonUIManager.Instance.Gold - dataBillLevel.Price);
        CommonUIManager.Instance.SetPeople();

        _panalBillSet.gameObject.SetActive(false);
    }
    private void OnBtnBillSetMove() {
        _panalBillSet.gameObject.SetActive(false);
    }
    private Dictionary<int, int> _billDeletePriceList = new Dictionary<int,int>();
    /// <summary>
    /// 撤去
    /// </summary>
    private void OnBtnBillSetDelete() {
        new UserPositionAccess().DeleteDataList(_posX + ConstCode.VALUE_SPLIT + _posZ);
        GameObject.Destroy(_objGoldList[_posX + ConstCode.VALUE_SPLIT + _posZ]);
        _objGoldList.Remove(_posX + ConstCode.VALUE_SPLIT + _posZ);
        UserPositionAccess.UserPosition pos = _posList[_posX + ConstCode.VALUE_SPLIT + _posZ];

        CommonUIManager.Instance.SetGold(CommonUIManager.Instance.Gold - _billDeletePriceList[pos.Level]);

        setPositionList();
        _panalBillSet.gameObject.SetActive(false);
    }

    private void OnBtnMessageYes() {
        _popupMessage.SetActive(false);
    }

    private int _billId = 0;
    private void OnBtnBill(int billId) {
        _billId = billId;
        _panalBill.gameObject.SetActive(false);
    }
}
