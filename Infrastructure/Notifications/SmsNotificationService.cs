using Application.Notifications;
using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Infrastructure.Notifications;

public class SmsNotificationService : ISmsService
{
    private readonly string _fromPhone;
    private readonly string _countryCode;

    public SmsNotificationService(IConfiguration config)
    {
        TwilioClient.Init(
            config["Twilio:AccountSid"],
            config["Twilio:AuthToken"]
        );
        _fromPhone = config["Twilio:FromPhone"]!;
        _countryCode = config["Settings:CountryCode"] ?? "+973";
    }

    public async Task SendAsync(string toPhoneNumber, string message)
    {
        await MessageResource.CreateAsync(new CreateMessageOptions(new PhoneNumber($"{_countryCode}{toPhoneNumber}"))
        {
            From = new PhoneNumber(_fromPhone),
            Body = message
        });
    }
}
