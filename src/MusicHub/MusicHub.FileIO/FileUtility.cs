using Microsoft.Extensions.Options;
using MusicHub.FileIO.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHub.FileIO
{
    public class FileUtility
    {

        private readonly IOptions<FileIOOptions> _options;
        public FileUtility(IOptions<FileIOOptions> options)
        {
            _options = options;
        }


        public string SaveFile(FileStream fs, string fileExtension)
        {

            


            throw new NotImplementedException();
        }
    }
}
