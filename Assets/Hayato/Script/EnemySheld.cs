using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySheld : Enemy {

    public static bool sheld_flag;

	// Use this for initialization
	void Start () {
        sheld_flag = true;
        damage_flag = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public override void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            HP -= other.gameObject.GetComponent<Bullet>().m_Damage;

            if (HP <= 0)
            {
                Destroy(this.gameObject);

                sheld_flag = false;
            }
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet" && damage_flag == true)
        {
            damage_flag = false;
            HP -= other.gameObject.GetComponent<Bullet>().m_Damage;

            if (HP <= 0)
            {
                Destroy(this.gameObject);

                sheld_flag = false;
            }
        }
    }

}
