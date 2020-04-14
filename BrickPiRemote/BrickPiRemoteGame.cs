using System;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BrickPiRemote
{
    public class BrickPiRemoteGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public BrickPiRemoteGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var padState = GamePad.GetState(PlayerIndex.One);
            var msg = GetUpdMessage(padState);
            SendGamepadData(msg);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }


        private string GetUpdMessage(GamePadState padState)
        {
            float left_x = padState.ThumbSticks.Left.X;
            float left_y = padState.ThumbSticks.Left.Y;
            float right_x = padState.ThumbSticks.Right.X;
            float right_y = padState.ThumbSticks.Right.Y;
            bool back = padState.IsButtonDown(Buttons.Back);

            return String.Format("{0:0.0},{1:0.0},{2:0.0},{3:0.0},{4}", left_x, left_y, right_x, right_y, back.ToString());
            
        }

        private void SendGamepadData(string msg)
        {
            // This constructor arbitrarily assigns the local port number.
            UdpClient udpClient = new UdpClient(11000);

            try
            {
         
                udpClient.Connect(UdpClientConfig.Host, UdpClientConfig.Port);

                // Sends a message to the host to which you have connected.

                Byte[] sendBytes = Encoding.ASCII.GetBytes(msg);
            
                udpClient.Send(sendBytes, sendBytes.Length);

                udpClient.Close();
            }  
            catch (Exception e) 
            {
                Debug.WriteLine(e.ToString());
            }
        }
    }
}
