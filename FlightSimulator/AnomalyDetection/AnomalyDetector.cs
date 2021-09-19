using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace FlightSimulator
{
    class AnomalyDetector : IDisposable
    {
        const string dll_path = @"AnomalyDetectionLib.dll";

        //[DllImport(dll_path, EntryPoint = "learn")]
        //static extern void learn(IntPtr detector, IntPtr names, int size, IntPtr sw);

        //[DllImport(dll_path, EntryPoint = "detect")]
        //static extern IntPtr detect(IntPtr detector, IntPtr names, int size, IntPtr sw);

        //[DllImport(dll_path, EntryPoint = "createSimpleAnomalyDetectorInstance")]
        //static extern IntPtr createSimpleAnomalyDetectorInstance();

        /*[DllImport(dll_path, EntryPoint = "dispose")]
        static extern int dispose(IntPtr v);*/

       /* [DllImport(dll_path, EntryPoint = "getTimeStep")]
        static extern int getTimeStep(IntPtr v, int index);*/

        //[DllImport(dll_path, EntryPoint = "getDesciption")]
        //static extern IntPtr getDiscription(IntPtr v, int index);

        //[DllImport(dll_path, EntryPoint = "createString")]
        //static extern IntPtr createString(int len);

        /*[DllImport(dll_path, EntryPoint = "addCharToString")]
        static extern void addCharToString(IntPtr sw, char c);*/

      /*  [DllImport(dll_path, EntryPoint = "getChar")]
        static extern char getChar(IntPtr sw, int index);

        [DllImport(dll_path, EntryPoint = "len")]
        static extern int len(IntPtr sw);*/

        /*[DllImport(dll_path, EntryPoint = "getAnomalyCount")]
        static extern int getAnomalyCount(IntPtr vw);*/

      /*  [DllImport(dll_path, EntryPoint = "getFunc")]
        static extern IntPtr getFunc(IntPtr vw, int index);*/

        IntPtr detector;
        IntPtr AnomalyReportVector;
        IntPtr pDll;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr createSimpleAnomalyDetectorInstance();
        public AnomalyDetector()
        {
            pDll = DllLoader.LoadLibrary(dll_path);
            if (pDll == IntPtr.Zero)
            {
                Trace.WriteLine("Error loading dll");
            }

            IntPtr pAddressOfFunctionToCall = DllLoader.GetProcAddress(pDll, "createSimpleAnomalyDetectorInstance");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Trace.WriteLine("Error loading function");
            }
            createSimpleAnomalyDetectorInstance createSimpleAnomalyDetectorInstance =
                (createSimpleAnomalyDetectorInstance)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall,typeof(createSimpleAnomalyDetectorInstance));
            this.detector = createSimpleAnomalyDetectorInstance();
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr createString(int len);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void addCharToString(IntPtr sw, char c);

        void AddCharToWrapper(IntPtr sw,char c)
        {
            IntPtr pAddressOfFunctionToCall = DllLoader.GetProcAddress(pDll, "addCharToString");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Trace.WriteLine("Error loading function: addCharToString");
            }

            addCharToString addCharToString =
               (addCharToString)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(addCharToString));
            addCharToString(sw, c);
        }

        public IntPtr sw_string(string s)
        {
            IntPtr pAddressOfFunctionToCall = DllLoader.GetProcAddress(pDll, "createString");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Trace.WriteLine("Error loading function: createString");
            }

            createString createString =
               (createString)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(createString));

            IntPtr STR = createString(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                AddCharToWrapper(STR, s[i]);
            }
            return STR;
        }


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void learn(IntPtr detector, IntPtr names, int size, IntPtr sw);
        public void LearnNormal(string names, string filename)
        {

            IntPtr pAddressOfFunctionToCall = DllLoader.GetProcAddress(pDll, "learn");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Trace.WriteLine("Error loading function: learn");
            }
            learn learn =
            (learn)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(learn));

            IntPtr sw_filename = sw_string(filename);
            IntPtr sw_names = sw_string(names);
            learn(detector, sw_names, names.Length, sw_filename);
            DisposeDLLObject(sw_filename);
            DisposeDLLObject(sw_names);
        }


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr detect(IntPtr detector, IntPtr names, int size, IntPtr sw);
        public void Detect(string names, string filename)
        {
            IntPtr pAddressOfFunctionToCall = DllLoader.GetProcAddress(pDll, "detect");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Trace.WriteLine("Error loading function: detect");
            }
            detect detect =
            (detect)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(detect));

            IntPtr sw_filename = sw_string(filename);
            IntPtr sw_names = sw_string(names);
            AnomalyReportVector = detect(this.detector, sw_names, names.Length, sw_filename);
            DisposeDLLObject(sw_filename);
            DisposeDLLObject(sw_names);
        }


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr getDesciption(IntPtr v, int index);
        public string GetDiscription(int index)
        {

            IntPtr pAddressOfFunctionToCall = DllLoader.GetProcAddress(pDll, "getDesciption");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Trace.WriteLine("Error loading function: getDiscription");
            }
            getDesciption getDesciption =
            (getDesciption)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(getDesciption));

            IntPtr sw = getDesciption(AnomalyReportVector, index);
            string s = "";
            for (int i = 0; i < GetWrapperLen(sw); i++)
            {
                s += GetCharFromWrapper(sw, i);
            }
            DisposeDLLObject(sw);
            return s;
        }


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int getTimeStep(IntPtr v, int index);
        public int GetTimeStep(int index)
        {

            IntPtr pAddressOfFunctionToCall = DllLoader.GetProcAddress(pDll, "getTimeStep");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Trace.WriteLine("Error loading function: getTimeStep");
            }
            getTimeStep getTimeStep =
            (getTimeStep)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(getTimeStep));

            return getTimeStep(AnomalyReportVector, index);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int getAnomalyCount(IntPtr vw);
        public int AnomalyCount()
        {
            IntPtr pAddressOfFunctionToCall = DllLoader.GetProcAddress(pDll, "getAnomalyCount");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Trace.WriteLine("Error loading function: getAnomalyCount");
            }
            getAnomalyCount getAnomalyCount =
            (getAnomalyCount)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(getAnomalyCount));

            return getAnomalyCount(AnomalyReportVector);
        }


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int len(IntPtr sw);
        int GetWrapperLen(IntPtr sw)
        {
            IntPtr pAddressOfFunctionToCall = DllLoader.GetProcAddress(pDll, "len");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Trace.WriteLine("Error loading function: len");
            }
            len len =
            (len)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(len));
            return len(sw);
        }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate char getChar(IntPtr sw, int index);


        char GetCharFromWrapper(IntPtr sw, int index)
        {
            IntPtr pAddressOfFunctionToCall = DllLoader.GetProcAddress(pDll, "getChar");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Trace.WriteLine("Error loading function: getChar");
            }
            getChar getChar =
            (getChar)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(getChar));
            return getChar(sw, index);

        }

        string WrapperToString(IntPtr sw, int length) { 
            string s = "";
            for (int i = 0; i < GetWrapperLen(sw); i++)
            {
                s += GetCharFromWrapper(sw, i);
            }
            return s;
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate IntPtr getFunc(IntPtr vw, int index);
        
        public string GetFunction(int index)
        {
            IntPtr pAddressOfFunctionToCall = DllLoader.GetProcAddress(pDll, "getFunc");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Trace.WriteLine("Error loading function: getFunc");
            }
            getFunc getFunc =
            (getFunc)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(getFunc));

            IntPtr sw = getFunc(AnomalyReportVector, index);
            string s = "";
            for (int i = 0; i < GetWrapperLen(sw); i++)
            {
                s += GetCharFromWrapper(sw, i);
            }
            DisposeDLLObject(sw);
            return s;
        }

        public void UnloadDlls()
        {
            DllLoader.FreeLibrary(pDll);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int dispose(IntPtr v);
        int DisposeDLLObject(IntPtr v)
        {
            IntPtr pAddressOfFunctionToCall = DllLoader.GetProcAddress(pDll, "dispose");
            if (pAddressOfFunctionToCall == IntPtr.Zero)
            {
                Trace.WriteLine("Error loading function: dispose");
            }
            dispose dispose =
            (dispose)Marshal.GetDelegateForFunctionPointer(pAddressOfFunctionToCall, typeof(dispose));
            return dispose(v);

        }
        public void Dispose()
        {
            UnloadDlls();
        }

        ~AnomalyDetector()
        {
           /* dispose(detector);
            dispose(AnomalyReportVector);*/
        }
    }
}