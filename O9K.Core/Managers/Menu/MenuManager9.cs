namespace O9K.Core.Managers.Menu
{
    using System;
    using System.ComponentModel.Composition;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using Ensage.SDK.Renderer;

    using EnsageSharp.Sandbox;

    using Input;
    using Input.EventArgs;
    using Input.Keys;

    using Items;

    using Logger;

    [Export(typeof(IMenuManager9))]
    public sealed class MenuManager9 : IMenuManager9, IDisposable
    {
        private readonly IInputManager9 inputManager;

        private readonly MainMenu mainMenu;

        private readonly Key menuHoldKey;

        private readonly Key menuToggleKey;

        private readonly IRenderManager renderer;

        private bool menuVisible;

        [ImportingConstructor]
        public MenuManager9(IInputManager9 inputManager, IRenderManager renderer)
        {
            this.inputManager = inputManager;
            this.renderer = renderer;

            this.mainMenu = new MainMenu(renderer, inputManager);

            if (SandboxConfig.Config.HotKeys.TryGetValue("Menu", out var menuKey))
            {
                this.menuHoldKey = KeyInterop.KeyFromVirtualKey(menuKey);
            }

            if (SandboxConfig.Config.HotKeys.TryGetValue("MenuToggle", out menuKey))
            {
                this.menuToggleKey = KeyInterop.KeyFromVirtualKey(menuKey);
            }

            inputManager.KeyDown += this.OnKeyDown;
        }

        public void AddRootMenu(Menu menu)
        {
            try
            {
                this.mainMenu.Add(menu);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public void Dispose()
        {
            this.inputManager.MouseKeyDown -= this.OnMouseKeyDown;
            this.inputManager.MouseKeyUp -= this.OnMouseKeyUp;
            this.inputManager.MouseMove -= this.OnMouseMove;
            this.inputManager.MouseWheel -= this.OnMouseWheel;
            this.inputManager.KeyDown -= this.OnKeyDown;
            this.inputManager.KeyUp -= this.OnKeyUp;
            this.renderer.Draw -= this.OnDraw;
        }

        public void RemoveRootMenu(Menu menu)
        {
            try
            {
                this.mainMenu.Remove(menu);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnDraw(IRenderer render)
        {
            try
            {
                this.mainMenu.DrawMenu(render);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == this.menuToggleKey)
            {
                if (this.menuVisible)
                {
                    this.Unsubscribe();
                    this.SaveMenu();
                }
                else
                {
                    this.Subscribe();
                }
            }
            else if (!this.menuVisible && e.Key == this.menuHoldKey)
            {
                this.Subscribe();
            }
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != this.menuHoldKey)
            {
                return;
            }

            this.Unsubscribe();
            this.SaveMenu();
        }

        private void OnMouseKeyDown(object sender, MouseEventArgs e)
        {
            if (e.MouseKey != MouseKey.Left)
            {
                return;
            }

            if (this.mainMenu.OnMousePress(e.ScreenPosition))
            {
                e.Process = false;
            }
        }

        private void OnMouseKeyUp(object sender, MouseEventArgs e)
        {
            if (e.MouseKey != MouseKey.Left)
            {
                return;
            }

            if (this.mainMenu.OnMouseRelease(e.ScreenPosition))
            {
                e.Process = false;
            }
        }

        private void OnMouseMove(object sender, MouseMoveEventArgs e)
        {
            try
            {
                if (this.mainMenu.OnMouseMove(e.ScreenPosition))
                {
                    e.Process = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            try
            {
                if (this.mainMenu.OnMouseWheel(e.Position, e.Up))
                {
                    e.Process = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void SaveMenu()
        {
            Task.Run(
                () =>
                    {
                        try
                        {
                            lock (this.mainMenu)
                            {
                                this.mainMenu.Save();
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Error(e);
                        }
                    });
        }

        private void Subscribe()
        {
            this.inputManager.MouseKeyDown += this.OnMouseKeyDown;
            this.inputManager.MouseKeyUp += this.OnMouseKeyUp;
            this.inputManager.MouseMove += this.OnMouseMove;
            this.inputManager.MouseWheel += this.OnMouseWheel;
            this.inputManager.KeyUp += this.OnKeyUp;
            this.renderer.Draw += this.OnDraw;
            this.menuVisible = true;
        }

        private void Unsubscribe()
        {
            this.inputManager.MouseKeyDown -= this.OnMouseKeyDown;
            this.inputManager.MouseKeyUp -= this.OnMouseKeyUp;
            this.inputManager.MouseMove -= this.OnMouseMove;
            this.inputManager.MouseWheel -= this.OnMouseWheel;
            this.inputManager.KeyUp -= this.OnKeyUp;
            this.renderer.Draw -= this.OnDraw;
            this.menuVisible = false;
        }
    }
}