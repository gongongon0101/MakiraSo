using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour {

    public GameObject spawnObject;

    void Update()
    {
        // 壁を沸かせる高さを設定
        transform.position = new Vector3(transform.position.x, Random.Range(5f, 30f), transform.position.z);
    }

    public void StartSpawn()
    {
        StartCoroutine("SpawnWalls");
    }

    public void StopSpawn()
    {
        StopCoroutine("SpawnWalls");
    }

    IEnumerator SpawnWalls()
    {
        while (true)
        {
            // 壁のインスタンスを生成
            GameObject wall = Instantiate(spawnObject, transform.position, transform.rotation);
            wall.GetComponent<Wall>().enabled = true;
            // 湧かせるインタバール
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }
}
