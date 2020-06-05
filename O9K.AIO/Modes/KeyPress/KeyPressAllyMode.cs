namespace O9K.AIO.Modes.KeyPress
{
    using Core.Managers.Menu.EventArgs;

    using Heroes.Base;

    internal abstract class KeyPressAllyMode : KeyPressMode
    {
        protected KeyPressAllyMode(BaseHero baseHero, KeyPressModeMenu menu)
            : base(baseHero, menu)
        {
        }

        protected override void KeyOnValueChanged(object sender, KeyEventArgs e)
        {
            if (e.NewValue)
            {
                if (this.LockTarget)
                {
                    var target = this.TargetManager.ClosestAllyHeroToMouse(this.Owner);
                    this.TargetManager.ForceSetTarget(target);
                    this.TargetManager.TargetLocked = true;
                }

                this.UpdateHandler.IsEnabled = true;
            }
            else
            {
                this.UpdateHandler.IsEnabled = false;
                if (this.LockTarget)
                {
                    this.TargetManager.TargetLocked = false;
                }
            }
        }
    }
}