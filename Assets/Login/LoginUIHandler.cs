using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginUIHandler : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button loginButton;
   // [SerializeField] private TextMeshProUGUI errorText;

    private void Start()
    {
        // Verifica componentes atribuídos
        if (usernameInput == null || passwordInput == null || loginButton == null ) // || errorText == null)
        {
            Debug.LogError("Componentes de UI não configurados no Inspector!");
            enabled = false; // Desativa o script
            return;
        }

        passwordInput.contentType = TMP_InputField.ContentType.Password;
        loginButton.onClick.AddListener(OnLoginClicked);
       // errorText.gameObject.SetActive(false);
    }

    public void OnLoginClicked()
    {
        string username = usernameInput.text.Trim();
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
          //  ShowError("Preencha todos os campos!");
            return;
        }

        // Verifica se o AuthManager está acessível
        if (AuthManager.Instance == null)
        {
            Debug.LogError("AuthManager não encontrado na cena!");
            return;
        }

        AuthManager.Instance.HandleLogin(username, password);
    }

    //public void ShowError(string message)
   // {
        //errorText.text = message;
        //errorText.gameObject.SetActive(true);
   // }

    public void ClearFields()
    {
        usernameInput.text = "";
        passwordInput.text = "";
       // errorText.gameObject.SetActive(false);
    }
}