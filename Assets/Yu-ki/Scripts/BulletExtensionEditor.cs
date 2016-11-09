using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Bullet))]
public class BulletExtensionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Bullet bullet = target as Bullet;

        switch (bullet.m_Type)
        {
            case Bullet.BulletType.Explosion:

                bullet.m_ExplosionParticle = (GameObject)EditorGUILayout.ObjectField("ExplosionParticle", bullet.m_ExplosionParticle,typeof(GameObject),true);

                break;

            case Bullet.BulletType.Diffusion:

                bullet.m_DiffusionNum = EditorGUILayout.IntField("DiffusionNum", bullet.m_DiffusionNum);

                bullet.m_DiffusionAngle = EditorGUILayout.FloatField("DiffusionAngle", bullet.m_DiffusionAngle);

                break;
        }

    }
}