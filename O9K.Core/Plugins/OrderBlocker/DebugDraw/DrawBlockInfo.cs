namespace O9K.Core.Plugins.OrderBlocker.DebugDraw
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Renderer;

    using Logger;

    using Managers.Menu.EventArgs;

    using SharpDX;

    using Color = System.Drawing.Color;

    internal class DrawBlockInfo : IDisposable
    {
        private readonly Dictionary<string, string> localized = new Dictionary<string, string>();

        private readonly OrderBlockerMenu menu;

        private readonly IRenderManager renderManager;

        private readonly Dictionary<int, int> apms = new Dictionary<int, int>();

        private int count;

        private readonly HashSet<OrderId> notApmOrders = new HashSet<OrderId>
        {
            OrderId.None,
            OrderId.Hold,
            OrderId.VectorTargetPosition,
        };

        private string order = "None";

        public DrawBlockInfo(IRenderManager renderManager, OrderBlockerMenu menu)
        {
            this.renderManager = renderManager;
            this.menu = menu;

            menu.ShowInfo.ValueChange += this.ShowInfoOnValueChange;
        }

        public void Blocked(ExecuteOrderEventArgs args, string info)
        {
            this.count++;
            this.order = this.GetLocalizedName(args) + " (" + args.OrderId + ")(" + info + ")";
        }

        public void Dispose()
        {
            this.menu.ShowInfo.ValueChange -= this.ShowInfoOnValueChange;
            this.renderManager.Draw -= this.OnDraw;
        }

        public void IncreaseAPM(ExecuteOrderEventArgs args)
        {
            if (this.notApmOrders.Contains(args.OrderId))
            {
                return;
            }

            var key = (int)(Game.RawGameTime / 60);

            if (this.apms.ContainsKey(key))
            {
                this.apms[key]++;
            }
            else
            {
                this.apms[key] = 1;
            }
        }

        private int GetAPM()
        {
            var apmCount = this.apms.Count;
            if (this.apms.Count == 0)
            {
                return 0;
            }

            return this.apms.Values.Sum() / apmCount;
        }

        private string GetLocalizedName(ExecuteOrderEventArgs args)
        {
            try
            {
                if (args.Ability != null)
                {
                    var abilityName = args.Ability.Name;

                    if (!this.localized.TryGetValue(abilityName, out var name))
                    {
                        this.localized[abilityName] = name = LocalizationHelper.LocalizeAbilityName(abilityName);
                    }

                    return name;
                }
                else
                {
                    var unitName = args.Entities.First().Name;

                    if (!this.localized.TryGetValue(unitName, out var name))
                    {
                        this.localized[unitName] = name = LocalizationHelper.LocalizeName(unitName);
                    }

                    return name;
                }
            }
            catch
            {
                return "unknown";
            }
        }

        private void OnDraw(IRenderer renderer)
        {
            try
            {
                var apm = this.GetAPM();
                var t0 = "APM: " + apm;
                var t0Size = renderer.MeasureText(t0, 18);
                var rec0 = new RectangleF(10, 240, t0Size.X + 8, t0Size.Y);
                renderer.DrawFilledRectangle(rec0, Color.Black);
                rec0.X += 4;
                renderer.DrawText(rec0, t0, apm < 350 ? Color.White : Color.Red, RendererFontFlags.Left, 18);

                var t1 = "Blocked: " + this.count;
                var t1Size = renderer.MeasureText(t1, 18);
                var rec1 = new RectangleF(10, rec0.Y + t0Size.Y, t1Size.X + 8, t1Size.Y);
                renderer.DrawFilledRectangle(rec1, Color.Black);
                rec1.X += 4;
                renderer.DrawText(rec1, t1, Color.White, RendererFontFlags.Left, 18);

                var t2 = this.order;
                var t2Size = renderer.MeasureText(t2, 18);
                var rec2 = new RectangleF(10, rec1.Y + t1Size.Y, t2Size.X + 8, t2Size.Y);
                renderer.DrawFilledRectangle(rec2, Color.Black);
                rec2.X += 4;
                renderer.DrawText(rec2, t2, Color.White, RendererFontFlags.Left, 18);
            }
            catch (InvalidOperationException)
            {
                // ignore
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void ShowInfoOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                this.renderManager.Draw += this.OnDraw;
            }
            else
            {
                this.renderManager.Draw -= this.OnDraw;
            }
        }
    }
}