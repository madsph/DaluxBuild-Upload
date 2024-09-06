using System;

namespace DaluxFileUpload.Model
{
    public class DaluxFileWrapper
    {
        public string IdentKey { get; private set; } = "";
        public string Revision { get; private set; } = "";

        public string fileID { get; set; } = "";
        public string folderID { get; set; } = "";
        public string fileName { get; set; } = "";

        public bool deleted { get; set; } = false;
        public int fileSize { get; set; } = -1;
        public int version { get; set; } = -1;
        public DateTime dateUploaded { get; set; }

        public string Prop_Title { get; set; } = "";
        public string Prop_Number { get; set; } = "";
        public string Prop_ContentDescr { get; set; } = "";


        private static char[] _chHatSplit = new char[] { '^' };
        private static char[] _chBlankSplit = new char[] { ' ' };

        public DaluxFileWrapper()
        {

        }

        public void FormatIdentKey(DaluxProjectWrapper pw)
        {
            if (pw.DaluxRawFolderNodes.ContainsKey(folderID))
            {
                string[] tokens = fileName.Split(_chHatSplit, StringSplitOptions.RemoveEmptyEntries);
                if (tokens.Length >= 2)
                {
                    string idPartOfFileName = tokens[0].Trim();
                    string parentFolderID = pw.DaluxRawFolderNodes[folderID].parentFolderID;
                    if (parentFolderID != "")
                    {
                        if (pw.DaluxInstallationPackageFolderId2IndexLookUp.ContainsKey(parentFolderID))
                        {
                            string installFolderIndexKey = pw.DaluxInstallationPackageFolderId2IndexLookUp[parentFolderID];
                            IdentKey = string.Concat(idPartOfFileName, " ", installFolderIndexKey);
                            if (tokens.Length >= 3) // Only relevant for drawings
                            {
                                Revision = tokens[1].Trim();
                            }
                            else
                            {
                                // Shipments are all given a revision equal to the Dalux version
                                Revision = version.ToString();
                            }
                        }
                    }
                }
            }
        }

        public string GetIdentWithRevision()
        {
            return $"{IdentKey} - {Revision}";
        }
    }
}