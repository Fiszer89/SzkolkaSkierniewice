using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using OfficeOpenXml;

namespace SzkolkaSkierniewice.Infrastructure.Logic
{
    public class CreateExcelFile<T>
    {
        public static void ExportToExcel(IEnumerable<T> employees, DirectoryInfo outputDir, string fileName, string sheetName)
        {
            fileName = FixFileNameExcel(fileName);

            FileInfo f = new FileInfo(outputDir.FullName + @"\" + fileName);
            DeleteFileIfExist(f);
            //foreach (var sheetName in sheetNames)
            {
                using (var excelFile = new ExcelPackage(f))
                {
                    var worksheet = excelFile.Workbook.Worksheets.Add(sheetName);
                    worksheet.Cells["A1"].LoadFromCollection(Collection: employees, PrintHeaders: true);
                    worksheet.Cells.AutoFitColumns(0);
                    excelFile.Save();
                }
            }
        }

        public static FileInfo DeleteFileIfExist(FileInfo newFile)
        {
            if (newFile.Exists)
            {
                newFile.Delete();  // ensures we create a new workbook
                newFile = new FileInfo(newFile.FullName);
            }
            return newFile;
        }

        public static string FixFileNameExcel(string fileName)
        {
            if (Path.HasExtension(fileName))
            {
                string ext = Path.GetExtension(fileName);

                if (ext != ".xlsx")
                {
                    fileName = Path.GetFileNameWithoutExtension(fileName) + ".xlsx";
                }
            }
            else
            {
                fileName += ".xlsx";
            }

            return fileName;
        }
    }
}