namespace Layers.Infrastructure.DbModels.MappingExtensions;

public static class UserMapping
{
    /// <summary> Смаппить пользователя из домена для БД </summary>
    /// <param name="domainUser"> Модель из Domains</param>
    public static User ToDbUser(this TestsExample.Models.User domainUser)
    {
        return new()
        {
            Id = domainUser.Id,
            Name = domainUser.Name,
            Password = domainUser.Password
        };
    }

    /// <summary> Смаппить пользователя из БД в пользователя из домена </summary>
    /// <returns> Пользователь из доменного слоя </returns>
    public static TestsExample.Models.User ToDomainUser(this User user)
    {
        return new()
        {
            Name = user.Name,
            Password = user.Password,
            Id = user.Id
        };
    }
}