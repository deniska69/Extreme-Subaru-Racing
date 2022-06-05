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
    public class Player //Класс авто игрока
    {
        //Переменные
        public Rectangle boundingBox, healthRectangle; //Перменные прямоугольников
        public Texture2D texture;                      //Переменные для текстур
        public Vector2 position, healthPosition;       //Переменные для позиций
        public int speed;                              //Переменные для скорости и здоровья
        public bool isCollidingPlayer;                 //Переменная для коллизии
        int texZnach = 1;                              //Переменная для скрытого режима
        
        public Player() //Конструктор
        {
            texture = null;                            //Значние текстуры авто = ничего
            position = new Vector2(300, 500);          //Стартовая позиция игрока (его авто)
            speed = 7;                                 //Скорость авто
            isCollidingPlayer = false;                 //Значение коллизии = false
            healthPosition = new Vector2(614, 262);    //Позиция здоровья на экране
        }

        public void LoadContent(ContentManager ContentPlayer)      //Загрузка контента
        {
            texture = ContentPlayer.Load<Texture2D>("auto1");
        }

        public void Draw(SpriteBatch spriteBatch)              //Отрисовка контента
        {
            spriteBatch.Draw(texture, position, Color.White);  //Отрисовка авто
        }

        public void Update(GameTime GameTimePlayer)       //Обновление контента
        {
            KeyboardState keyState = Keyboard.GetState(); //Получение состояние клавиатуры

            //Обновление позиции авто игрока на экране (строка ниже):
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);

            //Управление
            if (keyState.IsKeyDown(Keys.Up) || keyState.IsKeyDown(Keys.W))
                position.Y = position.Y - speed;
            if (keyState.IsKeyDown(Keys.Down) || keyState.IsKeyDown(Keys.S))
                position.Y = position.Y + speed;
            if (keyState.IsKeyDown(Keys.Left) || keyState.IsKeyDown(Keys.A))
                position.X = position.X - speed;
            if (keyState.IsKeyDown(Keys.Right) || keyState.IsKeyDown(Keys.D))
                position.X = position.X + speed;

            //Ограничение игровой зоны для авто игрока  
            if (position.X <= 50) position.X = 50;
            if (position.X >= 515) position.X = 515;
            if (position.Y <= 0) position.Y = 0;
            if (position.Y >= 525) position.Y = 525;
        }
    }
} //65 строк