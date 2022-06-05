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
    public class BackHud //Класс интерфейса
    {
        //Переменные
        public Texture2D texture; //Перменная текстуры
        public Vector2 position;  //Переменная позиции на экране

        public BackHud() //Конструктор контента
        {
            texture = null;               //Значение текстуры = ничего
            position = new Vector2(0, 0); //Позиция на экране
        }

        public void LoadContent(ContentManager ContentPlayer) //Загрузка контента
        {
            texture = ContentPlayer.Load<Texture2D>("InterfaceImage"); //Загрузка текстуры интерфейса
        }

        public void Draw(SpriteBatch SpriteBatchBackHud) //Отрисовка контента
        {
            SpriteBatchBackHud.Draw(texture, position, Color.White); //Отрисовка интерфейса
        }
    }
} //34 строки
