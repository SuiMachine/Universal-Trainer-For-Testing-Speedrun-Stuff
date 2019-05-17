using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Collections.Generic;
using System.Media;
using MemoryReads64;
using System.Linq;

namespace Flying47
{
    public partial class MainForm : Form
    {
        // Other variables.
        Process myProcess;
        string processName;

        float readCoordX = 0;
        float readCoordY = 0;
        float readCoordZ = 0;
        float readSinAlpha = 0;
        float readCosAlpha = 0;

        float storedCoordX = 0;
        float storedCoordY = 0;
        float storedCoordZ = 0;

        float moveAmountXYAxis = 5f;
        float moveAmountZAxis = 5f;

        //Block related to position
        bool AnglesEnabled = true;
        Pointer adrSinAlpha;
        bool isSinInverted = false;
        Pointer adrCosAlpha;
        bool isCosInverted = false;

        PositionSet positionAddress;
        Keys kStorePosition = Keys.NumPad7;
        Keys kLoadPosition = Keys.NumPad9;
        Keys kUp = Keys.Add;
        Keys kDown = Keys.Subtract;
        Keys kForward = Keys.NumPad8;

        string CurrentKeyChange;
        bool settingInputKey = false;

        Bitmap bitmap;
        Graphics gBuffer;

        KeyboardHook.KeyboardHook m_KeyboardHook;


        /*------------------
        -- INITIALIZATION --
        ------------------*/
        public MainForm()
        {
            InitializeComponent();
            processName = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (ConfigLoader.LoadFullConfig(out processName, out positionAddress, out adrSinAlpha, out isSinInverted, out adrCosAlpha, out isCosInverted, out moveAmountXYAxis, out moveAmountZAxis))
                {
                    if (adrSinAlpha.IsNull() || adrCosAlpha.IsNull())
                    {
                        AnglesEnabled = false;
                        kForward = Keys.None;
                        B_KeyForward.Enabled = false;
                    }

                    TTimer.Start();

                    B_KeyForward.Text = kForward.ToString();
                    B_StorePosition.Text = kStorePosition.ToString();
                    B_LoadPosition.Text = kLoadPosition.ToString();
                    B_KeyUp.Text = kUp.ToString();
                    B_KeyDown.Text = kDown.ToString();

                    TB_MoveXYAxis.Text = moveAmountXYAxis.ToString();
                    TB_MoveZAxis.Text = moveAmountZAxis.ToString();

                    bitmap = new Bitmap(vectorDisplay.Width, vectorDisplay.Height);
                    gBuffer = Graphics.FromImage(bitmap);
                    m_KeyboardHook = new KeyboardHook.KeyboardHook();
                    RegisterKeys();
                    m_KeyboardHook.KeyPressed += GlobalHook_KeyDown;
                }
                else
                    Application.Exit();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
            }
        }

        private void RegisterKeys()
        {
            m_KeyboardHook.RegisterHotKey(kStorePosition);
            m_KeyboardHook.RegisterHotKey(kStorePosition);
            m_KeyboardHook.RegisterHotKey(kLoadPosition);
            m_KeyboardHook.RegisterHotKey(kUp);
            m_KeyboardHook.RegisterHotKey(kDown);
            m_KeyboardHook.RegisterHotKey(kForward);
        }

        bool foundProcess = false;

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                m_KeyboardHook.Poll();

                myProcess = Process.GetProcessesByName(processName).FirstOrDefault();
                if (myProcess != null )
                {
                    if(foundProcess == false )
                    {
                        TTimer.Interval = 1000;
                    }

                    foundProcess = true;
                }
                else
                {
                    foundProcess = false;
                }

                
                if (foundProcess)
                {
                    // The game is running, ready for memory reading.
                    LB_Running.Text = processName + " is running";
                    LB_Running.ForeColor = Color.Green;

                    positionAddress.ReadSet(myProcess, out readCoordX, out readCoordY, out readCoordZ);
                    var test = Trainer.ReadPointerInteger(myProcess, positionAddress.X);
                    L_X.Text = readCoordX.ToString();
                    L_Y.Text = readCoordY.ToString();
                    L_Z.Text = readCoordZ.ToString();
                    if(AnglesEnabled)
                    {
                        readSinAlpha = Trainer.ReadPointerFloat(myProcess, adrSinAlpha);
                        if (isSinInverted)
                            readSinAlpha = -readSinAlpha;
                        readCosAlpha = Trainer.ReadPointerFloat(myProcess, adrCosAlpha);
                        if (isCosInverted)
                            readCosAlpha = -readCosAlpha;
                        DrawVectorDisplay();
                        vectorDisplay.Image = bitmap;
                    }

                    TTimer.Interval = 100;
                }
                else
                {
                    // The game process has not been found, reseting values.
                    LB_Running.Text = processName + " is not running";
                    LB_Running.ForeColor = Color.Red;
                    ResetValues();
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void DrawVectorDisplay()
        {
            gBuffer.Clear(Color.Black);
            float width = bitmap.Width;
            float half = width/2; //make sure it's square
            gBuffer.DrawLine(new Pen(Color.Blue, 2), half, half, half + half*readSinAlpha, half + half * readCosAlpha);
        }

        // Used to reset all the values.
        private void ResetValues()
        {
            L_X.Text = "NaN";
            L_Y.Text = "NaN";
            L_Z.Text = "NaN";
        }

        private void GlobalHook_KeyDown(object sender, KeyEventArgs e)
        {
            var hotkey = e.KeyCode;
            if (hotkey == Keys.None)
                return;

            if (!settingInputKey)
            {
                if (hotkey == kStorePosition)
                {
                    Save_Position();
                }
                else if (hotkey == kLoadPosition)
                {
                    Load_Position();
                }

                if (hotkey == kUp)
                {
                    SendMeUp();
                }
                else if (hotkey == kDown)
                {
                    SendMeDown();
                }

                if (hotkey == kForward)
                {
                    SendMeForward();
                }
            }
        }

        private void SendMeForward()
        {
            Trainer.WritePointerFloat(myProcess, positionAddress.X, readCoordX + readSinAlpha * moveAmountXYAxis);
            Trainer.WritePointerFloat(myProcess, positionAddress.Y, readCoordY + readCosAlpha * moveAmountXYAxis);
        }

        private void SendMeDown()
        {
            Trainer.WritePointerFloat(myProcess, positionAddress.Z, readCoordZ - moveAmountZAxis);
        }

        private void SendMeUp()
        {
            Trainer.WritePointerFloat(myProcess, positionAddress.Z, readCoordZ + moveAmountZAxis);
        }

        private void Load_Position()
        {
            Trainer.WritePointerFloat(myProcess, positionAddress.X, storedCoordX);
            Trainer.WritePointerFloat(myProcess, positionAddress.Y, storedCoordY);
            Trainer.WritePointerFloat(myProcess, positionAddress.Z, storedCoordZ);
        }

        private void Save_Position()
        {
            storedCoordX = readCoordX;
            storedCoordY = readCoordY;
            storedCoordZ = readCoordZ;
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_KeyboardHook != null)
            {
                m_KeyboardHook.UnregisterAllHotkeys();
                m_KeyboardHook.KeyPressed -= GlobalHook_KeyDown;
            }
        }

        private void B_ChangeButton(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                settingInputKey = true;
                CurrentKeyChange = ((CheckBox)sender).Name;
                if(sender == B_StorePosition)
                {
                    B_StorePosition.Text = "";
                }
                else if (sender == B_LoadPosition)
                {
                    B_LoadPosition.Text = "";
                }
                else if (sender == B_KeyForward)
                {
                    B_KeyForward.Text = "";
                }
                else if (sender == B_KeyUp)
                {
                    B_KeyUp.Text = "";
                }
                else if (sender == B_KeyDown)
                {
                    B_KeyDown.Text = "";
                }
            }
            else
            {
                settingInputKey = false;
            }
        }

        private void TB_MoveZAxis_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (float.TryParse(TB_MoveZAxis.Text, out float MovZAxisNew))
                {
                    if (MovZAxisNew > 0)
                        moveAmountZAxis = MovZAxisNew;
                }
            }
        }

        private void TB_MoveXYAxis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (float.TryParse(TB_MoveXYAxis.Text, out float MovXYAxisNew))
                {
                    if (MovXYAxisNew > 0)
                        moveAmountXYAxis = MovXYAxisNew;
                }
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if(settingInputKey)
            {
                var hotkey = e.KeyCode;

                if (hotkey == Keys.Escape)
                    hotkey = Keys.None;
                switch (CurrentKeyChange)
                {
                    case "B_KeyUp":
                        kUp = hotkey;
                        B_KeyUp.Text = kUp.ToString();
                        break;
                    case "B_KeyDown":
                        kDown = hotkey;
                        B_KeyDown.Text = kDown.ToString();
                        break;
                    case "B_KeyForward":
                        kForward = hotkey;
                        B_KeyForward.Text = kForward.ToString();
                        break;
                    case "B_StorePosition":
                        kStorePosition = hotkey;
                        B_StorePosition.Text = kStorePosition.ToString();
                        break;
                    case "B_LoadPosition":
                        kLoadPosition = hotkey;
                        B_LoadPosition.Text = kLoadPosition.ToString();
                        break;
                }

                m_KeyboardHook.UnregisterAllHotkeys();
                RegisterKeys();

                settingInputKey = false;
            }
        }
    }
}
