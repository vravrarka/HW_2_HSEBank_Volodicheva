using Bank.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.App.Factory
{
    public abstract class ObjectCreator
    {
        private static int _nextId = 0;
        protected int GetNextId() => ++_nextId;

        public abstract ObjectId FactoryMethod();
        public ObjectId CreateObject()
        {
            var _object = FactoryMethod();
            _object.Id = GetNextId();
            return _object;
        }
    }
}
