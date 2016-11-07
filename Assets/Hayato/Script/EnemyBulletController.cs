using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour {

    public int speed;

	// Use this for initialization
	void Start () {

        GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;
	}
	
	// Update is called once per frame
	void Update () {

        //画面外に出たら削除
        if (!GetComponent<Renderer>().isVisible)
        {
            Destroy(this.gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D other){

        if(other.tag == "Player")
        {
            Destroy(this.gameObject);
        }

    }
}
