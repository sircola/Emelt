using System;
using System.Collections.Generic;
using System.IO;

namespace SZTF210het
{
    class BankHashSet<K,T>
    {
        private class BankHashSetItem
        {
            public K key;
            public T content;
        }

        public delegate int HashCallback(K key, int size);

        private static int DefualtHashing( K key, int size )
        {
            return Math.Abs(key.GetHashCode()) % size;
        }

        private List<BankHashSetItem>[] _contents;
        private int _size { get; set; }
        private HashCallback HashFunction;

        public BankHashSet(int size, HashCallback hasitofv)
        {
            _size = size;
            _contents = new List<BankHashSetItem>[_size];
            for (int i = 0; i < _contents.Length; i++)
            {
                _contents[i] = new List<BankHashSetItem>();
            }
            if (hasitofv == null)
                HashFunction = DefualtHashing;
            else
                HashFunction = hasitofv;
        }

        public BankHashSet()
            : this(100,DefualtHashing)
        {
        }

        public void Insert( K key, T content )
        {
            int index = HashFunction(key,_size);
            _contents[index].Add(new BankHashSetItem()
            {
                key = key,
                content = content
            });
        }

        public T Find( K key )
        {
            int index = HashFunction(key, _size);
            int i = 0;
            while( i < _contents[index].Count && !_contents[index][i].key.Equals(key) )
            {
                ++i;
            }
            if (i < _contents[index].Count)
                return _contents[index][i].content;
            else
                throw new ArgumentException("Nincs ilyen kulcsú elem.");
        }

        public T this[K key] 
        {
            get 
            {
                return Find(key);
            }
            set 
            {
                Insert(key, value);
            }
        }
    }   
    
    class BankAccount
    {
        public string AccountNumber { get; private set; } 
        public double Money { get; private set; }

        public BankAccount(string s, double m)
        {
            AccountNumber = s;
            Money = m;
        }

        public void Deposit( double m )
        {
            Money += m;
        }

        public void Withdraw( double m )
        {
            Money -= m;
        }
    }

    class Bank
    {
        private BankHashSet<string, BankAccount> _accounts;
        private List<string> simaliba = new List<string>();

        public Bank(int size, BankHashSet<string,BankAccount>.HashCallback h)
        {
            _accounts = new BankHashSet<string, BankAccount>(size,h);
        }

        public Bank()
        {
            _accounts = new BankHashSet<string, BankAccount>();
        }

        public void Transaction(string from, string to, double amount)
        {
            _accounts[from].Withdraw(amount);
            _accounts[to].Deposit(amount);
        }

        public void RegisterAccount(string AccountNumber, double deposit)
        {
            _accounts[AccountNumber] = new BankAccount(AccountNumber,deposit);
            simaliba.Add(AccountNumber);
        }

        public BankAccount SimaLista( int i )
        {
            return _accounts[simaliba[i]];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();
            StreamReader sr = new StreamReader("bank_log.txt");

            while (!sr.EndOfStream)
            {
                string l = sr.ReadLine();
                if (l.Length < 2)
                    break;
                string[] a = l.Split(',');
                bank.RegisterAccount(a[0], double.Parse(a[1]));
            }

            while (!sr.EndOfStream)
            {
                string l = sr.ReadLine();
                string[] a = l.Split(',');
                bank.Transaction(a[0], a[1], double.Parse(a[2]));
            }

            sr.Close();

            int i = 0;
            bool tovább = true;
            while( tovább )
            {
                try
                {
                    BankAccount a = bank.SimaLista(i++);
                    Console.WriteLine($"{a.AccountNumber}: {a.Money}");
                }
                catch(ArgumentException)
                {
                    tovább = false;
                    Console.WriteLine($"{--i} db fiók volt.");
                }
            }
        }
    }
}
