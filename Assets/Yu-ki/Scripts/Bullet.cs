using UnityEngine;
using System.Collections;

//弾のスクリプト
public class Bullet : MonoBehaviour {

    //弾の種類
    public enum BulletType
    {
        Normal,         //通常弾
        Speed,          //スピード弾
        Penetration,    //貫通弾
        Diffusion,      //拡散弾
        Explosion,      //爆裂弾
        Division        //分裂弾
    }

    //種類
    public BulletType m_Type;

    //体力
    public int m_HitHP;

    //攻撃力
    public float m_Damage;

    //拡散弾の時の弾の数
    public int m_diffusionNum = 0;

    //消えるまでの時間
    public float m_DestoryTime = 5;

    //爆裂するかのフラグ
    private bool m_IsDivision = false;

    //出現してからの時間
    private float m_DestroyCurrentTime = 0;

    // Use this for initialization
    void Start () {

        //貫通弾だったら
        if (m_Type == BulletType.Penetration)
        {
            //あたり判定をするコリジョンを貫通タイプにする
            transform.FindChild("Collision").gameObject.layer = LayerMask.GetMask("PenetrationBullet");
        }
        //拡散だったら
        if (m_Type == BulletType.Diffusion)
        {
            GameObject BulletObj;

            //拡散させる弾の数だけ弾を生成
            for (int i = 1; i <= 2; i++)
            {
                BulletObj = Instantiate(gameObject);

                BulletObj.transform.position = transform.position;

                BulletObj.GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(0, 0, 30 * Mathf.Pow(-1, i)) * GetComponent<Rigidbody2D>().velocity;

                BulletObj.GetComponent<Bullet>().m_Type = BulletType.Normal;
            }

            //元のオブジェクトは削除
            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {

        //出現してから一定時間以上経過していたら削除
        if (m_DestoryTime < (m_DestroyCurrentTime += Time.deltaTime))
        {
            Destroy(gameObject);
        }

        //分裂するフラグが立っていたら
        if(m_IsDivision)
        {
            GameObject BulletObj;

            //二つに分裂させる
            for (int i = 1; i <= 2; i++)
            {
                BulletObj = Instantiate(gameObject);

                BulletObj.transform.position = transform.position;

                BulletObj.GetComponent<Rigidbody2D>().velocity = Quaternion.Euler(0, 0, 30 * Mathf.Pow(-1,i)) * GetComponent<Rigidbody2D>().velocity;

                BulletObj.GetComponent<Bullet>().m_HitHP = m_HitHP;
            }

            //元のオブジェクトは削除
            Destroy(gameObject);
        }

	}

    void OnCollisionEnter2D(Collision2D Hit)
    {
        //ヒットした時の処理は弾のタイプによって変更
        switch (m_Type)
        {
            //貫通弾の場合
            case BulletType.Penetration:

                //敵以外に当たっていたら弾の体力を減らす
                if(Hit.gameObject.tag != "Enemy")
                { 
                    m_HitHP--;
                }
             
                break;

            //爆裂弾の場合
            case BulletType.Explosion:

                m_HitHP--;

                //爆発のパーティクルを発生


                break;

            //分裂弾の場合
            case BulletType.Division:

                m_HitHP--;

                //弾の体力があったら
                if (m_HitHP > 0)
                {
                    //分裂するフラグを立てる
                    m_IsDivision = true;
                }

                break;
            
            default:

                //その他の弾の場合は弾の体力を減らすだけ
                m_HitHP--;

                break;
        }

        if (m_HitHP <= 0)
        {
            Destroy(gameObject);
        }

    }
}
