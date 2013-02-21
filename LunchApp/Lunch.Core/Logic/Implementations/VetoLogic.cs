using System;
using System.Collections.Generic;
using System.Linq;
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

        public Veto GetUsedTodayForUser(int userid)
        {
            return
                GetAll().FirstOrDefault(f => f.UserId == userid && f.Used && f.UsedAt != null && f.UsedAt.Value.ToShortDateString() == Helpers.AdjustTimeOffsetFromUtc(DateTime.UtcNow).ToShortDateString());
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