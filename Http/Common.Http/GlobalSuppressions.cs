// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Major Code Smell", "S3900:Arguments of public methods should be validated against null", Justification = "<Pending>", Scope = "type", Target = "~T:Common.Http.Extensions.HttpClientExtension")]
[assembly: SuppressMessage("Major Code Smell", "S4005:\"System.Uri\" arguments should be used instead of strings", Justification = "<Pending>", Scope = "type", Target = "~T:Common.Http.Extensions.HttpClientExtension")]
[assembly: SuppressMessage("Major Code Smell", "S103:Lines should not be too long", Justification = "<Pending>")]
[assembly: SuppressMessage("Minor Code Smell", "S4018:Generic methods should provide type parameters", Justification = "<Pending>", Scope = "member", Target = "~M:Common.Http.CachingHttpServiceClient.PostOrRetrieveFromCacheAsync``2(System.String,``0,System.String,System.String,System.Threading.CancellationToken)~System.Threading.Tasks.Task{``1}")]
[assembly: SuppressMessage("Major Code Smell", "S3994:URI Parameters should not be strings", Justification = "<Pending>", Scope = "type", Target = "~T:Common.Http.CachingHttpServiceClient")]
[assembly: SuppressMessage("Minor Code Smell", "S4018:Generic methods should provide type parameters", Justification = "<Pending>", Scope = "type", Target = "~T:Common.Http.CachingHttpServiceClient")]
[assembly: SuppressMessage("Minor Code Smell", "S4018:Generic methods should provide type parameters", Justification = "<Pending>", Scope = "type", Target = "~T:Common.Http.Interfaces.ICachingHttpServiceClient")]
[assembly: SuppressMessage("Major Code Smell", "S3925:\"ISerializable\" should be implemented correctly", Justification = "<Pending>", Scope = "type", Target = "~T:Common.Http.Exception.EmptyResponseException")]
[assembly: SuppressMessage("Info Code Smell", "S1309:Track uses of in-source issue suppressions", Justification = "<Pending>")]
[assembly: SuppressMessage("Major Code Smell", "S3900:Arguments of public methods should be validated against null", Justification = "<Pending>", Scope = "type", Target = "~T:Common.Http.Extensions.HttpResponseMessageExtension")]
[assembly: SuppressMessage("Major Code Smell", "S4005:\"System.Uri\" arguments should be used instead of strings", Justification = "<Pending>", Scope = "type", Target = "~T:Common.Http.CachingHttpServiceClient")]
[assembly: SuppressMessage("Major Code Smell", "S3996:URI properties should not be strings", Justification = "<Pending>", Scope = "member", Target = "~P:Common.Http.Interfaces.Authentication.IAuthenticationConfiguration.AuthenticationApiUrl")]
[assembly: SuppressMessage("Major Code Smell", "S3925:\"ISerializable\" should be implemented correctly", Justification = "<Pending>", Scope = "type", Target = "~T:Common.Http.Exception.ApiAuthenticationException")]
[assembly: SuppressMessage("Critical Code Smell", "S2360:Optional parameters should not be used", Justification = "<Pending>", Scope = "member", Target = "~M:Common.Http.Interfaces.Authentication.IAuthenticationContext`1.AcquireTokenAsync(System.Threading.CancellationToken)~System.Threading.Tasks.Task{`0}")]
[assembly: SuppressMessage("Major Code Smell", "S4005:\"System.Uri\" arguments should be used instead of strings", Justification = "<Pending>", Scope = "member", Target = "~M:Common.Http.Authentication.ClientCredentialAuthenticationContext`1.BuildAuthenticationMessage~System.Net.Http.HttpRequestMessage")]
[assembly: SuppressMessage("Major Code Smell", "S4005:\"System.Uri\" arguments should be used instead of strings", Justification = "<Pending>", Scope = "member", Target = "~M:Common.Http.Authentication.BasicAuthenticationContext`1.BuildAuthenticationMessage~System.Net.Http.HttpRequestMessage")]
[assembly: SuppressMessage("Major Code Smell", "S3994:URI Parameters should not be strings", Justification = "<Pending>", Scope = "member", Target = "~M:Common.Http.HttpServiceClientBase.HttpGetAndReadAsync``1(System.String,System.String,Common.Http.HttpServiceOptions,System.Threading.CancellationToken)~System.Threading.Tasks.Task{``0}")]
[assembly: SuppressMessage("Major Code Smell", "S4005:\"System.Uri\" arguments should be used instead of strings", Justification = "<Pending>", Scope = "member", Target = "~M:Common.Http.HttpServiceClientBase.HttpGetAndReadAsync``1(System.String,System.String,Common.Http.HttpServiceOptions,System.Threading.CancellationToken)~System.Threading.Tasks.Task{``0}")]
[assembly: SuppressMessage("Major Code Smell", "S3994:URI Parameters should not be strings", Justification = "<Pending>", Scope = "member", Target = "~M:Common.Http.HttpServiceClient.GetAsync``1(System.String,System.String,System.Threading.CancellationToken)~System.Threading.Tasks.Task{``0}")]
[assembly: SuppressMessage("Major Code Smell", "S3994:URI Parameters should not be strings", Justification = "<Pending>", Scope = "member", Target = "~M:Common.Http.Interfaces.IHttpServiceClient.GetAsync``1(System.String,System.String,System.Threading.CancellationToken)~System.Threading.Tasks.Task{``0}")]
