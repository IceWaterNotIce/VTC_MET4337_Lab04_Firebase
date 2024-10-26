using UnityEngine;
using TMPro;

public class UserProfilePanel : MonoBehaviour
{
    [SerializeField]
    private FirebaseManager firebaseManager;

    [SerializeField]
    private TMP_Text txt_userName;

    [SerializeField]
    private TMP_Text txt_userEmail;
    
    [SerializeField]
    private TMP_Text txt_userID;    

    private void OnEnable() 
    {
        UserData userData = firebaseManager.GetUserInfo();

        txt_userName.text = userData.name;
        txt_userEmail.text = userData.email;
        txt_userID.text = userData.userId;
    } 
}

