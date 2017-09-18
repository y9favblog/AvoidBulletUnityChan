using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public int id = 0;
	public int hp;
	public int score = 50;
	SpriteRenderer e_spr;
	float inv_time = 0.4f; //ダメージ字の点灯間隔

	void Start(){
		e_spr = GetComponent<SpriteRenderer> ();
	}

	void Update(){
		if (transform.position.x <= 5.5f) {
			return;
		}

		if (id == 0 && transform.position.y <= 1.5f) {
			transform.position = new Vector2 (transform.position.x, transform.position.y + (Time.deltaTime * 5));
		} else if (id == 2 && transform.position.y >= -1.5f) {
			transform.position = new Vector2 (transform.position.x, transform.position.y - (Time.deltaTime * 5));
		}
		transform.position = new Vector2 (transform.position.x - (Time.deltaTime*2), transform.position.y);
	}

	void OnTriggerEnter2D(Collider2D col){
			hp--;
			StartCoroutine (dmg ());
			if (hp <= 0) {
				GameMameger.Score += score;
				Destroy (gameObject);
			}
	}

	IEnumerator dmg(){
		for (int i = 0; i < 5; i++) {
			if (i % 2 == 0) {
				e_spr.color = new Color (1, 1, 1, 0);
			} else {
				e_spr.color = new Color (1, 1, 1, 1);
			}
			yield return new WaitForSeconds (inv_time / 5);
			e_spr.color = new Color (1, 1, 1, 1);
		}
	}
}
