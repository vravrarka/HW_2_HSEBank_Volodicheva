using Bank.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.App.Repositories
{
    public class BankAccountRepositoryProxy : IBankAccountRepository
    {
        private readonly DatabaseBankAccountRepository _realRepository;
        private readonly Dictionary<int, BankAccount> _cache;
        private IEnumerable<BankAccount> _allAccountsCache;
        private bool _allAccountsLoaded = false;
        public BankAccountRepositoryProxy()
        {
            _realRepository = new DatabaseBankAccountRepository();
            _cache = new Dictionary<int, BankAccount>();
        }
        public BankAccount GetById(int id)
        {
            if (_cache.ContainsKey(id))
            {
                return _cache[id];
            }
            var account = _realRepository.GetById(id);
            _cache[id] = account;
            return account;
        }
        public IEnumerable<BankAccount> GetAll()
        {
            if (_allAccountsLoaded && _allAccountsCache != null)
            {
                return _allAccountsCache;
            }
            _allAccountsCache = _realRepository.GetAll();
            _allAccountsLoaded = true;
            foreach (var account in _allAccountsCache)
            {
                _cache[account.Id] = account;
            }
            return _allAccountsCache;
        }
        public void Save(BankAccount account)
        {
            _realRepository.Save(account);
            _cache[account.Id] = account;
            _allAccountsLoaded = false; 
        }
        public void Delete(int id)
        {
            _realRepository.Delete(id);
            _cache.Remove(id);
            _allAccountsLoaded = false; 
        }
        public void PreloadCache()
        {
            GetAll(); 
        }
    }
}
