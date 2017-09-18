using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : MonoBehaviour {
	Animator anim;
	int wepon_id = 1; //未所持なら0、銃なら1、剣(未実装)なら2
	bool isAttack = false;
	public GameObject[] g_x; //左右の壁オブジェクト
	public GameObject[] g_y; //上下の壁オブジェクト
	float x_max,x_min,y_max,y_min;	//移動可能範囲の定義
	Vector2 pos;

	bool isDead = false;

	AudioSource aus;
	[SerializeField]
	AudioClip[] auc;
	public GameObject Gun;
	Vector2 t;

	bool aaa = true;
	// Use this for initialization
	void Start () {
		aus = GetComponent<AudioSource> ();
		x_max = g_x[0].transform.position.x - 0.5f;
		x_min = g_x[1].transform.position.x + 0.5f;
		y_max = g_y[0].transform.position.y - 0.5f;
		y_min = g_y[1].transform.position.y + 0.5f;
		anim = GetComponent<Animator> ();
		anim.SetInteger ("Wepon", wepon_id); //所持武器(銃or剣(未実装))により攻撃変化
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead) {
			//死んだ
			return;
		}

		pos = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint (Input.mousePosition).y);
		if (pos.x < x_max && pos.x > x_min && pos.y < y_max && pos.y > y_min) {
			transform.localPosition = pos;
		}

		if (isAttack) {
			if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Idle") && aaa) {
				aaa = false;
				StartCoroutine (AttackDelay ());
			}
		}

		if (Input.GetMouseButtonDown(0) && !isAttack) {
			isAttack = true;
			Attack (wepon_id);
		}

	}


	void Attack(int id){
		if (id > 0) {
			aus.PlayOneShot (auc[0]);
			anim.SetTrigger ("Attack");
			t = new Vector2 (transform.position.x+0.2f, transform.position.y+0.1f);
			Instantiate (Gun).transform.position = t;
		}
	}

	IEnumerator AttackDelay(){
		yield return new WaitForSeconds (0.3f);
		isAttack = false;
		aaa = true;
	}


	//使わない(使うつもりだったけど使わなかった)
	/// <summary>
	/// 移動可能座標の再定義(Wall移動後に呼び出し)
	/// </summary>
	public void WallPosUpdate(){
		x_max = g_x[0].transform.position.x - 0.5f;
		x_min = g_x[1].transform.position.x + 0.5f;
		y_max = g_y[0].transform.position.y - 0.5f;
		y_min = g_y[1].transform.position.y + 0.5f;
	}


	public void AnimTriggerSet(string name){
		anim.SetTrigger (name);
	}


	public void AnimBoolSet(bool b){
		aus.PlayOneShot (auc[1]);
		anim.SetBool ("Deth", b);
		isDead = true;
	}
}
