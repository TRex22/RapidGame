using Microsoft.Xna.Framework;

namespace RapidXNA.Interfaces
{
    /// <summary>
    /// This is the interface used to create new services for RapidXNA.
    /// </summary>
    public interface IRapidService
    {
        /// <summary>
        /// Over-ridable XNA Init function which is called after the Engine is set (so full engine access can be given in this function)
        /// <remarks>Note: Services will probably never need an actual Load(), but Init() can serve that purpose</remarks>
        /// </summary>
        void Init();
        /// <summary>
        /// Over-ridable Normal XNA Update function.
        /// For more information please refer to <see href="http://msdn.microsoft.com/en-us/library/microsoft.xna.framework.game.update.aspx">XNA Documentation</see>
        /// </summary>
        void Update(GameTime gameTime);
        /// <summary>
        /// Over-ridable Normal XNA Draw function.
        /// For more information please refer to <see href="http://msdn.microsoft.com/en-us/library/microsoft.xna.framework.game.draw.aspx">XNA Documentation</see>
        /// </summary>
        void Draw(GameTime gameTime);
    }
}
