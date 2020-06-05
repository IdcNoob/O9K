namespace O9K.Hud.Modules.Map.Runes
{
    using Ensage;

    using SharpDX;

    internal sealed class RuneData
    {
        private Rune cachedRune;

        private bool display;

        public RuneData(Vector3 position)
        {
            this.Position = position;
        }

        public bool Display
        {
            get
            {
                if (!this.display)
                {
                    return false;
                }

                if (this.cachedRune != null)
                {
                    if (!this.cachedRune.IsValid)
                    {
                        this.RemoveRune();
                        return true;
                    }

                    if (this.cachedRune.IsVisible)
                    {
                        return false;
                    }
                }

                return true;
            }
            set
            {
                this.display = value;
            }
        }

        public Vector3 Position { get; }

        public bool RunePicked { get; private set; }

        public string Texture { get; private set; }

        public void AddRune(Rune rune, string texture)
        {
            this.cachedRune = rune;
            this.Texture = texture;
            this.Display = true;
            this.RunePicked = false;
        }

        public void RemoveRune()
        {
            this.Display = true;
            this.cachedRune = null;
            this.RunePicked = true;
        }

        public void RemoveRune(Rune rune)
        {
            if (!this.Display || this.cachedRune != rune)
            {
                return;
            }

            this.cachedRune = null;
            this.RunePicked = true;
        }
    }
}