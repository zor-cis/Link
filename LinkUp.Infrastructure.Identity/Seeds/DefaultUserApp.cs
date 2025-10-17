using Microsoft.AspNetCore.Identity;

namespace LinkUp.Infrastructure.Identity.Seeds
{
    public static class DefaultUserApp
    {
        public static async Task SendAsync(UserManager<AppUser> userManager) 
        {
            AppUser user = new()
            {
                Name = "John",
                LastName = "Doe",
                Email = "useremail@email.com",
                EmailConfirmed = true,
                UserName = $"user_name",
                PhoneNumber = "000-000-0000",
                ProfileImage = "",
                IsActive = false
            };

            if (userManager.Users.All(u => u.Id != user.Id))
            {
                var entityUserEmail = await userManager.FindByEmailAsync(user.Email);
                var entityUserName = await userManager.FindByNameAsync(user.UserName);
                if (entityUserEmail == null && entityUserName == null) 
                { 
                    await userManager.CreateAsync(user, "123Pa$$word!");
                }

            }
        }
    }
}
