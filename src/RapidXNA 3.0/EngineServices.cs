using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using RapidXNA.Models;
using RapidXNA.Services;
/*TODO JMC Make sure models are truly models*/
namespace RapidXNA
{
    /// <summary>
    /// The RapidXNA core services.
    /// </summary>
    public class EngineServices
    {
        private readonly RapidEngine _rapidEngine;
        /*TODO JMC Implement QuadTree Service and Convert service and others*/
        //private readonly ConvertService _convertService;

        /// <summary>
        /// The default constructor instantiates a local object of the RapidEngine.
        /// </summary>
        /// <param name="rapidEngine"></param>
        public EngineServices(RapidEngine rapidEngine)
        {
            _rapidEngine = rapidEngine; 
        }

        /// <summary>
        /// This function should theoretically kill the main instance of the game and the RapidEngine.
        /// There is a bug in RapidXNA 3.0 where this may not happen.
        /// Rather use the XNA exit function to kill the game.
        /// </summary>
        /// <param name="game"></param>
        /// <exception cref="InvalidOperationException">
        /// This function should theoretically kill the main instance of the game and the RapidEngine.
        /// There is a bug in RapidXNA 3.0 where this may not happen.
        /// Rather use the XNA exit function to kill the game.
        /// </exception>
        public void Exit(Game game)
        {
            try
            {
                GC.Collect();
                game.Exit();
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException("The current platform does not allow games to exit.", e);
            }
        }

        private readonly Dictionary<int, RapidService> _services = new Dictionary<int, RapidService>();

        /// <summary>
        /// Seeding value for Service IDs
        /// </summary>
        private int _serviceSeed;

        /// <summary>
        /// Gives the first instance found of a Service type
        /// </summary>
        /// <typeparam name="T">The type of service you need to access</typeparam>
        /// <returns>The first instance of type T in services</returns>
        public T First<T>()
        {
            for (var i = 0; i < _serviceSeed; i++)
            {
                if (_services[i].GetType() == typeof(T))
                {
                    return (T)Convert.ChangeType(_services[i], typeof(T), null);
                }
            }

            /*TODO JMC Throw proper logic exception and document it*/
            throw new Exception("You need to add a service of T to be able to retrieve the first service.");
        }

        /// <summary>
        /// Retrieve a service by ID
        /// </summary>
        /// <param name="i">The Service ID to retrieve</param>
        /// <returns>The service of ID</returns>
        public RapidService this[int i]
        {
            get
            {
                if (_services.ContainsKey(i))
                    return _services[i];

                throw new Exception("Service instance (" + i + ") does not exist.");
            }
        }

        /// <summary>
        /// Add a service to the Services structure
        /// </summary>
        /// <param name="service">The Service instance to add</param>
        /// <returns>The ID of the added service</returns>
        public int Add(RapidService service)
        {
            _services.Add(_serviceSeed, service);
            service.Engine = _rapidEngine;
            service.Init();

            _serviceSeed++;
            return _serviceSeed - 1;
        }

        /// <summary>
        /// Update all the services
        /// - Updates ScreenService last for most up-to-date input snapshots
        /// </summary>
        public void Update(GameTime gameTime)
        {
            for (var i = 1; i < _services.Count; i++)
            {
                _services[i].Update(gameTime);
            }
            //Update the ScreenService last
            _services[0].Update(gameTime);
        }

        /// <summary>
        /// Draws each service that has DrawEnabled=true
        /// - No special ordering
        /// </summary>
        public void Draw(GameTime gameTime)
        {
            foreach (var service in _services.Values.Where(service => service.DrawEnabled))
            {
                service.Draw(gameTime);
            }
        }
    }
}
