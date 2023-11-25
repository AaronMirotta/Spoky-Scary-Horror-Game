using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get { return instance; }
    }

    //player prefab
    [SerializeField]
    private GameObject player;

    //starting Scene
    [SerializeField]
    private SceneLoader.Scene startingScene;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //Game Start
    public void GameStart()
    {
        //spawn player in, load first room
        SceneLoader.Load(startingScene);

        Vector3 startingPos = new Vector3(0, -1.5f, 0);

        Instantiate(player,startingPos,Quaternion.identity);
    }
}
