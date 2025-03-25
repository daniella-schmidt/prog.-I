namespace Atividade1703.Models
{
    public class AtividadeModel
    {
        // Propriedade para armazenar o valor inteiro a ser convertido
        public int Valor { get; set; }

        // Método que retorna o número por extenso
        public string PorExtenso()
        {
            if (Valor < 0 || Valor > 9999)
                return "Número fora do intervalo suportado.";

            return new NumeroPorExtenso().Converter(Valor);
        }
    }

    public class NumeroPorExtenso
    {
        private readonly string[] unidades = {
            "zero", "um", "dois", "três", "quatro", "cinco", "seis", "sete", "oito", "nove",
            "dez", "onze", "doze", "treze", "quatorze", "quinze", "dezesseis", "dezessete",
            "dezoito", "dezenove"
        };

        private readonly string[] dezenas = {
            "", "", "vinte", "trinta", "quarenta", "cinquenta", "sessenta", "setenta",
            "oitenta", "noventa"
        };

        private readonly string[] centenas = {
            "", "cem", "duzentos", "trezentos", "quatrocentos", "quinhentos", "seiscentos",
            "setecentos", "oitocentos", "novecentos"
        };

        // Método que converte o número inteiro para texto
        public string Converter(int numero)
        {
            // Caso especial: se o número for 0
            if (numero == 0)
                return unidades[0];

            string extenso = "";
            int milhar = numero / 1000; // Calcula quantos milhares existem
            int resto = numero % 1000; // Calcula o restante após os milhares

            // Adiciona a parte dos milhares, tratando singular e plural
            if (milhar > 0)
                extenso += (milhar == 1 ? "mil" : $"{unidades[milhar]} mil") + " ";

            // Se houver um valor restante, converte as centenas
            if (resto > 0)
                extenso += ConverterCentenas(resto);

            return extenso.Trim();
        }

        private string ConverterCentenas(int numero)
        {
            // Caso especial: se o número for 100
            if (numero == 100)
                return "cem";

            int centena = numero / 100; // Calcula a parte da centena
            int resto = numero % 100; // Calcula o restante após as centenas
            string extenso = "";

            // Adiciona a centena correspondente e conecta com "e", se houver resto
            if (centena > 0)
                extenso += centenas[centena] + (resto > 0 ? " e " : "");

            if (resto > 0)
                extenso += ConverterDezenas(resto);

            return extenso;
        }

        private string ConverterDezenas(int numero)
        {
            // Para números menores que 20, retorna diretamente do array de unidades
            if (numero < 20)
                return unidades[numero];

            int dezena = numero / 10; // Calcula a parte das dezenas
            int unidade = numero % 10; // Calcula a parte das unidades

            // Adiciona a dezena correspondente e conecta com "e", se houver unidade
            return dezenas[dezena] + (unidade > 0 ? " e " + unidades[unidade] : "");
        }
    }
}
