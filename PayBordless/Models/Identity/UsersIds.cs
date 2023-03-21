using AutoMapper;
using PayBordless.Data.Models;

namespace PayBordless.Models.Identity;

public class UsersIds : IMapFrom<ApplicationUser>
{
    public string Id { get; set; }
    public string UserName { get; set; }
    
    public virtual void Mapping(Profile mapper)
        => mapper
            .CreateMap<ApplicationUser, UsersIds>();
}