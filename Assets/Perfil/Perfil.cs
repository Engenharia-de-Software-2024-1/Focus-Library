using System;
using UnityEngine;

public class Perfil
{
    public string Id { get; set; } 
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime? DataNascimento { get; set; } // Alterado para permitir valores nulos
    public string Senha { get; private set; }
   
<<<<<<< HEAD:Assets/Perfil/Perfil.cs
    // Construtor com inicialização de DataNascimento como nulo
=======

    // Construtor com inicialização de DataNascimento
>>>>>>> 15b7abe (falta a logica de ober o perfil):Assets/Cadastro/Perfil.cs
    public Perfil(string username, string email, string senha)
    {   
        if (string.IsNullOrWhiteSpace(username) || username.Trim().Length <= 3)
            throw new ArgumentException("O nome de usuário deve conter mais de 3 caracteres.");
        
        if (string.IsNullOrWhiteSpace(senha) || senha.Length < 6)
            throw new ArgumentException("A senha deve conter no mínimo 6 caracteres.");

        Username = username.Trim();
        Email = email;
        Senha = senha;
<<<<<<< HEAD:Assets/Perfil/Perfil.cs
        DataNascimento = null; // Data inicialmente nula
    }

    // Método de atualização modificado para aceitar data de nascimento nula
    public void AtualizarPerfil(string novoUsername, string novoEmail, DateTime? dataNascimento)
=======
        DataNascimento = DateTime.Now; // Data padrão: data atual
        
    }

    // Método de atualização
    public void AtualizarPerfil(string novoUsername, string novoEmail, DateTime dataNascimento)
>>>>>>> 15b7abe (falta a logica de ober o perfil):Assets/Cadastro/Perfil.cs
    {
        if (string.IsNullOrWhiteSpace(novoUsername) || novoUsername.Trim().Length <= 3)
            throw new ArgumentException("O nome de usuário deve conter mais de 3 caracteres.");

        Username = novoUsername.Trim();
        Email = novoEmail;
        DataNascimento = dataNascimento;
<<<<<<< HEAD:Assets/Perfil/Perfil.cs
=======
       
>>>>>>> 15b7abe (falta a logica de ober o perfil):Assets/Cadastro/Perfil.cs
    }
}