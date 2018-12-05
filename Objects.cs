using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace LeaningCSharp2_Lesson1
{
    abstract class BaseObject
    {
        protected Point Pos;
        protected Point Dir;
        protected Size Size;

        static BaseObject(){}

        public BaseObject( Point pos, Point dir, Size size )
        {
            Pos = pos;
            Dir = dir;
            Size = size;
        }
        public virtual void Draw()
        {
            Game.Buffer.Graphics.DrawEllipse( Pens.White, Pos.X, Pos.Y, Size.Width, Size.Height );
        }
        public virtual void Update()
        {
            Pos.X = Pos.X + Dir.X;
            Pos.Y = Pos.Y + Dir.Y;
            if (Pos.X < 0) Dir.X = -Dir.X;
            if (Pos.X > Game.Width) Dir.X = -Dir.X;
            if (Pos.Y < 0) Dir.Y = -Dir.Y;
            if (Pos.Y > Game.Height) Dir.Y = -Dir.Y;
        }
    }

    class Star : BaseObject
    {
        private Image image;

        public Star( Point pos, Point dir, Size size ) : base( pos, dir, size )
        {
            image = new Bitmap(Assembly.GetEntryAssembly().GetManifestResourceStream( "LeaningCSharp2_Lesson1.Resources.Star.png" ) );
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage( image, new Rectangle( this.Pos, this.Size ) );
        }

        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }
    }

    class Asteroid : BaseObject
    {
        static private Image[] imageColl;
        static private Random rand;
        private Image image;

        static Asteroid()
        {
            imageColl = new Image[2];
            imageColl[0] = new Bitmap( Assembly.GetEntryAssembly().GetManifestResourceStream( "LeaningCSharp2_Lesson1.Resources.Asteroid1.png" ) );
            imageColl[1] = new Bitmap( Assembly.GetEntryAssembly().GetManifestResourceStream( "LeaningCSharp2_Lesson1.Resources.Asteroid2.png" ) );
            rand = new Random(0);
        }

        public Asteroid( Point pos, Point dir, Size size ) : base( pos, dir, size )
        {
            image = imageColl[rand.Next(0, 2)];
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage( image, new Rectangle( this.Pos, this.Size ) );
        }

        public override void Update()
        {
            base.Update();
        }
    }

    class Ufo : BaseObject
    {
        private static Image image;
        private static Random rand;

        static Ufo()
        {
            image = new Bitmap( Assembly.GetEntryAssembly().GetManifestResourceStream( "LeaningCSharp2_Lesson1.Resources.UFO.png" ) );
            rand = new Random();
        }

        public Ufo( Point pos, Point dir, Size size ) : base( pos, dir, size )
        {
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage( image, new Rectangle( this.Pos, this.Size ) );
        }

        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X > Game.Width)
            {
                Pos.X = 0;
                Pos.Y = rand.Next(Game.Height);
            }
        }
    }

    class StarDust : BaseObject
    {
        public StarDust( Point pos, Point dir, Size size ) : base( pos, dir, size )
        {
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine( Pens.White, Pos.X, Pos.Y, Pos.X + Size.Width, Pos.Y + Size.Height );
            Game.Buffer.Graphics.DrawLine( Pens.White, Pos.X + Size.Width, Pos.Y, Pos.X, Pos.Y + Size.Height );
        }

        public override void Update()
        {
            Pos.X = Pos.X - Dir.X;
            if (Pos.X < 0) Pos.X = Game.Width + Size.Width;
        }
    }
}