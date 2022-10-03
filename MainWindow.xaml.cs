using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static SeaBattle.BattleFieldCell;
using static SeaBattle.AppViewModel;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace SeaBattle
{
    // Создать прототип приложения «Морской бой».
    // Пользователь выбирает размер поля.
    // Создается поле из кнопок.
    // Приложение «прячет» корабли.
    // Пользователь ищет, где эти корабли находятся.
    // Приложение считает количество ходов и меняет цвет, и блокирует уже открытые клетки.

    public partial class MainWindow : Window
    {
        /// <summary>
        /// Лист интерфейса меню
        /// </summary>
        List<UIElement> StartGameInterface = new List<UIElement>();
        /// <summary>
        /// Значение, введеное пользователем в TextBoxBattleFieldSize и обозначающие размер игрового поля.
        /// </summary>
        AppViewModel appvm = new AppViewModel();

        int? text = 0; // Может принимать значение null
        Random rand = new Random();

        void StartGameInterfaceGeneralization() // Обобщение старт. интерфейса (меню)
        {
            StartGameInterface.Add(ButtonStartGame);
            StartGameInterface.Add(LabelFieldSize);
            StartGameInterface.Add(TextBoxFieldSize);
            StartGameInterface.Add(TextBoxAmountOfShips);
        }

        private void StartGameInterfaceVisibility(bool warp) // Изменение видимости меню
        {
            foreach (UIElement item in StartGameInterface)
            {
                item.Visibility = warp == false ? Visibility.Hidden : Visibility.Visible;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = appvm;
            //DataContext = new AppViewModel();
            //AppViewModel appvm = new AppViewModel();
            StartGameInterfaceGeneralization();
            StartGameInterfaceVisibility(true);
            //ButtonStartGame.IsEnabled = false;
            Binding LabelSizeBinding = new Binding();
            Binding labelusermoves = new Binding();
            labelusermoves.Path = new PropertyPath("Content");
            LabelUserMoves.SetBinding(Label.ContentProperty, labelusermoves);
            //LabelUserMoves.Content = "Количество ходов: " + appvm.UserMoveCounter;
            LabelSizeBinding.Source = appvm.BattleFieldSize;
            LabelSizeBinding.Path = new PropertyPath("Content");
            LabelFieldSize.SetBinding(Label.ContentProperty, LabelSizeBinding);
            LabelFieldSize.Content = "Размер поля: " + appvm.BattleFieldSize;
            //...
            //appvm.CreateBattleField(appvm.BattleFieldSize, appvm.AmountOfShips);
            //appvm.BattleFieldCells.Add(new BattleFieldCell(true, true, 3, 5));
            
        }

        private void ButtonStartGame_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Игра началась!");// Небольшое упоминание правил игры в эту поделку: На сервере запрещено использование читов, распрыжек скриптов и прочих хитростей");
            StartGameInterfaceVisibility(false);
            battlefieldbuttonscreate(appvm.BattleFieldSize, appvm.AmountOfShips);
        }
        private void ButtonBattleFieldCell_Click(object sender, RoutedEventArgs e)
        {
            appvm.UserMoveCounter++;
            MessageBox.Show("Test");
            // логика нажатия на кнопку поля
            //OnPropertyChanged(;
            //appvm.StillAlive();
            //if ()
        }

        /// <summary>
        /// Создание игрового поля из кнопок размером SIZExSIZE, где SIZE принимается из TextBoxStartGame
        /// </summary>
        private void battlefieldbuttonscreate(int size, int amount)
        {
            bool isshiplocatedhere;
            int randomhere;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    //while (amount != 0)
                    //{
                    randomhere = rand.Next(0, 20);
                    if (randomhere > 15) { isshiplocatedhere = true; amount--; }
                    else { isshiplocatedhere = false; }
                    appvm.BattleFieldCells.Add(new BattleFieldCell(isshiplocatedhere, true, x, y));
                    //}
                }
            }
        }

        private void TextBoxFieldSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxFieldSize.Text != "")
            {
                text = Convert.ToInt32(TextBoxFieldSize.Text);
                if (text >= 2 && text <= 9)
                {
                    appvm.BattleFieldSize = Convert.ToInt32(text); // usbfs - инт размера поля, введенное пользователем
                }
                else
                {
                    MessageBox.Show("Введите размер поля от 2 до 10 ячеек. Размер больше или меньше не принимается.");
                    TextBoxFieldSize.Text = "2";
                }
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e) // Ввод только цифр
        {
            if (!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back | e.Key == Key.Space)
            {
                e.Handled = true;
                MessageBox.Show("В этом текстбоксе можно использовать ТОЛЬКО целые числа от 2 до 10.");
            }
        }

        private void TextBoxAmountOfShips_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxAmountOfShips.Text != "")
            {
                text = Convert.ToInt32(TextBoxAmountOfShips.Text);
                if (text <= appvm.BattleFieldSize * appvm.BattleFieldSize)
                {
                    appvm.AmountOfShips = Convert.ToInt32(text); // aof - количество кораблей на поле
                }
                else
                {
                    MessageBox.Show("Введите количество кораблей на поле от 0 до количества ячеек на поле. Число больше не принимается.");
                    TextBoxAmountOfShips.Text = "1";
                }
            }
        }
    }
}