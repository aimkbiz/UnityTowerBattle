using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System;

/// <summary>
/// 演出関数
/// </summary>
public class EffectUtil {
	public static GameObject InsPrefab(Vector3 pos, string prefabName) {
		GameObject dtObj = GameObject.Instantiate(Resources.Load(prefabName), pos, Quaternion.identity) as GameObject;
		return dtObj;
	}
}