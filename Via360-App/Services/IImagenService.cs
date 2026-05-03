using System;
using System.Collections.Generic;
using System.Text;

namespace Via360.App.Services
{
    public interface IImagenService
    {
        Task<string> SubirImagenAsync(FileResult foto);
    }
}
