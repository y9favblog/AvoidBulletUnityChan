using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour {
	public float speed;

	void LateUpdate () {
		transform.position = new Vector2((transform.position.x + (speed*Time.deltaTime)),transform.position.y);
	}

	void OnTriggerEnter2D(Collider2D col){
		Destroy (gameObject,0.06f);
	}
}
