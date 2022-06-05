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
    public class Level1 //Класс дороги
    {
        //Переменные
        public Texture2D texture;  //Переменная текстуры
        public Vector2 pos1, pos2; //Переменная позиции на экране
        public int speed;   //Переменная скорости

        public Level1() //Конструктор контента
        {
            texture = null;               //Значние текстуры = ничего
            pos1 = new Vector2(0, 0);     //Стартовая позиция
            pos2 = new Vector2(0, -1200); //Дополнительная позиция
            speed = 4;     //Значание скорости
        }

        public void LoadContent(ContentManager ContentLevel1) //Загрузка контента
        {
            texture = ContentLevel1.Load<Texture2D>("lvl_1"); //Загрузка текстуры дороги
        }

        public void Draw(SpriteBatch SpriteBatchLevel1) //Отрисовка контента
        {
            SpriteBatchLevel1.Draw(texture, pos1, Color.White); //Отрисовка в стартовой позиции
            SpriteBatchLevel1.Draw(texture, pos2, Color.White); //Отрисовка в дополнительной позиции
        }

        public void Update(GameTime GameTimePlayer) //Обновление
        {
            pos1.Y = pos1.Y + speed; //Обновление координат для первой отрисовки
            pos2.Y = pos2.Y + speed; //Обновление координат для второй отрисовки

            if(pos1.Y >= 1200)       //Условие: если значние первой отрисовки по Y >= 1200, то
            {
                pos1.Y = 0;     //Значение по Y для первой = 0
                pos2.Y = -1200; //Значение по Y для второй = -1200
            }
        }
    }
} //50 строк