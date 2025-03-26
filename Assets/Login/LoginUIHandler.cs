using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginUIHandler : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button loginButton;
    [SerializeField] private TMP_Text errorText; // Novo campo para mensagens de erro

    private void Start()
    {
        // Verificação básica dos componentes
        if (usernameInput == null || passwordInput == null || loginButton == null || errorText == null)
        {
            Debug.LogError("Componentes de UI não configurados no Inspector!");
            enabled = false;
            return;
        }

        passwordInput.contentType = TMP_InputField.ContentType.Password;
        loginButton.onClick.AddListener(OnLoginClicked);
        errorText.text = ""; // Inicializa sem mensagem de erro
    }

    public void OnLoginClicked()
    {
        string username = usernameInput.text.Trim();
        string password = passwordInput.text;

        // Validação básica de campos vazios
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            errorText.text = "Preencha todos os campos!";
            return;
        }

        // Chama o AuthManager diretamente
        if (AuthManager.Instance != null)
        {
            errorText.text = ""; // Limpa erros anteriores
            AuthManager.Instance.HandleLogin(username, password);
        }
        else
        {
            errorText.text = "Erro interno! Tente novamente."; // Mensagem amigável
            Debug.LogError("AuthManager não encontrado!");
        }
    }

    public void ClearFields()
    {
        usernameInput.text = "";
        passwordInput.text = "";
        errorText.text = ""; // Limpa a mensagem ao limpar campos
    }

    // Método para atualizar mensagens de erro (pode ser chamado pelo AuthManager)
    public void ShowErrorMessage(string message)
    {
        errorText.text = message;
    }
}