using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;

namespace TravelLine.Food.Site.Helpers
{
    public static class ExcelHelper
    {
        public static MemoryStream CreateExcel( List<object[]> data )
        {
            var stream = new MemoryStream();
            using ( ExcelPackage package = new ExcelPackage( stream ) )
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add( "List1" );

                worksheet.Cells[ "A1" ].LoadFromArrays( data );

                var columns = data.Max( x => x.Length );
                for ( var i = 1; i <= columns; i++ ) 
                {
                    worksheet.Column( i ).AutoFit();
                }

                package.Save();

                stream.Seek( 0, SeekOrigin.Begin );

                return stream;
            }
        }
    }
}
