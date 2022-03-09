using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    // �������� ��������� ���������� � ������ ������ � ����
    public GameObject[] Enemies;
    public List<MeshCollider> planes;

    // ��� ���������� ������ ������� ���������� �����������
    public GameObject[] enemyRoom1, enemyRoom2, enemyWarehouse;

    private float x, z;
    private Vector3 spawnPos;

    private int randPlane, randEnemy;

    void Start()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        for (int i = 0; i < planes.Count; i++)
        {
            if (planes[i].name == "GroundRoom1")
            {
                for (int j = 0; j < enemyRoom1.Length; j++)
                {
                    randEnemy = Random.Range(0, Enemies.Length);
                    enemyRoom1[j] = EnemyCreate(i, randEnemy);
                }
            }

            if (planes[i].name == "GroundRoom2")
            {
                for (int j = 0; j < enemyRoom2.Length; j++)
                {
                    randEnemy = Random.Range(0, Enemies.Length);
                    enemyRoom2[j] = EnemyCreate(i, randEnemy);
                }
            }

            if (planes[i].name == "Ground_Warehouse")
            {
                for (int j = 0; j < enemyWarehouse.Length; j++)
                {
                    randEnemy = Random.Range(0, Enemies.Length);
                    enemyWarehouse[j] = EnemyCreate(i, randEnemy);
                }
            }
        }
    }

    private GameObject EnemyCreate(int plane, int whatEnamy)
    {
        // ������� ��������� ����� ��� ������ � �������� ��������� �������
        x = Random.Range(planes[plane].transform.position.x - Random.Range(0, planes[plane].bounds.extents.x), planes[plane].transform.position.x + Random.Range(0, planes[plane].bounds.extents.x));
        z = Random.Range(planes[plane].transform.position.z - Random.Range(0, planes[plane].bounds.extents.z), planes[plane].transform.position.z + Random.Range(0, planes[plane].bounds.extents.z));

        spawnPos = new Vector3(x, 0.2f, z);

        // ������� ���������� ���������� ���������
        GameObject enemy = Instantiate(Enemies[whatEnamy], spawnPos, Quaternion.identity);

        return enemy;
    }

    public int RandPlane()
    {

        return randPlane;
    }
}