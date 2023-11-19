using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    //holds data for a room

    //room name
    [SerializeField]
    private string roomName;

    [SerializeField]
    private GameObject roomPrefab;

    [SerializeField]
    private Door[] doors;

    public string GetRoomName()
    {
        return roomName;
    }
    public GameObject GetRoomPrefab()
    {
        return roomPrefab;
    }
}
