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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        KeyboardState ks;
        public Pacman pacboy;
        public Sprite block;
        public Sprite wall;
        public Sprite inviswall;
        public Sprite EscapeTile;
        public Minotaur[] minotaurs;
        public bool Win1 = false;
        public bool Win2 = false;
        public bool Win3 = false;
        public bool win1 = false;
        public bool win2 = false;
        public bool win3 = false;


        Enums.Levels currentLevel;

        public static Random random = new Random();


        int offset = 22;
        Tile[,] obstacleGrid;

        Dictionary<Enums.Levels, int[,]> levels = new Dictionary<Enums.Levels, int[,]>();

        public int blockPosX = 250;
        public int blockPosY = 250;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 900;
            graphics.PreferredBackBufferWidth = 900;
            graphics.ApplyChanges();
        }


        public List<Rectangle> frames = new List<Rectangle>()
        {
            new Rectangle(152, 2, 73, 73),
            new Rectangle(152, 77, 73, 73),
            new Rectangle(152, 152, 73, 73),
            new Rectangle(152, 77, 73, 73),
        };



        protected override void Initialize()
        {
            obstacleGrid = new Tile[18, 18];





            base.Initialize();
        }

        void addLevels()
        {

            levels.Add(Enums.Levels.Clear, new int[,] {
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 } });


            levels.Add(Enums.Levels.One, new int[,] {
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1 },
            { 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 0, 0, 1, 0, 1, 0, 1 },
            { 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1 },
            { 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 },
            { 1, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 0, 1, 0, 1 },
            { 1, 0, 1, 1, 0, 0, 0, 1, 2, 1, 0, 0, 0, 1, 0, 1, 0, 1 },
            { 1, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 1, 1, 1, 0, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1, 0, 1 },
            { 1, 0, 0, 0, 1, 0, 1, 0, 1, 1, 1, 0, 0, 0, 0, 1, 0, 1 },
            { 1, 1, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 0, 0, 1, 0, 1 },
            { 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1 },
            { 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 1 },
            { 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 1, 1, 1, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },});



            levels.Add(Enums.Levels.OneInsane, new int[,] {
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
            { 2, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 2 },
            { 2, 0, 0, 0, 0, 2, 2, 2, 0, 0, 0, 2, 0, 2, 0, 2, 0, 2 },
            { 2, 2, 0, 2, 2, 2, 0, 2, 2, 2, 0, 0, 0, 2, 0, 2, 0, 2 },
            { 2, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 2, 2, 0, 2, 0, 2 },
            { 2, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2, 0, 2 },
            { 2, 0, 0, 0, 0, 2, 0, 2, 0, 2, 0, 2, 2, 2, 0, 2, 0, 2 },
            { 2, 0, 2, 2, 0, 0, 0, 2, 2, 2, 0, 0, 0, 2, 0, 2, 0, 2 },
            { 2, 0, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
            { 2, 0, 2, 2, 2, 0, 2, 0, 0, 0, 0, 2, 2, 2, 2, 2, 0, 2 },
            { 2, 0, 0, 0, 2, 0, 2, 0, 2, 2, 2, 0, 0, 0, 0, 2, 0, 2 },
            { 2, 2, 2, 0, 2, 0, 2, 0, 2, 2, 2, 2, 2, 0, 0, 2, 0, 2 },
            { 2, 0, 0, 0, 2, 0, 2, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 2 },
            { 2, 0, 2, 2, 2, 0, 2, 2, 2, 2, 2, 0, 2, 0, 2, 2, 2, 2 },
            { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 2, 0, 0, 0, 0, 2 },
            { 2, 0, 2, 2, 2, 2, 2, 2, 2, 0, 2, 0, 2, 2, 2, 2, 0, 2 },
            { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },});


            levels.Add(Enums.Levels.Two, new int[,] {
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 },
            { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 },
            { 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1 },
            { 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1 },
            { 1, 0, 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 0, 1, 0, 1, 0, 1 },
            { 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1 },
            { 1, 2, 1, 0, 1, 0, 1, 0, 1, 1, 0, 0, 0, 1, 0, 1, 0, 1 },
            { 1, 0, 0, 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 0, 0, 1 },
            { 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 3, 1, 0, 1, 1, 1, 0, 1 },
            { 1, 0, 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 0, 1 },
            { 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 },
            { 1, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1 },
            { 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 },
            { 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },});



            levels.Add(Enums.Levels.TwoInsane, new int[,] {
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
            { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
            { 2, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 2 },
            { 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 2 },
            { 2, 0, 2, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 2, 0, 2 },
            { 2, 0, 2, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 2, 0, 2, 0, 2 },
            { 2, 0, 2, 0, 2, 0, 2, 0, 2, 2, 2, 2, 0, 2, 0, 2, 0, 2 },
            { 2, 0, 2, 0, 2, 0, 2, 0, 0, 0, 0, 2, 0, 2, 0, 2, 0, 2 },
            { 2, 2, 2, 0, 2, 0, 2, 0, 2, 2, 0, 2, 0, 2, 0, 2, 0, 2 },
            { 2, 0, 0, 0, 2, 0, 2, 0, 2, 2, 0, 2, 0, 2, 0, 0, 0, 2 },
            { 2, 0, 2, 0, 2, 0, 2, 0, 0, 0, 3, 2, 0, 2, 2, 2, 0, 2 },
            { 2, 0, 2, 0, 2, 0, 2, 2, 2, 2, 2, 2, 0, 2, 2, 2, 0, 2 },
            { 2, 0, 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 2 },
            { 2, 0, 2, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 2, 0, 2 },
            { 2, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 2 },
            { 2, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0, 2 },
            { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },});


            levels.Add(Enums.Levels.EasterEgg, new int[,] {
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },});


            levels.Add(Enums.Levels.Three, new int[,] {
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 },
            { 1, 0, 1, 1, 1, 0, 0, 1, 1, 0, 1, 1, 1, 0, 1, 0, 0, 1 },
            { 1, 0, 1, 1, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1 },
            { 1, 0, 1, 1, 2, 1, 1, 1, 0, 0, 1, 0, 0, 1, 0, 1, 0, 1 },
            { 1, 0, 1, 1, 0, 0, 0, 1, 1, 0, 0, 0, 0, 1, 0, 1, 0, 1 },
            { 1, 0, 0, 0, 0, 1, 0, 1, 1, 1, 0, 1, 1, 0, 0, 1, 0, 1 },
            { 1, 0, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 1, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 1 },
            { 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 1, 0, 1, 1, 0, 1, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 1, 0, 1 },
            { 1, 0, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0, 1, 0, 0, 1, 0, 1 },
            { 1, 0, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 0, 1, 0, 1 },
            { 1, 0, 0, 0, 1, 0, 1, 1, 1, 0, 1, 1, 0, 1, 0, 0, 0, 1 },
            { 1, 0, 1, 1, 1, 0, 0, 1, 3, 0, 0, 1, 0, 0, 1, 1, 0, 1 },
            { 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 1 },
            { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },});



            levels.Add(Enums.Levels.ThreeInsane, new int[,] {
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
            { 2, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 2 },
            { 2, 0, 2, 2, 2, 0, 0, 2, 2, 0, 2, 2, 2, 0, 2, 0, 0, 2 },
            { 2, 0, 2, 2, 0, 0, 2, 0, 0, 0, 2, 0, 2, 0, 0, 2, 0, 2 },
            { 2, 0, 2, 2, 2, 2, 2, 2, 0, 0, 2, 0, 0, 2, 0, 2, 0, 2 },
            { 2, 0, 2, 2, 0, 0, 0, 2, 2, 0, 0, 0, 0, 2, 0, 2, 0, 2 },
            { 2, 0, 0, 0, 0, 2, 0, 2, 2, 2, 0, 2, 2, 0, 0, 2, 0, 2 },
            { 2, 0, 2, 2, 2, 0, 2, 0, 0, 0, 0, 0, 2, 0, 2, 2, 0, 2 },
            { 2, 0, 0, 0, 0, 0, 2, 2, 0, 2, 2, 0, 2, 0, 0, 2, 0, 2 },
            { 2, 2, 2, 2, 2, 0, 2, 0, 0, 0, 2, 0, 2, 2, 0, 2, 0, 2 },
            { 2, 0, 0, 0, 0, 0, 0, 2, 2, 0, 2, 0, 0, 0, 0, 2, 0, 2 },
            { 2, 0, 2, 2, 2, 2, 2, 2, 2, 0, 2, 0, 2, 0, 0, 2, 0, 2 },
            { 2, 0, 2, 0, 0, 0, 0, 0, 2, 0, 2, 0, 0, 2, 0, 2, 0, 2 },
            { 2, 0, 0, 0, 2, 0, 2, 2, 2, 0, 2, 2, 0, 2, 0, 0, 0, 2 },
            { 2, 2, 2, 2, 2, 0, 0, 2, 3, 0, 0, 2, 0, 0, 2, 2, 0, 2 },
            { 2, 0, 2, 2, 2, 2, 0, 2, 2, 2, 0, 2, 0, 2, 0, 0, 0, 2 },
            { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 2, 2 },
            { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },});
        }

        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);


            currentLevel = Enums.Levels.One;
            

            if (currentLevel == Enums.Levels.EasterEgg)
            {
                for (int i = 0; i < minotaurs.Length; i++)
                {
                    minotaurs[i].Speed = new Vector2(0, 0);
                }

            }
            minotaurs = new Minotaur[5];
            for (int i = 0; i < minotaurs.Length; i++)
             {
                Color tint = new Color(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));

                minotaurs[i] = new Minotaur(Content.Load<Texture2D>("MinotaurSpriteShet"),
                    new Vector2(550 + offset, 410 + offset), tint, 1f, TimeSpan.FromMilliseconds(100), SpriteEffects.None, 0, 22);
                minotaurs[i].Speed = new Vector2(10, 10);
                minotaurs[i].direction = Enums.Direction.Left;
            }








            pacboy = new Pacman(Content.Load<Texture2D>("PacManSpriteSheet"), Content.Load<Texture2D>("labbyField"),
            new Vector2(250 + offset, 350 + offset), Color.White, 0.56f, frames, TimeSpan.FromMilliseconds(100), SpriteEffects.None, 0, 22);
            pacboy.fog = false;

            wall = new Sprite(Content.Load<Texture2D>("MossyStone"), new Vector2(0, 0), Color.White, 1f);
            inviswall = new Sprite(Content.Load<Texture2D>("MossyStone"), new Vector2(0, 0), Color.Black, 1f);
            EscapeTile = new Minotaur(Content.Load<Texture2D>("ESCAPEROOPEE"), new Vector2(540 + offset, 505 + offset), Color.White, 1f, TimeSpan.FromMilliseconds(100), SpriteEffects.None, 0, 0);
            addLevels();

            for (int i = 0; i < 18; i++)
            {
                for (int k = 0; k < 18; k++)
                {
                    if (levels[currentLevel][k, i] == 1)
                    {
                        obstacleGrid[i, k] = new Tile(i * 50, k * 50, wall);
                    }
                    else if (levels[currentLevel][k, i] == 2)
                    {
                        obstacleGrid[i, k] = new MinotaurCheckTile(i * 50, k * 50, inviswall);
                    }


                }

            }
        }

        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            ks = Keyboard.GetState();
            pacboy.Update(gameTime, ks, blockPosX, blockPosY, GraphicsDevice.Viewport);
            for (int i = 0; i < minotaurs.Length; i++)
            {
                minotaurs[i].Update(gameTime, ks, GraphicsDevice.Viewport);
            }

            if (EscapeTile.Hitbox.Intersects(pacboy.Hitbox) && currentLevel == Enums.Levels.One)
            {
                currentLevel = Enums.Levels.Two;
            }

            for (int i = 0; i < minotaurs.Length; i++)
            {
                if (minotaurs[i].Hitbox.Intersects(pacboy.Hitbox))
                {
                    Exit();
                }
            }


            if (EscapeTile.Hitbox.Intersects(pacboy.Hitbox))
            {
                Exit();
            }




            for (int i = 0; i < minotaurs.Length; i++)
            {


                if (minotaurs[i].direction == Enums.Direction.Right)
                {
                    if (minotaurs[i].Speed.X != 0)
                    {
                        minotaurs[i].Speed = new Vector2(Math.Abs(minotaurs[i].Speed.X), 0);
                    }
                }
                else if (minotaurs[i].direction == Enums.Direction.Left)
                {
                    if (minotaurs[i].Speed.X != 0)
                    {
                        minotaurs[i].Speed = new Vector2(-Math.Abs(minotaurs[i].Speed.X), 0);
                    }
                }
                else if (minotaurs[i].direction == Enums.Direction.Down)
                {
                    if (minotaurs[i].Speed.Y != 0)
                    {
                        minotaurs[i].Speed = new Vector2(0, Math.Abs(minotaurs[i].Speed.Y));
                    }
                }
                else if (minotaurs[i].direction == Enums.Direction.Up)
                {
                    if (minotaurs[i].Speed.Y != 0)
                    {
                        minotaurs[i].Speed = new Vector2(0, -Math.Abs(minotaurs[i].Speed.Y));
                    }
                }


            }

            foreach (Tile tile in obstacleGrid)
            {
                if (tile == null)
                {
                    continue;
                }
                if (tile.Hitbox().Intersects(pacboy.Hitbox))
                {

                    pacboy.HitWall();
                }

                for (int i = 0; i < minotaurs.Length; i++)
                {
                    if (minotaurs[i].Hitbox.Intersects(tile.Hitbox()))
                    {
                        minotaurs[i].HitWall();
                    }
                }
            }
        }




        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            wall.Draw(spriteBatch);
            for (int i = 0; i < minotaurs.Length; i++)
            {
                minotaurs[i].Draw(spriteBatch);
            }
            EscapeTile.Draw(spriteBatch);

            foreach (Tile tile in obstacleGrid)
            {
                if (tile == null)
                {
                    continue;
                }
                tile.Draw(spriteBatch);
            }
            pacboy.Draw(spriteBatch);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
