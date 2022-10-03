using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using static SeaBattle.BattleFieldCell;
using System.Windows.Input;

namespace SeaBattle
{
    public class AppViewModel : INotifyPropertyChanged
    {
        private BattleFieldCell selectedBattleFieldCell;
        private ObservableCollection<BattleFieldCell> battlefieldcells = new ObservableCollection<BattleFieldCell>();
        public ObservableCollection<BattleFieldCell> BattleFieldCells { get => battlefieldcells; set => battlefieldcells = value; }

        Random rand = new Random();
        private int amountofships;
        private int battlefieldsize;
        //private bool stillalive;
        private int usermovecounter = 0; // Счетчик ходов

        public int UserMoveCounter
        {
            get
            {
                return usermovecounter;
            }
            set
            {
                OnPropertyChanged("Content");
                usermovecounter = value;
            }
        }

        public string Content
        {
            get
            {
                return Convert.ToString(usermovecounter);
            }
        }

        public int AmountOfShips { get { return amountofships; } set { amountofships = value; } }
        public int BattleFieldSize { get { return battlefieldsize; } set { battlefieldsize = value; } }

        public AppViewModel()
        {
            //battlefieldcreate();
        }

        //public bool StillAlive
        //{
        //    get { return this.stillalive; }
        //    set
        //    {
        //        this.stillalive = value;
        //        OnPropertyChanged("Color");
        //        OnPropertyChanged("IsEnabled");
        //        OnPropertyChanged("StillAlive");
        //    }
        //}

        //private void battlefieldcreate()
        //{
        //    bool isshiplocatedhere;
        //    int randomhere;
        //    for (int x = 0; x < BattleFieldSize; x++)
        //    {
        //        for (int y = 0; y < BattleFieldSize; y++)
        //        {
        //            //while (amount != 0)
        //            //{
        //            randomhere = rand.Next(0, 20);
        //            if (randomhere > 15) { isshiplocatedhere = true; AmountOfShips--; }
        //            else { isshiplocatedhere = false; }
        //            BattleFieldCells.Add(new BattleFieldCell(isshiplocatedhere, true, x, y));
        //            //}
        //        }
        //    }
        //}

        //public ICommand Command 
        //{
        //    get
        //    {
        //        return Command;
        //    }
        //    set
        //    {
        //        Command = 0;
        //    }
        //}

        public BattleFieldCell SelectedBattleFieldCell //getset property=SelectedBattleFieldCell
        {
            get
            {
                return this.selectedBattleFieldCell;
            }

            set
            {
                this.selectedBattleFieldCell = value;
                OnPropertyChanged("SelectedBattleFieldCell");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged; // notify
        public void OnPropertyChanged([CallerMemberName] string prop = "") // notify bout propertychanges
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
