# Secure Logger & Swagger Middleware Package (.NET 6)

## Overview

This package provides robust logging, secure handling of Swagger requests and responses, and implements a highly secure encryption cycle. Designed for .NET 6 applications, it ensures that sensitive data is protected throughout the request/response lifecycle.

## Features

- **Logger Integration**: Centralized and configurable logging for all application events.
- **Swagger Middleware**: Securely logs and manages Swagger API requests and responses.
- **High-Security Encryption Cycle**: Utilizes industry-standard encryption algorithms to protect data in transit and at rest.

## Getting Started

1. **Install the Package**

2. **Configure Logging**
- Add logger configuration to your `appsettings.json` or via code.

3. **Enable Swagger Middleware**
- Register the middleware in your `Startup.cs` or `Program.cs`:app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<SecureSwaggerLoggerMiddleware>();
4. **Encryption Setup**
- Configure encryption keys and algorithms in your settings.

## Security

- All request and response data handled by Swagger is encrypted using a highly secure cycle.
- Logging is performed with strict adherence to security best practices, ensuring no sensitive information is exposed.

## License

MIT

## Support

For issues or feature requests, please open an issue on the repository.