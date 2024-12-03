using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static GameManager Instance {get; private set;}
    [FormerlySerializedAs("prefabs")]
    public List<GameObject> obstaclePrefabs;
    public float obstacleInterval = 1;
    public float obstacleSpeed = 15;
    public float obstacleOffsetX = 15;
    public Vector2 obstacleOffsetY = new Vector2(0, 0);
    [HideInInspector]
    public int score;
    private bool isGameOver = false;
    void Awake(){
        //Singletown
        if(Instance != null && Instance != this){
            Destroy(this);
        }else {
            Instance = this;
        }
    }
    public bool IsGameActive(){
        return !isGameOver;
    }
    public bool IsGameOver(){
        return isGameOver;
    }
    public void EndGame(){
        isGameOver = true;

        Debug.Log("Game Over... Your score was " + score);

        //Reload Scene
        StartCoroutine(ReloadScene(2));
    }

    private IEnumerator ReloadScene(float deLay){
        //Esperar 2 segundos(delay)
        yield return new WaitForSeconds(deLay);
        //Recarregar a cena
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
        Debug.Log("Reload scene please!!!");
    }
}
