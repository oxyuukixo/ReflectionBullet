using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    //弾の種類
    public enum BulletType
    {
        Normal,
        Penetration,
        Diffusion,
        Exploding,
        Division
    }

    //種類
    public BulletType m_Type;

    //体力
    public int HitHP;

    //消えるまでの時間
    public float DestoryTime = 5;


    //爆裂するかのフラグ
    private bool IsDivision = false;

    //出現してからの時間
    private float DestroyCurrentTime = 0;

    // Use this for initialization
    void Start () {

        if (m_Type == BulletType.Penetration)
        {
            transform.FindChild("Collision").gameObject.layer = LayerMask.GetMask("PenetrationBullet");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
        if(DestoryTime < (DestroyCurrentTime += Time.deltaTime))
        {
            Destroy(gameObject);
        }

        if(IsDivision)
        {
            GameObject BulletObj;

            for (int i = 1; i <= 2; i++)
            {
                BulletObj = Instantiate(gameObject);

                BulletObj.transform.position = transform.position;

                BulletObj.GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(0, 0, 30 * Mathf.Pow(-1,i)) * GetComponent<Rigidbody2D>().velocity;

                BulletObj.GetComponent<Bullet>().HitHP = HitHP;
            }

            Destroy(gameObject);
        }

	}

    void OnCollisionEnter2D(Collision2D Hit)
    {
        switch(m_Type)
        {
            case BulletType.Normal:

                HitHP--;

                break;

            case BulletType.Penetration:

                if(Hit.gameObject.tag == "Enemy")
                {

                }
                else
                {
                    HitHP--;
                }
             
                break;

            case BulletType.Diffusion:

                HitHP--;

                break;

            case BulletType.Exploding:

                HitHP--;

                break;

            case BulletType.Division:

                HitHP--;

                if (m_Type == BulletType.Division && HitHP > 0)
                {
                    IsDivision = true;
                }

                break;
        }

        if (HitHP <= 0)
        {
            Destroy(gameObject);
        }

    }
}
