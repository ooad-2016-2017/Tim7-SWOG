using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarBehaviour : MonoBehaviour {

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


	void FixedUpdate()
	{
		curspeed = new Vector2(rigidbody2D.velocity.x,    rigidbody2D.velocity.y);

		if (curspeed.magnitude > maxspeed)
		{
			curspeed = curspeed.normalized;
			curspeed *= maxspeed;
		}

		if (Input.GetKey(KeyCode.W))
		{
			rigidbody2D.AddForce(transform.up * power);
			rigidbody2D.drag = friction;
		}
		if (Input.GetKey(KeyCode.S))
		{
			rigidbody2D.AddForce(-(transform.up) * (power/2));
			rigidbody2D.drag = friction;
		}
		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(Vector3.forward * turnpower);
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(Vector3.forward * -turnpower);
		}
		if (Input.GetKey(KeyCode.R))
		{
			SceneManager.LoadScene ("level1");
		}

	}
	void OnCollisionEnter(Collision col){

	

		//transform.position = spos;
	}
	IEnumerator cekaj(){
		yield return new WaitForSeconds(5);
	}

}
