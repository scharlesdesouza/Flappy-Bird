using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float cooldown = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var gameManager = GameManager.Instance;
        if(gameManager.IsGameOver()){
            return;
        }

        //Update Cooldown
        cooldown -= Time.deltaTime;
        if (cooldown <= 0f){
            cooldown = gameManager.obstacleInterval;

            //Spawn obstacle
            int prefabIndex = Random.Range(0, gameManager.obstaclePrefabs.Count);
            GameObject prefab = gameManager.obstaclePrefabs[prefabIndex];
            float x = gameManager.obstacleOffsetX;
            float y = Random.Range(gameManager.obstacleOffsetY.x, gameManager.obstacleOffsetY.y);
            float z = 0;
            Vector3 position = new Vector3(x, y, z);
            Quaternion rotation = prefab.transform.rotation;
            Instantiate(prefab, position, rotation);
        }
    }
}
