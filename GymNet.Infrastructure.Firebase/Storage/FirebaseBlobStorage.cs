using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GymNet.Application.Abstractions.Services;

namespace GymNet.Infrastructure.Firebase.Storage;

// Stub temporal 
public sealed class FirebaseBlobStorage : IBlobStorage
{
    public Task<string> UploadAsync(string path, Stream content, string contentType, CancellationToken ct)
        => Task.FromResult($"https://example/{Uri.EscapeDataString(path)}");
}

