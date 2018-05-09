using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class LogIn : MonoBehaviour {

    public string desiredPass;
    public InputField password;
    public GameObject desiredWindow;

    public UnityEvent OnPasswordCorrect;
    
    public void CheckPass()
    {
        if (password.text == desiredPass)
        {
            this.transform.parent.gameObject.SetActive(false);
            desiredWindow.SetActive(true);
            OnPasswordCorrect.Invoke();
        }
        else
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void OnEnable()
    {
        password.text = string.Empty;
        this.transform.GetChild(0).gameObject.SetActive(false);
    }
}
