Threat intel — MVP v1.0
Product: Personal Vulnerability Intelligence Platform
Purpose: Application Security / DevSecOps portfolio project
Status: Initial MVP specification
1. Product vision
Build a secure, personal vulnerability intelligence platform that collects CVEs from the NVD API, identifies vulnerabilities listed in the CISA Known Exploited Vulnerabilities (KEV) catalogue, and helps me understand which vulnerabilities affect the software and tools I use.

Version 1 should be small enough to complete while establishing a secure, maintainable foundation for future web and mobile functionality.
2. Core features
A. Dashboard and CVE explorer
- Display CVE ID, description, CVSS severity and score, publication date, and affected products/versions where available.
- Clearly flag CVEs listed in the CISA KEV catalogue and display relevant KEV metadata.
- Support search, filtering by severity, KEV status and date, sorting, pagination, and a detailed CVE view.
- Retrieve data from official APIs.
- Support scheduled synchronisation, deduplication, error handling, and source/update timestamps.
- Make it clear when data was last successfully synchronised.
B. Login and access control
- Provide a login page and secure authentication.
- Enforce server-side authorisation for every protected API operation.
- Ensure personal inventory, settings, and alerts are accessible only to the authorised user.
- Prefer a trusted identity provider using OpenID Connect (OIDC) rather than implementing password security from scratch.
- Never rely on frontend checks alone to enforce access control.
C. Personal software inventory
- Allow me to add, edit, view, and remove tools, applications, and software I use.
- Store product name, vendor, version, and optional notes.
- Allow CVEs to be linked to inventory items, with a way to record why the vulnerability is relevant.
- Show linked vulnerabilities and highlight known-exploited vulnerabilities.
- Clearly distinguish a potential product/version match from a confirmed finding; do not claim a system is vulnerable based on a product name alone.
D. Alert system
- Create in-app alerts when new or updated CVEs match tracked software, with special attention to CVEs listed in CISA KEV.
- Prevent duplicate alerts and track read/unread status.
- Record why an alert was generated and which CVE and inventory item it relates to.
- Design notifications behind a reusable service so email and mobile push notifications can be added later.
- Keep the backend API reusable by a future mobile application.
E. Database and maintainable architecture
- Use PostgreSQL and Entity Framework Core.
- Model CVEs, affected products, personal inventory items, CVE-to-inventory links, alerts, and user-related data as appropriate entities.
- Use database migrations, relationships, constraints, and suitable indexes.
- Keep the application modular and easy to extend.
- Start with a modular monolith; avoid unnecessary microservices in the MVP.
3. Proposed technology stack
Confirm current stable versions and compatibility before implementation.
- Backend: C# / ASP.NET Core Web API
- Frontend: React + TypeScript, with responsive layouts
- Database: PostgreSQL + Entity Framework Core
- Background ingestion: .NET Worker Service or an equivalent scheduled worker
- Authentication: OpenID Connect (OIDC) with a suitable identity provider
- Version control and CI/CD: Git, GitHub, GitHub Actions
- Testing: xUnit and appropriate API/integration testing tools
- Security tooling: static analysis, dependency scanning, secret scanning, and later dynamic application security testing
- Deployment: local development first; cloud deployment over HTTPS after the MVP has been tested
4. Security requirements — essential
- Use OWASP ASVS as a security verification guide and consider relevant OWASP Top 10 and API Security Top 10 risks.
- Validate inputs on the server and use parameterised database operations.
- Protect against broken access control, injection, cross-site scripting (XSS), cross-site request forgery (CSRF) where applicable, server-side request forgery (SSRF), and API abuse.
- Use secure session/token handling and appropriate security headers.
- Store secrets outside source control; use least privilege and secure configuration.
- Treat data from external APIs as untrusted input.
- Apply sensible rate limits and safe error handling; do not expose secrets or internal details in error responses.
- Add unit and integration tests, including tests for authentication and authorisation boundaries.
- Add static analysis, dependency scanning, and secret scanning to CI/CD.
- Document the threat model, trust boundaries, security decisions, and known limitations.
- Never commit credentials, tokens, or private employer data to the repository.
5. MVP boundaries
Included in version 1
- Secure login and server-side access control.
- CVE ingestion from the NVD API.
- KEV enrichment from the CISA KEV catalogue.
- Searchable and filterable CVE dashboard.
- Personal software inventory.
- Manual linking of CVEs to inventory items, with a foundation for matching.
- Deduplicated in-app alerts.
- PostgreSQL persistence and database migrations.
- Automated tests for core features and access controls.
- Local setup instructions and basic architecture/security documentation.
Deferred until after the MVP
- AI-generated vulnerability analysis and recommendations.
- Additional threat intelligence feeds.
- Email notifications.
- Mobile push notifications.
- Native mobile application.
- Automated remediation or changes to user systems.
- Complex multi-tenant functionality or microservices.
The frontend should be responsive, and the backend API should be designed so a future mobile application can reuse the same services.
6. Acceptance criteria
The MVP is complete when:
1. I can log in and access only the data permitted to my account.
2. The application can synchronise CVE data from the NVD API and persist it in PostgreSQL.
3. CVEs listed in the CISA KEV catalogue are clearly identified.
4. I can search, sort, filter, paginate, and open a detailed CVE view.
5. I can create and manage a personal inventory of software and tools.
6. I can link relevant CVEs to inventory items and see those relationships.
7. The application generates deduplicated in-app alerts for relevant new or updated vulnerabilities.
8. Synchronisation failures are handled safely and the last successful sync is visible.
9. Automated tests cover core behaviour and important access-control rules.
10. Another developer can follow the documentation to run the application locally.
7. Guidance for implementation
Build SentinelScope incrementally. Do not generate the entire application at once.

Before implementation, propose:
1. A simple architecture and data-flow diagram.
2. The initial data model and entity relationships.
3. The main security trust boundaries and threat model.
4. The development tools and local setup steps.
5. A phased implementation plan with testable milestones.
Explain the reasoning behind technical and security decisions. Keep the scope manageable, identify assumptions, and wait for my feedback at each major step.

Portfolio goal: demonstrate secure software engineering, vulnerability data handling, authentication and authorisation, testing, and DevSecOps practices—not merely a dashboard that displays public CVE data.