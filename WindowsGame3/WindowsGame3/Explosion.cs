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
    public class Explosion //Класс взрывов
    {
        //Переменные
        public Texture2D texture;                           //Переменная текстуры
        public Vector2 position;                            //Переменная позиции на экране
        public float timer;                                 //Переменная таймера
        public float interval;                              //Переменная интервала таймера
        public Vector2 origin;                              //Переменная позиции на экране
        public int currentFrame, spriteWidth, spriteHeight; //Переменные формы, высоты, ширины
        public Rectangle sourceRect;                        //Переменная прямоугольника
        public bool isVisible;                              //Переменная отображения на экране

        public Explosion(Texture2D newTexture, Vector2 newPosition) //Конструктор контента
        {
            position = newPosition; //Объявление новой переменной для позиции
            texture = newTexture;   //Объявление новой переменной для текстуры
            timer = 0f;             //Значение таймера
            interval = 10f;         //Значенеи интервала таймера
            currentFrame = 1;       //Значенеи формы
            spriteWidth = 128;      //Ширина спрайта взрыва
            spriteHeight = 128;     //Высота спрайта взрыва
            isVisible = true;       //Отображать = да
        }

        public void LoadContent(ContentManager Content) //Загрузчик контента
        {
        }

        public void Update(GameTime gameTime) //Обновление контента
        {
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds; //Обновление значения таймера

            if(timer > interval) //Условие обновление таймера через интервал
            {
                currentFrame++;
                timer = 0f;
            }

            if(currentFrame == 17) //Условие обновление формы
            {
                isVisible = false;
                currentFrame = 0;
            }

            sourceRect = new Rectangle(currentFrame * spriteWidth, 0, spriteWidth, spriteHeight); //Обновление позиции
            origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);                    //Обновление позиции
        }

        public void Draw(SpriteBatch spriteBatch) //Отрисовка контента
        {
            if (isVisible == true) //Условие: если отображение = true, то
                //Отрисовка спрайта взрыва (строка ниже):
                spriteBatch.Draw(texture, position, sourceRect, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0);
        } 
    }
} //67 строк