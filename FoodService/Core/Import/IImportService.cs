using System.IO;

namespace TravelLine.Food.Core.Import
{
    public interface IImportService
    {
        void ImportFrom1c( Stream input );
    }
}
