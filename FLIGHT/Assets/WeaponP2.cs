using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponP2 : MonoBehaviour {

	public float firerate = 0;
	public float damage = 10;
	public LayerMask dontHit;

	public Transform BulletTrailPrefab;

	float timeToFire = 0;
	Transform firingpoint;
	Transform firindestination;

	// Use this for initialization
	void Awake () {
		firingpoint = transform.Find("FIREPOINT");
		firindestination = transform.Find("firingdestination");
		if (firingpoint == null) {
			Debug.LogError ("No fire point");
		}

	}

	// Update is called once per frame
	void Update () {
		if (firerate == 0) {
			if (Input.GetKeyDown (KeyCode.RightShift)) {
				Shoot();
			}
		} else {
			if(Input.GetKey (KeyCode.RightShift) && Time.time > timeToFire){
				timeToFire = Time.time + 1 / firerate;
				Shoot();
			}
		}
	}

	void Shoot () {
		Vector2 firedestination = new Vector2 (firindestination.position.x, firindestination.position.y);
		Vector2 firepoint = new Vector2 (firingpoint.position.x, firingpoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firepoint, firedestination - firepoint, 100, dontHit);
		Effect ();
		Debug.DrawLine (firepoint, firedestination, Color.cyan);
	}

	void Effect() {
		Instantiate (BulletTrailPrefab, firingpoint.position, firingpoint.rotation);
	}
}
