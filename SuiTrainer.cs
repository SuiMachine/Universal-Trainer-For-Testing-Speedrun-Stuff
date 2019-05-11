using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MemoryReads64
{
    public class Pointer
    {
        public Int64 baseaddress { get; private set; }
        public Int64[] offsets { get; private set; }

        public Pointer(Int64 baseaddress, Int64[] offsets)
        {
            this.baseaddress = baseaddress;
            this.offsets = offsets;
        }

        public Pointer(Int64 baseaddress)
        {
            this.baseaddress = baseaddress;
            offsets = new Int64[0];
        }

        internal bool IsNull()
        {
            return baseaddress == 0;
        }
    }

    public struct PositionSet
    {
        public Pointer X { get; private set; }
        public Pointer Y { get; private set; }
        public Pointer Z { get; private set; }

        public PositionSet(Pointer position, bool isXZY = false)
        {
            X = position;
            var offsets = position.offsets;
            if(!isXZY)
            {
                var copy1 = (long[])offsets.Clone();
                copy1[copy1.Length - 1] = copy1[copy1.Length - 1] + 0x4;
                var copy2 = (long[])copy1.Clone();
                copy2[copy2.Length - 1] = copy2[copy1.Length - 1] + 0x4;

                Y = new Pointer(position.baseaddress, copy1);
                Z = new Pointer(position.baseaddress, copy2);
            }
            else
            {
                var copy1 = (long[])offsets.Clone();
                copy1[copy1.Length - 1] = copy1[copy1.Length-1] + 0x4;
                var copy2 = (long[])copy1.Clone();
                copy2[copy2.Length - 1] = copy2[copy1.Length - 1] + 0x4;

                Z = new Pointer(position.baseaddress, copy1);
                Y = new Pointer(position.baseaddress, copy2);
            }
        }

        public void ReadSet(Process[] proc, long baseAddress, out float valX, out float valY, out float valZ)
        {
            valX = Trainer.ReadPointerFloat(proc, baseAddress + X.baseaddress, X.offsets);
            valY = Trainer.ReadPointerFloat(proc, baseAddress + Y.baseaddress, Y.offsets);
            valZ = Trainer.ReadPointerFloat(proc, baseAddress + Z.baseaddress, Z.offsets);
        }
    }

    public class Trainer
    {
        private const int PROCESS_ALL_ACCESS = 0x1F0FFF;
        [DllImport("kernel32")]
        private static extern int OpenProcess(int AccessType, int InheritHandle, int ProcessId);
        [DllImport("kernel32", EntryPoint = "WriteProcessMemory")]
        private static extern byte WriteProcessMemoryByte(int Handle, Int64 Address, ref byte Value, int Size, ref Int64 BytesWritten);
        [DllImport("kernel32", EntryPoint = "WriteProcessMemory")]
        private static extern int WriteProcessMemoryInteger(int Handle, Int64 Address, ref int Value, int Size, ref Int64 BytesWritten);
        [DllImport("kernel32", EntryPoint = "WriteProcessMemory")]
        private static extern float WriteProcessMemoryFloat(int Handle, Int64 Address, ref float Value, int Size, ref Int64 BytesWritten);
        [DllImport("kernel32", EntryPoint = "WriteProcessMemory")]
        private static extern double WriteProcessMemoryDouble(int Handle, Int64 Address, ref double Value, int Size, ref Int64 BytesWritten);


        [DllImport("kernel32", EntryPoint = "ReadProcessMemory")]
        private static extern byte ReadProcessMemoryByte(int Handle, Int64 Address, ref byte Value, int Size, ref Int64 BytesRead);
        [DllImport("kernel32", EntryPoint = "ReadProcessMemory")]
        private static extern int ReadProcessMemoryInteger(int Handle, Int64 Address, ref int Value, int Size, ref Int64 BytesRead);
        [DllImport("kernel32", EntryPoint = "ReadProcessMemory")]
        private static extern Int64 ReadProcessMemoryInteger64(int Handle, Int64 Address, ref Int64 Value, int Size, ref Int64 BytesRead);
        [DllImport("kernel32", EntryPoint = "ReadProcessMemory")]
        private static extern float ReadProcessMemoryFloat(int Handle, Int64 Address, ref float Value, int Size, ref Int64 BytesRead);
        [DllImport("kernel32", EntryPoint = "ReadProcessMemory")]
        private static extern double ReadProcessMemoryDouble(int Handle, Int64 Address, ref double Value, int Size, ref Int64 BytesRead);
        [DllImport("kernel32")]
        private static extern int CloseHandle(int Handle);



        public static byte ReadByte(Process[] Proc, Int64 Address)
        {
            byte Value = 0;
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        Int64 Bytes = 0;
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            ReadProcessMemoryByte(Handle, Address, ref Value, 1, ref Bytes);
                            CloseHandle(Handle);
                        }
                    }
                }
                catch
                { }
            }
            return Value;
        }
        public static int ReadInteger(Process[] Proc, Int64 Address)
        {
            int Value = 0;
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        Int64 Bytes = 0;
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            ReadProcessMemoryInteger(Handle, Address, ref Value, 4, ref Bytes);
                            CloseHandle(Handle);
                        }
                    }
                }
                catch
                { }
            }
            return Value;
        }
        public static float ReadFloat(Process[] Proc, Int64 Address)
        {
            float Value = 0;
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        Int64 Bytes = 0;
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            ReadProcessMemoryFloat((int)Handle, Address, ref Value, 4, ref Bytes);
                            CloseHandle(Handle);
                        }
                    }
                }
                catch
                { }
            }
            return Value;
        }
        public static double ReadDouble(Process[] Proc, Int64 Address)
        {
            double Value = 0;
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        Int64 Bytes = 0;
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            ReadProcessMemoryDouble((int)Handle, Address, ref Value, 8, ref Bytes);
                            CloseHandle(Handle);
                        }
                    }
                }
                catch
                { }
            }
            return Value;
        }

        public static byte ReadPointerByte(Process[] Proc, Int64 Pointer, Int64[] Offset)
        {
            byte Value = 0;
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        Int64 Bytes = 0;
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            foreach (int i in Offset)
                            {
                                ReadProcessMemoryInteger64((int)Handle, Pointer, ref Pointer, 8, ref Bytes);
                                Pointer += i;
                            }
                            ReadProcessMemoryByte((int)Handle, Pointer, ref Value, 2, ref Bytes);
                            CloseHandle(Handle);
                        }
                    }
                }
                catch
                { }
            }
            return Value;
        }
        public static int ReadPointerInteger(Process[] Proc, Int64 Pointer, Int64[] Offset)
        {
            int Value = 0;
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        Int64 Bytes = 0;
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            foreach (int i in Offset)
                            {
                                ReadProcessMemoryInteger64((int)Handle, Pointer, ref Pointer, 8, ref Bytes);
                                Pointer += i;
                            }
                            ReadProcessMemoryInteger((int)Handle, Pointer, ref Value, 4, ref Bytes);
                            CloseHandle(Handle);
                        }
                    }
                }
                catch
                { }
            }
            return Value;
        }

        public static float ReadPointerFloat(Process[] Proc, Int64 Pointer, Int64[] Offset)
        {
            float Value = 0;
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        Int64 Bytes = 0;
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            foreach (int i in Offset)
                            {
                                ReadProcessMemoryInteger64((int)Handle, Pointer, ref Pointer, 8, ref Bytes);
                                Pointer += i;
                            }
                            ReadProcessMemoryFloat((int)Handle, Pointer, ref Value, 4, ref Bytes);
                            CloseHandle(Handle);
                        }
                    }
                }
                catch
                { }
            }
            return Value;
        }
        public static double ReadPointerDouble(Process[] Proc, Int64 Pointer, Int64[] Offset)
        {
            double Value = 0;
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        Int64 Bytes = 0;
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            foreach (int i in Offset)
                            {
                                ReadProcessMemoryInteger64((int)Handle, Pointer, ref Pointer, 8, ref Bytes);
                                Pointer += i;
                            }
                            ReadProcessMemoryDouble((int)Handle, Pointer, ref Value, 8, ref Bytes);
                            CloseHandle(Handle);
                        }
                    }
                }
                catch
                { }
            }
            return Value;
        }

        public static void WriteByte(Process[] Proc, Int64 Address, byte Value)
        {
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        Int64 Bytes = 0;
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            WriteProcessMemoryByte(Handle, Address, ref Value, 1, ref Bytes);
                        }
                        CloseHandle(Handle);
                    }
                }
                catch
                { }
            }
        }
        public static void WriteInteger(Process[] Proc, Int64 Address, int Value)
        {
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        Int64 Bytes = 0;
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            WriteProcessMemoryInteger(Handle, Address, ref Value, 4, ref Bytes);
                        }
                        CloseHandle(Handle);
                    }
                }
                catch
                { }
            }
        }
        public static void WriteFloat(Process[] Proc, Int64 Address, float Value)
        {
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        Int64 Bytes = 0;
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            WriteProcessMemoryFloat(Handle, Address, ref Value, 4, ref Bytes);
                        }
                        CloseHandle(Handle);
                    }

                }
                catch
                { }
            }
        }
        public static void WriteDouble(Process[] Proc, Int64 Address, double Value)
        {
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        Int64 Bytes = 0;
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            WriteProcessMemoryDouble(Handle, Address, ref Value, 8, ref Bytes);
                        }
                        CloseHandle(Handle);
                    }
                }
                catch
                { }
            }
        }

        public static void WritePointerByte(Process[] Proc, Int64 Pointer, Int64[] Offset, byte Value)
        {
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            Int64 Bytes = 0;
                            foreach (int i in Offset)
                            {
                                ReadProcessMemoryInteger64(Handle, Pointer, ref Pointer, 8, ref Bytes);
                                Pointer += i;
                            }
                            WriteProcessMemoryByte(Handle, Pointer, ref Value, 2, ref Bytes);
                        }
                        CloseHandle(Handle);
                    }
                }
                catch
                { }
            }
        }
        public static void WritePointerInteger(Process[] Proc, Int64 Pointer, Int64[] Offset, int Value)
        {
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            Int64 Bytes = 0;
                            foreach (int i in Offset)
                            {
                                ReadProcessMemoryInteger64(Handle, Pointer, ref Pointer, 8, ref Bytes);
                                Pointer += i;
                            }
                            WriteProcessMemoryInteger(Handle, Pointer, ref Value, 4, ref Bytes);
                        }
                        CloseHandle(Handle);
                    }
                }
                catch
                { }
            }
        }
        public static void WritePointerFloat(Process[] Proc, Int64 Pointer, Int64[] Offset, float Value)
        {
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            Int64 Bytes = 0;
                            foreach (int i in Offset)
                            {
                                ReadProcessMemoryInteger64(Handle, Pointer, ref Pointer, 8, ref Bytes);
                                Pointer += i;
                            }
                            WriteProcessMemoryFloat(Handle, Pointer, ref Value, 4, ref Bytes);
                        }
                        CloseHandle(Handle);
                    }
                }
                catch
                { }
            }
        }
        public static void WritePointerDouble(Process[] Proc, Int64 Pointer, Int64[] Offset, double Value)
        {
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            Int64 Bytes = 0;
                            foreach (int i in Offset)
                            {
                                ReadProcessMemoryInteger64(Handle, Pointer, ref Pointer, 8, ref Bytes);
                                Pointer += i;
                            }
                            WriteProcessMemoryDouble(Handle, Pointer, ref Value, 8, ref Bytes);
                        }
                        CloseHandle(Handle);
                    }
                }
                catch
                { }
            }
        }
    }
}

namespace MemoryReads
{
    public class Pointer
    {
        public int baseaddress { get; private set; }
        public int[] offsets { get; private set; }

        public Pointer(int baseaddress, int[] offsets)
        {
            this.baseaddress = baseaddress;
            this.offsets = offsets;
        }

        public Pointer(int baseaddress)
        {
            this.baseaddress = baseaddress;
            offsets = new int[0];
        }

        internal bool IsNull()
        {
            return baseaddress == 0;
        }
    }

    public struct PositionSet
    {
        public Pointer X { get; private set; }
        public Pointer Y { get; private set; }
        public Pointer Z { get; private set; }

        public PositionSet(Pointer position, bool isXZY = false)
        {
            X = position;
            var offsets = position.offsets;

            if(offsets.Length == 0)
            {

            }
            if (!isXZY)
            {
                var copy1 = (int[])offsets.Clone();
                copy1[copy1.Length - 1] = copy1[copy1.Length - 1] + 0x4;
                var copy2 = (int[])copy1.Clone();
                copy2[copy2.Length - 1] = copy2[copy1.Length - 1] + 0x4;

                Y = new Pointer(position.baseaddress, copy1);
                Z = new Pointer(position.baseaddress, copy2);
            }
            else
            {
                var copy1 = (int[])offsets.Clone();
                copy1[copy1.Length - 1] = copy1[copy1.Length - 1] + 0x4;
                var copy2 = (int[])copy1.Clone();
                copy2[copy2.Length - 1] = copy2[copy1.Length - 1] + 0x4;

                Z = new Pointer(position.baseaddress, copy1);
                Y = new Pointer(position.baseaddress, copy2);
            }
        }

        public void ReadSet(Process[] proc, int baseAddress, out float valX, out float valY, out float valZ)
        {
            valX = Trainer.ReadPointerFloat(proc, baseAddress + X.baseaddress, X.offsets);
            valY = Trainer.ReadPointerFloat(proc, baseAddress + Y.baseaddress, Y.offsets);
            valZ = Trainer.ReadPointerFloat(proc, baseAddress + Z.baseaddress, Z.offsets);
        }
    }

    public class Trainer
    {
        private const int PROCESS_ALL_ACCESS = 0x1F0FFF;
        [DllImport("kernel32")]
        private static extern int OpenProcess(int AccessType, int InheritHandle, int ProcessId);
        [DllImport("kernel32", EntryPoint = "WriteProcessMemory")]
        private static extern byte WriteProcessMemoryByte(int Handle, int Address, ref byte Value, int Size, ref int BytesWritten);
        [DllImport("kernel32", EntryPoint = "WriteProcessMemory")]
        private static extern int WriteProcessMemoryInteger(int Handle, int Address, ref int Value, int Size, ref int BytesWritten);
        [DllImport("kernel32", EntryPoint = "WriteProcessMemory")]
        private static extern float WriteProcessMemoryFloat(int Handle, int Address, ref float Value, int Size, ref int BytesWritten);
        [DllImport("kernel32", EntryPoint = "WriteProcessMemory")]
        private static extern double WriteProcessMemoryDouble(int Handle, int Address, ref double Value, int Size, ref int BytesWritten);


        [DllImport("kernel32", EntryPoint = "ReadProcessMemory")]
        private static extern byte ReadProcessMemoryByte(int Handle, int Address, ref byte Value, int Size, ref int BytesRead);
        [DllImport("kernel32", EntryPoint = "ReadProcessMemory")]
        private static extern int ReadProcessMemoryInteger(int Handle, int Address, ref int Value, int Size, ref int BytesRead);
        [DllImport("kernel32", EntryPoint = "ReadProcessMemory")]
        private static extern float ReadProcessMemoryFloat(int Handle, int Address, ref float Value, int Size, ref int BytesRead);
        [DllImport("kernel32", EntryPoint = "ReadProcessMemory")]
        private static extern double ReadProcessMemoryDouble(int Handle, int Address, ref double Value, int Size, ref int BytesRead);
        [DllImport("kernel32")]
        private static extern int CloseHandle(int Handle);



        public static byte ReadByte(Process[] Proc, int Address)
        {
            byte Value = 0;
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        int Bytes = 0;
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            ReadProcessMemoryByte(Handle, Address, ref Value, 1, ref Bytes);
                            CloseHandle(Handle);
                        }
                    }
                }
                catch
                { }
            }
            return Value;
        }
        public static int ReadInteger(Process[] Proc, int Address)
        {
            int Value = 0;
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        int Bytes = 0;
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            ReadProcessMemoryInteger(Handle, Address, ref Value, 4, ref Bytes);
                            CloseHandle(Handle);
                        }
                    }
                }
                catch
                { }
            }
            return Value;
        }
        public static float ReadFloat(Process[] Proc, int Address)
        {
            float Value = 0;
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        int Bytes = 0;
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            ReadProcessMemoryFloat((int)Handle, Address, ref Value, 4, ref Bytes);
                            CloseHandle(Handle);
                        }
                    }
                }
                catch
                { }
            }
            return Value;
        }
        public static double ReadDouble(Process[] Proc, int Address)
        {
            double Value = 0;
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        int Bytes = 0;
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            ReadProcessMemoryDouble((int)Handle, Address, ref Value, 8, ref Bytes);
                            CloseHandle(Handle);
                        }
                    }
                }
                catch
                { }
            }
            return Value;
        }

        public static byte ReadPointerByte(Process[] Proc, int Pointer, int[] Offset)
        {
            byte Value = 0;
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        int Bytes = 0;
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            foreach (int i in Offset)
                            {
                                ReadProcessMemoryInteger((int)Handle, Pointer, ref Pointer, 4, ref Bytes);
                                Pointer += i;
                            }
                            ReadProcessMemoryByte((int)Handle, Pointer, ref Value, 2, ref Bytes);
                            CloseHandle(Handle);
                        }
                    }
                }
                catch
                { }
            }
            return Value;
        }
        public static int ReadPointerInteger(Process[] Proc, int Pointer, int[] Offset)
        {
            int Value = 0;
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        int Bytes = 0;
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            foreach (int i in Offset)
                            {
                                ReadProcessMemoryInteger((int)Handle, Pointer, ref Pointer, 4, ref Bytes);
                                Pointer += i;
                            }
                            ReadProcessMemoryInteger((int)Handle, Pointer, ref Value, 4, ref Bytes);
                            CloseHandle(Handle);
                        }
                    }
                }
                catch
                { }
            }
            return Value;
        }
        public static float ReadPointerFloat(Process[] Proc, int Pointer, int[] Offset)
        {
            float Value = 0;
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        int Bytes = 0;
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            foreach (int i in Offset)
                            {
                                ReadProcessMemoryInteger((int)Handle, Pointer, ref Pointer, 4, ref Bytes);
                                Pointer += i;
                            }
                            ReadProcessMemoryFloat((int)Handle, Pointer, ref Value, 4, ref Bytes);
                            CloseHandle(Handle);
                        }
                    }
                }
                catch
                { }
            }
            return Value;
        }
        public static double ReadPointerDouble(Process[] Proc, int Pointer, int[] Offset)
        {
            double Value = 0;
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        int Bytes = 0;
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            foreach (int i in Offset)
                            {
                                ReadProcessMemoryInteger((int)Handle, Pointer, ref Pointer, 4, ref Bytes);
                                Pointer += i;
                            }
                            ReadProcessMemoryDouble((int)Handle, Pointer, ref Value, 8, ref Bytes);
                            CloseHandle(Handle);
                        }
                    }
                }
                catch
                { }
            }
            return Value;
        }

        public static void WriteByte(Process[] Proc, int Address, byte Value)
        {
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        int Bytes = 0;
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            WriteProcessMemoryByte(Handle, Address, ref Value, 1, ref Bytes);
                        }
                        CloseHandle(Handle);
                    }
                }
                catch
                { }
            }
        }
        public static void WriteInteger(Process[] Proc, int Address, int Value)
        {
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        int Bytes = 0;
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            WriteProcessMemoryInteger(Handle, Address, ref Value, 4, ref Bytes);
                        }
                        CloseHandle(Handle);
                    }
                }
                catch
                { }
            }
        }
        public static void WriteFloat(Process[] Proc, int Address, float Value)
        {
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        int Bytes = 0;
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            WriteProcessMemoryFloat(Handle, Address, ref Value, 4, ref Bytes);
                        }
                        CloseHandle(Handle);
                    }

                }
                catch
                { }
            }
        }
        public static void WriteDouble(Process[] Proc, int Address, double Value)
        {
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        int Bytes = 0;
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            WriteProcessMemoryDouble(Handle, Address, ref Value, 8, ref Bytes);
                        }
                        CloseHandle(Handle);
                    }
                }
                catch
                { }
            }
        }

        public static void WritePointerByte(Process[] Proc, int Pointer, int[] Offset, byte Value)
        {
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            int Bytes = 0;
                            foreach (int i in Offset)
                            {
                                ReadProcessMemoryInteger(Handle, Pointer, ref Pointer, 4, ref Bytes);
                                Pointer += i;
                            }
                            WriteProcessMemoryByte(Handle, Pointer, ref Value, 2, ref Bytes);
                        }
                        CloseHandle(Handle);
                    }
                }
                catch
                { }
            }
        }
        public static void WritePointerInteger(Process[] Proc, int Pointer, int[] Offset, int Value)
        {
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            int Bytes = 0;
                            foreach (int i in Offset)
                            {
                                ReadProcessMemoryInteger(Handle, Pointer, ref Pointer, 4, ref Bytes);
                                Pointer += i;
                            }
                            WriteProcessMemoryInteger(Handle, Pointer, ref Value, 4, ref Bytes);
                        }
                        CloseHandle(Handle);
                    }
                }
                catch
                { }
            }
        }
        public static void WritePointerFloat(Process[] Proc, int Pointer, int[] Offset, float Value)
        {
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            int Bytes = 0;
                            foreach (int i in Offset)
                            {
                                ReadProcessMemoryInteger(Handle, Pointer, ref Pointer, 4, ref Bytes);
                                Pointer += i;
                            }
                            WriteProcessMemoryFloat(Handle, Pointer, ref Value, 4, ref Bytes);
                        }
                        CloseHandle(Handle);
                    }
                }
                catch
                { }
            }
        }
        public static void WritePointerDouble(Process[] Proc, int Pointer, int[] Offset, double Value)
        {
            checked
            {
                try
                {
                    if (Proc.Length != 0)
                    {
                        int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc[0].Id);
                        if (Handle != 0)
                        {
                            int Bytes = 0;
                            foreach (int i in Offset)
                            {
                                ReadProcessMemoryInteger(Handle, Pointer, ref Pointer, 4, ref Bytes);
                                Pointer += i;
                            }
                            WriteProcessMemoryDouble(Handle, Pointer, ref Value, 8, ref Bytes);
                        }
                        CloseHandle(Handle);
                    }
                }
                catch
                { }
            }
        }
    }
}