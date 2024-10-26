using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using System;

public class FirebaseManager : MonoBehaviour
{
    FirebaseAuth auth;
    FirebaseUser user;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            auth = FirebaseAuth.DefaultInstance;
        });
    }

    public void SignUp(string email, string password, Action OnSuccess, Action<String> OnFail)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                OnFail("Sign Up Failed: " + task.Exception);
                return;
            }

            FirebaseUser newUser = task.Result.User;
            user = newUser;

            Debug.LogFormat("User signed up successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

            OnSuccess();
        });
    }

    public void SignIn(string email, string password, Action OnSuccess, Action<String> OnFail)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                OnFail("Sign In Failed: " + task.Exception);
                return;
            }

            user = task.Result.User;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                user.DisplayName, user.UserId);

            OnSuccess();
        });
    }

    public void SignOut()
    {
        if (auth != null)
        {
            auth.SignOut();
            Debug.Log("User signed out successfully.");
        }
        else
        {
            Debug.LogError("Sign out failed: Auth is not initialized.");
        }
    }

    public UserData GetUserInfo()
    {
        if (user != null)
        {
            UserData userData = new UserData();

            userData.name = user.DisplayName ?? "No display name";
            userData.email = user.Email ?? "No email";
            userData.userId = user.UserId;

            Debug.LogFormat("User Info: {0}, {1}, {2}", userData.name, userData.email, userData.userId);

            return userData;
        }
        else
        {
            Debug.LogError("No user is signed in.");

            return null;
        }
    }

}
