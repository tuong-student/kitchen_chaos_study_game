using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using Sirenix.OdinInspector;
public class SelectModePopup : BasePopup<SelectModePopup>
{

    
    public void RandomMatch(){
        
        PhotonNetwork.JoinRandomRoom();
 
    }
    public void FindRoom(){
        FindRoomPopup.ShowPopup();
    }

    protected override void OnEnable(){
        base.OnEnable();
        PhotonManager.s.onJoinRoom += EnterRoom;
    }

    protected override void OnDisable(){
        base.OnDisable();
        PhotonManager.s.onJoinRoom -= EnterRoom;
    }

    void EnterRoom(){
        CreateRoomPopup.ShowPopup();
    }

    public void CreateRoom(){

        PhotonNetwork.CreateRoom(RandomStringGenerator.GenerateRandomString(7));

       
    }



}
