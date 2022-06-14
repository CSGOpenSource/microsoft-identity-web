﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Microsoft.Identity.Web
{
    /// <summary>
    /// Interface used to call a downstream REST API, for instance from controllers.
    /// </summary>
    public interface IDownstreamRestApi
    {
        /// <summary>
        /// Calls the downstream web API for the user, based on a description of the
        /// downstream web API in the configuration.
        /// </summary>
        /// <param name="serviceName">Name of the service describing the downstream web API. There can
        /// be several configuration named sections mapped to a <see cref="DownstreamRestApiOptions"/>,
        /// each for one downstream web API. You can pass-in null, but in that case <paramref name="calledDownstreamRestApiOptionsOverride"/>
        /// needs to be set.</param>
        /// <param name="calledDownstreamRestApiOptionsOverride">Overrides the options proposed in the configuration described
        /// by <paramref name="serviceName"/>.</param>
        /// <param name="user">[Optional] Claims representing a user. This is useful on platforms like Blazor
        /// or Azure Signal R, where the HttpContext is not available. In other platforms, the library
        /// will find the user from the HttpContext.</param>
        /// <param name="content">HTTP context in the case where <see cref="DownstreamRestApiOptions.HttpMethod"/> is
        /// <code>HttpMethod.Patch</code>, <see cref="HttpMethod.Post"/>, <see cref="HttpMethod.Put"/>.</param>
        /// <returns>An <see cref="HttpResponseMessage"/> that the application will process.</returns>
        Task<HttpResponseMessage> CallRestApiForUserAsync(
            string serviceName,
            Action<DownstreamRestApiOptions>? calledDownstreamRestApiOptionsOverride = null,
            ClaimsPrincipal? user = null,
            StringContent? content = null);

        /// <summary>
        /// Calls the downstream web API for the app, with the required scopes.
        /// </summary>
        /// <param name="serviceName">Name of the service describing the downstream web API. There can
        /// be several configuration named sections mapped to a <see cref="DownstreamRestApiOptions"/>,
        /// each for one downstream web API. You can pass-in null, but in that case <paramref name="DownstreamRestApiOptionsOverride"/>
        /// needs to be set.</param>
        /// <param name="DownstreamRestApiOptionsOverride">Overrides the options proposed in the configuration described
        /// by <paramref name="serviceName"/>.</param>
        /// <param name="content">HTTP content in the case where <see cref="DownstreamRestApiOptions.HttpMethod"/> is
        /// /// <code>HttpMethod.Patch</code>, <see cref="HttpMethod.Post"/>, <see cref="HttpMethod.Put"/>.</param>
        /// <returns>An <see cref="HttpResponseMessage"/> that the application will process.</returns>
        Task<HttpResponseMessage> CallWebApiForAppAsync(
            string serviceName,
            Action<DownstreamRestApiOptions>? DownstreamRestApiOptionsOverride = null,
            StringContent? content = null);

        /// <summary>
        /// Get a strongly typed response from the web API.
        /// </summary>
        /// <typeparam name="TOutput">Output type.</typeparam>
        /// <param name="serviceName">Name of the service describing the downstream web API. There can
        /// be several configuration named sections mapped to a <see cref="DownstreamRestApiOptions"/>,
        /// each for one downstream web API. You can pass-in null, but in that case <paramref name="DownstreamRestApiOptionsOverride"/>
        /// needs to be set.</param>
        /// <param name="DownstreamRestApiOptionsOverride">Overrides the options proposed in the configuration described
        /// by <paramref name="serviceName"/>.</param>
        /// <param name="user">[Optional] Claims representing a user. This is useful in platforms like Blazor
        /// or Azure Signal R, where the HttpContext is not available. In other platforms, the library
        /// will find the user from the HttpContext.</param>
        /// <returns>A strongly typed response from the web API.</returns>
        Task<TOutput?> GetForUserAsync<TOutput>(
            string serviceName,
            Action<DownstreamRestApiOptions>? DownstreamRestApiOptionsOverride = null,
            ClaimsPrincipal? user = null)
            where TOutput : class
        ;

        /// <summary>
        /// Calls the web API with an HttpPost, providing strongly typed input and getting
        /// strongly typed output.
        /// </summary>
        /// <typeparam name="TOutput">Output type.</typeparam>
        /// <typeparam name="TInput">Input type.</typeparam>
        /// <param name="serviceName">Name of the service describing the downstream web API. There can
        /// be several configuration named sections mapped to a <see cref="DownstreamRestApiOptions"/>,
        /// each for one downstream web API. You can pass-in null, but in that case <paramref name="DownstreamRestApiOptionsOverride"/>
        /// needs to be set.</param>
        /// <param name="relativePath">Path to the API endpoint relative to the base URL specified in the configuration.</param>
        /// <param name="inputData">Input data sent to the API.</param>
        /// <param name="DownstreamRestApiOptionsOverride">Overrides the options proposed in the configuration described
        /// by <paramref name="serviceName"/>.</param>
        /// <param name="user">[Optional] Claims representing a user. This is useful in platforms like Blazor
        /// or Azure Signal R, where the HttpContext is not available. In other platforms, the library
        /// will find the user from the HttpContext.</param>
        /// <returns>A strongly typed response from the web API.</returns>
        Task<TOutput?> PostForUserAsync<TOutput, TInput>(
            string serviceName,
            string relativePath,
            TInput inputData,
            Action<DownstreamRestApiOptions>? DownstreamRestApiOptionsOverride = null,
            ClaimsPrincipal? user = null)
            where TOutput : class;


        /// <summary>
        /// Calls the web API endpoint with an HttpPut, providing strongly typed input data.
        /// </summary>
        /// <typeparam name="TInput">Input type.</typeparam>
        /// <param name="serviceName">Name of the service describing the downstream web API. There can
        /// be several configuration named sections mapped to a <see cref="DownstreamRestApiOptions"/>,
        /// each for one downstream web API. You can pass-in null, but in that case <paramref name="DownstreamRestApiOptionsOverride"/>
        /// needs to be set.</param>
        /// <param name="relativePath">Path to the API endpoint relative to the base URL specified in the configuration.</param>
        /// <param name="inputData">Input data sent to the API.</param>
        /// <param name="DownstreamRestApiOptionsOverride">Overrides the options proposed in the configuration described
        /// by <paramref name="serviceName"/>.</param>
        /// <param name="user">[Optional] Claims representing a user. This is useful in platforms like Blazor
        /// or Azure Signal R, where the HttpContext is not available. In other platforms, the library
        /// will find the user from the HttpContext.</param>
        /// <returns>The value returned by the downstream web API.</returns>
        Task PutForUserAsync<TInput>(
            string serviceName,
            string relativePath,
            TInput inputData,
            Action<DownstreamRestApiOptions>? DownstreamRestApiOptionsOverride = null,
            ClaimsPrincipal? user = null);


        /// <summary>
        /// Calls the web API endpoint with an HttpPut, provinding strongly typed input data
        /// and getting back strongly typed data.
        /// </summary>
        /// <typeparam name="TOutput">Output type.</typeparam>
        /// <typeparam name="TInput">Input type.</typeparam>
        /// <param name="serviceName">Name of the service describing the downstream web API. There can
        /// be several configuration named sections mapped to a <see cref="DownstreamRestApiOptions"/>,
        /// each for one downstream web API. You can pass-in null, but in that case <paramref name="DownstreamRestApiOptionsOverride"/>
        /// needs to be set.</param>
        /// <param name="relativePath">Path to the API endpoint relative to the base URL specified in the configuration.</param>
        /// <param name="inputData">Input data sent to the API.</param>
        /// <param name="DownstreamRestApiOptionsOverride">Overrides the options proposed in the configuration described
        /// by <paramref name="serviceName"/>.</param>
        /// <param name="user">[Optional] Claims representing a user. This is useful in platforms like Blazor
        /// or Azure Signal R, where the HttpContext is not available. In other platforms, the library
        /// will find the user from the HttpContext.</param>
        /// <returns>A strongly typed response from the web API.</returns>
        Task<TOutput?> PutForUserAsync<TOutput, TInput>(
            string serviceName,
            string relativePath,
            TInput inputData,
            Action<DownstreamRestApiOptions>? DownstreamRestApiOptionsOverride = null,
            ClaimsPrincipal? user = null)
            where TOutput : class;


        /// <summary>
        /// Call a web API endpoint with an HttpGet,
        /// and return strongly typed data.
        /// </summary>
        /// <typeparam name="TOutput">Output type.</typeparam>
        /// <param name="serviceName">Name of the service describing the downstream web API. There can
        /// be several configuration named sections mapped to a <see cref="DownstreamRestApiOptions"/>,
        /// each for one downstream web API. You can pass-in null, but in that case <paramref name="DownstreamRestApiOptionsOverride"/>
        /// needs to be set.</param>
        /// <param name="DownstreamRestApiOptionsOverride">Overrides the options proposed in the configuration described
        /// by <paramref name="serviceName"/>.</param>
        /// <param name="user">[Optional] Claims representing a user. This is useful in platforms like Blazor
        /// or Azure Signal R, where the HttpContext is not available. In other platforms, the library
        /// will find the user from the HttpContext.</param>
        /// <returns>The value returned by the downstream web API.</returns>
        Task<TOutput?> CallWebApiForUserAsync<TOutput>(
            string serviceName,
            Action<DownstreamRestApiOptions>? DownstreamRestApiOptionsOverride = null,
            ClaimsPrincipal? user = null)
            where TOutput : class;

        /// <summary>
        /// Call a web API with a strongly typed input, with an HttpGet.
        /// </summary>
        /// <typeparam name="TInput">Input type.</typeparam>
        /// <param name="serviceName">Name of the service describing the downstream web API. There can
        /// be several configuration named sections mapped to a <see cref="DownstreamRestApiOptions"/>,
        /// each for one downstream web API. You can pass-in null, but in that case <paramref name="DownstreamRestApiOptionsOverride"/>
        /// needs to be set.</param>
        /// <param name="inputData">Input data.</param>
        /// <param name="DownstreamRestApiOptionsOverride">Overrides the options proposed in the configuration described
        /// by <paramref name="serviceName"/>.</param>
        /// <param name="user">[Optional] Claims representing a user. This is useful in platforms like Blazor
        /// or Azure Signal R, where the HttpContext is not available. In other platforms, the library
        /// will find the user from the HttpContext.</param>
        /// <returns>The value returned by the downstream web API.</returns>
        Task GetForUserAsync<TInput>(
            string serviceName,
            TInput inputData,
            Action<DownstreamRestApiOptions>? DownstreamRestApiOptionsOverride = null,
            ClaimsPrincipal? user = null);
    }
}