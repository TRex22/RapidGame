using Microsoft.Xna.Framework;
using RapidXNA.Interfaces;

namespace RapidXNA.Models
{
    /// <summary>
    /// This is the model which produces other services for RapidXNA.
    /// </summary>
    public abstract class RapidService : IRapidService
    {
        /// <summary>
        /// Engine instance for the Service to use
        /// </summary>
        public RapidEngine Engine;

        /// <summary>
        /// Used to adjust the order in which services need to be updated.
        /// </summary>
        public int UpdateOrder;

        /// <summary>
        /// Used to adjust the order in which services need to be drawn.
        /// </summary>
        public int DrawOrder;

        /// <summary>
        /// Over-ridable XNA Init function which is called after the Engine is set (so full engine access can be given in this function)
        /// <remarks>Note: Services will probably never need an actual Load(), but Init() can serve that purpose</remarks>
        /// </summary>
        public abstract void Init();
        /// <summary>
        /// Over-ridable Normal XNA Update function.
        /// For more information please refer to <see href="http://msdn.microsoft.com/en-us/library/microsoft.xna.framework.game.update.aspx">XNA Documentation</see>
        /// </summary>
        public abstract void Update(GameTime gameTime);
        /// <summary>
        /// Over-ridable Normal XNA Draw function.
        /// For more information please refer to <see href="http://msdn.microsoft.com/en-us/library/microsoft.xna.framework.game.draw.aspx">XNA Documentation</see>
        /// </summary>
        public abstract void Draw(GameTime gameTime);

        /// <summary>
        /// DrawEnabled: tells RapidEngine if this Service needs to draw anything
        /// </summary>
        public bool DrawEnabled = false;
    }
}