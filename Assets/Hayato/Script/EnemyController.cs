using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public Transform target; // 追いかける対象
    public float rotMax; // 回転速度
    public float speed = 1; // 移動スピード

    void Update()
    {
        // ターゲット方向のベクトルを求める
        Vector3 vec = target.position - transform.position;

        // ターゲットの方向を向く
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(vec.x, 0, vec.z)), rotMax);
        // 正面方向に移動
        transform.Translate(Vector3.forward * speed); 
    }
}
