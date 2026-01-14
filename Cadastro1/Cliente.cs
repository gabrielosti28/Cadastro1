// =============================================
// CLASSE MODELO CLIENTE
// Arquivo: Cliente.cs
// =============================================
using System;

public class Cliente
{
    public int ClienteID { get; set; }
    public string NomeCompleto { get; set; }
    public string CPF { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Endereco { get; set; }
    public string Cidade { get; set; }
    public string BeneficioINSS { get; set; }
    public DateTime DataCadastro { get; set; }
    public bool Ativo { get; set; }
}