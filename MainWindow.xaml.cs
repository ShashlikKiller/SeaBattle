using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace SeaBattle
{
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Лист интерфейса меню
        /// </summary>
        List<UIElement> StartGameInterface = new List<UIElement>();
        readonly AppViewModel appvm = new AppViewModel(); // Подключение ViewModel
        private int? text = 0; // Может принимать значение null
        private void StartGameInterfaceGeneralization() // Обобщение старт. интерфейса (меню)
        {
            StartGameInterface.Add(ButtonStartGame); // Кнопка начала игры
            StartGameInterface.Add(LabelFieldSize); // Лейбл над кнопкой
            StartGameInterface.Add(TextBoxFieldSize); // Текстбокс для ввода размера поля
        }
        private void StartGameInterfaceVisibility(bool warp) // Изменение видимости меню
        {
            foreach (UIElement item in StartGameInterface)
            {
                item.Visibility = warp == false ? Visibility.Hidden : Visibility.Visible;
            }
        }
        Binding amountofships = new Binding(); // Лейбл с количеством кораблей на поле
        Binding labelusermoves = new Binding(); // Лейбл с количеством ходов игрока
        public MainWindow()
        {
            InitializeComponent();
            ImageBrush myBrush = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "C:\\Users\\Roger\\source\\repos\\SeaBattle\\background.jpg"))
            };
            this.Background = myBrush; // Изменение фона окна приложения
            DataContext = appvm; // Подключение ViewModel к DataContext
            StartGameInterfaceGeneralization();
            labelusermoves.Path = new PropertyPath("Content"); // Связь лейбла со свойством
            LabelUserMoves.SetBinding(Label.ContentProperty, labelusermoves); 
            amountofships.Path = new PropertyPath("AmountContent"); // Ещё одна связь
            AmountOfShips.SetBinding(Label.ContentProperty, amountofships);
        }
        private void ButtonStartGame_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Игра началась!"); // Небольшое упоминание правил игры в эту поделку: На сервере запрещено использование читов, распрыжек скриптов и прочих хитростей");
            StartGameInterfaceVisibility(false); // Отключение стартового меню
        }
        private void ButtonBattleFieldCell_Click(object sender, RoutedEventArgs e)
        {
            appvm.UserMoveCounter++; // Увеличение количества ходов игрока
            appvm.AmountOfShips--; // Костыль для обновления кол-ва кораблей
            appvm.AmountOfShips++;
        }
        /// <summary>
        /// Контроль ввода в текстбокс: допускаются только натуральные числа от 2 до 9(включительно).
        /// </summary>
        private void TextBoxFieldSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxFieldSize.Text != "")
            {
                text = Convert.ToInt32(TextBoxFieldSize.Text);
                if (text >= 2 && text <= 9)
                {
                    appvm.Enabled = true; // Получение доступа к нажатию кнопки StartGame
                }
                else
                {
                    MessageBox.Show("Введите размер поля от 2 до 10 ячеек. Размер больше или меньше не принимается.");
                    TextBoxFieldSize.Text = "2"; // Автоматическое заполнение размера поля (2 - мин. размер)
                    appvm.Enabled = false; // Отказ доступа к нажатию кнопки StartGame
                }
            }
            else
            {
                MessageBox.Show("Введите значение.");
                TextBoxFieldSize.Text = "2";
            }
        }
        private void TextBox_KeyDown(object sender, KeyEventArgs e) // Ввод только цифр в TextBox
        {
            if (!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) & e.Key != Key.Back | e.Key == Key.Space)
            {
                e.Handled = true;
                MessageBox.Show("В этом текстбоксе можно использовать ТОЛЬКО целые числа от 2 до 10.");
            }
        }
    }
}