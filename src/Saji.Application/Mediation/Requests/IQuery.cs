﻿using MediatR;
using Saji.Application.Mediation.Responses;

namespace Saji.Application.Mediation;

/// <summary>
/// Query Interface
/// </summary>
/// <typeparam name="T">
/// Query response type
/// </typeparam>
public interface IQuery<T> : IApplicationRequest, IRequest<QueryResult<T>>
    where T : class
{
}
