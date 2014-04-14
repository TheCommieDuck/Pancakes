using System;
using Microsoft.Xna.Framework;

namespace WaffleCat.Core.Graphics
{
    /// <summary>
    /// Extension class for extra window functionality.
    /// </summary>
    public static class WindowExtensions
    {
        /// <summary>
        /// Set the position of a window on screen.
        /// </summary>
        /// <param name="position">A screen position to move the window to.</param>
        public static void SetPosition(this GameWindow window, Point position)
        {
            OpenTK.GameWindow OTKWindow = GetForm(window);
            if (OTKWindow != null)
            {
                OTKWindow.X = position.X;
                OTKWindow.Y = position.Y;
                OTKWindow.Height = 400;
            }
        }

        /// <summary>
        /// Get the OpenTK game window from a MonoGame window.
        /// </summary>
        /// <returns>The OpenTK window.</returns>
        public static OpenTK.GameWindow GetForm(this GameWindow gameWindow)
        {
            Type type = typeof(OpenTKGameWindow);
            System.Reflection.FieldInfo field = type.GetField("window", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (field != null)
                return field.GetValue(gameWindow) as OpenTK.GameWindow;
            return null;
        }
    }
}
