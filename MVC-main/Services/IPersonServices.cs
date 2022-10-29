using MVCAssigmentDay2.Models;
namespace MVCAssigmentDay2.Services
{
    public interface IPersonServices
    {
        List<PersonModel> GetAll();
        PersonModel? GetOne(int index);
        PersonModel Create(PersonModel model);
        PersonModel? Update(int index, PersonModel model);
        PersonModel? Delete(int index);
    }
}