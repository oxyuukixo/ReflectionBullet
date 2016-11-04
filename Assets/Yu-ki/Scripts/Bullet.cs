using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public enum BulletType
    {
        Normal,
        Penetration,
        Diffusion,
        Exploding,
        Division
    }

    public BulletType m_Type;

    bool IsDestroy = false;

    public int HitHP;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
        if(IsDestroy)
        {
            GameObject BulletObj;

            for (int i = 1; i <= 2; i++)
            {
                BulletObj = Instantiate(gameObject);

                BulletObj.transform.position = transform.position;

                BulletObj.GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(0, 0, 30 * Mathf.Pow(-1,i)) * GetComponent<Rigidbody2D>().velocity;

                BulletObj.GetComponent<Bullet>().HitHP = HitHP;

                Debug.Log(BulletObj.GetComponent<Rigidbody2D>().velocity);
            }

            Destroy(gameObject);
        }

	}

    void OnCollisionEnter2D(Collision2D Hit)
    {
        HitHP--;

        if(m_Type == BulletType.Division && HitHP > 0)
        {
            IsDestroy = true;
        }
        else if(HitHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
