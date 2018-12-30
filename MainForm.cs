using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.Collections.Generic;
using System.Media;
#if BUILD64
using MemoryReads64;
using UniversalInt = System.Int64;
#else
using MemoryReads;
using UniversalInt = System.Int32;
#endif


namespace Flying47
{
    public partial class MainForm : Form
    {
        // Base address value for pointers.
        UniversalInt baseAddress = 0x00000000;

        // Other variables.
        System.Text.Encoding enc = System.Text.Encoding.UTF8;
        Process[] myProcess;
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

        uint CurrentKeyChange;
        bool settingInputKey = false;

        Bitmap bitmap;
        Graphics gBuffer;

        Gma.System.MouseKeyHook.IKeyboardEvents m_GlobalHook;


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
                    m_GlobalHook = Gma.System.MouseKeyHook.Hook.GlobalEvents();
                    m_GlobalHook.KeyDown += M_GlobalHook_KeyDown;
                }
                else
                    Application.Exit();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Close();
            }
        }

        enum KeysUsed : uint
        {
            storePosition,
            loadPosition,
            up,
            down,
            forward
        }

        bool foundProcess = false;

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                myProcess = Process.GetProcessesByName(processName);
                if (myProcess.Length > 0 )
                {
                    if(foundProcess == false || baseAddress == 0x0 )
                    {
                        TTimer.Interval = 1000;
                        IntPtr startOffset = myProcess[0].MainModule.BaseAddress;
#if BUILD64
                        baseAddress = startOffset.ToInt64();
#else
                        baseAddress = startOffset.ToInt32();
#endif
                        Debug.WriteLine("Trying to get baseAddresses");
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

                    positionAddress.ReadSet(myProcess, baseAddress, out readCoordX, out readCoordY, out readCoordZ);
                    L_X.Text = readCoordX.ToString();
                    L_Y.Text = readCoordY.ToString();
                    L_Z.Text = readCoordZ.ToString();
                    if(AnglesEnabled)
                    {
                        readSinAlpha = Trainer.ReadPointerFloat(myProcess, baseAddress + adrSinAlpha.baseaddress, adrSinAlpha.offsets);
                        if (isSinInverted)
                            readSinAlpha = -readSinAlpha;
                        readCosAlpha = Trainer.ReadPointerFloat(myProcess, baseAddress + adrCosAlpha.baseaddress, adrCosAlpha.offsets);
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
            float height = bitmap.Height;
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

        private void M_GlobalHook_KeyDown(object sender, KeyEventArgs e)
        {
            var hotkey = e.KeyCode;

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

        private void SendMeForward()
        {
            Trainer.WritePointerFloat(myProcess, baseAddress + positionAddress.X.baseaddress, positionAddress.X.offsets, readCoordX + readSinAlpha * moveAmountXYAxis);
            Trainer.WritePointerFloat(myProcess, baseAddress + positionAddress.Y.baseaddress, positionAddress.Y.offsets, readCoordY + readCosAlpha * moveAmountXYAxis);
        }

        private void SendMeDown()
        {
            Trainer.WritePointerFloat(myProcess, baseAddress + positionAddress.Z.baseaddress, positionAddress.Z.offsets, readCoordZ - moveAmountZAxis);
        }

        private void SendMeUp()
        {
            Trainer.WritePointerFloat(myProcess, baseAddress + positionAddress.Z.baseaddress, positionAddress.Z.offsets, readCoordZ + moveAmountZAxis);
        }

        private void Load_Position()
        {
            Trainer.WritePointerFloat(myProcess, baseAddress + positionAddress.X.baseaddress, positionAddress.X.offsets, storedCoordX);
            Trainer.WritePointerFloat(myProcess, baseAddress + positionAddress.Y.baseaddress, positionAddress.Y.offsets, storedCoordY);
            Trainer.WritePointerFloat(myProcess, baseAddress + positionAddress.Z.baseaddress, positionAddress.Z.offsets, storedCoordZ);
        }

        private void Save_Position()
        {
            storedCoordX = readCoordX;
            storedCoordY = readCoordY;
            storedCoordZ = readCoordZ;
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_GlobalHook.KeyDown -= M_GlobalHook_KeyDown;
        }

        private void B_ChangeButton(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                settingInputKey = true;
                if(sender == B_StorePosition)
                {
                    B_StorePosition.Text = "";
                    CurrentKeyChange = (uint)KeysUsed.storePosition;
                }
                else if (sender == B_LoadPosition)
                {
                    B_LoadPosition.Text = "";
                    CurrentKeyChange = (uint)KeysUsed.loadPosition;
                }
                else if (sender == B_KeyForward)
                {
                    B_KeyForward.Text = "";
                    CurrentKeyChange = (uint)KeysUsed.forward;
                }
                else if (sender == B_KeyUp)
                {
                    B_KeyUp.Text = "";
                    CurrentKeyChange = (uint)KeysUsed.up;
                }
                else if (sender == B_KeyDown)
                {
                    B_KeyDown.Text = "";
                    CurrentKeyChange = (uint)KeysUsed.down;
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
    }
}
