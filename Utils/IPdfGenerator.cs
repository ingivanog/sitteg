using System.Collections.Generic;
using System.IO;

namespace GuanajuatoAdminUsuarios.Utils
{
    public interface IPdfGenerator<T> where T : class
    {
        (MemoryStream, string) CreatePdf(List<T> ModelData, string NamePdf, int SizeColumns, string title, string[] ColumnsNames);
    }
}
