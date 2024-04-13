using UnityEngine;
using System.Collections;

public class CharaAnime : MonoBehaviour {
	private Animator animator;
	public Transform cameraTra;
	
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		animator.SetBool("is_running_b", false);
		animator.SetBool("is_running_f", false);
		animator.SetBool("is_running_r", false);
		animator.SetBool("is_running_l", false);
		if (Input.GetKey(KeyCode.UpArrow)) {
			animator.SetBool("is_running_f", true);
		} else if (Input.GetKey(KeyCode.DownArrow)) {
			animator.SetBool("is_running_b", true);
		} else if (Input.GetKey(KeyCode.LeftArrow)) {
			animator.SetBool("is_running_l", true);
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			animator.SetBool("is_running_r", true);
		} 
	}
}
