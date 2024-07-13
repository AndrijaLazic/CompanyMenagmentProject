When you update database structure u have to use this command in PMConsole to generate new models:

- Scaffold-DbContext "Server=DESKTOP-6F991P0;Database=CompanyMenagmentProject;Trusted_Connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
