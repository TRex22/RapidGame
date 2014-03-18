using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#if WINDOWS_PHONE
using Microsoft.Xna.Framework.Input.Touch;
#endif
using RapidXNA.Models;


namespace RapidXNA.Services
{
    public class InputService : RapidService
    {
        /// <summary>
        /// Contains reference to all the input types supported (nicely wrapped for convenience and ease of use)
        /// </summary>

        private readonly KeyboardHandler _keyboard = new KeyboardHandler();
        private readonly MouseHandler _mouse = new MouseHandler();
        private readonly PhoneInputHandler _phoneInput = new PhoneInputHandler();
        //private PhoneMultitouchHandler _phoneMultitouch = new
        private readonly GamePadHandler _gamePad = new GamePadHandler();

        public KeyboardHandler Keyboard { get { return _keyboard; } }
        public MouseHandler Mouse { get { return _mouse; } }
        public PhoneInputHandler PhoneGestures { get { return _phoneInput; } }
        public GamePadHandler GamePad { get { return _gamePad; } }

        public override void Init()
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            _keyboard.Update(gameTime);
            _mouse.Update(gameTime);
            _phoneInput.Update(gameTime);
            _gamePad.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            
        }

        #region KEYBOARD
        public class KeyboardHandler
        {
#if WINDOWS
            private KeyboardState _currentState, _previousState;
            private readonly Dictionary<Keys, float>
                _pressLengths = new Dictionary<Keys, float>();

            private readonly Dictionary<Keys, float>
                _releasedLength = new Dictionary<Keys, float>();

            //keyboardHandler.add(Keys.A);
            private readonly List<Keys> _lengthCheckedKeys = new List<Keys>();
#endif


            public KeyboardHandler()
            {
#if WINDOWS
                _currentState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
                _previousState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
#endif
            }

            public void Update(GameTime gameTime)
            {
#if WINDOWS
                _previousState = _currentState;
                _currentState = Microsoft.Xna.Framework.Input.Keyboard.GetState();

                foreach (var t in _lengthCheckedKeys)
                {
                    if (KeyHeld(t))
                    {
                        _pressLengths[t] += gameTime.ElapsedGameTime.Milliseconds;
                    }
                    else if (KeyLeft(t))
                    {
                        _pressLengths[t] += gameTime.ElapsedGameTime.Milliseconds;
                        _releasedLength[t] = _pressLengths[t];
                    }
                    else
                    {
                        _pressLengths[t] = 0.0f;
                    }
                }
#endif
            }

            /// <summary>
            /// Check if a key was pressed, this will trigger exclusively over KeyHeld
            /// </summary>
            /// <param name="k">The key to check</param>
            /// <returns></returns>
            public bool KeyPress(Keys k)
            {
#if WINDOWS
                return ((_currentState.IsKeyDown(k)) && (!_previousState.IsKeyDown(k)));
#else
            return false;
#endif
            }

            /// <summary>
            /// Check if a key is currently being held down, this wont trigger at initial press
            /// </summary>
            /// <param name="k">The key to check</param>
            /// <returns></returns>
            public bool KeyHeld(Keys k)
            {
#if WINDOWS
                return ((_currentState.IsKeyDown(k)) && (_previousState.IsKeyDown(k)));
#else
            return false;
#endif
            }

            /// <summary>
            /// Check if a key was released
            /// </summary>
            /// <param name="k">The key to check</param>
            /// <returns></returns>
            public bool KeyLeft(Keys k)
            {
#if WINDOWS
                return ((!_currentState.IsKeyDown(k)) && (_previousState.IsKeyDown(k)));
#else
            return false;
#endif
            }

            /// <summary>
            /// Add a key to the list of keys you want the pressed length of time for
            /// </summary>
            /// <param name="k">The key to check</param>
            public void AddKey(Keys k)
            {
#if WINDOWS
                if (!_lengthCheckedKeys.Contains(k))
                {
                    _pressLengths.Add(k, 0.0f);
                    _releasedLength.Add(k, 0.0f);
                    _lengthCheckedKeys.Add(k);
                }
#endif
            }

            /// <summary>
            /// Checks how long a key was pressed for so far in millisecond
            /// </summary>
            /// <param name="k">The key to check</param>
            /// <returns></returns>
            public float HeldFor(Keys k)
            {
#if WINDOWS
                return _pressLengths.ContainsKey(k) ? _pressLengths[k] : 0f;
#else
                return 0f;
#endif
            }

            /// <summary>
            /// Check for how long a key was pressed for after release
            /// </summary>
            /// <param name="k">The key to check</param>
            /// <returns></returns>
            public float ReleasedTime(Keys k)
            {
#if WINDOWS
                return _releasedLength.ContainsKey(k) ? _releasedLength[k] : 0f;
#else
                return 0f;
#endif
            }
        }
        #endregion

        #region MOUSE
        public class MouseHandler
        {
#if XBOX
#else
            MouseState _previousState;
            MouseState _currentState;
#endif

            public MouseHandler()
            {
#if XBOX
#else
                _currentState = Microsoft.Xna.Framework.Input.Mouse.GetState();
                _previousState = Microsoft.Xna.Framework.Input.Mouse.GetState();
#endif
            }

            public void Update(GameTime gameTime)
            {
#if XBOX
#else
                _previousState = _currentState;
                _currentState = Microsoft.Xna.Framework.Input.Mouse.GetState();
#endif
            }

            public Vector2 Position()
            {
#if XBOX
            return Vector2.Zero;
#else
                return new Vector2(_currentState.X, _currentState.Y);
#endif
            }

            public bool LeftPressed()
            {
#if XBOX
            return false;
#else
                return ((_currentState.LeftButton == ButtonState.Pressed) && (_previousState.LeftButton == ButtonState.Released));
#endif
            }
            public bool LeftReleased()
            {
#if XBOX
            return false;
#else
                return ((_previousState.LeftButton == ButtonState.Pressed) && (_currentState.LeftButton == ButtonState.Released));
#endif
            }
            public bool LeftHeld()
            {
#if XBOX
            return false;
#else
                return ((_previousState.LeftButton == ButtonState.Pressed) && (_currentState.LeftButton == ButtonState.Pressed));
#endif
            }

            public bool MiddlePressed()
            {
#if XBOX
            return false;
#else
                return ((_currentState.MiddleButton == ButtonState.Pressed) && (_previousState.MiddleButton == ButtonState.Released));
#endif
            }
            public bool MiddleReleased()
            {
#if XBOX
            return false;
#else
                return ((_previousState.MiddleButton == ButtonState.Pressed) && (_currentState.MiddleButton == ButtonState.Released));
#endif
            }
            public bool MiddleHeld()
            {
#if XBOX
            return false;
#else
                return ((_previousState.MiddleButton == ButtonState.Pressed) && (_currentState.MiddleButton == ButtonState.Pressed));
#endif
            }

            public bool RightPressed()
            {
#if XBOX
            return false;
#else
                return ((_currentState.RightButton == ButtonState.Pressed) && (_previousState.RightButton == ButtonState.Released));
#endif
            }
            public bool RightReleased()
            {
#if XBOX
            return false;
#else
                return ((_previousState.RightButton == ButtonState.Pressed) && (_currentState.RightButton == ButtonState.Released));
#endif
            }
            public bool RightHeld()
            {
#if XBOX
            return false;
#else
                return ((_previousState.RightButton == ButtonState.Pressed) && (_currentState.RightButton == ButtonState.Pressed));
#endif
            }

            public int ScrollWheel
            {
#if XBOX
            get { return 0; }
#else
                get { return _currentState.ScrollWheelValue; }
#endif
            }

        }
        #endregion

        #region PHONEINPUT
        public class PhoneInputHandler
        {

#if WINDOWS_PHONE
            readonly List<GestureSample> _gestureSamples = new List<GestureSample>();
#endif
            public List<GestureSample> GestureSamples
            {
#if WINDOWS_PHONE
            get { return _gestureSamples; }
#else
                get { return new List<GestureSample>(); } //Should rethink this
#endif
            }

            public void Update(GameTime gameTime)
            {
#if WINDOWS_PHONE
            _gestureSamples.Clear();
            while (Microsoft.Xna.Framework.Input.Touch.TouchPanel.IsGestureAvailable)
            {
                var gs = Microsoft.Xna.Framework.Input.Touch.TouchPanel.ReadGesture();
                _gestureSamples.Add(gs);
            }
#endif
            }

        }

#if WINDOWS_PHONE
#else
        public class GestureSample
        {
            //Used for when there is no gesturesample class available (it isnt available on windows/xbox?)
            public Vector2 Delta { get { return new Vector2(); } }
            public Vector2 Delta2 { get { return new Vector2(); } }
            public TimeSpan Timestamp { get { return new TimeSpan(); } }
            public Vector2 Position { get { return new Vector2(); } }
            public Vector2 Position2 { get { return new Vector2(); } }
            public GestureType GestureType { get { return GestureType.CustomNone; } }
        }

        public enum GestureType
        {
            DoubleTap,
            DragComplete,
            Flick,
            FreeDrag,
            Hold,
            HorrizontalDrag,
            None,
            Pinch,
            PinchComplete,
            Tap,
            VerticalDrag,
            CustomNone = 9999
        }
#endif
        #endregion

        #region PHONEMULTITOUCH
        //TODO!!
        #endregion

        #region GAMEPAD
        public class GamePadHandler
        {
            readonly GamePadState[] _previousState = new GamePadState[4];
            readonly GamePadState[] _currentState = new GamePadState[4];
            public GamePadHandler()
            {
#if WINDOWS_PHONE
            _previousState[0] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.One);
            _currentState[0] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.One);
#else
                _previousState[0] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.One);
                _previousState[1] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.Two);
                _previousState[2] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.Three);
                _previousState[3] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.Four);

                _currentState[0] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.One);
                _currentState[1] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.Two);
                _currentState[2] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.Three);
                _currentState[3] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.Four);
#endif
            }

            public void Update(GameTime gameTime)
            {
#if WINDOWS_PHONE
            _previousState[0] = _currentState[0];

            _currentState[0] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.One);
#else
                _previousState[0] = _currentState[0];
                _previousState[1] = _currentState[1];
                _previousState[2] = _currentState[2];
                _previousState[3] = _currentState[3];

                _currentState[0] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.One);
                _currentState[1] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.Two);
                _currentState[2] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.Three);
                _currentState[3] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.Four);
#endif
            }

            /// <summary>
            /// Triggers functions
            /// </summary>

            public float LeftTriggerValue(int controllerNum)
            {
#if WINDOWS_PHONE
            return 0f;
#else
                return _currentState[controllerNum - 1].Triggers.Left;
#endif
            }
            public float RightTrigerValue(int controllerNum)
            {
#if WINDOWS_PHONE
            return 0f;
#else
                return _currentState[controllerNum - 1].Triggers.Right;
#endif
            }
            public bool LeftTriggerPressed(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                /*TODO JMC Check if more precise and working*/
                return ((Math.Abs(_previousState[controllerNum - 1].Triggers.Left) < 0.0f) && (_currentState[controllerNum - 1].Triggers.Left > 0.0f));
#endif
            }
            public bool RightTriggerPressed(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                /*TODO _previousState[ControllerNum - 1].Triggers.Right == 0.0f*/
                return ((Math.Abs(_previousState[controllerNum - 1].Triggers.Right) < 0.0f) && (_currentState[controllerNum - 1].Triggers.Right > 0.0f));
#endif
            }
            public bool LeftTriggerReleased(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].Triggers.Left > 0.0f) && (Math.Abs(_currentState[controllerNum - 1].Triggers.Left) < 0.0f));
#endif
            }
            public bool RightTriggerReleased(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].Triggers.Right > 0.0f) && (Math.Abs(_currentState[controllerNum - 1].Triggers.Right) < 0.0f));
#endif
            }

            /// <summary>
            /// Bumpers
            /// </summary>

            public bool LeftBumberPressed(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].Buttons.LeftShoulder == ButtonState.Released) && (_currentState[controllerNum - 1].Buttons.LeftShoulder == ButtonState.Pressed));
#endif
            }
            public bool RightBumberPressed(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].Buttons.RightShoulder == ButtonState.Released) && (_currentState[controllerNum - 1].Buttons.RightShoulder == ButtonState.Pressed));
#endif
            }
            public bool LeftBumberReleased(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].Buttons.LeftShoulder == ButtonState.Pressed) && (_currentState[controllerNum - 1].Buttons.LeftShoulder == ButtonState.Released));
#endif
            }
            public bool RightBumberReleased(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].Buttons.RightShoulder == ButtonState.Pressed) && (_currentState[controllerNum - 1].Buttons.RightShoulder == ButtonState.Released));
#endif
            }
            public bool LeftBumberHeld(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return (_currentState[controllerNum - 1].Buttons.LeftShoulder == ButtonState.Pressed);
#endif
            }
            public bool RightBumberHeld(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return (_currentState[controllerNum - 1].Buttons.RightShoulder == ButtonState.Pressed);
#endif
            }

            /// <summary>
            /// Sticks
            /// </summary>

            public Vector2 LeftStick(int controllerNum)
            {
#if WINDOWS_PHONE
            return Vector2.Zero;
#else
                return _currentState[controllerNum - 1].ThumbSticks.Left;
#endif
            }
            public Vector2 RightStick(int controllerNum)
            {
#if WINDOWS_PHONE
            return Vector2.Zero;
#else
                return _currentState[controllerNum - 1].ThumbSticks.Right;
#endif
            }
            public float LeftStickDirection(int controllerNum)
            {
#if WINDOWS_PHONE
            return 0f;
#else
                float x = _currentState[controllerNum - 1].ThumbSticks.Left.X, y = _currentState[controllerNum - 1].ThumbSticks.Left.Y;
                return (float)Math.Atan2(y, x);
#endif
            }
            public float RightStickDirection(int controllerNum)
            {
#if WINDOWS_PHONE
            return 0f;
#else
                float x = _currentState[controllerNum - 1].ThumbSticks.Right.X, y = _currentState[controllerNum - 1].ThumbSticks.Right.Y;
                return (float)Math.Atan2(y, x);
#endif
            }
            public bool LeftStickPressed(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].Buttons.LeftStick == ButtonState.Released) && (_currentState[controllerNum - 1].Buttons.LeftStick == ButtonState.Pressed));
#endif
            }
            public bool RightStickPressed(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].Buttons.RightStick == ButtonState.Released) && (_currentState[controllerNum - 1].Buttons.RightStick == ButtonState.Pressed));
#endif
            }
            public bool LeftStickReleased(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].Buttons.LeftStick == ButtonState.Pressed) && (_currentState[controllerNum - 1].Buttons.LeftStick == ButtonState.Released));
#endif
            }
            public bool RightStickReleased(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].Buttons.RightStick == ButtonState.Pressed) && (_currentState[controllerNum - 1].Buttons.RightStick == ButtonState.Released));
#endif
            }
            public bool LeftStickHeld(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return (_currentState[controllerNum - 1].Buttons.LeftStick == ButtonState.Pressed);
#endif
            }
            public bool RightStickHeld(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return (_currentState[controllerNum - 1].Buttons.RightStick == ButtonState.Pressed);
#endif
            }

            /// <summary>
            /// DPAD
            /// </summary>


            public bool LeftPressed(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].DPad.Left == ButtonState.Released) && (_currentState[controllerNum - 1].DPad.Left == ButtonState.Pressed));
#endif
            }
            public bool LeftReleased(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].DPad.Left == ButtonState.Pressed) && (_currentState[controllerNum - 1].DPad.Left == ButtonState.Released));
#endif
            }
            public bool LeftHeld(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return (_currentState[controllerNum - 1].DPad.Left == ButtonState.Pressed);
#endif
            }
            public bool RightPressed(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].DPad.Right == ButtonState.Released) && (_currentState[controllerNum - 1].DPad.Right == ButtonState.Pressed));
#endif
            }
            public bool RightReleased(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].DPad.Right == ButtonState.Pressed) && (_currentState[controllerNum - 1].DPad.Right == ButtonState.Released));
#endif
            }
            public bool RightHeld(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return (_currentState[controllerNum - 1].DPad.Right == ButtonState.Pressed);
#endif
            }
            public bool UpPressed(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].DPad.Up == ButtonState.Released) && (_currentState[controllerNum - 1].DPad.Up == ButtonState.Pressed));
#endif
            }
            public bool UpReleased(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].DPad.Up == ButtonState.Pressed) && (_currentState[controllerNum - 1].DPad.Up == ButtonState.Released));
#endif
            }
            public bool UpHeld(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return (_currentState[controllerNum - 1].DPad.Up == ButtonState.Pressed);
#endif
            }
            public bool DownPressed(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].DPad.Down == ButtonState.Released) && (_currentState[controllerNum - 1].DPad.Down == ButtonState.Pressed));
#endif
            }
            public bool DownReleased(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].DPad.Down == ButtonState.Pressed) && (_currentState[controllerNum - 1].DPad.Down == ButtonState.Released));
#endif
            }
            public bool DownHeld(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return (_currentState[controllerNum - 1].DPad.Down == ButtonState.Pressed);
#endif
            }

            public bool XPressed(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].Buttons.X == ButtonState.Released) && (_currentState[controllerNum - 1].Buttons.X == ButtonState.Pressed));
#endif
            }
            public bool XReleased(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].Buttons.X == ButtonState.Pressed) && (_currentState[controllerNum - 1].Buttons.X == ButtonState.Released));
#endif
            }
            public bool XHeld(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return (_currentState[controllerNum - 1].Buttons.X == ButtonState.Pressed);
#endif
            }
            public bool YPressed(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].Buttons.Y == ButtonState.Released) && (_currentState[controllerNum - 1].Buttons.Y == ButtonState.Pressed));
#endif
            }
            public bool YReleased(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].Buttons.Y == ButtonState.Pressed) && (_currentState[controllerNum - 1].Buttons.Y == ButtonState.Released));
#endif
            }
            public bool YHeld(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return (_currentState[controllerNum - 1].Buttons.Y == ButtonState.Pressed);
#endif
            }
            public bool APressed(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].Buttons.A == ButtonState.Released) && (_currentState[controllerNum - 1].Buttons.A == ButtonState.Pressed));
#endif
            }
            public bool AReleased(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].Buttons.A == ButtonState.Pressed) && (_currentState[controllerNum - 1].Buttons.A == ButtonState.Released));
#endif
            }
            public bool AHeld(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return (_currentState[controllerNum - 1].Buttons.A == ButtonState.Pressed);
#endif
            }
            public bool BPressed(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].Buttons.B == ButtonState.Released) && (_currentState[controllerNum - 1].Buttons.B == ButtonState.Pressed));
#endif
            }
            public bool BReleased(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].Buttons.B == ButtonState.Pressed) && (_currentState[controllerNum - 1].Buttons.B == ButtonState.Released));
#endif
            }
            public bool BHeld(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return (_currentState[controllerNum - 1].Buttons.B == ButtonState.Pressed);
#endif
            }

            public bool StartPressed(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].Buttons.Start == ButtonState.Released) && (_currentState[controllerNum - 1].Buttons.Start == ButtonState.Pressed));
#endif
            }
            public bool StartReleased(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((_previousState[controllerNum - 1].Buttons.Start == ButtonState.Pressed) && (_currentState[controllerNum - 1].Buttons.Start == ButtonState.Released));
#endif
            }
            public bool StartHeld(int controllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return (_currentState[controllerNum - 1].Buttons.Start == ButtonState.Pressed);
#endif
            }
            public bool BackPressed(int controllerNum)
            {
#if WINDOWS_PHONE
            return ((_previousState[0].Buttons.Back == ButtonState.Released) && (_currentState[0].Buttons.Back == ButtonState.Pressed));
#else
                return ((_previousState[controllerNum - 1].Buttons.Back == ButtonState.Released) && (_currentState[controllerNum - 1].Buttons.Back == ButtonState.Pressed));
#endif
            }
            public bool BackReleased(int controllerNum)
            {
#if WINDOWS_PHONE
            return ((_previousState[0].Buttons.Back == ButtonState.Pressed) && (_currentState[0].Buttons.Back == ButtonState.Released));
#else
                return ((_previousState[controllerNum - 1].Buttons.Back == ButtonState.Pressed) && (_currentState[controllerNum - 1].Buttons.Back == ButtonState.Released));
#endif
            }
            public bool BackHeld(int controllerNum)
            {
#if WINDOWS_PHONE
            return (_currentState[0].Buttons.Back == ButtonState.Pressed);
#else
                return (_currentState[controllerNum - 1].Buttons.Back == ButtonState.Pressed);
#endif
            }
        }
        #endregion
    }
}
