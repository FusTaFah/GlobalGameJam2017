using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkStartup : Photon.MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }

    public void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        foreach (object error in codeAndMsg)
        {
            Debug.Log(error);
        }
        PhotonNetwork.CreateRoom(null);
    }

    public void OnJoinedRoom()
    {
        Debug.Log("Connected to room");
        GameObject player = PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity, 0);
        player.transform.position = new Vector3(-3.5702f, 0.15367f, -3.35f);
    }
}
