using System;
using System.Collections.Generic;
using Lunch.Core.Models;
using Lunch.Core.RepositoryInterfaces;

namespace Lunch.Core.Logic.Implementations
{
    public class VetoLogic : IVetoLogic
    {
        private readonly IVetoRepository _vetoRepository;

        public VetoLogic(IVetoRepository vetoRepository)
        {
            _vetoRepository = vetoRepository;
        }


        public IEnumerable<Veto> GetAll()
        {
            return _vetoRepository.GetAll();
        }

        public IEnumerable<Veto> GetAllActive()
        {
            return _vetoRepository.GetAllActive();
        }

        public IEnumerable<Veto> GetAllActiveForUser(int userId)
        {
            return _vetoRepository.GetAllActiveForUser(userId);
        }

        public Veto Get(int id)
        {
            return _vetoRepository.Get(id);
        }

        public Veto Get(DateTime usedDate)
        {
            return _vetoRepository.Get(usedDate);
        }

        public Veto SaveOrUpdate(Veto entity)
        {
            return _vetoRepository.SaveOrUpdate(entity);
        }

        public Veto Delete(Veto entity)
        {
            return _vetoRepository.Delete(entity);
        }
    }
}