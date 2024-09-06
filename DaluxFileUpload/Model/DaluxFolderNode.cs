using System;

namespace DaluxFileUpload.Model
{
    public class DaluxFolderNode
    {
        public string DrwgFolderID { get; set; } = "";
        public string ShipmentFolderID { get; set; } = "";

        public string DaluxFolderNumberPart { get; set; } = "";
        public string folderID { get; set; }
        public string folderName { get; set; }
        public string parentFolderID { get; set; }

        public bool IsDrawingFolder { get; private set; } = false;
        public bool IsShipmentFolder { get; private set; } = false;

        private static char[] _chBlankSplit = new char[] { ' ' };

        public DaluxFolderNode()
        {

        }

        public void ParseDaluxFolderName()
        {
            string[] tokens = folderName.Split(_chBlankSplit, StringSplitOptions.RemoveEmptyEntries);
            string folderNumberPartPeek = tokens[0];
            //if (CmnUtil.IsNatural(folderNumberPartPeek))
            //{
            //    DaluxFolderNumberPart = folderNumberPartPeek;
            //    string upperCompacted = folderName.RemoveSpaces().ToUpper();
            //    if (upperCompacted.Contains("1DBDRWG"))
            //    {
            //        IsDrawingFolder = true;
            //    }
            //    else if (upperCompacted.Contains("2SHIPMENT"))
            //    {
            //        IsShipmentFolder = true;
            //    }
            //}

        }
    }
}