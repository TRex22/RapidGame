using Microsoft.Xna.Framework;

namespace RapidXNA_3._0.Interfaces
{
    public abstract class GameService
    {
        /// <summary>
        /// Engine instance for the Service to use
        /// </summary>
        public RapidEngine Engine;

        /// <summary>
        /// Overidable functions
        /// - Init: called after the Engine is set (so full engine access given in this function
        /// -- Note: Services will probably never need an actual Load(), but Init() can serve that purpose
        /// - Update: usual XNA update
        /// - Draw: usual XNA draw
        /// </summary>
        public abstract void Init();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);

        /// <summary>
        /// DrawEnabled: tells RapidEngine if this Service needs to draw anything
        /// </summary>
        public bool DrawEnabled = false;
    }
}
