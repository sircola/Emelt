using System;
using System.IO;

namespace SZTF112
{
    enum UserRole
    {
        // felsorolás típus
        Visitor,
        User = 10,
        Admin = 1
    }

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

            // CTRL - .   = using hozzáadás
            if (!Directory.Exists(folder) && !string.IsNullOrWhiteSpace(folder))
                Directory.CreateDirectory(folder);
        }
        public User() : this(UserRole.Visitor, string.Empty, string.Empty) { }

        public void Post(string message)
        {
            string path = Path.Combine(UserFolder, DateTime.Now.ToString("yyyyMMdd"));

            // if( UserRole == UserRole.User )

            int i = 0;
            switch (i)
            {
                case 0:
                    Console.WriteLine("nulla");
                    break;
                case 1:
                    Console.WriteLine("egy");
                    break;
                default:
                    Console.WriteLine("egyiksem");
                    break;
            }

            switch (UserRole)
            {
                case UserRole.User:
                case UserRole.Admin:
                    File.AppendAllText(path, $"[{UserRole.ToString().ToUpper()}][{DateTime.Now}] {message}\n");
                    break;
                    // case UserRole.Visitor:
                    //    break;
                    /*
                    case UserRole.User:
                        File.AppendAllLines(path, $"[USER][{DateTime.Now}] {message}\n");
                        break;
                    case UserRole.Admin:
                        File.AppendAllLines(path, $"[ADMIN][{DateTime.Now}] {message}\n");
                        break;
                    */
                    // default:
                    //    break;
            }
        }
    }

    class MyBookDB
    {
        string path, db;
        static MyBookDB myBook;

        public static MyBookDB[] Load()
        {
            MyBookDB[] x = new MyBookDB[0];
            return x;
        }

        public static MyBookDB Create()
        {
            // db-hez nem lehet hozzáférni, mert példányosítva van
            if (myBook == null)
                myBook = new MyBookDB();
            return myBook;
        }
        /*public*/
        MyBookDB() : this("mybook") { }
        /*public*/
        MyBookDB(string dbName)
        {
            db = dbName;
            if (!Directory.Exists(dbName))
                Directory.CreateDirectory(dbName);

            path = Path.Combine(dbName, "myusers.mydb");
            if (!File.Exists(path))
                CreateUser("myadmin", UserRole.Admin, "password");
        }

        public User CreateUser(string userName, UserRole userRole, string password)
        {
            File.AppendAllText(path, $"{userName}#{(int)userRole}#{password}\n");
            return new User(userRole, userName, Path.Combine(db, userName));
        }

        public bool IsUserValid(User user, string password)
        {
            return IsUserValid(user.UserName, user.UserRole, password);
        }
        public bool IsUserValid(string userName, UserRole userRole, string password)
        {
            string[] users = File.ReadAllLines(path);
            for (int i = 0; i < users.Length; i++)
            {
                string[] user = users[i].Split('#');
                if (user[0] == userName && int.Parse(user[1]) == (int)userRole && user[2] == password)
                    return true;
            }
            return false;
        }

        public User LoadUser(string userName, UserRole userRole, string password)
        {
            if (IsUserValid(userName, userRole, password))
                return new User(userRole, userName, Path.Combine(db, userName));
            return new User(UserRole.Visitor, userName, Path.Combine(db, userName));
        }
    }

    class A
    {
        public A a;
    }

    class Program
    {
        static void Main(string[] args)
        {
            // new User().UserRole = (UserRole)10;
            int role = (int)UserRole.Visitor;
            string str = UserRole.Admin.ToString();

            // MyBookDB db = new MyBookDB();
            MyBookDB db = MyBookDB.Create();
            MyBookDB.Load();

            User user1 = db.CreateUser("Béla", UserRole.Admin, "123456789");
            for (int i = 0; i < 10; i++)
            {
                user1.Post($"{i}. message...");
            }

            db = MyBookDB.Create();
            User user2 = db.CreateUser("Gizi", UserRole.User, "qwertz");
            for (int i = 0; i < 10; i++)
            {
                user2.Post($"{i}. message...");
            }

            db = MyBookDB.Create();
            User invalid = db.LoadUser("", UserRole.User, "qwertz");
            for (int i = 0; i < 10; i++)
            {
                invalid.Post($"{i}. message...");
            }
        }
    }
}
