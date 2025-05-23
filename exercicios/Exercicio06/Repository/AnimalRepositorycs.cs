using Model;

namespace Repository
{
    public class AnimalRepositorycs
    {
        public AnimalRepositorycs() { }

        public Animal Retrieve()
        {
            // Você precisa fornecer os parâmetros obrigatórios
            TypeAnimal defaultType = new TypeAnimal("Desconhecido");
            return new Animal(
                "[Nome não encontrado]",
                DateTime.MinValue, // Data padrão
                defaultType
            );
        }
        public void Save(Animal customer)
        {

        }
    }
}
