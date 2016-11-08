using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySheld : MonoBehaviour {

    public int HP;

    public static bool sheld_flag;

	// Use this for initialization
	void Start () {
        sheld_flag = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            HP--;

            if (HP <= 0)
            {
                Destroy(this.gameObject);

                sheld_flag = false;
            }
        }
    }
}
