using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RapidXNA.Interfaces;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
#if WINDOWS_PHONE
using Microsoft.Xna.Framework.Input.Touch;
#endif

namespace RapidXNA.Services
{
    public class InputService : IGameService
    {
        /// <summary>
        /// Contains reference to all the input types supported (nicely wrapped for convenience and ease of use)
        /// </summary>

        private KeyboardHandler _keyboard = new KeyboardHandler();
        private MouseHandler _mouse = new MouseHandler();
        private PhoneInputHandler _phoneInput = new PhoneInputHandler();
        //private PhoneMultitouchHandler _phoneMultitouch = new
        private GamePadHandler _gamePad = new GamePadHandler();

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
            private KeyboardState CurrentState, PreviousState;
            private Dictionary<Keys, float>
                PressLengths = new Dictionary<Keys, float>(),
                ReleasedLength = new Dictionary<Keys, float>();
            //keyboardHandler.add(Keys.A);
            private List<Keys> LengthCheckedKeys = new List<Keys>();
#endif


            public KeyboardHandler()
            {
#if WINDOWS
                CurrentState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
                PreviousState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
#endif
            }

            public void Update(GameTime gameTime)
            {
#if WINDOWS
                PreviousState = CurrentState;
                CurrentState = Microsoft.Xna.Framework.Input.Keyboard.GetState();

                for (int i = 0; i < LengthCheckedKeys.Count; i++)
                {
                    if (this.KeyHeld(LengthCheckedKeys[i]))
                    {
                        PressLengths[LengthCheckedKeys[i]] += gameTime.ElapsedGameTime.Milliseconds;
                    }
                    else if (this.KeyLeft(LengthCheckedKeys[i]))
                    {
                        PressLengths[LengthCheckedKeys[i]] += gameTime.ElapsedGameTime.Milliseconds;
                        ReleasedLength[LengthCheckedKeys[i]] = PressLengths[LengthCheckedKeys[i]];
                    }
                    else
                    {
                        PressLengths[LengthCheckedKeys[i]] = 0.0f;
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
                return ((CurrentState.IsKeyDown(k)) && (!PreviousState.IsKeyDown(k)));
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
                return ((CurrentState.IsKeyDown(k)) && (PreviousState.IsKeyDown(k)));
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
                return ((!CurrentState.IsKeyDown(k)) && (PreviousState.IsKeyDown(k)));
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
                if (!LengthCheckedKeys.Contains(k))
                {
                    PressLengths.Add(k, 0.0f);
                    ReleasedLength.Add(k, 0.0f);
                    LengthCheckedKeys.Add(k);
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
                if (PressLengths.ContainsKey(k))
                {
                    return PressLengths[k];
                }
                else
#endif
                    return 0f;
            }

            /// <summary>
            /// Check for how long a key was pressed for after release
            /// </summary>
            /// <param name="k">The key to check</param>
            /// <returns></returns>
            public float ReleasedTime(Keys k)
            {
#if WINDOWS
                if (ReleasedLength.ContainsKey(k))
                {
                    return ReleasedLength[k];
                }
                else
#endif
                    return 0f;
            }

        }
        #endregion

        #region MOUSE
        public class MouseHandler
        {
#if XBOX
#else
            MouseState previousState;
            MouseState currentState;
#endif

            public MouseHandler()
            {
#if XBOX
#else
                currentState = Microsoft.Xna.Framework.Input.Mouse.GetState();
                previousState = Microsoft.Xna.Framework.Input.Mouse.GetState();
#endif
            }

            public void Update(GameTime gameTime)
            {
#if XBOX
#else
                previousState = currentState;
                currentState = Microsoft.Xna.Framework.Input.Mouse.GetState();
#endif
            }

            public Vector2 Position()
            {
#if XBOX
            return Vector2.Zero;
#else
                return new Vector2(currentState.X, currentState.Y);
#endif
            }

            public bool LeftPressed()
            {
#if XBOX
            return false;
#else
                return ((currentState.LeftButton == ButtonState.Pressed) && (previousState.LeftButton == ButtonState.Released));
#endif
            }
            public bool LeftReleased()
            {
#if XBOX
            return false;
#else
                return ((previousState.LeftButton == ButtonState.Pressed) && (currentState.LeftButton == ButtonState.Released));
#endif
            }
            public bool LeftHeld()
            {
#if XBOX
            return false;
#else
                return ((previousState.LeftButton == ButtonState.Pressed) && (currentState.LeftButton == ButtonState.Pressed));
#endif
            }

            public bool MiddlePressed()
            {
#if XBOX
            return false;
#else
                return ((currentState.MiddleButton == ButtonState.Pressed) && (previousState.MiddleButton == ButtonState.Released));
#endif
            }
            public bool MiddleReleased()
            {
#if XBOX
            return false;
#else
                return ((previousState.MiddleButton == ButtonState.Pressed) && (currentState.MiddleButton == ButtonState.Released));
#endif
            }
            public bool MiddleHeld()
            {
#if XBOX
            return false;
#else
                return ((previousState.MiddleButton == ButtonState.Pressed) && (currentState.MiddleButton == ButtonState.Pressed));
#endif
            }

            public bool RightPressed()
            {
#if XBOX
            return false;
#else
                return ((currentState.RightButton == ButtonState.Pressed) && (previousState.RightButton == ButtonState.Released));
#endif
            }
            public bool RightReleased()
            {
#if XBOX
            return false;
#else
                return ((previousState.RightButton == ButtonState.Pressed) && (currentState.RightButton == ButtonState.Released));
#endif
            }
            public bool RightHeld()
            {
#if XBOX
            return false;
#else
                return ((previousState.RightButton == ButtonState.Pressed) && (currentState.RightButton == ButtonState.Pressed));
#endif
            }

            public int ScrollWheel
            {
#if XBOX
            get { return 0; }
#else
                get { return currentState.ScrollWheelValue; }
#endif
            }

        }
        #endregion

        #region PHONEINPUT
        public class PhoneInputHandler
        {

#if WINDOWS_PHONE
        List<GestureSample> _GestureSamples = new List<GestureSample>();
#endif
            public List<GestureSample> GestureSamples
            {
#if WINDOWS_PHONE
            get { return _GestureSamples; }
#else
                get { return new List<GestureSample>(); } //Should rethink this
#endif
            }

            public void Update(GameTime gameTime)
            {
#if WINDOWS_PHONE
            _GestureSamples.Clear();
            while (Microsoft.Xna.Framework.Input.Touch.TouchPanel.IsGestureAvailable)
            {
                var gs = Microsoft.Xna.Framework.Input.Touch.TouchPanel.ReadGesture();
                _GestureSamples.Add(gs);
            }
#endif
            }

        }

#if WINDOWS_PHONE
#else
        public class GestureSample
        {
            //Used for when there is no gesturesample class available (it isnt available on windows/xbox?)
            public Vector2 Delta { get { return new Vector2(); } set { } }
            public Vector2 Delta2 { get { return new Vector2(); } set { } }
            public TimeSpan Timestamp { get { return new TimeSpan(); } }
            public Vector2 Position { get { return new Vector2(); } set { } }
            public Vector2 Position2 { get { return new Vector2(); } set { } }
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
            GamePadState[] PreviousState = new GamePadState[4];
            GamePadState[] CurrentState = new GamePadState[4];
            public GamePadHandler()
            {
#if WINDOWS_PHONE
            PreviousState[0] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.One);
            CurrentState[0] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.One);
#else
                PreviousState[0] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.One);
                PreviousState[1] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.Two);
                PreviousState[2] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.Three);
                PreviousState[3] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.Four);

                CurrentState[0] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.One);
                CurrentState[1] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.Two);
                CurrentState[2] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.Three);
                CurrentState[3] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.Four);
#endif
            }

            public void Update(GameTime gameTime)
            {
#if WINDOWS_PHONE
            PreviousState[0] = CurrentState[0];

            CurrentState[0] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.One);
#else
                PreviousState[0] = CurrentState[0];
                PreviousState[1] = CurrentState[1];
                PreviousState[2] = CurrentState[2];
                PreviousState[3] = CurrentState[3];

                CurrentState[0] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.One);
                CurrentState[1] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.Two);
                CurrentState[2] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.Three);
                CurrentState[3] = Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.Four);
#endif
            }

            /// <summary>
            /// Triggers functions
            /// </summary>

            public float LeftTriggerValue(int ControllerNum)
            {
#if WINDOWS_PHONE
            return 0f;
#else
                return CurrentState[ControllerNum - 1].Triggers.Left;
#endif
            }
            public float RightTrigerValue(int ControllerNum)
            {
#if WINDOWS_PHONE
            return 0f;
#else
                return CurrentState[ControllerNum - 1].Triggers.Right;
#endif
            }
            public bool LeftTriggerPressed(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].Triggers.Left == 0.0f) && (CurrentState[ControllerNum - 1].Triggers.Left > 0.0f));
#endif
            }
            public bool RightTriggerPressed(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].Triggers.Right == 0.0f) && (CurrentState[ControllerNum - 1].Triggers.Right > 0.0f));
#endif
            }
            public bool LeftTriggerReleased(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].Triggers.Left > 0.0f) && (CurrentState[ControllerNum - 1].Triggers.Left == 0.0f));
#endif
            }
            public bool RightTriggerReleased(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].Triggers.Right > 0.0f) && (CurrentState[ControllerNum - 1].Triggers.Right == 0.0f));
#endif
            }

            /// <summary>
            /// Bumpers
            /// </summary>

            public bool LeftBumberPressed(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].Buttons.LeftShoulder == ButtonState.Released) && (CurrentState[ControllerNum - 1].Buttons.LeftShoulder == ButtonState.Pressed));
#endif
            }
            public bool RightBumberPressed(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].Buttons.RightShoulder == ButtonState.Released) && (CurrentState[ControllerNum - 1].Buttons.RightShoulder == ButtonState.Pressed));
#endif
            }
            public bool LeftBumberReleased(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].Buttons.LeftShoulder == ButtonState.Pressed) && (CurrentState[ControllerNum - 1].Buttons.LeftShoulder == ButtonState.Released));
#endif
            }
            public bool RightBumberReleased(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].Buttons.RightShoulder == ButtonState.Pressed) && (CurrentState[ControllerNum - 1].Buttons.RightShoulder == ButtonState.Released));
#endif
            }
            public bool LeftBumberHeld(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return (CurrentState[ControllerNum - 1].Buttons.LeftShoulder == ButtonState.Pressed);
#endif
            }
            public bool RightBumberHeld(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return (CurrentState[ControllerNum - 1].Buttons.RightShoulder == ButtonState.Pressed);
#endif
            }

            /// <summary>
            /// Sticks
            /// </summary>

            public Vector2 LeftStick(int ControllerNum)
            {
#if WINDOWS_PHONE
            return Vector2.Zero;
#else
                return CurrentState[ControllerNum - 1].ThumbSticks.Left;
#endif
            }
            public Vector2 RightStick(int ControllerNum)
            {
#if WINDOWS_PHONE
            return Vector2.Zero;
#else
                return CurrentState[ControllerNum - 1].ThumbSticks.Right;
#endif
            }
            public float LeftStickDirection(int ControllerNum)
            {
#if WINDOWS_PHONE
            return 0f;
#else
                float _x = CurrentState[ControllerNum - 1].ThumbSticks.Left.X, _y = CurrentState[ControllerNum - 1].ThumbSticks.Left.Y;
                return (float)Math.Atan2(_y, _x);
#endif
            }
            public float RightStickDirection(int ControllerNum)
            {
#if WINDOWS_PHONE
            return 0f;
#else
                float _x = CurrentState[ControllerNum - 1].ThumbSticks.Right.X, _y = CurrentState[ControllerNum - 1].ThumbSticks.Right.Y;
                return (float)Math.Atan2(_y, _x);
#endif
            }
            public bool LeftStickPressed(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].Buttons.LeftStick == ButtonState.Released) && (CurrentState[ControllerNum - 1].Buttons.LeftStick == ButtonState.Pressed));
#endif
            }
            public bool RightStickPressed(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].Buttons.RightStick == ButtonState.Released) && (CurrentState[ControllerNum - 1].Buttons.RightStick == ButtonState.Pressed));
#endif
            }
            public bool LeftStickReleased(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].Buttons.LeftStick == ButtonState.Pressed) && (CurrentState[ControllerNum - 1].Buttons.LeftStick == ButtonState.Released));
#endif
            }
            public bool RightStickReleased(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].Buttons.RightStick == ButtonState.Pressed) && (CurrentState[ControllerNum - 1].Buttons.RightStick == ButtonState.Released));
#endif
            }
            public bool LeftStickHeld(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return (CurrentState[ControllerNum - 1].Buttons.LeftStick == ButtonState.Pressed);
#endif
            }
            public bool RightStickHeld(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return (CurrentState[ControllerNum - 1].Buttons.RightStick == ButtonState.Pressed);
#endif
            }

            /// <summary>
            /// DPAD
            /// </summary>


            public bool LeftPressed(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].DPad.Left == ButtonState.Released) && (CurrentState[ControllerNum - 1].DPad.Left == ButtonState.Pressed));
#endif
            }
            public bool LeftReleased(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].DPad.Left == ButtonState.Pressed) && (CurrentState[ControllerNum - 1].DPad.Left == ButtonState.Released));
#endif
            }
            public bool LeftHeld(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return (CurrentState[ControllerNum - 1].DPad.Left == ButtonState.Pressed);
#endif
            }
            public bool RightPressed(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].DPad.Right == ButtonState.Released) && (CurrentState[ControllerNum - 1].DPad.Right == ButtonState.Pressed));
#endif
            }
            public bool RightReleased(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].DPad.Right == ButtonState.Pressed) && (CurrentState[ControllerNum - 1].DPad.Right == ButtonState.Released));
#endif
            }
            public bool RightHeld(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return (CurrentState[ControllerNum - 1].DPad.Right == ButtonState.Pressed);
#endif
            }
            public bool UpPressed(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].DPad.Up == ButtonState.Released) && (CurrentState[ControllerNum - 1].DPad.Up == ButtonState.Pressed));
#endif
            }
            public bool UpReleased(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].DPad.Up == ButtonState.Pressed) && (CurrentState[ControllerNum - 1].DPad.Up == ButtonState.Released));
#endif
            }
            public bool UpHeld(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return (CurrentState[ControllerNum - 1].DPad.Up == ButtonState.Pressed);
#endif
            }
            public bool DownPressed(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].DPad.Down == ButtonState.Released) && (CurrentState[ControllerNum - 1].DPad.Down == ButtonState.Pressed));
#endif
            }
            public bool DownReleased(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].DPad.Down == ButtonState.Pressed) && (CurrentState[ControllerNum - 1].DPad.Down == ButtonState.Released));
#endif
            }
            public bool DownHeld(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return (CurrentState[ControllerNum - 1].DPad.Down == ButtonState.Pressed);
#endif
            }

            public bool XPressed(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].Buttons.X == ButtonState.Released) && (CurrentState[ControllerNum - 1].Buttons.X == ButtonState.Pressed));
#endif
            }
            public bool XReleased(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].Buttons.X == ButtonState.Pressed) && (CurrentState[ControllerNum - 1].Buttons.X == ButtonState.Released));
#endif
            }
            public bool XHeld(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return (CurrentState[ControllerNum - 1].Buttons.X == ButtonState.Pressed);
#endif
            }
            public bool YPressed(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].Buttons.Y == ButtonState.Released) && (CurrentState[ControllerNum - 1].Buttons.Y == ButtonState.Pressed));
#endif
            }
            public bool YReleased(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].Buttons.Y == ButtonState.Pressed) && (CurrentState[ControllerNum - 1].Buttons.Y == ButtonState.Released));
#endif
            }
            public bool YHeld(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return (CurrentState[ControllerNum - 1].Buttons.Y == ButtonState.Pressed);
#endif
            }
            public bool APressed(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].Buttons.A == ButtonState.Released) && (CurrentState[ControllerNum - 1].Buttons.A == ButtonState.Pressed));
#endif
            }
            public bool AReleased(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].Buttons.A == ButtonState.Pressed) && (CurrentState[ControllerNum - 1].Buttons.A == ButtonState.Released));
#endif
            }
            public bool AHeld(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return (CurrentState[ControllerNum - 1].Buttons.A == ButtonState.Pressed);
#endif
            }
            public bool BPressed(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].Buttons.B == ButtonState.Released) && (CurrentState[ControllerNum - 1].Buttons.B == ButtonState.Pressed));
#endif
            }
            public bool BReleased(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].Buttons.B == ButtonState.Pressed) && (CurrentState[ControllerNum - 1].Buttons.B == ButtonState.Released));
#endif
            }
            public bool BHeld(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return (CurrentState[ControllerNum - 1].Buttons.B == ButtonState.Pressed);
#endif
            }

            public bool StartPressed(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].Buttons.Start == ButtonState.Released) && (CurrentState[ControllerNum - 1].Buttons.Start == ButtonState.Pressed));
#endif
            }
            public bool StartReleased(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return ((PreviousState[ControllerNum - 1].Buttons.Start == ButtonState.Pressed) && (CurrentState[ControllerNum - 1].Buttons.Start == ButtonState.Released));
#endif
            }
            public bool StartHeld(int ControllerNum)
            {
#if WINDOWS_PHONE
            return false;
#else
                return (CurrentState[ControllerNum - 1].Buttons.Start == ButtonState.Pressed);
#endif
            }
            public bool BackPressed(int ControllerNum)
            {
#if WINDOWS_PHONE
            return ((PreviousState[0].Buttons.Back == ButtonState.Released) && (CurrentState[0].Buttons.Back == ButtonState.Pressed));
#else
                return ((PreviousState[ControllerNum - 1].Buttons.Back == ButtonState.Released) && (CurrentState[ControllerNum - 1].Buttons.Back == ButtonState.Pressed));
#endif
            }
            public bool BackReleased(int ControllerNum)
            {
#if WINDOWS_PHONE
            return ((PreviousState[0].Buttons.Back == ButtonState.Pressed) && (CurrentState[0].Buttons.Back == ButtonState.Released));
#else
                return ((PreviousState[ControllerNum - 1].Buttons.Back == ButtonState.Pressed) && (CurrentState[ControllerNum - 1].Buttons.Back == ButtonState.Released));
#endif
            }
            public bool BackHeld(int ControllerNum)
            {
#if WINDOWS_PHONE
            return (CurrentState[0].Buttons.Back == ButtonState.Pressed);
#else
                return (CurrentState[ControllerNum - 1].Buttons.Back == ButtonState.Pressed);
#endif
            }
        }
        #endregion
    }
}
