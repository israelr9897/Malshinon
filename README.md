# 🚨 Malshinon

Malshinon is a professional-grade C# intelligence reporting and analysis system. It enables the gathering, tracking, and assessment of human intelligence (HUMINT) on individuals, with a robust alerting mechanism and seamless MySQL integration. Designed for security, research, or investigative environments, Malshinon empowers organizations to maintain situational awareness and proactively identify risks.

---

## ✨ Key Features

- **Structured Intelligence Reporting**
  - Submit, store, and retrieve detailed intelligence reports on persons of interest.
  - Each report links a reporter (agent) and a target (suspect or subject).

- **Automatic Identity Management**
  - Generates unique code names for every individual (reporter or target) for secure anonymization and reference.
  - Tracks roles: `reporter`, `target`, `both`, or `potential_agent`.

- **Real-Time Alerts & Risk Analysis**
  - Instant alerts when a target receives 10+ or 20+ reports.
  - Detects rapid reporting surges (e.g., 3+ reports in 15 minutes).
  - Identifies and highlights "potential agents" and "dangerous targets" using advanced logic.

- **Actionable Intelligence Dashboards**
  - Lists all potential agents and dangerous targets.
  - Shows alert history and statistics for in-depth analysis.

- **Modern Database Integration**
  - Persistent, reliable data storage using MySQL.
  - Efficient, scalable queries for large intelligence datasets.

- **Professional Command-Line Interface**
  - Clean, color-coded, and user-friendly console experience.
  - Validates and guides user input for operational integrity.

---

## 🗂️ Repository Structure

```
Malshinon/
│
├── DAl/                # Data Access Layer (MySQL operations)
│   ├── DalPeople.cs
│   ├── DalReport.cs
│   └── AuxiliaryFunctions.cs
│
├── Menu.cs             # User interface & main workflow
├── Program.cs          # Application entry point
├── mySqlConnect.cs     # Database connection management
└── ...
```

---

## ⚡ Quick Start

1. **Clone the Project**
   ```bash
   git clone https://github.com/israelr9897/Malshinon.git
   cd Malshinon
   ```

2. **Set Up MySQL**
   - Create a database named `Malshinon`.
   - Add the necessary tables: `peoples`, `intelReports`, `alerts`.
   - Update the connection string in `mySqlConnect.cs` with your credentials.

3. **Build & Run**
   - Open in Visual Studio or another C# IDE.
   - Restore packages (install `MySql.Data` via NuGet).
   - Build the solution and run.

---

## 📝 Example Workflow

- **Submit a Report:**  
  Enter your code name, target's full name, and report text. The system will handle identification, storage, and trigger alerts if thresholds are breached.

- **Get Intelligence Summaries:**  
  View lists of potential agents and dangerous targets, including statistics and alert histories.

- **Monitor Threats in Real Time:**  
  Receive color-coded alerts for abnormal activity directly in your console.

---

## 🔧 Requirements

- .NET (compatible version)
- MySQL Server
- [MySql.Data](https://www.nuget.org/packages/MySql.Data/) NuGet package

---

## 👤 Author

Developed by [israelr9897](https://github.com/israelr9897)

---

## 📄 License

*This repository currently does not specify a license.*

---

> **Malshinon** — Professional Intelligence Reporting & Risk Alert System
