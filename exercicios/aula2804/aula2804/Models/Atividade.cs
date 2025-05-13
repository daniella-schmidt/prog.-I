using System;
using System.Collections.Generic;

namespace aula2804.Models
{
    // Classe que representa um Animal
    public class Animal
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Especie { get; private set; }
        public string Raca { get; private set; }
        public Tutor Dono { get; private set; }

        public Animal(int id, string nome, string especie, string raca, Tutor dono)
        {
            Id = id;
            Nome = nome;
            Especie = especie;
            Raca = raca;
            Dono = dono;
        }
    }

    // Classe que representa um Tutor
    public class Tutor
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Telefone { get; private set; }
        public List<Animal> Animals { get; private set; }

        public Tutor(int id, string nome, string telefone)
        {
            Id = id;
            Nome = nome;
            Telefone = telefone;
            Animals = new List<Animal>();
        }

        public void AdicionarAnimal(Animal animal)
        {
            Animals.Add(animal);
        }

        public void AtualizarTelefone(string novoTelefone)
        {
            Telefone = novoTelefone;
        }
    }

    // Classe que representa um Veterinário
    public class Veterinario
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Especialidade { get; private set; }
        public int CRM { get; private set; }

        public Veterinario(int id, string nome, string especialidade, int crm)
        {
            Id = id;
            Nome = nome;
            Especialidade = especialidade;
            CRM = crm;
        }

        public void AtualizarEspecialidade(string novaEspecialidade)
        {
            Especialidade = novaEspecialidade;
        }
    }

    // Classe que representa um Parceiro
    public class Parceiro
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Endereco { get; private set; }

        public Parceiro(int id, string nome, string endereco)
        {
            Id = id;
            Nome = nome;
            Endereco = endereco;
        }
    }

    // Classe que representa um Atendimento
    public class Atendimento
    {
        public int Id { get; private set; }
        public Animal AnimalAtendido { get; private set; }
        public Clinica Clinica { get; private set; }
        public Veterinario MedicoResponsavel { get; private set; }
        public DateTime Data { get; private set; }
        public List<Procedimento> Procedimentos { get; private set; }
        public Pagamento PagamentoAtendimento { get; private set; }

        public Atendimento(int id, Animal animalAtendido, Clinica clinica, Veterinario medicoResponsavel, DateTime data)
        {
            Id = id;
            AnimalAtendido = animalAtendido;
            Clinica = clinica;
            MedicoResponsavel = medicoResponsavel;
            Data = data;
            Procedimentos = new List<Procedimento>();
        }

        public void AdicionarProcedimento(Procedimento procedimento)
        {
            Procedimentos.Add(procedimento);
        }

        public void AtualizarData(DateTime novaData)
        {
            Data = novaData;
        }
    }

    // Classe que representa um Procedimento
    public class Procedimento
    {
        public int Id { get; private set; }
        public string Descricao { get; private set; }
        public decimal Custo { get; private set; }

        public Procedimento(int id, string descricao, decimal custo)
        {
            Id = id;
            Descricao = descricao;
            Custo = custo;
        }
    }

    // Classe que representa um Pagamento
    public class Pagamento
    {
        public int Id { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataPagamento { get; private set; }
        public string MetodoPagamento { get; private set; }

        public Pagamento(int id, decimal valor, DateTime dataPagamento, string metodoPagamento)
        {
            Id = id;
            Valor = valor;
            DataPagamento = dataPagamento;
            MetodoPagamento = metodoPagamento;
        }
    }

    // Classe que representa uma Clínica
    public class Clinica
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }

        public Clinica(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }

    // Classe que representa uma Especialidade
    public class Especialidade
    {
        public string Tipo { get; private set; }

        public Especialidade(string tipo)
        {
            Tipo = tipo;
        }
    }
}