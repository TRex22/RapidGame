using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RapidXNA.Interfaces;
using Microsoft.Xna.Framework;

namespace RapidXNA
{
    public class EngineServices
    {
        private RapidEngine _engine;
        public EngineServices(RapidEngine engine)
        {
            _engine = engine;
        }


        private Dictionary<int, IGameService> _Services = new Dictionary<int, IGameService>();

        /// <summary>
        /// Seeding value for Service IDs
        /// </summary>
        private int _ServiceSeed = 0;

        /// <summary>
        /// Gives the first instance found of a Service type
        /// </summary>
        /// <typeparam name="T">The type of service you need to access</typeparam>
        /// <returns>The first instance of type T in services</returns>
        public T First<T>()
        {
            for (int i = 0; i < _ServiceSeed; i++)
            {
                if (_Services[i].GetType() == typeof(T))
                {
                    return (T)Convert.ChangeType(_Services[i], typeof(T), null);
                }
            }

            throw new Exception("You need to add a service of T to be able to retrieve the first service.");
        }

        /// <summary>
        /// Retrieve a service by ID
        /// </summary>
        /// <param name="i">The Service ID to retrieve</param>
        /// <returns>The service of ID</returns>
        public IGameService this[int i]
        {
            get
            {
                if (_Services.ContainsKey(i))
                    return _Services[i];

                throw new Exception("Service instance (" + i.ToString() + ") does not exist.");
            }
        }

        /// <summary>
        /// Add a service to the Services structure
        /// </summary>
        /// <param name="service">The Service instance to add</param>
        /// <returns>The ID of the added service</returns>
        public int Add(IGameService service)
        {
            _Services.Add(_ServiceSeed, service);
            service.Engine = _engine;
            service.Init();

            _ServiceSeed++;
            return _ServiceSeed - 1;
        }

        /// <summary>
        /// Update all the services
        /// - Updates screenservice last for most up-to-date input snapshots
        /// </summary>
        public void Update(GameTime gameTime)
        {
            for (int i = 1; i < _Services.Count; i++)
            {
                _Services[i].Update(gameTime);
            }
            //Update the ScreenService last
            _Services[0].Update(gameTime);
        }

        /// <summary>
        /// Draws each service that has DrawEnabled=true
        /// - No special ordering
        /// </summary>
        public void Draw(GameTime gameTime)
        {
            foreach (IGameService service in _Services.Values)
            {
                if (service.DrawEnabled)
                {
                    service.Draw(gameTime);
                }
            }
        }
    }
}
