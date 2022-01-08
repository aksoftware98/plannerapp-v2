using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace PlannerApp.Shared
{
    public partial class MainLayout
    {

        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        bool _drawerOpen = true;
        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        protected async override Task OnInitializedAsync()
        {
            if (await LocalStorage.ContainKeyAsync("theme"))
                _themeName = await LocalStorage.GetItemAsStringAsync("theme");
            else
                _themeName = "light";

            _currentTheme = _themeName == "light" ? _lightTheme : _darkTheme;
        }

        MudTheme _currentTheme = null;

        private string _themeName = "light";

        MudTheme _darkTheme = new MudTheme
        {
            Palette = new Palette
            {
                AppbarBackground = "#0097FF",
                AppbarText = "#FFFFFF",
                Primary = "#007CD1",
                TextPrimary = "#FFFFFF",
                Background = "#001524",
                TextSecondary = "#E2EEF6",
                DrawerBackground = "#093958",
                DrawerText = "#FFFFFF",
                Surface = "#093958",
                ActionDefault = "#0C1217",
                ActionDisabled = "#2F678C",
                TextDisabled = "#B0B0B0",
            }
        };

        MudTheme _lightTheme = new MudTheme
        {
            Palette = new Palette
            {
                AppbarBackground = "#0097FF",
                AppbarText = "#FFFFFF",
                Primary = "#007CD1",
                TextPrimary = "#0C1217",
                Background = "#F4FDFF",
                TextSecondary = "#0C1217",
                DrawerBackground = "#C5E5FF",
                DrawerText = "#0C1217",
                Surface = "#E4FAFF",
                ActionDefault = "#0C1217",
                ActionDisabled = "#2F678C",
                TextDisabled = "#676767",
            }
        };

        private async Task ChangeThemeAsync()
        {
            if (_themeName == "light")
            {
                _currentTheme = _darkTheme;
                _themeName = "dark"; 
            }
            else
            {
                _currentTheme = _lightTheme;
                _themeName = "light";
            }

            await LocalStorage.SetItemAsStringAsync("theme", _themeName);
        }

    }
}