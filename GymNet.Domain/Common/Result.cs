using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymNet.Domain.Common;

public readonly struct Result
{
    public bool IsSuccess { get; }
    public string? Error { get; }
    private Result(bool ok, string? error) { IsSuccess = ok; Error = error; }
    public static Result Success() => new(true, null);
    public static Result Failure(string error) => new(false, error);
}

public readonly struct Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public string? Error { get; }
    private Result(bool ok, T? value, string? error) { IsSuccess = ok; Value = value; Error = error; }
    public static Result<T> Success(T value) => new(true, value, null);
    public static Result<T> Failure(string error) => new(false, default, error);
}

