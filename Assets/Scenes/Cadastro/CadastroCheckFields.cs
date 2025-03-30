using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CadastroCheckFields
{

    public bool Check(string username, string email, string senha, string confirmarSenha){
        if (senha != confirmarSenha)
        {
            Debug.Log("As senhas não coincidem!");
            return false;
        }

        if (senha.Length == 0)
        {
            Debug.Log("A senha não deve ser vazia");
            return false;
        }

        if (username.Length == 0)
        {
            Debug.Log("O username não deve ser vazio!");
            return false;
        }

        if (email.Length == 0)
        {
            Debug.Log("O email não deve ser vazio!");
            return false;
        }

        return true;
    }

}
