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
    public class Health //Класс сердечек на дороге
    {
        //Переменные
        public Rectangle boundingBox; //Перемення прямоугольника
        public Texture2D texture;     //Переменная текстуры
        public Vector2 position;      //Переменная позиции на экране
        public int speed;             //Переменная скорости
        public bool isVisible;        //Переменная отображения
        Random random = new Random(); //Переменная рандома
        public float randX, randY;    //Переменные координат X и Y

        public Health(Texture2D newTexture, Vector2 newPosition) //Конструткор
        {
            position = newPosition;          //Назначение новой переменной
            texture = newTexture;            //Назначение новой переменной
            isVisible = true;                //Значение переменной отображение на true
            speed = 4;                       //Скорость движения препятствия (задавать равной скорости движения дороги)
            randX = random.Next(50, 400);    //Присвоения значения для координаты X 
            randY = random.Next(-4500, -50); //Присвоения значения для координаты Y
        }

        public void LoadConten(ContentManager ContentBarrier) //Загрузчик контента
        {
        }

        public void Update(GameTime gameTimeBarrier) //Обновление контента
        {
            boundingBox = new Rectangle((int)position.X, (int)position.Y, 27, 27); //Обновление координат позиции препятствия

            position.Y = position.Y + speed; //Позиция по Y + значение скорости

            if (position.Y >= 705) //Если позиция по Y >= 705
                position.Y = -4500;  //То возвращать на Y = -50
        }

        public void Draw(SpriteBatch spriteBatchBarrier) //Отрисовка контента
        {
            if (isVisible) //Если переменная отображения = true, то
                spriteBatchBarrier.Draw(texture, position, Color.White); //Отрисовка препятствия
        }
    }
} //53 строки