namespace Aula02.Models
{
    public class DataType
    {
        public char myVar = 'a';
        public char myConst = 'b';

        public char myChar1 = 'f';
        public char myChar2 = ':';
        public char myChar3 = 'x';
        //podemos tbm atribuir referencia de caracteres UniCode
        public char myChar4 = '\u2726';
        //podemos ainda mesclar caracteres especiais, como: nova linha, tabulação, etc.
        public string TextLine = "Esta é uma linha de texto.\n\n\n E esta é outra linha";

        /*
            \e - caracter de escape
            \n - new line
            \r - retorno
            \t - tabulação orizontal
            \" - usadas para exibir aspas dentro da string
            \' - usados para exibir aspas simples dentro da string
         */

        public int count = 10;
        public string message;
        public DataType() {
            //interpolação de strings, combinado string de diferentes maneiras
            message = $"o contador esta em {count}";
            string username = "Calíope";
            int inboxCount = 10;
            int MaxCount = 100;

            message += $"\nusuario {username} tem {inboxCount} mensagens";
            message += $"\nespaço restante em sua caixa {MaxCount - inboxCount}";

        }
    }
}
