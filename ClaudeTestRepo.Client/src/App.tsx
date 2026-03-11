import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import { AuthProvider, useAuth } from "./context/AuthContext";
import LoginPage from "./pages/LoginPage";
import HomePage from "./pages/HomePage";
import ListPage from "./pages/ListPage";
import type { ReactNode } from "react";

function ProtectedRoute({ children }: { children: ReactNode }) {
  const { isLoggedIn } = useAuth();
  if (!isLoggedIn) return <Navigate to="/" replace />;
  return <>{children}</>;
}

function AppRoutes() {
  const { isLoggedIn } = useAuth();

  return (
    <Routes>
      <Route path="/" element={isLoggedIn ? <Navigate to="/lists" replace /> : <LoginPage />} />
      <Route path="/lists" element={<ProtectedRoute><HomePage /></ProtectedRoute>} />
      <Route path="/list/:id" element={<ProtectedRoute><ListPage /></ProtectedRoute>} />
    </Routes>
  );
}

function App() {
  return (
    <AuthProvider>
      <BrowserRouter>
        <AppRoutes />
      </BrowserRouter>
    </AuthProvider>
  );
}

export default App;
