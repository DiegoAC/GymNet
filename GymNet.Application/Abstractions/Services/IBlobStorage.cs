using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymNet.Application.Abstractions.Services;
public interface IBlobStorage
{
    Task<string> UploadAsync(string path, Stream content, string contentType, CancellationToken ct);
}