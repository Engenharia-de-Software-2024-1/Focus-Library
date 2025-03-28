using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CadastroCheckFields : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Check(string username, string email, string senha, string confirmarSenha){
        if (senha != confirmarSenha)
        {
            Debug.LogError("As senhas não coincidem!");
            return false;
        }

        if (senha.Length == 0)
        {
            Debug.LogError("A senha não deve ser vazia");
            return false;
        }

        if (username.Length == 0)
        {
            Debug.LogError("O username não deve ser vazio!");
            return false;
        }

        if (email.Length == 0)
        {
            Debug.LogError("O email não deve ser vazio!");
            return false;
        }

        return true;
    }

}
