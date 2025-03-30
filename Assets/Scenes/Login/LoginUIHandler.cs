using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginUIHandler : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button loginButton;
    [SerializeField] private TMP_Text errorText; // Novo campo para mensagens de erro

    private void Start(){}

    public void OnLoginClicked()
    {
        
        if (usernameInput == null || passwordInput == null || loginButton == null || errorText == null)
        {
            Debug.LogError("Componentes de UI não configurados no Inspector!");
            enabled = false;
            return;
        }

        passwordInput.contentType = TMP_InputField.ContentType.Password;
        errorText.text = ""; 

        string username = usernameInput.text.Trim();
        string password = passwordInput.text;

    
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            errorText.text = "Preencha todos os campos!";
            return;
        }

        
        if (AuthManager.Instance != null)
        {
            errorText.text = ""; // Limpa erros anteriores
            AuthManager.Instance.HandleLogin(username, password, () => SceneManager.LoadScene("Estante Scene"), ShowErrorMessage);
        }
        else
        {
            errorText.text = "Erro interno! Tente novamente."; 
            Debug.LogError("AuthManager não encontrado!");
        }
    }

    public void OnCadastroClicked() => SceneManager.LoadScene("TelaCadastro");

    public void ShowErrorMessage(string message)
    {
        errorText.text = message;
    }
}