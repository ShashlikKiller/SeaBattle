using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

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
        /// <summary>
        /// Координата по оси Х(строка/row)
        /// </summary> 
        private int x;
        /// <summary>
        /// Координата по оси Y(столбец/column)
        /// </summary>
        private int y;

        //// getters and setters
        public int Y
        {
            get { return y; }
            set
            {
                y = value;
                OnPropertyChanged("Y");
            }
        }
        public int X
        {
            get { return x; }
            set
            {
                x = value;
                OnPropertyChanged("X");
            }
        }
        public bool ShipLocation
        {
            get { return shiplocation; }
            set 
            {
                shiplocation = value;
                OnPropertyChanged("ShipLocation");
            }
        }
        public bool StillAlive
        {
            get { return this.stillalive; }
            set
            {
                this.stillalive = value;
                OnPropertyChanged("Color");
                OnPropertyChanged("IsEnabled");
                OnPropertyChanged("StillAlive");
            }
        }

        public string Color
        {
            get
            {
                return this.stillalive ? "Black" : "Red";
                // (condition) ? if true : else
            }
        }
        public bool IsEnabled
        {
            get
            {
                return this.stillalive ? true : false;
            }
        }

        /// <summary>
        /// Конструктор отдельной клетки поля
        /// </summary>
        public BattleFieldCell(bool shiplocation, bool stillalive, int x, int y)
        {
            this.shiplocation = shiplocation;
            this.stillalive = stillalive;
            this.x = x;
            this.y = y;
        }

        public event PropertyChangedEventHandler PropertyChanged; //Событие, которое будет вызвано при изменении модели

        //Метод, который скажет ViewModel, что нужно передать виду новые данные
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
