using Infrastructure.Auth.Interfaces;
using Infrastructure.Auth.Models;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Auth;

public class OtpService : IOtpService
{
    private readonly ApplicationDbContext _db;
    private readonly ILogger<OtpService> _logger;
    private readonly IHostEnvironment _env;

    public OtpService(ApplicationDbContext db, ILogger<OtpService> logger, IHostEnvironment env)
    {
        _db = db;
        _logger = logger;
        _env = env;
    }

    public async Task<string> GenerateAsync(string phoneNumber)
    {
        var existing = await _db.OtpCodes
            .Where(o => o.PhoneNumber == phoneNumber && !o.IsUsed)
            .ToListAsync();

        existing.ForEach(o => o.IsUsed = true);

        var code = Random.Shared.Next(100000, 999999).ToString();

        _db.OtpCodes.Add(new OtpCode
        {
            PhoneNumber = phoneNumber,
            Code = code,
            ExpiresAt = DateTime.UtcNow.AddMinutes(10)
        });

        await _db.SaveChangesAsync();

        if (_env.IsDevelopment())
            _logger.LogInformation("[DEV] OTP for {Phone}: {Code}", phoneNumber, code);

        return code;
    }

    public async Task InvalidateAsync(string phoneNumber)
    {
        var existing = await _db.OtpCodes
            .Where(o => o.PhoneNumber == phoneNumber && !o.IsUsed)
            .ToListAsync();

        existing.ForEach(o => o.IsUsed = true);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> ValidateAsync(string phoneNumber, string code)
    {
        var otp = await _db.OtpCodes
            .FirstOrDefaultAsync(o => o.PhoneNumber == phoneNumber && o.Code == code);

        if (otp == null || !otp.IsValid) return false;

        otp.IsUsed = true;
        await _db.SaveChangesAsync();

        return true;
    }
}
