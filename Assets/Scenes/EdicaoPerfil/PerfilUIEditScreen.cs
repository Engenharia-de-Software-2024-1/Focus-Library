using UnityEngine;
<<<<<<< HEAD
using UnityEngine.SceneManagement;
=======
>>>>>>> 15b7abe (falta a logica de ober o perfil)
using UnityEngine.UI;
using TMPro;
using System;

public class PerfilEditScreen : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField emailInput;
    [SerializeField] private TMP_InputField dataNascimentoInput;
    [SerializeField] private Button salvarButton;

    private PerfilManager perfilManager;
    private Perfil perfilAtual;

    private void Start()
    {
        perfilManager = new PerfilManager();
        
        salvarButton.onClick.AddListener(SalvarPerfil);

        CarregarPerfilAtual();
    }

    private void CarregarPerfilAtual()
    {
        perfilAtual = ObterPerfilAtual();

        usernameInput.text = perfilAtual.Username;
        emailInput.text = perfilAtual.Email;
        dataNascimentoInput.text = perfilAtual.DataNascimento.ToString("dd/MM/yyyy");
    }

    private async void SalvarPerfil()
    {
        try
        {
            DateTime dataNascimento = DateTime.Parse(dataNascimentoInput.text);

            await perfilManager.EditarPerfil(
                perfilAtual, 
                usernameInput.text, 
                emailInput.text, 
                dataNascimento
                //
            );

            Debug.Log("Perfil atualizado com sucesso!");
            gameObject.SetActive(false);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Erro ao salvar perfil: {ex.Message}");
        }
    }

<<<<<<< HEAD
    public void OnBackClicked() => SceneManager.LoadScene("Configuracoes Scene");

=======
>>>>>>> 15b7abe (falta a logica de ober o perfil)
    private Perfil ObterPerfilAtual()
    {
        // pegar um usuario com o get no http://localhost:8080/usuario passando o token recebido no login
        return new Perfil("UsuarioExemplo", "exemplo@email.com", "senhaExemplo");
    }
}