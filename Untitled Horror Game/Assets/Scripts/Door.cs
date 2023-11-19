using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour, IInteractable
{
    //transports player to the next room

    private RoomManager roomManager;

    [SerializeField]
    private SceneLoader.Scene room;

    private void Awake()
    {
    }

    public void Interact()
    {
        roomManager = RoomManager.Instance;
        Debug.Log("Door " + gameObject.name + " Opened");
        StartCoroutine(roomManager.ChangeRoom(room));
    }
}
