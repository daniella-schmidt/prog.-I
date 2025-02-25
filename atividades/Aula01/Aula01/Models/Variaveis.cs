namespace Aula1.Models
{
    public class Variaveis
    {
        // Tipos implicitos
        // var teste = 10;

        // Anotação de Tipos
        public int userCount = 10;

        // uma variavel pode ser declarada e não inicializada
        public int totalCount;

        //CONSTANTES
        // para declarar utilizamos a palavra-chave CONST
        // no enntanto deve ser inicializada quando declarada
        public const int interestRate = 10;

        // O método construtor é invocado na instanciação do objeto por meio da palavra reservada new
        // Por regra, o construtor sempre tem o mesmo nome da classete

        //Atividades

        public byte MinValue = 0;
        public byte MaxValue = 255;
        public short MiniValue = -32768;
        public short MaxiValue = 32767;
        public int MinimValue = -2147483648;
        public int MaximValue = 2147483647;
        public uint MinimoValue = 0;
        public uint MaximoValue = 4294967295;
        public long MiniiValue = -9223372036854775808;
        public long MaxiiValue = 9223372036854775807;
            
        public Variaveis()
        {
            totalCount = 0;

            // Tipo implícito
            //palavra chave var se engarrega  de definir o tipo da variavel na instrução de atribuição
            var signalStrength = 20;
            var companyName = "ACME";
        }
    }
}
