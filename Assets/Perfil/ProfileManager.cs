using System;
using System.Collections.Generic;
using System.Linq; //exclui quando tirar os testes

public class Perfil
{
    public Guid UserId { get; private set; } // Identificador único o guid gera um numero unico para cada usuario
    public string NomeCompleto { get; set; } 
    public string Username { get; set; } // também poder ser usado para o login
    public string Email { get; set; }
    public DateTime DataNascimento { get; set; } // para usar o formato de data
    public string Senha { get; private set; } // alterar depois para ocultar a senha
    public string FotoPerfil { get; set; } // Caminho ou URL da imagem implementar depois o pegar imagen do sistema

    // Construtor usado no cadastro
    public Perfil(string username, string email, string senha)
    {
        if (string.IsNullOrWhiteSpace(username) || username.Trim().Length <= 3)
            throw new ArgumentException("O nome de usuário deve conter mais de 3 caracteres.");
        
        if (string.IsNullOrWhiteSpace(senha) || senha.Length < 6)
            throw new ArgumentException("A senha deve conter no mínimo 6 caracteres.");

        UserId = Guid.NewGuid();
        Username = username.Trim();
        Email = email;
        Senha = senha;
    }

    
    public void AtualizarPerfil(string nomeCompleto, string novoUsername, string novoEmail, DateTime dataNascimento, string fotoPerfil)
    {
        if (string.IsNullOrWhiteSpace(novoUsername) || novoUsername.Trim().Length <= 3)
            throw new ArgumentException("O nome de usuário deve conter mais de 3 caracteres.");

        NomeCompleto = nomeCompleto;
        Username = novoUsername.Trim();
        Email = novoEmail;
        DataNascimento = dataNascimento;
        FotoPerfil = fotoPerfil;
    }

    // Método para alterar a senha ver dps como deixa privado
    public void AlterarSenha(string novaSenha)
    {
        if (string.IsNullOrWhiteSpace(novaSenha) || novaSenha.Length < 6)
            throw new ArgumentException("A senha deve conter no mínimo 6 caracteres.");

        Senha = novaSenha;
    }
}

// usado apenas para testes lembrar de excluir depois
 public class GerenciadorPerfis
{
    private List<Perfil> perfis = new List<Perfil>();

    public Perfil CadastrarPerfil(string username, string email, string senha)
    {
        Perfil novoPerfil = new Perfil(username, email, senha);
        perfis.Add(novoPerfil);
        return novoPerfil;
    }

    public Perfil BuscarPerfil(Guid userId)
    {
        return perfis.FirstOrDefault(p => p.UserId == userId);
    }

    public void SimularBancoDeDados()
    {
        foreach (var perfil in perfis)
        {
            Console.WriteLine($"INSERT INTO Perfis (UserId, NomeCompleto, Username, Email, DataNascimento, Senha, FotoPerfil) " +
                              $"VALUES ('{perfil.UserId}', '{perfil.NomeCompleto}', '{perfil.Username}', '{perfil.Email}', " +
                              $"'{perfil.DataNascimento}', '{perfil.Senha}', '{perfil.FotoPerfil}');");
        }
    }
} 


