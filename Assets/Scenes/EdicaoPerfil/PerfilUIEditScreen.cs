using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Globalization;

public class PerfilEditScreen : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField dataNascimentoInput;
    [SerializeField] private Button salvarButton;

    private Perfil perfilAtual;

    private void Start()
    {
        salvarButton.onClick.AddListener(SalvarPerfil);
        CarregarPerfilAtual(); 
    }

    private async void CarregarPerfilAtual()
    {
        string token = PlayerPrefs.GetString("JWT_TOKEN");
        
        // Verificação mais rigorosa
        if (string.IsNullOrEmpty(token))
        {
            Debug.LogError("Token JWT não encontrado nos PlayerPrefs!");
            // Considere redirecionar para a tela de login
            return;
        }
        
        // Verificação adicional se quiser validar formato básico do token
        if (!token.Contains(".") || token.Split('.').Length != 3)
        {
            Debug.LogError("O token JWT parece estar em formato inválido!");
            // Considere redirecionar para a tela de login
            return;
        }

        PerfilManagerDB managerDB = new PerfilManagerDB();
        try
        {
            perfilAtual = await managerDB.ObterPerfil(token);
            PreencherCampos();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Falha ao carregar perfil: {ex.Message}");
        }
    }

    private void PreencherCampos()
    {
        usernameInput.text = perfilAtual.Username;
        emailInput.text = perfilAtual.Email;
        
        // Se tiver data de nascimento, exibe formatada; caso contrário, deixa em branco
        dataNascimentoInput.text = perfilAtual.DataNascimento.HasValue 
            ? perfilAtual.DataNascimento.Value.ToString("dd/MM/yyyy") 
            : "";
    }

    private async void SalvarPerfil()
    {
        DateTime? dataNascimento = null;
        
        // Verifica se o campo não está vazio antes de tentar converter
        if (!string.IsNullOrEmpty(dataNascimentoInput.text))
        {
            // Tenta validar a data de nascimento apenas no formato dd/MM/yyyy
            DateTime dataParseada;
            bool dataValida = DateTime.TryParseExact(
                dataNascimentoInput.text, 
                "dd/MM/yyyy", 
                CultureInfo.InvariantCulture, 
                DateTimeStyles.None, 
                out dataParseada);
                
            if (!dataValida)
            {
                Debug.LogError("Data de nascimento inválida! Use o formato dd/MM/yyyy");
                return;
            }
            
            dataNascimento = dataParseada;
        }

        // Atualiza o objeto perfil com os novos valores
        try {
            perfilAtual.Username = usernameInput.text;
            perfilAtual.Email = emailInput.text;
            perfilAtual.DataNascimento = dataNascimento;
            
            Debug.Log($"Perfil atualizado localmente: Username={perfilAtual.Username}, Email={perfilAtual.Email}, DataNascimento={perfilAtual.DataNascimento?.ToString("dd/MM/yyyy") ?? "Não informada"}");

            // Executa a atualização no servidor
            PerfilManagerDB managerDB = new PerfilManagerDB();
            string token = PlayerPrefs.GetString("JWT_TOKEN");
            
            if (string.IsNullOrEmpty(token)) {
                Debug.LogError("Token de autenticação não encontrado!");
                return;
            }
            
            Debug.Log("Iniciando atualização do perfil no servidor...");
            string novoToken = await managerDB.AtualizarPerfilComRefresh(perfilAtual, token);

            Debug.Log("Perfil atualizado com sucesso!");
            Debug.Log("Token atualizado com sucesso: " + novoToken);

            // Esconde a tela de edição
            
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao salvar perfil: {ex.Message}");
        }
    }
}
