using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour
{
    public TMP_InputField loginUsernameTxt;
    public TMP_InputField loginPasswordTxt;

    public TMP_InputField registerUsernameTxt;
    public TMP_InputField registerPasswordTxt;
    public TMP_InputField registerConfirmPasswordTxt;

    public Button loginBtn;
    public Button registerBtn;

    public TextMeshProUGUI statusTxt;
    public Button openRegisterBtn;
    public Button openLoginBtn;

    public GameObject loginPanel;
    public GameObject registerPanel;

    private void Awake()
    {
        loginBtn.onClick.AddListener(Login);
        registerBtn.onClick.AddListener(Register);
        openLoginBtn.onClick.AddListener(ToggleRegisterPanel);
        openRegisterBtn.onClick.AddListener(ToggleRegisterPanel);
    }
    private void OnEnable()
    {
        loginUsernameTxt.text = GetUsername();
        loginPasswordTxt.text = GetPassword();
    }

    public void AutoLogin()
    {
        if(GetUsername() == "")
        {
            this.gameObject.SetActive(true);
            return;
        }
        LoginUser user = new LoginUser(GetUsername(),GetPassword());
        string json = JsonConvert.SerializeObject(user);
        DbRequestManager.Instance.DataSendRequest(APIUrls.userLoginApi, json, (s) =>
        {
            Debug.Log(s);
            LoginUserRespone respone = JsonConvert.DeserializeObject<LoginUserRespone>(s);
            Debug.Log(respone.result);
            SaveAccount(loginUsernameTxt.text, loginPasswordTxt.text, respone.result);
            if (!respone.isSuccess)
            {
                this.gameObject.SetActive(true);
            }
        });
    }
    private void Login()
    {
        if(IsLoginInputCorrect())
        {
            LoginUser user = new LoginUser(loginUsernameTxt.text , loginPasswordTxt.text); 
            string json = JsonConvert.SerializeObject(user);
            DbRequestManager.Instance.DataSendRequest(APIUrls.userLoginApi, json , (s) =>
            {
                Debug.Log(s);
                LoginUserRespone respone = JsonConvert.DeserializeObject<LoginUserRespone>(s);
                if(respone.isSuccess)
                {
                    SetStatus("Success");
                    SaveAccount(loginUsernameTxt.text,loginPasswordTxt.text , respone.result);
                    this.gameObject.SetActive(false);
                }
                else
                {
                    SetStatus("Failed");
                }
            });
        }
    }

    private void Register()
    {
        if(IsRegisterInputCorrect())
        {
            UserRegister user = new UserRegister(registerUsernameTxt.text, registerPasswordTxt.text, registerConfirmPasswordTxt.text);
            string json = JsonConvert.SerializeObject(user);
            Debug.Log(json);
            DbRequestManager.Instance.DataSendRequest(APIUrls.userRegisterApi, json, (s) =>
            {
                Debug.Log(s);
                LoginUserRespone respone = JsonConvert.DeserializeObject<LoginUserRespone>(s);
                if (respone.isSuccess)
                {
                    SetStatus("Success");
                    SaveAccount(registerUsernameTxt.text , registerPasswordTxt.text, respone.result);
                }
                else
                {
                    SetStatus("Failed");
                }

            });
        }
    }

    private void ToggleRegisterPanel()
    {
        registerPanel.SetActive(!registerPanel.activeInHierarchy);
        loginPanel.SetActive(!registerPanel.activeInHierarchy);
    }
    private bool IsLoginInputCorrect()
    {
        return loginUsernameTxt.text != "" && loginPasswordTxt.text != "";
    }

    private bool IsRegisterInputCorrect()
    {
        return registerUsernameTxt.text != "" && registerPasswordTxt.text != "" && registerConfirmPasswordTxt.text != "";
    }

    private void SetStatus(string status)
    {
        statusTxt.text = status;
    }

    private void SaveAccount(string username , string password , string userToken)
    {
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetString("password",password);
        PlayerPrefs.SetString("usertoken", userToken);
        Debug.Log(userToken);
    }

    private string GetUsername()
    {
        return PlayerPrefs.GetString("username","");
    }

    private string GetPassword()
    {
        return PlayerPrefs.GetString("password","");
    }

    private string GetUserToken()
    {
        return PlayerPrefs.GetString("usertoken", "");
    }

}

public class LoginUser
{
    public string username;
    public string password;
    public LoginUser(string username, string password)
    {
        this.username = username;
        this.password = password;
    }
}

public class LoginUserRespone
{
    public string statusCode;
    public bool isSuccess;
    public string errorMessage;
    public string result;
}

public class UserRegister
{
    public string userName;
    public string password;
    public string confirmPassword;
    public string email = "";
    public string fistName = "";
    public string lastName = "";
    public UserRegister(string username, string password, string confirmPassword)
    {
        this.userName = username;
        this.password = password;
        this.confirmPassword = confirmPassword;
    }
}