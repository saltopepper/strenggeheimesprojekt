using System;
using System.Windows.Forms;
using FlowCalibrationInterface;
using System.Reflection;
using DynAbg.valve;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace DynAbg
{
    static class Program
    {
        internal static FormMain formMain;
		internal static PrivateFontCollection fonts;

        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //test();
			//int b = 2;

			fonts = GetEmbeddedFonts();
            InitPrivateFonts();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			formMain = new FormMain();

			Data.valveInfos = new List<ValveInfo>();

			try
			{
				ICalibration calibration = new FlowCalibration.Calibration();

                if (calibration == null)
				{
					MessageBox.Show("Die Bibliothek zum hydraulischen Abgleich konnte nicht geladen werden.");
					return;
				}

				Data.calibration = calibration;

				Assembly assembly = Assembly.GetAssembly(formMain.GetType());
				string[] resourceNames = assembly.GetManifestResourceNames();
				string search = (assembly.GetName().Name + ".Resources.liste_").ToLower();

				foreach (string resourceName in resourceNames)
				{
					if (!resourceName.ToLower().StartsWith(search))
						continue;

					string manufacturer = resourceName.Substring(search.Length, resourceName.Length - search.Length - 4);
					
					StringBuilder builder = new StringBuilder(manufacturer);
					builder[0] = manufacturer.ToUpper()[0];
					manufacturer = builder.ToString();

					StreamReader reader = new StreamReader(assembly.GetManifestResourceStream(resourceName), System.Text.Encoding.GetEncoding(852));

					while (!reader.EndOfStream)
					{
						String line = reader.ReadLine();

						string[] values = line.Split(';');

						ValveInfo valveInfo = new ValveInfo();
						valveInfo.Manufacturer = manufacturer;
						valveInfo.TGA = values[1];
						valveInfo.Description = values[5];

						Data.valveInfos.Add(valveInfo);
					}
				}
                formMain.loadOption_Click(null, null);
			}
			catch (CalibrationException exception)
			{
				Console.WriteLine(exception);
				return;
			}


			Application.Run(formMain);
        }

		private static Stream GetResourceStream(string embeddedFileName)
		{
			Assembly assembly = Assembly.GetCallingAssembly();
			Stream stream = assembly.GetManifestResourceStream(assembly.GetName().Name + ".Resources." + embeddedFileName);
			return stream;
		}

		public static string GetApplicationsPath()
		{
			FileInfo fi = new FileInfo(Assembly.GetEntryAssembly().Location);
			return fi.DirectoryName;
		}

		public static PrivateFontCollection GetEmbeddedFonts()
		{
			PrivateFontCollection fontCollection = new PrivateFontCollection();
			Assembly asm = Assembly.GetCallingAssembly();

			string[] embeddedResources = asm.GetManifestResourceNames();

			foreach (string resourceName in embeddedResources)
			{
				if (resourceName.ToLower().EndsWith("ttf"))
				{
					Stream fontStream = asm.GetManifestResourceStream(resourceName);
					byte[] buffer = new byte[fontStream.Length];

					fontStream.Read(buffer, 0, (int)fontStream.Length);
					fontStream.Close();

					GCHandle pinnedArray = GCHandle.Alloc(buffer, GCHandleType.Pinned);
					IntPtr pointer = pinnedArray.AddrOfPinnedObject();
					fontCollection.AddMemoryFont(pointer, buffer.Length);
					pinnedArray.Free();
				}
			}

			return fontCollection;
		}

		internal static Font GetFont(string name, int size)
		{
			foreach (FontFamily font in fonts.Families)
				if (font.Name.Equals(name))
					return new Font(font, size);

			return null;
		}

        private static void InitPrivateFonts()
        {
            Assembly asm = Assembly.GetCallingAssembly();
            string[] embeddedResources = asm.GetManifestResourceNames();

            foreach (string resourceName in embeddedResources)
            {
                if (resourceName.ToLower().EndsWith("ttf"))
                {
                    Stream fontStream = asm.GetManifestResourceStream(resourceName);
                    byte[] buffer = new byte[fontStream.Length];

                    fontStream.Read(buffer, 0, (int)fontStream.Length);
                    fontStream.Close();

                    //XPrivateFontCollection.Global.AddFont(buffer);
                }
            }
        }

		//unsafe static void test3(int* b)
		//{
		//    b[0] = 3;
		//}
        //unsafe static void test()
        //{
        //    double[] testD = new double[3];
        //    double[] testD2 = new double[1];

        //    fixed (double* ptr = &testD[1], ptr2 = testD2)
        //    {
        //        test2(ptr, ptr2);
        //    }

        //    Console.WriteLine(testD2[0]);
        //}

        //unsafe static void test2(double* data, double* result)
        //{
        //    data[1] = 12;
        //    data[2] = 13;
        //    *result = data[1] + data[2];
        //}

	}
}
