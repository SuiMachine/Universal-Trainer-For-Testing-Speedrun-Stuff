using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace MemoryReads64
{
	/// <summary>
	/// Pointer class used for reading from memory.
	/// </summary>
	public class Pointer
	{
		/// <summary>
		/// Base adress of a module (gets set when using trainer class read or write).
		/// </summary>
		public Int64 BaseModuleAddress { get; internal set; }
		/// <summary>
		/// Name of a module from which to get the address
		/// </summary>
		public string BaseModuleName { get; private set; }
		/// <summary>
		/// First offset (this gets added to base module adress
		/// </summary>
		public Int64 BaseOffset { get; private set; }
		/// <summary>
		/// Array of further offset.
		/// </summary>
		public int[] Offsets { get; private set; }
		/// <summary>
		/// Bool saying whatever it's absolute address or not
		/// </summary>
		public bool IsDirectPointer { get; private set; }
		/// <summary>
		/// Bool saying whatever the process is 64bit or not. This gets together with base module adress (and it has to be set correctly, in order for trainer class to treverse pointer offsets correctly).
		/// </summary>
		public bool IsPtrFor64BitProcess { get; internal set; }

		/// <summary>
		/// Constructor for pointer class.
		/// </summary>
		/// <param name="baseModuleName">Base module name (leave as string empty to use main process' main module).</param>
		/// <param name="baseOffset">First offset.</param>
		/// <param name="offsets">Further offsets.</param>
		public Pointer(string baseModuleName, Int64 baseOffset, int[] offsets)
		{
			this.BaseModuleName = baseModuleName.ToLower();
			this.BaseOffset = baseOffset;
			this.Offsets = offsets;
			this.IsDirectPointer = offsets.Length == 0;
		}

		/// <summary>
		/// Simplified constructor for used when no angles are provided in config file.
		/// </summary>
		/// <param name="baseaddress"></param>
		public Pointer(Int64 baseaddress)
		{
			this.BaseOffset = baseaddress;
			Offsets = new int[0];
			this.IsDirectPointer = false;
		}

		/// <summary>
		/// Tells whatever the base address is null or not.
		/// </summary>
		/// <returns>Bool value.</returns>
		internal bool IsNull()
		{
			return BaseOffset == 0;
		}
	}

	/// <summary>
	/// A set of 3 pointers describing position in 3d game (all of the reasonably written 3d games have them clamped together).
	/// </summary>
	public struct PositionSet_Pointer
	{
		/// <summary>
		/// Pointer for a point describing position on X axis.
		/// </summary>
		public Pointer X { get; private set; }
		/// <summary>
		/// Pointer for a point describing position on Y axis.
		/// </summary>
		public Pointer Y { get; private set; }
		/// <summary>
		/// Pointer for a point describing position on Z axis.
		/// </summary>
		public Pointer Z { get; private set; }


		/// <summary>
		/// Constructor for a struct, that creates 2 further pointers after providing it pointer to X axis.
		/// </summary>
		/// <param name="position">Pointer to X axis.</param>
		/// <param name="isXZY">Bool value of whatever the coordinates are stored as XZY, where Z is height.</param>
		/// <param name="byteGapLenth">Amount of bytes between the values.</param>
		public PositionSet_Pointer(Pointer position, int byteGapLenth, bool isXZY = false)
		{
			X = position;
			var offsets = position.Offsets;

			if (offsets.Length == 0)
			{
				if (!isXZY)
				{
					Y = new Pointer(position.BaseModuleName, position.BaseOffset + 0x4, new int[0]);
					Z = new Pointer(position.BaseModuleName, position.BaseOffset + 0x8, new int[0]);
				}
				else
				{
					Y = new Pointer(position.BaseModuleName, position.BaseOffset + 0x8, new int[0]);
					Z = new Pointer(position.BaseModuleName, position.BaseOffset + 0x4, new int[0]);
				}
			}
			else
			{
				if (!isXZY)
				{
					var copy1 = (int[])offsets.Clone();
					copy1[copy1.Length - 1] = copy1[copy1.Length - 1] + byteGapLenth;
					var copy2 = (int[])copy1.Clone();
					copy2[copy2.Length - 1] = copy2[copy1.Length - 1] + byteGapLenth;

					Y = new Pointer(position.BaseModuleName, position.BaseOffset, copy1);
					Z = new Pointer(position.BaseModuleName, position.BaseOffset, copy2);
				}
				else
				{
					var copy1 = (int[])offsets.Clone();
					copy1[copy1.Length - 1] = copy1[copy1.Length - 1] + byteGapLenth;
					var copy2 = (int[])copy1.Clone();
					copy2[copy2.Length - 1] = copy2[copy1.Length - 1] + byteGapLenth;

					Z = new Pointer(position.BaseModuleName, position.BaseOffset, copy1);
					Y = new Pointer(position.BaseModuleName, position.BaseOffset, copy2);
				}
			}
		}

		/// <summary>
		/// Reads entire set of pointers describing position in 3D space.
		/// </summary>
		/// <param name="proc">Process from which to read.</param>
		/// <param name="valX">Outputed float value for X axis.</param>
		/// <param name="valY">Outputed float value for Y axis.</param>
		/// <param name="valZ">Outputed float value for Z (height) axis.</param>
		public void ReadSet(Process proc, out float valX, out float valY, out float valZ)
		{
			valX = Trainer.ReadPointerFloat(proc, X);
			valY = Trainer.ReadPointerFloat(proc, Y);
			valZ = Trainer.ReadPointerFloat(proc, Z);
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

		/// <summary>
		/// Gets a base address for a module in a pointer and sets it in pointer struct
		/// </summary>
		/// <param name="Proc">Proces for which to get the base address.</param>
		/// <param name="pointerInstance">Pointer object for which to get address (module name is specified inside of pointer struct).</param>
		private static void GetModuleBaseAddress(Process Proc, Pointer pointerInstance)
		{
			pointerInstance.IsPtrFor64BitProcess = Proc.IsProcess64Bit();

			if (pointerInstance.BaseModuleName != String.Empty)
			{
				if (pointerInstance.IsPtrFor64BitProcess)
				{
					var modules = Proc.Modules;
					foreach (ProcessModule module in modules)
					{
						if (module.ModuleName.ToLower() == pointerInstance.BaseModuleName)
						{
							pointerInstance.BaseModuleAddress = module.BaseAddress.ToInt64();
							Debug.WriteLine("Address for manually specified module is: 0x" + pointerInstance.BaseModuleAddress.ToString("X8"));
						}
					}
				}
				else
				{
					//Getting a mo
					pointerInstance.BaseModuleAddress = (Int64)Proc.GetWow64ModuleBase(pointerInstance.BaseModuleName);
				}
			}
			else
			{
				pointerInstance.BaseModuleAddress = Proc.MainModule.BaseAddress.ToInt64();
			}
		}

		/// <summary>
		/// Reads a byte from external process memory.
		/// </summary>
		/// <param name="Proc">Process for which to read.</param>
		/// <param name="pointerInstance">Pointer object containing adresses and offsets.</param>
		/// <returns>Byte read from memory or 0 (on error).</returns>
		public static byte ReadPointerByte(Process Proc, Pointer pointerInstance)
		{
			byte Value = 0;
			checked
			{
				try
				{
					if (Proc != null)
					{
						if (pointerInstance.BaseModuleAddress != 0x0)
						{
							Int64 Bytes = 0;
							int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc.Id);
							Int64 Pointer = pointerInstance.BaseModuleAddress + pointerInstance.BaseOffset;

							if (Handle != 0)
							{
								if (pointerInstance.IsDirectPointer)
								{
									ReadProcessMemoryByte((int)Handle, Pointer, ref Value, 1, ref Bytes);
									CloseHandle(Handle);
								}
								else
								{
									foreach (long i in pointerInstance.Offsets)
									{
										ReadProcessMemoryInteger64((int)Handle, Pointer, ref Pointer, pointerInstance.IsPtrFor64BitProcess ? 8 : 4, ref Bytes);
										Pointer += i;
									}
									ReadProcessMemoryByte((int)Handle, Pointer, ref Value, 1, ref Bytes);
									CloseHandle(Handle);
								}
							}
						}
						else
						{
							GetModuleBaseAddress(Proc, pointerInstance);
						}
					}
				}
				catch
				{ }
			}
			return Value;
		}

		/// <summary>
		/// Reads an integer from external process memory.
		/// </summary>
		/// <param name="Proc">Process for which to read.</param>
		/// <param name="pointerInstance">Pointer object containing adresses and offsets.</param>
		/// <returns>Integer read from memory or 0 (on error).</returns>
		public static int ReadPointerInteger(Process Proc, Pointer pointerInstance)
		{
			int Value = 0;
			checked
			{
				try
				{
					if (Proc != null)
					{
						if (pointerInstance.BaseModuleAddress != 0x0)
						{
							Int64 Bytes = 0;
							int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc.Id);
							Int64 Pointer = pointerInstance.BaseModuleAddress + pointerInstance.BaseOffset;

							if (Handle != 0)
							{
								if (pointerInstance.IsDirectPointer)
								{
									ReadProcessMemoryInteger((int)Handle, Pointer, ref Value, 4, ref Bytes);
									CloseHandle(Handle);
								}
								else
								{
									foreach (long i in pointerInstance.Offsets)
									{
										ReadProcessMemoryInteger64((int)Handle, Pointer, ref Pointer, pointerInstance.IsPtrFor64BitProcess ? 8 : 4, ref Bytes);
										Pointer += i;
									}
									ReadProcessMemoryInteger((int)Handle, Pointer, ref Value, 4, ref Bytes);
									CloseHandle(Handle);
								}
							}
						}
						else
						{
							GetModuleBaseAddress(Proc, pointerInstance);
						}
					}
				}
				catch
				{ }
			}
			return Value;
		}

		/// <summary>
		/// Reads a float from external process memory.
		/// </summary>
		/// <param name="Proc">Process for which to read.</param>
		/// <param name="pointerInstance">Pointer object containing adresses and offsets.</param>
		/// <returns>Float read from memory or 0.0f (on error).</returns>
		public static float ReadPointerFloat(Process Proc, Pointer pointerInstance)
		{
			float Value = 0;
			checked
			{
				try
				{
					if (Proc != null)
					{
						if (pointerInstance.BaseModuleAddress != 0x0)
						{
							Int64 Bytes = 0;
							int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc.Id);
							Int64 Pointer = pointerInstance.BaseModuleAddress + pointerInstance.BaseOffset;

							if (Handle != 0)
							{
								if (pointerInstance.IsDirectPointer)
								{
									ReadProcessMemoryFloat((int)Handle, Pointer, ref Value, 4, ref Bytes);
									CloseHandle(Handle);
								}
								else
								{
									foreach (int i in pointerInstance.Offsets)
									{
										ReadProcessMemoryInteger64((int)Handle, Pointer, ref Pointer, pointerInstance.IsPtrFor64BitProcess ? 8 : 4, ref Bytes);
										Pointer += i;
									}
									ReadProcessMemoryFloat((int)Handle, Pointer, ref Value, 4, ref Bytes);
									CloseHandle(Handle);
								}
							}
						}
						else
						{
							GetModuleBaseAddress(Proc, pointerInstance);
						}
					}
				}
				catch
				{ }
			}
			return Value;
		}

		/// <summary>
		/// Reads a double from external process memory.
		/// </summary>
		/// <param name="Proc">Process for which to read.</param>
		/// <param name="pointerInstance">Pointer object containing adresses and offsets.</param>
		/// <returns>Double read from memory or 0.0d (on error).</returns>
		public static double ReadPointerDouble(Process Proc, Pointer pointerInstance)
		{
			double Value = 0;
			checked
			{
				try
				{
					if (Proc != null)
					{
						if (pointerInstance.BaseModuleAddress != 0x0)
						{
							Int64 Bytes = 0;
							int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc.Id);
							Int64 Pointer = pointerInstance.BaseModuleAddress + pointerInstance.BaseOffset;

							if (Handle != 0)
							{
								if (pointerInstance.IsDirectPointer)
								{
									ReadProcessMemoryDouble((int)Handle, Pointer, ref Value, 4, ref Bytes);
									CloseHandle(Handle);
								}
								else
								{
									foreach (int i in pointerInstance.Offsets)
									{
										ReadProcessMemoryInteger64((int)Handle, Pointer, ref Pointer, pointerInstance.IsPtrFor64BitProcess ? 8 : 4, ref Bytes);
										Pointer += i;
									}
									ReadProcessMemoryDouble((int)Handle, Pointer, ref Value, 8, ref Bytes);
									CloseHandle(Handle);
								}
							}
						}
						else
						{
							GetModuleBaseAddress(Proc, pointerInstance);
						}
					}
				}
				catch
				{ }
			}
			return Value;
		}

		/// <summary>
		/// Write a byte to external process memory.
		/// </summary>
		/// <param name="Proc">Process to which to write.</param>
		/// <param name="pointerInstance">Pointer object containing adresses and offsets.</param>
		/// <param name="Value">Value to write to memory.</param>
		public static void WritePointerByte(Process Proc, Pointer pointerInstance, byte Value)
		{
			checked
			{
				try
				{
					if (Proc != null)
					{
						if (pointerInstance.BaseModuleAddress != 0x0)
						{
							Int64 Bytes = 0;
							int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc.Id);
							Int64 Pointer = pointerInstance.BaseModuleAddress + pointerInstance.BaseOffset;

							if (Handle != 0)
							{
								if (pointerInstance.IsDirectPointer)
								{
									WriteProcessMemoryByte(Handle, Pointer, ref Value, 1, ref Bytes);
									CloseHandle(Handle);
								}
								else
								{
									foreach (int i in pointerInstance.Offsets)
									{
										ReadProcessMemoryInteger64((int)Handle, Pointer, ref Pointer, pointerInstance.IsPtrFor64BitProcess ? 8 : 4, ref Bytes);
										Pointer += i;
									}
									WriteProcessMemoryByte(Handle, Pointer, ref Value, 1, ref Bytes);
									CloseHandle(Handle);
								}
							}
						}
						else
						{
							GetModuleBaseAddress(Proc, pointerInstance);
						}
					}
				}
				catch
				{ }
			}
		}

		/// <summary>
		/// Write an integer to external process memory.
		/// </summary>
		/// <param name="Proc">Process to which to write.</param>
		/// <param name="pointerInstance">Pointer object containing adresses and offsets.</param>
		/// <param name="Value">Value to write to memory.</param>
		public static void WritePointerInteger(Process Proc, Pointer pointerInstance, int Value)
		{
			checked
			{
				try
				{
					if (Proc != null)
					{
						if (pointerInstance.BaseModuleAddress != 0x0)
						{
							Int64 Bytes = 0;
							int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc.Id);
							Int64 Pointer = pointerInstance.BaseModuleAddress + pointerInstance.BaseOffset;

							if (Handle != 0)
							{
								if (pointerInstance.IsDirectPointer)
								{
									WriteProcessMemoryInteger(Handle, Pointer, ref Value, 4, ref Bytes);
									CloseHandle(Handle);
								}
								else
								{
									foreach (int i in pointerInstance.Offsets)
									{
										ReadProcessMemoryInteger64((int)Handle, Pointer, ref Pointer, pointerInstance.IsPtrFor64BitProcess ? 8 : 4, ref Bytes);
										Pointer += i;
									}
									WriteProcessMemoryInteger(Handle, Pointer, ref Value, 4, ref Bytes);
									CloseHandle(Handle);
								}
							}
						}
						else
						{
							GetModuleBaseAddress(Proc, pointerInstance);
						}
					}
				}
				catch
				{ }
			}
		}

		/// <summary>
		/// Write a float to external process memory.
		/// </summary>
		/// <param name="Proc">Process to which to write.</param>
		/// <param name="pointerInstance">Pointer object containing adresses and offsets.</param>
		/// <param name="Value">Value to write to memory.</param>
		public static void WritePointerFloat(Process Proc, Pointer pointerInstance, float Value)
		{
			checked
			{
				try
				{
					if (Proc != null)
					{
						if (pointerInstance.BaseModuleAddress != 0x0)
						{
							Int64 Bytes = 0;
							int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc.Id);
							Int64 Pointer = pointerInstance.BaseModuleAddress + pointerInstance.BaseOffset;

							if (Handle != 0)
							{
								if (pointerInstance.IsDirectPointer)
								{
									WriteProcessMemoryFloat(Handle, Pointer, ref Value, 4, ref Bytes);
									CloseHandle(Handle);
								}
								else
								{
									foreach (int i in pointerInstance.Offsets)
									{
										ReadProcessMemoryInteger64((int)Handle, Pointer, ref Pointer, pointerInstance.IsPtrFor64BitProcess ? 8 : 4, ref Bytes);
										Pointer += i;
									}
									WriteProcessMemoryFloat(Handle, Pointer, ref Value, 4, ref Bytes);
									CloseHandle(Handle);
								}
							}
						}
						else
						{
							GetModuleBaseAddress(Proc, pointerInstance);
						}
					}
				}
				catch
				{ }
			}
		}

		/// <summary>
		/// Write a double to external process memory.
		/// </summary>
		/// <param name="Proc">Process to which to write.</param>
		/// <param name="pointerInstance">Pointer object containing adresses and offsets.</param>
		/// <param name="Value">Value to write to memory.</param>
		public static void WritePointerDouble(Process Proc, Pointer pointerInstance, double Value)
		{
			checked
			{
				try
				{
					if (Proc != null)
					{
						if (pointerInstance.BaseModuleAddress != 0x0)
						{
							Int64 Bytes = 0;
							int Handle = OpenProcess(PROCESS_ALL_ACCESS, 0, Proc.Id);
							Int64 Pointer = pointerInstance.BaseModuleAddress + pointerInstance.BaseOffset;

							if (Handle != 0)
							{
								if (pointerInstance.IsDirectPointer)
								{
									WriteProcessMemoryDouble(Handle, Pointer, ref Value, 8, ref Bytes);
									CloseHandle(Handle);
								}
								else
								{
									foreach (int i in pointerInstance.Offsets)
									{
										ReadProcessMemoryInteger64((int)Handle, Pointer, ref Pointer, pointerInstance.IsPtrFor64BitProcess ? 8 : 4, ref Bytes);
										Pointer += i;
									}
									WriteProcessMemoryDouble(Handle, Pointer, ref Value, 8, ref Bytes);
									CloseHandle(Handle);
								}
							}
						}
						else
						{
							GetModuleBaseAddress(Proc, pointerInstance);
						}
					}
				}
				catch
				{ }
			}
		}
	}

	/// <summary>
	/// Extension methods for a process.
	/// </summary>
	public static class ProcessExtansions
	{
		[DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsWow64Process([In] IntPtr process, [Out] out bool wow64Process);

		/// <summary>
		/// Checks whatever process is 64bit or not.
		/// </summary>
		/// <param name="proc">Process which to check.</param>
		/// <returns>Bool value (true if 64bit, false if 32bit)</returns>
		public static bool IsProcess64Bit(this Process proc)
		{
			if (IsWow64Process(proc.Handle, out bool retValue))
				return !retValue;
			else
				throw new Exception("Failed to check whatever process is 64bit!");
		}

		/// <summary>
		/// Gets a handle for a window in foreground
		/// </summary>
		/// <returns>Handle of the window or null</returns>
		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();

		/// <summary>
		/// Gets a process ID from window handle
		/// </summary>
		/// <param name="hWnd">Handle of the window</param>
		/// <param name="processId">Process that window belongs to</param>
		/// <returns>Thread identifier ID</returns>
		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint processId);


		[DllImport("psapi.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool EnumProcessModulesEx(IntPtr hProcess, [Out] IntPtr[] lphModule, uint cb,
			out uint lpcbNeeded, uint dwFilterFlag);

		[DllImport("psapi.dll")]
		private static extern uint GetModuleBaseName(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName,
			uint nSize);

		[DllImport("psapi.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetModuleInformation(IntPtr hProcess, IntPtr hModule, [Out] out MODULEINFO lpmodinfo,
		  uint cb);

		[StructLayout(LayoutKind.Sequential)]
		public struct MODULEINFO
		{
			public IntPtr lpBaseOfDll;
			public uint SizeOfImage;
			public IntPtr EntryPoint;
		}

		/// <summary>
		/// Gets a base address of module inside of the 32-bit process.
		/// </summary>
		/// <param name="gameProcess">Process for which to get base address.</param>
		/// <param name="moduleName">Module nume for which to get base address (lowercase!).</param>
		/// <returns>Base address of a module or 0 if none found.</returns>
		public static IntPtr GetWow64ModuleBase(this Process gameProcess, string moduleName)
		{
			const int LIST_MODULES_ALL = 5;
			const int MAX_PATH = 260;
			var hModules = new IntPtr[1024];

			uint cb = (uint)IntPtr.Size * (uint)hModules.Length;
			if (!EnumProcessModulesEx(gameProcess.Handle, hModules, cb, out uint cbNeeded, LIST_MODULES_ALL))
				throw new Win32Exception();
			uint numMods = cbNeeded / (uint)IntPtr.Size;

			var sb = new StringBuilder(MAX_PATH);
			for (int i = 0; i < numMods; i++)
			{
				sb.Clear();
				if (GetModuleBaseName(gameProcess.Handle, hModules[i], sb, (uint)sb.Capacity) == 0)
					throw new Win32Exception();
				string baseName = sb.ToString();

				if (baseName.ToLower() == moduleName)
				{
					var moduleInfo = new MODULEINFO();
					if (!GetModuleInformation(gameProcess.Handle, hModules[i], out moduleInfo,
											  (uint)Marshal.SizeOf(moduleInfo)))
					{
						throw new Win32Exception();
					}
					return moduleInfo.lpBaseOfDll;
				}
			}

			return IntPtr.Zero;
		}
	}
}