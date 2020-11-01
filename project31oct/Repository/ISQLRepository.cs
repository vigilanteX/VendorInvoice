using project31oct.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project31oct.Repository
{
    public interface ISQLRepository
    {
        Task<DataReturn> UploadDataAsync(IList<UploadModel> uploadModels);
    }
}
