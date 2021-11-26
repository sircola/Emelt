using System;
using System.IO;

namespace Ora12
{
    class Program
    {
        static void Main(string[] args)
        {
            MyBookDb db = new MyBookDb();
            User béla = db.CreateUser("Béla", UserRole.Admin, "123456");
            for (int i = 0; i < 10; i++)
                béla.Post($"Béla {i}. post...");

            User gizi = db.CreateUser("Gizi", UserRole.User, "qwerty");
            for (int i = 0; i < 10; i++)
                gizi.Post($"Gizi {i}. post...");

            User admin = db.LoadUser("myadmin", UserRole.Admin, "password");
            for (int i = 0; i < 10; i++)
                admin.Post($"Admin {i}. post...");

            User invalid = db.LoadUser("", UserRole.Admin, "");
            for (int i = 0; i < 10; i++)
                invalid.Post($"Invalid {i}. post...");
        }
    }
    enum UserRole { Visitor, User, Admin }
    class User
    {
        public UserRole UserRole { get; private set; }
        public string UserName { get; private set; }
        public string UserFolder { get; private set; }

        public User(UserRole role, string name) : this(role, name, "") { }
        public User(UserRole role, string name, string folder)
        {
            UserRole = role;
            UserName = name;
            UserFolder = folder;

            if (!string.IsNullOrWhiteSpace(UserFolder) && !Directory.Exists(UserFolder))
                Directory.CreateDirectory(UserFolder);
        }

        public void Post(string message)
        {
            string path = Path.Combine(UserFolder, DateTime.Now.ToString("yyyy-MM-dd"));
            switch (UserRole)
            {
                //case UserRole.User:
                //    File.AppendAllText(path, $"[USER][{DateTime.Now}] {message}\n");
                //    break;
                //case UserRole.Admin:
                //    File.AppendAllText(path, $"[ADMIN][{DateTime.Now}] {message}\n");
                //    break;
                case UserRole.User:
                case UserRole.Admin:
                    File.AppendAllText(path, $"[{UserRole.ToString().ToUpper()}][{DateTime.Now}] {message}\n");
                    break;
            }
        }
    }
    class MyBookDb
    {
        string db, path;

        public MyBookDb() : this("mybook") { }
        public MyBookDb(string dbName)
        {
            db = dbName;
            if (!Directory.Exists(db))
                Directory.CreateDirectory(db);

            path = Path.Combine(db, "myusers.mydb");
            if (!File.Exists(path))
                CreateUser("myadmin", UserRole.Admin, "password");
        }
        public User CreateUser(string name, UserRole role, string password)
        {
            File.AppendAllText(path, $"{name}#{(int)role}#{password}\n");
            return new User(role, name, Path.Combine(db, name));
        }
        public bool IsValidUser(User user, string password)
        {
            return IsValidUser(user.UserName, user.UserRole, password);
        }
        public bool IsValidUser(string name, UserRole role, string password)
        {
            string[] users = File.ReadAllLines(path);
            for (int i = 0; i < users.Length; i++)
            {
                string[] user = users[i].Split('#');
                if (user[0] == name && int.Parse(user[1]) == (int)role && user[2] == password)
                    return true;
            }
            return false;
        }
        public User LoadUser(string name, UserRole role, string password)
        {
            if (IsValidUser(name, role, password))
                return new User(role, name, Path.Combine(db, name));
            return new User(UserRole.Visitor, name, Path.Combine(db, name));
        }
    }
}
