public interface IProfileManagement
{
    void CreateProfileData();
    Task<int> CreateProfile(User user);
    Task<User> GetUserById(int userId); // Assuming username is unique and used to identify the user
    Task<bool> UpdateUserId(int userId, User updatedUser); // Returns true if update is successful

    Task<List<User>> ViewAllUsers();
}
