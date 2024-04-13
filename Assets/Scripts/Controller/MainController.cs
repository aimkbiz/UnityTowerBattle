using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;


public class MainController : MonoBehaviour {
	private List<GameObject> _enemyList = new List<GameObject>();

    /// <summary>
    /// 初期設定
    /// </summary>
    protected virtual void Awake() {
		Vector3 enemyPos = transform.position;
		//enemyPos.x -= 25;
		for(int i=0;i<5;i++){
			enemyPos.x += 4;
			_enemyList.Add(EffectUtil.InsPrefab(enemyPos,"Prefabs/Lobot"));
		}
    }

	void Update () {

	}
}
