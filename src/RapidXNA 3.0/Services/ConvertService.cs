//using System.Drawing;

namespace RapidXNA.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class ConvertHelper
    {
        /*TODO JMC Properly Implement*/
        /*public string SystemColourToName(Color colour)
        {
            return colour.Name;
        }

        public Color NameToSystemColour(string name)
        {
            return Color.FromName(name);
        }

        public void SystemColourToComponets(Color colour, out byte r, out byte g, out byte b, out byte a)
        {
            r = colour.R;
            g = colour.G;
            b = colour.B;
            a = colour.A;
        }

        public T ColourNameReflection<T>(string name)
        {
            var colour = default(T);
            var colorProperties = typeof(T).GetProperties(BindingFlags.Static | BindingFlags.Public);

            foreach (var info in colorProperties.Where(info => info.Name == name))
            {
                colour = (T)info.GetValue(null, null);
                //break;
            }

            return colour;
        }*/

        /*TODO JMC Properly Implement
         found: http://thedeadpixelsociety.com/2012/01/hex-colors-in-xna/*/
        /// <summary>
        /// Creates an ARGB hex string representation of the <see cref="Color"/> value.
        /// </summary>
        /// <param name="color">The <see cref="Color"/> value to parse.</param>
        /// <param name="includeHash">Determines whether to include the hash mark (#) character in the string.</param>
        /// <returns>A hex string representation of the specified <see cref="Color"/> value.</returns>
        /*public static string ColourToHex(Color color, bool includeHash)*/
        //
            /*string[] argb = {
                color.A.ToString("X2"),
                color.R.ToString("X2"),
                color.G.ToString("X2"),
                color.B.ToString("X2"),
            };
            return (includeHash ? "#" : string.Empty) + string.Join(string.Empty, argb);*/
            //throw new NotImplementedException();
        //}

        /// Creates a <see cref="Color"/> value from an ARGB or RGB hex string.  The string may
        /// begin with or without the hash mark (#) character.
        /// </summary>
        /// <param name="hexString">The ARGB hex string to parse.</param>
        /// <returns>
        /// A <see cref="Color"/> value as defined by the ARGB or RGB hex string.
        /// </returns>
        /// <exception cref="InvalidOperationException">Thrown if the string is not a valid ARGB or RGB hex value.</exception>
        //public static Color HexToColour(string hexString)
        //{
            /*if (hexString.StartsWith("#"))
                hexString = hexString.Substring(1);
            uint hex = uint.Parse(hexString, System.Globalization.NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            Color color = Color.White;
            if (hexString.Length == 8)
            {
                color.A = (byte)(hex >> 24);
                color.R = (byte)(hex >> 16);
                color.G = (byte)(hex >> 8);
                color.B = (byte)(hex);
            }
            else if (hexString.Length == 6)
            {
                color.R = (byte)(hex >> 16);
                color.G = (byte)(hex >> 8);
                color.B = (byte)(hex);
            }
            else
            {
                throw new InvalidOperationException("Invald hex representation of an ARGB or RGB color value.");
            }
            return color;*/
        //    throw new NotImplementedException();
        //}
    }
}
