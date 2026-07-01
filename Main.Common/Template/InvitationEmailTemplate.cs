namespace Main.Common;

public static class InvitationEmailTemplate
{
    public static string BuildInvitationEmail (string recipientEmail,string inviterName,string tenantName,string acceptUrl)
    {
        return $@"
<!DOCTYPE html>
<html lang='en'>
<head>
  <meta charset='UTF-8' />
  <meta name='viewport' content='width=device-width, initial-scale=1.0' />
  <title>You're invited</title>
</head>
<body style='margin:0; padding:0; background-color:#f5f7fb; font-family:Arial, sans-serif;'>
  <table role='presentation' width='100%' cellspacing='0' cellpadding='0' border='0' style='background-color:#f5f7fb; padding:24px;'>
    <tr>
      <td align='center'>
        <table role='presentation' width='600' cellspacing='0' cellpadding='0' border='0' style='background-color:#ffffff; border-radius:8px; overflow:hidden;'>
          <tr>
            <td style='background-color:#0f6cbd; padding:24px; color:#ffffff; text-align:center;'>
              <h2 style='margin:0;'>You’re invited to join {tenantName}</h2>
            </td>
          </tr>
          <tr>
            <td style='padding:32px;'>
              <p style='font-size:16px; color:#333333;'>Hello {recipientEmail},</p>
              <p style='font-size:16px; color:#333333;'>{inviterName} has invited you to join <strong>{tenantName}</strong>.</p>
              <p style='font-size:16px; color:#333333;'>Please accept the invitation below to create or access your account and join the tenant.</p>
              <p style='margin:32px 0; text-align:center;'>
                <a href='{acceptUrl}' style='background-color:#0f6cbd; color:#ffffff; text-decoration:none; padding:12px 24px; border-radius:4px; display:inline-block;'>Accept Invitation</a>
              </p>
              <p style='font-size:13px; color:#777777;'>If the button does not work, copy and open this link in your browser:</p>
              <p style='font-size:13px; color:#777777; word-break:break-all;'>{acceptUrl}</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
</body>
</html>";
    }
}
