using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed;

    public GameObject target;

    public float HP;

    public bool damage_flag;

    public float m_Damage;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            HP -= other.gameObject.GetComponent<Bullet>().m_Damage;

            if (HP <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet" && damage_flag == true && other.gameObject.GetComponent<Bullet>().m_Type == Bullet.BulletType.Penetration)
        {
            damage_flag = false;
            HP -= other.gameObject.GetComponent<Bullet>().m_Damage;

            if (HP <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        damage_flag = true;
    }
}
