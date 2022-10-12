using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows;

namespace SeaBattle
{
    public class AppViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<BattleFieldCell> BattleField { get; set; } = new ObservableCollection<BattleFieldCell>();
        private readonly Random rand = new Random();
        private bool enabled; // Включение кнопки старта игры
        private int usermovecounter = 0; // Счетчик ходов
        private int amountofships = 0; // Количество кораблей на поле
        public int UserMoveCounter
        {
            get => usermovecounter;
            set
            {
                usermovecounter = value;
                OnPropertyChanged("Content");
            }
        }
        public int AmountOfShips
        {
            get => amountofships;
            set
            {
                amountofships = value;
                OnPropertyChanged("AmountContent");
            }
        }
        public string AmountContent => Convert.ToString(amountofships); // get return amount =...

        public string Content => Convert.ToString(usermovecounter);

        public AppViewModel()
        {

        }
        public void BattleFieldButtonsCreate(int size)
        {
            bool isshiplocatedhere;
            int randomhere;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    randomhere = rand.Next(0, 20);
                    if (randomhere > 15) { isshiplocatedhere = true; amountofships++; }
                    else isshiplocatedhere = false;
                    BattleField.Add(new BattleFieldCell(isshiplocatedhere, true, x, y));
                }
            }
        }
        /// <summary>
        /// Это можно перенести в обычный класс
        /// </summary>
        private Command startGame;
        public Command StartGame
        {
            get
            {
                return startGame ?? (startGame = new Command(obj =>
                {
                    if (int.TryParse((string)obj, out int size))
                    {
                        if (size >= 2 && size <=9)
                        {
                            BattleFieldButtonsCreate(size);
                        }
                    }
                    else MessageBox.Show("Вы не можете видеть это.");
                }));
            }
        }
        public bool Enabled // isEnabled change for StartButton
        {
            get => enabled;
            set
            {
                enabled = value;
                OnPropertyChanged("Enabled");
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