using AutoMapper;

namespace PayBordless;

public interface IMapFrom<T>
{
    void Mapping(Profile mapper) => mapper.CreateMap(typeof(T), this.GetType());
}