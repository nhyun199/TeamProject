using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomManager : MonoBehaviourPunCallbacks
{

    public static RoomManager instance;
    // Start is called before the first frame update
    public GameObject player;

    public Transform spawnPoint;
    
    [Space]
    public GameObject roomCam;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        Debug.Log("Connection...");

        PhotonNetwork.ConnectUsingSettings();  
    }

    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        Debug.Log("Connected to Server");

        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();

        PhotonNetwork.JoinOrCreateRoom("test", null, null);

        Debug.Log("We're connecte and in a room now");
    }
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Joined a room");

        roomCam.SetActive(false);

        SpawnPlayer();
    }
    public void SpawnPlayer()
    {
        GameObject _player = PhotonNetwork.Instantiate(player.name,
                                   spawnPoint.position,
                                   Quaternion.identity);

        _player.GetComponent<PlayerSetup>().IsLocalPlayer();
        _player.GetComponent<Health>().isLocalPlayer = true;
    }
}
