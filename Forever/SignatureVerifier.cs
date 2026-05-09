using System.Runtime.InteropServices;

namespace Forever
{
    public static class SignatureVerifier
    {
        public enum SignatureCheckResult
        {
            ValidSigned,
            NoSignature,
            InvalidSignature,
            OtherError
        }

        private const string WINTRUST_ACTION_GENERIC_VERIFY_V2_STRING =
            "{00AAC56B-CD44-11D0-8CC2-00C04FC295EE}";

        [DllImport("wintrust.dll", CharSet = CharSet.Unicode, SetLastError = false)]
        private static extern int WinVerifyTrust(IntPtr hwnd, ref Guid pgActionID, IntPtr pWVTData);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct WINTRUST_FILE_INFO
        {
            public uint cbStruct;
            public string pcwszFilePath;
            public IntPtr hFile;
            public IntPtr pgKnownSubject;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct WINTRUST_DATA
        {
            public uint cbStruct;
            public IntPtr pPolicyCallbackData;
            public IntPtr pSIPClientData;
            public uint dwUIChoice;
            public uint fdwRevocationChecks;
            public uint dwUnionChoice;
            public IntPtr pFile;
            public uint dwStateAction;
            public IntPtr hWVTStateData;
            public string pwszURLReference;
            public uint dwProvFlags;
            public uint dwUIContext;
            public IntPtr pSignatureSettings;
        }

        public static SignatureCheckResult VerifySignature(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                return SignatureCheckResult.NoSignature;

            Guid actionGuid = new Guid(WINTRUST_ACTION_GENERIC_VERIFY_V2_STRING);

            var fileInfo = new WINTRUST_FILE_INFO
            {
                cbStruct = (uint)Marshal.SizeOf(typeof(WINTRUST_FILE_INFO)),
                pcwszFilePath = filePath,
                hFile = IntPtr.Zero,
                pgKnownSubject = IntPtr.Zero
            };

            var data = new WINTRUST_DATA
            {
                cbStruct = (uint)Marshal.SizeOf(typeof(WINTRUST_DATA)),
                dwUIChoice = 2,
                fdwRevocationChecks = 0,
                dwUnionChoice = 1,
                dwStateAction = 0,
                dwProvFlags = 0x1000 | 0x800,      // Cache only + Lifetime signing
                dwUIContext = 0
            };

            IntPtr pFileInfo = IntPtr.Zero;
            IntPtr pData = IntPtr.Zero;

            try
            {
                pFileInfo = Marshal.AllocHGlobal(Marshal.SizeOf(fileInfo));
                Marshal.StructureToPtr(fileInfo, pFileInfo, false);
                data.pFile = pFileInfo;

                pData = Marshal.AllocHGlobal(Marshal.SizeOf(data));
                Marshal.StructureToPtr(data, pData, false);

                int result = WinVerifyTrust(new IntPtr(-1), ref actionGuid, pData);

                switch (result)
                {
                    case 0:
                        return SignatureCheckResult.ValidSigned;

                    case unchecked((int)0x800B0100):
                        return SignatureCheckResult.NoSignature;

                    case unchecked((int)0x800B0101):                // CERT_E_EXPIRED
                    case unchecked((int)0x800B0102):                // CERT_E_VALIDITYPERIODNESTING
                        return SignatureCheckResult.ValidSigned;    // Accept expired certs

                    default:
                        return SignatureCheckResult.InvalidSignature;
                }
            }
            catch (Exception)
            {
                return SignatureCheckResult.OtherError;
            }
            finally
            {
                if (pFileInfo != IntPtr.Zero)
                {
                    Marshal.DestroyStructure<WINTRUST_FILE_INFO>(pFileInfo);
                    Marshal.FreeHGlobal(pFileInfo);
                }
                if (pData != IntPtr.Zero)
                {
                    Marshal.DestroyStructure<WINTRUST_DATA>(pData);
                    Marshal.FreeHGlobal(pData);
                }
            }
        }
    }
}