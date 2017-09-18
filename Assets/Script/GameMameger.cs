using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using UnityEngine.SceneManagement;

public class GameMameger : MonoBehaviour {
	int StageLv = 1;
	int enemycounter = 1;
	AudioSource aus;
	public AudioClip auc;

	public GameObject[] p_obj;
	static public float Score;
	int hp = 3;
	public Text score_txt;
	public Text stagelv;
	StringBuilder s = new StringBuilder("Score:");	//頻繁に文字連結を行うためStringBuilderを使用
	public Button[] bt;
	public GameObject[] enemy;
	public GameObject titlebt;

	bool is_dead = false;

	void Start () {
		Cursor.visible = false;	//マウスカーソル非表示
		aus = GetComponent<AudioSource> ();
		Score = 0;
	}
	

	void Update () {
		
		if (hp > 0) {
			Score = Score + (Time.deltaTime * 16); //FPSに応じたスコア加算
			s.Append ((int)Score);
			score_txt.text = s.ToString ();
			s.Length = 0;
			s.Append ("Score:");
		} else if(is_dead == false){
			is_dead = true;
			Cursor.visible = true;	//マウスカーソル表示
			StartCoroutine (waiting ());
		}

		/*
		 * スコア500毎にステージレベルアップ 
		 * スコア650毎に敵出現、650*4スコアまでは1体、650*8スコアまでは２体、それ以降は3体出現
		 * 敵のHPと移動先座標(id)と倒した時のスコア(score)はPrefabフォルダ内の"Uni"のEnemyコンポーネントから変更可能
		*/

		if (Score > (500 * StageLv) && StageLv < 8) {
			aus.PlayOneShot (auc);
			StageLv++;
			stagelv.text = "Lv:"+StageLv;
			p_obj [StageLv-1].SetActive (true);
		}
		if (Score > (650 * enemycounter)) {
			enemycounter++;
			if (enemycounter <= 4) {
				Instantiate (enemy [0]);
			} else if (enemycounter <= 8) {
				Instantiate (enemy [0]);
				Instantiate (enemy [2]);
			} else {
				Instantiate (enemy [0]);
				Instantiate (enemy [1]);
				Instantiate (enemy [2]);
			}
		}
	}

	public void Damage(){
		hp--;
		bt [hp].interactable = false;
	}

	public void MoveToTitle(){
		Score = 0;
		SceneManager.LoadScene ("Title");
	}

	IEnumerator waiting(){
		yield return new WaitForSeconds (1.0f);
		titlebt.SetActive (true);
	}
}
