using ETicaretAPI_V2.Application.Repositories.FileRepositories;
using ETicaretAPI_V2.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI_V2.Persistence.Repositories.FileRepositories
{
    public class FileWriteRepository : WriteRepository<Domain.Entities.File>, IFileWriteRepository
    {
        public FileWriteRepository(ETicaretAPI_V2DBContext context) : base(context)
        {
        }
    }
}
