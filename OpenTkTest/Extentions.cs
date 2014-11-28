using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK;

namespace OpenTkTest
{
    public static class Extentions
    {
        public static Color Blend(this Color color, Color blendColor){
            return Color.FromArgb((color.A + blendColor.A) / 2, (color.R + blendColor.R) / 2, (color.G + blendColor.G) / 2, (color.B + blendColor.B) / 2);
        }

        public static Vector2 toScreenCoords(this Vector2 position, Vector2 screenExtents)
        {
            float x = position.X / (screenExtents.X/2);
            float y = position.Y / (screenExtents.Y/2);
            return new Vector2(x-1, y-1);
        }
    }
}
