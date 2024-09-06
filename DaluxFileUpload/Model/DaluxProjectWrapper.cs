using System.Collections.Generic;

namespace DaluxFileUpload.Model
{
    public class DaluxProjectWrapper
    {
        public int ProjNo { get; private set; }
        public string DaluxProjectID { get; set; } = "";
        public string DaluxFileAreaID { get; set; } = "";
        public string DaluxFileAreaName { get; set; } = "";
        public string DaluxFileRootFolderID { get; set; } = "";
        public string CurrentFileUploadGuid { get; set; } = "";

        public string DocCachePath { get; private set; }
        public string InstallationManualRoot { get; private set; }
        public const string NoPackingListUploadedFileFullPath = @"\\AET110\AETLIB\AET_Programmer\Brugerflade database\Templates\No packing list uploaded.pdf";

        public Dictionary<string, DbFileWrapper> DbFileWrappers { get; private set; }
        public Dictionary<string, SortedDictionary<string, DaluxFileWrapper>> DaluxFileWrappers { get; private set; } = new Dictionary<string, SortedDictionary<string, DaluxFileWrapper>>();
        public Dictionary<string, DaluxFolderNode> DaluxRawFolderNodes { get; private set; } = new Dictionary<string, DaluxFolderNode>();
        public Dictionary<string, DaluxFolderNode> DaluxInstallationPackageFolderNodes { get; private set; } = new Dictionary<string, DaluxFolderNode>();
        public Dictionary<string, string> DaluxInstallationPackageFolderId2IndexLookUp { get; private set; } = new Dictionary<string, string>();

        private static System.Globalization.CultureInfo _ciUS = new System.Globalization.CultureInfo("en-US");
        private static char[] _chHatSplit = new char[] { '^' };
        private static char[] _chBlankSplit = new char[] { ' ' };

        public DaluxProjectWrapper(int projNo, string docCachePath, string installationManualRoot)
        {
            ProjNo = projNo;
            DocCachePath = docCachePath;
            InstallationManualRoot = installationManualRoot;            
        }

    }
}