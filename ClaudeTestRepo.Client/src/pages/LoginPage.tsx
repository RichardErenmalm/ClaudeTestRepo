import { useState } from "react";
import { useAuth } from "../context/AuthContext";
import { login, register } from "../api/auth";
import { useNavigate } from "react-router-dom";

export default function LoginPage() {
  const [isRegister, setIsRegister] = useState(false);
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);
  const { setAuth } = useAuth();
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError("");

    if (!username.trim() || !password.trim()) {
      setError("Please fill in all fields.");
      return;
    }

    if (isRegister && password.length < 6) {
      setError("Password must be at least 6 characters.");
      return;
    }

    setLoading(true);
    try {
      const result = isRegister
        ? await register(username.trim(), password)
        : await login(username.trim(), password);
      setAuth(result.token, result.username);
      navigate("/lists");
    } catch (err: any) {
      setError(err.message || "Something went wrong.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div style={styles.wrapper}>
      <div style={styles.card}>
        <h1 style={styles.title}>{isRegister ? "Create Account" : "Welcome Back"}</h1>
        <p style={styles.subtitle}>
          {isRegister
            ? "Sign up to start organizing your lists"
            : "Log in to access your lists"}
        </p>

        <form onSubmit={handleSubmit} style={styles.form}>
          <div style={styles.field}>
            <label style={styles.label}>Username</label>
            <input
              type="text"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              style={styles.input}
              placeholder="Enter username"
              autoComplete="username"
            />
          </div>

          <div style={styles.field}>
            <label style={styles.label}>Password</label>
            <input
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              style={styles.input}
              placeholder="Enter password"
              autoComplete={isRegister ? "new-password" : "current-password"}
            />
          </div>

          {error && <p style={styles.error}>{error}</p>}

          <button type="submit" style={styles.submitBtn} disabled={loading}>
            {loading
              ? "..."
              : isRegister
              ? "Create Account"
              : "Log In"}
          </button>
        </form>

        <p style={styles.switch}>
          {isRegister ? "Already have an account?" : "Don't have an account?"}{" "}
          <button
            onClick={() => {
              setIsRegister(!isRegister);
              setError("");
            }}
            style={styles.switchBtn}
          >
            {isRegister ? "Log In" : "Sign Up"}
          </button>
        </p>
      </div>
    </div>
  );
}

const styles: Record<string, React.CSSProperties> = {
  wrapper: {
    minHeight: "100vh",
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    padding: "1.5rem",
  },
  card: {
    width: "100%",
    maxWidth: 420,
    backgroundColor: "var(--surface)",
    border: "1px solid var(--border)",
    borderRadius: "var(--radius)",
    padding: "2.5rem 2rem",
  },
  title: {
    fontSize: "1.8rem",
    fontWeight: 700,
    letterSpacing: "-0.02em",
    marginBottom: "0.25rem",
    textAlign: "center" as const,
  },
  subtitle: {
    color: "var(--text-muted)",
    fontSize: "0.95rem",
    textAlign: "center" as const,
    marginBottom: "2rem",
  },
  form: {
    display: "flex",
    flexDirection: "column" as const,
    gap: "1.25rem",
  },
  field: {
    display: "flex",
    flexDirection: "column" as const,
    gap: "0.4rem",
  },
  label: {
    fontSize: "0.85rem",
    fontWeight: 600,
    color: "var(--text-muted)",
  },
  input: {
    padding: "0.85rem 1rem",
    backgroundColor: "var(--bg)",
    border: "1px solid var(--border)",
    borderRadius: "var(--radius)",
    color: "var(--text)",
    fontSize: "1rem",
    outline: "none",
    transition: "border-color 0.2s",
  },
  error: {
    color: "var(--danger)",
    fontSize: "0.9rem",
    textAlign: "center" as const,
  },
  submitBtn: {
    padding: "0.85rem",
    backgroundColor: "var(--primary)",
    color: "#fff",
    border: "none",
    borderRadius: "var(--radius)",
    fontSize: "1rem",
    fontWeight: 600,
    cursor: "pointer",
    transition: "background-color 0.2s",
    marginTop: "0.5rem",
  },
  switch: {
    textAlign: "center" as const,
    color: "var(--text-muted)",
    fontSize: "0.9rem",
    marginTop: "1.5rem",
  },
  switchBtn: {
    background: "none",
    border: "none",
    color: "var(--primary)",
    fontSize: "0.9rem",
    fontWeight: 600,
    cursor: "pointer",
    textDecoration: "underline",
  },
};
