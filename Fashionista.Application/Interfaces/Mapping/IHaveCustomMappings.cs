using AutoMapper;

namespace Fashionista.Application.Interfaces.Mapping
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IProfileExpression configuration);
    }
}
