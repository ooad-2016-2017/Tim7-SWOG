using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rigi : MonoBehaviour {

	public float power = 3;
	public float maxspeed = 5;
	public float turnpower = 2;
	public float friction = 3;
	public Vector2 curspeed ;
	Rigidbody2D rigidbody2D;
	UnityEngine.Vector3 spos;
	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D>();
		spos = transform.position;
	}	
	void OnCollisionEnter2D(Collision2D col){
		//SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		SceneManager.LoadScene ("gameover");

	}


}
