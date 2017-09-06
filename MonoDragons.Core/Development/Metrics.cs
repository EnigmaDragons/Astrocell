using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.Entities;
using MonoDragons.Core.PhysicsEngine;
using MonoDragons.Core.Render;
using MonoDragons.Core.Text;

namespace MonoDragons.Core.Development
{
    public sealed class Metrics
    {
        public static GameObject Enable()
        {
            return Entity.Create(new Transform2 {Size = new Size2(
                    GameInstance.GraphicsDevice.PresentationParameters.BackBufferWidth, 
                    GameInstance.GraphicsDevice.PresentationParameters.BackBufferHeight) })
                .Add(new TextDisplay
                {
                    Align = TextAlign.TopRight,
                    Color = Color.Yellow,
                    Text = () =>$"FPS: {FrameRate.PerSecond:00} " +
                                $"UPS: {UpdateRate.PerSecond:00} " +
                                $"Ent: {Entity.Count:0000} " +
                                $"Res: {Entity.ResourceCount:0000}"
                });
        }
    }
}
