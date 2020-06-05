namespace O9K.Core.Managers.Renderer.Utils
{
    using System;

    using SharpDX;

    public struct Rectangle9
    {
        public Size2F Size;

        public Vector2 Location;

        public static Rectangle9 Zero
        {
            get
            {
                return new Rectangle9
                {
                    IsZero = true
                };
            }
        }

        public Rectangle9 MoveTopBorder(float offset)
        {
            return new Rectangle9(this.Location.X, this.Location.Y - offset, this.Size.Width, this.Size.Height + offset);
        }

        public Rectangle9 MoveBottomBorder(float offset)
        {
            return new Rectangle9(this.Location.X, this.Location.Y, this.Size.Width, this.Size.Height + offset);
        }

        public Rectangle9 MoveRightBorder(float offset)
        {
            return new Rectangle9(this.Location.X, this.Location.Y, this.Size.Width + offset, this.Size.Height);
        }

        public Rectangle9 MoveLeftBorder(float offset)
        {
            return new Rectangle9(this.Location.X + offset, this.Location.Y, this.Size.Width - offset, this.Size.Height);
        }

        public Rectangle9 SinkToBottomRight(float width, float height)
        {
            return this.SinkToBottomRight(new Size2F(width, height));
        }

        public Rectangle9 SinkToBottomRight(Size2F size)
        {
            return new Rectangle9(this.Right - size.Width, this.Bottom - size.Height, size);
        }

        public Rectangle9 SinkToBottomRight(Vector2 size)
        {
            return this.SinkToBottomRight(new Size2F(size.X, size.Y));
        }

        public Rectangle9 SinkToBottomLeft(float width, float height)
        {
            return this.SinkToBottomLeft(new Size2F(width, height));
        }

        public Rectangle9 SinkToBottomLeft(Size2F size)
        {
            return new Rectangle9(this.X, this.Bottom - size.Height, size);
        }

        public Rectangle9 SinkToBottomLeft(Vector2 size)
        {
            return this.SinkToBottomLeft(new Size2F(size.X, size.Y));
        }

        public bool IsZero { get; private set; }

        public float X
        {
            get
            {
                return this.Location.X;
            }
            set
            {
                this.Location.X = value;
            }
        }

        public Vector2 TopLeft
        {
            get
            {
                return new Vector2(this.Left, this.Top);
            }
        }

        public Vector2 TopRight
        {
            get
            {
                return new Vector2(this.Right, this.Top);
            }
        }

        public Vector2 BottomLeft
        {
            get
            {
                return new Vector2(this.Left, this.Bottom);
            }
        }

        public Vector2 BottomRight
        {
            get
            {
                return new Vector2(this.Right, this.Bottom);
            }
        }

        public bool Contains(Vector2 position)
        {
            return position.X >= this.Left && position.X <= this.Right && position.Y >= this.Top && position.Y <= this.Bottom;
        }

        public float Left
        {
            get
            {
                return this.Location.X;
            }
            set
            {
                this.Location.X = value;
            }
        }

        public float Width
        {
            get
            {
                return this.Size.Width;
            }
            set
            {
                this.Size.Width = value;
            }
        }

        public float Height
        {
            get
            {
                return this.Size.Height;
            }
            set
            {
                this.Size.Height = value;
            }
        }

        public Vector2 Center
        {
            get
            {
                return new Vector2(this.Location.X + (this.Size.Width / 2), this.Location.Y + (this.Size.Height / 2));
            }
        }

        public float Right
        {
            get
            {
                return this.Location.X + this.Size.Width;
            }
            set
            {
                this.Location.X = value;
            }
        }

        public float Top
        {
            get
            {
                return this.Location.Y;
            }
        }

        public float Bottom
        {
            get
            {
                return this.Location.Y + this.Size.Height;
            }
        }

        public float Y
        {
            get
            {
                return this.Location.Y;
            }
            set
            {
                this.Location.Y = value;
            }
        }

        public Rectangle9(float x, float y, float width, float height)
        {
            this.Location = new Vector2(x, y);
            this.Size = new Size2F(width, height);
            this.IsZero = false;
        }

        public Rectangle9(Vector2 location, Size2F size)
        {
            this.Location = location;
            this.Size = size;
            this.IsZero = false;
        }

        public Rectangle9(Vector2 location, Vector2 size)
        {
            this.Location = location;
            this.Size = new Size2F(size.X, size.Y);
            this.IsZero = false;
        }

        public Rectangle9(float x, float y, Size2F size)
        {
            this.Location = new Vector2(x, y);
            this.Size = size;
            this.IsZero = false;
        }

        public Rectangle9(float x, float y, Vector2 size)
        {
            this.Location = new Vector2(x, y);
            this.Size = new Size2F(size.X, size.Y);
            this.IsZero = false;
        }

        public Rectangle9(Vector2 location, float width, float height)
        {
            this.Location = location;
            this.Size = new Size2F(width, height);
            this.IsZero = false;
        }

        public static Rectangle9 operator +(Rectangle9 rec, Size2F size)
        {
            return new Rectangle9(rec.Location.X, rec.Location.Y, rec.Size.Width + size.Width, rec.Size.Height + size.Height);
        }

        public static Rectangle9 operator *(Rectangle9 rec, Size2F size)
        {
            var widthSize = rec.Size.Width * size.Width;
            var heightSize = rec.Size.Height * size.Height;

            return new Rectangle9(
                rec.Location.X + ((rec.Width - widthSize) / 2f),
                rec.Location.Y + ((rec.Height - heightSize) / 2f),
                widthSize,
                heightSize);
        }

        public static Rectangle9 operator +(Rectangle9 rec, Vector2 location)
        {
            return new Rectangle9(rec.Location.X + location.X, rec.Location.Y + location.Y, rec.Size);
        }

        public static Rectangle9 operator *(Rectangle9 rec, Vector2 location)
        {
            return new Rectangle9(rec.Location.X * location.X, rec.Location.Y * location.Y, rec.Size);
        }

        public static Rectangle9 operator +(Rectangle9 rec, float size)
        {
            return new Rectangle9(rec.Location.X - size, rec.Location.Y - size, rec.Size.Width + (size * 2), rec.Size.Height + (size * 2));
        }

        public static Rectangle9 operator -(Rectangle9 rec, float size)
        {
            return new Rectangle9(rec.Location.X + (size / 2), rec.Location.Y + (size / 2), rec.Size.Width - size, rec.Size.Height - size);
        }

        public static Rectangle9 operator *(Rectangle9 rec, float size)
        {
            return rec * new Size2F(size, size);
        }

        public static implicit operator RectangleF(Rectangle9 rec)
        {
            return new RectangleF(rec.Location.X, rec.Location.Y, rec.Size.Width, rec.Size.Height);
        }

        public static Rectangle9 operator +(Rectangle9 rec1, Rectangle9 rec2)
        {
            var left = Math.Min(rec1.Left, rec2.Left);
            var right = Math.Max(rec1.Right, rec2.Right);
            var top = Math.Min(rec1.Top, rec2.Top);
            var bottom = Math.Max(rec1.Bottom, rec2.Bottom);
            return new Rectangle9(left, top, right - left, bottom - top);
        }

        public override string ToString()
        {
            return $"X:{this.X} Y:{this.Y} Width:{this.Width} Height:{this.Height}";
        }
    }
}