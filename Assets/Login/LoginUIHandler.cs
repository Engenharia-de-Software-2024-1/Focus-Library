using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginUIHandler : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button loginButton;

    private void Start()
    {
        // Verificação básica dos componentes
        if (usernameInput == null || passwordInput == null || loginButton == null)
        {
            Debug.LogError("Componentes de UI não configurados no Inspector!");
            enabled = false;
            return;
        }

        passwordInput.contentType = TMP_InputField.ContentType.Password;
        loginButton.onClick.AddListener(OnLoginClicked);
    }

    public void OnLoginClicked()
    {
        string username = usernameInput.text.Trim();
        string password = passwordInput.text;

        // Chama o AuthManager diretamente (validação feita no servidor)
        if (AuthManager.Instance != null)
        {
            AuthManager.Instance.HandleLogin(username, password);
        }
        else
        {
            Debug.LogError("AuthManager não encontrado!");
        }
    }

    public void ClearFields()
    {
        usernameInput.text = "";
        passwordInput.text = "";
    }
}