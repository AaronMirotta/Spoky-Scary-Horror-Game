using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviour
{
    //control, track the rooms in the house

    [SerializeField]
    private SpriteRenderer black;

    [SerializeField]
    private float fadeTime;
    private static RoomManager instance;
    public static RoomManager Instance
    {
        get { return instance; }
    }


    [SerializeField]
    private Scene startingRoom;

    [SerializeField]
    private Scene[] rooms;

    private Scene currentRoom;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    //fade to black
    //fade screen to black, lock player input
    public IEnumerator ChangeRoom(SceneLoader.Scene newScene)
    {
        //disable player controller script
        PlayerController.Instance.enabled = false;
        
        //fade screen to black
        Color oldColor = black.color;
        Color color = Color.black;

        while(black.color.a < color.a)
        {
            float fadeAmount = black.color.a + fadeTime * Time.deltaTime;
            oldColor = new Color(black.color.r, black.color.g, black.color.b, fadeAmount);
            black.color = oldColor;
            yield return new WaitForEndOfFrame();
        }

        //load new scene and move player to proper position
        SceneLoader.Load(newScene);

        Vector3 playerPos = PlayerController.Instance.transform.position;

        Vector3 newPos = new Vector3(-playerPos.x, playerPos.y, playerPos.z);

        PlayerController.Instance.transform.position = newPos;

        //fade back in
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        Color oldColor = black.color;
        Color color = Color.clear;

        //pause for a better feeling transition
        yield return new WaitForSeconds(0.5f);

        //fade back in
        while (black.color.a > color.a)
        {
            float fadeAmount = black.color.a - fadeTime * Time.deltaTime;
            oldColor = new Color(black.color.r, black.color.g, black.color.b, fadeAmount);
            black.color = oldColor;
            yield return new WaitForEndOfFrame();
            
            if(black.color.a < 0.6f)
            {
                //enable player controls mid way through the fade in
                PlayerController.Instance.enabled = true;
            }
        }

    }
}
