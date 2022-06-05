using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;


namespace WindowsGame3
{
    public class SoundManager              //Класс менеджера звуков
    {
        //Объявление переменных
        public Song radio1;                //Переменная для 1ой мелодии радио
        public Song radio2;                //Переменная для 2ой мелодии радио
        public Song radio3;                //Переменная для 3ой мелодии радио
        public Song radio4;                //Переменная для 4ой мелодии радио
        public Song radio5;                //Переменная для 5ой мелодии радио
        public Song radio6;                //Переменная для 6ой мелодии радио
        public SoundEffect explodeSound;   //Переменная для звука разрушения 1го вида препятствия
        public SoundEffect explodeSound2;  //Переменная для звука разрушения 2го вида препятствия
        public SoundEffect destroyerSound; //Переменная для звука уничтожителя
        public SoundEffect HealthSound;    //Переменная для звука сердечка

        public SoundManager()              //Конструктор контента
        {
            radio1 = null;                 //Значение = ничего
            radio2 = null;                 //Значение = ничего
            radio3 = null;                 //Значение = ничего
            radio4 = null;                 //Значение = ничего
            radio5 = null;                 //Значение = ничего
            radio6 = null;                 //Значение = ничего
            explodeSound = null;           //Значение = ничего
            explodeSound2 = null;          //Значение = ничего
            HealthSound = null;            //Значение = ничего
        }

        public void LoadContent(ContentManager Content)                   //Загрузчик контента
        {
            radio1 = Content.Load<Song>("radio1s");                       //Загрузка 1ой мелодии радио
            radio2 = Content.Load<Song>("radio2s");                       //Загрузка 2ой мелодии радио
            radio3 = Content.Load<Song>("radio3s");                       //Загрузка 3ой мелодии радио
            radio4 = Content.Load<Song>("radio4s");                       //Загрузка 4ой мелодии радио
            radio5 = Content.Load<Song>("radio5s");                       //Загрузка 5ой мелодии радио
            radio6 = Content.Load<Song>("radio6s");                       //Загрузка 6ой мелодии радио
            explodeSound = Content.Load<SoundEffect>("explode");          //Загрузка звука разрушения 1го вида препятствия
            explodeSound2 = Content.Load<SoundEffect>("break");           //Загрузка звука разрушения 2го вида препятствия
            destroyerSound = Content.Load<SoundEffect>("soundDestroyer"); //Загрузка звука уничтожителя
            HealthSound = Content.Load<SoundEffect>("HealthSound");       //Загрузка звука сердечка
        }
    }
} //56 строк