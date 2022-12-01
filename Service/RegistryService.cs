using Examine.Data.DataBase;
using Examine.Models;
using Microsoft.EntityFrameworkCore;

namespace Examine.Service;

public class RegistryService
{
    private readonly ApplicationDbContext context;

    public RegistryService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public async Task<string> RegisterAsync(RegistryViewModel model)
    {
        var presentation = model.Presentation.Split(" | ")[0];
        var count = await context.Users.CountAsync(x => x.Presentation == presentation);
        switch (presentation)
        {
            case "Языки пламени":
                if (count >= 50)
                    return "Превышен лимит участников.";
                break;
            case "Золотая лихорадка":
                if (count >= 35)
                    return "Превышен лимит участников.";
                break;
            case "Добро пожаловать на сеанс!":
                if (count >= 50)
                    return "Превышен лимит участников.";
                break;
        }

        var fullYears = (int)((DateTime.Now - model.Date).Days / 365);
        if (fullYears < 18)
            return "Вы не достигли 18 лет.";
        var regUser = new RegistredUser
        {
            Phone = model.Phone,
            Presentation = presentation,
            LastName = model.LastName,
            DateOfBirthday = model.Date,
            Key = Guid.NewGuid().ToString().Remove(5)
        };
        while (context.Users.Any(x => x.Key == regUser.Key))
            regUser.Key = Guid.NewGuid().ToString().Remove(5);
        await context.AddAsync(regUser);
        await context.SaveChangesAsync();
        return regUser.Key;
    }

    public async Task<string> UnregistryAsync(UnregisrtyViewModel model)
    {
        var user = await context.Users.FirstOrDefaultAsync(x => x.Key == model.Key);
        if (user == null)
            return "Неверные данные пользователя.";
        if (user.Phone != model.Phone || user.Presentation != model.Presentation ||
            user.LastName != model.LastName || user.DateOfBirthday != model.Date)
            return "Неверные данные пользователя.";
        context.Users.Remove(user);
        await context.SaveChangesAsync();
        return "Пользователь успешно удалён.";
    }
}