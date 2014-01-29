using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using RapidXNA_3._0.Interfaces;

namespace RapidXNA_3._0
{
    public class EngineServices
    {
        private readonly RapidEngine _rapidEngine;
        public EngineServices(RapidEngine rapidEngine)
        {
            _rapidEngine = rapidEngine;
        }


        private readonly Dictionary<int, GameService> _services = new Dictionary<int, GameService>();

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

            throw new Exception("You need to add a service of T to be able to retrieve the first service.");
        }

        /// <summary>
        /// Retrieve a service by ID
        /// </summary>
        /// <param name="i">The Service ID to retrieve</param>
        /// <returns>The service of ID</returns>
        public GameService this[int i]
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
        public int Add(GameService service)
        {
            _services.Add(_serviceSeed, service);
            service.Engine = _rapidEngine;
            service.Init();

            _serviceSeed++;
            return _serviceSeed - 1;
        }

        /// <summary>
        /// Update all the services
        /// - Updates screenservice last for most up-to-date input snapshots
        /// </summary>
        public void Update(GameTime gameTime)
        {
            for (int i = 1; i < _services.Count; i++)
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
            foreach (GameService service in _services.Values)
            {
                if (service.DrawEnabled)
                {
                    service.Draw(gameTime);
                }
            }
        }
    }
}
