using Blazored.LocalStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CTA.BlazorWasm.Shared;
using CTA.BlazorWasm.Shared.Models;
using CTA.BlazorWasm.Client;
using CTA.BlazorWasm.Shared.Models.UserAccount;

namespace CTA.BlazorWasm.Client.Services
{
    // Press Ctrl-R, Ctrl-I to Extract an Interface
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<RegisterResult> Register(RegisterModel registerModel)
        {
            var response = await _httpClient.PostAsJsonAsync("api/accounts", registerModel);
            return await response.Content.ReadFromJsonAsync<RegisterResult>();
        }
        public async Task<PasswordResetConfirmation> ResetPassword(PasswordResetRequest resetRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("api/accounts/reset-password", resetRequest);
            return await response.Content.ReadFromJsonAsync<PasswordResetConfirmation>();
        }
        public async Task<PasswordChangeConfirmation> ChangePassword(PasswordChangeRequest changeRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("api/accounts/change-password", changeRequest);
            return await response.Content.ReadFromJsonAsync<PasswordChangeConfirmation>();
        }
        public async Task<PasswordChangeConfirmation> SetPassword(PasswordSetRequest passwordSetRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("api/accounts/set-password", passwordSetRequest);
            return await response.Content.ReadFromJsonAsync<PasswordChangeConfirmation>();
        }
        public async Task<EmailConfirmationResponse> ConfirmEmail(EmailConfirmationRequest confirmEmailRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("api/accounts/confirm-email", confirmEmailRequest);
            return await response.Content.ReadFromJsonAsync<EmailConfirmationResponse>();
        }
        public async Task<EmailConfirmationResponse> ResendEmail(ResendEmailRequest emailRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("api/accounts/resend-confirmation", emailRequest);
            return await response.Content.ReadFromJsonAsync<EmailConfirmationResponse>();
        }

        public async Task<LoginResult> Login(LoginModel loginModel)
        {
            var loginAsJson = JsonSerializer.Serialize(loginModel);
            var response = await _httpClient.PostAsync("api/Login", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
            var loginResult = JsonSerializer.Deserialize<LoginResult>(await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!response.IsSuccessStatusCode)
            {
                return loginResult;
            }

            if(loginResult is not null)
            {
                await _localStorage.SetItemAsync("authToken", loginResult.Token);
                ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginResult.Token);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);
            }

            return loginResult;
        }
        
        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider)
                .MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}