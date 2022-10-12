using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SeaBattle
{
    public class BattleFieldCell : INotifyPropertyChanged
    {
        /// <summary>
        /// Находится ли в этой клетке корабль? (Да/Нет)
        /// </summary>
        private bool shiplocation;
        /// <summary>
        /// Доступна ли эта клетка для изменения(нажатия)? (Да/Нет)
        /// </summary>
        private bool stillalive;
        private string color; // цвет
        private bool isenabledCell; // работоспособность кнопки
        private string buttoncontent; // содержимое кнопки

        //// getters and setters
        public int Y { get; set; }
        public int X { get; set; }
        public string Color
        {
            get => this.stillalive ? "Black" : "Red";// (condition) ? if true : else
            set => color = value;
        }
        public bool IsEnabledCell
        {
            get => this.stillalive ? true : false;
            set => isenabledCell = value;
        }
        public string ButtonContent
        {
            get
            {
                return this.shiplocation == true && this.stillalive == false ? (buttoncontent = "There are a ship!") : (buttoncontent = "");
            }
            set { buttoncontent = value; OnPropertyChanged("ButtonContent"); }
        }
        public bool ShipLocation
        {
            get => shiplocation;
            set
            {
                shiplocation = value;
                OnPropertyChanged("ShipLocation");
            }
        }
        public bool StillAlive
        {
            get => stillalive;
            set
            {
                stillalive = value;
                OnPropertyChanged("IsEnabledCell");
                OnPropertyChanged("Color");
                OnPropertyChanged("ButtonContent");
            }
        }
        public void KillThisCell()
        {
            this.StillAlive = false;
        }
        private Command pickCell;
        public Command PickCell
        {
            get
            {
                return pickCell ?? (pickCell = new Command(obj =>
                {
                        KillThisCell();
                }));
            }
        }
        /// <summary>
        /// Конструктор отдельной клетки поля
        /// </summary>
        public BattleFieldCell(bool shiplocation, bool stillalive, int x, int y)
        {
            this.shiplocation = shiplocation;
            this.stillalive = stillalive;
            this.X = x;
            this.Y = y;
        }

        public event PropertyChangedEventHandler PropertyChanged; //Событие, которое будет вызвано при изменении модели
        public void OnPropertyChanged([CallerMemberName] string prop = "") //Метод, который скажет ViewModel, что нужно передать виду новые данные
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}