using TaxiBookingService.DAL.UnitOfWork.Interfaces;
using TaxiBookingService.Data.Domain;
using TaxiBookingService.Data.Models;
using TaxiBookingService.Services.Interfaces;

namespace TaxiBookingService.Services.Service
{
    public class StateService : IStateService
    {
        private readonly IUnitOfWorkState _unitOfWork;

        public StateService(IUnitOfWorkState unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<State> GetAll()
        {
            return _unitOfWork.States.GetAll(item => item.IsDeleted == false);
        }

        public List<DisplayStateDTO> GetStates()
        {
            List<DisplayStateDTO> states = new List<DisplayStateDTO>();

            List<State> existingStates = _unitOfWork.States.GetAll(item => item.IsDeleted == false);

            foreach (var state in existingStates)
            {
                DisplayStateDTO newdisplayState = new()
                {
                    Name = state.Name,
                };

                states.Add(newdisplayState);
            }

            return states;
        }

        public bool Add(StateDTO newState)
        {
            bool userExists = _unitOfWork.Users.Exists(item => item.UserName == newState.UserName);
            bool stateExists = _unitOfWork.States.Exists(item => item.Name == newState.Name);

            if (!userExists) return false;

            if (stateExists)
            {
                State existingState = _unitOfWork.States.Get(item => item.Name == newState.Name);

                if (existingState.IsDeleted == false)
                {
                    return false;
                }
            }

            User user = _unitOfWork.Users.Get(item => item.UserName == newState.UserName);

            State state = new()
            {
                Name = newState.Name,
                CreatedAt = DateTime.Now,
                IsDeleted = false,
                CreatedBy = user.Id
            };

            _unitOfWork.States.Add(state);
            _unitOfWork.Complete();

            return true;
        }

        public bool Update(StateDTO updatedState, int id)
        {
            State state = _unitOfWork.States.Get(item => item.Id == id);
            User user = _unitOfWork.Users.Get(item => item.UserName == updatedState.UserName);

            if (state == null) return false;

            state.Name = updatedState.Name;
            state.ModifiedAt = DateTime.Now;
            state.ModifiedBy = user.Id;

            _unitOfWork.States.Update(state);
            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            State state = _unitOfWork.States.Get(item => item.Id == id);

            if (state == null) return false;
            if (state.IsDeleted) return false;

            state.IsDeleted = true;

            _unitOfWork.States.Update(state);
            _unitOfWork.Complete();

            return true;
        }
    }
}
