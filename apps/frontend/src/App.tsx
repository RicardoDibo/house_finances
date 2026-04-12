import { BrowserRouter, Routes, Route, Navigate } from 'react-router-dom';
import Layout from './components/Layout';
import PersonsPage from './pages/PersonsPage';
import CategoriesPage from './pages/CategoriesPage';
import TransactionsPage from './pages/TransactionsPage';
import TotalsPage from './pages/TotalsPage';

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route index element={<Navigate to="/persons" replace />} />
          <Route path="persons" element={<PersonsPage />} />
          <Route path="categories" element={<CategoriesPage />} />
          <Route path="transactions" element={<TransactionsPage />} />
          <Route path="totals" element={<TotalsPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
