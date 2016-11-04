using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public GameObject target;
    Vector3 prev; // １フレーム前の座標保持
    public float speed = 0.1f; // 移動量
    public float moveForce = 10.0f; // 移動量(Rigidbody用)

    void Start()
    {
        prev = this.gameObject.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 enemyPos = this.gameObject.transform.localPosition;
        float vx = target.transform.position.x - enemyPos.x;
        //float vy = target.transform.position.y - enemyPos.y;
        float dx, dy, radian, value;

        /**
    　　　　 * ターゲットとのラジアン/ベクトル
    　　　　 */
        // Mathf.Atan2を使う場合
        radian = Mathf.Atan2(0, vx); // ２つの座標から角度を計算
        dx = Mathf.Cos(radian) * speed; // Sin,Cosを使用してその方向へ移動
        //dy = Mathf.Sin(radian) * speed;

        // Mathf.Atan2を使わない場合
        //value = Mathf.Sqrt( (vx * vx) + (vy * vy) ); // ２つの座標からベクトル計算
        //dx = (vx / value) * speed; // valueで割って正規化
        //dy = (vy / value) * speed;

        // 移動制御
        if (Mathf.Abs(vx) < 0.1f)
        {
            dx = 0f;
        }
        //if (Mathf.Abs(vy) < 0.1f)
        //{
        //    dy = 0f;
        //}

        // 移動を反映
        enemyPos.x += dx;
        //enemyPos.y += dy;

        this.gameObject.transform.localPosition = enemyPos;
    }
}
