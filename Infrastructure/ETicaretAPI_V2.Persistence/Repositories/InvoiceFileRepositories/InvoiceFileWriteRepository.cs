﻿using ETicaretAPI_V2.Application.Repositories.InvoiceFileRepositories;
using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI_V2.Persistence.Repositories.InvoiceFileRepositories
{
    public class InvoiceFileWriteRepository : WriteRepository<InvoiceFile>, IInvoiceFileWriteRepository
    {
        public InvoiceFileWriteRepository(ETicaretAPI_V2DBContext context) : base(context)
        {
        }
    }
}
