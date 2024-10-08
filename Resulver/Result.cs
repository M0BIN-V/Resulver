﻿namespace Resulver;

public class Result
{
    public bool IsSuccess => Errors.Count == 0;
    public bool IsFailure => Errors.Count > 0;

    public List<ResultError> Errors { get; init; } = [];

    public string? Message { get; init; }

    public Result(params ResultError[] errors)
    {
        Errors.AddRange(errors);
    }

    public Result(string? message = null)
    {
        Message = message;
    }

    public static implicit operator Result(ResultError error) => new(error);
    public static implicit operator Task<Result>(Result result) => Task.FromResult(result);
}

public class Result<TContent> : Result
{
    public TContent? Content { get; set; }
    public Result(string? message = null)
    {
        Message = message;
    }

    public Result(params ResultError[] errors)
    {
        Errors.AddRange(errors);
    }

    public Result(TContent content, string? message = null)
    {
        Content = content;
        Message = message;
    }

    public static implicit operator Result<TContent>(ResultError error) => new(error);

    public static implicit operator Task<Result<TContent>>(Result<TContent> result) => Task.FromResult(result);

    public static implicit operator Result<TContent>(TContent content) => new(content);

    public static implicit operator TContent?(Result<TContent> result) => result.Content;
}