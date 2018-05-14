using Grasshoppers.Helpers;
using Grasshoppers.Interfaces;
using Grasshoppers.Models;
using Grasshoppers.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Grasshoppers.ViewModels
{
    public class ComponentsViewModel<T, U> : INotifyPropertyChanged
         where T : Component, new()
         where U : IComponentsServices<T>, new()

    {
        private ObservableCollection<T> _allComponents;
        private T _newComponent = new T
        {
            Name = ""
        };
        
        public ComponentsViewModel()
        {
            _isBusy = false;
        }

        public bool IsAdmin => Settings.AdminLevel == 1;

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                IsNotBusy = !_isBusy;
                OnPropertyChanged();
            }
        }

        public bool IsNotBusy
        {
            get { return !_isBusy; }
            set
            {
                OnPropertyChanged();
            }
        }

        public INavigation Navigation { private get; set; }

        public async Task InitializeAllComponentsAsync()
        {
            IsBusy = true;
            var componentServices = new U();
            AllComponents = await componentServices.GetAllComponentsAsync();
            IsBusy = false;
        }

        private T _selectedComponent;
        public T SelectedComponent
        {
            get { return _selectedComponent; }
            set
            {
                if (_selectedComponent != value) // only used when value is changed, not when just set
                {
                    _selectedComponent = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<T> AllComponents
        {
            get { return _allComponents; }
            set
            {
                if (_allComponents != value) // only used when value is changed, not when just set
                {
                    _allComponents = value;
                    OnPropertyChanged();
                }
            }
        }

        public T NewComponent
        {
            get { return _newComponent; }
            set
            {
                if (_newComponent != value) // only used when value is changed, not when just set
                {
                    _newComponent = value;
                    OnPropertyChanged();
                }
            }
        }

        //volane z EditComponentsPage
        public async Task<bool> DeleteComponentAsync(T component)
        {
            IsBusy = true;
            var componentsServices = new U();
            var success = await componentsServices.DeleteComponentAsync(component.Id);
            IsBusy = false;

            return success;
        }

        //volane z EditComponentsPage
        public async Task<bool> AddComponentAsync(T newComponent)
        {
            IsBusy = true;
            var componentsServices = new U();
            var success = await componentsServices.PostComponentAsync(newComponent);
            IsBusy = false;

            return success;
        }

        //bindnute v EditComponentPage kde upravujem vybraty komponent
        public Command EditComponentCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var componentsServices = new U();

                    if (_selectedComponent.Name == null || _selectedComponent.Name == "")
                    {
                        DependencyService.Get<IMessage>().LongAlert("Zadajte názov");
                    }

                    IsBusy = true;
                    if (await componentsServices.PutComponentAsync(_selectedComponent.Id, _selectedComponent))
                    {
                        DependencyService.Get<IMessage>().LongAlert("Uložené");
                    }
                    else
                    {
                        DependencyService.Get<IMessage>().LongAlert("Vyskytla sa chyba. Skúste to znova");
                    }
                    await Navigation.PopAsync();
                    IsBusy = false;
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
