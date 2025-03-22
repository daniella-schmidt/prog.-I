namespace Aula02.Models
{
    public class TypeCasting
    {
        //declarando variaveis na classe
        public int myInterger = 20;
        public long myLong;

        public string myType1;
        public string myType2;
        public TypeCasting() {
            //conversão implicita de tipos
            myLong = myInterger;
            //conersão explcita de tipos
            long myLong2 = 138459620;
            int myInterger2;
            myInterger2 = (int)myLong2;

            //é possivel identificar o tipo de variavel em tempo de execução
            myType1 = myLong2.GetType().ToString();
            myType2 = myInterger2.GetType().ToString();
        }
    }
}
