using Grasshoppers.Models;
using Grasshoppers.Services;

namespace Grasshoppers.ViewModels
{
    public class PositionsViewModel : ComponentsViewModel<Position, PositionsServices>
    {
        public int NewComponentNumberOfHours
        {
            get { return NewComponent.NumberOfHours; }
            set
            {
                if (NewComponent.NumberOfHours != value)
                {
                    NewComponent.NumberOfHours = value;
                    OnPropertyChanged();
                }
            }
        }

        public int SelectedComponentNumberOfHours
        {
            get { return SelectedComponent.NumberOfHours; }
            set
            {
                if (SelectedComponent.NumberOfHours != value)
                {
                    SelectedComponent.NumberOfHours = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
