using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace WindowsGame3
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public enum State //Объявление состояний экрана (система меню)
        {
            Menu,         //Меню
            Playing,      //Игра
            Pause,        //Пауза
            Help,         //Помощь
            Autor,        //Об авторе
            GameOver,     //Конец игры (проигрыш)
            Exit          //Выход
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        //Вызов классов и объявление переменных
        HUD hud = new HUD();                            //Переменная для счётчика очков
        Player p = new Player();                        //Переменная для авто игрока
        Random random = new Random();                   //Переменная для рандома
        Level1 lvl1 = new Level1();                     //Переменная для класса с дорогой
        BackHud bh = new BackHud();                     //Переменная для класса интерфейса
        List<Barrier> brList = new List<Barrier>();     //Переменная для списика 1 вида препятствий
        List<Barrier1> brList1 = new List<Barrier1>();  //Переменная для списика дырок в асфальте для 1 вида препятствий
        List<Barrier2> brList2 = new List<Barrier2>();  //Переменная для списика 2 вида препятствий
        List<Health> HealthList = new List<Health>();   //Переменная для списика сердечек
        Hell hell = new Hell();                         //Переменная для объекта уничтожения препятствий
        List<Explosion> exList = new List<Explosion>(); //Переменная для списка взрывов
        SoundManager sm = new SoundManager();           //Переменная для класса звуков
        State CurrentGameState = State.Menu;            //Переменная для состояния меню
        public Texture2D bgMenu;                        //Переменная для фонового рисунка в меню
        public Texture2D GameOverImage;                 //Переменная для изображеия проигрыша
        public int health, countbarrier, countHealth;   //Переменная для счётчика здоровья, колличества препятсвий каждого вида, сердечек
        public Texture2D healthTexture;                 //Переменные для текстур здоровья
        public Texture2D hellTop;                       //Переменная для текстуры свечение надо уничтожителем
        public Texture2D PauseImage;                    //Переменная для изображения паузы
        public Texture2D HelpImage;                     //Переменная для фонового изображения в меню помощи
        public Texture2D AutorImage;                    //Переменная для фонового изображения в меню об авторе
        public int radio;                               //Переменная для значения номера песни на радио
        public Texture2D radioTexture;                  //Переменная для текстуры Радио
        public Rectangle radioRectangle;                //Переменная для прямоугольника Радио
        public Vector2 radioPosition;                   //Переменная для позиции Радио
        
        //Переменные для кнопок
        MenuBut1 MenuBut1;
        MenuBut2 MenuBut2;
        MenuBut3 MenuBut3;
        MenuBut4 MenuBut4;
        GameOverBut1 GameOverBut1;
        HelpBut1 HelpBut1;
        PauseButton2 PauseButton2;

        public Game1()                                  //Конструктор контента
        {
            graphics = new GraphicsDeviceManager(this);

            IsMouseVisible = true;                       //Видимость курсоса мыши в окне
            graphics.IsFullScreen = false;               //Запрет развёртки на весь экран
            bgMenu = null;                               //Значение переменной = ничего
            GameOverImage = null;                        //Значение переменной = ничего
            health = 5;                                  //Значение здоровья
            countbarrier = 1;                            //Значение колличества препятсвий каждого вида на экране
            countHealth = 1;                             //Значение колличества сердечек на экране
            hellTop = null;                              //Значение переменной = ничего
            PauseImage = null;                           //Значение переменной = ничего
            HelpImage = null;                            //Значение переменной = ничего
            graphics.PreferredBackBufferWidth = 800;     //Размеры игрового окна
            graphics.PreferredBackBufferHeight = 600;
            radioTexture = null;                         //Значение переменной = ничего
            radioPosition = new Vector2(600, 456);       //Позиция Радио

            this.Window.Title = "ESR - Extreme Subaru Racing v.1.0.0"; //Название в шапке игрового окна

            Content.RootDirectory = "Content"; //Препдоложительно название папки для файлов проекта в корневой папке проекта
        }

        protected override void Initialize()   //Инициализация
        {
            base.Initialize();
        }

        protected override void LoadContent()                         //Загрузчик контента
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            hud.LoadContent(Content);                                 //Загрузка счётчика очков
            lvl1.LoadContent(Content);                                //Загрузка дороги   
            p.LoadContent(Content);                                   //Загрузка авто игрока 
            bh.LoadContent(Content);                                  //Загрузка интерфейса
            hell.LoadContent(Content);                                //Загрузка уничтожителя
            sm.LoadContent(Content);                                  //Загрузка менеджера звуков
            bgMenu = Content.Load<Texture2D>("MenuImage");            //Загрузка фонового изображения для меню
            GameOverImage = Content.Load<Texture2D>("GameOverImage"); //Загрузка фонового изображения для меню проигрыша
            hellTop = Content.Load<Texture2D>("hellTop");             //Загрузка текстуры свечения надо уничтожителем
            PauseImage = Content.Load<Texture2D>("PauseImage");       //Загрузка текстуры паузы
            HelpImage = Content.Load<Texture2D>("HelpImage");         //Загрузка текстуры фонового изображения в меню помощи
            AutorImage = Content.Load<Texture2D>("Autor");            //Загрузка текстуры фонового изображения в меню об авторе
            radio = 1;                                                //Переменная для значения номера песни на радио


            //Загрузка текстур для кнопок в меню
            MenuBut1 = new MenuBut1(Content.Load<Texture2D>("MenuBut1"), graphics.GraphicsDevice);
            MenuBut1.SetPosition(new Vector2(574, 249));
            MenuBut2 = new MenuBut2(Content.Load<Texture2D>("MenuBut2"), graphics.GraphicsDevice);
            MenuBut2.SetPosition(new Vector2(634, 299));
            MenuBut3 = new MenuBut3(Content.Load<Texture2D>("MenuBut3"), graphics.GraphicsDevice);
            MenuBut3.SetPosition(new Vector2(581, 349));
            MenuBut4 = new MenuBut4(Content.Load<Texture2D>("MenuBut4"), graphics.GraphicsDevice);
            MenuBut4.SetPosition(new Vector2(663, 405));
            GameOverBut1 = new GameOverBut1(Content.Load<Texture2D>("GameOverBut1"), graphics.GraphicsDevice);
            GameOverBut1.SetPosition(new Vector2(433, 426));
            HelpBut1 = new HelpBut1(Content.Load<Texture2D>("HelpBut1"), graphics.GraphicsDevice);
            HelpBut1.SetPosition(new Vector2(417, 545));
            PauseButton2 = new PauseButton2(Content.Load<Texture2D>("PauseBut2"), graphics.GraphicsDevice);
            PauseButton2.SetPosition(new Vector2(294, 388));
        }

        protected override void UnloadContent() //Выгрузка контента
        {
        }

        protected override void Update(GameTime gameTime) //Обновление контента
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            MouseState mouse = Mouse.GetState(); //Получение состояние мыши

            switch (CurrentGameState)            //Переключатель состояний экрана
            {
                case State.Playing:              //Состояние экрана - Игра:
                    {
                        KeyboardState keyState = Keyboard.GetState(); //Получение состояние клавиатуры

                        if (keyState.IsKeyDown(Keys.Escape)) //Условие нажатия кнопки паузы с клавиатуры
                        {
                            CurrentGameState = State.Pause;  //Продолжить игру
                            MediaPlayer.Pause();             //Воспроизведение фоновой музыки
                        }

                        if (keyState.IsKeyDown(Keys.D1)) //Условие воспроизведения 1 песни на радио
                        {
                            MediaPlayer.Play(sm.radio1);
                            radio = 1;
                        }

                        if (keyState.IsKeyDown(Keys.D2)) //Условие воспроизведения 2 песни на радио
                        {
                            MediaPlayer.Play(sm.radio2);
                            radio = 2;
                        }

                        if (keyState.IsKeyDown(Keys.D3)) //Условие воспроизведения 3 песни на радио
                        {
                            MediaPlayer.Play(sm.radio3);
                            radio = 3;
                        }

                        if (keyState.IsKeyDown(Keys.D4)) //Условие воспроизведения 4 песни на радио
                        {
                            MediaPlayer.Play(sm.radio4);
                            radio = 4;
                        }

                        if (keyState.IsKeyDown(Keys.D5)) //Условие воспроизведения 5 песни на радио
                        {
                            MediaPlayer.Play(sm.radio5);
                            radio = 5;
                        }

                        if (keyState.IsKeyDown(Keys.D6)) //Условие воспроизведения 5 песни на радио
                        {
                            MediaPlayer.Play(sm.radio6);
                            radio = 6;
                        }

                        if (keyState.IsKeyDown(Keys.D7)) //Условие остановления воспроизведения радио
                        {
                            MediaPlayer.Stop();
                            radio = 7;
                        }

                        foreach (Explosion ex in exList) //Обновление взрывов
                        {
                            ex.Update(gameTime);
                        }

                        p.Update(gameTime);              //Обновление состояния авто игрока

                        foreach (Health a in HealthList) //Обновление препятствий
                        {
                            //Изменение скорости  и колличества сердечек на экране
                            if (hud.playerScore >= 0)
                            {
                                lvl1.speed = 4;
                                a.speed = 4;
                                hud.speed = 4;
                                countbarrier = 1;

                                if (hud.playerScore >= 10)
                                {
                                    lvl1.speed = 5;
                                    a.speed = 5;
                                    hud.speed = 5;
                                    countbarrier = 1;

                                    if (hud.playerScore >= 20)
                                    {
                                        lvl1.speed = 6;
                                        a.speed = 6;
                                        hud.speed = 6;
                                        countbarrier = 2;

                                        if (hud.playerScore >= 30)
                                        {
                                            lvl1.speed = 7;
                                            a.speed = 7;
                                            hud.speed = 7;
                                            countbarrier = 2;

                                            if (hud.playerScore >= 45)
                                            {
                                                lvl1.speed = 8;
                                                a.speed = 8;
                                                hud.speed = 8;
                                                countbarrier = 2;

                                                if (hud.playerScore >= 70)
                                                {
                                                    lvl1.speed = 9;
                                                    a.speed = 9;
                                                    hud.speed = 9;
                                                    countbarrier = 3;

                                                    if (hud.playerScore >= 100)
                                                    {
                                                        lvl1.speed = 10;
                                                        a.speed = 10;
                                                        hud.speed = 10;
                                                        countbarrier = 3;

                                                        if (hud.playerScore >= 250)
                                                        {
                                                            lvl1.speed = 12;
                                                            a.speed = 12;
                                                            hud.speed = 12;
                                                            countbarrier = 3;

                                                            if (hud.playerScore >= 500)
                                                            {
                                                                lvl1.speed = 15;
                                                                a.speed = 15;
                                                                hud.speed = 15;
                                                                countbarrier = 4;

                                                                if (hud.playerScore >= 800)
                                                                {
                                                                    lvl1.speed = 18;
                                                                    a.speed = 18;
                                                                    hud.speed = 18;
                                                                    countbarrier = 4;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (a.boundingBox.Intersects(hell.boundingBox)) //Условие столкновения препятсвия с уничтожителем
                            {
                                sm.destroyerSound.Play();                   //Проигрыш звука взрыва
                                a.isVisible = false;                        //Препятствие пропадает с экрана 
                            }

                            if (a.boundingBox.Intersects(p.boundingBox))    //Условие столкновения препятствия с авто игрока
                            {
                                sm.HealthSound.Play();                      //Воспроизведение звука взрыва

                                if (health <= 4)                            //Условие для восполнения значения здоровья
                                    health = health + 1;

                                if (health == 5)                            //Условие от переполнения значения здоровья
                                    health = health + 0;

                                a.isVisible = false;                        //Препятствие пропадает с экрана
                            }
                            a.Update(gameTime);                             //Обновление состояния препятствия
                        }

                        foreach (Barrier a in brList)                       //Обновление препятствий
                        {
                            //Изменение скорости и колличества препятствий на экране
                            if (hud.playerScore >= 0)
                            {
                                lvl1.speed = 4;
                                a.speed = 4;
                                hud.speed = 4;
                                countbarrier = 1;

                                if (hud.playerScore >= 10)
                                {
                                    lvl1.speed = 5;
                                    a.speed = 5;
                                    hud.speed = 5;
                                    countbarrier = 1;

                                    if (hud.playerScore >= 20)
                                    {
                                        lvl1.speed = 6;
                                        a.speed = 6;
                                        hud.speed = 6;
                                        countbarrier = 2;

                                        if (hud.playerScore >= 30)
                                        {
                                            lvl1.speed = 7;
                                            a.speed = 7;
                                            hud.speed = 7;
                                            countbarrier = 2;

                                            if (hud.playerScore >= 45)
                                            {
                                                lvl1.speed = 8;
                                                a.speed = 8;
                                                hud.speed = 8;
                                                countbarrier = 2;

                                                if (hud.playerScore >= 70)
                                                {
                                                    lvl1.speed = 9;
                                                    a.speed = 9;
                                                    hud.speed = 9;
                                                    countbarrier = 3;

                                                    if (hud.playerScore >= 100)
                                                    {
                                                        lvl1.speed = 10;
                                                        a.speed = 10;
                                                        hud.speed = 10;
                                                        countbarrier = 3;

                                                        if (hud.playerScore >= 250)
                                                        {
                                                            lvl1.speed = 12;
                                                            a.speed = 12;
                                                            hud.speed = 12;
                                                            countbarrier = 3;

                                                            if (hud.playerScore >= 500)
                                                            {
                                                                lvl1.speed = 15;
                                                                a.speed = 15;
                                                                hud.speed = 15;
                                                                countbarrier = 4;

                                                                if (hud.playerScore >= 800)
                                                                {
                                                                    lvl1.speed = 18;
                                                                    a.speed = 18;
                                                                    hud.speed = 18;
                                                                    countbarrier = 4;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (a.boundingBox.Intersects(hell.boundingBox)) //Условие столкновения препятсвия с уничтожителем
                            {
                                sm.destroyerSound.Play();             //Проигрыш звука взрыва
                                hud.playerScore += 1;                 //+1 очков в счётчки очков
                                a.isVisible = false;                  //Препятствие пропадает с экрана 
                            }

                            if (a.boundingBox.Intersects(p.boundingBox)) //Условие столкновения препятствия с авто игрока
                            {
                                sm.explodeSound.Play();                  //Воспроизведение звука взрыва

                                //Воспроизведение анимации взрыва (строка ниже):
                                exList.Add(new Explosion(Content.Load<Texture2D>("explosion1"), new Vector2(a.position.X + 35, a.position.Y + 17)));
                                
                                health -= 1;                         //-1 сердце (очко) здоровья
                                a.isVisible = false;                 //Препятствие пропадает с экрана
                            }
                            a.Update(gameTime);                      //Обновление состояния препятствия
                        }


                        foreach (Barrier2 a in brList2)              //Обновление 2 вида препятствий
                        {
                            //Изменение скорости  и колличества препятствий на экране
                            if (hud.playerScore >= 0)
                            {
                                lvl1.speed = 4;
                                a.speed = 4;
                                hud.speed = 4;
                                countbarrier = 1;

                                if (hud.playerScore >= 10)
                                {
                                    lvl1.speed = 5;
                                    a.speed = 5;
                                    hud.speed = 5;
                                    countbarrier = 1;

                                    if (hud.playerScore >= 20)
                                    {
                                        lvl1.speed = 6;
                                        a.speed = 6;
                                        hud.speed = 6;
                                        countbarrier = 2;

                                        if (hud.playerScore >= 30)
                                        {
                                            lvl1.speed = 7;
                                            a.speed = 7;
                                            hud.speed = 7;
                                            countbarrier = 2;

                                            if (hud.playerScore >= 45)
                                            {
                                                lvl1.speed = 8;
                                                a.speed = 8;
                                                hud.speed = 8;
                                                countbarrier = 2;

                                                if (hud.playerScore >= 70)
                                                {
                                                    lvl1.speed = 9;
                                                    a.speed = 9;
                                                    hud.speed = 9;
                                                    countbarrier = 3;

                                                    if (hud.playerScore >= 100)
                                                    {
                                                        lvl1.speed = 10;
                                                        a.speed = 10;
                                                        hud.speed = 10;
                                                        countbarrier = 3;

                                                        if (hud.playerScore >= 250)
                                                        {
                                                            lvl1.speed = 12;
                                                            a.speed = 12;
                                                            hud.speed = 12;
                                                            countbarrier = 3;

                                                            if (hud.playerScore >= 500)
                                                            {
                                                                lvl1.speed = 15;
                                                                a.speed = 15;
                                                                hud.speed = 15;
                                                                countbarrier = 4;

                                                                if (hud.playerScore >= 800)
                                                                {
                                                                    lvl1.speed = 18;
                                                                    a.speed = 18;
                                                                    hud.speed = 18;
                                                                    countbarrier = 4;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (a.boundingBox.Intersects(hell.boundingBox)) //Условие столкновения препятсвия с уничтожителем
                            {
                                sm.destroyerSound.Play();                   //Проигрыш звука взрыва
                                hud.playerScore += 1;                       //+1 очков в счётчки очков
                                a.isVisible = false;                        //Препятствие пропадает с экрана 
                            }

                            if (a.boundingBox.Intersects(p.boundingBox))    //Условие столкновения препятствия с авто игрока
                            {
                                sm.explodeSound2.Play();                    //Воспроизведение звука взрыва

                                //Воспроизведение анимации взрыва (строка ниже):
                                exList.Add(new Explosion(Content.Load<Texture2D>("explosion2"), new Vector2(a.position.X + 20, a.position.Y + 18)));

                                health -= 1;                                //-1 сердце (очко) здоровья
                                a.isVisible = false;                        //Препятствие пропадает с экрана
                            }
                            a.Update(gameTime);                             //Обновление состояния препятствия
                        }

                        foreach (Barrier1 a in brList1)                     //Обновление препятствий
                        {
                            //Изменение скорости и колличества препятствий на экране
                            if (hud.playerScore >= 0)
                            {
                                lvl1.speed = 4;
                                a.speed = 4;
                                hud.speed = 4;
                                countbarrier = 1;

                                if (hud.playerScore >= 10)
                                {
                                    lvl1.speed = 5;
                                    a.speed = 5;
                                    hud.speed = 5;
                                    countbarrier = 1;

                                    if (hud.playerScore >= 20)
                                    {
                                        lvl1.speed = 6;
                                        a.speed = 6;
                                        hud.speed = 6;
                                        countbarrier = 2;

                                        if (hud.playerScore >= 30)
                                        {
                                            lvl1.speed = 7;
                                            a.speed = 7;
                                            hud.speed = 7;
                                            countbarrier = 2;

                                            if (hud.playerScore >= 45)
                                            {
                                                lvl1.speed = 8;
                                                a.speed = 8;
                                                hud.speed = 8;
                                                countbarrier = 2;

                                                if (hud.playerScore >= 70)
                                                {
                                                    lvl1.speed = 9;
                                                    a.speed = 9;
                                                    hud.speed = 9;
                                                    countbarrier = 3;

                                                    if (hud.playerScore >= 100)
                                                    {
                                                        lvl1.speed = 10;
                                                        a.speed = 10;
                                                        hud.speed = 10;
                                                        countbarrier = 3;

                                                        if (hud.playerScore >= 250)
                                                        {
                                                            lvl1.speed = 12;
                                                            a.speed = 12;
                                                            hud.speed = 12;
                                                            countbarrier = 3;

                                                            if (hud.playerScore >= 500)
                                                            {
                                                                lvl1.speed = 15;
                                                                a.speed = 15;
                                                                hud.speed = 15;
                                                                countbarrier = 4;

                                                                if (hud.playerScore >= 800)
                                                                {
                                                                    lvl1.speed = 18;
                                                                    a.speed = 18;
                                                                    hud.speed = 18;
                                                                    countbarrier = 4;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            if (a.boundingBox.Intersects(hell.boundingBox)) //Условие столкновения препятсвия с уничтожителем
                            {
                                a.isVisible = false;                        //Препятствие пропадает с экрана     
                            }

                            a.Update(gameTime);                             //Обновление состояния препятствия
                        }

                        lvl1.Update(gameTime);                              //Обновление состояния дороги
                        hell.Update(gameTime);                              //Обновление состояния уничтожителя
                        LoadBarrier();                                      //Обновление препятствий
                        MenageExplosion();                                  //Обновление менеджера взрывов

                        break;                                              //Остановка выполненения состояния экрана - Игра
                    }

                case State.Pause: //Состояние экрана - Пауза:
                    {
                        KeyboardState keyState = Keyboard.GetState(); //Получение состояние клавиатуры

                        if (keyState.IsKeyDown(Keys.Enter))           //Условие нажатой 1ой кнопки
                        {
                            CurrentGameState = State.Playing;         //Продолжить игру
                            MediaPlayer.Resume();                     //Воспроизведение фоновой музыки
                        }

                        //Нажатие мышью второй кнопки
                        if (PauseButton2.IsClicked == true) CurrentGameState = State.Menu;    
                        PauseButton2.Update(mouse);

                        if (PauseButton2.IsClicked == true)     //Условие нажатой второй кнопки
                        {
                            CurrentGameState = State.Menu;      //Выход в меню
                            MediaPlayer.Stop();                 //Остановление фоновой музыки
                            lvl1.speed = 4;                     //Сброс скорости прокрутки дороги
                            hud.playerScore = 0;                //Обнуление счётчика очков
                            countbarrier = 1;                   //Обнуление счётчика колличества препятствий на экране
                            health = 5;                         //Возврат исходного значения здоровья
                            brList.Clear();                     //Очистить список препятствий
                            brList1.Clear();                    //Очистить список препятствий
                            brList2.Clear();                    //Очистить список препятствий
                            exList.Clear();                     //Очистить список взрывов
                            p.position = new Vector2(300, 500); //Обнуление стартовой позиции игрока (его авто)
                            countHealth = 1;                    //Обнуление счётчика здоровья
                            radio = 1;                          //Восстановление номера песни на радио
                        }

                        if (keyState.IsKeyDown(Keys.Escape))    //Условие, что-бы не было бага по Esc
                        {
                            CurrentGameState = State.Pause;     //Сохранения состояния паузы
                            MediaPlayer.Pause();                //Приостановление фоновой музыки
                        }
                        break;                                  //Остановка выполненения состояния экрана - Меню.
                    }
                
                case State.Menu: //Состояние экрана - Меню:
                    {
                        //Условия по нажатию кнопок в меню
                        if (MenuBut1.IsClicked == true)  CurrentGameState = State.Playing;
                        MenuBut1.Update(mouse);

                        if (MenuBut1.IsClicked == true)  //Условие нажатой первой кнопки
                        {
                            MediaPlayer.Play(sm.radio1); //Воспроизведение радио
                            countbarrier = 1;
                        }

                        if (MenuBut2.IsClicked == true) CurrentGameState = State.Help;
                        MenuBut2.Update(mouse);

                        if (MenuBut3.IsClicked == true) CurrentGameState = State.Autor;
                        MenuBut3.Update(mouse);

                        if (MenuBut4.IsClicked == true) CurrentGameState = State.Exit; 
                        MenuBut4.Update(mouse);

                        lvl1.Update(gameTime);                        //Обновление состояния дороги

                        break;                                        //Остановка выполненения состояния экрана - Меню.
                    }
                    
                case State.GameOver: //Состояние экрана - Конец игры:
                    {
                        MediaPlayer.Stop();   //Остановка воспроизведения фоновой музыки

                        //Условия по нажатию кнопок в меню проигрыша
                        if (GameOverBut1.IsClicked == true) CurrentGameState = State.Menu;
                        GameOverBut1.Update(mouse);

                        if (GameOverBut1.IsClicked == true)     //Условие нажатой кнопки
                        {
                            lvl1.speed = 4;                     //Сброс скорости прокрутки дороги
                            hud.playerScore = 0;                //Обнуление счётчика очков
                            countbarrier = 1;                   //Обнуление счётчика колличества препятствий на экране
                            health = 5;                         //Возврат исходного значения здоровья
                            brList.Clear();                     //Очистить список препятствий
                            brList1.Clear();                    //Очистить список препятствий
                            brList2.Clear();                    //Очистить список препятствий
                            exList.Clear();                     //Очистить список взрывов
                            p.position = new Vector2(300, 500); //Обнуление стартовой позиции игрока (его авто)
                            countHealth = 1;                    //Обнуление счётчика здоровья
                            radio = 1;                          //Восстановление номера песни на радио
                        }

                        break; //Остановка выполненения состояния экрана - Конец игры
                    }

                case State.Help: //Состояние экрана - Помощь:
                    {
                        //Условия по нажатию кнопок в меню помощи
                        if (HelpBut1.IsClicked == true) CurrentGameState = State.Menu;
                        HelpBut1.Update(mouse);

                        if (HelpBut1.IsClicked == true)
                        { }

                        break; //Остановка выполненения состояния экрана - Конец игры.
                    }

                case State.Autor: //Состояние экрана - Об авторе:
                    {
                        //Условия по нажатию кнопrb в меню помощи
                        if (HelpBut1.IsClicked == true) CurrentGameState = State.Menu;
                        HelpBut1.Update(mouse);

                        if (HelpBut1.IsClicked == true)
                        { }

                        break; //Остановка выполненения состояния экрана - Конец игры.
                    }

                case State.Exit: //Состояние экрана - Выход из игры
                    {
                        this.Exit(); //Выход из приложения
                        break;
                    }
            }

            if (health <= 0)   //Условие: при 0 здоровье запустить режим проигрыша
            {
                MediaPlayer.Stop();
                CurrentGameState = State.GameOver;
            }

            //Обновление состояния кнопок
            MenuBut1.Update(mouse);
            MenuBut2.Update(mouse);
            MenuBut3.Update(mouse);
            MenuBut4.Update(mouse);
            GameOverBut1.Update(mouse);
            HelpBut1.Update(mouse);
            PauseButton2.Update(mouse);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) //Отрисовка контента
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();      //Начало отрисовки

            switch (CurrentGameState) //Переключатель состояний экрана
            {
                    
                case State.Playing:   //Состояние экрана - Игра:
                    {
                        //Условие загрузки текстуры здоровья
                        if (health == 0)
                            healthTexture = Content.Load<Texture2D>("health0");
                        if (health >= 1)
                            healthTexture = Content.Load<Texture2D>("health1");
                        if (health >= 2)
                            healthTexture = Content.Load<Texture2D>("health2");
                        if (health >= 3)
                            healthTexture = Content.Load<Texture2D>("health3");
                        if (health >= 4)
                            healthTexture = Content.Load<Texture2D>("health4");
                        if (health == 5)
                            healthTexture = Content.Load<Texture2D>("health5");

                        //Условия переключение песен на Радио
                        if (radio == 1)
                            radioTexture = Content.Load<Texture2D>("radio1");
                        if (radio == 2)
                            radioTexture = Content.Load<Texture2D>("radio2");
                        if (radio == 3)
                            radioTexture = Content.Load<Texture2D>("radio3");
                        if (radio == 4)
                            radioTexture = Content.Load<Texture2D>("radio4");
                        if (radio == 5)
                            radioTexture = Content.Load<Texture2D>("radio5");
                        if (radio == 6)
                            radioTexture = Content.Load<Texture2D>("radio6");
                        if (radio == 7)
                            radioTexture = Content.Load<Texture2D>("radio7");

                        lvl1.Draw(spriteBatch);          //Отрисовка дороги

                        foreach (Barrier1 a in brList1)  //Отрисовка дырок в асфальте после взрыва
                        {
                            a.Draw(spriteBatch);
                        }

                        foreach (Barrier2 a in brList2)    //Отрисовка препятствия
                        {
                            a.Draw(spriteBatch);
                        }

                        foreach (Barrier a in brList)     //Отрисовка препятствия
                        {
                            a.Draw(spriteBatch);
                        }

                        p.Draw(spriteBatch);              //Отрисовка авто игрока

                        foreach (Explosion ex in exList)  //Отрисовка взрыва
                        {
                            ex.Draw(spriteBatch);
                        }

                        foreach (Health ex in HealthList) //Отрисовка сердечек
                        {
                            ex.Draw(spriteBatch);
                        }
                        
                        //Отрисовка свечения над уничтожителем (строка ниже):
                        spriteBatch.Draw(hellTop, new Vector2(50, 565), Color.White); 

                        bh.Draw(spriteBatch);            //Отрисовка интерфейса
                        hud.Draw(spriteBatch);           //Отрисовка счётчика очков

                        //Отрисовка здоровья (строка ниже):
                        spriteBatch.Draw(healthTexture, p.healthPosition, Color.White);

                        //Отрисовка радио (строка ниже):
                        spriteBatch.Draw(radioTexture, radioPosition, Color.White);

                        break;                           //Остановка выполненения состояния экрана - Игра.
                    }

                case State.Pause: //Состояние экрана - Пауза:
                    {
                        lvl1.Draw(spriteBatch);          //Отрисовка дороги

                        foreach (Barrier1 a in brList1)  //Отрисовка препятствия
                        {
                            a.Draw(spriteBatch);
                        }

                        foreach (Barrier2 a in brList2)  //Отрисовка препятствия
                        {
                            a.Draw(spriteBatch);
                        }

                        foreach (Barrier a in brList)    //Отрисовка препятствия
                        {
                            a.Draw(spriteBatch);
                        }

                        p.Draw(spriteBatch);             //Отрисовка авто игрока

                        foreach (Explosion ex in exList) //Отрисовка взрыва
                        {
                            ex.Draw(spriteBatch);
                        }

                        foreach (Health ex in HealthList) //Отрисовка сердечек
                        {
                            ex.Draw(spriteBatch);
                        }

                        //Отрисовка свечения над уничтожителем (строка ниже):
                        spriteBatch.Draw(hellTop, new Vector2(50, 565), Color.White); 

                        //Отрисовка фонового изображения меню (строка ниже):
                        spriteBatch.Draw(PauseImage, new Vector2(0, 0), Color.White); 

                        PauseButton2.Draw(spriteBatch);  //Отрисовка 2ой кнопки

                        break;                           //Остановка выполненения состояния экрана - Меню.
                    }

                case State.Menu: //Состояние экрана - Меню:
                    {
                        //Отрисовка фонового изображения меню (строка ниже):
                        spriteBatch.Draw(bgMenu, new Vector2(0, 0), Color.White); 

                        //Отрисовка кнопок в меню
                        MenuBut1.Draw(spriteBatch);
                        MenuBut2.Draw(spriteBatch);
                        MenuBut3.Draw(spriteBatch);
                        MenuBut4.Draw(spriteBatch);

                        break; //Остановка выполненения состояния экрана - Меню.
                    }

                case State.GameOver: //Состояние экрана - Конец игры:
                    {
                        //Отрисовка фонового изображения меню проигрыша
                        spriteBatch.Draw(GameOverImage, new Vector2(0, 0), Color.White);

                        //Отрисовка кнопки возвращения в главное меню
                        GameOverBut1.Draw(spriteBatch);

                        // Отрисовка конечного колличества набранных очков
                        spriteBatch.DrawString(hud.playerScoreFont, "" + hud.playerScore.ToString(), new Vector2(240, 423), Color.Wheat);
                        
                        break; //Остановка выполненения состояния экрана - Конец игры.
                    }

                case State.Help: //Состояние экрана - Помощь:
                    {
                        //Отрисовка фонового изображения
                        spriteBatch.Draw(HelpImage, new Vector2(0, 0), Color.White);

                        HelpBut1.Draw(spriteBatch); //Отрисовка кнопки возвращения в главное меню

                        break; //Остановка выполненения состояния экрана - Конец игры.
                    }

                case State.Autor: //Состояние экрана - Об авторе:
                    {
                        spriteBatch.Draw(AutorImage, new Vector2(0, 0), Color.White);
                        HelpBut1.Draw(spriteBatch);
                        break; //Остановка выполненения состояния экрана - Конец игры.
                    }
            }
            spriteBatch.End();//Завершение отрисовки
            base.Draw(gameTime);
        }

        public void LoadBarrier() //Загрузка препятствия Health
        {
            int randX1 = random.Next(50, 500);    //Назначение рандомного значения для координаты X
            int randY1 = random.Next(-1200, -50); //Назначение рандомного значения для координаты Y

            int randX2 = random.Next(50, 500);    //Назначение рандомного значения для координаты X
            int randY2 = random.Next(-1200, -50); //Назначение рандомного значения для координаты Y

            int randX3 = random.Next(50, 500);    //Назначение рандомного значения для координаты X
            int randY3 = random.Next(-4200, -50); //Назначение рандомного значения для координаты Y

            if(brList.Count() < countbarrier)    //Условие: если число препятсвий на экране <3, то
            {
                brList.Add(new Barrier(Content.Load<Texture2D>("pr_1"), new Vector2(randX1, randY1))); //Добавить ещё одно
                brList1.Add(new Barrier1(Content.Load<Texture2D>("pr_1_1"), new Vector2(randX1, randY1))); //Добавить ещё одно
            }

            if (brList2.Count() < countbarrier)    //Условие: если число препятсвий на экране <3, то
            {
                brList2.Add(new Barrier2(Content.Load<Texture2D>("pr_2"), new Vector2(randX2, randY2))); //Добавить ещё одно
            }

            if (HealthList.Count() < countbarrier)    //Условие: если число препятсвий на экране <3, то
            {
                HealthList.Add(new Health(Content.Load<Texture2D>("health"), new Vector2(randX3, randY3))); //Добавить ещё одно
            }

            for (int i = 0; i < brList.Count; i++) //Занесение препятствий в массив
            {
                if(!brList[i].isVisible)
                {
                    brList.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < brList1.Count; i++) //Занесение препятствий в массив
            {
                if (!brList1[i].isVisible)
                {
                    brList1.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < brList2.Count; i++) //Занесение препятствий в массив
            {
                if (!brList2[i].isVisible)
                {
                    brList2.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < HealthList.Count; i++) //Занесение препятствий в массив
            {
                if (!HealthList[i].isVisible)
                {
                    HealthList.RemoveAt(i);
                    i--;
                }
            }
        }

        public void MenageExplosion() //Менеджер взрывов
        {
            for (int i = 0; i < exList.Count; i++) //Занесение взрывов в массив
            {
                if(!exList[i].isVisible)
                {
                    exList.RemoveAt(i);
                    i--;
                }

            }
        }
    }
} //Да да да это 1000-ная строка :)