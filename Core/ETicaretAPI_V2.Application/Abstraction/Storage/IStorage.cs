using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI_V2.Application.Abstraction.Storage
{
    public interface IStorage
    {
        Task<List<(string fileName, string pathOrContainer)>> UploadAsync(IFormFileCollection files, string pathOrContainer);
        Task DeleteAsync(string pathOrContainer, string fileName);
        List<string>GetFiles(string pathOrContainer);
        bool HasFile(string pathOrContainer,string fileName);
    }
}
