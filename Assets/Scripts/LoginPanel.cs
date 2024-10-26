using UnityEngine;
using TMPro;

public class LoginPanel : MonoBehaviour
{
    [SerializeField]
    private FirebaseManager firebaseManager;

    [SerializeField]
    private TMP_InputField email;

    [SerializeField]
    private TMP_InputField pw;

    [SerializeField]
    private GameObject userProfile;


    public void OnLoginButtonClicked()
    {
        firebaseManager.SignIn(email.text, pw.text,
            () =>
            {
                ShowUserProfile();
            },
            (errorMsg) =>
            {

            }
        );
    }


    public void OnCreateButtonClicked()
    {
        firebaseManager.SignUp(email.text, pw.text,
            () =>
            {
                ShowUserProfile();
            },
            (errorMsg) =>
            {

            }
        );
    }


    private void ShowUserProfile()
    {
        userProfile.SetActive(true);
        this.gameObject.SetActive(false);
    }

}

