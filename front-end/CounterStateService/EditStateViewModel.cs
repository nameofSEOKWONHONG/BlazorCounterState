using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CounterState
{
    public interface IEditStateViewModel : IViewModelBase
    {
        IUserStateViewModel SelectedUser { get; set; }
        IList<IUserStateViewModel> Users { get; }
        void Save(IUserStateViewModel user);
        void Clear();
        void Delete(string name);
    }
    public class EditStateViewModel : ViewModelBase, IEditStateViewModel
    {
        private IUserStateViewModel _selectedUser = new UserStateViewModel();

        public IUserStateViewModel SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
            }
        }
        
        private List<IUserStateViewModel> _users = new List<IUserStateViewModel>()
        {
            new UserStateViewModel() { Name = "test1", Age = 10},
            new UserStateViewModel() { Name = "test2", Age = 20},
            new UserStateViewModel() { Name = "test3", Age = 30},
        };

        public IList<IUserStateViewModel> Users
        {
            get => _users;
        }

        public void Save(IUserStateViewModel user)
        {
            var exists = _users.FirstOrDefault(m => m.Name == user.Name);
            if (exists == null)
            {
                _users.Add(user);
                OnPropertyChanged("Users");                
            }
        }

        public void Delete(string name)
        {
            var exists = _users.FirstOrDefault(m => m.Name == name);
            if (exists != null)
            {
                _users.Remove(exists);
                OnPropertyChanged("Users");
            }
        }

        public void Clear()
        {
            _users.Clear();
        }

        public override void Dispose()
        {
            
        }
    }

    public interface IUserStateViewModel : IViewModelBase
    {
        string Name { get; set; }
        int Age { get; set; }
    }

    public class UserStateViewModel : ViewModelBase, IUserStateViewModel
    {
        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private int _age;

        public int Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }

        public override void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}