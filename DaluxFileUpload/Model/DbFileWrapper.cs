using System;

namespace DaluxFileUpload.Model
{
    public class DbFileWrapper
    {
        public string IdentKey { get; set; } = "";
        public string ParentFolderFormattedCounter { get; set; } = "";
        public string FileName { get; set; } = "";
        public string FileFullPath { get; set; } = "";
        public DateTime DateFileCreated { get; set; }
        public string Title { get; private set; } = "";
        public string Number { get; private set; } = "";
        public string Revision { get; private set; } = "";
        public bool ToBeUploaded { get; set; } = false;
        public bool IsDrawingFile { get; set; } = true; // To be extended when Shipments are added
        public bool IsShipmentFile { get { return !IsDrawingFile; } }
        public bool OnlyUploadNewVersionOfFile { get; set; } = false;
        public bool IsPreliminaryPIDiagram { get; set; } = false;
        public string DocID { get; private set; } = "";
        public int SiteDelivID { get; private set; } = -1;
        public string ContentDescription { get; private set; } = "";

        public string DaluxFolderId { get; set; } = "";
        public string DaluxFileId { get; set; } = "";

        private static char[] _chHatSplit = new char[] { '^' };
        private static char[] _chBlankSplit = new char[] { ' ' };
    }
}