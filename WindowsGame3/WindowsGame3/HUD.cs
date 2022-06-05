using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGame3
{
    public class HUD //Класс счётчика очков
    {
        //Переменные
        public int playerScore, speed,screenHeight, screenWidth, radio; //Переменные счётчика очков, высоты, ширины
        public SpriteFont playerScoreFont;                              //Переменная для шрифта
        public Vector2 playerScorePos, speedPos;                        //Переменная позиции на экране
        public bool showHUD;                                            //Переменная отображения

        public HUD() //Конструктор контента
        {
            playerScore = 0;                        //Значение счётчки по-умолчанию = 0
            speed = 0;                              //
            radio = 1;                              //
            showHUD = true;                         //Отображать = да
            screenHeight = 600;                     //Размеры игрового экрана (ширина)
            screenWidth = 800;                      //Размеры игрового экрана (высота)
            playerScoreFont = null;                 //Так и не понял зачем это, но надо
            playerScorePos = new Vector2(705, 187); //Позиция счётчика на экране
            speedPos = new Vector2(630, 335);       //Позиция показателя скорости на экране
        }

        public void LoadContent(ContentManager Content)            //Загрузчик контента
        {
            playerScoreFont = Content.Load<SpriteFont>("GetVoIP"); //Загрузка шрифта
        }

        public void Update(GameTime gameTime)             //Обновление контента
        {
            KeyboardState keyState = Keyboard.GetState(); //Получение состояние клавиатуры (не знаю, зачем оно тут нужно)
        }

        public void Draw(SpriteBatch spriteBatch)         //Отрисовка контента
        {
            if (showHUD) //Если переменная отображения = true, то
                //Отрисовка счётчика (строка ниже):
                spriteBatch.DrawString(playerScoreFont, "" + playerScore, playerScorePos, Color.Wheat);

            if (showHUD) //Если переменная отображения = true, то
                //Отрисовка счётчика (строка ниже):
                spriteBatch.DrawString(playerScoreFont, "" + speed, speedPos, Color.Wheat);
        }
    }
} //54 строки