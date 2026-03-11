import { createContext, useContext, useState, useEffect } from "react";
import type { ReactNode } from "react";

interface AuthContextType {
  token: string | null;
  username: string | null;
  setAuth: (token: string, username: string) => void;
  logout: () => void;
  isLoggedIn: boolean;
}

const AuthContext = createContext<AuthContextType>(null!);

export function AuthProvider({ children }: { children: ReactNode }) {
  const [token, setToken] = useState<string | null>(
    () => localStorage.getItem("token")
  );
  const [username, setUsername] = useState<string | null>(
    () => localStorage.getItem("username")
  );

  const setAuth = (token: string, username: string) => {
    localStorage.setItem("token", token);
    localStorage.setItem("username", username);
    setToken(token);
    setUsername(username);
  };

  const logout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("username");
    setToken(null);
    setUsername(null);
  };

  return (
    <AuthContext.Provider value={{ token, username, setAuth, logout, isLoggedIn: !!token }}>
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  return useContext(AuthContext);
}
