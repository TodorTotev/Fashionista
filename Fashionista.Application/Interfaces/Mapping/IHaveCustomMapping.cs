namespace Fashionista.Application.Interfaces
{
    using AutoMapper;

    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile configuration);
    }
}
