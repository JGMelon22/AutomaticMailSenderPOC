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
    <li>After that, you either have to init and create an user-secret or manually populate the
        <code>appsettings.json</code> file to bind some data.</li>
</ul>

<h4>First approach: populating <code>appsettings.json</code></h4>
<ol type="a">
    <li>Include the following code in the configuration file:</li>
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

<h4>Second approach: using <code>dotnet user secrets</code></h4>
<ol type="a">
    <li>Inside the <code>csproj</code> directory, initialize the user secrets with:
        <code>dotnet user-secrets init</code>
    </li>
    <li>Then, one by one, type the following in the terminal:</li>
    <pre><code>dotnet user-secrets set "SmtpSettings:Server" "sandbox.smtp.mailtrap.io"
dotnet user-secrets set "SmtpSettings:Port" "2525"
dotnet user-secrets set "SmtpSettings:SenderEmail" "somemail@mail.com"
dotnet user-secrets set "SmtpSettings:Username" "Your Username"
dotnet user-secrets set "SmtpSettings:Password" "Your safe password"
dotnet user-secrets set "SmtpSettings:MailTrapUserName" "Your MailTrap Username"
dotnet user-secrets set "SmtpSettings:MailTrapPassword" "Your MailTrap Password"
</code></pre>
</ol>

<img alt="zed-editor" src="https://github.com/user-attachments/assets/ba3ef202-f9da-47a7-b08a-be697197fd31" width="1080" height="auto">
<img alt="swagger-ui" src="https://github.com/user-attachments/assets/2ccf2e5c-c37d-4494-9690-63178cfde3d3" width="720" height="auto">

<h3>References 📚</h3>
<a href="https://macoratti.net/22/06/aspn_mailkitapi1.htm">ASP.NET Core - Enviando emails com Mailkit em uma API </a><br/>
<a href="https://mimekit.net/docs/html/Introduction.htm">MailKit Documentation</a><br/>
<a href="https://github.com/jstedfast/MailKit">MailKit GitHub Project</a>
