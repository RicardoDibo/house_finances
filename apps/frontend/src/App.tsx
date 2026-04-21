import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import { AuthProvider } from './context/AuthContext';
import Layout from './components/Layout';
import ProtectedRoute from './components/ProtectedRoute';
import LoginPage from './pages/LoginPage';
import PersonsPage from './pages/PersonsPage';
import CategoriesPage from './pages/CategoriesPage';
import TransactionsPage from './pages/TransactionsPage';
import TotalsPage from './pages/TotalsPage';
import RegisterUserPage from './pages/RegisterUserPage';

export default function App() {
  return (
    <AuthProvider>
      <BrowserRouter>
        <Routes>
          <Route path="/login" element={<LoginPage />} />

          {/* All authenticated routes */}
          <Route element={<ProtectedRoute />}>
            <Route path="/" element={<Layout />}>
              <Route index element={<Navigate to="/transactions" replace />} />
              <Route path="transactions" element={<TransactionsPage />} />
              <Route path="totals" element={<TotalsPage />} />

              {/* Admin-only routes */}
              <Route element={<ProtectedRoute requireAdmin />}>
                <Route path="persons" element={<PersonsPage />} />
                <Route path="categories" element={<CategoriesPage />} />
                <Route path="users" element={<RegisterUserPage />} />
              </Route>
            </Route>
          </Route>

          <Route path="*" element={<Navigate to="/transactions" replace />} />
        </Routes>
      </BrowserRouter>
    </AuthProvider>
  );
}
