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
    public class Hell //Класс уничтожителя
    {
        //Переменные
        public Rectangle boundingBox; //Переменная прямоугольника
        public Texture2D texture;     //Переменная текстуры
        public Vector2 position;      //Переменная позиции на экране
        public bool isCollidingHell;  //Переменная коллизии (столкновения)
        
        public Hell() //Конструктор контента
        {
            texture = null;                  //Значения 
            position = new Vector2(0, 618); //Стартовая позиция игрока (его авто)
            isCollidingHell = false;         //Значение коллизии = false
        }

        public void LoadContent(ContentManager ContentHell) //Загрузка контента
        {
            texture = ContentHell.Load<Texture2D>("14"); //Загрузка текстуры
        }

        public void Draw(SpriteBatch spriteBatch) //Отрисовка контента
        {
            spriteBatch.Draw(texture, position, Color.White); //Отрисовка уничтожителя
        }

        public void Update(GameTime GameTimePlayer) //Обновление контента
        {
            KeyboardState keyState = Keyboard.GetState(); //Получение состояние клавиатуры
            
            //Обновление позиции уничтожителя на экране (строка ниже):
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
    }
} //45 строк