using System;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace ToggleCompleted
{
    class BBMenuItem : IGameMenuItemPlugin
    {
        private const string ToggleOn = "Mark as Completed";
        private const string ToggleOff = "Unmark as Completed";

        public bool SupportsMultipleGames => false;

        public string Caption { get; private set; }

        public System.Drawing.Image IconImage => null;

        public bool ShowInLaunchBox => false;

        public bool ShowInBigBox => true;

        public bool GetIsValidForGame(IGame selectedGame)
        {
            Caption = selectedGame.Completed ? ToggleOff : ToggleOn;
            return true;
        }

        public bool GetIsValidForGames(IGame[] selectedGames)
        {
            return false;
        }

        public void OnSelected(IGame selectedGame)
        {
            if (selectedGame.Completed.Equals(false))
            {
                selectedGame.Completed = true;
                Caption = ToggleOff;
            }
            else
            {
                selectedGame.Completed = false;
                Caption = ToggleOn;
            }

            try
            {
                Unbroken.LaunchBox.Wpf.BigBox.ViewModels.TextGamesViewModel ActiveViewModel
                    = Unbroken.LaunchBox.Wpf.BigBox.App.MainViewModel.ActiveViewModel as Unbroken.LaunchBox.Wpf.BigBox.ViewModels.TextGamesViewModel;

                ActiveViewModel.RefreshGame();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void OnSelected(IGame[] selectedGames)
        {
            return;
        }
    }
}
