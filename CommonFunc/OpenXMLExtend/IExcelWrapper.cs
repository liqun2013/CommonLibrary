using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.OpenXMLExtend
{
	public interface IExcelWrapper
	{
		void GenerateXlsxFile(List<BaseSheetData> exlsSheetData, string[] shNames, string filename, bool doMerge = false, bool customFormat = false);
		MemoryStream GenerateXlsxFile(List<BaseSheetData> exlsSheetData, string[] shNames, bool doMerge = false, bool customFormat = false);
		ExlsSheetData ReadSpreadSheetDoc(string filename, int sheetIndex, int startRowIndex);
		ExlsSheetData ReadSpreadSheetDoc(Stream stream, int sheetIndex, int startRowIndex);
	}
}
