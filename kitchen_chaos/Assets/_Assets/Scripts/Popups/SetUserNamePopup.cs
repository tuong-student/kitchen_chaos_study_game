using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUserNamePopup : BasePopup<SetUserNamePopup>
{

    public GameObject closeButton;
    string userName;
    public void Start(){
        SetCloseButton(UserData.isInitName);
    }

    public void SetName(string name){
        userName = name;
    }

    public void Next(){
        UserData.userName = userName;
        HidePopup();
    }

    

    public void SetCloseButton(bool isShow){
        closeButton.SetActive(isShow);
    }
}