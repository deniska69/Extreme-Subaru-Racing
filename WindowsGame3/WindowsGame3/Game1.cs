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
        public enum State //���������� ��������� ������ (������� ����)
        {
            Menu,         //����
            Playing,      //����
            Pause,        //�����
            Help,         //������
            Autor,        //�� ������
            GameOver,     //����� ���� (��������)
            Exit          //�����
        }

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        //����� ������� � ���������� ����������
        HUD hud = new HUD();                            //���������� ��� �������� �����
        Player p = new Player();                        //���������� ��� ���� ������
        Random random = new Random();                   //���������� ��� �������
        Level1 lvl1 = new Level1();                     //���������� ��� ������ � �������
        BackHud bh = new BackHud();                     //���������� ��� ������ ����������
        List<Barrier> brList = new List<Barrier>();     //���������� ��� ������� 1 ���� �����������
        List<Barrier1> brList1 = new List<Barrier1>();  //���������� ��� ������� ����� � �������� ��� 1 ���� �����������
        List<Barrier2> brList2 = new List<Barrier2>();  //���������� ��� ������� 2 ���� �����������
        List<Health> HealthList = new List<Health>();   //���������� ��� ������� ��������
        Hell hell = new Hell();                         //���������� ��� ������� ����������� �����������
        List<Explosion> exList = new List<Explosion>(); //���������� ��� ������ �������
        SoundManager sm = new SoundManager();           //���������� ��� ������ ������
        State CurrentGameState = State.Menu;            //���������� ��� ��������� ����
        public Texture2D bgMenu;                        //���������� ��� �������� ������� � ����
        public Texture2D GameOverImage;                 //���������� ��� ���������� ���������
        public int health, countbarrier, countHealth;   //���������� ��� �������� ��������, ����������� ���������� ������� ����, ��������
        public Texture2D healthTexture;                 //���������� ��� ������� ��������
        public Texture2D hellTop;                       //���������� ��� �������� �������� ���� �������������
        public Texture2D PauseImage;                    //���������� ��� ����������� �����
        public Texture2D HelpImage;                     //���������� ��� �������� ����������� � ���� ������
        public Texture2D AutorImage;                    //���������� ��� �������� ����������� � ���� �� ������
        public int radio;                               //���������� ��� �������� ������ ����� �� �����
        public Texture2D radioTexture;                  //���������� ��� �������� �����
        public Rectangle radioRectangle;                //���������� ��� �������������� �����
        public Vector2 radioPosition;                   //���������� ��� ������� �����
        
        //���������� ��� ������
        MenuBut1 MenuBut1;
        MenuBut2 MenuBut2;
        MenuBut3 MenuBut3;
        MenuBut4 MenuBut4;
        GameOverBut1 GameOverBut1;
        HelpBut1 HelpBut1;
        PauseButton2 PauseButton2;

        public Game1()                                  //����������� ��������
        {
            graphics = new GraphicsDeviceManager(this);

            IsMouseVisible = true;                       //��������� ������� ���� � ����
            graphics.IsFullScreen = false;               //������ �������� �� ���� �����
            bgMenu = null;                               //�������� ���������� = ������
            GameOverImage = null;                        //�������� ���������� = ������
            health = 5;                                  //�������� ��������
            countbarrier = 1;                            //�������� ����������� ���������� ������� ���� �� ������
            countHealth = 1;                             //�������� ����������� �������� �� ������
            hellTop = null;                              //�������� ���������� = ������
            PauseImage = null;                           //�������� ���������� = ������
            HelpImage = null;                            //�������� ���������� = ������
            graphics.PreferredBackBufferWidth = 800;     //������� �������� ����
            graphics.PreferredBackBufferHeight = 600;
            radioTexture = null;                         //�������� ���������� = ������
            radioPosition = new Vector2(600, 456);       //������� �����

            this.Window.Title = "ESR - Extreme Subaru Racing v.1.0.0"; //�������� � ����� �������� ����

            Content.RootDirectory = "Content"; //���������������� �������� ����� ��� ������ ������� � �������� ����� �������
        }

        protected override void Initialize()   //�������������
        {
            base.Initialize();
        }

        protected override void LoadContent()                         //��������� ��������
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            hud.LoadContent(Content);                                 //�������� �������� �����
            lvl1.LoadContent(Content);                                //�������� ������   
            p.LoadContent(Content);                                   //�������� ���� ������ 
            bh.LoadContent(Content);                                  //�������� ����������
            hell.LoadContent(Content);                                //�������� ������������
            sm.LoadContent(Content);                                  //�������� ��������� ������
            bgMenu = Content.Load<Texture2D>("MenuImage");            //�������� �������� ����������� ��� ����
            GameOverImage = Content.Load<Texture2D>("GameOverImage"); //�������� �������� ����������� ��� ���� ���������
            hellTop = Content.Load<Texture2D>("hellTop");             //�������� �������� �������� ���� �������������
            PauseImage = Content.Load<Texture2D>("PauseImage");       //�������� �������� �����
            HelpImage = Content.Load<Texture2D>("HelpImage");         //�������� �������� �������� ����������� � ���� ������
            AutorImage = Content.Load<Texture2D>("Autor");            //�������� �������� �������� ����������� � ���� �� ������
            radio = 1;                                                //���������� ��� �������� ������ ����� �� �����


            //�������� ������� ��� ������ � ����
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

        protected override void UnloadContent() //�������� ��������
        {
        }

        protected override void Update(GameTime gameTime) //���������� ��������
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            MouseState mouse = Mouse.GetState(); //��������� ��������� ����

            switch (CurrentGameState)            //������������� ��������� ������
            {
                case State.Playing:              //��������� ������ - ����:
                    {
                        KeyboardState keyState = Keyboard.GetState(); //��������� ��������� ����������

                        if (keyState.IsKeyDown(Keys.Escape)) //������� ������� ������ ����� � ����������
                        {
                            CurrentGameState = State.Pause;  //���������� ����
                            MediaPlayer.Pause();             //��������������� ������� ������
                        }

                        if (keyState.IsKeyDown(Keys.D1)) //������� ��������������� 1 ����� �� �����
                        {
                            MediaPlayer.Play(sm.radio1);
                            radio = 1;
                        }

                        if (keyState.IsKeyDown(Keys.D2)) //������� ��������������� 2 ����� �� �����
                        {
                            MediaPlayer.Play(sm.radio2);
                            radio = 2;
                        }

                        if (keyState.IsKeyDown(Keys.D3)) //������� ��������������� 3 ����� �� �����
                        {
                            MediaPlayer.Play(sm.radio3);
                            radio = 3;
                        }

                        if (keyState.IsKeyDown(Keys.D4)) //������� ��������������� 4 ����� �� �����
                        {
                            MediaPlayer.Play(sm.radio4);
                            radio = 4;
                        }

                        if (keyState.IsKeyDown(Keys.D5)) //������� ��������������� 5 ����� �� �����
                        {
                            MediaPlayer.Play(sm.radio5);
                            radio = 5;
                        }

                        if (keyState.IsKeyDown(Keys.D6)) //������� ��������������� 5 ����� �� �����
                        {
                            MediaPlayer.Play(sm.radio6);
                            radio = 6;
                        }

                        if (keyState.IsKeyDown(Keys.D7)) //������� ������������ ��������������� �����
                        {
                            MediaPlayer.Stop();
                            radio = 7;
                        }

                        foreach (Explosion ex in exList) //���������� �������
                        {
                            ex.Update(gameTime);
                        }

                        p.Update(gameTime);              //���������� ��������� ���� ������

                        foreach (Health a in HealthList) //���������� �����������
                        {
                            //��������� ��������  � ����������� �������� �� ������
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

                            if (a.boundingBox.Intersects(hell.boundingBox)) //������� ������������ ���������� � �������������
                            {
                                sm.destroyerSound.Play();                   //�������� ����� ������
                                a.isVisible = false;                        //����������� ��������� � ������ 
                            }

                            if (a.boundingBox.Intersects(p.boundingBox))    //������� ������������ ����������� � ���� ������
                            {
                                sm.HealthSound.Play();                      //��������������� ����� ������

                                if (health <= 4)                            //������� ��� ����������� �������� ��������
                                    health = health + 1;

                                if (health == 5)                            //������� �� ������������ �������� ��������
                                    health = health + 0;

                                a.isVisible = false;                        //����������� ��������� � ������
                            }
                            a.Update(gameTime);                             //���������� ��������� �����������
                        }

                        foreach (Barrier a in brList)                       //���������� �����������
                        {
                            //��������� �������� � ����������� ����������� �� ������
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

                            if (a.boundingBox.Intersects(hell.boundingBox)) //������� ������������ ���������� � �������������
                            {
                                sm.destroyerSound.Play();             //�������� ����� ������
                                hud.playerScore += 1;                 //+1 ����� � ������� �����
                                a.isVisible = false;                  //����������� ��������� � ������ 
                            }

                            if (a.boundingBox.Intersects(p.boundingBox)) //������� ������������ ����������� � ���� ������
                            {
                                sm.explodeSound.Play();                  //��������������� ����� ������

                                //��������������� �������� ������ (������ ����):
                                exList.Add(new Explosion(Content.Load<Texture2D>("explosion1"), new Vector2(a.position.X + 35, a.position.Y + 17)));
                                
                                health -= 1;                         //-1 ������ (����) ��������
                                a.isVisible = false;                 //����������� ��������� � ������
                            }
                            a.Update(gameTime);                      //���������� ��������� �����������
                        }


                        foreach (Barrier2 a in brList2)              //���������� 2 ���� �����������
                        {
                            //��������� ��������  � ����������� ����������� �� ������
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

                            if (a.boundingBox.Intersects(hell.boundingBox)) //������� ������������ ���������� � �������������
                            {
                                sm.destroyerSound.Play();                   //�������� ����� ������
                                hud.playerScore += 1;                       //+1 ����� � ������� �����
                                a.isVisible = false;                        //����������� ��������� � ������ 
                            }

                            if (a.boundingBox.Intersects(p.boundingBox))    //������� ������������ ����������� � ���� ������
                            {
                                sm.explodeSound2.Play();                    //��������������� ����� ������

                                //��������������� �������� ������ (������ ����):
                                exList.Add(new Explosion(Content.Load<Texture2D>("explosion2"), new Vector2(a.position.X + 20, a.position.Y + 18)));

                                health -= 1;                                //-1 ������ (����) ��������
                                a.isVisible = false;                        //����������� ��������� � ������
                            }
                            a.Update(gameTime);                             //���������� ��������� �����������
                        }

                        foreach (Barrier1 a in brList1)                     //���������� �����������
                        {
                            //��������� �������� � ����������� ����������� �� ������
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

                            if (a.boundingBox.Intersects(hell.boundingBox)) //������� ������������ ���������� � �������������
                            {
                                a.isVisible = false;                        //����������� ��������� � ������     
                            }

                            a.Update(gameTime);                             //���������� ��������� �����������
                        }

                        lvl1.Update(gameTime);                              //���������� ��������� ������
                        hell.Update(gameTime);                              //���������� ��������� ������������
                        LoadBarrier();                                      //���������� �����������
                        MenageExplosion();                                  //���������� ��������� �������

                        break;                                              //��������� ������������ ��������� ������ - ����
                    }

                case State.Pause: //��������� ������ - �����:
                    {
                        KeyboardState keyState = Keyboard.GetState(); //��������� ��������� ����������

                        if (keyState.IsKeyDown(Keys.Enter))           //������� ������� 1�� ������
                        {
                            CurrentGameState = State.Playing;         //���������� ����
                            MediaPlayer.Resume();                     //��������������� ������� ������
                        }

                        //������� ����� ������ ������
                        if (PauseButton2.IsClicked == true) CurrentGameState = State.Menu;    
                        PauseButton2.Update(mouse);

                        if (PauseButton2.IsClicked == true)     //������� ������� ������ ������
                        {
                            CurrentGameState = State.Menu;      //����� � ����
                            MediaPlayer.Stop();                 //������������ ������� ������
                            lvl1.speed = 4;                     //����� �������� ��������� ������
                            hud.playerScore = 0;                //��������� �������� �����
                            countbarrier = 1;                   //��������� �������� ����������� ����������� �� ������
                            health = 5;                         //������� ��������� �������� ��������
                            brList.Clear();                     //�������� ������ �����������
                            brList1.Clear();                    //�������� ������ �����������
                            brList2.Clear();                    //�������� ������ �����������
                            exList.Clear();                     //�������� ������ �������
                            p.position = new Vector2(300, 500); //��������� ��������� ������� ������ (��� ����)
                            countHealth = 1;                    //��������� �������� ��������
                            radio = 1;                          //�������������� ������ ����� �� �����
                        }

                        if (keyState.IsKeyDown(Keys.Escape))    //�������, ���-�� �� ���� ���� �� Esc
                        {
                            CurrentGameState = State.Pause;     //���������� ��������� �����
                            MediaPlayer.Pause();                //��������������� ������� ������
                        }
                        break;                                  //��������� ������������ ��������� ������ - ����.
                    }
                
                case State.Menu: //��������� ������ - ����:
                    {
                        //������� �� ������� ������ � ����
                        if (MenuBut1.IsClicked == true)  CurrentGameState = State.Playing;
                        MenuBut1.Update(mouse);

                        if (MenuBut1.IsClicked == true)  //������� ������� ������ ������
                        {
                            MediaPlayer.Play(sm.radio1); //��������������� �����
                            countbarrier = 1;
                        }

                        if (MenuBut2.IsClicked == true) CurrentGameState = State.Help;
                        MenuBut2.Update(mouse);

                        if (MenuBut3.IsClicked == true) CurrentGameState = State.Autor;
                        MenuBut3.Update(mouse);

                        if (MenuBut4.IsClicked == true) CurrentGameState = State.Exit; 
                        MenuBut4.Update(mouse);

                        lvl1.Update(gameTime);                        //���������� ��������� ������

                        break;                                        //��������� ������������ ��������� ������ - ����.
                    }
                    
                case State.GameOver: //��������� ������ - ����� ����:
                    {
                        MediaPlayer.Stop();   //��������� ��������������� ������� ������

                        //������� �� ������� ������ � ���� ���������
                        if (GameOverBut1.IsClicked == true) CurrentGameState = State.Menu;
                        GameOverBut1.Update(mouse);

                        if (GameOverBut1.IsClicked == true)     //������� ������� ������
                        {
                            lvl1.speed = 4;                     //����� �������� ��������� ������
                            hud.playerScore = 0;                //��������� �������� �����
                            countbarrier = 1;                   //��������� �������� ����������� ����������� �� ������
                            health = 5;                         //������� ��������� �������� ��������
                            brList.Clear();                     //�������� ������ �����������
                            brList1.Clear();                    //�������� ������ �����������
                            brList2.Clear();                    //�������� ������ �����������
                            exList.Clear();                     //�������� ������ �������
                            p.position = new Vector2(300, 500); //��������� ��������� ������� ������ (��� ����)
                            countHealth = 1;                    //��������� �������� ��������
                            radio = 1;                          //�������������� ������ ����� �� �����
                        }

                        break; //��������� ������������ ��������� ������ - ����� ����
                    }

                case State.Help: //��������� ������ - ������:
                    {
                        //������� �� ������� ������ � ���� ������
                        if (HelpBut1.IsClicked == true) CurrentGameState = State.Menu;
                        HelpBut1.Update(mouse);

                        if (HelpBut1.IsClicked == true)
                        { }

                        break; //��������� ������������ ��������� ������ - ����� ����.
                    }

                case State.Autor: //��������� ������ - �� ������:
                    {
                        //������� �� ������� ����rb � ���� ������
                        if (HelpBut1.IsClicked == true) CurrentGameState = State.Menu;
                        HelpBut1.Update(mouse);

                        if (HelpBut1.IsClicked == true)
                        { }

                        break; //��������� ������������ ��������� ������ - ����� ����.
                    }

                case State.Exit: //��������� ������ - ����� �� ����
                    {
                        this.Exit(); //����� �� ����������
                        break;
                    }
            }

            if (health <= 0)   //�������: ��� 0 �������� ��������� ����� ���������
            {
                MediaPlayer.Stop();
                CurrentGameState = State.GameOver;
            }

            //���������� ��������� ������
            MenuBut1.Update(mouse);
            MenuBut2.Update(mouse);
            MenuBut3.Update(mouse);
            MenuBut4.Update(mouse);
            GameOverBut1.Update(mouse);
            HelpBut1.Update(mouse);
            PauseButton2.Update(mouse);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) //��������� ��������
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();      //������ ���������

            switch (CurrentGameState) //������������� ��������� ������
            {
                    
                case State.Playing:   //��������� ������ - ����:
                    {
                        //������� �������� �������� ��������
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

                        //������� ������������ ����� �� �����
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

                        lvl1.Draw(spriteBatch);          //��������� ������

                        foreach (Barrier1 a in brList1)  //��������� ����� � �������� ����� ������
                        {
                            a.Draw(spriteBatch);
                        }

                        foreach (Barrier2 a in brList2)    //��������� �����������
                        {
                            a.Draw(spriteBatch);
                        }

                        foreach (Barrier a in brList)     //��������� �����������
                        {
                            a.Draw(spriteBatch);
                        }

                        p.Draw(spriteBatch);              //��������� ���� ������

                        foreach (Explosion ex in exList)  //��������� ������
                        {
                            ex.Draw(spriteBatch);
                        }

                        foreach (Health ex in HealthList) //��������� ��������
                        {
                            ex.Draw(spriteBatch);
                        }
                        
                        //��������� �������� ��� ������������� (������ ����):
                        spriteBatch.Draw(hellTop, new Vector2(50, 565), Color.White); 

                        bh.Draw(spriteBatch);            //��������� ����������
                        hud.Draw(spriteBatch);           //��������� �������� �����

                        //��������� �������� (������ ����):
                        spriteBatch.Draw(healthTexture, p.healthPosition, Color.White);

                        //��������� ����� (������ ����):
                        spriteBatch.Draw(radioTexture, radioPosition, Color.White);

                        break;                           //��������� ������������ ��������� ������ - ����.
                    }

                case State.Pause: //��������� ������ - �����:
                    {
                        lvl1.Draw(spriteBatch);          //��������� ������

                        foreach (Barrier1 a in brList1)  //��������� �����������
                        {
                            a.Draw(spriteBatch);
                        }

                        foreach (Barrier2 a in brList2)  //��������� �����������
                        {
                            a.Draw(spriteBatch);
                        }

                        foreach (Barrier a in brList)    //��������� �����������
                        {
                            a.Draw(spriteBatch);
                        }

                        p.Draw(spriteBatch);             //��������� ���� ������

                        foreach (Explosion ex in exList) //��������� ������
                        {
                            ex.Draw(spriteBatch);
                        }

                        foreach (Health ex in HealthList) //��������� ��������
                        {
                            ex.Draw(spriteBatch);
                        }

                        //��������� �������� ��� ������������� (������ ����):
                        spriteBatch.Draw(hellTop, new Vector2(50, 565), Color.White); 

                        //��������� �������� ����������� ���� (������ ����):
                        spriteBatch.Draw(PauseImage, new Vector2(0, 0), Color.White); 

                        PauseButton2.Draw(spriteBatch);  //��������� 2�� ������

                        break;                           //��������� ������������ ��������� ������ - ����.
                    }

                case State.Menu: //��������� ������ - ����:
                    {
                        //��������� �������� ����������� ���� (������ ����):
                        spriteBatch.Draw(bgMenu, new Vector2(0, 0), Color.White); 

                        //��������� ������ � ����
                        MenuBut1.Draw(spriteBatch);
                        MenuBut2.Draw(spriteBatch);
                        MenuBut3.Draw(spriteBatch);
                        MenuBut4.Draw(spriteBatch);

                        break; //��������� ������������ ��������� ������ - ����.
                    }

                case State.GameOver: //��������� ������ - ����� ����:
                    {
                        //��������� �������� ����������� ���� ���������
                        spriteBatch.Draw(GameOverImage, new Vector2(0, 0), Color.White);

                        //��������� ������ ����������� � ������� ����
                        GameOverBut1.Draw(spriteBatch);

                        // ��������� ��������� ����������� ��������� �����
                        spriteBatch.DrawString(hud.playerScoreFont, "" + hud.playerScore.ToString(), new Vector2(240, 423), Color.Wheat);
                        
                        break; //��������� ������������ ��������� ������ - ����� ����.
                    }

                case State.Help: //��������� ������ - ������:
                    {
                        //��������� �������� �����������
                        spriteBatch.Draw(HelpImage, new Vector2(0, 0), Color.White);

                        HelpBut1.Draw(spriteBatch); //��������� ������ ����������� � ������� ����

                        break; //��������� ������������ ��������� ������ - ����� ����.
                    }

                case State.Autor: //��������� ������ - �� ������:
                    {
                        spriteBatch.Draw(AutorImage, new Vector2(0, 0), Color.White);
                        HelpBut1.Draw(spriteBatch);
                        break; //��������� ������������ ��������� ������ - ����� ����.
                    }
            }
            spriteBatch.End();//���������� ���������
            base.Draw(gameTime);
        }

        public void LoadBarrier() //�������� ����������� Health
        {
            int randX1 = random.Next(50, 500);    //���������� ���������� �������� ��� ���������� X
            int randY1 = random.Next(-1200, -50); //���������� ���������� �������� ��� ���������� Y

            int randX2 = random.Next(50, 500);    //���������� ���������� �������� ��� ���������� X
            int randY2 = random.Next(-1200, -50); //���������� ���������� �������� ��� ���������� Y

            int randX3 = random.Next(50, 500);    //���������� ���������� �������� ��� ���������� X
            int randY3 = random.Next(-4200, -50); //���������� ���������� �������� ��� ���������� Y

            if(brList.Count() < countbarrier)    //�������: ���� ����� ���������� �� ������ <3, ��
            {
                brList.Add(new Barrier(Content.Load<Texture2D>("pr_1"), new Vector2(randX1, randY1))); //�������� ��� ����
                brList1.Add(new Barrier1(Content.Load<Texture2D>("pr_1_1"), new Vector2(randX1, randY1))); //�������� ��� ����
            }

            if (brList2.Count() < countbarrier)    //�������: ���� ����� ���������� �� ������ <3, ��
            {
                brList2.Add(new Barrier2(Content.Load<Texture2D>("pr_2"), new Vector2(randX2, randY2))); //�������� ��� ����
            }

            if (HealthList.Count() < countbarrier)    //�������: ���� ����� ���������� �� ������ <3, ��
            {
                HealthList.Add(new Health(Content.Load<Texture2D>("health"), new Vector2(randX3, randY3))); //�������� ��� ����
            }

            for (int i = 0; i < brList.Count; i++) //��������� ����������� � ������
            {
                if(!brList[i].isVisible)
                {
                    brList.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < brList1.Count; i++) //��������� ����������� � ������
            {
                if (!brList1[i].isVisible)
                {
                    brList1.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < brList2.Count; i++) //��������� ����������� � ������
            {
                if (!brList2[i].isVisible)
                {
                    brList2.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < HealthList.Count; i++) //��������� ����������� � ������
            {
                if (!HealthList[i].isVisible)
                {
                    HealthList.RemoveAt(i);
                    i--;
                }
            }
        }

        public void MenageExplosion() //�������� �������
        {
            for (int i = 0; i < exList.Count; i++) //��������� ������� � ������
            {
                if(!exList[i].isVisible)
                {
                    exList.RemoveAt(i);
                    i--;
                }

            }
        }
    }
} //�� �� �� ��� 1000-��� ������ :)