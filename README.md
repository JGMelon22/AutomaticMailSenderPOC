# AutomaticMailSenderPOC
<span>This is a simple ASP .NET Core Web API to send E-mails using MailKit and Mailtrap free service.</span> <br />
<span>The future plan is to send e-mails in batches to have success contact with my college course Coordinator, as he
    never responds.</span>

<h3>Tech Stack</h3>
<div style="display: flex; gap: 10px;">
    <img height="32" width="32" src="https://cdn.simpleicons.org/dotnet" alt="dotnet" />&nbsp;
    <img height="32" width="32" src="https://cdn.simpleicons.org/swagger" alt="swagger" />&nbsp;
    <img height="32" width="32" src="https://cdn.simpleicons.org/zedindustries" alt="zedindustries" />&nbsp;
</div>

<h3>How to build and execute it? 🛠️</h3>
<span><strong>First of all, make sure you have .NET 8 SDK installed and a <a href="https://mailtrap.io/">Mailtrap account</a></strong></span>
<ul>
    <li>Clone the repository</li>
    <li>Inside of the sln folder, restore the dependencies with <code>dotnet restore</code></li>
    <li>Change the directory to AutomaticMailSenderPOC with <code>cd AutomaticMailSenderPOC</code> and execute the
        publisher project with <code>dotnet run</code></li>
    <li> After that, you either have to init and create an user-secret or manually populate the
        <code>appsettings.json</code> file to bind some data.</li>
    <ol type="a">
        <li>First approach: populating <code>appsettings.json</code></li>
        <ol>
            <li>Include the following code at the configuration file: </li>
            <pre><code>"SmtpSettings": {
   "Server": "sandbox.smtp.mailtrap.io",
   "Port": 2525,
   "SenderEmail": "somemail@mail.com",
   "Username": "Your Username",
   "Password": "Your safe password",
   "MailTrapUserName": "Your Mailtrap Username",
   "MailTrapPassword": "Your Mailtrap Password"
}</code></pre>
        </ol>
        <ol type="a" start = "2">
            <li>Second approach: using <code>dotnet user secrets</code></li>
            <ol>
                <li>Inside the <code>csproj</code>directory, initialize the user secrets with:
                    <code>dotnet user-secrets init</code></li>
                <li>Them, one by one, type at the openned terminal emulator: </li>
                <pre><code>dotnet user-secrets set "SmtpSettings:Server" "sandbox.smtp.mailtrap.io"
dotnet user-secrets set "SmtpSettings:Port" "2525"
dotnet user-secrets set "SmtpSettings:SenderEmail" "somemail@mail.com"
dotnet user-secrets set "SmtpSettings:Username" "Your Username"
dotnet user-secrets set "SmtpSettings:Password" "Your safe password"
dotnet user-secrets set "SmtpSettings:MailTrapUserName" "Your MailTrap Username"
dotnet user-secrets set "SmtpSettings:MailTrapUserName" "Your MailTrap Password"
</code></pre>
            </ol>
        </ol>
    </ol>
</ul>

<h3>References 📚</h3>
<a href="https://macoratti.net/22/06/aspn_mailkitapi1.htm">ASP.NET Core - Enviando emails com Mailkit em uma API </a><br/>
<a href="https://mimekit.net/docs/html/Introduction.htm">MailKit Documentation</a><br/>
<a href="https://github.com/jstedfast/MailKit">MailKit GitHub Project</a>
