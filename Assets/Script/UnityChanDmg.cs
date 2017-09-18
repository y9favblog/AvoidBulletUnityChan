using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class UnityChanDmg : MonoBehaviour {
	[SerializeField]int hp = 3;
	bool inv = false; //無敵状態か
	public float inv_time = 1.5f; //ダメージ後の無敵時間
	[SerializeField]SpriteRenderer uni_spr;
	[SerializeField]UnityChanController uni_cnt;
	AudioSource aus;
	public AudioClip dmg_se;
	public AudioSource audios;
	public GameMameger g;

	void Start(){
		aus = GetComponent<AudioSource> ();
	}


	void OnParticleCollision(GameObject obj){
		if (obj.tag == "DamageObject" && !inv) {
			inv = true;
			StartCoroutine (invobj());
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "DamageObject" && !inv) {
			inv = true;
			StartCoroutine (invobj());
		}
	}


	IEnumerator invobj(){
		hp--;
		g.Damage ();
		if (hp > 0) {
			aus.PlayOneShot (dmg_se);
			if (hp == 1) {
				//aum.SetFloat ("pich",1.5f);
				audios.pitch = 1.5f;
			}
			uni_cnt.AnimTriggerSet ("Damage");

			for (int i = 0; i < 10; i++) {
				if (i % 2 == 0) {
					uni_spr.color = new Color (1, 1, 1, 0);
				} else {
					uni_spr.color = new Color (1, 1, 1, 1);
				}
				yield return new WaitForSeconds (inv_time / 10);
				uni_spr.color = new Color (1, 1, 1, 1);
			}
			yield return new WaitForSeconds (inv_time / 10);
			inv = false;

		} else {
			uni_cnt.AnimBoolSet (true);
			Destroy (gameObject);
		}
	}
}
